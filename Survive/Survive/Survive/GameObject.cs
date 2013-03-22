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
        private int xPosition;
        private int yPosition;
        protected Rectangle location;

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
