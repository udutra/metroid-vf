using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MetroidVF
{
    class UI : Character
    {
        private static ContentManager content;
        Texture2D uiTex;

        public UI(Vector2 initPos) : base(initPos)
        {
            uiTex = Content.Load<Texture2D>(("Sprites/UI"));
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public override Texture2D GetSprite()
        {
            uiTex = Content.Load<Texture2D>(("Sprites/UI"));
            return uiTex;
        }

        public override void Draw(GameTime gameTime)
        {
            
        }
    }
}
