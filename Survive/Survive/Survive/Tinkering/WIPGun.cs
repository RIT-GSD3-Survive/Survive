using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Survive;

namespace Survive.Tinkering {
    class WIPGun {
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
                if(gun.clip != null && gun.scope != null) {
                    return new WeaponCustomizable(gun.body, gun.barrel, gun.stock, gun.scope, gun.clip);
                } else if(gun.clip != null) {
                    return new WeaponCustomizable(gun.body, gun.barrel, gun.stock, gun.clip);
                } else if(gun.scope != null) {
                    return new WeaponCustomizable(gun.body, gun.barrel, gun.stock, gun.scope);
                } else {
                    return new WeaponCustomizable(gun.body, gun.barrel, gun.stock);
                }
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
    }
}
