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

        //several overloads since scope and stock are optional
        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
        }

        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck, GunClip clp)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
            clip = clp;
        }

        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck, GunScope scp)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
            scope = scp;
        }

        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck, GunScope scp, GunClip clp)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
            scope = scp;
            clip = clp;
        }

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
