using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunStock : GunBits
    {
        public GunStock(int accuracyAddition, int weightAddition)
        {
            accuracy += accuracyAddition;
            weight += weightAddition;
        }
    }
}
