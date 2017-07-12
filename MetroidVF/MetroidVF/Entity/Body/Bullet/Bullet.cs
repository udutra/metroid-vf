using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    class Bullet : Body
    {

        private static ContentManager content;
        Texture2D texBullet;

        public float damage = -50f;

        public Vector2 dir = Vector2.Zero;

        public Character myShooter = null;

        float timeCounter = 1f;
        float bulletTime = 1.3f;

        public enum BulletState { comTempo, semTempo };
        BulletState currentBulletState = BulletState.comTempo;


        public Bullet(Character shooter, Vector2 initPos, Vector2 initDir) : base(initPos)
        {
            myShooter = shooter;
            dir = initDir;
            speed *= 2;
            size = new Vector2(32,32);
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public override Vector2 GetDir()
        {
            return dir;
        }

        public override Texture2D GetSprite()
        {
            texBullet = Content.Load<Texture2D>("Sprites/Bullet");
            return texBullet;
        }

        public override void CollisionDetected(Entity other)
        {
            if (other is Enemy2)
            {
                Enemy2 c = (Enemy2)other;
                c.SetHealth(damage);
                Game1.entities.Remove(this);
                if (c.GetHealth() <= 0)
                {
                    Game1.entities.Remove(other);
                }
            }

            if (other is Enemy1)
            {
                Enemy1 c = (Enemy1)other;
                c.SetHealth(damage);
                Game1.entities.Remove(this);
                if (c.GetHealth() <= 0)
                {
                    Game1.entities.Remove(other);

                }
                
            }

            if (other is Door)
            {
                Door d = (Door)other;
                d.SetHealth(damage);
                Game1.entities.Remove(this);
            }

            if (other is Map)
            {
                Game1.entities.Remove(this);
            }

        }

        public override bool IgnoreCollision(Entity other)
        {
            if (other == myShooter) //ignore collision against my shooter!
                return true;

            if (other is Bullet)
            { //ignore collision against other bullets!
                return true;
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentBulletState)
            {
                case BulletState.comTempo:
                    {
                        if (timeCounter <= bulletTime)
                        {
                            timeCounter += dt;
                        }
                        else
                        {
                            currentBulletState = BulletState.semTempo;
                        }
                    }
                    break;

                case BulletState.semTempo:
                    {
                        Game1.entities.Remove(this);
                    }
                    break;
            }
            base.Update(gameTime);
        }

        public override Vector2 GetSize()
        {
            return new Vector2(7, 10);
        }

    }
}
