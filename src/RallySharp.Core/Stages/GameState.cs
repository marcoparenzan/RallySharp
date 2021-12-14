using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Stages
{
    public class GameState
    {
        public int Level { get; set; }
        public int Fuel { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
        public int FlagScore { get; set; }
        public int Delay { get; set; }
    }
}
