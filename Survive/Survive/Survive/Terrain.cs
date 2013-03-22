﻿using System;
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
        protected int x;
        protected int y;
        protected Boolean collidable;
        protected Rectangle location;

        //Properties
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

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