using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor {
    class Area {
        private String name;

        public static Texture2D cap, mid;

        public String Name {
            get { return name; }
            set { name = value; }
        }

        private struct Tile {
            int x, y;

            public override bool Equals(object obj) {
                if(obj is Tile) {
                    Tile test = (Tile)obj;
                    return (test.x == this.x && test.y == this.y);
                }
                return false;
            }

            public Tile(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        List<Tile> map = new List<Tile>();

        public void AddTile(int x, int y) {
            if(!map.Contains(new Tile(x, y))) {
                map.Add(new Tile(x, y));
            }
        }

        public void RemoveTile(int x, int y) {
            if(map.Contains(new Tile(x, y))) {
                map.Remove(new Tile(x, y));
            }
        }

        public void DrawArea(SpriteBatch sb) {

        }

        public int DrawButton(SpriteBatch sb, SpriteFont sf, int x) {
            int strLength = (int)Math.Ceiling(sf.MeasureString(name).X);

            return 0;
        }
    }
}
