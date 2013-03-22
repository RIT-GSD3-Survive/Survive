﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class AmmoItem : Item
    {
        protected int amount;
        protected string name;

        public AmmoItem(int amt, string nm)
        {
            amount = amt;
            name = nm;
        }
    }
}
