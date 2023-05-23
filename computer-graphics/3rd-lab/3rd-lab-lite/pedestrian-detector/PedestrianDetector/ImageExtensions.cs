using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector
{
    public static class ImageExtensions
    {
        public static Bitmap Resize(this Image image, Size size)
        {
            // Create a new bitmap with the new size
            Bitmap resizedImage = new(size.Width, size.Height);

            // Create a graphics object from the resized image
            Graphics graphics = Graphics.FromImage(resizedImage);

            // Set the interpolation mode to high quality bicubic
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // Draw the resized image on the graphics object
            graphics.DrawImage(image, 0, 0, size.Width, size.Height);
            return resizedImage;
        }

        public static Image Crop(this Image image, int x1, int y1, int x2, int y2)
        {
            int width = x2 - x1;   // the width of the crop rectangle
            int height = y2 - y1;  // the height of the crop rectangle

            Rectangle cropRectangle = new(x1, y1, width, height);
            Bitmap croppedBitmap = new(cropRectangle.Width, cropRectangle.Height);

            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                g.DrawImage(image, new Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height),
                    cropRectangle, GraphicsUnit.Pixel);
            }

            return croppedBitmap;
        }
    }
}
