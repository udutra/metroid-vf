using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MetroidVF
{
    public class Enemy1 : Character
    {
        SpriteSheet spriteSheet, explodeSheet;
        private static ContentManager content;
        Texture2D texEnemy, texExplode;
        Vector2 moveDir;
        public float health = 100f;
        float timeCounter = 0;
        float timeImune = 0;
        public bool explosion = false;
        public bool stoped = false;

        enum EnemyState { Null , Stop, Down, Idle, Explode }
        EnemyState currentEnState;

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

                case EnemyState.Stop:
                    {

                    }
                    break;

                case EnemyState.Idle:
                    {
                        spriteSheet.PlayAnim(0, 1, 5.0f);
                    }
                    break;

                case EnemyState.Down:
                    {
                        spriteSheet.PlayAnim(0, 1, 12f);
                    }
                    break;

                case EnemyState.Explode:
                    {
                        explosion = true;
                        explodeSheet.PlayAnim(0, 2, 10f);
                        timeCounter = 0;
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

                case EnemyState.Stop:
                    {

                    }
                    break;

                case EnemyState.Idle:
                    {
                        
                    }
                    break;

                case EnemyState.Down:
                    {
                        
                    }
                    break;

                case EnemyState.Explode:
                    {
                        timeCounter = 0;
                    }
                    break;
            }
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
                            EnterEnemyState(EnemyState.Down);
                        }
                    }
                    break;

                case EnemyState.Stop:
                    {
                        moveDir = Vector2.Zero;
                        if (timeCounter <= 2f)
                        {
                            timeCounter += dt;
                        }
                        else
                        {
                            EnterEnemyState(EnemyState.Explode);
                        }
                    }
                    break;

                case EnemyState.Idle:
                    {
                        if (HumanColission())
                        {    
                            EnterEnemyState(EnemyState.Down);
                        }
                    }
                    break;

                case EnemyState.Down:
                    {
                        moveDir = Vector2.Zero;
                        speed = 300;
                       
                        Human nearestBody = null;
                        float nearestDist = 9999.9f;

                        Human testBody = Game1.hum;

                        float testDist = Vector2.Distance(this.position, testBody.position) - (float)testBody.GetDiagonal() * 100;

                        if ((testDist <= GetDiagonal() * 100) && (testDist < nearestDist))
                        {
                            nearestDist = testDist;
                            nearestBody = testBody;
                        }

                        if (nearestBody != null)
                        {
                            moveDir += nearestBody.position - position;
                        }

                        if (health <= 50 && stoped == false)
                        {
                            stoped = true;
                            EnterEnemyState(EnemyState.Null);
                        }

                        if (IsOnFirmGround())
                        {
                            EnterEnemyState(EnemyState.Stop);
                        }
                    }
                    break;

                case EnemyState.Explode:
                    {
                        position.Y -= 0.5f;
                        if (timeCounter <= 0.3f)
                        {
                            timeCounter += dt;
                        }
                        else
                        {
                            Game1.entities.Remove(this);
                        }
                    }
                    break;
            }
        }

        public Enemy1(Vector2 initPos) : base(initPos)
        {
            speed /= 2f;
            texEnemy = Content.Load<Texture2D>("SpriteSheets/enemy1Sheet");
            texExplode = Content.Load<Texture2D>("SpriteSheets/enemy1Explode");
            spriteSheet = new SpriteSheet(texEnemy, 2, 1);
            explodeSheet = new SpriteSheet(texExplode, 3, 1);

            EnterEnemyState(EnemyState.Idle);
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
            if (explosion == true)
            {
                return explodeSheet.tex;
            }
            else
            {
                return spriteSheet.tex;
            }
        }

        public override Vector2 GetSize()
        {
            if (explosion == true)
            {
                return new Vector2(200, 128);
            }
            else
            {
                return new Vector2(32, 64); 
            }
        }

        public override Rectangle? GetSourceRectangle()
        {
            if (explosion == true)
            {
                return explodeSheet.GetSourceRectangle((int)explodeSheet.animFrame);
            }
            else
            {
                return spriteSheet.GetSourceRectangle((int)spriteSheet.animFrame);
            }
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

            // System.Console.WriteLine("position.X: " + position.X);
            // System.Console.WriteLine("RETORNO TETO: " + Game1.map.TestCollisionRect(min, max));
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
                    System.Console.WriteLine("RETORNO: " + Game1.hum.imune);
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
        }

        private void AffectWithGravity()
        {
            //Gravity
            moveDir += Vector2.UnitX * 10f;
            moveDir -= Vector2.UnitX * 10f;
          //  moveDir += Vector2.UnitY * 1f;
        }

        public override void Update(GameTime gameTime)
        {
            spriteSheet.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            explodeSheet.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            UpdateEnemyState(gameTime);
            AffectWithGravity();
            base.Update(gameTime);
        }

        public override Vector2 GetMin()
        {
            return position - size / 2;
        }
        public override Vector2 GetMax()
        {
            return position + size / 2;
        }

        public bool HumanColission()
        {
            Vector2 min = Game1.hum.GetMin();
            Vector2 max = Game1.hum.GetMax();


            return TestCollisionHuman(min, max);
        }

        public bool TestCollisionHuman(Vector2 testMin, Vector2 testMax)
        {
            Vector2 myMin = new Vector2(position.X - size.X / 2f - 100, position.Y + size.Y / 2f - 4);
            Vector2 myMax = new Vector2(position.X + size.X / 2f + 100, position.Y + size.Y / 2f + 300);

            //test collision between my rectangle and other's rectangle
            if ((testMax.X >= myMin.X) && (testMax.Y >= myMin.Y) &&
                (testMin.X <= myMax.X) && (testMin.Y <= myMax.Y))
                return true;
            else
                return false;
        }

        public void SetHealth(float f)
        {
            health += f;
        }

        public float GetHealth()
        {
            return health;
        }


    }
}
