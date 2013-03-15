using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class WeaponCustomizable : Weapon
    {
        protected string name;
        protected GunBarrel barrel;
        protected GunBody body;
        protected GunClip clip;
        protected GunScope scope;
        protected GunStock stock;
    }
}
