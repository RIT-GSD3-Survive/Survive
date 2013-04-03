using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Terrain
    {
        //Attributes
        protected Boolean collidable;
        protected Rectangle location;

        //Properties
        public Boolean Collidable
        {
            get { return collidable; }
            set { collidable = value; }
        }

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
