using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter
{
    public static class ImageColorMerger
    {
        public static Bitmap Merge(ImageColorSpace source, ImageColorSpace target)
        {
            double[] Es = CalculateMatExpectations(source.LABValues);
            double[] Ds = CalculateDispersions(source.LABValues, Es);

            double[] Et = CalculateMatExpectations(target.LABValues);
            double[] Dt = CalculateDispersions(target.LABValues, Et);

            double[,,] mergedColorSpace = new double[source.ImageWidth, source.ImageHeight, 3];
            for (int i = 0; i < source.ImageWidth; i++)
            {
                for (int j = 0; j < source.ImageHeight; j++)
                {
                    mergedColorSpace[i, j, 0] = Et[0] + (source.LABValues[i, j, 0] - Es[0]) * (Dt[0] / Ds[0]);
                    mergedColorSpace[i, j, 1] = Et[1] + (source.LABValues[i, j, 1] - Es[1]) * (Dt[1] / Ds[1]);
                    mergedColorSpace[i, j, 2] = Et[2] + (source.LABValues[i, j, 2] - Es[2]) * (Dt[2] / Ds[2]);
                }
            }

            ImageColorSpace.GetRGBColorSpaceFromLAB(mergedColorSpace, out double[,,] rgbValues);
            return CreateBitmapFromRGB(rgbValues);
        }

        private static Bitmap CreateBitmapFromRGB(double[,,] rgbValues)
        {
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

        private static double[] CalculateMatExpectations(in double[,,] colorSpace)
        {
            int width = colorSpace.GetLength(0);
            int height = colorSpace.GetLength(1);
            double[] matExpectations = new double[3];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matExpectations[0] += colorSpace[i, j, 0];
                    matExpectations[1] += colorSpace[i, j, 1];
                    matExpectations[2] += colorSpace[i, j, 2];
                }
            }

            int pixelCount = width * height;
            for (int i = 0; i < matExpectations.Length; i++)
            {
                matExpectations[i] /= pixelCount;
            }

            return matExpectations;
        }

        private static double[] CalculateDispersions(in double[,,] colorSpace, in double[] matExpectations)
        {
            int width = colorSpace.GetLength(0);
            int height = colorSpace.GetLength(1);
            double[] dispersions = new double[3];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    dispersions[0] += Math.Pow(colorSpace[i, j, 0] - matExpectations[0], 2);
                    dispersions[1] += Math.Pow(colorSpace[i, j, 1] - matExpectations[1], 2);
                    dispersions[2] += Math.Pow(colorSpace[i, j, 2] - matExpectations[2], 2);
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
