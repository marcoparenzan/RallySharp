using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{
    public class CarAnimation
    {
        int baseFrame;
        int frameLength;
        int currentFrame;
        int lastFrame;
        int rotation;

        public CarAnimation(int baseFrame = 0, int frameLength = 12)
        {
            this.baseFrame = baseFrame;
            this.frameLength = frameLength;
            this.rotation = 1;
        }

        public int CurrentFrame => baseFrame + currentFrame;

        public void Crashed()
        {
            currentFrame = lastFrame = 48-baseFrame;
        }

        public void NewDirection(int newDirection, int rotation = 1)
        {
            this.lastFrame = newDirection * 3;
            this.rotation = rotation;
        }

        public void Update()
        {
            if (currentFrame != lastFrame)
            {
                currentFrame = (currentFrame + rotation) % frameLength;
                if (currentFrame < 0) currentFrame += frameLength;
            }
        }

        public override string ToString()
        {
            return $"{currentFrame}/{lastFrame}";
        }
    }
}
