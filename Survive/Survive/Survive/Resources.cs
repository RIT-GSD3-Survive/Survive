using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Survive {
    class Resources {
        SpriteFont courier;

        public Resources(ContentManager cm) {
            courier = cm.Load<SpriteFont>("Courier");
        }
    }
}
