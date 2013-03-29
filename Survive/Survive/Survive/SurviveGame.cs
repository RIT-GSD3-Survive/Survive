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
        enum GameState { Menu, InGame, Pause, SingleTinker, MultiTinker, GameOver };
        GameState gameState;
        enum PlayerInput { Left, Right, Jump, Fire, SwitchWeapon, Interact};
        PlayerInput playerInput;
        GamePadState previousGPS;
        GamePadState currentGPS;

        // Class containing all of the resources we're using
        Resources res;

        public SurviveGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

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
               
            }
            if (gameState == GameState.InGame)
            {
                if (left.X > 0)
                {
                    playerInput = PlayerInput.Right;
                }
                if (left.X < 0)
                {
                    playerInput = PlayerInput.Left;
                }
                if (left.Y > 0 || currentGPS.IsButtonDown(Buttons.A))
                {
                    playerInput = PlayerInput.Jump;
                }
                if(currentGPS.IsButtonDown(Buttons.RightTrigger))
                {
                    playerInput = PlayerInput.Fire;
                }
                if (currentGPS.IsButtonDown(Buttons.DPadLeft) || currentGPS.IsButtonDown(Buttons.DPadRight))
                {
                    playerInput = PlayerInput.SwitchWeapon;
                }
                if (currentGPS.IsButtonDown(Buttons.B))
                {
                    playerInput = PlayerInput.Interact;
                }
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

            }
            if (gameState == GameState.InGame)
            {

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
            if (gameState == GameState.GameOver)
            {

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
