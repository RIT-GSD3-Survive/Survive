﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class GunClip : GunBits
    {
        int current; //Current amount of ammo in clip

        public GunClip(int reloadSpeed, int clipCapacity)
        {
            this.reloadSpeed = reloadSpeed;
            this.clipCapacity = clipCapacity;
            current = clipCapacity;
        }

        public int Current
        {
            get { return current; }
            set { current = value; }
        }
    }
}
