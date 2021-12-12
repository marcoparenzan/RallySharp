using RallySharp.Models;
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
            for (var i = 1; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        public Action Update { get; private set; }
    }
}

