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
        protected int accuracy;
        protected int charSpeed;
        protected int weight;
        protected int attackPower;
        protected int reloadSpeed;
        protected int clipCapacity;

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }

        public int ReloadSpeed
        {
            get { return reloadSpeed; }
            set { reloadSpeed = value; }
        }

        public int ClipCapacity
        {
            get { return clipCapacity; }
            set { clipCapacity = value; }
        }
    }
}
