using RallySharp.Models;
using System.Collections.Generic;


namespace RallySharp.Levels
{
    public class Level0
    {
        int carSpeedMag = 6;

        public Level0()
        {
            //MainCar = new Sprite { Type = 0, Pos = (480 + 12, 1272 + 12) };
            MainCar = new Sprite { Type = 0, Pos = (480, 1272) };
        }

        public Sprite MainCar { get; private set; }

        private List<Sprite> enemies = new List<Sprite>();

        public IEnumerable<Sprite> Enemies => enemies;

        public ContinuousTrigger Fire { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveLeft { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveRight { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveUp { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveDown { get; } = new ContinuousTrigger();

        public void Update()
        {
            switch (State)
            {
                case LevelState.Running:

                    var newType = -1;
                    var newSpeed = MainCar.Speed; // auto or not // Direction.None;
                    var newPos = MainCar.Pos;
                    var colliding = Collision.None;
                    var rotation = 0;
                    var xt = 0;
                    var yt = 0;

                    if (Fire.Triggered())
                    {
                    }
                    else if (MoveUp.Triggered())
                    {
                        newType = 0;
                    }
                    else if (MoveRight.Triggered())
                    {
                        newType = 1;
                    }
                    else if (MoveDown.Triggered())
                    {
                        newType = 2;
                    }
                    else if (MoveLeft.Triggered())
                    {
                        newType = 3;
                    }

                    do
                    {
                        if ((newType == 0) || (newType == -1) && (newSpeed.y > 0))
                        {
                            newType = 0;
                            newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            newPos = MainCar.Pos + (newSpeed.x, -newSpeed.y);
                            yt = (int)((newPos.y) / Resources.TileHeight);
                            xt = (int)((newPos.x) / Resources.TileWidth);
                            if (CollisionAt(xt, yt))
                            {
                                colliding = Collision.Top;
                                MainCar.Collided = true;
                                newType = (newType + (rotation++)) % 4;
                                newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            }
                            else
                            {
                                colliding = Collision.None;
                            }
                        }
                        else if ((newType == 1) || (newType == -1) && (newSpeed.x > 0))
                        {
                            newType = 1;
                            newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            newPos = MainCar.Pos + (newSpeed.x, -newSpeed.y);
                            yt = (int)((newPos.y) / Resources.TileHeight);
                            xt = (int)((newPos.x + Resources.TileWidth - 1) / Resources.TileWidth);
                            if (CollisionAt(xt, yt))
                            {
                                colliding = Collision.Top;
                                MainCar.Collided = true;
                                newType = (newType+(rotation++))%4;
                                newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            }
                            else
                            {
                                colliding = Collision.None;
                            }
                        }
                        else if ((newType == 2) || (newType == -1) && (newSpeed.y < 0))
                        {
                            newType = 2;
                            newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            newPos = MainCar.Pos + (newSpeed.x, -newSpeed.y);
                            yt = (int)((newPos.y + Resources.TileHeight - 1) / Resources.TileHeight);
                            xt = (int)((newPos.x) / Resources.TileWidth);
                            if (CollisionAt(xt, yt))
                            {
                                colliding = Collision.Top;
                                MainCar.Collided = true;
                                newType = (newType + (rotation++)) % 4;
                                newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            }
                            else
                            {
                                colliding = Collision.None;
                            }
                        }
                        else if ((newType == 3) || (newType == -1) && (newSpeed.x < 0))
                        {
                            newType = 3;
                            newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            newPos = MainCar.Pos + (newSpeed.x, -newSpeed.y);
                            yt = (int)((newPos.y) / Resources.TileHeight);
                            xt = (int)((newPos.x) / Resources.TileWidth);
                            if (CollisionAt(xt, yt))
                            {
                                colliding = Collision.Top;
                                MainCar.Collided = true;
                                newType = (newType + (rotation++)) % 4;
                                newSpeed = Direction.Rotation[newType] * carSpeedMag;
                            }
                            else
                            {
                                colliding = Collision.None;
                            }
                        }
                    } while (colliding != Collision.None);

                    // out...then update
                    MainCar.Collided = false;
                    MainCar.Pos = newPos;
                    MainCar.Speed = newSpeed;
                    MainCar.Type = newType;

                    foreach (var sprite in enemies)
                    {
                        sprite.Pos += (sprite.Speed.x, -sprite.Speed.y);
                    }

                    break;
                case LevelState.Ready:
                    if (Fire.Triggered())
                    {
                        Running();
                        MainCar.Speed = Direction.Up * carSpeedMag;
                    }
                    break;
            }
        }

        private bool CollisionAt(int xt, int yt)
        {
            var offset = (int) (yt * Resources.Width + xt);
            var tileId = Resources.Tiles[0][offset];

            if (tileId == 2) return false;
            return true;
        }

        public LevelState State { get; private set; }

        public Level0 Ready()
        {
            State = LevelState.Ready;
            return this;
        }

        public Level0 Running()
        {
            State = LevelState.Running;
            return this;
        }
    }
}
