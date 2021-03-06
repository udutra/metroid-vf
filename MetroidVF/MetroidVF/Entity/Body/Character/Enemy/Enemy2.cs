﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class Enemy2 : Character
    {      
        SpriteSheet spriteSheet;
        private static ContentManager content;
        float animFrame = 0f;
        float animSpeed = 0f;
        int frameStart;
        int frameEnd;
        int animTotalFrames;
        Texture2D teste;
        Vector2 moveDir;
        public float health = 100f;
        public float timeImune;
        public bool stoped = false;
        bool andandoPorta, ehPorta;

        enum EnemyState { Null, Right, Up, Down, Left, Stop, UpDoor, DownDoor }
        EnemyState currentEnState = EnemyState.Stop;
        EnemyState oldEnState = EnemyState.Null;

        void EnterEnemyState(EnemyState newState)
        {
            LeaveEnemyState();

            currentEnState = newState;

            switch (currentEnState)
            {
                case EnemyState.Null:
                    {
                       
                    }
                    break;

                case EnemyState.Right:
                    {

                    }
                    break;

                case EnemyState.Left:
                    {

                    }
                    break;

                case EnemyState.Up:
                    {

                    }
                    break;

                case EnemyState.Down:
                    {

                    }
                    break;
                case EnemyState.UpDoor:
                    {

                    }
                    break;

                case EnemyState.DownDoor:
                    {

                    }
                    break;

                case EnemyState.Stop:
                    {
                    }
                    break;
            }
        }

        void LeaveEnemyState()
        {
            switch (currentEnState)
            {
                case EnemyState.Null:
                    {
                        timeImune = 0;
                    }
                    break;

                case EnemyState.Right:
                    {
                        
                    }
                    break;

                case EnemyState.Left:
                    {
                       
                    }
                    break;

                case EnemyState.Up:
                    {
                        
                    }
                    break;

                case EnemyState.Down:
                    {

                    }
                    break;
                case EnemyState.UpDoor:
                    {

                    }
                    break;

                case EnemyState.DownDoor:
                    {

                    }
                    break;

                case EnemyState.Stop:
                    {
                        
                    }
                    break;
            }
        }
        public void SetHealth(float f)
        {
            health += f;
        }

        public float GetHealth()
        {
            return health;
        }

        void UpdateEnemyState(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (currentEnState)
            {
                case EnemyState.Null:
                    {
                        moveDir = Vector2.Zero;
                        
                        if (timeImune <= 1f)
                        {
                            timeImune += dt;
                        }
                        else
                        {
                            //currentEnState = oldEnState;
                            
                            EnterEnemyState(oldEnState);
                        }
                    }
                    break;

                case EnemyState.Right:
                    {
                        moveDir = Vector2.Zero;

                        

                        if (IsOnFirmGround() && IsOnRight())
                        {
                            rotation -= 1.57f;
                            currentEnState = EnemyState.Up;
                        }

                        if (IsOnFirmGround())
                        {
                            moveDir += Vector2.UnitX;
                        }
                        else
                        {
                            rotation += 1.57f;
                            currentEnState = EnemyState.Down;
                        }
                    }
                    break;

                case EnemyState.Left:
                    {
                        moveDir = Vector2.Zero;

                        

                        if (IsOnRoof() && IsOnLeft())
                        {
                            rotation -= 1.57f;
                            currentEnState = EnemyState.Down;
                        }

                        if (IsOnRoof())
                        {
                            moveDir -= Vector2.UnitX;
                        }
                        else
                        {
                            rotation += 1.57f;
                            currentEnState = EnemyState.Up;
                        }
                    }
                    break;

                case EnemyState.Up:
                    {
                        moveDir = Vector2.Zero;

                        

                        /*if (andandoPorta)
                        {
                            moveDir -= Vector2.UnitY;
                        }
                        else
                        {*/
                            if (IsOnRight() && IsOnRoof())
                            {
                                rotation -= 1.57f;
                                currentEnState = EnemyState.Left;
                            }
                            if (IsOnRight())
                            {

                                moveDir -= Vector2.UnitY;
                            }

                            else
                            {
                                rotation += 1.57f;
                                currentEnState = EnemyState.Right;
                            }
                        /*}
                        if (!ehPorta)
                        {
                            andandoPorta = false;
                        }*/
                    }
                    break;

                case EnemyState.Down:
                    {
                        moveDir = Vector2.Zero;

                        
                        
                            if (IsOnLeft() && IsOnFirmGround())
                            {
                                rotation -= 1.57f;
                                currentEnState = EnemyState.Right;
                            }

                            if (IsOnLeft())
                            {
                                moveDir += Vector2.UnitY;
                            }
                            else if (IsOnLeftDoor() && andandoPorta)
                            {
                                moveDir += Vector2.UnitY;
                            }
                            else
                            {
                                rotation += 1.57f;
                                currentEnState = EnemyState.Left;
                            }
                        }

                    
                    break;

                case EnemyState.Stop:
                    {
                      moveDir = Vector2.Zero;
                    // if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                    // if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }
                    // if (Keyboard.GetState().IsKeyDown(Keys.Down)) { moveDir += Vector2.UnitY; }
                    // if (Keyboard.GetState().IsKeyDown(Keys.Up)) { moveDir -= Vector2.UnitY; }


                        if (IsOnFirmGround())
                        {
                            currentEnState = EnemyState.Right;
                        }
                    }
                    break;

                case EnemyState.UpDoor:
                    {
                        moveDir -= Vector2.UnitY;
                        if (IsOnRoof())
                        {
                            rotation -= 1.57f;
                            currentEnState = EnemyState.Left;
                        }
                    }
                    break;

                case EnemyState.DownDoor:
                    {
                        
                        if (IsOnLeftDoor())
                        {
                            moveDir += Vector2.UnitY; 
                        }
                        if (IsOnLeftDoor() && IsOnFirmGround())
                        {
                            rotation -= 1.57f;
                            moveDir += Vector2.UnitX;
                            currentEnState = EnemyState.Right;
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

        public Enemy2(Vector2 initPos) : base(initPos)
     {
            speed /= 2f;
           
            teste = Content.Load<Texture2D>("SpriteSheets/enemy2Sheet");
            spriteSheet =
        new SpriteSheet(teste, 2, 1);

            PlayAnim(0, 1, 24.0f);
        }

        public override Vector2 GetDir()
        {
            return moveDir;
        }

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
            return new Vector2(28, 28);
        }

        public override Rectangle? GetSourceRectangle()
        {
            return spriteSheet.GetSourceRectangle((int)animFrame);
        }

        public bool IsOnFirmGround()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y + size.Y / 2f);
            Vector2 max = new Vector2(position.X + size.X / 2f + 4, position.Y + size.Y / 2f + 4);
         //  System.Console.WriteLine("RETORNO CHAO: " + Game1.map.TestCollisionRect(min, max));
         //  System.Console.WriteLine("position: " + position);


            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRoof()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f - 4, position.Y - size.Y / 2f - 4);
            Vector2 max = new Vector2(position.X + size.X / 2f, position.Y - size.Y / 2f);
            // System.Console.WriteLine("RETORNO TETO: " + Game1.map.TestCollisionRect(min, max));
            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("position.Y: " + position.Y);

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRight()
        {
            Vector2 max = new Vector2(position.X + size.X / 2f + 4, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X + size.X / 2f + 6, position.Y - size.Y / 2f - 4);
            //  System.Console.WriteLine("RETORNO DIREITA: " + Game1.map.TestCollisionRect(min, max));

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRightDoor()
        {
            Vector2 max = new Vector2(position.X + size.X / 2f + 4, position.Y - size.Y / 2f);
            Vector2 min = new Vector2(position.X + size.X / 2f, position.Y + size.Y / 2f - 4);
            //  System.Console.WriteLine("RETORNO DIREITA: " + Game1.map.TestCollisionRect(min, max));

            return TestCollisionRect(min, max);
        }

        public bool IsOnLeftDoor()
        {
            Vector2 max = new Vector2(position.X - size.X / 2f, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X - size.X / 2f -4, position.Y - size.Y / 2f-4);

            // System.Console.WriteLine("RETORNO ESQUERDO: " + Game1.map.TestCollisionRect(min, max));
            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("position.Y: " + position.Y);
            // System.Console.WriteLine("sposition.Y - size.Y / 2f - 4: " + (position.Y - size.Y / 2f));
            // System.Console.WriteLine("size.Y / 2f + 4: " + (size.Y / 2f));

            return TestCollisionRect(min, max);

        }

        public bool IsOnLeft()
        {
            Vector2 max = new Vector2(position.X - size.X / 2f, position.Y + size.Y / 2f + 4);
            Vector2 min = new Vector2(position.X - size.X / 2f - 4, position.Y - size.Y / 2f);

            // System.Console.WriteLine("RETORNO ESQUERDO: " + Game1.map.TestCollisionRect(min, max));
            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("position.Y: " + position.Y);
            // System.Console.WriteLine("sposition.Y - size.Y / 2f - 4: " + (position.Y - size.Y / 2f));
            // System.Console.WriteLine("size.Y / 2f + 4: " + (size.Y / 2f));

            return Game1.map.TestCollisionRect(min, max);

        }

        public override bool IgnoreCollision(Entity other)
        {
            if (other is Enemy1)
            {
                return true;
            }

            if (other is Enemy2)
            {
                return true;
            }

            return false;
        }

        

        public override void CollisionDetected(Entity other)
        {
            if (other is Human)
            {
                if (Game1.hum.imune == true)
                {
                    //System.Console.WriteLine("RETORNO: " + Game1.hum.imune);
                    return;
                }

                Human h = (Human)other;
                Game1.hum.SetHealth(-8);
                Game1.hum.imune = true;

                if (Game1.hum.GetHealth() <= 0)
                {
                    Game1.hum.playSong.Stop();
                    Game1.entities.Remove(other);
                    Game1.currGameState = Game1.GameState.Null;
                }
            }
            if(other is Door)
            {
                if(currentEnState == EnemyState.Right)
                {
                    rotation -= 1.57f;
                    currentEnState = EnemyState.UpDoor;
                }
                if (currentEnState == EnemyState.Left)
                {
                    rotation -= 1.57f;
                    currentEnState = EnemyState.DownDoor;
                }
            }

            /*if (other is Door)
            {
                ehPorta = true;
                if (IsOnLeftDoor())
                {
                    andandoPorta = true;
                    //currentEnState = EnemyState.Up;
                }
                else if (IsOnRightDoor())
                {
                    andandoPorta = true;
                    rotation -= 1.57f;
                    currentEnState = EnemyState.Up;
                }

                
            }
            else
            {
                ehPorta = false;
            
            }*/

        }

        public override void Update(GameTime gameTime)
        {
            UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            UpdateEnemyState(gameTime);

            if(health <= 50 && stoped == false)
            {
                stoped = true;
                oldEnState = currentEnState;
                EnterEnemyState(EnemyState.Null);
            }

            base.Update(gameTime);
        }
    }
}
