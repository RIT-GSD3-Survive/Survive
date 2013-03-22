using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Player : Humanoid
    {
        //Attributes
        string name;
        int number;

        public Player(string nm, int num)
        {
            name = nm;
            number = num;
        }

        //Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
