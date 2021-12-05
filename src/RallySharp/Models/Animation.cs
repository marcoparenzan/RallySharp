using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class Animation
    {
        int baseFrame;
        int frameLength;
        int currentFrame;
        int lastFrame;
        int rotation;

        public Animation(int baseFrame = 0, int frameLength = 12)
        {
            this.baseFrame = baseFrame;
            this.frameLength = frameLength;
            this.rotation = 1;
        }

        public int CurrentFrame => baseFrame + currentFrame;

        public void NewDirection(int newDirection, int rotation = 1)
        {
            this.lastFrame = newDirection * 3;
            this.rotation = rotation;
        }

        public void Update()
        {
            if (currentFrame != lastFrame)
            {
                currentFrame = (currentFrame + rotation) % 12;
                if (currentFrame < 0) currentFrame += 12;
            }
        }

        public override string ToString()
        {
            return $"{currentFrame}/{lastFrame}";
        }
    }
}
