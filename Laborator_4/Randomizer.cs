using OpenTK;
using System;
using System.Drawing;

namespace Laborator_4
{
    class Randomizer
    {
        private Random r;
     

        //constructor implicit
        public Randomizer()
        {
            r = new Random();
        }
        //generarea culorii random
        public Color GetRandomColor()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);

            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }

        
    }
}
