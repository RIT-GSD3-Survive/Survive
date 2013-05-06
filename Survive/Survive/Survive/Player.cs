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
    class Player : Humanoid
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
        protected Control controls;
        protected int score;
        protected int ammo;
        protected int healingItemsAmount;
        protected int fireRateTimer;
        protected int reloadTimer;

        protected Portal move = null;

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


        public Portal Vote {
            get { return move; }
            set { move = value; }
        }

        public int HealingItemsAmount
        {
            get { return healingItemsAmount; }
            set { healingItemsAmount = value; }
        }

        // methods
        //call in the constructors so we don't have have duplicate lines in each
        public void SetUp(string nm, Rectangle loc)
        {
            healingItemsAmount = 0;
            score = 0;
            ammo = 0;
            name = nm;
            items = new List<GunBits>();
            weapons = new List<Weapon>();
            moveSpeed = 2;
            weaponIndex = 0;
            weapons.Add(new WeaponStock("Beginner's Pistol", 75, 1, 5, 5, 100000, "Pistol", 10, new Rectangle(0,0,0,0)));
            currentWeapon = weapons[weaponIndex];
            currentClip = new GunClip(currentWeapon.ReloadSpeed, currentWeapon.ClipCapacity);
            currentClip.Current = currentClip.ClipCapacity;
            fireRateTimer = 100 / CurrentWeapon.FireRate;
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
            reloadTimer = currentWeapon.ReloadSpeed * 60;
        }

        //returns a bullet to add to bulletList
        public Bullet Fire()
        {
            if (!(GlobalVariables.map.AtSafehouse))
            {
                if (currentClip.Current > 0 && fireRateTimer <= 0)
                {
                    if (currentWeapon.Name != "Beginner's Pistol")
                    {
                        currentClip.Current--;
                    }
                    fireRateTimer = 100 / CurrentWeapon.FireRate;
                    Bullet b = new Bullet((faceRight ? 1 : 0), X, Y + 32, currentWeapon.AttackPower + rgen.Next(5), currentWeapon.Accuracy);
                    return b;
                }
                else
                {
                    Reload();
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
            reloadTimer = currentWeapon.ReloadSpeed * 60;
            SwitchCurrentClip();
        }

        public void SwitchWeaponsPrevious()
        {
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
            reloadTimer = currentWeapon.ReloadSpeed * 60;
            SwitchCurrentClip();
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
            currentClip = new GunClip(currentWeapon.ReloadSpeed, currentWeapon.ClipCapacity);
            currentClip.Current = leftOverAmmo;
        }

        public void Reload()
        {
            GunClip best = null; 
            //check for filled clips
            for (int i = 0; i < reloadTimer; i++)
            {
            }
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
                currentClip = best;
            }
        }

        public void Interact() {
            List<Portal> portals = GlobalVariables.map.GetPortals();
            foreach(Portal alpha in portals) {
                if(alpha.Location.Intersects(this.Location)) {
                    move = alpha;
                    return;
                }
            }
        }

        public void FireRateTimer()
        {
            fireRateTimer--;
        }
    }
}
