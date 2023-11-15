using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Emissary
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //--------------------------------------------------
        //class testing
        private Player player;
        private Inventory inventory;
        private ItemManager itemManager;
        private TileManager tileManager;

        private Enemy enemyTest;
        //--------------------------------------------------

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //changing the window sizing to be our preferred size
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            #region Assigning values to Globals class fields
            Globals.SB = new SpriteBatch(GraphicsDevice);
            Globals.SF = Content.Load<SpriteFont>("Arial40");

            Dictionary<int, Rectangle> hotbarLocations = new Dictionary<int, Rectangle>();
            Dictionary<TileTexture, Rectangle> tiles = new Dictionary<TileTexture, Rectangle>();

            int tileX = 0;
            int tileY = 0;
            for (int i = 0; i < 64; i++)
            {
                tiles[(TileTexture)i] = new Rectangle(tileX, tileY, 32, 32);

                tileX += 32;
                if (tileX >= 224)
                {
                    tileX = 0;
                    tileY += 32;
                }
            }
            Globals.PrintTiles = tiles;

            int xPos = 18;
            for (int i = 0; i < 10; i++)
            {
                hotbarLocations[i + 1] = new Rectangle(xPos, 18, 45, 45);

                xPos += 47;
            }
            Globals.HotbarPositions = hotbarLocations;
            #endregion

            Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
            //------------------------------------------------------
            //Load textures here:
            textures["DebugImage"] = Content.Load<Texture2D>("DebugImage");

            textures["Player"] = Content.Load<Texture2D>("CharacterSpritesheet");
            textures["Inventory"] = Content.Load<Texture2D>("Inventory_style_02a");
            textures["InventoryCursor"] = Content.Load<Texture2D>("Inventory_select");
            textures["InventoryBar"] = Content.Load<Texture2D>("Inventory_Bar");
            textures["GrassTiles"] = Content.Load<Texture2D>("TX Tileset Grass");
            //------------------------------------------------------
            Globals.GameTextures = textures;


            //--------------------------------------------------
            //class testing
            player = new Player();
            inventory = new Inventory();
            itemManager = new ItemManager();
            tileManager = new TileManager("../../../Rooms/BaseLevel.txt");

            enemyTest = new Enemy(
                new FloatRectangle(800, 480, 50, 50),
                Globals.GameTextures["DebugImage"],
                10);

            //event subscribing
            itemManager.GetInventorySlots += inventory.GiveInventorySlots;
            player.GetCurrentCollidableTiles += tileManager.GetCollidableTiles;

            itemManager.TestMethod();
            //--------------------------------------------------
        }

        protected override void Update(GameTime gameTime)
        {
            //updating the GameTime reference in Globals
            Globals.Time = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //--------------------------------------------------
            //class testing
            player.Update();
            inventory.Update();
            itemManager.Update();

            enemyTest.Update();
            //--------------------------------------------------

            base.Update(gameTime);
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //--------------------------------------------------
            //class testing
            tileManager.Draw();
            player.Draw();
            inventory.Draw();

            if (inventory.IsOpen)
            {
                itemManager.Draw();
            }

            enemyTest.Draw();
            //--------------------------------------------------

            base.Draw(time);
        }
    }
}