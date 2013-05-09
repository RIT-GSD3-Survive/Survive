using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class WeaponMelee : Weapon
    {
        //Attributes
        protected int range;
        protected int attackSpeed;

        //Properties
        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        public int AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }
    }
}
