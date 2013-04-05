using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using fNbt;
using Microsoft.Xna.Framework.Graphics;

namespace Survive {
    class Map {

        private List<Area> areas = new List<Area>();

        private Area currArea = null;

        public Map() {
            if(File.Exists("mapfile.nbt")) {
                NbtFile saveFile = new NbtFile("mapfile.nbt");

                NbtCompound root = saveFile.RootTag;
                if(root.Get<NbtString>("fileType").StringValue.Equals("map")) {
                    NbtList aList = root.Get<NbtList>("areas");
                    foreach(NbtCompound alpha in aList) {
                        areas.Add(new Area(alpha));
                    }
                }
                foreach(Area alpha in areas) {
                    if(alpha.Name == "default") {
                        currArea = alpha;
                        break;
                    }
                }
            }
        }

        public void DrawArea(SpriteBatch sb) {
            currArea.DrawArea(sb);
        }

        public List<Platform> GetTiles() {
            return (currArea != null)?currArea.GetTiles():null;
        }
    }
}
