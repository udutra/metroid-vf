using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MetroidVF
{
    public class Body : Entity
    {
        public Vector2 position = new Vector2(320, 240);
        public Vector2 size;
        public float speed = 200f;
        public float rotation = 0;

        public Body(Vector2 initPos)
        {
            position = initPos;
        }      

        public virtual Vector2 GetMin() { return position - size / 2; }
        public virtual Vector2 GetMax() { return position + size / 2; }

        public virtual Vector2 GetDir()
        {
            return Vector2.Zero;
        }

        public virtual Texture2D GetSprite()
        {
            return null;
        }

        public virtual Vector2 GetSize()
        {
            return Vector2.Zero;
        }

        public virtual Rectangle? GetSourceRectangle()
        {
            return null;
        }

        public bool TestPoint(Vector2 testPos)
        {
            return (testPos.X > (position.X - size.X / 2f)) &&
                   (testPos.Y > (position.Y - size.Y / 2f)) &&
                   (testPos.X < (position.X + size.X / 2f)) &&
                   (testPos.Y < (position.Y + size.Y / 2f));
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
            Texture2D sprite = GetSprite();
            if (sprite == null)
                return;

            size  = GetSize();
            if (size == null)
                return;
            // size = new Vector2(sprite.Width, sprite.Height);
            // size = new Vector2(32 -4,32 - 4);

            

            int spriteWidth = sprite.Width;
            int spriteHeight = sprite.Height;

            Rectangle? sourceRectangle = GetSourceRectangle();
            if (sourceRectangle != null)
            {
                spriteWidth = ((Rectangle)sourceRectangle).Width;
                spriteHeight = ((Rectangle)sourceRectangle).Height;
            }

              Game1.spriteBatch.Draw(sprite,
              Game1.camera.ProjectPos(position),
              sourceRectangle,
              Color.White,
              rotation,
              new Vector2(spriteWidth,
                          spriteHeight) / 2f, //pivot
              Game1.camera.ProjectScale(
                new Vector2(size.X / spriteWidth,
                            size.Y / spriteHeight)), //scale
              SpriteEffects.None,
              0.0f
            );
        }

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
        public double GetDiagonal()
        {
            float b, a;
            double d;
            b = size.X / 2;
            a = size.Y / 2;
            d = Math.Sqrt((b * b) + (a * a));
            return d;
        }

    }
}
