using RallySharp.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RallySharp.Models
{

    public class MainSprite : Sprite
    {
        public MainSprite()
        {
            this.Animation = new Animation(0);
            Ready();
        }
    }
}
