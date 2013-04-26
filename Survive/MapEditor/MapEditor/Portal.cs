using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor {
    class Portal : Terrain {

        public Portal(int x, int y, Area area) : base(x, y, area) { }

        Portal link;

        public Portal Link {
            get { return link; }
            set { link = value; }
        }

        public void MakeLink(Portal to) {
            to.Link = this;
            Link = to;
        }

        public static Dictionary<Portal, fNbt.NbtCompound> portalsToProcess = new Dictionary<Portal, fNbt.NbtCompound>();
    }
}
