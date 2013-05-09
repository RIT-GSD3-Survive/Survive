using System;

namespace Survive {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SurviveGame game = new SurviveGame())
            {
                GlobalVariables.survGameInstance = game;
                game.Run();
            }
        }
    }
#endif
}

