using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Survive
{
    class Zombie : Humanoid
    {
        //Attributes
        Random rgen;
        int attackPower;
        ZombieActions zombieAction;

        public Zombie(Rectangle loc)
            : base(loc)
        {
            rgen = new Random();
            attackPower = rgen.Next(5) + 5; //attack power between 5-10
            zombieAction = ZombieActions.Patrol;
        }

        //Properties
        public ZombieActions ZombieAction
        {
            get { return zombieAction; }
            set { zombieAction = value; }
        }

        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }

        //methods
        public void attack(Player player)
        {
            player.HP -= attackPower;
        }

        public void DetectPlayers()
        {
            if (zombieAction != ZombieActions.Chase)
            {

            }
        }
    }
}
