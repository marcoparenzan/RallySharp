﻿using RallySharp.Models;
using System;
using System.Collections.Generic;

namespace RallySharp.Levels
{
    public abstract class Level
    {
        public Level()
        {
            sprites = new();
            // Ready
            Update = Ready;
        }

        public GameState GameState { get; set; }

        public abstract int Index { get; }

        MainSprite mainSprite;

        readonly List<Sprite> sprites;

        protected void Add<TSprite>(TSprite sprite)
            where TSprite: Sprite
        {
            sprite.Ready();
            sprites.Add(sprite);
            if (sprite is MainSprite)
            {
                mainSprite = (MainSprite)(object) sprite;
            }
        }

        public MainSprite MainSprite => mainSprite;

        public IEnumerable<Sprite> Sprites => sprites;

        public ContinuousTrigger Fire { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveLeft { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveRight { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveUp { get; } = new ContinuousTrigger();
        public ContinuousTrigger MoveDown { get; } = new ContinuousTrigger();

        private void Running()
        {
            // updating mainSprite
            if (Fire.Triggered())
            {
                mainSprite.NewDirection();
            }
            else if (MoveUp.Triggered())
            {
                mainSprite.NewDirection(0);
            }
            else if (MoveRight.Triggered())
            {
                mainSprite.NewDirection(1);
            }
            else if (MoveDown.Triggered())
            {
                mainSprite.NewDirection(2);
            }
            else if (MoveLeft.Triggered())
            {
                mainSprite.NewDirection(3);
            }
            else
            {
                mainSprite.NewDirection();
            }
            mainSprite.Update();

            // update all other enemies
            for (var i = 1; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();

                if (mainSprite.CollidesWith(sprite))
                {
                    if (sprite is EnemySprite || sprite is RockSprite)
                    {
                        GameState.Lives--;
                        if (GameState.Lives == 0)
                        {
                            GoToFinished();
                        }
                        else
                        {
                            GoToCrashed();
                        }
                        break;
                    }
                    else if (sprite is FlagSprite)
                    {
                        GameState.Score += GameState.FlagScore;
                        GameState.FlagScore += 100;
                        sprites.Remove(sprite);
                    }
                }
            }

            GameState.Fuel--;
            if (GameState.Fuel == 0)
            {
                GoToCrashed();
            }
        }

        private void GoToCrashed()
        {
            MainSprite.Crashed();
            Update = Crashed;
            foreach (var sprite1 in sprites)
            {
                sprite1.Crashed();
            }
        }

        private void GoToFinished()
        {
            MainSprite.Finished();
            Update = Finished;
            foreach (var sprite1 in sprites)
            {
                sprite1.Finished();
            }
        }

        private void GoToCompleted()
        {
            MainSprite.Completed();
            Update = Completed;
            foreach (var sprite1 in sprites)
            {
                sprite1.Completed();
            }
        }

        private void Ready()
        {
            if (Fire.Triggered())
            {
                Update = Running;
                foreach (var sprite in sprites)
                {
                    sprite.Running();
                }
            }

            mainSprite.Update();
            for (var i = 1; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Crashed()
        {
            mainSprite.Update();
            for (var i = 1; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Completed()
        {
            mainSprite.Update();
            for (var i = 1; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Finished()
        {
            mainSprite.Update();
            for (var i = 1; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        public Action Update { get; private set; }

        static Random rnd = new Random();

        protected void RandomPlace<TSprite>(int count)
            where TSprite : Sprite
        {
            var i = 0;
            while (i < count)
            {
                var x0 = rnd.Next(42);
                var y0 = rnd.Next(64);
                // exclusion condition outside 5,4 - 36-59
                if (x0 < 5) continue;
                if (x0 > 36) continue;
                if (y0 < 4) continue;
                if (y0 > 59) continue;
                // exclusion condition in the starting rectangle 13,49 28,59
                if (x0 >= 13 && x0 <= 28 && y0 >= 49 && y0 <= 59) continue;
                // exclusion if tile not empty
                var tileId = Tilemap.Data0[y0 * Tilemap.Width + x0];
                if (tileId != 2) continue;

                var sprite = Activator.CreateInstance<TSprite>();
                sprite.Pos = (x0 * 24, y0 * 24);
                Add(sprite);

                i++;
            }
        }

    }
}
