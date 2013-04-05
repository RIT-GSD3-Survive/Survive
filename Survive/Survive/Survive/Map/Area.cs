using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using fNbt;

namespace Survive {
    class Area {
        private String name = "";

        public String Name {
            get { return name; }
        }

        List<Point> map = new List<Point>();

        public Area() {

        }

        public Area(NbtCompound cmpd) {
            name = cmpd.Get<NbtString>("name").Value;
            NbtList ground = cmpd.Get<NbtList>("ground");
            foreach(NbtCompound alpha in ground) {
                int x = alpha.Get<NbtByte>("x").Value;
                int y = alpha.Get<NbtByte>("y").Value;
                map.Add(new Point(x, y));
            }
        }

        public void DrawArea(SpriteBatch sb) {
            foreach(Point alpha in map) {
                sb.Draw(Resources.Tiles, new Vector2((alpha.X - 1) * 32, (alpha.Y + 2) * 32 + 4), new Rectangle(32, 0, 32, 32), Color.White);
            }
        }
    }
}
