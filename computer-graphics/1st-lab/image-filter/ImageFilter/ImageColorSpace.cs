using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter
{
    public class ImageColorSpace
    {
        private readonly Image image;
        private static readonly MatrixBuilder<double> matrixBuilder = Matrix<double>.Build;

        public double[,,] LABValues { get; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public ImageColorSpace(Image image)
        {
            this.image = image;
            ImageWidth = image.Width;
            ImageHeight = image.Height;
            LABValues = new double[ImageWidth, ImageHeight, 3];
            GetLABColorSpace();
        }

        private void GetLABColorSpace()
        {
            GetLMSColorSpace(out double[,,] lmsValues);
            var transfOne = matrixBuilder.DenseOfDiagonalArray(new[] { .5774, .4082, .7071 });
            var transfTwo = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { 1, 1, 1 },
                { 1, 1, -2 },
                { 1, -1, 0 }
            });

            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
                {
                    double[] lmsValue = { Math.Log10(lmsValues[i, j, 0]), Math.Log10(lmsValues[i, j, 1]), Math.Log10(lmsValues[i, j, 2]) };
                    var lmsMatrix = matrixBuilder.DenseOfColumnMajor(3, 1, lmsValue);
                    var labValue = transfOne * transfTwo * lmsMatrix;
                    LABValues[i, j, 0] = labValue[0, 0];
                    LABValues[i, j, 1] = labValue[1, 0];
                    LABValues[i, j, 2] = labValue[2, 0];
                }
            }
        }

        private void GetLMSColorSpace(out double[,,] lmsValues)
        {
            Bitmap bitmap = new(image);
            lmsValues = new double[ImageWidth, ImageHeight, 3];
            var transfMatrix = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { .3811, .5783, .0402 },
                { .1967, .7244, .0782 },
                { .0241, .1288, .8444 }
            });

            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    double[] rgbValues = 
                    { 
                        ((double)color.R / 255) * 0.92157,
                        ((double)color.G / 255) * 0.92157,
                        ((double)color.B / 255) * 0.92157 
                    };
                    var rgbMatrix = matrixBuilder.DenseOfColumnMajor(3, 1, rgbValues);
                    var lmsValue = transfMatrix * rgbMatrix;
                    for (int k = 0; k < 3; k++)
                        lmsValue[k, 0] = lmsValue[k, 0] < 0.01176 ? 0.01176 : lmsValue[k, 0];

                    lmsValues[i, j, 0] = lmsValue[0, 0];
                    lmsValues[i, j, 1] = lmsValue[1, 0];
                    lmsValues[i, j, 2] = lmsValue[2, 0];
                }
            }
        }

        public static void GetRGBColorSpaceFromLAB(in double[,,] labValues, out double[,,] rgbValues)
        {
            GetLMSColorSpaceFromLAB(in labValues, out double[,,] lmsValues);
            int width = labValues.GetLength(0);
            int height = labValues.GetLength(1);
            rgbValues = new double[width, height, 3];
            var transfMatrix = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { 4.4679, -3.5873, .1193 },
                { -1.2186, 2.3809, -.1624 },
                { .0497, -.2439, 1.2045 }
            });

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double[] lmsValue = new[] { lmsValues[i, j, 0], lmsValues[i, j, 1], lmsValues[i, j, 2] };
                    var labMatrix = matrixBuilder.DenseOfColumnMajor(3, 1, lmsValue);
                    var rgbValue = transfMatrix * labMatrix;
                    rgbValues[i, j, 0] = rgbValue[0, 0];
                    rgbValues[i, j, 1] = rgbValue[1, 0];
                    rgbValues[i, j, 2] = rgbValue[2, 0];
                }
            }
        }

        private static void GetLMSColorSpaceFromLAB(in double[,,] labValues, out double[,,] lmsValues)
        {
            int width = labValues.GetLength(0);
            int height = labValues.GetLength(1);
            lmsValues = new double[width, height, 3];
            var transfOne = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { 1, 1, 1 },
                { 1, 1, -1 },
                { 1, -2, 0 }
            });
            var transfTwo = matrixBuilder.DenseOfDiagonalArray(new[] { .5774, .4082, .7071 });


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double[] labValue = { labValues[i, j, 0], labValues[i, j, 1], labValues[i, j, 2] };
                    var labMatrix = matrixBuilder.DenseOfColumnMajor(3, 1, labValue);
                    var lmsExponent = transfOne * transfTwo * labMatrix;
                    lmsValues[i, j, 0] = Math.Pow(10, lmsExponent[0, 0]);
                    lmsValues[i, j, 1] = Math.Pow(10, lmsExponent[1, 0]);
                    lmsValues[i, j, 2] = Math.Pow(10, lmsExponent[2, 0]);
                }
            }
        }
    }
}
