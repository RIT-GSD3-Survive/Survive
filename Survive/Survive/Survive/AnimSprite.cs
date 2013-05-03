using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Survive {
    class AnimSprite {
        private Texture2D sprite;
        private TimeSpan time = new TimeSpan(0);
        private int frame = 0;

        private TimeSpan delay;
        private int frames;
        private Vector2 size;

        public AnimSprite(Texture2D sprite, TimeSpan delay, int frames, Vector2 size) {
            this.sprite = sprite;
            this.delay = delay;
            this.frames = frames;
            this.size = size;
        }

        public void Update(GameTime gt) {
            time += gt.ElapsedGameTime;
            if(time > delay) {
                time -= delay;
                frame++;
                if(frame == frames) {
                    frame = 0;
                }
            }
        }

        public Rectangle GetCurrClip() {
            int x = (frame * (int)size.X) % sprite.Width, y = (frame * (int)size.X) / sprite.Width;
            return new Rectangle(x, y, (int)size.X, (int)size.Y);
        }
    }
}
