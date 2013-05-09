using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class GunClip : GunBits
    {
        int current; //Current amount of ammo in clip

        public GunClip(int reloadSpeed, int clipCapacity, Rectangle loc)
        {
            this.reloadSpeed = reloadSpeed;
            this.clipCapacity = clipCapacity;
            current = clipCapacity;
            location = loc;
        }

        public int Current
        {
            get { return current; }
            set { current = value; }
        }
    }
}
