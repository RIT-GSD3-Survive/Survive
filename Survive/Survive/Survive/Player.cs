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
        double yVelocity = 0.0;
        Boolean onGround; //Prevents jumping while in air
        List<Item> items;
        List<Weapon> weapons;

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

        // methods
        public void Walk(GamePadState pad)
        {
            if (pad.ThumbSticks.Left.X < 0)
            {
                this.X -= 1;
            }
            if (pad.ThumbSticks.Left.X > 0)
            {
                this.X += 1;
            }
        }
        public void Jump(GamePadState pad)
        {
            if (onGround == true)
            {
                //onGround = false;
            }
        }
    }
}
