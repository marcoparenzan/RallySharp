using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class ContinuousTrigger
    {
        bool triggered;

        public void Set()
        {
            if (triggered) return;
            triggered = true;
        }

        public void Reset()
        {
            triggered = false;
        }

        public override string ToString()
        {
            return $"{triggered}";
        }

        public bool Triggered()
        {
            if (!triggered) return false;
            return true;
        }
    }
}
