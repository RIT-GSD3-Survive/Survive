using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class Weapon : Item
    {
        //Attributes
        protected int baseAttackPower;
        protected int baseWeight;
        protected int baseAccuracy;

        //Properties
        public int BaseAttackPower
        {
            get { return baseAttackPower; }
            set { baseAttackPower = value; }
        }

        public int BaseWeight
        {
            get { return baseWeight; }
            set { baseWeight = value; }
        }

        public int BaseAccuracy
        {
            get { return baseAccuracy; }
            set { baseAccuracy = value; }
        }
    }
}
