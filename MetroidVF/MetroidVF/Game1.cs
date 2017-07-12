using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        public static Texture2D uiTex, texMainMenu, texGameOver;
        public static SpriteFont uiFont;
        public static bool bulletDir;
        public static bool BulletUP;
        public static Human hum;
        public static Door d1, d2, d3;
        public static PowerUp pu1;
        public static ExitGame eg1;
        public static Enemy2 e21, e22, e23, e24, e25, e26, e27;
        public static Enemy1 e11, e12, e13, e14, e15, e16;
        public static SoundEffectInstance playSound;
        public SoundEffect sndMenu;
        public static bool iniciaMusica = false;
        float timeCounter = 0f;

        public enum GameState { Null, MainMenu, Playing };
        public static GameState currGameState = GameState.MainMenu;

        public static void EnterGameState(GameState newState)
        {
            LeaveGameState();

            currGameState = newState;

            switch (currGameState)
            {
                case GameState.Null:
                    {
                        hum = null;
                        DrawHumano();
                    }
                    break;

                case GameState.MainMenu:
                    {
                         playSound.Play();

                    }
                    break;

                case GameState.Playing:
                    {
                        iniciaMusica = true;
                        DrawHumano();

                        //DrawInimigosSala1();
                    }
                    break;
            }
        }

        public static void LeaveGameState()
        {
            switch (currGameState)
            {

                case GameState.Null:
                    {
                       
                    }
                    break;

                case GameState.MainMenu:
                    {
                        playSound.Stop();
                    }
                    break;

                case GameState.Playing:
                    {
                        LimpaSala1();
                        LimpaSala2();
                        LimpaSala3();

                    }
                    break;               
            }
        }

        public void UpdateGameState(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (currGameState)
            {

                case GameState.Null:
                    {
                        if(timeCounter <= 4)
                        {
                            timeCounter += dt;
                        }
                        else
                        {
                            EnterGameState(GameState.MainMenu);
                        }
                    }
                    break;

                case GameState.MainMenu:
                    {
                        playSound.Play();
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            
                            EnterGameState(GameState.Playing);
                            iniciaMusica = true;

                        }
                    }
                    break;

                case GameState.Playing:
                    {
                        timeCounter = 0;
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

                case GameState.Null:
                    {
                        spriteBatch.Draw(texGameOver, new Vector2(1, 1), Color.White);
                    }
                    break;

                case GameState.Playing:
                    {
                        foreach (Entity e in entities)
                        {
                            e.Draw(gameTime);
                            if(e is Human)
                            {
                                spriteBatch.Draw(uiTex, new Vector2(50, 75), Color.White);
                                string aux = "" + hum.GetHealth();
                                spriteBatch.DrawString(uiFont, aux, new Vector2(130, 80), Color.White);
                            }
                        }
                    }
                    break;

                case GameState.MainMenu:
                    {
                        spriteBatch.Draw(texMainMenu, new Vector2(1, 1), Color.White);
                        
                        Game1.LimpaSala1();
                        Game1.LimpaSala2();
                        Game1.LimpaSala3();
                        Game1.DrawInimigosSala1();
                        Game1.DrawInimigosSala2();
                        Game1.DrawInimigosSala3();

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
            ExitGame.Content = Content;


            
            sndMenu = Content.Load<SoundEffect>("Sounds/Tela_Inicial");
            playSound = sndMenu.CreateInstance();
            playSound.IsLooped = true;
            



            //UI
            uiTex = Content.Load<Texture2D>("Sprites/UI");
            uiFont = Content.Load<SpriteFont>("Fonts/Fonts");

            //MainMenu
            texMainMenu = Content.Load<Texture2D>("Sprites/MainMenu");
            texGameOver = Content.Load<Texture2D>("Sprites/gameOver");

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

        public static void DrawHumano()
        {
            //Samus
            hum = new Human(new Vector2(1038, 335));
            //hum = new Human(new Vector2(4538, 335));
            entities.Add(hum);
            
        }

        public static void DrawInimigosSala1()
        {
            //Doors
            d1 = new Door(new Vector2(2430, 209));
            
            entities.Add(d1);
            


            //PowerUP
            pu1 = new PowerUp(new Vector2(465, 290));
            entities.Add(pu1);

            //Enemy2 sala 1
            e21 = new Enemy2(new Vector2(935, 80));
            e22 = new Enemy2(new Vector2(1132, 81));
            e23 = new Enemy2(new Vector2(2185, 400));
            entities.Add(e21);
            entities.Add(e22);
            entities.Add(e23);
            

            //Enemy1 sala 1
            e11 = new Enemy1(new Vector2(1490, 65));
            e12 = new Enemy1(new Vector2(1646, 125));
            entities.Add(e11);
            entities.Add(e12);
        }
        public static void DrawInimigosSala2()
        {
            d2 = new Door(new Vector2(2942, 209));
            entities.Add(d2);
            //enemy2 sala 2
            e24 = new Enemy2(new Vector2(2686, 175));
           // entities.Add(e24);
        }

        public static void DrawInimigosSala3()
        {
            //enemy2 sala 2
            e25 = new Enemy2(new Vector2(3156, 400));
            e26 = new Enemy2(new Vector2(3276, 400));
            e27 = new Enemy2(new Vector2(3898, 400));
            entities.Add(e25);
            entities.Add(e26);
            entities.Add(e27);

            e13 = new Enemy1(new Vector2(3215, 100));
            e14 = new Enemy1(new Vector2(3250, 125));
            e15 = new Enemy1(new Vector2(3692, 125));
            e16 = new Enemy1(new Vector2(4450, 125));
            entities.Add(e13);
            entities.Add(e14);
            entities.Add(e15);
            entities.Add(e16);

            d3 = new Door(new Vector2(4950, 368));
            entities.Add(d3);

            eg1 = new ExitGame(new Vector2(5108, 368));
            entities.Add(eg1);

        }

        public static void LimpaSala1()
        {
            entities.Remove(d1);
            entities.Remove(pu1);
            entities.Remove(e21);
            entities.Remove(e22);
            entities.Remove(e23);
            entities.Remove(e11);
            entities.Remove(e12);
            iniciaMusica = false;
        }

        public static void LimpaSala2()
        {
            entities.Remove(e24);
            entities.Remove(d2);
            iniciaMusica = false;
        }

        public static void LimpaSala3()
        {
            entities.Remove(e25);
            entities.Remove(e26);
            entities.Remove(e27);
            entities.Remove(e13);
            entities.Remove(e14);
            entities.Remove(e15);
            entities.Remove(eg1);
            entities.Remove(d3);
            iniciaMusica = false;
        }


    }
}
