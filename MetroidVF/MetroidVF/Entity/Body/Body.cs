using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetroidVF
{
    public class Body : Entity
    {
        public Vector2 position;
        public Vector2 size;
        public float speed = 100f;

        public Body(Vector2 initPos)
        {
            position = initPos;              
        }

        public Vector2 GetMin() { return position - size / 2; }
        public Vector2 GetMax() { return position + size / 2; }

        public virtual Vector2 GetDir() { return Vector2.Zero; }

        public virtual Texture2D GetSprite() { return null; }

        public override bool TestCollisionRect(Vector2 testMin, Vector2 testMax)
        {
            Vector2 myMin = GetMin();
            Vector2 myMax = GetMax();

            //test collision between my rectangle and other's rectangle
            if ((testMax.X >= myMin.X) && (testMax.Y >= myMin.Y) &&
                (testMin.X <= myMax.X) && (testMin.Y <= myMax.Y))
                return true;
            else
                return false;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 dir = GetDir();

            float s = dir.Length();
            if (s > 0)
                dir = dir / s;

            //simulate physics independently in two axis
            for (int axis = 0; axis < 2; axis++)
            {
                Vector2 bkpPos = position; //save current position

                if (axis == 0)
                    position += new Vector2(dir.X, 0f) * dt * speed; //only X
                else
                    position += new Vector2(0f, dir.Y) * dt * speed; //only Y

                Entity collider = null;

                Vector2 myMin = GetMin();
                Vector2 myMax = GetMax();

                //test collision against all world entities
                foreach (Entity e in Game1.entities)
                {
                    if ((e != this) && //not myself?
                        (IgnoreCollision(e) == false) && //ignore collision with other?
                        (e.IgnoreCollision(this) == false) && //other ignores collision with me?
                        e.TestCollisionRect(myMin, myMax)) //is colliding against other entity?
                    {
                        collider = e; //collision detected!
                        CollisionDetected(e);
                        break;
                    }
                }

                if (collider != null) //undo movement!
                    position = bkpPos;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D texture = GetSprite();
            if (texture == null)
                return;

            //Texture Real Size
            size = new Vector2(texture.Width - 2, texture.Height - 2);
            
             Game1.spriteBatch.Draw(texture,
             Game1.camera.ProjectPos(position),
             null,
             Color.White,
             0.0f,
             new Vector2(texture.Width, texture.Height) / 2f, 
             Game1.camera.ProjectScale(new Vector2(size.X / texture.Width, size.Y / texture.Height)), 
             SpriteEffects.None,
             0.0f
             );

            
        }
    }
}
