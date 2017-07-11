using Microsoft.Xna.Framework;

namespace MetroidVF
{
    public class Character : Body
    {
        public static float health = 100f;

        public float fireRate = 10f; //hz

        float fireTimer = 0;

        Vector2 shootDir = new Vector2(1, 0);

        public Character(Vector2 initPos) : base(initPos) { }

        public virtual bool WantsToFire() { return false; }

        public virtual void SetHealth(float f)
        {
            health += f;
        }

        public virtual float GetHealth()
        {
            return health;
        }

        public override void Update(GameTime gameTime)
        {
            /*float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            Vector2 dir = GetDir();
            if (dir.Length() > 0)
                shootDir = dir;

            if (fireTimer <= 0)
            {
                fireTimer = 1.0f / fireRate;

                if (WantsToFire())
                {
             //       Game1.entities.Add(new Bullet(this, position, shootDir));
                }
            }
            else
            {
                fireTimer -= dt;
            }

          //  if (health <= 0.0f)
            //    Game1.entities.Remove(this);*/

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            Vector2 dir = GetDir();
            if (dir.Length() > 0)
            {
                if (Game1.bulletDir == true)
                {
                    shootDir = new Vector2(1, 0);
                }
                if (Game1.bulletDir == false)
                {
                    shootDir = new Vector2(-1, 0);
                }
                if (Game1.BulletUP == true)
                {
                    shootDir = new Vector2(0, -1);
                    Game1.BulletUP = false;
                }

            }

            if (fireTimer <= 0)
            {
                fireTimer = 1f / fireRate;

                if (WantsToFire())
                {
                    Vector2 novoPos = Vector2.Zero;
                    if (shootDir.X == 1)
                    {
                        novoPos = new Vector2(position.X + 16f, position.Y - 6f);
                    }
                    if (shootDir.X == -1)
                    {
                        novoPos = new Vector2(position.X - 16f, position.Y - 6f);
                    }
                    if (shootDir.Y == -1)
                    {
                        novoPos = new Vector2(position.X + 4f, position.Y - 16f);
                    }
                    Game1.entities.Add(new Bullet(this, position, shootDir));

                }
            }
            else
            {
                fireTimer -= dt;
            }
        } 
    }
}
