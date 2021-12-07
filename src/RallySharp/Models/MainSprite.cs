﻿using RallySharp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class MainSprite: Sprite
    {
        int? newDirection;

        public void NewDirection(int? value = null) => newDirection = value;

        protected override void UpdateRunning()
        {
            newDirection ??= Direction;
            var rotation = 0;

            while (true)
            {
                var xt = 0;
                var yt = 0;

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
                    newDirection = (newDirection + (++rotation)) % 4; // increased rotation to make 90/180/270
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
