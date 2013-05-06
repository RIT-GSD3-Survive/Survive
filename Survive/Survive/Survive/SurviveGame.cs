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
    //global variables
    enum ZombieActions { Patrol, Chase }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SurviveGame :Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //images
        Texture2D GUIAmmo;
        Texture2D GUIMedkit;
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
        Texture2D gunSheet;
        Texture2D humanoidSheet;
        Texture2D portalImage;
        Texture2D backgroundImage;
        //game/menu states
        enum GameState { Menu, InGame, Pause, SingleTinker, MultiTinker, GameOver };
        enum MenuButtonState { None, Single, Multi, Quit };
        MenuButtonState menuButtonState;
        GameState gameState;
        //player input
        enum PlayerOtherInput { Jump, Fire, SwitchWeapon, Interact, Reload };
        PlayerOtherInput playerOtherInput;
        /// <summary>
        /// A dummy player for menus
        /// </summary>
        Player menuPlayer = new Player("MenuMan", 0, new Rectangle());
        //humanoid
        int humanoidWidth;
        int humanoidHeight;
        //players
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
        Dictionary<String, Rectangle> gunImagesList;

        //Random generator
        public static Random rgen;

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
            hpBarWidth = 130;
            tileSize = 32;
            viewportHeight = GraphicsDevice.Viewport.Height;
            viewportWidth = GraphicsDevice.Viewport.Width;
            humanoidHeight = 75;
            humanoidWidth = 23;

            platformTilesList = new List<Platform>();
            zombieList = new List<Zombie>();
            activeItems = new List<Item>();
            bulletList = new List<Bullet>();
            playerList = new List<Player>();

            gunImagesList = new Dictionary<string, Rectangle>();
            gunImagesList.Add("Pistol", new Rectangle(0, 0, 11, 8));
            gunImagesList.Add("SMG", new Rectangle(13, 0, 23, 13));
            gunImagesList.Add("AR", new Rectangle(36, 0, 40, 12));
            gunImagesList.Add("GunBarrel", new Rectangle(0, 14, 29, 10));
            gunImagesList.Add("GunScope", new Rectangle(0, 24, 26, 10));
            gunImagesList.Add("GunStock", new Rectangle(29, 14, 10, 12));
            gunImagesList.Add("GunBody", new Rectangle(26, 25, 28, 16));
            gunImagesList.Add("GunClip", new Rectangle(55, 13, 18, 24));

            initializeGround();
            rgen = new Random();

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
            backgroundImage = this.Content.Load<Texture2D>("Background");
            portalImage = this.Content.Load<Texture2D>("Portal");
            ammoImage = this.Content.Load<Texture2D>("Ammo");
            medkitImage = this.Content.Load<Texture2D>("Medkit");
            bulletImage = new Texture2D(graphics.GraphicsDevice, 1, 1);
            bulletImage.SetData<Color>(new Color[1] { Color.White }); //sets bullets to white


            GUIAmmo = this.Content.Load<Texture2D>("GUIAmmo");
            GUIMedkit = this.Content.Load<Texture2D>("GUIMedkit");
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
            gunSheet = this.Content.Load<Texture2D>("Guns");
            humanoidSheet = this.Content.Load<Texture2D>("PersonSheet");

            GlobalVariables.map = new Map();
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
            // getting gamePad, keyboard, and mouse info
            foreach (Player p in playerList)
            {
                p.Controls.Refresh();
            }
            /*
            previousGPS = currentGPS;
            currentGPS = GamePad.GetState(PlayerIndex.One);
            kStatePrevious = kStateCurrent;
            kStateCurrent = Keyboard.GetState();
            mStatePrevious = mStateCurrent;
            mStateCurrent = Mouse.GetState();
            //Converting 
            GamePadThumbSticks sticks = currentGPS.ThumbSticks;
            Vector2 left = sticks.Left;
            Vector2 right = sticks.Right;
            */
            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.Menu:
                    menuPlayer.Controls.Refresh();
                    if (menuPlayer.Controls.CurrentGPS.IsConnected)
                    {
                        //currentGPS = GamePad.GetState(0);

                        if (SingleKeyPress(Buttons.LeftThumbstickUp))
                        {
                            if (menuButtonState == MenuButtonState.None)
                            {
                                menuButtonState = MenuButtonState.Quit;
                            }
                            else if (menuButtonState == MenuButtonState.Single)
                            {
                                menuButtonState = MenuButtonState.Quit;
                            }
                            else if (menuButtonState == MenuButtonState.Multi)
                            {
                                menuButtonState = MenuButtonState.Single;
                            }
                            else if (menuButtonState == MenuButtonState.Quit)
                            {
                                menuButtonState = MenuButtonState.Multi;
                            }
                        }
                        if (SingleKeyPress(Buttons.LeftThumbstickDown))
                        {
                            if (menuButtonState == MenuButtonState.None)
                            {
                                menuButtonState = MenuButtonState.Single;
                            }
                            else if (menuButtonState == MenuButtonState.Single)
                            {
                                menuButtonState = MenuButtonState.Multi;
                            }
                            else if (menuButtonState == MenuButtonState.Multi)
                            {
                                menuButtonState = MenuButtonState.Quit;
                            }
                            else if (menuButtonState == MenuButtonState.Quit)
                            {
                                menuButtonState = MenuButtonState.Single;
                            }
                        }
                        if (menuPlayer.Controls.CurrentGPS.IsButtonDown(Buttons.A))
                        {
                            playerList.Clear(); // Empty the player list.
                            if (menuButtonState == MenuButtonState.Single)
                            {
                                playerList.Add(new Player("Name", 1, new Rectangle(200, 345, humanoidWidth, humanoidHeight)));
                                gameState = GameState.InGame;
                            }
                            if (menuButtonState == MenuButtonState.Multi)
                            {
                                if (GamePad.GetState(PlayerIndex.One).IsConnected)
                                {
                                    playerList.Add(new Player("Name", PlayerIndex.One, new Rectangle(200, 345, humanoidWidth, humanoidHeight)));
                                }
                                if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                                {
                                    playerList.Add(new Player("Name", PlayerIndex.Two, new Rectangle(220, 345, humanoidWidth, humanoidHeight)));
                                }
                                if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                                {
                                    playerList.Add(new Player("Name", PlayerIndex.Three, new Rectangle(240, 345, humanoidWidth, humanoidHeight)));
                                }
                                if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                                {
                                    playerList.Add(new Player("Name", PlayerIndex.Four, new Rectangle(260, 345, humanoidWidth, humanoidHeight)));
                                }
                                gameState = GameState.InGame;
                            }
                            if (menuButtonState == MenuButtonState.Quit)
                            {
                                this.Exit();
                            }
                        }
                    }
                    else
                    {
                        if (menuPlayer.Controls.CurrentMS.X > 280 && menuPlayer.Controls.CurrentMS.X < 500)
                        {
                            if (menuPlayer.Controls.CurrentMS.Y > 165 && menuPlayer.Controls.CurrentMS.Y < 210)
                            {
                                menuButtonState = MenuButtonState.Single;
                            }
                            else if (menuPlayer.Controls.CurrentMS.Y > 265 && menuPlayer.Controls.CurrentMS.Y < 310)
                            {
                                menuButtonState = MenuButtonState.Multi;
                            }
                            else if (menuPlayer.Controls.CurrentMS.Y > 365 && menuPlayer.Controls.CurrentMS.Y < 410)
                            {
                                menuButtonState = MenuButtonState.Quit;
                            }
                            else
                            {
                                menuButtonState = MenuButtonState.None;
                            }
                        }
                        else
                        {
                            menuButtonState = MenuButtonState.None;
                        }
                    }
                    if (menuPlayer.Controls.CurrentMS.LeftButton == ButtonState.Pressed)
                    {
                        playerList.Clear(); // Empty the player list.
                        if (menuButtonState == MenuButtonState.Single)
                        {
                            playerList.Add(new Player("Name", 1, new Rectangle(200, 345, humanoidWidth, humanoidHeight)));
                            gameState = GameState.InGame;
                        }
                        if (menuButtonState == MenuButtonState.Multi)
                        {
                            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                            {
                                playerList.Add(new Player("Name", PlayerIndex.One, new Rectangle(200, 345, humanoidWidth, humanoidHeight)));
                            }
                            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                            {
                                playerList.Add(new Player("Name", PlayerIndex.Two, new Rectangle(220, 345, humanoidWidth, humanoidHeight)));
                            }
                            if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                            {
                                playerList.Add(new Player("Name", PlayerIndex.Three, new Rectangle(240, 345, humanoidWidth, humanoidHeight)));
                            }
                            if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                            {
                                playerList.Add(new Player("Name", PlayerIndex.Four, new Rectangle(260, 345, humanoidWidth, humanoidHeight)));
                            }
                            gameState = GameState.InGame;
                        }
                        if (menuButtonState == MenuButtonState.Quit)
                        {
                            this.Exit();
                        }
                    }
                    break;

                case GameState.InGame:
                    foreach (Player p in playerList)
                    {
                        if (p.Controls.MoveRight())
                        {
                            p.WalkRight();
                            p.Vote = null;
                        }
                        if (p.Controls.MoveLeft())
                        {
                            p.WalkLeft();
                            p.Vote = null;
                        }
                        if (p.Controls.IsJump())
                        {
                            playerOtherInput = PlayerOtherInput.Jump;
                            p.Jump();
                            p.Vote = null;
                        }
                        if (p.Controls.IsFire())
                        {
                            playerOtherInput = PlayerOtherInput.Fire;
                            Bullet b = null;
                            if ((b = p.Fire()) != null)
                                bulletList.Add(b);
                        }
                        if (p.Controls.Interact())
                        {
                            playerOtherInput = PlayerOtherInput.Interact;
                            p.Interact();
                        }
                        if (p.Controls.Pause())
                        {
                            gameState = GameState.Pause;
                        }
                        if (p.Controls.Reload())
                        {
                            playerOtherInput = PlayerOtherInput.Reload;
                            p.Reload();
                        }
                        if (p.Controls.SwitchWeaponsNext())
                        {
                            playerOtherInput = PlayerOtherInput.SwitchWeapon;
                            p.SwitchWeaponsNext();
                        }
                        if (p.Controls.SwitchWeaponsPrevious())
                        {
                            playerOtherInput = PlayerOtherInput.SwitchWeapon;
                            p.SwitchWeaponsPrevious();
                        }
                        if (p.Controls.Heal())
                        {
                            if (p.HealingItemsAmount > 0 && p.HP != 100)
                            {
                                p.HealingItemsAmount--;
                                p.HP += rgen.Next(10, 30);
                            }
                        }
                        p.Gravity();
                        p.PosUpdate();
                        p.InvulnerabilityTimer();
                        p.FireRateTimer();
                        foreach (Platform pl in platformTilesList)
                        {
                            p.CheckCollisions(pl, p);
                        }
                        foreach (Platform pl in GlobalVariables.map.GetTiles())
                        {
                            p.CheckCollisions(pl, p);
                        }
                    }

                    bool go = true;
                    Portal use = null;
                    foreach (Player p in playerList)
                    {
                        if (use == null && p.Vote != null)
                        {
                            use = p.Vote;
                        }
                        else
                        {
                            go = false;
                        }
                    }
                    if (go && use != null)
                    {
                        GlobalVariables.map.SwitchArea(use.LinkTo);
                        foreach (Player p in playerList)
                        {
                            p.X = use.LinkX;
                            p.Y = use.LinkY;
                            p.Vote = null;
                        }
                        zombieList.Clear();
                    }

                    //always at least one zombie
                    if (zombieList.Count == 0)
                        zombieList.Add(new Zombie(new Rectangle(400, 345, humanoidWidth, humanoidHeight)));
                    if (!(GlobalVariables.map.AtSafehouse))
                    {
                        for (int i = 0; i < zombieList.Count; i++)
                        {
                            //run zombie actions
                            Zombie zombie = zombieList[i];

                            if (zombie.ZombieAction == ZombieActions.Chase)
                            {
                                //get closest player
                                int distanceClosestPlayer = int.MaxValue;
                                Player closestPlayer = null;
                                foreach (Player player in playerList)
                                {
                                    int dist = (int)Math.Sqrt(Math.Pow((player.X - zombie.X), 2) + Math.Pow((player.Y - zombie.Y), 2));
                                    if (dist < distanceClosestPlayer)
                                    {
                                        distanceClosestPlayer = dist;
                                        closestPlayer = player;
                                    }
                                }

                                //chase after closest player
                                if (zombie.X > closestPlayer.X)
                                {
                                    zombie.WalkLeft();
                                    zombie.FacingRight = false;


                                }
                                else if (zombie.X < closestPlayer.X)
                                {
                                    zombie.WalkRight();
                                    zombie.FacingRight = true;
                                }

                                zombie.PosUpdate();
                                zombie.Gravity();

                                if (zombie.Jumping == true)
                                {
                                    zombie.MoveSpeed = 2;
                                }
                                else
                                {
                                    zombie.MoveSpeed = 1;
                                }
                            }
                            else if (zombie.ZombieAction == ZombieActions.Patrol)
                            {
                                //move back and forth until player is detected
                                foreach (Player player in playerList)
                                {
                                    if (zombie.DetectPlayers(player))
                                        zombie.ZombieAction = ZombieActions.Chase;
                                }
                                if (zombie.FacingRight) zombie.WalkRight();
                                else zombie.WalkLeft();

                                zombie.changeDirection();
                            }
                            else zombie.ZombieAction = ZombieActions.Patrol;

                            foreach (Zombie z in zombieList)
                            {
                                foreach (Player player in playerList)
                                {
                                    player.CheckCollisions(z, player);
                                }
                                foreach (Platform p in platformTilesList)
                                {
                                    z.CheckCollisions(p, z);
                                }
                                foreach (Platform pl in GlobalVariables.map.GetTiles())
                                {
                                    z.CheckCollisions(pl, z);
                                }
                            }
                            if (zombie.HP <= 0)
                            {
                                if (rgen.Next(40) == 0)
                                {
                                    activeItems.Add(new AmmoItem(rgen.Next(50, 100), new Rectangle(zombieList[i].X, zombieList[i].Y + zombieList[i].Location.Height - ammoImage.Height, ammoImage.Width, ammoImage.Height)));
                                }
                                else if (rgen.Next(25) == 0)
                                {
                                    activeItems.Add(new HealingItem(new Rectangle(zombieList[i].X, zombieList[i].Y + zombieList[i].Location.Height - medkitImage.Height, medkitImage.Width, medkitImage.Height)));
                                }
                                else if (rgen.Next(15) == 0)
                                {
                                    activeItems.Add(new WeaponStock("Pistol", rgen.Next(60, 100), rgen.Next(1, 5), rgen.Next(10, 25), rgen.Next(1, 3), rgen.Next(6, 12), "Pistol", rgen.Next(1, 15), new Rectangle(zombieList[i].X, zombieList[i].Y + zombieList[i].Location.Height - gunImagesList["Pistol"].Height, gunImagesList["Pistol"].Width * 3, gunImagesList["Pistol"].Height * 3)));
                                }
                                else if (rgen.Next(10) == 0)
                                {
                                    activeItems.Add(new WeaponStock("SMG", rgen.Next(50, 100), rgen.Next(6, 10), rgen.Next(10, 25), rgen.Next(2, 5), rgen.Next(20, 40), "SMG", rgen.Next(1, 15), new Rectangle(zombieList[i].X, zombieList[i].Y + zombieList[i].Location.Height - gunImagesList["SMG"].Height, gunImagesList["SMG"].Width * 2, gunImagesList["SMG"].Height * 2)));
                                }
                                else if (rgen.Next(5) == 0)
                                {
                                    activeItems.Add(new WeaponStock("AR", rgen.Next(75, 100), rgen.Next(11, 20), rgen.Next(20, 50), rgen.Next(4, 10), rgen.Next(30, 50), "AR", rgen.Next(1, 15), new Rectangle(zombieList[i].X, zombieList[i].Y + zombieList[i].Location.Height - gunImagesList["AR"].Height, gunImagesList["AR"].Width, gunImagesList["AR"].Height)));
                                }
                                zombieList.Remove(zombieList[i]);
                            }
                        }
                    } //end loop through zombies' actions

                    //check if player can pickup item
                    foreach (Item item in activeItems)
                    {
                        foreach (Player p in playerList)
                        {
                            p.PickUpItemCheck(item);
                        }
                    }
                    //move bullets
                    //counts backwards so deleting bullets won't mess with the loop
                    for (int i = bulletList.Count - 1; i >= 0; i--)
                    {
                        Bullet bullet = bulletList[i];
                        if (bullet.Active == true)
                        {
                            bullet.Move();
                            foreach (Zombie z in zombieList)
                                z.CheckCollisions(bullet, z);

                            if (bullet.X < 0 || bullet.X > viewportWidth || bullet.Y < 0 || bullet.Y > viewportHeight)
                                bulletList.RemoveAt(i);
                        }
                        else bulletList.RemoveAt(i);
                    }

                    //move bullets
                    //counts backwards so deleting bullets won't mess with the loop
                    for (int i = bulletList.Count - 1; i >= 0; i--)
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

                    // Check Player Liveliness
                    List<Player> temp = new List<Player>(playerList); // Create a temporary list to be looping through
                    foreach (Player p in temp)
                    {
                        if (p.HP <= 0)
                        { // If they're dead
                            playerList.Remove(p); // Remove from player list
                        }
                    }
                    if (playerList.Count == 0)
                    { // If everyone's dead
                        gameState = GameState.GameOver; // Game Over.
                    }

                    break; //end case inGame

                case GameState.Pause:
                    foreach (Player p in playerList)
                    {
                        if (SingleKeyPress(p, Buttons.A) || SingleKeyPress(p, Keys.Enter))
                        {
                            gameState = GameState.Menu;
                            break;
                        }
                    }
                    break;

                case GameState.SingleTinker:
                    break;

                case GameState.MultiTinker:
                    break;

                case GameState.GameOver:
                    menuPlayer.Controls.Refresh();
                    if (SingleKeyPress(Buttons.A) || SingleKeyPress(Keys.Enter))
                    {
                        gameState = GameState.Menu;
                        menuButtonState = MenuButtonState.None;
                        break;
                    }
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

            switch (gameState)
            {
                case GameState.Menu:
                    spriteBatch.DrawString(Resources.Courier, " SURVIVE!", new Vector2(323, 75), Color.Crimson);
                    spriteBatch.DrawString(Resources.Courier, "Single Player", new Vector2(300, 175), (menuButtonState == MenuButtonState.Single) ? Color.Gold : Color.Black);
                    spriteBatch.DrawString(Resources.Courier, "Multi Player", new Vector2(310, 275), (menuButtonState == MenuButtonState.Multi) ? Color.Gold : Color.Black);
                    spriteBatch.DrawString(Resources.Courier, "Quit", new Vector2(360, 375), (menuButtonState == MenuButtonState.Quit) ? Color.Gold : Color.Black);
                    break; //end case Menu

                case GameState.InGame:
                    DrawGameScreen();

                    break;

                case GameState.Pause:
                    DrawGameScreen();
                    spriteBatch.DrawString(Resources.Courier, "PAUSED", new Vector2(320, 225), Color.Black);
                    if (playerList[0].Controls.CurrentGPS.IsConnected)
                    {
                        spriteBatch.DrawString(Resources.Courier, "Hit A to Continue", new Vector2(245, 265), Color.Black);
                    }
                    else
                    {
                        spriteBatch.DrawString(Resources.Courier, "Hit Enter to Continue", new Vector2(240, 265), Color.Black);
                    }
                    break;

                case GameState.SingleTinker:
                    break;

                case GameState.MultiTinker:
                    break;

                case GameState.GameOver:
                    spriteBatch.DrawString(Resources.Courier, "GAME OVER!", new Vector2(320, 75), Color.Maroon);
                    spriteBatch.DrawString(Resources.Courier, "Score : ", new Vector2(300, 175), Color.Black);
                    spriteBatch.DrawString(Resources.Courier, "Hit Enter to Continue", new Vector2(240, 275), Color.Black);
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
            return SingleKeyPress(menuPlayer, k);
        }

        public Boolean SingleKeyPress(Buttons b)
        {
            return SingleKeyPress(menuPlayer, b);
        }

        private Boolean SingleKeyPress(Player p, Keys k)
        {
            if (p.Controls.CurrentKS.IsKeyDown(k) && p.Controls.PreviousKS.IsKeyDown(k))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean SingleKeyPress(Player p, Buttons b)
        {
            if (p.Controls.CurrentGPS.IsButtonDown(b) && p.Controls.PreviousGPS.IsButtonUp(b))
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
            if (player.PIndex == PlayerIndex.Two || player.PIndex == PlayerIndex.Four)
                barX = 662;

            int barY = 63;
            if (player.PIndex == PlayerIndex.Three || player.PIndex == PlayerIndex.Four)
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
            if (player.PIndex == PlayerIndex.Two || player.PIndex == PlayerIndex.Four)
                ammoX = 696;

            int ammoY = 50;
            if (player.PIndex == PlayerIndex.Three || player.PIndex == PlayerIndex.Four)
                ammoY = 443;

            int ammoDir = -1;
            if (player.PIndex == PlayerIndex.Two || player.PIndex == PlayerIndex.Four)
                ammoDir = 1;

            if (player.Items.Count > 0)
            {
                int ammoClipsUserHasLeft = 0;

                foreach (Item item in player.Items)
                {
                    if (item.GetType() == typeof(AmmoItem))
                        ammoClipsUserHasLeft++;
                }

                for (int i = 0; i < ammoClipsUserHasLeft; i++)
                {
                    spriteBatch.Draw(GUIAmmo,
                        new Rectangle(ammoX + (16 * i * ammoDir), ammoY, GUIAmmo.Width, GUIAmmo.Height), Color.White);
                }
            }
        }

        private void drawMedkitIcons(Player player)
        {
            int mkX = 77;
            if (player.PIndex == PlayerIndex.Two || player.PIndex == PlayerIndex.Four)
                mkX = 696;

            int mkY = 38;
            if (player.PIndex == PlayerIndex.Three || player.PIndex == PlayerIndex.Four)
                mkY = 431;

            int mkDir = -1;
            if (player.PIndex == PlayerIndex.Two || player.PIndex == PlayerIndex.Four)
                mkDir = 1;

            for (int i = 0; i < player.HealingItemsAmount; i++)
            {
                spriteBatch.Draw(GUIMedkit,
                    new Rectangle(mkX + (16 * i * mkDir), mkY, GUIMedkit.Width, GUIMedkit.Height), Color.White);
            }
        }

        private void drawAmmo(Player player)
        {
            int ammoX = 106;
            if (player.PIndex == PlayerIndex.Two || player.PIndex == PlayerIndex.Four)
                ammoX = 632;
            int ammoY = 5;
            if (player.PIndex == PlayerIndex.Three || player.PIndex == PlayerIndex.Four)
                ammoY = 441;

            int width = GUIAmmoClipEmpty.Width;
            int height = GUIAmmoClipEmpty.Height;
            SpriteEffects rotate = SpriteEffects.None;
            if (player.PIndex == PlayerIndex.Two)
                rotate = SpriteEffects.FlipHorizontally;
            else if (player.PIndex == PlayerIndex.Three)
                rotate = SpriteEffects.FlipVertically;
            else if (player.PIndex == PlayerIndex.Four)
                rotate = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;

            int ammoDir = 1;
            if (player.PIndex == PlayerIndex.Three || player.PIndex == PlayerIndex.Four)
                ammoDir = 0;

            //empty clip
            spriteBatch.Draw(GUIAmmoClipEmpty,
                new Rectangle(ammoX, ammoY, width, height), new Rectangle(0, 0, width, height), Color.White,
                0, Vector2.Zero, rotate, 0);

            if (player.CurrentClip != null)
            {
                int ammoLeft = 0;
                if (player.CurrentClip.ClipCapacity != 0)
                {
                    ammoLeft = (player.CurrentClip.Current * height / player.CurrentClip.ClipCapacity);
                }

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
            // List<Platform> area = map.GetTiles();
            // if (area != null)
            //     platformTilesList.AddRange(area);
        }

        private void DrawGround()
        {
            for (int i = 0; i < platformTilesList.Count; i++)
                spriteBatch.Draw(tileSheet, platformTilesList[i].Location, platformTilesList[i].SourceRectangle, Color.White);
            GlobalVariables.map.DrawArea(spriteBatch);
        }

        private void DrawGameScreen()
        {
            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, viewportWidth, viewportHeight), Color.White);
            DrawGround();
            //draw Zombie
            if (!GlobalVariables.map.AtSafehouse)
                foreach (Zombie z in zombieList)
                {
                    DrawHumanoid(z);
                    //spriteBatch.DrawString(Resources.Courier, "Zombie Jumping: " + zombieList[0].Jumping, new Vector2(200, 100), Color.Black);
                }


            //draw bullets
            foreach (Bullet bullet in bulletList)
                spriteBatch.Draw(bulletImage, bullet.Location, Color.White);

            foreach (Player p in playerList)
            {
                DrawHumanoid(p);
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
                break;
            }

            //draw in items
            int count = activeItems.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                Item item = activeItems[i];

                if (item.Active == true)
                {
                    if (item.GetType() == typeof(AmmoItem))
                        spriteBatch.Draw(ammoImage, item.Location, Color.White);
                    else if (item.GetType() == typeof(HealingItem))
                        spriteBatch.Draw(medkitImage, item.Location, Color.White);
                    else if (item is Weapon)
                    {
                        Weapon weapon = (Weapon)item;
                        spriteBatch.Draw(gunSheet, weapon.Location, gunImagesList[weapon.Type], Color.White);
                    }
                    else if (item is GunBarrel)
                        spriteBatch.Draw(gunSheet, item.Location, gunImagesList["GunBarrel"], Color.White);
                    else if (item is GunBody)
                        spriteBatch.Draw(gunSheet, item.Location, gunImagesList["GunBody"], Color.White);
                    else if (item is GunClip)
                        spriteBatch.Draw(gunSheet, item.Location, gunImagesList["GunClip"], Color.White);
                    else if (item is GunScope)
                        spriteBatch.Draw(gunSheet, item.Location, gunImagesList["GunScope"], Color.White);
                    else if (item is GunStock)
                        spriteBatch.Draw(gunSheet, item.Location, gunImagesList["GunStock"], Color.White);
                }
                else //item has been picked up
                {
                    activeItems.Remove(item);
                }
            }
            //************************GUI************************
            spriteBatch.Draw(GUIMain, new Rectangle(0, 0, GUIMain.Width, GUIMain.Height), Color.White);

            //get players then loop through and draw GUI elements
            foreach (Player p in playerList)
            {
                if (p.PIndex != PlayerIndex.One)
                {
                    switch (p.PIndex)
                    {
                        case PlayerIndex.Two:
                            spriteBatch.Draw(GUIp2, new Rectangle(619, 5, GUIp2.Width, GUIp2.Height), Color.White);
                            break;
                        case PlayerIndex.Three:
                            spriteBatch.Draw(GUIp3, new Rectangle(5, 425, GUIp3.Width, GUIp3.Height), Color.White);
                            break;
                        case PlayerIndex.Four:
                            spriteBatch.Draw(GUIp4, new Rectangle(620, 425, GUIp4.Width, GUIp4.Height), Color.White);
                            break;
                    }
                }
                drawAmmo(p);
                drawMedkitIcons(p);
                drawAmmoClips(p);
                drawHPBar(p);
            }

            spriteBatch.Draw(GUIVerticalFadeBars, new Rectangle(0, 0, GUIMain.Width, GUIMain.Height), Color.White);
        }

        private void DrawHumanoid(Humanoid obj)
        {
            Rectangle arm = new Rectangle(0, 22, 24, 17);
            Rectangle leg = new Rectangle(43, 0, 13, 27);
            Rectangle body = new Rectangle(24, 0, 19, 32);
            Rectangle head = new Rectangle(0, 0, 23, 22);

            if (obj is Zombie)
            {
                int z = 56;
                arm.X += z;
                leg.X += z;
                body.X += z;
                head.X += z;
            }

            //flip parts
            SpriteEffects flip = SpriteEffects.FlipHorizontally;
            if (obj.FacingRight == false)
            {
                flip = SpriteEffects.None;
                //draw bottom arm
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X - 5, obj.Y + 23, arm.Width, arm.Height), arm,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw bottom leg
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 2, obj.Y + 48, leg.Width, leg.Height), leg,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw body
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 3, obj.Y + 20, body.Width, body.Height), body,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw head
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X, obj.Y, head.Width, head.Height), head,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw gun (if player)
                if (obj is Player)
                {
                    //get gun image rectangle
                    Rectangle rect = gunImagesList[((Player)obj).CurrentWeapon.Type];
                    spriteBatch.Draw(gunSheet, new Rectangle(obj.X - 9, obj.Y + 31, rect.Width, rect.Height), rect,
                        Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                }

                //draw top leg
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 9, obj.Y + 48, leg.Width, leg.Height), leg,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw top arm
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X - 3, obj.Y + 23, arm.Width, arm.Height), arm,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
            }
            else
            {
                //draw bottom arm
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 6, obj.Y + 23, arm.Width, arm.Height), arm,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw bottom leg
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 8, obj.Y + 48, leg.Width, leg.Height), leg,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw body
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 1, obj.Y + 20, body.Width, body.Height), body,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw head
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X, obj.Y, head.Width, head.Height), head,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw gun (if player)
                if (obj is Player)
                {
                    //get gun image rectangle
                    Rectangle rect = gunImagesList[((Player)obj).CurrentWeapon.Type];
                    spriteBatch.Draw(gunSheet, new Rectangle(obj.X + 22, obj.Y + 31, rect.Width, rect.Height), rect,
                        Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                }
                //draw top leg
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 1, obj.Y + 48, leg.Width, leg.Height), leg,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
                //draw top arm
                spriteBatch.Draw(humanoidSheet, new Rectangle(obj.X + 2, obj.Y + 23, arm.Width, arm.Height), arm,
                    Color.White, 0.0f, new Vector2(0, 0), flip, 0.0f);
            }
        }
    }
}