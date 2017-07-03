using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class Enemy1 : Character
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

        enum EnemyState { Right, Up, Down, Left, Stop }
        EnemyState currentEnState = EnemyState.Stop;

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

                        if (IsOnRight())
                        {
                            moveDir -= Vector2.UnitY;
                        }
                        else
                        {
                            rotation += 1.57f;
                            currentEnState = EnemyState.Right;
                        }
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
                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) { moveDir += Vector2.UnitX; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) { moveDir -= Vector2.UnitX; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Down)) { moveDir += Vector2.UnitY; }
                        if (Keyboard.GetState().IsKeyDown(Keys.Up)) { moveDir -= Vector2.UnitY; }


                        if (IsOnFirmGround())
                        {
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

        public Enemy1(Vector2 initPos) : base(initPos)
        {
            speed /= 2f;            
            teste = Content.Load<Texture2D>("SpriteSheets/enemy1Sheet");
            spriteSheet = new SpriteSheet(teste, 2, 1);

            PlayAnim(0, 1, 5.0f);
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
            return new Vector2(32, 64);
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

            if (other is Human)
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            UpdateEnemyState(gameTime);
            base.Update(gameTime);
        }
    }
}
