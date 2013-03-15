using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Survive {
    class Resources {
        SpriteFont acknow;

        public Resources(ContentManager cm) {
            acknow = cm.Load<SpriteFont>("AcknowTT");
        }
    }
}
