using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    public class Portal : Terrain
    {
        //attributes
        protected Area linkTo;
        protected int linkX, linkY;

        private AnimSprite sprite = new AnimSprite(Resources.Portal, new TimeSpan(1500), 4, new Vector2(25, 24));

        public Portal(Rectangle loc, Area to, int xlink, int ylink)
            :base(loc)
        {
            linkTo = to;
            linkX = xlink;
            linkY = ylink;
        }

        //properties
        public Area LinkTo
        {
            get { return linkTo; }
            set { linkTo = value; }
        }

        public int LinkX
        {
            get { return linkX; }
            set { linkX = value; }
        }

        public int LinkY
        {
            get { return linkY; }
            set { linkY = value; }
        }

        public void Update(GameTime gt) {
            sprite.Update(gt);
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(Resources.Portal, new Rectangle(location.X, location.Y + 64, location.Width, location.Height), sprite.GetCurrClip(), Color.White);
        }
    }
}
