using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class AmmoItem : Item
    {
        protected int amount;

        public AmmoItem(int amt, Rectangle loc)
        {
            location = loc;
            amount = amt;
        }

        //properties
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
