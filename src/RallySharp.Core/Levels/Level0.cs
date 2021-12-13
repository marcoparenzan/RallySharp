using RallySharp.Models;
using System;

namespace RallySharp.Levels
{
    public class Level0 : Level
    {
        public Level0()
        {
            Add(new MainSprite { Pos = (480, 1272), Animation = new(0) });
            Add(new EnemySprite { Pos = (480, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
            Add(new EnemySprite { Pos = (480 - 48, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
            Add(new EnemySprite { Pos = (480 + 48, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });

            RandomPlace<FlagSprite>(10);
            RandomPlace<RockSprite>(5);
        }

        public override int Index => 0;
    }
}
