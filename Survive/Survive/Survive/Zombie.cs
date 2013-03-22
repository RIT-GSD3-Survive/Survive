using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class Zombie : Humanoid
    {
        //Attributes
        int attackPower;

        //Properties
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }
    }
}
