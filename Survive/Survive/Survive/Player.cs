﻿using System;
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
        protected GunClip nextClip;
        protected Control controls;
        protected int score;
        protected int ammo;
        protected int healingItemsAmount;
        protected int fireRateTimer;
        protected int reloadTimer;
        protected Boolean reloading;

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

        // methods
        //call in the constructors so we don't have have duplicate lines in each
        public void SetUp(string nm, Rectangle loc)
        {
            reloading = false;
            healingItemsAmount = 0;
            score = 0;
            ammo = 1000;
            name = nm;
            items = new List<GunBits>();
            weapons = new List<Weapon>();
            moveSpeed = 2;
            weaponIndex = 0;
            weapons.Add(new WeaponStock("Beginner's Pistol", 75, 1, 10, 5, 1, "Pistol", 10, new Rectangle(0,0,0,0)));
            currentWeapon = weapons[weaponIndex];
            currentClip = new GunClip(currentWeapon.ReloadSpeed, currentWeapon.ClipCapacity, new Rectangle(0,0,0,0));
            nextClip = null;
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
            foreach (GunClip clip in items)
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
            if (fireRateTimer < 0)
            {
                fireRateTimer = 0;
            }
        }
    }
}
