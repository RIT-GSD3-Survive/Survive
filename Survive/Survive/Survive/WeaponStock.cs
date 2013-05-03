﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class WeaponStock : Weapon
    {
        public WeaponStock(string nm, int acc, int wei, int att, int rel, int cli, string typ, int fr)
        {
            name = nm;
            accuracy = acc;
            weight = wei;
            attackPower = att;
            reloadSpeed = rel;
            clipCapacity = cli;
            type = typ;
            fireRate = fr;
        }
    }
}
