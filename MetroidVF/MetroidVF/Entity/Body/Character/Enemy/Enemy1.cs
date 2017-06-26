using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class Enemy1 : Character
    {
        private static ContentManager content;
        Texture2D texEnemy1;        
        Vector2 moveDir;
        enum EnemyState { Right, Up, Down, Left, Stop }
        EnemyState currentEnState = EnemyState.Right;
        public bool rotationDown = false;

        void EnterEnemyState(EnemyState newState)
        {
            LeaveEnemyState();

            currentEnState = newState;

            switch (currentEnState)
            {
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

                case EnemyState.Stop:
                    {
                    }
                    break;
            }
        }

        void UpdateEnemyState(GameTime gameTime)
        {
            switch (currentEnState)
            {
                case EnemyState.Right:
                    {
                        moveDir = Vector2.Zero;
                        if (IsOnFirmGround())
                        {
                            { moveDir += Vector2.UnitX; }                            
                        }
                        else
                        {                          
                            currentEnState = EnemyState.Down;
                        }                
                    }
                    break;

                case EnemyState.Left:
                    {
                   //   moveDir = Vector2.Zero;
                   //  moveDir -= Vector2.UnitX;
                   //  if (IsOnRoof())
                   //  {
                   //      moveDir -= Vector2.UnitX;
                   //  }
                   //  else
                   //  {
                   //      currentEnState = EnemyState.Up;
                   //  }
                    }
                    break;

                case EnemyState.Up:
                    {                       

                    }
                    break;

                case EnemyState.Down:
                    {
                        moveDir = Vector2.Zero;
                        moveDir += Vector2.UnitY;
                        if (IsOnLeft())
                        {
                            moveDir += Vector2.UnitY;
                        }
                        else
                        {
                            currentEnState = EnemyState.Left;
                        }
                    }
                    break;

                case EnemyState.Stop:
                    {                       
                    }
                    break;
            }
        }

        public Enemy1(Vector2 initPos) : base(initPos)
        {
            speed /= 2f;
            size = new Vector2(32,32);
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
            texEnemy1 = Content.Load<Texture2D>("Sprites/enemy1");
            return texEnemy1;
        }

        public bool IsOnFirmGround()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y + size.Y / 2f + 1);
            Vector2 max = new Vector2(position.X + size.X / 2f, position.Y + size.Y / 2f + 1);
            System.Console.WriteLine("RETORNO CHAO: " + Game1.map.TestCollisionRect(min, max));

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRoof()
        {
            Vector2 min = new Vector2(position.X - size.X / 2f, position.Y - size.Y / 2f - 1);
            Vector2 max = new Vector2(position.X + size.X / 2f, position.Y - size.Y / 2f - 1);
            System.Console.WriteLine("RETORNO TETO: " + Game1.map.TestCollisionRect(min, max));

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnRight()
        {
            Vector2 max = new Vector2(position.X + size.X / 2f + 1, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X + size.X / 2f + 1, position.Y - size.Y / 2f);
            System.Console.WriteLine("RETORNO DIREITA: " + Game1.map.TestCollisionRect(min, max));

            return Game1.map.TestCollisionRect(min, max);
        }

        public bool IsOnLeft()
        {
            Vector2 max = new Vector2(position.X - size.X / 2f - 4, position.Y + size.Y / 2f);
            Vector2 min = new Vector2(position.X - size.X / 2f - 4, position.Y - size.Y / 2f);
            System.Console.WriteLine("RETORNO ESQUERDA: " + Game1.map.TestCollisionRect(min, max));

            return Game1.map.TestCollisionRect(min, max);
        }

        private void AffectWithGravity()
        {
            //Gravity
            moveDir += Vector2.UnitY * .5f;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateEnemyState(gameTime);

            //AffectWithGravity();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
         //   //size = new Vector2(texEnemy1.Width - 2, texEnemy1.Height - 2);
         //   if (rotationDown) 
         //   {
         //       Game1.spriteBatch.Draw(texEnemy1, 
         //       Game1.camera.ProjectPos(position),
         //       null,
         //       Color.White,
         //       90f,
         //       new Vector2(texEnemy1.Width, texEnemy1.Height) / 2f,
         //       Game1.camera.ProjectScale(new Vector2(size.X / texEnemy1.Width, size.Y / texEnemy1.Height)),
         //       SpriteEffects.None,
         //       0.0f
         //       );
            //}

          base.Draw(gameTime);
        }
    }
}
