using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class GunScope : GunBits
    {
        public GunScope(int accur, Rectangle loc)
        {
            accuracy = accur;
            location = loc;
        }
    }
}
