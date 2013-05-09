using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Survive;

namespace Survive.Tinkering {
    public class TinkerBackend {
        public enum BitType {
            Barrel, Body, Clip, Scope, Stock, None
        }

        private WIPGun wip = null;

        public WIPGun WIP {
            get { return wip; }
            set { wip = value; }
        }

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

        public void BeginGun() {
            wip = new WIPGun();
        }

        public void FinishGun() {
            p.Items.Remove(wip.Body);
            p.Items.Remove(wip.Barrel);
            p.Items.Remove(wip.Stock);
            if(wip.Scope != null) {
                p.Items.Remove(wip.Scope);
            }
            if(wip.Clip != null) {
                p.Items.Remove(wip.Clip);
            }

            p.Weapons.Add((WeaponCustomizable)wip);
            wip = null;
        }

        public void ModGun(WeaponCustomizable gun) {
            if(!p.Weapons.Contains(gun)) {
                return;
            } else {
                wip = gun;

                p.Weapons.Remove(gun);

                p.Items.AddRange(new GunBits[] { wip.Barrel, wip.Body, wip.Stock });
                if(wip.Clip != null) {
                    p.Items.Add(wip.Clip);
                }
                if(wip.Scope != null) {
                    p.Items.Add(wip.Scope);
                }
            }
        }
    }
}
