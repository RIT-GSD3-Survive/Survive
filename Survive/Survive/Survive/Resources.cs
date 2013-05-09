using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Survive {
    public class Resources {
        private static SpriteFont courier, courierSmall;

        private static Texture2D tiles, portal, table, gunSheet;

        public static SpriteFont Courier {
            get { return courier; }
        }

        public static SpriteFont CourierSmall {
            get { return courierSmall; }
        }

        public static Texture2D Tiles {
            get { return tiles; }
        }

        public static Texture2D Portal {
            get { return portal; }
        }

        public static Texture2D Table {
            get { return table; }
        }

        public static Texture2D GunSheet {
            get { return gunSheet; }
        }

        public static void LoadRes(ContentManager cm) {
            courier = cm.Load<SpriteFont>("Courier");
            courierSmall = cm.Load<SpriteFont>("CourierSmall");
            tiles = cm.Load<Texture2D>("Tiles");
            portal = cm.Load<Texture2D>("Portal");
            table = cm.Load<Texture2D>("TinkerTableWIP");
            gunSheet = cm.Load<Texture2D>("Guns");
            Tinker.LoadRes(cm);
        }

        public class Tinker {
            private static Texture2D singleBack, newGun, scrollUp, scrollDn, box;

            public static void LoadRes(ContentManager cm) {
                singleBack = cm.Load<Texture2D>("TinkerScreen/BackButton");
                newGun = cm.Load<Texture2D>("TinkerScreen/NewGun");
                scrollUp = cm.Load<Texture2D>("TinkerScreen/ScrollUp");
                scrollDn = cm.Load<Texture2D>("TinkerScreen/ScrollDn");
                box = cm.Load<Texture2D>("TinkerScreen/Box");
            }

            public static Texture2D SingleBack {
                get { return singleBack; }
            }

            public static Texture2D NewGun {
                get { return newGun; }
            }

            public static Texture2D ScrollUp {
                get { return scrollUp; }
            }

            public static Texture2D ScrollDn {
                get { return scrollDn; }
            }

            public static Texture2D Box {
                get { return box; }
            }
        }
    }
}
