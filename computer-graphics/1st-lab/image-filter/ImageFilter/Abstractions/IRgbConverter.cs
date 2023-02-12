using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter.Abstractions
{
    public interface IRgbConverter
    {
        public void ConvertToRgb(in double[,,] source, out double[,,] rgbValues);
    }
}
