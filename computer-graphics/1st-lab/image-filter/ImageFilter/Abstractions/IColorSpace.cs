using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter.Abstractions
{
    public interface IColorSpace
    {
        public double this[int x, int y, int z] { get; }
        public double[,,] ColorSpace { get; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
    }
}
