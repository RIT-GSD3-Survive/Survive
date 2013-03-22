using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    abstract class GameObject
    {
        protected Rectangle location;

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }

        public int X
        {
            get { return location.X; }
            set { location = new Rectangle(value, location.Y, location.Width, location.Height); }
        }

        public int Y
        {
            get { return location.Y; }
            set { location = new Rectangle(location.X, value, location.Width, location.Height); }
        }
    }
}
