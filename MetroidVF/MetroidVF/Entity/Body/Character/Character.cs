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

        public void setHealth(float f)
        {
            health += f;
        }

        public float getHealth()
        {
            return health;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            //    Game1.entities.Remove(this);
        }   
        
    }
}
