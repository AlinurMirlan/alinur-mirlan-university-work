using Microsoft.VisualBasic.Logging;
using System.Data;

namespace TreasureSeeker
{ 
    internal class TreasureSeeker
    {
        private readonly byte[,,] rgbValues;
        private const int morphLength = 3;
        private readonly int width;
        private readonly int height;
        private readonly Bitmap bitmap;
        private readonly byte[,] binaryImage;
        private readonly byte[,] grayscale;

        public TreasureSeeker(Image image)
        {
            bitmap = new(image);
            ColorSpace colorSpace = new(bitmap);
            rgbValues = colorSpace.RGBValues;
            grayscale = colorSpace.Grayscale;
            width = image.Width;
            height = image.Height;
            binaryImage = GetBinaryImage(grayscale);
        }

        public Bitmap GetTreasureMap()
        {
            int[,] labels = new int[width, height];
            GetComponents(labels, out Dictionary<int, ItemFeatures> itemFeatures);
            Bitmap resultingBitmap = new(bitmap);
            int itemLabel = GetStartingLabel(labels, itemFeatures);
            Graphics graphics = Graphics.FromImage(resultingBitmap);
            ItemFeatures pointedItem;
            Pen pen = new(Color.SkyBlue, 3);
            do
            {
                Arrowhead arrowhead = new(itemFeatures[itemLabel], binaryImage);
                float x = arrowhead.Features.CenterOfMassX;
                float y = arrowhead.Features.CenterOfMassY;
                int xInt = (int)Math.Round(x);
                int yInt = (int)Math.Round(y);
                Point startPoint = new(xInt, yInt);
                while (labels[xInt, yInt] == 0 || labels[xInt, yInt] == itemLabel)
                {
                    x = arrowhead.NextAbscissa(x);
                    y = arrowhead.AxisFunc(x);
                    xInt = (int)Math.Round(x);
                    yInt = (int)Math.Round(y);
                }
                itemLabel = labels[xInt, yInt];
                pointedItem = itemFeatures[itemLabel];
                Point endPoint = new(pointedItem.CenterOfMassX, pointedItem.CenterOfMassY);
                graphics.DrawLine(pen, startPoint, endPoint);
            }
            while (pointedItem.Compactness > 16 && pointedItem.Compactness < 20
                && pointedItem.Elongation > 3.69 && pointedItem.Elongation < 3.95);

            DemarcateTreasure(labels, itemLabel, resultingBitmap);
            return resultingBitmap;
        }

        private void DemarcateTreasure(int[,] labels, int label, Bitmap map)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Point point = new(i, j);
                    if (binaryImage[point.X, point.Y] == 1)
                        continue;

