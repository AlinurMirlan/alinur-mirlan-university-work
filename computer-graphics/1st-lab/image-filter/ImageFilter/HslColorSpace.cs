using ImageFilter.Abstractions;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter
{
    public class HslColorSpace : IColorSpace
    {
        private readonly Image image;

        public double[,,] ColorSpace { get; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public double this[int x, int y, int z]
        {
            get => ColorSpace[x, y, z];
        }

        public HslColorSpace(Image image)
        {
            this.image = image;
            ImageWidth = image.Width;
            ImageHeight = image.Height;
            ColorSpace = new double[ImageWidth, ImageHeight, 3];
            GetHslColorSpace();
        }

        private void GetHslColorSpace()
        {
            Bitmap bitmap = new(image);
            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
                {
                    double[] rgbValue =
                    {
                        (double)bitmap.GetPixel(i, j).R / 255,
                        (double)bitmap.GetPixel(i, j).G / 255,
                        (double)bitmap.GetPixel(i, j).B / 255
                    };
                    double cMax = rgbValue.Max();
                    double cMin = rgbValue.Min();
                    double delta = cMax - cMin;

                    double l = (cMax + cMin) / 2;
                    double h;
                    if (delta <= 0)
                        h = 0;
                    else if (cMax == rgbValue[0])
                        h = 60 * (((rgbValue[1] - rgbValue[2]) / delta) % 6);
                    else if (cMax == rgbValue[1])
                        h = 60 * (((rgbValue[2] - rgbValue[0]) / delta) + 2);
                    else
                        h = 60 * (((rgbValue[0] - rgbValue[1]) / delta) + 4);

                    double s = delta <= 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));
                    ColorSpace[i, j, 0] = h;
                    ColorSpace[i, j, 1] = s;
                    ColorSpace[i, j, 2] = l;
                }
            }
        }
    }
}
