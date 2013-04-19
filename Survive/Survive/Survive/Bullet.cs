using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Bullet: GameObject
    {
        //Attributes
        int direction; // 0 for left, 1 for right
        Boolean active; 

        //Constructor
        public Bullet(int dir, int x, int y)
        {
            direction = dir;
            active = true;
            location = new Rectangle(x, y, 3, 2);
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

        //Methods
        public void Move()
        {
            if (direction == 0)
            {
                this.X = this.X - 4;
            }
            if (direction == 1)
            {
                this.X = this.X + 4;
            }
        }
    }
}
