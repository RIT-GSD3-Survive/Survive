using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Portal : Terrain
    {
        //attributes
        protected Area linkTo;
        protected int linkX, linkY;

        public Portal(Rectangle loc, Area to, int xlink, int ylink)
            :base(loc)
        {
            linkTo = to;
            linkX = xlink;
            linkY = ylink;
        }

        //properties
        public Area LinkTo
        {
            get { return linkTo; }
            set { linkTo = value; }
        }

        public int LinkX
        {
            get { return linkX; }
            set { linkX = value; }
        }

        public int LinkY
        {
            get { return linkY; }
            set { linkY = value; }
        }
    }
}
