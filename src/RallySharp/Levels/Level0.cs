using RallySharp.Models;
using System;
using System.Collections.Generic;


namespace RallySharp.Levels
{
    public class Level0
    {
        int carSpeedMag = 6;

        public Level0()
        {
            MainCar = new Sprite { Type = 0, Pos = (480 + 12, 1272 + 12) };
        }

        public Sprite MainCar { get; private set; }

        private List<Sprite> enemies = new List<Sprite>();

        public IEnumerable<Sprite> Enemies => enemies;

        public void Fire()
        {
            if (State == LevelState.Ready)
            {
                Running();
                MoveUp();
            }
        }

        public void MoveDown()
        {
            if (State != LevelState.Running) return;
            MainCar.Speed = Direction.Down * carSpeedMag;
            MainCar.Type = 6;
        }

        public void MoveUp()
        {
            if (State != LevelState.Running) return;
            MainCar.Speed = Direction.Up * carSpeedMag;
            MainCar.Type = 0;
        }

        public void MoveLeft()
        {
            if (State != LevelState.Running) return;
            MainCar.Speed = Direction.Left * carSpeedMag;
            MainCar.Type = 9;
        }

        public void MoveRight()
        {
            if (State != LevelState.Running) return;
            MainCar.Speed = Direction.Right * carSpeedMag;
            MainCar.Type = 3;
        }

        public void Update()
        {
            switch (State)
            {
                case LevelState.Running:

                    // main update
                    var project_pos = MainCar.Pos + (MainCar.Speed.x, -MainCar.Speed.y);
                    var collision = CollisionAt(project_pos, MainCar.Speed);
                    if (collision != Collision.None)
                    {
                        // TileCrash
                        if (MainCar.Speed.x > 0) MoveDown();
                        else if (MainCar.Speed.y < 0) MoveLeft();
                        else if (MainCar.Speed.x < 0) MoveUp();
                        else if (MainCar.Speed.y > 0) MoveRight();
                    }
                    else
                    {
                        MainCar.Pos = project_pos;
                    }

                    foreach (var sprite in enemies)
                    {
                        sprite.Pos += (sprite.Speed.x, -sprite.Speed.y);
                    }

                    break;
                case LevelState.Ready:
                    break;
            }
        }

        public Collision CollisionAt(Vec pos, Vec Speed)
        {
            int yt;
            int xt;
            
            if (Speed.x > 0)
            {
                yt = (int)((pos.y) / Resources.TileHeight);
                xt = (int)((pos.x + Resources.TileWidth / 2) / Resources.TileWidth);
                var xm = (pos.x + Resources.TileWidth / 2) % Resources.TileWidth;
                if (xm < 1) return Collision.None;
                if (CollisionAt(xt, yt)) return Collision.Left;
            }
            else if (Speed.x < 0)
            {
                yt = (int)((pos.y) / Resources.TileHeight);
                xt = (int)((pos.x - Resources.TileWidth / 2) / Resources.TileWidth);
                var xm = (pos.x - Resources.TileWidth / 2) % Resources.TileWidth;
                if (xm < 1) return Collision.None;
                if (CollisionAt(xt, yt)) return Collision.Right;
            }
            else if (Speed.y > 0)
            {
                yt = (int)((pos.y - Resources.TileHeight / 2) / Resources.TileHeight);
                xt = (int)((pos.x) / Resources.TileWidth);
                var ym = (pos.y + Resources.TileHeight / 2) % Resources.TileHeight;
                if (ym < 1) return Collision.None;
                if (CollisionAt(xt, yt)) return Collision.Bottom;
            }
            else if (Speed.y < 0)
            {
                yt = (int)((pos.y + Resources.TileHeight / 2) / Resources.TileHeight);
                xt = (int)((pos.x) / Resources.TileWidth);
                var ym = (pos.y + Resources.TileHeight / 2) % Resources.TileHeight;
                if (ym < 1) return Collision.None;
                if (CollisionAt(xt, yt)) return Collision.Top;
            }

            return Collision.None;
        }

        private bool CollisionAt(int xt, int yt)
        {
            if (yt < 0) return false;
            if (yt >= Resources.Height) return false;
            if (xt < 0) return false;
            if (xt >= Resources.Width) return false;

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
