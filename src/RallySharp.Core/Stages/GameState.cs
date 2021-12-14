using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Stages
{
    public class GameState
    {
        public int Level { get; set; } = 0;
        public int Fuel { get; set; } = 512;
        public int Lives { get; set; } = 3;
        public int Score { get; set; } = 0;
        public int FlagScore { get; set; } = 100;
    }
}
