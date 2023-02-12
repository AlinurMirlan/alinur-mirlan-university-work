using ImageFilter.Abstractions;
using MathNet.Numerics.LinearAlgebra;

namespace ImageFilter.Converters
{
    internal class LabConvertable : IRgbConverter
    {
        private static readonly MatrixBuilder<double> matrixBuilder = Matrix<double>.Build;
        private int imageWidth;
        private int imageHeight;

        private void GetLmsColorSpace(in double[,,] source, out double[,,] lmsValues)
        {
            imageWidth = source.GetLength(0);
            imageHeight = source.GetLength(1);
            lmsValues = new double[imageWidth, imageHeight, 3];
            var transfOne = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { 1, 1, 1 },
                { 1, 1, -1 },
                { 1, -2, 0 }
            });
            var transfTwo = matrixBuilder.DenseOfDiagonalArray(new[] { .5774, .4082, .7071 });


            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
                {
                    double[] labValue = { source[i, j, 0], source[i, j, 1], source[i, j, 2] };
                    var labMatrix = matrixBuilder.DenseOfColumnMajor(3, 1, labValue);
                    var lmsExponent = transfOne * transfTwo * labMatrix;
                    lmsValues[i, j, 0] = Math.Pow(10, lmsExponent[0, 0]);
                    lmsValues[i, j, 1] = Math.Pow(10, lmsExponent[1, 0]);
                    lmsValues[i, j, 2] = Math.Pow(10, lmsExponent[2, 0]);
                }
            }
        }

        public void ConvertToRgb(in double[,,] source, out double[,,] rgbValues)
        {
            GetLmsColorSpace(in source, out double[,,] lmsValues);
            rgbValues = new double[imageWidth, imageHeight, 3];
            var transfMatrix = matrixBuilder.DenseOfArray(new double[3, 3]
            {
                { 4.4679, -3.5873, .1193 },
                { -1.2186, 2.3809, -.1624 },
                { .0497, -.2439, 1.2045 }
            });

            for (int i = 0; i < imageWidth; i++)
            {
                for (int j = 0; j < imageHeight; j++)
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
    }
}
