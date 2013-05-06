using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Survive;

namespace Survive.Tinkering {
    class TinkerBackend {
        private WIPGun wip = null;

        private Player p;

        public TinkerBackend(Player p) {
            this.p = p;
        }

        public List<T> GetBits<T>() where T : GunBits {
            List<T> rtn = new List<T>();
            foreach(GunBits alpha in p.Items) {
                if(alpha is T) {
                    rtn.Add((T)alpha);
                }
            }

            return rtn;
        }

        public List<Weapon> GetWeapons() {
            return p.Weapons;
        }
    }
}
