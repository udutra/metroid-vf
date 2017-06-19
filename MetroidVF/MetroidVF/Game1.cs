using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MetroidVF
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState { Null, MainMenu, Playing };
        GameState currGameState = GameState.MainMenu;

        void EnterGameState(GameState newState)
        {
            LeaveGameState();

            currGameState = newState;

            switch (currGameState)
            {
                case GameState.MainMenu:
                    { }
                    break;

                case GameState.Playing:
                    { }
                    break;
            }
        }

        void LeaveGameState()
        {
            switch (currGameState)
            {
                case GameState.MainMenu:
                    { }
                    break;

                case GameState.Playing:
                    { }
                    break;               
            }
        }

        void UpdateGameState(GameTime gameTime)
        {
            switch (currGameState)
            {
                case GameState.MainMenu:
                    { }
                    break;

                case GameState.Playing:
                    { }
                    break;
            }
        }

        void DrawGameState(GameTime gameTime)
        {
            switch (currGameState)
            {
                case GameState.MainMenu:
                    { }
                    break;

                case GameState.Playing:
                    { }
                    break;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        
        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
                       

            base.Draw(gameTime);
        }
    }
}
