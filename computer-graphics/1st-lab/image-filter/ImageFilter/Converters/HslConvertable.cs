using ImageFilter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilter.Converters
{
    internal class HslConvertable : IRgbConverter
    {
        public void ConvertToRgb(in double[,,] source, out int[,,] rgbValues)
        {
            int width = source.GetLength(0);
            int height = source.GetLength(1);
            rgbValues = new int[width, height, 3];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double h = source[i, j, 0];
                    double s = source[i, j, 1];
                    double l = source[i, j, 2];

                    double c = (1 - Math.Abs(2 * l - 1)) * s;
                    double x = c * (1 - Math.Abs((h / 60) % 2 - 1));
                    double m = l - c / 2;
                    double[] rgbValue = new double[3];
                    switch (h) 
                    {
                        case >= 0 and < 60:
                            rgbValue[0] = c;
                            rgbValue[1] = x;
                            rgbValue[2] = 0;
                            break;
                        case < 120:
                            rgbValue[0] = x;
                            rgbValue[1] = c;
                            rgbValue[2] = 0;
                            break;
                        case < 180:
                            rgbValue[0] = 0;
                            rgbValue[1] = c;
                            rgbValue[2] = x;
                            break;
                        case < 240:
                            rgbValue[0] = 0;
                            rgbValue[1] = x;
                            rgbValue[2] = c;
                            break;
                        case < 300:
                            rgbValue[0] = x;
                            rgbValue[1] = 0;
                            rgbValue[2] = c;
                            break;
                        case < 360:
                            rgbValue[0] = c;
                            rgbValue[1] = 0;
                            rgbValue[2] = x;
                            break;
                    }

                    for (int k = 0; k < 3; k++)
                    {
                        rgbValues[i, j, k] = (int)Math.Round((rgbValue[k] + m) * 255);
                        if (rgbValues[i, j, k] > 255)
                            rgbValues[i, j, k] = 255;
                        else if (rgbValues[i, j, k] < 0)
                            rgbValues[i, j, k] = 0;

                    }
                }
            }
        }
    }
}
