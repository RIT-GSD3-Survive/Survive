using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class GunBody : GunBits
    {
        public GunBody(int accur, int weight, int ap, int reloadSpeed, Rectangle loc)
        {
            accuracy = accur;
            this.weight = weight;
            attackPower = ap;
            this.reloadSpeed += reloadSpeed;
            this.clipCapacity = 1;
            location = loc;
        }
    }
}
