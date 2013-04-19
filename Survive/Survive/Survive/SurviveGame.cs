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

namespace Survive
{
    //global enum
    enum ZombieActions { Patrol, Chase }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SurviveGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //images
        Texture2D playerImage;
        Texture2D zombieImage;
        Texture2D GUIAmmo;
        Texture2D GUIAmmoClipEmpty;
        Texture2D GUIAmmoClipFull;
        Texture2D GUIhpBARgrey;
        Texture2D GUIhpBARgreyside;
        Texture2D GUIhpBARred;
        Texture2D GUIhpBARredside;
        Texture2D GUIMain;
        Texture2D GUIp2;
        Texture2D GUIp3;
        Texture2D GUIp4;
        Texture2D GUIVerticalFadeBars;
        Texture2D tileSheet;
        Texture2D ammoImage;
        Texture2D medkitImage;
        Texture2D bulletImage;
        //game/menu states
        enum GameState { Menu, InGame, Pause, SingleTinker, MultiTinker, GameOver };
        enum MenuButtonState { None, Single, Multi, Quit };
        enum GameLocation { Safehouse, Level1, Level2 };
        MenuButtonState menuButtonState;
        GameState gameState;
        GameLocation gameLocation;
        //player input
        enum PlayerMovementInput { Left, Right };
        enum PlayerOtherInput { Jump, Fire, SwitchWeapon, Interact, Reload };
        PlayerMovementInput playerMovementInput;
        PlayerOtherInput playerOtherInput;
        //controller state
        GamePadState previousGPS;
        GamePadState currentGPS;
        //Keyboard State
        KeyboardState kStateCurrent;
        KeyboardState kStatePrevious;
        //Mouse State
        MouseState mStateCurrent;
        MouseState mStatePrevious;
        //players
        Player p1;
        Player p2;
        Player p3;
        Player p4;
        List<Player> playerList;
        //zombies
        List<Zombie> zombieList;
        //gui variables
        int hpBarWidth;
        public static int tileSize;
        public static int viewportHeight;
        public static int viewportWidth;
        List<Platform> platformTilesList;
        //items
        List<Item> activeItems;
        List<Bullet> bulletList;

        // Map.
        Map map;

