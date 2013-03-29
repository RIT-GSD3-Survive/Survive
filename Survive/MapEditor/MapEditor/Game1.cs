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

namespace MapEditor {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D pencil, erase, reticle;
        SpriteFont courier;

        Vector2 mouseLoc;

        Boolean left, right;

        List<Button> buttons = new List<Button>();

        List<Area> areas = new List<Area>();

        public Game1() {
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
            pencil = Content.Load<Texture2D>("Pencil32");
            erase = Content.Load<Texture2D>("Erase32");
            reticle = Content.Load<Texture2D>("Reticle8");

            courier = Content.Load<SpriteFont>("Courier");

            Area.cap = Content.Load<Texture2D>("areaCap");
            Area.mid = Content.Load<Texture2D>("areaMid");

            buttons.Add(new Button(Content.Load<Texture2D>("NewArea32"), false, 16, 16, delegate() { // New Area Function

            }));
            buttons.Add(new Button(Content.Load<Texture2D>("EditArea32"), false, 48, 16, delegate() { // New Area Function

            }));
            buttons.Add(new Button(Content.Load<Texture2D>("DeleteArea32"), false, 80, 16, delegate() { // New Area Function

            }));
            buttons.Add(new Button(Content.Load<Texture2D>("Save32"), false, 112, 16, delegate() { // Save Function

            }));
            buttons.Add(new Button(Content.Load<Texture2D>("ScrollLeft"), true, 0, GraphicsDevice.Viewport.Height - 32, delegate() { // Scroll Areas Left

            }));
            buttons.Add(new Button(Content.Load<Texture2D>("ScrollRight"), true, GraphicsDevice.Viewport.Width - 16, GraphicsDevice.Viewport.Height - 32, delegate() { // Scroll Areas Right

            }));

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
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            MouseState mState = Mouse.GetState();

            mouseLoc = new Vector2(mState.X, mState.Y);

            left = (mState.LeftButton == ButtonState.Pressed);
            right = (mState.RightButton == ButtonState.Pressed);

            IsMouseVisible = !left && !right;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.AliceBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach(Button alpha in buttons) {
                alpha.Draw(spriteBatch);
            }

            if(left) {
                spriteBatch.Draw(pencil, mouseLoc + new Vector2(-4, -29), Color.White);
            } else if(right) {
                spriteBatch.Draw(erase, mouseLoc + new Vector2(-4, -29), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
