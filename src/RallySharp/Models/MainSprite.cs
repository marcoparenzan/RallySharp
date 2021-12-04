using RallySharp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{

    public class MainSprite : Sprite
    {
        int newDirection;

        public void NewDirection(int value = -1) => newDirection = value;

        protected override void Running()
        {
            var newSpeed = Speed; // auto or not // Direction.None;
            var newPos = Pos;
            var colliding = -1;
            var rotation = 0;
            var xt = 0;
            var yt = 0;

            do
            {
                if ((newDirection == 0) || (newDirection == -1) && (newSpeed.y > 0))
                {
                    newDirection = 0;
                    newSpeed = Direction[newDirection];
                    newPos = Pos + (newSpeed.x, -newSpeed.y);
                    yt = (int)((newPos.y) / Resources.TileHeight);
                    xt = (int)((newPos.x) / Resources.TileWidth);
                    if (CollisionAt(xt, yt))
                    {
                        colliding = 0;
                        Collided = true;
                        newDirection = (newDirection + (rotation++)) % 4;
                        newSpeed = Direction[newDirection];
                    }
                    else
                    {
                        colliding = -1;
                    }
                }
                else if ((newDirection == 1) || (newDirection == -1) && (newSpeed.x > 0))
                {
                    newDirection = 1;
                    newSpeed = Direction[newDirection];
                    newPos = Pos + (newSpeed.x, -newSpeed.y);
                    yt = (int)((newPos.y) / Resources.TileHeight);
                    xt = (int)((newPos.x + Resources.TileWidth - 1) / Resources.TileWidth);
                    if (CollisionAt(xt, yt))
                    {
                        colliding = 1;
                        Collided = true;
                        newDirection = (newDirection + (rotation++)) % 4;
                        newSpeed = Direction[newDirection];
                    }
                    else
                    {
                        colliding = -1;
                    }
                }
                else if ((newDirection == 2) || (newDirection == -1) && (newSpeed.y < 0))
                {
                    newDirection = 2;
                    newSpeed = Direction[newDirection];
                    newPos = Pos + (newSpeed.x, -newSpeed.y);
                    yt = (int)((newPos.y + Resources.TileHeight - 1) / Resources.TileHeight);
                    xt = (int)((newPos.x) / Resources.TileWidth);
                    if (CollisionAt(xt, yt))
                    {
                        colliding = 2;
                        Collided = true;
                        newDirection = (newDirection + (rotation++)) % 4;
                        newSpeed = Direction[newDirection];
                    }
                    else
                    {
                        colliding = -1;
                    }
                }
                else if ((newDirection == 3) || (newDirection == -1) && (newSpeed.x < 0))
                {
                    newDirection = 3;
                    newSpeed = Direction[newDirection];
                    newPos = Pos + (newSpeed.x, -newSpeed.y);
                    yt = (int)((newPos.y) / Resources.TileHeight);
                    xt = (int)((newPos.x) / Resources.TileWidth);
                    if (CollisionAt(xt, yt))
                    {
                        colliding = 3;
                        Collided = true;
                        newDirection = (newDirection + (rotation++)) % 4;
                        newSpeed = Direction[newDirection];
                    }
                    else
                    {
                        colliding = -1;
                    }
                }
            } while (colliding != -1);

            // out...then update
            Collided = false;
            Pos = newPos;
            Speed = newSpeed;
            AnimationEnds = newDirection;

            newDirection = -1; // reset so does not loop
        }

        private bool CollisionAt(int xt, int yt)
        {
            var offset = (int)(yt * Resources.Width + xt);
            var tileId = Resources.Tiles[0][offset];

            if (tileId == 2) return false;
            return true;
        }
    }
}
