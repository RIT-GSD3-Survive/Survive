using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunBarrel : GunBits
    {
        public GunBarrel(int accuracyAddition, int attackPowerAddition, int charSpeedAddition)
        {
            accuracy += accuracyAddition;
            attackPower += attackPowerAddition;
            charSpeed += charSpeedAddition;
        }
    }
}
