using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    public class Item : GameObject
    {
        protected Boolean active;

        public Item()
        {
            active = true;
        }

        public Boolean Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}
