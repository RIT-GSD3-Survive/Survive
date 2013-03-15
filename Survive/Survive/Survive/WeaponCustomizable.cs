using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class WeaponCustomizable : Weapon
    {
        protected string name;
        //Gun parts
        protected GunBarrel barrel;
        protected GunBody body;
        protected GunClip clip;
        protected GunScope scope;
        protected GunStock stock;
        //Gun attributes
        protected int accuarcy;
        protected int charSpeed;
        protected int weight;
        protected int attackPower;
        protected int reloadSpeed;
        protected int clipCapacity;
    }
}
