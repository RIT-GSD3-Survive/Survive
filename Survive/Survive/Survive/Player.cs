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
        protected int number;
        protected List<Item> items;
        protected List<Weapon> weapons;
        protected Weapon currentWeapon;
        protected GunClip currentClip;

        public Player(string nm, int num, Rectangle loc)
            : base(loc)
        {
            name = nm;
            number = num;
            items = new List<Item>();
            weapons = new List<Weapon>();
            moveSpeed = 2;

            weapons.Add(new WeaponStock("Beginner's Pistol",5,5,5,5,5));
            currentWeapon = weapons[0];
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

        public List<Item> Items
        {
            get { return items; }
        }

        // methods
        //returns a bullet to add to bulletList
        public Bullet Fire()
        {
            Bullet b = new Bullet(0, X+10, Y+10, currentWeapon.AttackPower+rgen.Next(5));
            return b;
        }

        public void PickUpItemCheck(Item item)
        {
            //make sure player is colliding with item
            if (this.location.Intersects(item.Location))
            {
                items.Add(item);
                item.Active = false;
            }
        }
    }
}
