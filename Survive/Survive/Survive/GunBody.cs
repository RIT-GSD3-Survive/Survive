using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunBody : GunBits
    {
        public GunBody(int accuracyAddition, int weightAddition, int attackPowerAddition, int reloadSpeedAddition, int clipCapacityAddition)
        {
            accuracy += accuracyAddition;
            weight += weightAddition;
            attackPower += attackPowerAddition;
            reloadSpeed += reloadSpeedAddition;
            clipCapacity += clipCapacityAddition;
        }
    }
}
