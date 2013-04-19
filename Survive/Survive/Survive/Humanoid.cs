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
        protected double moveSpeed;
        protected int hp;
        protected int maxHP;
        protected double yVelocity = 0.0;
        protected Boolean falling; //Allows falling off higher level platforms
        protected Boolean jumping; //Prevents jumping while in air
        protected Boolean invulnerable; //Prevents the player from taking multiple hits from zombies
        protected int invulnerableTimer;

        public Humanoid(Rectangle loc)
        {
            location = loc;
            maxHP = hp = 100;
            falling = false;
            jumping = false;
            invulnerable = true;
            invulnerableTimer = 0;
        }

        //properties
        public Boolean Invulnerable
        {
            get { return invulnerable; }
            set { invulnerable = value; }
        }

        public Boolean Falling
        {
            get { return falling; }
            set { falling = value; }
        }

        public Boolean Jumping
        {
            get { return jumping; }
            set { jumping = value; }
        }

        public int HP
        {
            get { return hp; }
            set
            {
                if (value > maxHP)
                    hp = maxHP;
                else if (value < 0)
                    hp = 0;
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
            if (falling)
            {
                yVelocity += .4;
            }
        }

        public void PosUpdate()
        {
            Y += (int)Math.Round(yVelocity);
        }

        public void WalkLeft() { this.X -= (int)moveSpeed; }

        public void WalkRight() { this.X += (int)moveSpeed; }

        public void Jump()
        {
            if (jumping == false)
            {
                jumping = true;
                falling = true;
                yVelocity = -8;
            }
        }
        public void CheckCollisions(GameObject obj)
        {
            if (obj is Zombie)
            {
                if (this.Location.Intersects(obj.Location) && invulnerable == false)
                {
                    HP -= 20;
                    invulnerable = true;
                }
            }
            if (obj is Platform)
            {
                if (falling == true)
                {
                    if (this.Location.Intersects(obj.Location))
                    {
                        
                        if(this.Y < obj.Y && this.Y + this.Location.Height > obj.Y + obj.Location.Height && this.X + this.Location.Width > obj.X + obj.Location.Width)
                        {
                            this.X += 2;
                        }
                        else if (this.Y < obj.Y && this.Y + this.Location.Height > obj.Y + obj.Location.Height && this.X < obj.X)
                        {
                            this.X -= 2;
                        }
                        /*
                        else if (this.Y < obj.Y + obj.Location.Height && this.Y > obj.Y)
                        {
                            this.Y = obj.Y + obj.Location.Height;
                            falling = true;
                            jumping = true;
                            yVelocity = 0;
                        }
                        */
                        else if (this.Y + this.Location.Height > obj.Y && this.Y + this.Location.Height < obj.Y + obj.Location.Height)
                        {
                            this.Y = obj.Y - this.Location.Height;
                            falling = true;
                            jumping = false;
                            yVelocity = 0;
                        }
                    }
                }
            }
        }

        public void InvulnerabilityTimer()
        {
            if (invulnerable == true)
            {
                invulnerableTimer++;
                if (invulnerableTimer >= 120)
                {
                    invulnerable = false;
                    invulnerableTimer = 0;
                }
            }
        }
    }
}
