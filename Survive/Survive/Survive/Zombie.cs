﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
