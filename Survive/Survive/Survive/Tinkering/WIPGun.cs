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

        public static implicit operator WeaponCustomizable(WIPGun gun) {
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
    }
}
