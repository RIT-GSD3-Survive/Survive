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
        string name;
        int number;
        List<Item> items;
        List<Weapon> weapons;
        Weapon currentWeapon;
        AmmoItem currentClip;

        public Player(string nm, int num, Rectangle loc)
            : base(loc)
        {
            name = nm;
            number = num;
            items = new List<Item>();
            weapons = new List<Weapon>();
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

        public AmmoItem CurrentClip
        {
            get { return currentClip; }
            set { currentClip = value; }
        }

        public List<Item> Items
        {
            get { return items; }
        }

        // methods
        public void Fire()
        {

        }

        public void PickUpItem(Item item)
        {
            items.Add(item);
            item.Active = false;
        }
    }
}
