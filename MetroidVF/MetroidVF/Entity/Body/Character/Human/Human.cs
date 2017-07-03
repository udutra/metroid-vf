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
        Texture2D texSamusStart;
        KeyboardState KeyState;
        float camZoom = 1.0f;
        float timeCounter = 0f;
        float jumpTime = 0.8f;
        Vector2 bkpPosition;
        GameTime gameTimeLeave;
        SpriteSheet spriteSheet;
        float animFrame = 0f;
        float animSpeed = 0f;
        int frameStart;
        int frameEnd;
        int animTotalFrames;
        public bool lookingRight;       

        enum PlayerState { Null, Start, Idle, walkingRight, walkingLeft, Jumping, jumpingRight, jumpingLeft, Falling };
        PlayerState currPlayerState = PlayerState.Null;
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
                        PlayAnim(0, 3, 0.7f);                                                
                    }
                    break;

                case PlayerState.Idle:
                    {
                        speed = 200;
                        //Animation Idle Based on Loking
                        if (lookingRight == true)
                        {
                            //Animation Idle
                            PlayAnim(4, 4, 1f);
                        }
                        else
                        {
                            PlayAnim(23, 23, 1f);
                        }
                    }
                    break;

                case PlayerState.walkingRight:
                    {
                        speed = 200;
                        lookingRight = true;
                        //Animation Right
                        PlayAnim(6, 8, 12f);                        
                    }
                    break;

                case PlayerState.walkingLeft:
                    {
                        speed = 200;
                        lookingRight = false;
                        //Animation Left
                        PlayAnim(19, 21, 12f);
                    }
                    break;

                case PlayerState.Jumping:
                    {
                        oldJumpState = PlayerState.Jumping;
                        timeCounter = 0f;
                        bkpPosition = position;

                        //Animation Jumping Direction
                        if (lookingRight == true)
                        {
                            PlayAnim(9, 9, 12f);
                        }
                        else
                        {
                            PlayAnim(18, 18, 1f);
                        }
                    }
                    break;

                case PlayerState.jumpingRight:
                    {
                        oldJumpState = PlayerState.jumpingRight;
                        timeCounter = 0f;
                        bkpPosition = position;

                        //Animation Jumping Right
                        PlayAnim(10, 13, 15f);
                    }
                    break;

                case PlayerState.jumpingLeft:
                    {
                        oldJumpState = PlayerState.jumpingLeft;
                        timeCounter = 0f;
                        bkpPosition = position;

                        //Animation Jumping Left
                        PlayAnim(14, 17, 12f);
                    }
                    break;

                case PlayerState.Falling:
                    {
                        //Animation Based on Looking
                        if (oldJumpState == PlayerState.Jumping)
                        {
                            if (lookingRight == true)
                            {   
                                PlayAnim(9, 9, 1f);
                            }
                            else
                            {
                                PlayAnim(18, 18, 1f);
                            }
                        }

                        if (oldJumpState == PlayerState.jumpingRight || oldJumpState == PlayerState.jumpingLeft)
                        {
                            if (lookingRight == true)
                            {
                                //Animation Idle
                                PlayAnim(10, 13, 15f);
                            }
                            else
                            {
                                PlayAnim(14, 17, 12f);
                            }
                        }
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
                       if (timeCounter <= 4f)
                       {                           
                           timeCounter += dt;
                       }
                       else
                       {
                            PlayAnim(3, 3, 1f);
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
                    }
                    break;

                case PlayerState.walkingRight:
                    {
                        moveDir = Vector2.Zero;

                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            moveDir += Vector2.UnitX;
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

                        if (KeyState.IsKeyDown(Keys.Left))
                        {
                            moveDir -= Vector2.UnitX;
                        }

                        if (KeyState.IsKeyDown(Keys.Right))
                        {
                            EnterPlayerState(PlayerState.walkingRight);
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
                        if (timeCounter <= jumpTime)
                        {                            
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }

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
                        if (timeCounter <= jumpTime)
                        {                            
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }

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
                        if (timeCounter <= jumpTime)
                        {
                            if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                            if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }

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

                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX / 2.5f; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX / 2.5f; }

                        if(speed >= 350)
                        {
                            speed = 250;
                        }
                        
                        if(IsOnFirmGround())
                        {
                            EnterPlayerState(PlayerState.Idle);
                        }
                    }
                    break;
            }
        }       

        public void PlayAnim(int frameStart, int frameEnd, float animSpeed)
        {
            animFrame = (float)frameStart;
            this.frameStart = frameStart;
            this.frameEnd = frameEnd;
            this.animSpeed = animSpeed;

            animTotalFrames = frameEnd - frameStart + 1;
        }

        public void UpdateAnim(float dt)
        {
            animFrame -= frameStart;

            animFrame += dt * animSpeed;

            animFrame = animFrame % animTotalFrames;
            if (animFrame < 0f)
                animFrame += animTotalFrames;

            animFrame += frameStart;
        }

        public Human(Vector2 initPos) : base(initPos)
        {
            texSamusStart = Content.Load<Texture2D>("SpriteSheets/Sheet");
            spriteSheet = new SpriteSheet(texSamusStart, 14, 2);

            EnterPlayerState(PlayerState.Start);
            

        }

        public override Vector2 GetDir() { return moveDir; }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        
      

        public override Texture2D GetSprite()
        {
            return spriteSheet.tex;
        }

        public override Vector2 GetSize()
        {
            return new Vector2(32, 76);
        }

        public override Rectangle? GetSourceRectangle()
        {
            return spriteSheet.GetSourceRectangle((int)animFrame);
        }

        private void AffectWithGravity()
        {
            //Gravity
            moveDir += Vector2.UnitY * .5f;
        }   

        public bool IsOnFirmGround()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y + size.Y / 2f + 4);
            Vector2 max = new Vector2(position.X + size.X / 2f + 4, position.Y + size.Y / 2f + 4);
           // System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRoof()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y - size.Y / 2f + 4 );
            Vector2 max = new Vector2(position.X + size.X / 2f, position.Y - size.Y / 2f - 4);

             //System.Console.WriteLine("RETORNO: " + Game1.map.TestCollisionRect(min, max));
            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("size.X / 2f: " + ((size.X / 2f)-4));
            // System.Console.WriteLine("position.Y: " + position.Y);
            // System.Console.WriteLine("size.Y / 2f: " + ((size.Y / 2f)-4));
            //
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
            UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            UpdatePlayerState(gameTime);
            
            
           
            gameTimeLeave = gameTime;
            AffectWithGravity();

            //Falling without jump
            if(IsOnFirmGround() == false )
            {
                if(currPlayerState == PlayerState.walkingRight || currPlayerState == PlayerState.walkingLeft)
                {
                    EnterPlayerState(PlayerState.Falling);
                }
            }              


            base.Update(gameTime);
            Game1.camera.camPos = position;
            Game1.camera.camZoom = camZoom;
        }
    }
}
