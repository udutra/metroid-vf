using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MetroidVF
{
    public class Game1 : Game
    {
        public static Game1 instance = null;

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Camera camera = null;
        public static List<Entity> entities = new List<Entity>();
        public static Map map;
        public static Texture2D textest;        

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
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 250 * 2;
            graphics.PreferredBackBufferHeight = 240 * 2;

        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera();
            entities.Add(camera);
            entities.Add(map = new Map("Content/Map/map1.tmx", "Map/metro"));
            Human.Content = Content;
            Enemy1.Content = Content;
            Enemy2.Content = Content;

            entities.Add(new Human(new Vector2(1500, 325)));

            //Enemy POS OK!
            entities.Add(new Enemy2(new Vector2(905, 80)));
            entities.Add(new Enemy2(new Vector2(1102, 81)));
                       

            textest = Content.Load<Texture2D>("Sprites/enemy1");
        }
        
        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            List<Entity> tmp = new List<Entity>(entities);
            foreach (Entity e in tmp)
                e.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (Entity e in entities)
                e.Draw(gameTime);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
