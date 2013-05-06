using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class WeaponCustomizable : Weapon
    {
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

        public static implicit operator Survive.Tinkering.WIPGun(WeaponCustomizable gun) {
            Survive.Tinkering.WIPGun wip = new Tinkering.WIPGun();

            wip.Barrel = gun.barrel;
            wip.Body = gun.body;
            wip.Clip = gun.clip;
            wip.Scope = gun.scope;
            wip.Stock = gun.stock;

            return wip;
        }
    }
}