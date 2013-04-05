using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MapEditor {

    class Button {
        Texture2D tex;
        Rectangle rect;

        bool hold;

        bool last = false;

        public Button(Texture2D tex, bool holdButton, int x, int y, Click fn) {
            this.tex = tex;
            hold = holdButton;
            rect = new Rectangle(x, y, tex.Width, tex.Height);
            Clicked = fn;
        }

        public delegate void Click();

        public Click Clicked;

        public bool CheckClicked(int x, int y) {
            if(rect.Contains(new Point(x, y)) && this.Clicked != null) {
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
            sb.Draw(tex, rect, Color.White);
        }
    }
}
