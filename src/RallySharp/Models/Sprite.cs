using RallySharp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public abstract class Sprite
    {
        protected readonly static Vec[] Speed = { (0, -6), (6, 0), (0, 6), (-6, 0) };

        public byte Id { get; set; }
        public Vec Pos { get; set; }
        public int Direction { get; set; }

        // animation
        public Animation Animation { get; set; }

        protected virtual void UpdateRunning()
        {
            Pos += Speed[Direction];
        }

        protected virtual void UpdateReady()
        {
        }

        public Action Update { get; protected set; }

        public Sprite Running()
        {
            Update = UpdateRunning;
            return this;
        }

        public Sprite Ready()
        {
            Update = UpdateReady;
            return this;
        }
    }
}
