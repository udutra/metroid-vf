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
        Texture2D uiTex, texMainMenu;
        SpriteFont uiFont;
        public static bool bulletDir;
        public static bool BulletUP;
        public static Human hum;

        public enum GameState { Null, MainMenu, Playing };
        public static GameState currGameState = GameState.MainMenu;

        public void EnterGameState(GameState newState)
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

        public void LeaveGameState()
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

        public void UpdateGameState(GameTime gameTime)
        {
            switch (currGameState)
            {
                case GameState.MainMenu:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            EnterGameState(GameState.Playing);
                        }
                    }
                    break;

                case GameState.Playing:
                    {
                        List<Entity> tmp = new List<Entity>(entities);
                        foreach (Entity e in tmp)
                            e.Update(gameTime);
                    }
                    break;
            }
        }

        public void DrawGameState(GameTime gameTime)
        {
            switch (currGameState)
            {
                case GameState.Playing:
                    {
                        foreach (Entity e in entities)
                        {
                            e.Draw(gameTime);

                            spriteBatch.Draw(uiTex, new Vector2(50, 75), Color.White);
                            string aux = "" + hum.GetHealth();
                            spriteBatch.DrawString(uiFont, aux, new Vector2(130, 80), Color.White);


                        }
                    }
                    break;

                case GameState.MainMenu:
                    {
                        spriteBatch.Draw(texMainMenu, new Vector2(1, 1), Color.White);
                    }
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
            entities.Add(map = new Map("Content/Map/map.tmx", "Map/metro"));
            Human.Content = Content;
            Enemy1.Content = Content;
            Enemy2.Content = Content;
            Bullet.Content = Content;
            Door.Content = Content;
            PowerUp.Content = Content;

           

            //Samus
            hum = new Human(new Vector2(1032, 305));
            entities.Add(hum);

            

            //UI
            uiTex = Content.Load<Texture2D>("Sprites/UI");
            uiFont = Content.Load<SpriteFont>("Fonts/Fonts");

            //MainMenu
            texMainMenu = Content.Load<Texture2D>("Sprites/MainMenu");

            EnterGameState(GameState.MainMenu);

        }
        
        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           

            UpdateGameState(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            DrawGameState(gameTime);

            spriteBatch.End();


            
            base.Draw(gameTime);
        }


        public static void DrawInimigos()
        {
            //Doors
            entities.Add(new Door(new Vector2(2430, 209)));
            entities.Add(new Door(new Vector2(2942, 209)));
            

            //PowerUP
            entities.Add(new PowerUp(new Vector2(465, 290)));

            //Enemy2 sala 1
            entities.Add(new Enemy2(new Vector2(935, 80)));
            entities.Add(new Enemy2(new Vector2(1132, 81)));
            entities.Add(new Enemy2(new Vector2(2185, 400)));
            //enemy2 sala 2
            entities.Add(new Enemy2(new Vector2(2706, 177)));

            //Enemy1 sala 1
            entities.Add(new Enemy1(new Vector2(1490, 65)));
            entities.Add(new Enemy1(new Vector2(1646, 125)));
        }
    }
}
