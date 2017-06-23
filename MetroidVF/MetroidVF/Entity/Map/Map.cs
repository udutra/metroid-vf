
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace MetroidVF
{
    public class Map : Entity
    {
        int mapWidth;
        int mapHeight;

        int[,] tiles;

        Texture2D tileset;

        int tileWidth;
        int tileHeight;

        public Map(string filenameTMX, string filenameTex)
        {
            XmlDocument tmx = new XmlDocument();

            tmx.Load(filenameTMX);

            XmlNode map = tmx.SelectSingleNode("map");

            tileWidth = int.Parse(map.Attributes["tilewidth"].Value);
            tileHeight = int.Parse(map.Attributes["tileheight"].Value);

            XmlNodeList layers = tmx.SelectNodes("map/layer");

            foreach (XmlNode layer in layers)
            {
                mapWidth = int.Parse(layer.Attributes["width"].Value);
                mapHeight = int.Parse(layer.Attributes["height"].Value);

                tiles = new int[mapWidth, mapHeight];

                string data = layer.SelectSingleNode("data").InnerText.Trim();

                string[] cells = data.Split(',');

                for (int i = 0, y = 0; y < mapHeight; y++)
                {
                    for (int x = 0; x < mapWidth; x++, i++)
                    {
                        tiles[x, y] = int.Parse(cells[i]);
                    }
                }

                break;
            }

            tileset = Game1.instance.Content.Load<Texture2D>(filenameTex);
        }

        public override void Draw(GameTime gameTime)
        {

           int columns = tileset.Width / tileWidth;

           for (int y = 0; y < mapHeight; y++)
           {
              for (int x = 0; x < mapWidth; x++)
              {
                 int tile = tiles[x, y];
                 if (tile == 0)
                     continue;
             
                 tile--; //make tile index to be zero-base!
             
                 Vector2 pos = new Vector2(x * tileWidth, y * tileHeight);
             
                 Rectangle sourceRect;
             
                 sourceRect.X = (tile % columns) * tileWidth;
                 sourceRect.Y = (tile / columns) * tileHeight;
                 sourceRect.Width = tileWidth;
                 sourceRect.Height = tileHeight;
             
                 Game1.spriteBatch.Draw(tileset, Game1.camera.ProjectPos(pos), sourceRect,
                 Color.White, 0.0f, Vector2.Zero, Game1.camera.ProjectScale(Vector2.One), SpriteEffects.None, 1.0f);
             
              }
           }
        }

        public override bool TestCollisionRect(Vector2 testMin, Vector2 testMax)
        {
          for (int y = 0; y < mapHeight; y++)
          {
            for (int x = 0; x < mapWidth; x++)
            {
               int tile = tiles[x, y];
               if (tile == 0)
                   continue;
          
               Vector2 myMin = new Vector2(x * tileWidth, y * tileHeight);
               Vector2 myMax = myMin + new Vector2(tileWidth, tileHeight);
          
               if ((testMax.X >= myMin.X) && (testMax.Y >= myMin.Y) &&
                   (testMin.X <= myMax.X) && (testMin.Y <= myMax.Y))
                   return true;
            }
          }  
          
          return false;
        }

        
    }
}
