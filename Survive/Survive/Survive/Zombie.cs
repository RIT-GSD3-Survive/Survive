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
        protected int attackPower;
        protected ZombieActions zombieAction;

        public Zombie(Rectangle loc)
            : base(loc)
        {
            attackPower = rgen.Next(5) + 5; //attack power between 5-10
            zombieAction = ZombieActions.Patrol;
            moveSpeed = 1;
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
        public Boolean DetectPlayers(Player player)
        {
            if (zombieAction != ZombieActions.Chase)
            {
                int rangeMultiplier = 4;
                Rectangle detectRange = new Rectangle(
                    location.X - ((location.Width * rangeMultiplier) / 2), location.Y - ((location.Height * rangeMultiplier) / 2),
                    location.Width * rangeMultiplier, location.Height * rangeMultiplier/2);

                if (detectRange.Intersects(player.Location))
                    return true;
            }
            return false;
        }

        public void changeDirection()
        {
            //check edges
            if (location.X < 5)
                faceRight = true;
            else if (location.X > (SurviveGame.viewportWidth - location.Width))
                faceRight = false;

            //randomly change direction
            if (rgen.Next(350) == 1)
                if (faceRight) faceRight = false;
                else faceRight = true;
        }
    }
}
