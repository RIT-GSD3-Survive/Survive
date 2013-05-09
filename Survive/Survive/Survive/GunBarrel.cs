using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class GunBarrel : GunBits
    {
        public GunBarrel(int accur, int ap, int weight, Rectangle loc)
        {
            accuracy = accur;
            attackPower = ap;
            this.weight= weight;
            location = loc;
        }
    }
}
