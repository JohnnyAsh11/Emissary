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

        private Player player_Test;

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

            Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
            //------------------------------------------------------
            //Load textures here:
            textures["DebugImage"] = Content.Load<Texture2D>("DebugImage");

            textures["Player"] = Content.Load<Texture2D>("CharacterSpritesheet");
            //------------------------------------------------------
            Globals.GameTextures = textures;

            //player_Test = new Player();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //player_Test.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //player_Test.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}