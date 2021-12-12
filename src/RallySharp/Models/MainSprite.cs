using RallySharp.Levels;
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
                    yt = (int)((nextPos.Y) / Tilesheet.Height);
                    xt = (int)((nextPos.X) / Tilesheet.Width);
                }
                else if (newDirection == 1)
                {
                    yt = (int)((nextPos.Y) / Tilesheet.Height);
                    xt = (int)((nextPos.X + Tilesheet.Width - 1) / Tilesheet.Width);
                }
                else if (newDirection == 2)
                {
                    yt = (int)((nextPos.Y + Tilesheet.Height - 1) / Tilesheet.Height);
                    xt = (int)((nextPos.X) / Tilesheet.Width);
                }
                else if (newDirection == 3)
                {
                    yt = (int)((nextPos.Y) / Tilesheet.Height);
                    xt = (int)((nextPos.X) / Tilesheet.Width);
                }

                var offset = (int)(yt * Tilemap.Width + xt);
                var tileId = Tilemap.Data[0][offset];

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