        public SurviveGame()
        {
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
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menuButtonState = MenuButtonState.None;
            gameState = GameState.Menu;
            gameLocation = GameLocation.Level1;
            hpBarWidth = 130;
            tileSize = 32;
            viewportHeight = GraphicsDevice.Viewport.Height;
            viewportWidth = GraphicsDevice.Viewport.Width;

            platformTilesList = new List<Platform>();
            zombieList = new List<Zombie>();
            activeItems = new List<Item>();
            bulletList = new List<Bullet>();

            map = new Map();
            initializeGround();

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Resources.LoadRes(Content);
            playerImage = this.Content.Load<Texture2D>("Person");
            zombieImage = this.Content.Load<Texture2D>("Zombie");
            ammoImage = this.Content.Load<Texture2D>("Ammo");
            medkitImage = this.Content.Load<Texture2D>("Medkit");
            bulletImage = new Texture2D(graphics.GraphicsDevice, 1, 1);
            bulletImage.SetData<Color>(new Color[1] { Color.White }); //sets bullets to white


            GUIAmmo = this.Content.Load<Texture2D>("GUIAmmo");
            GUIAmmoClipEmpty = this.Content.Load<Texture2D>("GUIAmmoClipEmpty");
            GUIAmmoClipFull = this.Content.Load<Texture2D>("GUIAmmoClipFull");

            GUIhpBARred = this.Content.Load<Texture2D>("GUIhpBARred");
            GUIhpBARgrey = this.Content.Load<Texture2D>("GUIhpBARgrey");
            GUIhpBARredside = this.Content.Load<Texture2D>("GUIhpBARredside");
            GUIhpBARgreyside = this.Content.Load<Texture2D>("GUIhpBARgreyside");

            GUIMain = this.Content.Load<Texture2D>("GUIInGame1player");
            GUIp2 = this.Content.Load<Texture2D>("GUIInGame2player");
            GUIp3 = this.Content.Load<Texture2D>("GUIInGame3player");
            GUIp4 = this.Content.Load<Texture2D>("GUIInGame4player");
            GUIVerticalFadeBars = this.Content.Load<Texture2D>("GUIInGameVerticalFadeBars");

            tileSheet = this.Content.Load<Texture2D>("Tiles");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // getting gamePad, keyboard, and mouse info
            previousGPS = currentGPS;
            currentGPS = GamePad.GetState(0);
            kStatePrevious = kStateCurrent;
            kStateCurrent = Keyboard.GetState();
            mStatePrevious = mStateCurrent;
            mStateCurrent = Mouse.GetState();
            //Converting 
            GamePadThumbSticks sticks = currentGPS.ThumbSticks;
            Vector2 left = sticks.Left;
            Vector2 right = sticks.Right;
            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.Menu:
                    p1 = new Player("Name", 1, new Rectangle(200, 343, playerImage.Width, playerImage.Height));
                    gameState = GameState.InGame;
                    break;

                case GameState.InGame:
                    if (currentGPS.IsConnected)
                    {
                        if (left.X > 0)
                        {
                            playerMovementInput = PlayerMovementInput.Right;
                            p1.WalkRight();
                        }
                        if (left.X < 0)
                        {
                            playerMovementInput = PlayerMovementInput.Left;
                            p1.WalkLeft();
                        }
                        if (left.Y > 0.8 || currentGPS.IsButtonDown(Buttons.A) && !(p1.Jumping))
                        {
                            playerOtherInput = PlayerOtherInput.Jump;
                            p1.Jump();
                        }
                        if (currentGPS.IsButtonDown(Buttons.RightTrigger))
                        {
                            playerOtherInput = PlayerOtherInput.Fire;
                            p1.Fire();
                        }
                        if (currentGPS.IsButtonDown(Buttons.DPadLeft) || currentGPS.IsButtonDown(Buttons.DPadRight))
                        {
                            playerOtherInput = PlayerOtherInput.SwitchWeapon;
                            //p1.SwitchWeapon();
                        }
                        if (currentGPS.IsButtonDown(Buttons.B))
                        {
                            playerOtherInput = PlayerOtherInput.Interact;
                            //p1.Interact();
                        }
                        if (currentGPS.IsButtonDown(Buttons.X))
                        {
                            playerOtherInput = PlayerOtherInput.Reload;
                            //p1.Reload();
                        }
                    }//end gamePad is connected
                    else
                    {
                        if (kStateCurrent.IsKeyDown(Keys.W) || kStateCurrent.IsKeyDown(Keys.Space) && !(p1.Jumping))
                        {
                            p1.Jump();
                            playerOtherInput = PlayerOtherInput.Jump;
                        }
                        if (kStateCurrent.IsKeyDown(Keys.A))
                        {
                            p1.WalkLeft();
                            playerMovementInput = PlayerMovementInput.Left;
                        }
                        /*
                        if (kStateCurrent.IsKeyDown(Keys.S))
                        {
                        
                        }
                        */
                        if (kStateCurrent.IsKeyDown(Keys.D))
                        {
                            p1.WalkRight();
                            playerMovementInput = PlayerMovementInput.Right;
                        }
                        if (mStateCurrent.LeftButton == ButtonState.Pressed)
                        {
                            p1.Fire();
                            playerOtherInput = PlayerOtherInput.Fire;
                        }
                        if (kStateCurrent.IsKeyDown(Keys.E))
                        {
                            playerOtherInput = PlayerOtherInput.Interact;
                        }
                        if (kStateCurrent.IsKeyDown(Keys.R))
                        {
                            playerOtherInput = PlayerOtherInput.Reload;
                        }
                    }//end gamePad is not connected 
                    p1.Gravity();
                    p1.PosUpdate();
                    p1.InvulnerabilityTimer();
                    foreach (Platform p in platformTilesList)
                    {
                        p1.CheckCollisions(p);
                    }

                    //always at least one zombie
                    if (zombieList.Count == 0)
                        zombieList.Add(new Zombie(new Rectangle(0, 343, zombieImage.Width, zombieImage.Height)));

                    for (int i = 0; i < zombieList.Count; i++)
                    {
                        //run zombie actions
                        Zombie zombie = zombieList[i];

                        if (zombie.ZombieAction == ZombieActions.Chase)
                        {
                            //get closest player
                            int distanceClosestPlayer = (int)Math.Sqrt(Math.Pow((p1.X - zombie.X), 2) + Math.Pow((p1.Y - zombie.Y), 2));
                            Player closestPlayer = p1;
                            if (p4 != null)
                            {
                                int dist = (int)Math.Sqrt(Math.Pow((p4.X - zombie.X), 2) + Math.Pow((p4.Y - zombie.Y), 2));
                                if (dist < distanceClosestPlayer)
                                {
                                    distanceClosestPlayer = dist;
                                    closestPlayer = p4;
                                }
                            }
                            if (p3 != null)
                            {
                                int dist = (int)Math.Sqrt(Math.Pow((p3.X - zombie.X), 2) + Math.Pow((p3.Y - zombie.Y), 2));
                                if (dist < distanceClosestPlayer)
                                {
                                    distanceClosestPlayer = dist;
                                    closestPlayer = p3;
                                }
                            }
                            if (p2 != null)
                            {
                                int dist = (int)Math.Sqrt(Math.Pow((p2.X - zombie.X), 2) + Math.Pow((p2.Y - zombie.Y), 2));
                                if (dist < distanceClosestPlayer)
                                {
                                    distanceClosestPlayer = dist;
                                    closestPlayer = p2;
                                }
                            }

                            //chase after closest player
                            if (zombie.X > closestPlayer.X)
                            {
                                zombie.WalkLeft();
                                zombie.Direction = -1;
                            }
                            else if (zombie.X < closestPlayer.X)
                            {
                                zombie.WalkRight();
                                zombie.Direction = 1;
                            }

                        }
                        else if (zombie.ZombieAction == ZombieActions.Patrol)
                        {
                            //move back and forth until player is detected
                            if (zombie.DetectPlayers(p1))
                                zombie.ZombieAction = ZombieActions.Chase;
                            else
                            {
                                if (zombie.Direction > 0) zombie.WalkRight();
                                else zombie.WalkLeft();

                                zombie.changeDirection();
                            }
                        }
                        else zombie.ZombieAction = ZombieActions.Patrol;

                        foreach (Zombie z in zombieList)
                            p1.CheckCollisions(z);

                    } //end loop through zombies' actions

                    //check if player can pickup item
                    foreach (Item item in activeItems)
                    {
                        p1.PickUpItemCheck(item);
                    }
                    //move bullets
                    //counts backwards so deleting bullets won't mess with the loop
                    for (int i = bulletList.Count-1; i >= 0; i--)
                    {
                        Bullet bullet = bulletList[i];
                        if (bullet.Active == true)
                        {
                            bullet.Move();
                            if (bullet.X < 0 || bullet.X > viewportWidth || bullet.Y < 0 || bullet.Y > viewportHeight)
                                bulletList.RemoveAt(i);
                        }
                        else bulletList.RemoveAt(i);
                    }

                    break; //end case inGame

                case GameState.Pause:
                    break;

                case GameState.SingleTinker:
                    break;

                case GameState.MultiTinker:
                    break;

                case GameState.GameOver:
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Debug Info Draw
            spriteBatch.DrawString(Resources.Courier, "Player X: " + p1.X + " Y: " + p1.Y, new Vector2(200, 50), Color.Black);
            spriteBatch.DrawString(Resources.Courier, "Player 1 HP: " + p1.HP, new Vector2(200, 75), Color.Black);
            spriteBatch.DrawString(Resources.Courier, "Zombie Direction: " + zombieList[0].Direction, new Vector2(200, 150), Color.Black);
            switch (gameState)
            {
                case GameState.Menu:
                    switch (menuButtonState)
                    {
                        case MenuButtonState.None:
                            break;

                        case MenuButtonState.Single:
                            break;

                        case MenuButtonState.Multi:
                            break;

                        case MenuButtonState.Quit:
                            break;
                    }
                    break; //end case Menu

                case GameState.InGame:
                    drawGround();
                    switch (gameLocation)
                    {
                        case GameLocation.Safehouse:

                            break;

                        case GameLocation.Level1:

                            break;

                        case GameLocation.Level2:

                            break;
                    }
                    //draw Zombie
                    if (gameLocation != GameLocation.Safehouse)
                        for (int i = 0; i < zombieList.Count; i++)
                        {
                            if (zombieList[i].Direction == -1)
                            {
                                spriteBatch.Draw(zombieImage, zombieList[i].Location, Color.White);
                                continue;
                            }
                            if (zombieList[i].Direction == 1)
                            {
                                spriteBatch.Draw(zombieImage, zombieList[i].Location, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.0f);
                                continue;
                                //spriteBatch.Draw(playerImage, p1.Location, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.0f);
                            }
                        }
                    switch (playerMovementInput)
                    {
                        case PlayerMovementInput.Left:
                            spriteBatch.Draw(playerImage, p1.Location, Color.White);
                            switch (playerOtherInput)
                            {
                                case PlayerOtherInput.Jump:

                                    break;

                                case PlayerOtherInput.Fire:

                                    break;

                                case PlayerOtherInput.Interact:

                                    break;

                                case PlayerOtherInput.Reload:

                                    break;

                                case PlayerOtherInput.SwitchWeapon:

                                    break;
                            }
                            break; //end left movement case

                        case PlayerMovementInput.Right:
                            spriteBatch.Draw(playerImage, p1.Location, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.0f);
                            switch (playerOtherInput)
                            {
                                case PlayerOtherInput.Jump:

                                    break;

                                case PlayerOtherInput.Fire:

                                    break;

                                case PlayerOtherInput.Interact:

                                    break;

                                case PlayerOtherInput.Reload:

                                    break;

                                case PlayerOtherInput.SwitchWeapon:

                                    break;
                            }
                            break; //end right movement case
                    }//end switch player movement

                    //draw in items
                    int count = activeItems.Count;
                    for (int i = 0; i < count;i++)
                    {
                        Item item = activeItems[i];

                        if (item.Active == true)
                        {
                            Texture2D draw = ammoImage;
                            if (item.GetType() == typeof(HealingItem))
                                draw = medkitImage;

                            spriteBatch.Draw(draw, item.Location, Color.White);
                        }
                        else //item has been picked up
                        {
                            activeItems.Remove(item);
                        }
                    }

                    //draw bullets
                    foreach (Bullet bullet in bulletList)
                        spriteBatch.Draw(bulletImage, bullet.Location, Color.White);

                    //************************GUI************************
                    spriteBatch.Draw(GUIMain, new Rectangle(0, 0, GUIMain.Width, GUIMain.Height), Color.White);

                    //get players then loop through and draw GUI elements
                    drawAmmo(p1);
                    drawAmmoClips(p1);
                    drawHPBar(p1);

                    if (p2 != null)
                    {
                        spriteBatch.Draw(GUIp2, new Rectangle(619, 5, GUIp2.Width, GUIp2.Height), Color.White);
                        drawAmmo(p2);
                        drawAmmoClips(p2);
                        drawHPBar(p2);
                    }
                    if (p3 != null)
                    {
                        spriteBatch.Draw(GUIp3, new Rectangle(5, 425, GUIp3.Width, GUIp3.Height), Color.White);
                        drawAmmo(p3);
                        drawAmmoClips(p3);
                        drawHPBar(p3);
                    }
                    if (p4 != null)
                    {
                        spriteBatch.Draw(GUIp4, new Rectangle(620, 425, GUIp4.Width, GUIp4.Height), Color.White);
                        drawAmmo(p4);
                        drawAmmoClips(p4);
                        drawHPBar(p4);
                    }

                    spriteBatch.Draw(GUIVerticalFadeBars, new Rectangle(0, 0, GUIMain.Width, GUIMain.Height), Color.White);
                    break; //end case inGame

                case GameState.Pause:
                    break;

                case GameState.SingleTinker:
                    break;

                case GameState.MultiTinker:
                    break;

                case GameState.GameOver:
                    break;
            }//end switch GameStates

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Prevents a key from being processed mutliple times if held
        /// </summary>
        /// <returns></returns>
        public Boolean SingleKeyPress(Keys k)
        {
            if (kStateCurrent.IsKeyDown(k) && kStatePrevious.IsKeyUp(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        private void drawHPBar(Player player)
        {
            int barX = 6;
            if (player.Number == 2 || player.Number == 4)
                barX = 662;

            int barY = 63;
            if (player.Number == 3 || player.Number == 4)
                barY = 428;

            //calculates bar size
            int percentHP = (player.HP * hpBarWidth / player.MaxHP);
            if (percentHP > hpBarWidth) { percentHP = hpBarWidth; }

            //draws the bar
            spriteBatch.Draw(GUIhpBARredside, new Rectangle(barX, barY, 1, 9), Color.White);
            spriteBatch.Draw(GUIhpBARred, new Rectangle(barX + 1, barY, percentHP, 9), Color.White);
            spriteBatch.Draw(GUIhpBARgrey, new Rectangle(percentHP + barX + 1, barY, hpBarWidth - percentHP, 9), Color.White);
            if (percentHP >= hpBarWidth)
                spriteBatch.Draw(GUIhpBARredside, new Rectangle(hpBarWidth + barX + 1, barY, 1, 9), Color.White);
            else
                spriteBatch.Draw(GUIhpBARgreyside, new Rectangle(hpBarWidth + barX + 1, barY, 1, 9), Color.White);
        }

        private void drawAmmoClips(Player player)
        {
            int ammoX = 89;
            if (player.Number == 2 || player.Number == 4)
                ammoX = 696;

            int ammoY = 50;
            if (player.Number == 3 || player.Number == 4)
                ammoY = 443;

            int ammoDir = -1;
            if (player.Number == 2 || player.Number == 4)
                ammoDir = 1;

            if (player.Items.Count > 0)
            {
                int ammoClipsUserHasLeft = 0;

                for (int i = 0; i < player.Items.Count; i++)
                {
                    if (player.Items[i].GetType() == typeof(AmmoItem))
                        ammoClipsUserHasLeft++;
                }

                for (int i = 0; i < ammoClipsUserHasLeft; i++)
                {
                    spriteBatch.Draw(GUIAmmo,
                        new Rectangle(ammoX + (16 * i * ammoDir), ammoY, GUIAmmo.Width, GUIAmmo.Height), Color.White);
                }
            }

        }

        private void drawAmmo(Player player)
        {
            int ammoX = 106;
            if (player.Number == 2 || player.Number == 4)
                ammoX = 632;
            int ammoY = 5;
            if (player.Number == 3 || player.Number == 4)
                ammoY = 441;

            int width = GUIAmmoClipEmpty.Width;
            int height = GUIAmmoClipEmpty.Height;
            SpriteEffects rotate = SpriteEffects.None;
            if (player.Number == 2)
                rotate = SpriteEffects.FlipHorizontally;
            else if (player.Number == 3)
                rotate = SpriteEffects.FlipVertically;
            else if (player.Number == 4)
                rotate = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

            int ammoDir = 1;
            if (player.Number == 3 || player.Number == 4)
                ammoDir = 0;

            //empty clip
            spriteBatch.Draw(GUIAmmoClipEmpty,
                new Rectangle(ammoX, ammoY, width, height), new Rectangle(0, 0, width, height), Color.White,
                0, Vector2.Zero, rotate, 0);

            if (player.CurrentClip != null)
            {
                int ammoLeft = (player.CurrentClip.Amount * height / player.CurrentClip.AmountTotal);

                //full/partially full clip
                spriteBatch.Draw(GUIAmmoClipFull,
                    new Vector2(ammoX, ammoY + (height - ammoLeft) * ammoDir),
                    new Rectangle(0, height - ammoLeft, width, ammoLeft), Color.White,
                    0, Vector2.Zero, 1, rotate, 0);
            }
        }

        private void initializeGround()
        {
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < (GraphicsDevice.Viewport.Width / tileSize); i++)
                {
                    if (j == 2)
                        platformTilesList.Add(new Platform(
                            new Rectangle(i * tileSize, viewportHeight - (j * tileSize) - (tileSize / 2), tileSize, tileSize),
                            new Vector2(0, 0)));
                    else
                        platformTilesList.Add(new Platform(
                            new Rectangle(i * tileSize, viewportHeight - (j * tileSize) - (tileSize / 2), tileSize, tileSize),
                            new Vector2(4, 0)));
                }

            // Load in the map.
            List<Platform> area = map.GetTiles();
            if(area != null) {
                platformTilesList.AddRange(area);
            }
        }

        private void drawGround()
        {
            for (int i = 0; i < platformTilesList.Count; i++)
                spriteBatch.Draw(tileSheet, platformTilesList[i].Location, platformTilesList[i].SourceRectangle, Color.White);
        }
    }
}
