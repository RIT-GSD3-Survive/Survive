using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Survive {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SurviveGame : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //images
        Texture2D playerImage;
        Texture2D GUIAmmo;
        Texture2D GUIAmmoClipEmpty;
        Texture2D GUIAmmoClipFull;
        Texture2D GUIhpBARgrey;
        Texture2D GUIhpBARgreyside;
        Texture2D GUIhpBARred;
        Texture2D GUIhpBARredside;
        Texture2D GUIMain;
        Texture2D GUIVerticalFadeBars;
        //game/menu state
        enum GameState { Menu, InGame, Pause, SingleTinker, MultiTinker, GameOver };
        enum MenuButtonState { None, Single, Multi, Quit };
        MenuButtonState menuButtonState;
        GameState gameState;
        //player input
        enum PlayerMovementInput { Left, Right };
        enum PlayerOtherInput { Jump, Fire, SwitchWeapon, Interact };
        PlayerMovementInput playerMovementInput;
        PlayerOtherInput playerOtherInput;
        //controller state
        GamePadState previousGPS;
        GamePadState currentGPS;
        //players
        Player p1;
        Player p2;
        Player p3;
        Player p4;
        //gui variables
        int hpBarWidth;

        // Class containing all of the resources we're using
        Resources res;

        public SurviveGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //sets screen size to 800x500
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            menuButtonState = MenuButtonState.None;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            res = new Resources(this.Content);
            playerImage = this.Content.Load<Texture2D>("Person");

            GUIAmmo = this.Content.Load<Texture2D>("GUIAmmo");
            GUIAmmoClipEmpty = this.Content.Load<Texture2D>("GUIAmmoClipEmpty");
            GUIAmmoClipFull = this.Content.Load<Texture2D>("GUIAmmoClipFull");

            GUIhpBARred = this.Content.Load<Texture2D>("GUIhpBARred");
            GUIhpBARgrey = this.Content.Load<Texture2D>("GUIhpBARgrey");
            GUIhpBARredside = this.Content.Load<Texture2D>("GUIhpBARredside");
            GUIhpBARgreyside = this.Content.Load<Texture2D>("GUIhpBARgreyside");

            GUIMain = this.Content.Load<Texture2D>("GUIInGame4player");
            GUIVerticalFadeBars = this.Content.Load<Texture2D>("GUIInGameVerticalFadeBars");
        }
        
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // getting gamePad info
            previousGPS = currentGPS;
            currentGPS = GamePad.GetState(0);
            GamePadThumbSticks sticks = currentGPS.ThumbSticks;
            Vector2 left = sticks.Left;
            Vector2 right = sticks.Right;
            // TODO: Add your update logic here
            if (gameState == GameState.Menu)
            {
                p1 = new Player("Name", 1, new Rectangle(200,200,playerImage.Width, playerImage.Height));
                gameState = GameState.InGame;
            }
            if (gameState == GameState.InGame)
            {
                if (left.X > 0)
                {
                    playerMovementInput = PlayerMovementInput.Right;
                }
                if (left.X < 0)
                {
                    playerMovementInput = PlayerMovementInput.Left;
                }
                if (left.Y > 0.8  || currentGPS.IsButtonDown(Buttons.A) && p1.OnGround)
                {
                    playerOtherInput = PlayerOtherInput.Jump;
                }
                if(currentGPS.IsButtonDown(Buttons.RightTrigger))
                {
                    playerOtherInput = PlayerOtherInput.Fire;
                }
                if (currentGPS.IsButtonDown(Buttons.DPadLeft) || currentGPS.IsButtonDown(Buttons.DPadRight))
                {
                    playerOtherInput = PlayerOtherInput.SwitchWeapon;
                }
                if (currentGPS.IsButtonDown(Buttons.B))
                {
                    playerOtherInput = PlayerOtherInput.Interact;
                }
                p1.Walk(currentGPS);
                if (playerOtherInput == PlayerOtherInput.Jump)
                {
                    p1.Jump(currentGPS);
                }
                if (playerOtherInput == PlayerOtherInput.Fire)
                {

                }
                if (playerOtherInput == PlayerOtherInput.SwitchWeapon)
                {

                }
                if (playerOtherInput == PlayerOtherInput.Interact)
                {

                }
                p1.Gravity();
                p1.PosUpdate();
            }
            if (gameState == GameState.Pause)
            {

            }
            if (gameState == GameState.MultiTinker)
            {
                
            }
            if (gameState == GameState.SingleTinker)
            {

            }
            if(gameState == GameState.GameOver)
            {

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (gameState == GameState.Menu)
            {
                if (menuButtonState == MenuButtonState.None)
                {

                }
                if (menuButtonState == MenuButtonState.Single)
                {

                }
                if (menuButtonState == MenuButtonState.Multi)
                {

                }
                if (menuButtonState == MenuButtonState.Quit)
                {

                }
            }
            if (gameState == GameState.InGame)
            {
                if (playerMovementInput == PlayerMovementInput.Left)
                {
                    spriteBatch.Draw(playerImage, p1.Location, Color.White);
                    if (playerOtherInput == PlayerOtherInput.Jump)
                    {

                    }
                    if (playerOtherInput == PlayerOtherInput.Fire)
                    {

                    }
                }
                if (playerMovementInput == PlayerMovementInput.Right)
                {
                    spriteBatch.Draw(playerImage, p1.Location, null, Color.White, 0.0f, new Vector2(17.5f, 38.5f), SpriteEffects.FlipHorizontally, 0.0f);
                    if (playerOtherInput == PlayerOtherInput.Jump)
                    {

                    }
                    if (playerOtherInput == PlayerOtherInput.Fire)
                    {

                    }
                }

                //************************GUI************************


            }//end gameState.InGame
            if (gameState == GameState.Pause)
            {

            }
            if (gameState == GameState.MultiTinker)
            {

            }
            if (gameState == GameState.SingleTinker)
            {

            }
            if (gameState == GameState.GameOver)
            {

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /*
        /// <summary>
        /// Prevents a key from being processed mutliple times if held
        /// </summary>
        /// <returns></returns>
        public Boolean SingleKeyPress(Keys k)
        {
            if (kState.IsKeyDown(k) && kStatePrevious.IsKeyUp(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */

        public Boolean SingleKeyPress(Buttons b)
        {
            if (currentGPS.IsButtonDown(b) && previousGPS.IsButtonUp(b))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
