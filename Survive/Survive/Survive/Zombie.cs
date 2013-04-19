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
        protected Random rgen;
        protected int attackPower;
        protected ZombieActions zombieAction;
        protected int direction;

        public Zombie(Rectangle loc)
            : base(loc)
        {
            rgen = new Random();
            attackPower = rgen.Next(5) + 5; //attack power between 5-10
            zombieAction = ZombieActions.Patrol;
            moveSpeed = 1;
            direction = 1;
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

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        //methods
        public void attack(Player player)
        {
            player.HP -= attackPower;
        }

        public Boolean DetectPlayers(Player player)
        {
            if (zombieAction != ZombieActions.Chase)
            {
                int rangeMultiplier = 2;
                Rectangle detectRange = new Rectangle(
                    location.X, location.Y,
                    location.Width * rangeMultiplier, location.Height * rangeMultiplier);

                if (detectRange.Intersects(player.Location))
                    return true;
            }
            return false;
        }

        public void changeDirection()
        {
            //check edges
            if (location.X < 5)
                direction = 1;
            else if (location.X > (SurviveGame.viewportWidth - location.Width))
                direction = -1;

            //randomly change direction
            if (rgen.Next(350) == 1)
                direction *= -1;
        }
    }
}
