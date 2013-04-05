using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Humanoid : GameObject
    {
        //attributes
        protected int moveSpeed;
        protected int hp;
        protected int maxHP;
        protected double yVelocity = 0.0;
        protected Boolean onGround; //Prevents jumping while in air

        public Humanoid(Rectangle loc)
        {
            location = loc;
            maxHP = hp = 100;
            onGround = true;
        }

        //properties
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

        //methods
        public void Gravity()
        {
            if (!onGround)
            {
                yVelocity += .4;
            }
        }

        public void PosUpdate()
        {
            Y += (int)Math.Round(yVelocity);
        }

        public void WalkLeft() { this.X -= 2; }

        public void WalkRight() { this.X += 2; }

        public void Jump()
        {
            if (onGround == true)
            {
                onGround = false;
                yVelocity = -8;
            }
        }
        public void CheckCollisions(GameObject obj)
        {
            if (obj is Zombie)
            {
               
            }
            if (obj is Platform)
            {
                this.Y += obj.Y;
                onGround = true;
                yVelocity = 0;
            }
        }
    }
}
