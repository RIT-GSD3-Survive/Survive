using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survive
{
    class Player : Humanoid
    {
        //Attributes
        string name;

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
