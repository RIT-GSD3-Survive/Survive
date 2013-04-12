using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class HealingItem : Item
    {
        protected int amount;
        protected string name;

        public HealingItem(int amt, string nm, Rectangle loc)
        {
            location = loc;
            amount = amt;
            name = nm;
        }
    }
}
