using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetroidVF
{
    class SpriteSheet
    {
        public Texture2D tex;

        int columns;
        int lines;
        int spriteWidth;
        int spriteHeight;

        public SpriteSheet(Texture2D tex, int columns, int lines)
        {
            this.tex = tex;

            this.columns = columns;
            this.lines = lines;

            spriteWidth = tex.Width / columns;
            spriteHeight = tex.Height / lines;
        }

        public Rectangle GetSourceRectangle(int frame)
        {
            int x = (frame % this.columns) * this.spriteWidth;
            int y = (frame / this.columns) * this.spriteHeight;

            return new Rectangle(x, y, this.spriteWidth, this.spriteHeight);
        }
    }
}
