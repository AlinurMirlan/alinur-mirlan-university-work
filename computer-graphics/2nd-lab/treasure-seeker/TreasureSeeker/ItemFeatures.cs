using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureSeeker
{
    internal class ItemFeatures
    {
        public List<Point> Coordinates { get; set; } = new();
        public int Area => Coordinates.Count;
        public int XSum => Coordinates.Aggregate(0, (total, point) => total + point.X);
        public int YSum => Coordinates.Aggregate(0, (total, point) => total + point.Y);
        public int CenterOfMassX => (int)Math.Round((double)XSum / Area);
        public int CenterOfMassY => (int)Math.Round((double)YSum / Area);
        public int Perimeter { get; set; } = 0;
        public double Compactness => Math.Pow(Perimeter, 2) / Area;
        public double Elongation
        {
            get
            {
                CentralMoments(out double m20, out double m02, out double m11);
                double enumerator = m20 + m02 + Math.Pow(Math.Pow(m20 - m02, 2) + 4 * Math.Pow(m11, 2), .5);
                double denominator = m20 + m02 - Math.Pow(Math.Pow(m20 - m02, 2) + 4 * Math.Pow(m11, 2), .5);
                return enumerator / denominator;
            }
        }
        public double Orientation
        {
            get
            {
                CentralMoments(out double m20, out double m02, out double m11);
                return Math.Atan2((2 * m11), (m20 - m02)) / 2;
            }
        }
        public double Slope => Orientation * 180 / Math.PI;

        private void CentralMoments(out double m20, out double m02, out double m11)
        {
            m20 = 0;
            m02 = 0;
            m11 = 0;
            int xc = CenterOfMassX;
            int yc = CenterOfMassY;
            for (int i = 0; i < Coordinates.Count; i++)
            {
                Point p = Coordinates[i];
                m20 += Math.Pow(p.X - xc, 2);
                m02 += Math.Pow(p.Y - yc, 2);
                m11 += (p.X - xc) * (p.Y - yc);
            }
        }
    }
}
