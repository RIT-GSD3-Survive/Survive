using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunBarrel : GunBits
    {
        public GunBarrel(int accur, int ap, int weight)
        {
            accuracy = accur;
            attackPower = ap;
            this.weight= weight;
        }
    }
}
