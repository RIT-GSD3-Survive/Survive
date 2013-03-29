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
        Boolean onGround; //Prevents jumping while in air

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

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public Boolean OnGround
        {
            get { return onGround; }
            set { onGround = value; }
        }
    }
}
