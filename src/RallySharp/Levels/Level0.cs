using RallySharp.Models;
using System;
using System.Collections.Generic;


namespace RallySharp.Levels
{
    public class Level0
    {
        public Level0()
        {
            mainSprite = new MainSprite { CurrentAnimationFrame = 0, Pos = (480, 1272) };
            sprites.Add(mainSprite);
            Update = Ready;
        }

        private MainSprite mainSprite;

        private List<Sprite> sprites = new List<Sprite>();

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
                    sprite.GoToRunning();
                }
                mainSprite.Speed = Sprite.Direction[0];
            }
            foreach (var sprite in sprites)
            {
                sprite.Update();
            }
        }

        public Action Update { get; private set; }
    }
}
