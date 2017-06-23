using Microsoft.Xna.Framework;

namespace MetroidVF
{
    public class Camera : Entity
    {
        public Vector2 camPos = Vector2.Zero;
        public float camZoom = 2.0f;

        Vector2 camOffset;

        public Camera()
        {
            camOffset = new Vector2(Game1.graphics.PreferredBackBufferWidth / 2f, Game1.graphics.PreferredBackBufferHeight / 1.15f);
        }

        public Vector2 ProjectPos(Vector2 pos)
        {
            Vector2 ve = new Vector2(camOffset.X + (pos.X - camPos.X) * camZoom, pos.Y * camZoom);
            return ve;
        }

        public Vector2 ProjectScale(Vector2 scale)
        {
            return scale * camZoom;
        }
    }
}
