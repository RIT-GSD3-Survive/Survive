﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class Bullet : GameObject
    {
        //Attributes
        protected int direction; // 0 for left, 1 for right
        protected Boolean active;
        protected int damage; //gotten from fired weapon
        protected int accuracy;

        //Constructor
        public Bullet(int dir, int x, int y, int dam, int acc)
        {
            direction = dir;
            active = true;
            location = new Rectangle(x, y, 2, 1);
            damage = dam;
            accuracy = acc;
        }

        //Properties
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Boolean Active
        {
            get { return active; }
            set { active = value; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public int Accuracy
        {
            get { return accuracy; }
        }

        //Methods
        public void Move()
        {
            if (direction == 0)
            {
                this.X = this.X - 6;
            }
            if (direction == 1)
            {
                this.X = this.X + 6;
            }
        }
    }
}
