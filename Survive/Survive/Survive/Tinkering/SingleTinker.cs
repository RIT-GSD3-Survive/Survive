using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive.Tinkering {
    class SingleTinker {
        private TinkerBackend back;

        public SingleTinker(Player p) {
            back = new TinkerBackend(p);
        }
    }
}
