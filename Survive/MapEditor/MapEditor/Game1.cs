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

using fNbt;
using System.IO;

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
        int scrollX = 16;
        Area currArea = null;
        bool editName = false;
        KeyboardState? prevKState = null;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //sets screen size
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

            GlobalVars.view = GraphicsDevice.Viewport;

            Load();

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

            Area.tiles = Content.Load<Texture2D>("Tiles");

            buttons.Add(new Button(Content.Load<Texture2D>("NewArea32"), false, 16, 16, delegate() { // New Area Function
                Area alpha = new Area();
                currArea = alpha;
                areas.Add(alpha);
                editName = true;
            }));
            buttons.Add(new Button(Content.Load<Texture2D>("EditArea32"), false, 48, 16, delegate() { // Edit Area Name Function
                editName = true;
            }));
            buttons.Add(new Button(Content.Load<Texture2D>("DeleteArea32"), false, 80, 16, delegate() { // Delete Area Function
                if(currArea != null) {
                    areas.Remove(currArea);
                    currArea = null;
                }
            }));
            buttons.Add(new Button(Content.Load<Texture2D>("Save32"), false, 112, 16, Save));
            buttons.Add(new Button(Content.Load<Texture2D>("ScrollLeft"), true, 0, GraphicsDevice.Viewport.Height - 32, delegate() { // Scroll Areas Left
                scrollX -= 5;
            }));
            buttons.Add(new Button(Content.Load<Texture2D>("ScrollRight"), true, GraphicsDevice.Viewport.Width - 16, GraphicsDevice.Viewport.Height - 32, delegate() { // Scroll Areas Right
                scrollX += 5;
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

            if(left) {
                foreach(Button alpha in buttons) {
                    if(alpha.CheckClicked(mState.X, mState.Y)) {
                        left = false;
                        break;
                    }
                }
            }
            if(left) {
                foreach(Area alpha in areas) {
                    if(alpha.ButtonClicked(mState.X, mState.Y)) {
                        currArea = alpha;
                        editName = false;
                        left = false;
                        break;
                    }
                }
            }

            IsMouseVisible = !left && !right;

            if(currArea != null && !IsMouseVisible) {
                int x = mState.X - (mState.X % 32) + 32;
                x /= 32;
                int y = mState.Y - (mState.Y % 32) - 60;
                y /= 32;
                if(left) {
                    currArea.AddTile(x, y);
                } else if(right) {
                    currArea.RemoveTile(x, y);
                }
            }

            if(editName && currArea != null) {
                ProcessEditName();
            }

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

            int x = scrollX;

            if(currArea != null) {
                currArea.DrawArea(spriteBatch);
            }

            foreach(Area alpha in areas) {
                Color back = Color.White;
                if(alpha == currArea) {
                    if(editName) {
                        back = Color.LightYellow;
                    } else {
                        back = Color.LightSkyBlue;
                    }
                }
                x += alpha.DrawButton(spriteBatch, courier, x, back);
            }

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

        private void ProcessEditName() {
            KeyboardState kState = Keyboard.GetState();

            Keys[] keys = kState.GetPressedKeys();
            List<Keys> pressed = new List<Keys>();

            if(prevKState == null) {
                pressed.AddRange(keys);
            } else {
                foreach(Keys key in keys) {
                    if(((KeyboardState)prevKState).IsKeyUp(key)) {
                        pressed.Add(key);
                    }
                }
            }

            Boolean shift = kState.IsKeyDown(Keys.LeftShift) || kState.IsKeyDown(Keys.RightShift);

            foreach(Keys key in pressed) {
                switch(key) {
                    case Keys.A:
                        currArea.Name += (shift) ? "A" : "a";
                        break;
                    case Keys.B:
                        currArea.Name += (shift) ? "B" : "b";
                        break;
                    case Keys.C:
                        currArea.Name += (shift) ? "C" : "c";
                        break;
                    case Keys.D:
                        currArea.Name += (shift) ? "D" : "d";
                        break;
                    case Keys.E:
                        currArea.Name += (shift) ? "E" : "e";
                        break;
                    case Keys.F:
                        currArea.Name += (shift) ? "F" : "f";
                        break;
                    case Keys.G:
                        currArea.Name += (shift) ? "G" : "g";
                        break;
                    case Keys.H:
                        currArea.Name += (shift) ? "H" : "h";
                        break;
                    case Keys.I:
                        currArea.Name += (shift) ? "I" : "i";
                        break;
                    case Keys.J:
                        currArea.Name += (shift) ? "J" : "j";
                        break;
                    case Keys.K:
                        currArea.Name += (shift) ? "K" : "k";
                        break;
                    case Keys.L:
                        currArea.Name += (shift) ? "L" : "l";
                        break;
                    case Keys.M:
                        currArea.Name += (shift) ? "M" : "m";
                        break;
                    case Keys.N:
                        currArea.Name += (shift) ? "N" : "n";
                        break;
                    case Keys.O:
                        currArea.Name += (shift) ? "O" : "o";
                        break;
                    case Keys.P:
                        currArea.Name += (shift) ? "P" : "p";
                        break;
                    case Keys.Q:
                        currArea.Name += (shift) ? "Q" : "q";
                        break;
                    case Keys.R:
                        currArea.Name += (shift) ? "R" : "r";
                        break;
                    case Keys.S:
                        currArea.Name += (shift) ? "S" : "s";
                        break;
                    case Keys.T:
                        currArea.Name += (shift) ? "T" : "t";
                        break;
                    case Keys.U:
                        currArea.Name += (shift) ? "U" : "u";
                        break;
                    case Keys.V:
                        currArea.Name += (shift) ? "V" : "v";
                        break;
                    case Keys.W:
                        currArea.Name += (shift) ? "W" : "w";
                        break;
                    case Keys.X:
                        currArea.Name += (shift) ? "X" : "x";
                        break;
                    case Keys.Y:
                        currArea.Name += (shift) ? "Y" : "y";
                        break;
                    case Keys.Z:
                        currArea.Name += (shift) ? "Z" : "z";
                        break;
                    case Keys.D0:
                        currArea.Name += 0;
                        break;
                    case Keys.D1:
                        currArea.Name += 1;
                        break;
                    case Keys.D2:
                        currArea.Name += 2;
                        break;
                    case Keys.D3:
                        currArea.Name += 3;
                        break;
                    case Keys.D4:
                        currArea.Name += 4;
                        break;
                    case Keys.D5:
                        currArea.Name += 5;
                        break;
                    case Keys.D6:
                        currArea.Name += 6;
                        break;
                    case Keys.D7:
                        currArea.Name += 7;
                        break;
                    case Keys.D8:
                        currArea.Name += 8;
                        break;
                    case Keys.D9:
                        currArea.Name += 9;
                        break;
                    case Keys.Space:
                        currArea.Name += " ";
                        break;
                    case Keys.Back:
                        currArea.Name = (currArea.Name.Equals(""))?"":currArea.Name.Substring(0, currArea.Name.Length - 1);
                        break;
                    default:
                        break;
                }
            }

            prevKState = kState;

            editName = kState.IsKeyUp(Keys.Enter);
        }

        public void Save() {
            NbtCompound root = new NbtCompound("base");
            root.Add(new NbtString("fileType", "map"));

            NbtList aList = new NbtList("areas", NbtTagType.Compound);
            foreach(Area alpha in areas) {
                aList.Add(alpha.Save());
            }
            root.Add(aList);

            NbtFile saveFile = new NbtFile(root);
            saveFile.BigEndian = true;
            saveFile.SaveToFile("mapfile.nbt", NbtCompression.None);
        }

        public void Load() {
            if(File.Exists("mapfile.nbt")) {
                NbtFile saveFile = new NbtFile("mapfile.nbt");

                NbtCompound root = saveFile.RootTag;
                if(root.Get<NbtString>("fileType").StringValue.Equals("map")) {
                    NbtList aList = root.Get<NbtList>("areas");
                    foreach(NbtCompound alpha in aList) {
                        areas.Add(new Area(alpha));
                    }
                }
            }
        }
    }
}
