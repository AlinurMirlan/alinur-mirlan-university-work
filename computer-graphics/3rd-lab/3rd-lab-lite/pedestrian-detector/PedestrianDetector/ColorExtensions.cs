using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector
{
    public static class ColorExtensions
    {
        public static int Grayscale(this Color color)
        {
            return (int)Math.Floor(0.299 * color.R + 0.587 * color.B + 0.114 * color.G);
        }
    }
}
