﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class HealingItem : Item
    {
        public HealingItem(Rectangle loc)
        {
            location = loc;
        }
    }
}
