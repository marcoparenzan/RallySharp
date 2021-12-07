﻿using RallySharp.Models;
using System;
using System.Collections.Generic;


namespace RallySharp.Levels
{
    public class Level0
    {
        public Level0()
        {
            mainSprite = new MainSprite { Pos = (480, 1272) };
            sprites = new();
            sprites.Add(mainSprite);
            sprites.Add(new Sprite { Pos = (480, 1272 + 24 * 4), Animation = new(12) });
            //sprites.Add(new Sprite { Pos = (480 - 48, 1272 + 24 * 4), Animation = new(12) });
            //sprites.Add(new Sprite { Pos = (480 + 48, 1272 + 24 * 4), Animation = new(12) });
            //sprites.Add(new Sprite { Pos = (480 - 96, 1272 + 24 * 4), Animation = new(12) });
            //sprites.Add(new Sprite { Pos = (480 + 96, 1272 + 24 * 4), Animation = new(12) });
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

            foreach (var sprite in sprites)
            {
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
            foreach (var sprite in sprites)
            {
                sprite.Update();
            }
        }

        public Action Update { get; private set; }
    }
}
