﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Survive {
    class Resources {
        private static SpriteFont courier, courierSmall;

        private static Texture2D tiles, portal;

        public static SpriteFont Courier {
            get { return courier; }
        }

        public static SpriteFont CourierSmall {
            get { return courierSmall; }
        }

        public static Texture2D Tiles {
            get { return tiles; }
        }

        public static Texture2D Portal {
            get { return portal; }
        }

        public static void LoadRes(ContentManager cm) {
            courier = cm.Load<SpriteFont>("Courier");
            courierSmall = cm.Load<SpriteFont>("CourierSmall");
            tiles = cm.Load<Texture2D>("Tiles");
            portal = cm.Load<Texture2D>("Portal");
        }
    }
}
