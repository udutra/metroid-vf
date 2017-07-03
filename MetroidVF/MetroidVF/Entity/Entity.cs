using Microsoft.Xna.Framework;

namespace MetroidVF
{
    public class Entity
    {
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        //test collision againt a rectangle
        public virtual bool TestCollisionRect(Vector2 testMin, Vector2 testMax)
        {
            return false;
        }

        //test if must ignore collision against other entity
        public virtual bool IgnoreCollision(Entity other)
        {
            return false;
        }

        //called when a collision has occurred
        public virtual void CollisionDetected(Entity other)
        {
        }
    }
}
