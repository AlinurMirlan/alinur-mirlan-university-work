using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TreasureSeeker
{
    internal class ColorSpace
    {
        private readonly int width;
        private readonly int height;

        public Bitmap Image { get; }
        public byte[,] Grayscale { get; }
        public byte[,,] RGBValues { get; }

        public ColorSpace(System.Drawing.Image image)
        {
            Image = new Bitmap(image);
            width = image.Width;
            height = image.Height;
            RGBValues = new byte[width, height, 3];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color color = Image.GetPixel(i, j);
                    RGBValues[i, j, 0] = color.R;
                    RGBValues[i, j, 1] = color.G;
                    RGBValues[i, j, 2] = color.B;
                }
            }
            Grayscale = GetGrayscale(RGBValues);
        }

        private byte[,] GetGrayscale(byte[,,] rgb)
        {
            byte[,] grayscaleBytes = new byte[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    byte scale = (byte)(0.299 * rgb![i, j, 0] + 0.587 * rgb[i, j, 1] + 0.114 * rgb[i, j, 2]);
                    grayscaleBytes[i, j] = scale;
                }
            }

            return grayscaleBytes;
        }
    }
}
