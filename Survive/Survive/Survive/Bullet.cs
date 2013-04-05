using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class Bullet: GameObject
    {
        //Attributes
        int direction; // 0 for left, 1 for right
        //Properties
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        //Constructor
        public Bullet(int dir)
        {
            direction = dir;
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
