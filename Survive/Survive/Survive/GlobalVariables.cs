using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Survive
{
    public static class GlobalVariables
    {
        public static int player1Score = 0;
        public static int player2Score = 0;
        public static int player3Score = 0;
        public static int player4Score = 0;
        public static Map map;

        public static Rectangle tableLoc = new Rectangle(165, 387, 64, 33);

        public static SurviveGame survGameInstance;

        //game/menu states
        public enum GameState { Menu, InGame, Pause, GameOver };
        public static GameState gameState;
    }
}
