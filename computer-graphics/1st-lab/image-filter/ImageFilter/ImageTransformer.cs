using ImageFilter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter
{
    public class ImageTransformer
    {
        public IColorSpace Source { get; set; }
        public IColorSpace Target { get; set; }

        public ImageTransformer(IColorSpace source, IColorSpace target)
        {
            this.Source = source;
            this.Target = target;
        }

        public Bitmap MergeTargetColorSpace(IRgbConverter converter)
        {
            MergeColorSpaces(out double[,,] colorSpace);
            converter.ConvertToRgb(colorSpace, out double[,,] rgbValues);
            int width = rgbValues.GetLength(0);
            int height = rgbValues.GetLength(1);
            Bitmap bitmap = new(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int r = (int)Math.Round((rgbValues[i, j, 0] < 0 ? 0 : rgbValues[i, j, 0]) * 255);
                    int g = (int)Math.Round((rgbValues[i, j, 1] < 0 ? 0 : rgbValues[i, j, 1]) * 255);
                    int b = (int)Math.Round((rgbValues[i, j, 2] < 0 ? 0 : rgbValues[i, j, 2]) * 255);
                    Color color = Color.FromArgb(255,
                        r > 255 ? 255 : r,
                        g > 255 ? 255 : g,
                        b > 255 ? 255 : b
                    );

                    bitmap.SetPixel(i, j, color);
                }
            }

            return bitmap;
        }

        private void MergeColorSpaces(out double[,,] colorSpace)
        {
            double[] Es = CalculateMatExpectations(Source);
            double[] Ds = CalculateDispersions(Source, Es);

            double[] Et = CalculateMatExpectations(Target);
            double[] Dt = CalculateDispersions(Target, Et);

            int width = Source.ImageWidth;
            int height = Source.ImageHeight;
            colorSpace = new double[width, height, 3];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    colorSpace[i, j, 0] = Et[0] + (Source[i, j, 0] - Es[0]) * (Dt[0] / Ds[0]);
                    colorSpace[i, j, 1] = Et[1] + (Source[i, j, 1] - Es[1]) * (Dt[1] / Ds[1]);
                    colorSpace[i, j, 2] = Et[2] + (Source[i, j, 2] - Es[2]) * (Dt[2] / Ds[2]);
                }
            }
        }

        private static double[] CalculateMatExpectations(IColorSpace source)
        {
            double[] matExpectations = new double[3];
            int width = source.ImageWidth;
            int height = source.ImageHeight;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matExpectations[0] += source[i, j, 0];
                    matExpectations[1] += source[i, j, 1];
                    matExpectations[2] += source[i, j, 2];
                }
            }

            int pixelCount = width * height;
            for (int i = 0; i < matExpectations.Length; i++)
            {
                matExpectations[i] /= pixelCount;
            }

            return matExpectations;
        }

        private static double[] CalculateDispersions(IColorSpace source, in double[] matExpectations)
        {
            int width = source.ImageWidth;
            int height = source.ImageHeight;
            double[] dispersions = new double[3];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    dispersions[0] += Math.Pow(source[i, j, 0] - matExpectations[0], 2);
                    dispersions[1] += Math.Pow(source[i, j, 1] - matExpectations[1], 2);
                    dispersions[2] += Math.Pow(source[i, j, 2] - matExpectations[2], 2);
                }
            }

            int pixelCount = width * height;
            for (int i = 0; i < dispersions.Length; i++)
            {
                dispersions[i] = Math.Sqrt(dispersions[i] / pixelCount);
            }

            return dispersions;
        }

    }
}
