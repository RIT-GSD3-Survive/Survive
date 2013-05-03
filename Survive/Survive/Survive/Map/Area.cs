using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using fNbt;

namespace Survive {
    public class Area {
        private String name = "";

        public String Name {
            get { return name; }
        }

        List<Point> platformMap = new List<Point>();
        List<Portal> portals = new List<Portal>();

        public Area(NbtCompound cmpd) {
            name = cmpd.Get<NbtString>("name").Value;
            NbtList ground = cmpd.Get<NbtList>("ground");
            foreach(NbtCompound alpha in ground) {
                int x = alpha.Get<NbtByte>("x").Value;
                int y = alpha.Get<NbtByte>("y").Value;
                platformMap.Add(new Point(x, y));
            }
            Map.portalsToProcess[this] = cmpd.Get<NbtList>("portal").ToArray<NbtCompound>().ToList<NbtCompound>();
        }

        public void AddPortals(List<Portal> toAdd) {
            portals.AddRange(toAdd);
        }

        public void DrawArea(SpriteBatch sb) {
            foreach(Point alpha in platformMap) {
                int sheetX = 1, sheetY = 0;
                if(!platformMap.Contains(new Point(alpha.X - 1, alpha.Y))) {
                    sheetX = 3;
                } else if(!platformMap.Contains(new Point(alpha.X + 1, alpha.Y))) {
                    sheetX = 2;
                }
                if(platformMap.Contains(new Point(alpha.X, alpha.Y - 1))) {
                    sheetY = 1;
                    sheetX--;
                }
                sb.Draw(Resources.Tiles, new Vector2((alpha.X - 1) * 32, (alpha.Y + 2) * 32 + 4), new Rectangle(sheetX * 32, sheetY * 32, 32, 32), Color.White);
            }
            foreach(Portal alpha in portals) {
                alpha.Draw(sb);
            }
        }

        public List<Platform> GetTiles() {
            List<Platform> rtn = new List<Platform>();
            foreach(Point alpha in platformMap) {
                rtn.Add(new Platform(new Rectangle((alpha.X - 1) * 32, (alpha.Y + 2) * 32 + 4, 32, 32), new Vector2(1, 0)));
            }

            return rtn;
        }

        public List<Portal> GetPortals() {
            return portals;
        }
    }
}
