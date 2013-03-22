using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class WeaponMelee : Weapon
    {
        //Attributes
        protected string name;
        protected int range;

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Range
        {
            get { return range; }
            set { range = value; }
        }
    }
}
