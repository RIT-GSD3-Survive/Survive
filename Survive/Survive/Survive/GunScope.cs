using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunScope : GunBits
    {
        public GunScope(int accuracyAddition)
        {
            accuracy += accuracyAddition;
        }
    }
}
