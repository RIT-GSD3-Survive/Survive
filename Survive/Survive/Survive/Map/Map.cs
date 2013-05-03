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

        public static Dictionary<Area, List<NbtCompound>> portalsToProcess = new Dictionary<Area,List<NbtCompound>>();

        private Area currArea = null;

        public Boolean AtSafehouse {
            get { return currArea.Name.Equals("Safehouse", StringComparison.OrdinalIgnoreCase); }
        }

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
                    if(alpha.Name.Equals("safehouse", StringComparison.OrdinalIgnoreCase)) {
                        currArea = alpha;
                        break;
                    }
                }
                foreach(Area alpha in portalsToProcess.Keys) {
                    List<Portal> newPortals = new List<Portal>();
                    foreach(NbtCompound beta in portalsToProcess[alpha]) {
                        foreach(Area charlie in areas) {
                            if(charlie.Name == beta.Get<NbtString>("linkTo").StringValue) {
                                newPortals.Add(new Portal(new Microsoft.Xna.Framework.Rectangle(beta.Get<NbtByte>("x").IntValue * 32 + 4, beta.Get<NbtByte>("y").IntValue * 32 + 8, 25, 24),
                                    charlie, beta.Get<NbtByte>("linkX").IntValue * 32, beta.Get<NbtByte>("linkY").IntValue * 32));
                            }
                        }
                    }
                    alpha.AddPortals(newPortals);
                }
                portalsToProcess.Clear();
            }
        }

        public void DrawArea(SpriteBatch sb) {
            if(currArea != null) currArea.DrawArea(sb);
        }

        public List<Platform> GetTiles() {
            return (currArea != null)?currArea.GetTiles():null;
        }
    }
}
