using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class GunStock : GunBits
    {
        public GunStock(int accur, int weight, Rectangle loc)
        {
            accuracy = accur;
            this.weight = weight;
            location = loc;
        }
    }
}
