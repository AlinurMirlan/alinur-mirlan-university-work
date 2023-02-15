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

        public Bitmap MergeTargetColorSpace(IRgbConverter converter, bool persistContrast = true)
        {
            MergeColorSpaces(out double[,,] colorSpace, persistContrast);
            converter.ConvertToRgb(colorSpace, out int[,,] rgbValues);
            int width = rgbValues.GetLength(0);
            int height = rgbValues.GetLength(1);
            Bitmap bitmap = new(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color color = Color.FromArgb(255,
                        rgbValues[i, j, 0],
                        rgbValues[i, j, 1],
                        rgbValues[i, j, 2]
                    );

                    bitmap.SetPixel(i, j, color);
                }
            }

            return bitmap;
        }

        private void MergeColorSpaces(out double[,,] colorSpace, bool persistContrast = true)
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
                    for (int k = 0; k < 3; k++)
                    {
                        colorSpace[i, j, k] = Et[k] + (Source[i, j, k] - Es[k]) * (persistContrast ? (Dt[k] / Ds[k]) : 1);
                    }
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
