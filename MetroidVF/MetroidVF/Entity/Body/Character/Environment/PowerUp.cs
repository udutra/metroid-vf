using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class PowerUp : Character
    {
        Texture2D texPowerUp;
        SpriteSheet powerUpSheet;
        private static ContentManager content;

        public PowerUp(Vector2 initPos) : base(initPos)
        {
            texPowerUp = Content.Load<Texture2D>("SpriteSheets/powerUpSheet");

            powerUpSheet = new SpriteSheet(texPowerUp, 2, 1);

            powerUpSheet.PlayAnim(0, 2, 30f);
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public override Texture2D GetSprite()
        {
            return powerUpSheet.tex;
        }

        public override Rectangle? GetSourceRectangle()
        {
            return powerUpSheet.GetSourceRectangle((int)powerUpSheet.animFrame);
        }

        public override Vector2 GetSize()
        {
            return new Vector2(32, 32);
        }

        public override void Update(GameTime gameTime)
        {
            powerUpSheet.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        public override bool IgnoreCollision(Entity other)
        {
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
                    Game1.entities.Remove(this);
                }

        }        
    }
}
