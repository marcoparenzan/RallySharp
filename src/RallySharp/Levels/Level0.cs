using RallySharp.Models;
using System;
using System.Collections.Generic;

namespace RallySharp.Levels
{
    public class Level0
    {
        public Level0()
        {
            mainSprite = new MainSprite { Id=0, Pos = (480, 1272), Animation = new(0) };
            mainSprite.Ready();
            sprites = new();
            sprites.Add(mainSprite);
            var enemy = new EnemySprite { Id = 1, Pos = (480, 1272 + 24 * 4), Animation = new(12), MainSprite = mainSprite };
            enemy.Ready();
            sprites.Add(enemy);
            enemy = new EnemySprite { Id = 2, Pos = (480 - 48, 1272 + 24 * 4), Animation = new(12), MainSprite = mainSprite };
            enemy.Ready();
            sprites.Add(enemy);
            enemy = new EnemySprite { Id = 3, Pos = (480 + 48, 1272 + 24 * 4), Animation = new(12), MainSprite = mainSprite };
            enemy.Ready();
            sprites.Add(enemy);
            Update = Ready;
        }

        public int Index { get => 0; }

        readonly MainSprite mainSprite;

        readonly List<Sprite> sprites;

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
                mainSprite.Direction = 0;
            }

            mainSprite.Update();
            for (var i = 1; i<sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        public Action Update { get; private set; }
    }
}
