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
            Globals.SB = new SpriteBatch(GraphicsDevice);
            Globals.SF = Content.Load<SpriteFont>("Arial40");

            Dictionary<int, Rectangle> hotbarLocations = new Dictionary<int, Rectangle>();
            int xPos = 18;
            for (int i = 0; i < 10; i++)
            {
                hotbarLocations[i + 1] = new Rectangle(xPos, 18, 45, 45);

                xPos += 47;
            }
            Globals.HotbarLocations = hotbarLocations;

            Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
            //------------------------------------------------------
            //Load textures here:
            textures["DebugImage"] = Content.Load<Texture2D>("DebugImage");

            textures["Player"] = Content.Load<Texture2D>("CharacterSpritesheet");
            textures["Inventory"] = Content.Load<Texture2D>("Inventory_style_02a");
            textures["InventoryCursor"] = Content.Load<Texture2D>("Inventory_select");
            textures["InventoryBar"] = Content.Load<Texture2D>("Inventory_Bar");
            //------------------------------------------------------
            Globals.GameTextures = textures;


            //--------------------------------------------------
            //class testing
            player = new Player();
            inventory = new Inventory();
            itemManager = new ItemManager();

            //event subscribing
            itemManager.GetInventorySlots += inventory.GiveInventorySlots;

            itemManager.TestMethod();
            //--------------------------------------------------
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //--------------------------------------------------
            //class testing
            player.Update();
            inventory.Update();
            itemManager.Update();
            //--------------------------------------------------

            base.Update(gameTime);
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //--------------------------------------------------
            //class testing
            player.Draw(time);
            inventory.Draw();

            if (inventory.IsOpen)
            {
                itemManager.Draw();
            }
            //--------------------------------------------------

            base.Draw(time);
        }
    }
}