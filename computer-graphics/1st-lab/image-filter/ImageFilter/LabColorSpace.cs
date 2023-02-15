using ImageFilter.Abstractions;
using MathNet.Numerics.LinearAlgebra;

namespace ImageFilter
{
    internal class LabColorSpace : IColorSpace
    {
        private readonly Image image;
        private static readonly MatrixBuilder<double> matrixBuilder = Matrix<double>.Build;

        public double[,,] ColorSpace { get; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public double this[int x, int y, int z]
        {
            get => ColorSpace[x, y, z];
        }

        public LabColorSpace(Image image)
        {
            this.image = image;
            ImageWidth = image.Width;
            ImageHeight = image.Height;
            ColorSpace = new double[ImageWidth, ImageHeight, 3];
            GetLABColorSpace();
        }

        private void GetLABColorSpace()
        {
            GetLMSColorSpace(out double[,,] lmsValues);
            var transfOne = matrixBuilder.DenseOfDiagonalArray(new[] { .5774, .4082, .7071 });
            var transfTwo = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { 1, 1, 1 },
                { 1, 1, -2 } ,
                { 1, -1, 0 }
            });

            for (int i = 0; i < ImageWidth; i++)
            {
                for (int j = 0; j < ImageHeight; j++)
                {
                    double[] lmsValue = { Math.Log10(lmsValues[i, j, 0]), Math.Log10(lmsValues[i, j, 1]), Math.Log10(lmsValues[i, j, 2]) };
                    var lmsMatrix = matrixBuilder.DenseOfColumnMajor(3, 1, lmsValue);
                    var labValue = transfOne * transfTwo * lmsMatrix;
                    ColorSpace[i, j, 0] = labValue[0, 0];
                    ColorSpace[i, j, 1] = labValue[1, 0];
                    ColorSpace[i, j, 2] = labValue[2, 0];
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
    }
}
