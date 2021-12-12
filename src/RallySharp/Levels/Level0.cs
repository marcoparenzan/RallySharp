using RallySharp.Models;

namespace RallySharp.Levels
{
    public class Level0 : Level
    {
        public Level0()
        {
            Add(new MainSprite { Id = 0, Pos = (480, 1272), Animation = new(0) });
            Add(new EnemySprite { Id = 1, Pos = (480, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
            Add(new EnemySprite { Id = 2, Pos = (480 - 48, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
            Add(new EnemySprite { Id = 3, Pos = (480 + 48, 1272 + 24 * 4), Animation = new(12), MainSprite = MainSprite });
        }

        public override int Index => 0;
    }
}
