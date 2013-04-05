using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Terrain:GameObject
    {
        //Attributes
        protected Boolean collidable;

        public Terrain(Rectangle loc)
        {
            location = loc;
        }

        //Properties
        public Boolean Collidable
        {
            get { return collidable; }
            set { collidable = value; }
        }
    }
}