                    if (IsTreasureBorder(point, labels, label))
                        map.SetPixel(point.X, point.Y, Color.SkyBlue);
                }
            }
        }

        private int GetStartingLabel(int[,] labels, Dictionary<int, ItemFeatures> itemFeatures)
        {
            int itemLabel = 1;
            HashSet<int> passedComponents = new();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int label = labels[i, j];
                    if (label == 0 || passedComponents.Contains(label))
                        continue;

                    ItemFeatures item = itemFeatures[label];
                    double compactness = item.Compactness;
                    double elongation = item.Elongation;
                    int r = rgbValues[i, j, 0];
                    int g = rgbValues[i, j, 1];
                    int b = rgbValues[i, j, 2];
                    bool isRed = r - 30 >= g && r - 30 >= b;
                    passedComponents.Add(label);
                    if (compactness > 16 && compactness < 20 && elongation > 3.69 && elongation < 3.95 && isRed)
                    {
                        itemLabel = label;
                        i = width;
                        break;
                    }
                }
            }

            return itemLabel;
        }

        private void GetComponents(int[,] labels, out Dictionary<int, ItemFeatures> itemFeatures)
        {
            int label = 1;
            itemFeatures = new Dictionary<int, ItemFeatures>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (binaryImage[i, j] != 0 && labels[i, j] == 0)
                    {
                        itemFeatures[label] = new ItemFeatures();
                        Fill(labels, itemFeatures, i, j, label++, out _);
                    }
                }
            }
        }

        private void Fill(int[,] labels, Dictionary<int, ItemFeatures> itemFeatures, int x, int y, int label, out bool isParentEdge)
        {
            if (binaryImage[x, y] == 0)
            {
                isParentEdge = true;
                return;
            }

            isParentEdge = false;
            if (labels[x, y] == 0)
            {
                itemFeatures[label].Coordinates.Add(new Point(x, y));
                bool isEdgePixel = false;
                labels[x, y] = label;

                if (x > 0)
                {
                    Fill(labels, itemFeatures, x - 1, y, label, out bool isEdge);
                    if (isEdge)
                        isEdgePixel = isEdge;
                    if (y > 0)
                        Fill(labels, itemFeatures, x - 1, y - 1, label, out _);
                    if (y < height - 1)
                        Fill(labels, itemFeatures, x - 1, y + 1, label, out _);
                }
                if (x < width - 1)
                {
                    Fill(labels, itemFeatures, x + 1, y, label, out bool isEdge);
                    if (isEdge)
                        isEdgePixel = isEdge;
                    if (y > 0)
                        Fill(labels, itemFeatures, x + 1, y - 1, label, out _);
                    if (y < height - 1)
                        Fill(labels, itemFeatures, x + 1, y + 1, label, out _);
                }
                if (y > 0)
                {
                    Fill(labels, itemFeatures, x, y - 1, label, out bool isEdge);
                    if (isEdge)
                        isEdgePixel = isEdge;
                }

                if (y < height - 1)
                {
                    Fill(labels, itemFeatures, x, y + 1, label, out bool isEdge);
                    if (isEdge)
                        isEdgePixel = isEdge;
                }

                if (isEdgePixel)
                    itemFeatures[label].Perimeter++;
            }
        }

        private int GetThresholdByOtsu(int[] histogram)
        {
            int totalPixels = width * height, threshold = 0; ;
            double maxBcv = 0;
            for (int i = 0; i < 255; i++)
            {
                if (histogram[i] == 0)
                    continue;

                int backgroundPixels = 0;
                int foregroundPixels = 0;
                double ub = 0;
                double uf = 0;
                for (int j = 0; j < 256; j++)
                {
                    if (j <= i)
                    {
                        backgroundPixels += histogram[j];
                        ub += histogram[j] * j;
                    }
                    else
                    {
                        foregroundPixels += histogram[j];
                        uf += histogram[j] * j;
                    }
                }
                double wb = (double)backgroundPixels / totalPixels;
                double wf = (double)foregroundPixels / totalPixels;
                ub /= backgroundPixels;
                uf /= foregroundPixels;
                // Between Class Variance metric
                double bcv = wb * wf * Math.Pow(ub - uf, 2);
                if (bcv > maxBcv)
                {
                    maxBcv = bcv;
                    threshold = i;
                }
            }

            return threshold;
        }

        private int GetThresholdByHuang(int[] histogram, byte minLevel, byte maxLevel)
        {
            int c = maxLevel - minLevel;
            int sl_1 = 0, wl_1 = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                sl_1 += histogram[i];
                wl_1 += histogram[i] * i;
            }
            int st_1 = 0, wt_1 = 0;
            double minEntropy = double.MaxValue;
            byte threshold = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    st_1 += histogram[j];
                    wt_1 += j * histogram[j];
                }
                int st = st_1 + histogram[i];
                int wt = wt_1 + i * histogram[i];
                int _st = sl_1 - st;
                int _wt = wl_1 - wt;
                byte u0 = (byte)Math.Round((double)wt / st);
                byte u1 = (byte)Math.Round((double)_wt / _st);
                double sumS = 0;
                for (int j = 0; j < histogram.Length; j++)
                {
                    double u;
                    if (j <= i)
                        u = 1 / ((1 + (double)Math.Abs(j - u0) / c));
                    else
                        u = 1 / ((1 + (double)Math.Abs(j - u1) / c));
                    sumS += ShannonFunc(u) * histogram[j];
                }
                double entropy = sumS / (width * height * Math.Log(2));
                if (minEntropy > entropy)
                {
                    minEntropy = entropy;
                    threshold = (byte)i;
                }
            }

            return threshold;
        }

        private byte[,] GetBinaryImage(byte[,] grayscaleValues)
        {
            int[] histogram = new int[256];
            byte minLevel = 255, maxLevel = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grayscaleValues[i, j] < minLevel)
                        minLevel = grayscaleValues[i, j];

                    if (grayscaleValues[i, j] > maxLevel)
                        maxLevel = grayscaleValues[i, j];

                    histogram[grayscaleValues[i, j]] += 1;
                }
            }

            int threshold = GetThresholdByHuang(histogram, minLevel, maxLevel);
            if (threshold > 3)
                threshold -= 3;
            byte[,] binary = new byte[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (grayscale[i, j] >= threshold)
                        binary[i, j] = 1;
                    else
                        binary[i, j] = 0;
                }
            }

            return binary;
        }

        private static double ShannonFunc(double u) => -u * Math.Log(u == 0 ? 0.0000000000001 : u) - (1 - u) * Math.Log(1 - (u == 1 ? 0.999999999999 : u));

        private bool IsTreasureBorder(Point point, int[,] labels, int label)
        {
            int rightBorder = point.X + morphLength;
            int bottomBorder = point.Y + morphLength;
            int leftBorder = point.X - morphLength;
            int topBorder = point.Y - morphLength;
            if (rightBorder >= width)
                rightBorder = width - 1;
            if (bottomBorder >= height)
                bottomBorder = height - 1;
            if (leftBorder < 0)
                leftBorder = 0;
            if (topBorder < 0)
                topBorder = 0;

            for (int i = leftBorder; i <= rightBorder; i++)
            {
                for (int j = topBorder; j <= bottomBorder; j++)
                {
                    if (binaryImage[i, j] == 1 && labels[i, j] == label)
                        return true;
                }
            }

            return false;
        }
    }
}


