using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class GunBody : GunBits
    {
        public GunBody(int accur, int weight, int ap, int fr, int reloadSpeed, Rectangle loc)
        {
            accuracy = accur;
            this.weight = weight;
            attackPower = ap;
            fireRate = fr;
            this.reloadSpeed += reloadSpeed;
            this.clipCapacity = 1;
            location = loc;
        }
    }
}
