using RallySharp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class EnemySprite: Sprite
    {
        public Sprite MainSprite { get; set; }

        byte[][] tentative_sets = new byte[][]
        {
            new byte[] { 1,2,0,3 },
            new byte[] { 2,1,3,0 },
            new byte[] { 1,0,2,3 },
            new byte[] { 0,1,3,2 },
            new byte[] { 3,0,2,1 },
            new byte[] { 0,3,1,2 },
            new byte[] { 3,2,0,1 },
            new byte[] { 2,3,1,0 }
        };

        protected override void UpdateRunning()
        {
            int? newDirection;
            var tentative_set_idx = 0;

            var offsetMain = MainSprite.Pos - this.Pos;
            if (offsetMain.X >= 0)
            {
                if (offsetMain.Y >= 0)
                {
                    tentative_set_idx = (offsetMain.X > offsetMain.Y) ? 0 : 1;
                }
                else
                {
                    tentative_set_idx = (offsetMain.X > -offsetMain.Y) ? 2 :3;
                }
            }
            else
            {
                if (offsetMain.Y >= 0)
                {
                    tentative_set_idx = (offsetMain.X > -offsetMain.Y) ? 4 : 5;
                }
                else
                {
                    tentative_set_idx = (offsetMain.X < offsetMain.Y) ? 6 : 7;
                }
            }

            var rotation = 0;

            while (true)
            {
                var xt = 0;
                var yt = 0;

                newDirection = tentative_sets[tentative_set_idx][rotation];

                var nextPos = Pos + Speed[newDirection.Value];
                if (newDirection == 0)
                {
                    yt = (int)((nextPos.Y) / Resources.TileHeight);
                    xt = (int)((nextPos.X) / Resources.TileWidth);
                }
                else if (newDirection == 1)
                {
                    yt = (int)((nextPos.Y) / Resources.TileHeight);
                    xt = (int)((nextPos.X + Resources.TileWidth - 1) / Resources.TileWidth);
                }
                else if (newDirection == 2)
                {
                    yt = (int)((nextPos.Y + Resources.TileHeight - 1) / Resources.TileHeight);
                    xt = (int)((nextPos.X) / Resources.TileWidth);
                }
                else if (newDirection == 3)
                {
                    yt = (int)((nextPos.Y) / Resources.TileHeight);
                    xt = (int)((nextPos.X) / Resources.TileWidth);
                }

                var offset = (int)(yt * Resources.Width + xt);
                var tileId = Resources.Tiles[0][offset];

                if (tileId != 2)
                {
                    rotation++; // and loop!
                }
                else
                {
                    if (newDirection == Direction)
                    {
                        Animation.Update();
                    }
                    else
                    {
                        Animation.NewDirection(newDirection.Value, (rotation == 2) ? -1 : 1);
                    }
                    Pos = nextPos;
                    Direction = newDirection.Value;
                    newDirection = null; // reset so does not loop
                    break;
                }
            }
        }
    }
}
