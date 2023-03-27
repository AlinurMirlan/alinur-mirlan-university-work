using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureSeeker
{
    internal class MedianComparer : IComparer<(Point point, byte scale)>
    {
        public int Compare((Point point, byte scale) x, (Point point, byte scale) y)
        {
            return x.scale.CompareTo(y.scale);
        }
    }
}
