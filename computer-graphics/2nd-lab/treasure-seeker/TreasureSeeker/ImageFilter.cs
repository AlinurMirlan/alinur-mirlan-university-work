using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureSeeker
{
    internal class ImageFilter
    {
        private readonly ColorSpace colorSpace;
        private int medianLength = 1;
        private readonly Bitmap bitmap;

        public ImageFilter(ColorSpace colorSpace)
        {
            this.colorSpace = colorSpace;
            bitmap = colorSpace.Image;
        }

        public Bitmap GreyWorldFilter()
        {
            double[] rgbAvg = new double[3] { 0, 0, 0 };
            int width = bitmap.Width;
            int height = bitmap.Height;
            int length = width * height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int k = 0; k < 3; k++)
                        rgbAvg[k] += colorSpace.RGBValues[i, j, k];
                }
            }
            double avg = 0;
            for (int i = 0; i < 3; i++)
            {
                rgbAvg[i] /= length;
                avg += rgbAvg[i];
            }

            avg /= rgbAvg.Length;
            Bitmap resultingBitmap = new(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int[] rgb = new int[3];
                    for (int k = 0; k < 3; k++)
                    {
                        rgb[k] = (int)Math.Round((colorSpace.RGBValues[i, j, k] * avg) / rgbAvg[k]);
                        if (rgb[k] > 255)
                            rgb[k] = 255;
                    }
                    Color color = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    resultingBitmap.SetPixel(i, j, color);
                }
            }

            return resultingBitmap;
        }

        private Point Median(Point point)
        {
            List<(Point point, byte scale)> points = new();
            int rightBorder = point.X + medianLength;
            int bottomBorder = point.Y + medianLength;
            int leftBorder = point.X - medianLength;
            int topBorder = point.Y - medianLength;
            if (rightBorder >= bitmap.Width)
                rightBorder = bitmap.Width - 1;
            if (bottomBorder >= bitmap.Height)
                bottomBorder = bitmap.Height - 1;
            if (leftBorder < 0)
                leftBorder = 0;
            if (topBorder < 0)
                topBorder = 0;

            for (int i = leftBorder; i <= rightBorder; i++)
            {
                for (int j = topBorder; j <= bottomBorder; j++)
                    points.Add((new Point(i, j), colorSpace.Grayscale[i, j]));
            }

            points.Sort(new MedianComparer());
            return points[points.Count / 2].point;
        }

        public Bitmap MedianFilter(int medianLen)
        {
            medianLength = medianLen;
            Bitmap resultingBitmap = new(bitmap);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Point median = Median(new Point(i, j));
                    Color medianColor = bitmap.GetPixel(median.X, median.Y);
                    resultingBitmap.SetPixel(i, j, medianColor);
                }
            }

            return resultingBitmap;
        }
    }
}
