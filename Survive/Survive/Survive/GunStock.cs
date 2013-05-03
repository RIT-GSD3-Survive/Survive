using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunStock : GunBits
    {
        public GunStock(int accur, int weight)
        {
            accuracy = accur;
            this.weight = weight;
        }
    }
}
