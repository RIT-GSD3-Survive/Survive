using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Survive.NBTAssist {

    public enum TagType {
        
    }

    abstract class NBTTag {
        private String name;

        public NBTTag(String name) {
            this.name = name;
        }

        public String Name {
            get { return name; }
            set { name = value; }
        }
    }

    class NBTByte : NBTTag {
        
    }

    class NBTShort : NBTTag {
        
    }

    class NBTInt : NBTTag {

    }

    class NBTLong : NBTTag {

    }

    class NBTFloat : NBTTag {

    }

    class NBTDouble : NBTTag {

    }

    class NBTByteArray : NBTTag {

    }

    class NBTString : NBTTag {

    }

    class NBTList : NBTTag {

    }

    class NBTCompound : NBTTag {

    }

    class NBTIntArray : NBTTag {

    }
}
