using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunBody : GunBits
    {
        public GunBody(int accur, int weight, int ap, int reloadSpeed)
        {
            accuracy = accur;
            this.weight = weight;
            attackPower = ap;
            this.reloadSpeed += reloadSpeed;
            this.clipCapacity = 1;
        }
    }
}
