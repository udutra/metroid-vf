using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class Enemy2 : Character
    {
        private static ContentManager content;
        Texture2D texEnemy1;
        Vector2 moveDir;
        enum EnemyState { Stop, Move, Explode }
        EnemyState currentEnState = EnemyState.Stop;

        void EnterEnemyState(EnemyState newState)
        {
            LeaveEnemyState();

            currentEnState = newState;

            switch (currentEnState)
            {
                case EnemyState.Stop:
                    {

                    }
                    break;

                case EnemyState.Move:
                    {

                    }
                    break;

                case EnemyState.Explode:
                    {

                    }
                    break;
            }
        }

        void LeaveEnemyState()
        {
            switch (currentEnState)
            {
                case EnemyState.Stop:
                    {

                    }
                    break;

                case EnemyState.Move:
                    {

                    }
                    break;

                case EnemyState.Explode:
                    {

                    }
                    break;
            }
        }

        void UpdateEnemyState(GameTime gameTime)
        {
            switch (currentEnState)
            {
                case EnemyState.Stop:
                    {
                        if (testeHuman())
                        {
                            currentEnState = EnemyState.Move;
                        }
                    }
                    break;

                case EnemyState.Move:
                    {
                        rotation += 1.57f;
                    }
                    break;

                case EnemyState.Explode:
                    {

                    }
                    break;
            }
        }

        public Enemy2(Vector2 initPos) : base(initPos)
        {
            speed /= 2f;
            size = new Vector2(32, 32);
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

        public bool testeHuman()
        {
            foreach (Entity e in Game1.entities)
            {
                if (e is Human)
                {
                    Vector2 min = new Vector2((position.X - size.X / 2), position.Y + size.Y / 2f);
                    
                    Vector2 max = new Vector2((position.X - size.X / 2) * 5, (position.Y + size.Y / 2f) * 2);
                    return e.TestCollisionRect(min, max);
                }
            }          

            return false;
        }

        public override bool TestCollisionRect(Vector2 testMin, Vector2 testMax)
        {

            Vector2 myMin = new Vector2((position.X - size.X / 2) / 2, position.Y + size.Y / 2f);
            Vector2 myMax = new Vector2((position.X + size.X / 2) * 2, (position.Y + size.Y / 2f) *1);

            if ((testMax.X >= myMin.X) && (testMax.Y >= myMin.Y) &&
               (testMin.X <= myMax.X) && (testMin.Y <= myMax.Y))
                return true;

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateEnemyState(gameTime);


            base.Update(gameTime);
        }
    }
}
