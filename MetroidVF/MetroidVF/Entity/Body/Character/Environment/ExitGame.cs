using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    public class ExitGame : Character
    {
        Texture2D texExit;
        SpriteSheet exitSheet;

        private static ContentManager content;

        public ExitGame(Vector2 initPos) : base(initPos)
        {
            texExit = Content.Load<Texture2D>("SpriteSheet/exitSheet");

            exitSheet = new SpriteSheet(texExit, 2, 1);
            exitSheet.PlayAnim(0, 2, 30f);
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public override Texture2D GetSprite()
        {
            return exitSheet.tex;
        }

        public override Rectangle? GetSourceRectangle()
        {
            return exitSheet.GetSourceRectangle((int)exitSheet.animFrame);
        }

        public override Vector2 GetSize()
        {
            return new Vector2(32, 32);
        }

        public override void Update(GameTime gameTime)
        {
            exitSheet.UpdateAnim((float)gameTime.ElapsedGameTime.TotalSeconds);
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
