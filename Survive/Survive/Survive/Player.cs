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
        string name;
        int number;
        double yVelocity = 0.0;
        Boolean onGround; //Prevents jumping while in air
        List<Item> items;
        List<Weapon> weapons;
        int hp;
        int maxHP;
        Weapon currentWeapon;
        AmmoItem currentClip;

        public Player(string nm, int num, Rectangle location)
        {
            this.location = location;
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

        public Boolean OnGround
        {
            get { return onGround; }
            set { onGround = value; }
        }

        public int HP
        {
            get { return hp; }
            set
            {
                if (hp > maxHP)
                    hp = maxHP;
                else
                    hp = value;
            }
        }

        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
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
        public void Gravity()
        {
            if(!onGround)
            {
                yVelocity += 1.6;
            }
        }

        public void PosUpdate()
        {
            Y += (int)Math.Round(yVelocity);
        }

        /*
        public void WalkGamePad(GamePadState pad)
        {
            if (pad.ThumbSticks.Left.X < 0)
            {
                this.X -= 2;
            }
            if (pad.ThumbSticks.Left.X > 0)
            {
                this.X += 2;
            }
        }
        */
        public void WalkLeft()
        {
            this.X -= 2;
        }
        public void WalkRight()
        {
            this.X += 2;
        }
        public void Jump()
        {
            if (onGround == true)
            {
                //onGround = false;
            }
        }
        public void Fire()
        {

        }
    }
}
