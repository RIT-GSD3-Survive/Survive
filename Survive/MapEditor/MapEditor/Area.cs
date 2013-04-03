using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MapEditor {
    class Area {
        private String name = "";

        private int buttonScrollX = 0;
        private int buttonWidth = 0;

        public static Texture2D cap, mid;

        public String Name {
            get { return name; }
            set { name = value; }
        }

        List<Point> map = new List<Point>();

        public void AddTile(int x, int y) {
            if(!map.Contains(new Point(x, y))) {
                map.Add(new Point(x, y));
            }
        }

        public void RemoveTile(int x, int y) {
            if(map.Contains(new Point(x, y))) {
                map.Remove(new Point(x, y));
            }
        }

        public void DrawArea(SpriteBatch sb) {

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
    }
}
