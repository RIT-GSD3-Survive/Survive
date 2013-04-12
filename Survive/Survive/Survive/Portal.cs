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
        protected Area linkTo;
        protected int linkX, linkY;

        public Portal(Rectangle loc)
            :base(loc)
        {

        }
    }
}
