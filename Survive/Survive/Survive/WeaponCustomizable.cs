using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class WeaponCustomizable : Weapon
    {
        //Attributes
        protected string name;
        //Gun parts
        protected GunBarrel barrel;
        protected GunBody body;
        protected GunClip clip;
        protected GunScope scope;
        protected GunStock stock;
        //Gun attributes
        protected int accuracy;
        protected int weight;
        protected int attackPower;
        protected int reloadSpeed;
        protected int clipCapacity;

        //several overloads since scope and stock are optional
        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;

            accuracy += barrel.Accuracy + body.Accuracy + stock.Accuracy;
            weight += barrel.Weight + body.Weight + stock.Weight;
            attackPower += barrel.AttackPower + body.AttackPower;
            reloadSpeed += body.ReloadSpeed;
            clipCapacity += body.ClipCapacity;
        }

        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck, GunClip clp)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
            clip = clp;

            accuracy += barrel.Accuracy + body.Accuracy + stock.Accuracy;
            weight += barrel.Weight + body.Weight + stock.Weight;
            attackPower += barrel.AttackPower + body.AttackPower;
            reloadSpeed += body.ReloadSpeed + clip.ReloadSpeed;
            clipCapacity += body.ClipCapacity + clip.ClipCapacity;
        }

        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck, GunScope scp)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
            scope = scp;

            accuracy += barrel.Accuracy + body.Accuracy + stock.Accuracy + scope.Accuracy;
            weight += barrel.Weight + body.Weight + stock.Weight;
            attackPower += barrel.AttackPower + body.AttackPower;
            reloadSpeed += body.ReloadSpeed;
            clipCapacity += body.ClipCapacity;
        }

        public WeaponCustomizable(GunBody bdy, GunBarrel brrl, GunStock stck, GunScope scp, GunClip clp)
        {
            barrel = brrl;
            body = bdy;
            stock = stck;
            scope = scp;
            clip = clp;

            accuracy += barrel.Accuracy + body.Accuracy + stock.Accuracy + scope.Accuracy;
            weight += barrel.Weight + body.Weight + stock.Weight;
            attackPower += barrel.AttackPower + body.AttackPower;
            reloadSpeed += body.ReloadSpeed + clip.ReloadSpeed;
            clipCapacity += body.ClipCapacity + clip.ClipCapacity;
        }

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
