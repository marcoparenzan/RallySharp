using RallySharp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class Sprite
    {
        public readonly static Vec[] Direction = { (0, 6), (6, 0), (0, -6), (-6, 0) };

        public Sprite()
        {
            Update = Ready;
        }

        public Vec Pos { get; set; }
        public Vec Speed { get; set; }

        // animation
        public int CurrentAnimationFrame { get; set; }
        public int AnimationEnds { get; set; }

        // debugging
        public bool Collided { get; set; }

        protected virtual void Ready()
        { 
        }

        protected virtual void Running()
        {
            Pos += (Speed.x, -Speed.y);
        }

        public Action Update { get; private set; }

        public void GoToRunning()
        {
            Update = Running;
        }
    }
}
