using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class Platform : Terrain
    {
        protected int tileSize;
        protected Rectangle sourceRect;

        public Platform(Rectangle loc, Vector2 tileSheetLoc)
            :base(loc)
        {
            collidable = true;
            this.tileSize = SurviveGame.tileSize;
            sourceRect = new Rectangle((int)tileSheetLoc.X * tileSize, (int)tileSheetLoc.Y * tileSize, tileSize, tileSize);
        }

        public Vector2 TileType
        {
            get { return new Vector2(sourceRect.X, sourceRect.Y); }
            set { sourceRect = new Rectangle((int)value.X * tileSize, (int)value.Y * tileSize, tileSize, tileSize); }
        }

        public Rectangle SourceRectangle
        {
            get { return sourceRect; }
        }
    }
}
