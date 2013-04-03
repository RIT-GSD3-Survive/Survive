﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Ground : Terrain
    {
        public Ground(Rectangle loc)
        {
            collidable = true;
            location = loc;
        }
    }
}
