using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survive.Tinkering {

    class Button {
        Texture2D tex;
        Rectangle rect;

        Rectangle? srcRect = null;

        bool hold;

        bool last = false;
        bool visible = true;
        bool active = true;

        public bool Last {
            set { last = value; }
        }

        public int X {
            get { return rect.X; }
            set { rect.X = value; }
        }

        public int Y {
            get { return rect.Y; }
            set { rect.Y = value; }
        }

        public bool Visible {
            get { return visible; }
            set { visible = value; }
        }

        public bool Active {
            get { return active; }
            set { active = value; }
        }

        public Button(Texture2D tex, bool holdButton, int x, int y, Click fn) {
            this.tex = tex;
            hold = holdButton;
            rect = new Rectangle(x, y, tex.Width, tex.Height);
            Clicked = fn;
        }

        public Button(Texture2D tex, Rectangle srcRect, int scale, bool holdButton, int x, int y, Click fn) {
            this.tex = tex;
            hold = holdButton;
            this.srcRect = srcRect;
            rect = new Rectangle(x, y, srcRect.Width * scale, srcRect.Height * scale);
            Clicked = fn;
        }

        public delegate void Click();

        public Click Clicked;

        public bool CheckClicked(int x, int y) {
            if(this.active && rect.Contains(new Point(x, y)) && this.Clicked != null) {
                if(hold || !last) {
                    this.Clicked();
                    last = true;
                }
                return true;
            } else {
                last = false;
                return false;
            }
        }

        public void Draw(SpriteBatch sb) {
            if(visible) {
                sb.Draw(tex, rect, srcRect, (active)?Color.White:Color.LightGray);
            }
        }
    }
}
