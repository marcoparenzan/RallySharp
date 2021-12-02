using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class Sprite
    {
        public int Type { get; set; }
        public Vec Pos { get; set; }
        public Vec Speed { get; set; }
        public bool Collided { get; set; }
    }
}
