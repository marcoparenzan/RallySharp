using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPlayfield
{
    public class Stage
    {
        public int X { get; set; }
        int delta = 10;

        public void Update()
        {
            if (delta >= 0)
            {
                if (X >= 200)
                {
                    delta = -delta;
                }
                else
                {
                    X += delta;
                }
            }
            else if (delta < 0)
            {
                if (X <= -200)
                {
                    delta = -delta;
                }
                else
                {
                    X += delta;
                }
            }
        }
    }
}
