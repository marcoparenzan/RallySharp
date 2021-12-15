using RallySharp.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RallySharp.Stages
{
    public class Stage
    {
        public Stage()
        {
            sprites = new();

            GameState = new();

            GoToStart();
        }

        public GameState GameState { get; set; }

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

        public OneShotTrigger Fire { get; } = new OneShotTrigger();
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
                        GoToCrashed();
                        break;
                    }
                    else if (sprite is FlagSprite)
                    {
                        GameState.Score += GameState.FlagScore;
                        GameState.FlagScore += 100;
                        sprites.Remove(sprite);

                        if (!sprites.Any(xx => xx is FlagSprite))
                        {
                            GoToCompleted();
                        }
                    }
                }
            }

            GameState.Fuel--;
            if (GameState.Fuel == 0)
            {
                GoToCrashed();
            }
        }

        public void GoToStart()
        {
            GameState.Level = 0;
            GameState.Score = 0;
            GameState.Lives = 3;
            GameState.Delay = 0;

            InitSprites();

            Add(new MainSprite { Pos = (480, 1272), Animation = new(0) });

            Update = Start;
            foreach (var sprite1 in sprites)
            {
                sprite1.Start();
            }
        }

        private void InitSprites()
        {
            sprites.Clear();
            mainSprite = default;
        }

        public void GoToReady()
        {
            InitSprites();

            Add(new MainSprite { Pos = (480, 1272), Animation = new(0) });
            Add(new EnemySprite { Pos = (480, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
            if (GameState.Level > 0) Add(new EnemySprite { Pos = (480 - 48, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
            if (GameState.Level > 1) Add(new EnemySprite { Pos = (480 + 48, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });

            AddRandom<FlagSprite>(GameState.Level + 1);
            AddRandom<RockSprite>(GameState.Level);

            GameState.Fuel = 512;
            GameState.Delay = 50;
            GameState.FlagScore = 100;

            Update = Ready;
            foreach (var sprite1 in sprites)
            {
                sprite1.Ready();
            }
        }

        public void GoToRunning()
        {
            GameState.Delay = 0;

            Update = Running;
            foreach (var sprite1 in sprites)
            {
                sprite1.Running();
            }
        }

        private void GoToCrashed()
        {
            GameState.Lives--;
            if (GameState.Lives == 0)
            {
                GoToEnded();
                return;
            }

            GameState.Delay = 50;

            Update = Crashed;
            foreach (var sprite1 in sprites)
            {
                sprite1.Crashed();
            }
        }

        private void GoToEnded()
        {
            Update = Ended;
            foreach (var sprite1 in sprites)
            {
                sprite1.Ended();
            }
        }

        private void GoToCompleted()
        {
            GameState.Delay = GameState.Fuel;

            Update = Completed;
            foreach (var sprite1 in sprites)
            {
                sprite1.Completed();
            }
        }

        private void Start()
        {
            if (Fire.Triggered())
            {
                GoToReady();
                return;
            }

            for (var i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Ready()
        {
            if (GameState.Delay > 0)
            {
                GameState.Delay--;
            }
            else
            {
                GoToRunning();
                return;
            }
            if (Fire.Triggered())
            {
                GoToRunning();
                return;
            }

            for (var i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Crashed()
        {
            if (GameState.Delay > 0)
            {
                GameState.Delay--;
            }
            else
            {
                GoToReady();
                return;
            }

            for (var i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Completed()
        {
            if (GameState.Delay > 0)
            {
                GameState.Delay -= 10;
                GameState.Score += 10;
                // missing lives and will update hud
            }
            else
            {
                GameState.Level++;
                GoToReady();
                return;
            }

            for (var i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        private void Ended()
        {
            if (GameState.Delay > 0)
            {
                GameState.Delay--;
            }
            else
            {
                GoToStart();
                return;
            }

            for (var i = 0; i < sprites.Count; i++)
            {
                var sprite = sprites[i];
                sprite.Update();
            }
        }

        [JsonIgnore]
        public Action Update { get; private set; }

        static Random rnd = new Random();

        protected void AddRandom<TSprite>(int count)
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
                var tileId = Tilemap.Data[y0 * Tilemap.Width + x0];
                if (tileId != 2) continue;

                var sprite = Activator.CreateInstance<TSprite>();
                sprite.Pos = (x0 * 24, y0 * 24);
                Add(sprite);

                i++;
            }
        }

    }
}

