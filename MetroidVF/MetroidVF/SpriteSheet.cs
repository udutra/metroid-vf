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

        public float animFrame = 0f;
        public float animSpeed = 0f;
        public int frameStart;
        public int frameEnd;
        public int animTotalFrames;

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

        public void PlayAnim(int frameStart, int frameEnd, float animSpeed)
        {
            animFrame = (float)frameStart;
            this.frameStart = frameStart;
            this.frameEnd = frameEnd;
            this.animSpeed = animSpeed;

            animTotalFrames = frameEnd - frameStart + 1;
        }

        public void UpdateAnim(float dt)
        {
            animFrame -= frameStart;

            animFrame += dt * animSpeed;

            animFrame = animFrame % animTotalFrames;
            if (animFrame < 0f)
                animFrame += animTotalFrames;

            animFrame += frameStart;
        }
    }
}
