using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TreasureSeeker
{
    internal class Arrowhead
    {
        private readonly byte[,] binaryImage;
        public ItemFeatures Features { get; }

        public Func<float, float> AxisFunc { get; }

        public Func<float, float> NextAbscissa { get; }

        public Point Head { get; set; }

        public Arrowhead(ItemFeatures features, byte[,] binaryImage)
        {
            this.binaryImage = binaryImage;
            Features = features;
            AxisFunc = GetLineEquation(features.CenterOfMassX, features.CenterOfMassY, features.Orientation);
            bool direction = GetDirection();
            NextAbscissa = direction ?
                (float x) => x + (float)Math.Cos(features.Orientation) * 1 :
                (float x) => x - (float)Math.Cos(features.Orientation) * 1;
        }

        public static Func<float, float> GetLineEquation(float x, float y, double angle)
        {
            // Slope
            double m = Math.Tan(angle);
            double b = y - (m * x);
            return (float x) => (float)Math.Round(m * x + b);
        }

        private bool GetDirection()
        {
            int x = Features.CenterOfMassX, y = Features.CenterOfMassY;
            double angle = Features.Orientation;
            PointF prevPoint = new(x, y);
            while (true)
            {
                PointF nextPoint = new() { X = prevPoint.X + (float)Math.Cos(angle) * 1 };
                nextPoint.Y = AxisFunc(nextPoint.X);

                if (binaryImage[(int)Math.Round(nextPoint.X), (int)Math.Round(nextPoint.Y)] == 0)
                    break;

                prevPoint = nextPoint;
            }
            int straightWidth = GetWidth(prevPoint);

            prevPoint = new(x, y);
            while (true)
            {
                PointF nextPoint = new() { X = prevPoint.X - (float)Math.Cos(angle) * 1 };
                nextPoint.Y = AxisFunc(nextPoint.X);
                if (binaryImage[(int)Math.Round(nextPoint.X), (int)Math.Round(nextPoint.Y)] == 0)
                    break;

                prevPoint = nextPoint;
            }
            int oppositeWidth = GetWidth(prevPoint);
            return oppositeWidth > straightWidth;
        }

        private int GetWidth(PointF point)
        {
            int width = 0;
            PointF crossPoint = point;
            Func<float, float> crossEquation = GetLineEquation((int)Math.Round(crossPoint.X), (int)Math.Round(crossPoint.Y), Features.Orientation + Math.PI / 2);
            double angle = Features.Orientation;
            while (binaryImage[(int)Math.Round(crossPoint.X), (int)Math.Round(crossPoint.Y)] != 0)
            {
                width++;
                crossPoint = new() { X = crossPoint.X + (float)Math.Cos(angle + Math.PI / 2) * 1 };
                crossPoint.Y = crossEquation(crossPoint.X);
            }

            crossPoint = point;
            while (binaryImage[(int)Math.Round(crossPoint.X), (int)Math.Round(crossPoint.Y)] != 0)
            {
                width++;
                crossPoint = new() { X = crossPoint.X - (float)Math.Cos(angle + Math.PI / 2) * 1 };
                crossPoint.Y = crossEquation(crossPoint.X);
            }

            return width;
        }
    }
}