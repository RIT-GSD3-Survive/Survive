using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using fNbt;

namespace MapEditor {
    class Area {
        private String name = "";

        private int buttonScrollX = 0;
        private int buttonWidth = 0;

        public static Texture2D cap, mid, tiles;

        public String Name {
            get { return name; }
            set { name = value; }
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

        public void AddTile(int x, int y) {
            if(!map.Contains(new Point(x, y)) && InXRange(x) && InYRange(y)) {
                map.Add(new Point(x, y));
            }
        }

        public void RemoveTile(int x, int y) {
            if(map.Contains(new Point(x, y))) {
                map.Remove(new Point(x, y));
            }
        }

        public void DrawArea(SpriteBatch sb) {
            int x = -32;
            while(x < 800) {
                x += 32;
                sb.Draw(tiles, new Vector2(x, 420), new Rectangle(0, 0, 32, 32), Color.White);
                sb.Draw(tiles, new Vector2(x, 452), new Rectangle(128, 0, 32, 32), Color.White);
                sb.Draw(tiles, new Vector2(x, 484), new Rectangle(128, 0, 32, 32), Color.White);
            }
            foreach(Point alpha in map) {
                sb.Draw(tiles, new Vector2((alpha.X - 1) * 32, (alpha.Y + 2) * 32 + 4), new Rectangle(32, 0, 32, 32), Color.White);
            }
        }

        public int DrawButton(SpriteBatch sb, SpriteFont sf, int x, Color button) {
            int strLength = (int)Math.Ceiling(sf.MeasureString(name).X);

            buttonScrollX = x;

            int y = GlobalVars.view.Height - 32;
            int padding = 3;

            sb.Draw(cap, new Vector2(x + padding, y), button);
            sb.Draw(mid, new Rectangle(x + padding + 1, y, strLength + (padding * 2), 32), button);
            sb.Draw(cap, new Vector2(x + (padding * 3) + strLength + 1, y), button);
            sb.DrawString(sf, name, new Vector2(x + (padding * 2) + 1, y + 2), Color.Black);

            buttonWidth = padding * 2 + strLength;

            return padding * 4 + strLength + 2;
        }

        public bool ButtonClicked(int x, int y) {
            return new Rectangle(buttonScrollX + 3, GlobalVars.view.Height - 32, buttonWidth, 32).Contains(new Point(x, y));
        }

        private bool InXRange(int x) {
            return x > 0 && x <= 25;
        }

        private bool InYRange(int y) {
            return y > 0 && y <= 10;
        }

        public NbtCompound Save() {
            NbtCompound rtn = new NbtCompound();

            rtn.Add(new NbtString("name", name));

            NbtList ground = new NbtList("ground", NbtTagType.Compound);
            foreach(Point alpha in map) {
                ground.Add(new NbtCompound(new NbtByte[] { new NbtByte("x", (byte)alpha.X), new NbtByte("y", (byte)alpha.Y) }));
            }

            NbtList portal = new NbtList("portal", NbtTagType.Compound);
            // Not implemented, no save

            NbtList decor = new NbtList("decor", NbtTagType.Compound);
            // Not implemented, no save

            rtn.AddRange(new NbtList[] { ground, portal, decor });

            return rtn;
        }
    }
}
