using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class Humanoid : GameObject
    {
        //attributes
        protected int moveSpeed;
        protected int hp;
        protected int maxHP;
        protected double yVelocity = 0.0;
        protected Boolean falling; //Allows falling off higher level platforms
        protected Boolean jumping; //Prevents jumping while in air
        protected Boolean invulnerable; //Prevents the player from taking multiple hits from zombies
        protected Boolean faceRight = true; //True: Facing right.  False: Facing left.
        protected int invulnerableTimer;

        public Humanoid(Rectangle loc)
        {
            location = loc;
            maxHP = hp = 100;
            falling = true;
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
        public int MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        /// <summary>
        /// Returns whether or not this humanoid's facing right.  True = Facing right.  False = Facing left.
        /// </summary>
        public Boolean FacingRight {
            get { return faceRight; }
            set { faceRight = value; }
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

        public void WalkLeft() { this.X -= (int)moveSpeed; faceRight = false; }

        public void WalkRight() { this.X += (int)moveSpeed; faceRight = true; }

        public void Jump()
        {
            if (jumping == false)
            {
                jumping = true;
                falling = true;
                yVelocity = -8;
            }
        }
        public void CheckCollisions(GameObject obj, GameObject objCheckingCollision)
        {
            //Bullet hits zombie
            if (obj is Bullet && objCheckingCollision is Zombie)
            {
                if (this.Location.Intersects(obj.Location))
                {
                    //zombie turns to face bullet
                    if (((Zombie)objCheckingCollision).FacingRight == true && ((Bullet)obj).Direction==1) //both right
                        ((Zombie)objCheckingCollision).FacingRight = false;
                    if(rgen.Next(0,100) < ((Bullet)obj).Accuracy)
                    {
                        ((Zombie)objCheckingCollision).HP -= ((Bullet)obj).Damage;
                    }
                    ((Bullet)obj).Active = false;
                }
            }
            //zombie hits player
            else if (obj is Zombie && objCheckingCollision is Player)
            {
                if (this.Location.Intersects(obj.Location) && invulnerable == false)
                {
                    HP -= ((Zombie)obj).AttackPower;
                    invulnerable = true;
                }
            }
            //humanoid hits platform
            else if (obj is Platform)
            {
                //if (falling == true)
                //{
                if (this.Location.Intersects(obj.Location))
                {
                    if (this.Y <= obj.Y && this.Y + this.Location.Height >= obj.Y + obj.Location.Height && this.X + this.Location.Width >= obj.X + obj.Location.Width)
                    {
                        this.X += this.moveSpeed;
                        if (this is Zombie)
                        {
                            //if (this.Y > objCheckingCollision.Y)
                            //{
                                this.Jump();
                                this.PosUpdate();
                                this.Gravity();
                            //}
                        }
                    }
                    else if (this.Y <= obj.Y && this.Y + this.Location.Height >= obj.Y + obj.Location.Height && this.X <= obj.X)
                    {
                        this.X -= moveSpeed;
                        if (this is Zombie)
                        {
                            //if (this.Y > objCheckingCollision.Y)
                            //{
                                this.Jump();
                                this.PosUpdate();
                                this.Gravity();
                            //}
                        }
                    }
                    
                    else if (this.Y < obj.Y + obj.Location.Height && this.Y > obj.Y)
                    {
                        this.Y = obj.Y + obj.Location.Height;
                        falling = true;
                        jumping = true;
                        yVelocity = 0;
                    }
                    
                    else if (this.Y + this.Location.Height > obj.Y && this.Y + this.Location.Height < obj.Y + obj.Location.Height)
                    {
                        this.Y = obj.Y - this.Location.Height;
                        falling = true;
                        jumping = false;
                        yVelocity = 0;
                    }
                    //}
                }
            }
            if (X > (800-this.Location.Width))
            {
                X = (800 - this.Location.Width);
            }
            if (X < 0)
            {
                X = 0;
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