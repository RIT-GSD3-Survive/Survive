using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class HealingItem : Item
    {
        protected int amount;
        protected string name;

        public HealingItem(int amt, string nm)
        {
            amount = amt;
            name = nm;
        }
    }
}
