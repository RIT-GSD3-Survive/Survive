using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Survive;

namespace Survive.Tinkering {
    public class WIPGun {
        public WIPGun() {

        }

        private GunBody body = null;
        private GunBarrel barrel = null;
        private GunScope scope = null;
        private GunStock stock = null;
        private GunClip clip = null;

        public static explicit operator WeaponCustomizable(WIPGun gun) {
            if(gun.body == null || gun.barrel == null || gun.stock == null) {
                throw new Exception("A required component is missing!");
            } else {
                WeaponCustomizable newWeap;
                if(gun.clip != null && gun.scope != null) {
                    newWeap = new WeaponCustomizable(gun.body, gun.barrel, gun.stock, gun.scope, gun.clip);
                } else if(gun.clip != null) {
                    newWeap = new WeaponCustomizable(gun.body, gun.barrel, gun.stock, gun.clip);
                } else if(gun.scope != null) {
                    newWeap = new WeaponCustomizable(gun.body, gun.barrel, gun.stock, gun.scope);
                } else {
                    newWeap = new WeaponCustomizable(gun.body, gun.barrel, gun.stock);
                }
                newWeap.Name = "CustWeapon";
                newWeap.Type = "AR";
                return newWeap;
            }
        }

        public GunBody Body {
            get { return body; }
            set { body = value; }
        }

        public GunBarrel Barrel {
            get { return barrel; }
            set { barrel = value; }
        }

        public GunStock Stock {
            get { return stock; }
            set { stock = value; }
        }

        public GunScope Scope {
            get { return scope; }
            set { scope = value; }
        }

        public GunClip Clip {
            get { return clip; }
            set { clip = value; }
        }

        public int Accuracy {
            get {
                int rtn = 0;
                if(barrel != null) rtn += barrel.Accuracy;
                if(body != null) rtn += body.Accuracy;
                if(stock != null) rtn += stock.Accuracy;
                if(scope != null) rtn += scope.Accuracy;
                return rtn;
            }
        }

        public int AttackPower {
            get {
                int rtn = 0;
                if(barrel != null) rtn += barrel.AttackPower;
                if(body != null) rtn += body.AttackPower;
                return rtn;
            }
        }

        public int FireRate {
            get { if(body != null) { return body.FireRate; } else { return 0; } }
        }

        public int Weight {
            get {
                int rtn = 0;
                if(barrel != null) rtn += barrel.Weight;
                if(body != null) rtn += body.Weight;
                if(stock != null) rtn += stock.Weight;
                return rtn;
            }
        }
    }
}
