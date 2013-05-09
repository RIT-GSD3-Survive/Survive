using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Survive
{
    public class Player : Humanoid
    {
        //Attributes
        protected string name;
        protected int weaponIndex;
        protected int number;
        protected PlayerIndex pi;
        protected List<GunBits> items;
        protected List<Weapon> weapons;
        protected Weapon currentWeapon;
        protected GunClip currentClip;
        protected GunClip nextClip;
        protected Control controls;
        protected int score;
        protected int ammo;
        protected int healingItemsAmount;
        protected int fireRateTimer;
        protected int reloadTimer;
        protected Boolean reloading;

        protected Portal move = null;
        protected Boolean tinkering = false;

        private Tinkering.TinkerBackend tBackend;

        public Player(string nm, PlayerIndex num, Rectangle loc)
            : base(loc)
        {
            pi = num;
            controls = new Control(num);
            SetUp(nm, loc);
        }

        public Player(string nm, int num, Rectangle loc)
            : base(loc)
        {
            number = num;
            controls = new Control();
            SetUp(nm, loc);
        }

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        
        public PlayerIndex PIndex
        {
            get { return pi; }
            set { pi = value; }
        }

        public Weapon CurrentWeapon
        {
            get { return currentWeapon; }
            set { currentWeapon = value; }
        }

        public GunClip CurrentClip
        {
            get { return currentClip; }
            set { currentClip = value; }
        }

        public GunClip NextClip
        {
            get { return nextClip; }
            set { nextClip = value; }
        }

        public List<GunBits> Items
        {
            get { return items; }
        }

        public List<Weapon> Weapons {
            get { return weapons; }
        }

        public Control Controls
        {
            get { return controls; }
            set { controls = value; }
        }

        public int Ammo
        {
            get { return ammo; }
            set { ammo = value;}
        }

        public Portal Vote {
            get { return move; }
            set { move = value; }
        }

        public int HealingItemsAmount
        {
            get { return healingItemsAmount; }
            set { healingItemsAmount = value; }
        }

        public Boolean Reloading
        {
            get { return reloading; }
            set { reloading = value; }
        }

        public int ReloadTimer
        {
            get { return reloadTimer; }
            set { reloadTimer = value; }
        }

        public Boolean Tinkering {
            get { return tinkering; }
            set { tinkering = value; }
        }

        public Tinkering.TinkerBackend TBackend {
            get { return tBackend; }
        }

        // methods
        //call in the constructors so we don't have have duplicate lines in each
        public void SetUp(string nm, Rectangle loc)
        {
            tBackend = new Tinkering.TinkerBackend(this);
            reloading = false;
            healingItemsAmount = 0;
            score = 0;
            ammo = 1000;
            name = nm;
            items = new List<GunBits>();
            weapons = new List<Weapon>();
            weaponIndex = 0;
            weapons.Add(new WeaponStock("Beginner's Pistol", 75, 1, 10, 5, 1, "Pistol", 10, new Rectangle(0,0,0,0)));
            currentWeapon = weapons[weaponIndex];
            currentClip = new GunClip(currentWeapon.ReloadSpeed, currentWeapon.ClipCapacity, new Rectangle(0,0,0,0));
            nextClip = null;
            currentClip.Current = currentClip.ClipCapacity;
            fireRateTimer = 100 / CurrentWeapon.FireRate;
            moveSpeed = 4;
            reloadTimer = 0;
        }

        //returns a bullet to add to bulletList
        public Bullet Fire()
        {
            if (!(GlobalVariables.map.AtSafehouse) && reloading == false)
            {
                if ((currentClip.Current > 0 || (currentWeapon.Name.Equals("Beginner's Pistol"))) && fireRateTimer <= 0)
                {
                    if (!(currentWeapon.Name.Equals("Beginner's Pistol")))
                    {
                        currentClip.Current--;
                    }
                    fireRateTimer = 100 / CurrentWeapon.FireRate;
                    Bullet b = new Bullet((faceRight ? 1 : 0), X, Y + 32, currentWeapon.AttackPower + rgen.Next(5), currentWeapon.Accuracy);
                    return b;
                }
                else if (currentClip.Current <= 0 && !(currentWeapon.Name.Equals("Beginner's Pistol")))
                {
                    FindNextBestClip();
                    Reload();
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void PickUpItemCheck(Item item)
        {
            //make sure player is colliding with item
            if (this.location.Intersects(item.Location))
            {
                if (item is AmmoItem)
                {
                    ammo += rgen.Next(100);
                }
                else if (item is HealingItem)
                {
                    healingItemsAmount++;
                }
                else if (item is Weapon)
                {
                    weapons.Add((Weapon)item);
                    items.Add(((Weapon)item).Clip);
                }
                else if(item is GunBits)
                {
                    items.Add((GunBits)item);
                }
                item.Active = false;
            }
        }

        public void SwitchWeaponsNext()
        {
            reloading = false;
            reloadTimer = 0;
            weaponIndex += 1;
            if (weaponIndex > weapons.Count - 1)
            {
                weaponIndex = 0;
            }
            currentWeapon = weapons[weaponIndex];
            fireRateTimer = 100 / currentWeapon.FireRate;
            if (currentWeapon.Weight >= 0 && currentWeapon.Weight <= 5)
            {
                moveSpeed = 4;
            }
            else if (currentWeapon.Weight >= 6 && currentWeapon.Weight <= 10)
            {
                moveSpeed = 3;
            }
            else if (currentWeapon.Weight >= 11 && currentWeapon.Weight <= 15)
            {
                moveSpeed = 2;
            }
            else if (currentWeapon.Weight >= 16 && currentWeapon.Weight <= 20)
            {
                moveSpeed = 1;
            }
            currentClip = currentWeapon.Clip;
            //SwitchCurrentClip();
        }

        public void SwitchWeaponsPrevious()
        {
            reloading = false;
            reloadTimer = 0;
            weaponIndex -= 1;
            if (weaponIndex < 0)
            {
                weaponIndex = weapons.Count - 1;
            }
            currentWeapon = weapons[weaponIndex];
            fireRateTimer = 100 / currentWeapon.FireRate;
            if (currentWeapon.Weight >= 0 && currentWeapon.Weight <= 5)
            {
                moveSpeed = 4;
            }
            else if (currentWeapon.Weight >= 6 && currentWeapon.Weight <= 10)
            {
                moveSpeed = 3;
            }
            else if (currentWeapon.Weight >= 11 && currentWeapon.Weight <= 15)
            {
                moveSpeed = 2;
            }
            else if (currentWeapon.Weight >= 16 && currentWeapon.Weight <= 20)
            {
                moveSpeed = 1;
            }
            currentClip = currentWeapon.Clip;
            //SwitchCurrentClip();
        }

        private void SwitchCurrentClip()
        {
            //put current ammo into new gun
            int leftOverAmmo = currentClip.Current;
            if (leftOverAmmo > currentWeapon.ClipCapacity) //too much ammo for current clip, convert back to ammo
            {
                int excessAmmo = leftOverAmmo - currentWeapon.ClipCapacity;
                ammo += excessAmmo;
                leftOverAmmo -= excessAmmo;
            }
            currentClip = currentWeapon.Clip;
            currentClip.Current = leftOverAmmo;
        }

        /// <summary>
        /// Used to refill the clips when the player is in the Safehouse
        /// </summary>
        public void RefillClips()
        {
            int missingAmmo;
            List<GunClip> clips = tBackend.GetBits<GunClip>();
            foreach (GunClip clip in clips)
            {
                missingAmmo = clip.ClipCapacity - clip.Current;
                if (ammo >= missingAmmo)
                {
                    clip.Current = clip.ClipCapacity;
                    ammo -= missingAmmo;
                }
                else if (ammo > 0 && (clip.Current + ammo < clip.ClipCapacity))
                {

                    clip.Current += ammo;
                    ammo = 0;
                }
            }
        }

        public void FindNextBestClip()
        {
            if (nextClip == null)
            {
                GunClip best = null;
                foreach (GunBits gunbit in items)
                {
                    if (gunbit is GunClip)
                    {
                        if (best == null)
                            best = (GunClip)gunbit;
                        else
                            if (((GunClip)gunbit).Current > best.Current)
                                best = (GunClip)gunbit;
                    }
                }

                if (best != null) //best clip found
                {
                    nextClip = best;
                }
            }
        }

        public void Reload()
        {
            if(GlobalVariables.map.AtSafehouse) return;
            reloading = true;
            //check for filled clips
            if (reloadTimer >= (nextClip.ReloadSpeed*60))
            {
                currentClip = nextClip;
                nextClip = null;
                reloadTimer = 0;
                reloading = false;
            }
        }

        public void Interact() {
            if(GlobalVariables.map.AtSafehouse && GlobalVariables.tableLoc.Intersects(this.Location)) {
                tinkering = true;
            } else {
                List<Portal> portals = GlobalVariables.map.GetPortals();
                foreach(Portal alpha in portals) {
                    if(alpha.Location.Intersects(this.Location)) {
                        move = alpha;
                        return;
                    }
                }
            }
        }

        public void FireRateTimer()
        {
            fireRateTimer--;
            if (fireRateTimer < 0)
            {
                fireRateTimer = 0;
            }
        }

        public void Update(GameTime gt, List<Platform> ptl, List<Bullet> bl) {
            //Weapon Info printed to Console
            Console.WriteLine("Player: " + PIndex.ToString());
            Console.WriteLine("Ammo: " + Ammo);
            Console.WriteLine("Reloading: " + Reloading);
            Console.WriteLine("Time spent reloading: " + ReloadTimer);
            Console.WriteLine("Current Weapon Info:");
            Console.WriteLine("Type: " + CurrentWeapon.Type);
            Console.WriteLine("Power: " + CurrentWeapon.AttackPower);
            Console.WriteLine("Fire Rate: " + CurrentWeapon.FireRate);
            Console.WriteLine("Accuracy: " + CurrentWeapon.Accuracy);
            Console.WriteLine("Ammo in Clip: " + CurrentClip.Current);
            Console.WriteLine("Clip Capacity: " + CurrentClip.ClipCapacity);
            Console.WriteLine("Weight: " + CurrentWeapon.Weight);
            Console.WriteLine("Inventory: " + Items.Count);
            Console.WriteLine();
            if(NextClip != null) {
                Console.WriteLine("Reload Speed (Next Clip): " + NextClip.ReloadSpeed);
            }
            if(Controls.MoveRight()) {
                WalkRight();
                Vote = null;
            }
            if(Controls.MoveLeft()) {
                WalkLeft();
                Vote = null;
            }
            if(Controls.IsJump()) {
                Jump();
                Vote = null;
            }
            if(Controls.IsFire()) {
                Bullet b = null;
                if((b = Fire()) != null)
                    bl.Add(b);
            }
            if(Controls.Interact()) {
                Interact();
            }
            if(Controls.Pause()) {
                GlobalVariables.gameState = GlobalVariables.GameState.Pause;
            }
            if(Controls.Reload()) {
                FindNextBestClip();
                Reload();
            }
            if(Controls.SwitchWeaponsNext()) {
                SwitchWeaponsNext();
            }
            if(Controls.SwitchWeaponsPrevious()) {
                SwitchWeaponsPrevious();
            }
            if(Controls.Heal()) {
                if(HealingItemsAmount > 0 && HP != 100) {
                    HealingItemsAmount--;
                    HP += rgen.Next(10, 30);
                }
            }
            Gravity();
            PosUpdate();
            InvulnerabilityTimer();
            FireRateTimer();
            if(Reloading) {
                FindNextBestClip();
                ReloadTimer++;
                Reload();
            }
            for(int i = 50; i < 75; i++) {
                CheckCollisions(ptl[i], this);
            }
            foreach(Platform pl in GlobalVariables.map.GetTiles()) {
                CheckCollisions(pl, this);
            }
        }
    }
}
