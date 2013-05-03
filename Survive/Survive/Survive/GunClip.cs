using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunClip : GunBits
    {
        int capacity; //Max capacity
        int current; //Current amount of ammo in clip

        public GunClip(int reloadSpeedAddition, int clipCapacityAddition)
        {
            this.reloadSpeed = reloadSpeed;
            this.clipCapacity = clipCapacity;
        }

        public int Current
        {
            get { return current; }
            set { current = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; }
        }
    }
}
