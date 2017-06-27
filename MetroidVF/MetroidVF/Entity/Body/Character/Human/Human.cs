using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class Human : Character
    {        
        private static ContentManager content;        
        Vector2 moveDir;
        Texture2D texSamus;
        KeyboardState KeyState;
        float camZoom = 2.0f;
        float timeCounter = 0f;
        float jumpTime = 2f;
        Vector2 bkpPosition;
        GameTime gameTimeLeave;

        enum PlayerState { Null, Walking, Jumping, Falling };
        PlayerState currPlayerState = PlayerState.Walking;

        void EnterPlayerState(PlayerState newState)
        {
            LeavePlayerState();

            currPlayerState = newState;

            switch (currPlayerState)
            {
                case PlayerState.Walking:
                    {
                        
                    }
                    break;

                case PlayerState.Jumping:
                    {
                    }
                    break;
            }
        }

        void LeavePlayerState()
        {
            switch (currPlayerState)
            {
                case PlayerState.Walking:
                    {
                        
                    }
                    break;

                case PlayerState.Jumping:
                    {                      
                        
                    }
                    break;
            }
        }

        void UpdatePlayerState(GameTime gameTime)
        {            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currPlayerState)
            {                
                case PlayerState.Walking:
                    {
                        speed = 100;
                        moveDir = Vector2.Zero;

                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }
                        if (KeyState.IsKeyDown(Keys.Up) && IsOnFirmGround()) { currPlayerState = PlayerState.Jumping; timeCounter = 0f; bkpPosition = position; }
                        if (IsOnLeft()) { return; }
                        if (IsOnRight()) { return; }

                    }
                    break;

                case PlayerState.Jumping:
                    {                        
                        if (timeCounter <= jumpTime)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }

                            speed += speed * dt / 1.5f;
                            moveDir.Y += -2.0f;
                            timeCounter += dt;

                            if (position.Y + texSamus.Width <= bkpPosition.Y - (texSamus.Width * 1.2f))
                            {
                                speed -= speed * dt * 1.5f;                                
                            }

                            if (position.Y + texSamus.Width <= bkpPosition.Y - (texSamus.Width * 3.3f) || IsOnRoof())
                            {
                                speed = 100;
                                speed += speed * dt ;
                                currPlayerState = PlayerState.Falling;
                            }                            
                        }
                        else
                        {                            
                            currPlayerState = PlayerState.Falling;
                        }                       
                    }
                    break;

                case PlayerState.Falling:
                    {
                        moveDir = Vector2.Zero;
                        speed += speed * dt / 1.5f;

                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX / 2.5f; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX / 2.5f; }
                        
                        if(IsOnFirmGround())
                        {
                            currPlayerState = PlayerState.Walking;
                        }

                    }
                    break;
            }
        }

        public override Vector2 GetDir() { return moveDir; }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        
        public Human(Vector2 initPos) : base(initPos)
        {

        }

        public override Texture2D GetSprite()
        {
            texSamus = Content.Load<Texture2D>("Sprites/samus");
            return texSamus;
        }
                
        private void AffectWithGravity()
        {
            //Gravity
            moveDir += Vector2.UnitY * .5f;
        }

        public bool IsOnFirmGround()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y + size.Y / 2f + 4);
            Vector2 max = new Vector2(position.X + size.X / 2f, position.Y + size.Y / 2f + 4);

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRoof()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y - size.Y / 2f - 4);
            Vector2 max = new Vector2(position.X + size.X / 2f, position.Y - size.Y / 2f - 4);

            // System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));
            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("size.X / 2f: " + ((size.X / 2f)-4));
            // System.Console.WriteLine("position.Y: " + position.Y);
            // System.Console.WriteLine("size.Y / 2f: " + ((size.Y / 2f)-4));
            //
            return Game1.map.TestCollisionRect(min, max);

        }

        public bool IsOnRight()
        {
            Vector2 max = new Vector2(position.X + size.X / 2f + 4, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X + size.X / 2f - 4, position.Y - size.Y / 2f);

          // System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));
          // System.Console.WriteLine("position.X: " + position.X);
          // System.Console.WriteLine("position.Y: " + position.Y);
          // System.Console.WriteLine("sposition.Y - size.Y / 2f - 4: " + (position.Y - size.Y / 2f));
          // System.Console.WriteLine("size.Y / 2f + 4: " + (size.Y / 2f));

            return Game1.map.TestCollisionRect(min, max);

        }

        public bool IsOnLeft()
        {
            Vector2 max = new Vector2(position.X - size.X / 2f - 4, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X - size.X / 2f - 4, position.Y - size.Y / 2f);

            //System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));
            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("position.Y: " + position.Y);
            // System.Console.WriteLine("sposition.Y - size.Y / 2f - 4: " + (position.Y - size.Y / 2f));
            // System.Console.WriteLine("size.Y / 2f + 4: " + (size.Y / 2f));

            return Game1.map.TestCollisionRect(min, max);

        }

        public override void Update(GameTime gameTime)
        {
            KeyState = Keyboard.GetState();
            UpdatePlayerState(gameTime);
            gameTimeLeave = gameTime;
            AffectWithGravity();

            //Falling without jump
          // if(IsOnFirmGround() == false )
          // {
          //     if(currPlayerState == PlayerState.Walking)
          //     {
          //         currPlayerState = PlayerState.Falling;
          //     }
          // }               


            base.Update(gameTime);
            Game1.camera.camPos = position;
            Game1.camera.camZoom = camZoom;
        }
    }
}
