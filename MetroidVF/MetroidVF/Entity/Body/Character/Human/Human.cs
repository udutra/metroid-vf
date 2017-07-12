using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace MetroidVF
{
    public class Human : Character
    {        
        private static ContentManager content;        
        Vector2 moveDir;
        Texture2D texSamusStart, texSamusBall;
        KeyboardState KeyState;
        float camZoom = 1.0f;
        float timeCounter = 0f;
        float jumpTime = 0.8f;
        Vector2 bkpPosition;
        GameTime gameTimeLeave;
        SpriteSheet spriteSheet, ballMove;     
        public bool lookingRight;
        public bool hasPowerUp = false;
        public bool isBall = false;
        public bool imune = false;
        public float health;
        float imuneCounter = 0f;
        SoundEffect sndStart, sndGame, sndJump, sndShoot;
        public SoundEffectInstance playSong;


        public enum PlayerState { Null, Start, Idle, walkingRight, walkingLeft, Jumping, jumpingRight, jumpingLeft, Falling, TurnBall, LookingUP, LookingRight, LookingLeft, Imune, RunRightShoot, RunLeftShoot };
        public PlayerState currPlayerState = PlayerState.Null;
        PlayerState oldJumpState = PlayerState.Null;



        void EnterPlayerState(PlayerState newState)
        {
            LeavePlayerState();

            currPlayerState = newState;

            switch (currPlayerState)
            {
                case PlayerState.Start:
                    {
                        //Animation Start
                        spriteSheet.PlayAnim(0, 3, 0.5f);
                        sndStart.Play();
                    }
                    break;

                case PlayerState.Idle:
                    {
                        speed = 200;
                        //Animation Idle Based on Loking
                        if (lookingRight == true)
                        {
                            //Animation Idle
                            spriteSheet.PlayAnim(4, 4, 1f);
                        }
                        else
                        {
                            spriteSheet.PlayAnim(39, 39, 1f);
                        }
                    }
                    break;

                case PlayerState.walkingRight:
                    {
                        speed = 200;
                        lookingRight = true;
                        //Animation Right
                        spriteSheet.PlayAnim(6, 8, 12f);
                        
                    }
                    break;

                case PlayerState.walkingLeft:
                    {
                        speed = 200;
                        lookingRight = false;
                        //Animation Left
                        spriteSheet.PlayAnim(35, 37, 12f);
                        
                    }
                    break;

                case PlayerState.Jumping:
                    {
                        sndJump.Play();
                        oldJumpState = PlayerState.Jumping;
                        timeCounter = 0f;
                        bkpPosition = position;

                        //Animation Jumping Direction
                        if (lookingRight == true)
                        {
                            spriteSheet.PlayAnim(9, 9, 12f);
                        }
                        else
                        {
                            spriteSheet.PlayAnim(34, 34, 1f);
                        }
                    }
                    break;

                case PlayerState.jumpingRight:
                    {
                        sndJump.Play();
                        oldJumpState = PlayerState.jumpingRight;
                        timeCounter = 0f;
                        bkpPosition = position;

                        //Animation Jumping Right
                        spriteSheet.PlayAnim(10, 13, 20f);
                    }
                    break;

                case PlayerState.jumpingLeft:
                    {
                        sndJump.Play();
                        oldJumpState = PlayerState.jumpingLeft;
                        timeCounter = 0f;
                        bkpPosition = position;

                        //Animation Jumping Left
                        spriteSheet.PlayAnim(30, 33, 20f);
                    }
                    break;

                case PlayerState.Falling:
                    {
                        //Animation Based on Looking
                        if (oldJumpState == PlayerState.Jumping)
                        {
                            if (lookingRight == true)
                            {
                                spriteSheet.PlayAnim(9, 9, 1f);
                            }
                            else
                            {
                                spriteSheet.PlayAnim(34, 34, 1f);
                            }
                        }

                        if (oldJumpState == PlayerState.jumpingRight || oldJumpState == PlayerState.jumpingLeft)
                        {
                            if (lookingRight == true)
                            {
                                //Animation Idle
                                spriteSheet.PlayAnim(10, 13, 15f);
                            }
                            else
                            {
                                spriteSheet.PlayAnim(30, 33, 20f);
                            }
                        }                       
                    }
                    break;

                case PlayerState.TurnBall:
                    {
                        isBall = true;
                        ballMove.PlayAnim(0, 3, 15f);
                    }
                    break;

                case PlayerState.LookingUP:
                    {
                        Game1.BulletUP = true;
                        if (lookingRight == true)
                        {
                            //Animation Idle
                            spriteSheet.PlayAnim(5, 5, 1f);
                        }
                        else
                        {
                            spriteSheet.PlayAnim(38, 38, 1f);
                        }
                    }
                    break;

                case PlayerState.LookingRight:
                    {
                        Game1.BulletUP = true;
                        spriteSheet.PlayAnim(18, 20, 12f);
                    }
                    break;

                case PlayerState.LookingLeft:
                    {
                        Game1.BulletUP = true;
                        spriteSheet.PlayAnim(23, 25, 12f);
                    }
                    break;

                case PlayerState.RunRightShoot:
                    {
                        spriteSheet.PlayAnim(14, 16, 12f);
                    }
                    break;

                case PlayerState.RunLeftShoot:
                    {
                        spriteSheet.PlayAnim(27, 29, 12f);
                    }
                    break;

                case PlayerState.Imune:
                    {
                        timeCounter = 0; 
                        spriteSheet.PlayAnim(3, 3, 1f);
                    }
                    break;
            }
        }

        void LeavePlayerState()
        {
            switch (currPlayerState)
            {
                case PlayerState.Start:
                    {
                        //Game1.DrawHumano();
                        //Game1.limpaSala1();
                        //Game1.DrawInimigosSala1();     
                        playSong.Play();                   
                    }
                    break;

                case PlayerState.Idle:
                    {
                        
                    }
                    break;

                case PlayerState.walkingRight:
                    {
                        
                    }
                    break;

                case PlayerState.walkingLeft:
                    {

                    }
                    break;

                case PlayerState.Jumping:
                    {

                    }
                    break;

                case PlayerState.Falling:
                    {
                        oldJumpState = PlayerState.Null ;
                    }
                    break;

                case PlayerState.TurnBall:
                    {
                        isBall = false;
                    }
                    break;

                case PlayerState.LookingUP:
                    {
                        Game1.BulletUP = false;
                    }
                    break;

                case PlayerState.LookingRight:
                    {
                        Game1.BulletUP = false;
                    }
                    break;

                case PlayerState.LookingLeft:
                    {
                        Game1.BulletUP = false;
                    }
                    break;

                case PlayerState.RunRightShoot:
                    {

                    }
                    break;

                case PlayerState.RunLeftShoot:
                    {

                    }
                    break;

                case PlayerState.Imune:
                    {
                        timeCounter = 0;
                    }
                    break;
            }
        }

        void UpdatePlayerState(GameTime gameTime)
        {            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyState = Keyboard.GetState();

            switch (currPlayerState)
            {
                case PlayerState.Start:
                    {                        
                       if (timeCounter <= 6.5f)
                       {                           
                           timeCounter += dt;
                       }
                       else
                       {
                            spriteSheet.PlayAnim(3, 3, 1f);
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) { EnterPlayerState(PlayerState.walkingRight); }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) { EnterPlayerState(PlayerState.walkingLeft); }
                            if (KeyState.IsKeyDown(Keys.Up) && IsOnFirmGround()) { EnterPlayerState(PlayerState.Jumping); }
                            
                        }
                    }
                    break;


                case PlayerState.Idle:
                    {  
                        moveDir = Vector2.Zero;
                        if (KeyState.IsKeyDown(Keys.Right)) { EnterPlayerState(PlayerState.walkingRight); }
                        if (KeyState.IsKeyDown(Keys.Left)) { EnterPlayerState(PlayerState.walkingLeft); }
                        if (KeyState.IsKeyDown(Keys.Space)) { EnterPlayerState(PlayerState.Jumping); }
                        if (KeyState.IsKeyDown(Keys.Up)) { EnterPlayerState(PlayerState.LookingUP); }
                        if (KeyState.IsKeyDown(Keys.Down))
                        {
                          if (hasPowerUp == true)
                          {
                              EnterPlayerState(PlayerState.TurnBall);
                          }
                        }
                    }
                    break;

                case PlayerState.walkingRight:
                    {
                        moveDir = Vector2.Zero;
                        Game1.bulletDir = true;
                        Game1.BulletUP = false;

                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            moveDir += Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Right) && KeyState.IsKeyDown(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.LookingRight);
                        }

                        if (KeyState.IsKeyDown(Keys.Right) && KeyState.IsKeyDown(Keys.LeftControl))
                        {
                            EnterPlayerState(PlayerState.RunRightShoot);
                        }

                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            EnterPlayerState(PlayerState.walkingLeft);
                        }                     

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.jumpingRight);
                        }

                        if (KeyState.IsKeyUp(Keys.Right))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }
                        //if (IsOnLeft()) { return; }
                        //if (IsOnRight()) { return; }
                        //if (IsOnRoof()) { return; }
                        //if (IsOnRight()) { return; }
                    }
                    break;

                case PlayerState.walkingLeft:
                    {
                        moveDir = Vector2.Zero;
                        Game1.BulletUP = false;
                        Game1.bulletDir = false;

                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            moveDir -= Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            EnterPlayerState(PlayerState.walkingRight);
                        }

                        if (KeyState.IsKeyDown(Keys.Left) && KeyState.IsKeyDown(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.LookingLeft);
                        }

                        if (KeyState.IsKeyDown(Keys.Left) && KeyState.IsKeyDown(Keys.LeftControl))
                        {
                            EnterPlayerState(PlayerState.RunLeftShoot);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.jumpingLeft);
                        }

                        if (KeyState.IsKeyUp(Keys.Left))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }
                        //if (IsOnLeft()) { return; }
                        //if (IsOnRight()) { return; }
                        //if (IsOnRoof()) { return; }
                        //if (IsOnRight()) { return; }
                    }
                    break;

                case PlayerState.Jumping:
                    {
                        
                        if (timeCounter <= jumpTime && !IsOnRoof())
                        {                            
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                                moveDir += Vector2.UnitX;
                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                                moveDir -= Vector2.UnitX;
                            }

                            if (KeyState.IsKeyUp(Keys.Space))
                            {
                                EnterPlayerState(PlayerState.Falling);
                            }

                            speed += speed * dt / 1.5f;
                            moveDir.Y += -2.0f;


                            if (position.Y + spriteSheet.tex.Width <= bkpPosition.Y - (spriteSheet.tex.Width * 0.6f))
                            {
                                speed -= speed * dt * 1.5f;                                
                            }

                            if (position.Y + spriteSheet.tex.Width <= bkpPosition.Y - (spriteSheet.tex.Width * 3.3f) || IsOnRoof())
                            {
                                speed = 200;
                                speed += speed * dt ;
                                EnterPlayerState(PlayerState.Falling);
                            }
                            timeCounter += dt;
                        }
                        else
                        {
                            speed = 200;
                            EnterPlayerState(PlayerState.Falling);
                        }                       
                    }
                    break;

                case PlayerState.jumpingRight:
                    {
                        
                        if (timeCounter <= jumpTime && !IsOnRoof())
                        {                            
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                                moveDir += Vector2.UnitX;
                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                                moveDir -= Vector2.UnitX;
                            }

                            speed += speed * dt / 1.5f;
                            moveDir.Y += -2.0f;
                            timeCounter += dt;

                            if (position.Y + spriteSheet.tex.Width <= bkpPosition.Y - (spriteSheet.tex.Width * 1.2f))
                            {
                                speed -= speed * dt * 1.5f;
                            }

                            if (position.Y + spriteSheet.tex.Width <= bkpPosition.Y - (spriteSheet.tex.Width * 3.3f) || IsOnRoof())
                            {
                                speed = 200;
                                speed += speed * dt;
                                EnterPlayerState(PlayerState.Falling);
                            }
                        }
                        else
                        {
                            speed = 200;
                            EnterPlayerState(PlayerState.Falling);
                        }
                    }
                    break;

                case PlayerState.jumpingLeft:
                    {
                                            
                        if (timeCounter <= jumpTime && !IsOnRoof())
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                                moveDir += Vector2.UnitX;
                                
                            }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                                moveDir -= Vector2.UnitX;
                                
                            }

                            speed += speed * dt / 1.5f;
                            moveDir.Y += -2.0f;
                            timeCounter += dt;
                            

                            if (position.Y + spriteSheet.tex.Width <= bkpPosition.Y - (spriteSheet.tex.Width * 1.2f))
                            {
                                speed -= speed * dt * 1.5f;
                            }

                            if (position.Y + spriteSheet.tex.Width <= bkpPosition.Y - (spriteSheet.tex.Width * 3.3f) || IsOnRoof())
                            {
                                speed = 200;
                                speed += speed * dt;
                                EnterPlayerState(PlayerState.Falling);
                            }
                        }
                        else
                        {
                            speed = 200;
                            EnterPlayerState(PlayerState.Falling);
                        }
                    }
                    break;

                case PlayerState.Falling:
                    {
                        moveDir = Vector2.Zero;
                        speed += speed * dt / 1.5f;

                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                            moveDir += Vector2.UnitX / 2.5f;

                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                            moveDir -= Vector2.UnitX / 2.5f;

                        }

                        if(speed >= 350)
                        {
                            speed = 250;
                        }

                        //System.Console.WriteLine(IsOnFirmGround());
                        if (IsOnFirmGround())
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }
                    }
                    break;

                case PlayerState.TurnBall:
                    {
                        moveDir = Vector2.Zero;
                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            lookingRight = true;
                            moveDir += Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            lookingRight = false;
                            moveDir -= Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Up))
                        {
                            position.Y -= 32f;
                            EnterPlayerState(PlayerState.Idle);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            position.Y -= 32f;
                            EnterPlayerState(PlayerState.Idle);
                        }
                    }
                    break;

                case PlayerState.LookingUP:
                    {
                        moveDir = Vector2.Zero;
                        if (KeyState.IsKeyDown(Keys.Right) && KeyState.IsKeyDown(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.LookingRight);
                        }
                        if (KeyState.IsKeyDown(Keys.Left) && KeyState.IsKeyDown(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.LookingLeft);
                        }

                        if (KeyState.IsKeyUp(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.Jumping);
                        }
                    }
                    break;

                case PlayerState.LookingRight:
                    {
                        moveDir = Vector2.Zero;
                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            lookingRight = true;
                            moveDir += Vector2.UnitX;
                        }
                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            lookingRight = false;
                            EnterPlayerState(PlayerState.LookingLeft);
                        }

                        if (KeyState.IsKeyUp(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }

                        if (KeyState.IsKeyUp(Keys.Right))
                        {
                            EnterPlayerState(PlayerState.LookingUP);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.jumpingRight);
                        }
                    }
                    break;

                case PlayerState.LookingLeft:
                    {
                        moveDir = Vector2.Zero;
                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            lookingRight = false;
                            moveDir -= Vector2.UnitX;
                        }
                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            lookingRight = true;
                            EnterPlayerState(PlayerState.LookingRight);
                        }

                        if (KeyState.IsKeyUp(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }

                        if (KeyState.IsKeyUp(Keys.Left))
                        {
                            EnterPlayerState(PlayerState.LookingUP);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.jumpingLeft);
                        }

                    }
                    break;

                case PlayerState.RunRightShoot:
                    {
                        moveDir = Vector2.Zero;
                        Game1.bulletDir = true;
                        Game1.BulletUP = false;

                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            moveDir += Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Right) && KeyState.IsKeyDown(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.LookingRight);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.jumpingRight);
                        }

                        if (KeyState.IsKeyUp(Keys.LeftControl))
                        {
                            EnterPlayerState(PlayerState.walkingRight);
                        }

                        if (KeyState.IsKeyUp(Keys.Right))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }
                    }
                    break;

                case PlayerState.RunLeftShoot:
                    {
                        moveDir = Vector2.Zero;
                        Game1.bulletDir = false;
                        Game1.BulletUP = false;

                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            moveDir -= Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Left) && KeyState.IsKeyDown(Keys.Up))
                        {
                            EnterPlayerState(PlayerState.LookingLeft);
                        }

                        if (KeyState.IsKeyDown(Keys.Space))
                        {
                            EnterPlayerState(PlayerState.jumpingLeft);
                        }

                        if (KeyState.IsKeyUp(Keys.LeftControl))
                        {
                            EnterPlayerState(PlayerState.walkingLeft);
                        }

                        if (KeyState.IsKeyUp(Keys.Left))
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }
                    }
                    break;

                case PlayerState.Imune:
                    {
                        if(timeCounter <= 3f)
                        {
                            position.Y -= 3f;
                            timeCounter += dt;
                        }
                        else
                        {
                            playSong.Stop();
                            Game1.currGameState = Game1.GameState.Null;
                        }
                    }
                    break;
            }
        }     

        public Human(Vector2 initPos) : base(initPos)
        {
            sndStart = Content.Load<SoundEffect>("Sounds/Animacao_Samus");
            sndGame = Content.Load<SoundEffect>("Sounds/Musica_Jogo");
            sndJump = Content.Load<SoundEffect>("Sounds/soundJump");
            sndShoot = Content.Load<SoundEffect>("Sounds/tiro");

            playSong = sndGame.CreateInstance();
            playSong.IsLooped = true;


            texSamusStart = Content.Load<Texture2D>("SpriteSheets/samusSheet");
            texSamusBall = Content.Load<Texture2D>("SpriteSheets/ballSheet");

            spriteSheet = new SpriteSheet(texSamusStart, 22, 2);
            ballMove = new SpriteSheet(texSamusBall, 4, 1);
            EnterPlayerState(PlayerState.Start);
            health = 30;
            Game1.bulletDir = true;
            speed = 230;

        }
                
        public override Vector2 GetDir() { return moveDir; }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public override Texture2D GetSprite()
        {
            if (isBall == true)
            {
                return ballMove.tex;
            }
            else
            {
                return spriteSheet.tex;
            }
        }

        public override Vector2 GetSize()
        {
            if (isBall == true)
            {
                return new Vector2(32, 32);
            }
            else
            {
                return new Vector2(32, 76);
            }            
        }

        public override Rectangle? GetSourceRectangle()
        {
            if (isBall == true)
            {
                return ballMove.GetSourceRectangle((int)ballMove.animFrame);
            }
            else
            {
                return spriteSheet.GetSourceRectangle((int)spriteSheet.animFrame);
            }
        }

        private void AffectWithGravity()
        {
            //Gravity
            moveDir += Vector2.UnitY * .5f;
        }   

        public bool IsOnFirmGround()
        {
            //Vector2 min = new Vector2(position.X - size.X / 2f, position.Y - 1);
            //Vector2 max = new Vector2(position.X + size.X / 2f, position.Y + 5);
            //Vector2 max = new Vector2(position.X, position.Y + 32);
            //Vector2 min = new Vector2(position.X - (size.Y / 2) + 36, position.Y);

            // System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));

            Vector2 min = new Vector2(position.X - size.X / 2f +14 , position.Y + size.Y / 2f - 4);
            Vector2 max = new Vector2(position.X + size.X / 2f -14, position.Y + size.Y / 2f + 4);
            //System.Console.WriteLine("position.y: " + position.Y);
            //System.Console.WriteLine("position.x: " + position.X);

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRoof()
        {
            Vector2 min = new Vector2(position.X, position.Y -32);
            
            Vector2 max = new Vector2(position.X + (size.Y / 2) -36, position.Y);
            

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRight()
        {
            Vector2 max = new Vector2(position.X + size.X / 2f + 8, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X + size.X / 2f - 8, position.Y - size.Y / 2f);

          // System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));
          // System.Console.WriteLine("position.X: " + position.X);
          // System.Console.WriteLine("position.Y: " + position.Y);
          // System.Console.WriteLine("sposition.Y - size.Y / 2f - 4: " + (position.Y - size.Y / 2f));
          // System.Console.WriteLine("size.Y / 2f + 4: " + (size.Y / 2f));

            return Game1.map.TestCollisionRect(min, max);

        }

        public bool IsOnLeft()
        {
            Vector2 max = new Vector2(position.X - size.X / 2f - 8, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X - size.X / 2f - 8, position.Y - size.Y / 2f);

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
            spriteSheet.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            ballMove.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdatePlayerState(gameTime);
           
            gameTimeLeave = gameTime;
            AffectWithGravity();

            //Falling without jump
            if(IsOnFirmGround() == false )
            {
                if(currPlayerState == PlayerState.walkingRight || currPlayerState == PlayerState.walkingLeft || currPlayerState == PlayerState.RunRightShoot || currPlayerState == PlayerState.RunLeftShoot)
                {
                    EnterPlayerState(PlayerState.Falling);
                }
            }
            if(imune == true)
            {
                color = Color.CornflowerBlue;
                if(imuneCounter <= 3f)
                {
                    imuneCounter += dt;
                }
                else
                {
                    imune = false;
                    color = Color.White;
                    imuneCounter = 0;
                }
            }
            
            base.Update(gameTime);
            Game1.camera.camPos = position;
            Game1.camera.camZoom = camZoom;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void CollisionDetected(Entity other)
        {
            if (other is Enemy1)
            {
                if (imune == true)
                {
                    return;
                }
                Game1.hum.SetHealth(-8);
                Game1.hum.imune = true;

                if (Game1.hum.GetHealth() <= 0)
                {
                    Game1.entities.Remove(Game1.hum);
                    //Game1.DrawHumano();
                    playSong.Stop();
                    Game1.currGameState = Game1.GameState.Null;
                    Game1.EnterGameState(Game1.currGameState);
                }
            }
            if(other is Enemy2)
            {
               if(imune == true)
                {
                    return;
                }
                Game1.hum.SetHealth(-8);
                Game1.hum.imune = true;

                if (Game1.hum.GetHealth() <= 0)
                {
                    Game1.entities.Remove(Game1.hum);
                    //Game1.DrawHumano();
                    playSong.Stop();
                    Game1.currGameState = Game1.GameState.Null;
                    Game1.EnterGameState(Game1.currGameState);
                }
            }

            if (other is PowerUp)
            {
                hasPowerUp = true;
                PowerUp p = (PowerUp)other;
                Game1.entities.Remove(p);
            }

            if (other is ExitGame)
            {
                ExitGame x = (ExitGame)other;
                Game1.entities.Remove(x);
                EnterPlayerState(PlayerState.Imune);

            }
        }

        public float GetHealth()
        {
            return health;
        }

        public void SetHealth(float f)
        {
            health += f;
        }
        
        public override bool WantsToFire()
        {
            if((currPlayerState == PlayerState.Start || currPlayerState == PlayerState.jumpingRight || currPlayerState == PlayerState.jumpingLeft || oldJumpState == PlayerState.jumpingRight || oldJumpState == PlayerState.jumpingLeft || isBall == true))
            {
                return false;
            }            
            return Keyboard.GetState().IsKeyDown(Keys.LeftControl);
        }


        public override Vector2 GetMin()
        {
            return new Vector2(position.X - (size.X / 2) + 13, position.Y - (size.Y / 2) + 10);
            //return position - size / 2 ;
        }
        public override Vector2 GetMax()
        {
            return new Vector2(position.X + (size.X / 2) - 13, position.Y + (size.Y / 2) - 4);
            //return position + size / 2;
        }

        


    }
}
