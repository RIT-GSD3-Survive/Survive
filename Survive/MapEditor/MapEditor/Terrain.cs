using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapEditor {
    enum TerrainType {
        PLATFORM, PORTAL, DECOR
    }

    abstract class Terrain {
        Point loc;
        Area areaLoc;

        public Terrain(int x, int y, Area area) {
            loc = new Point(x, y);
            areaLoc = area;
        }

        public Point Location {
            get { return loc; }
        }

        public Area MyArea {
            get { return areaLoc; }
        }

        public override bool Equals(object obj) {
            return (obj is Terrain && ((Terrain)obj).Location == this.Location && ((Terrain)obj).MyArea == this.MyArea);
        }
    }
}
