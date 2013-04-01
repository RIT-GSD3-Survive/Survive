using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class AmmoItem : Item
    {
        protected int amount;
        protected int amountTotal;
        protected string name;

        public AmmoItem(int amt, string nm)
        {
            amountTotal = amt;
            amount = amt;
            name = nm;
        }

        //properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int AmountTotal
        {
            get { return amountTotal; }
            set { amountTotal = value; }
        }
    }
}
