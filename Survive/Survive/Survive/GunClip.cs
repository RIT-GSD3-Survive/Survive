using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunClip : GunBits
    {
        public GunClip(int reloadSpeedAddition, int clipCapacityAddition)
        {
            reloadSpeed += reloadSpeedAddition;
            clipCapacity += clipCapacityAddition;
        }
    }
}
