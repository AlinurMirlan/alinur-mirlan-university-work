using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
using PedestrianDetector.MachineLearning.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector;
public class HogDescriptor
{
    private static readonly Random random = new();
    private const int CellSize = 8;
    private const int BlockSize = 16;
    private const int BinSize = 9;

    public string ImageFilesDirectory { get; set; }
    public string LocationsFilePath { get; set; }

    public HogDescriptor(string imageFilesPath, string locationsFilePath)
    {
        ImageFilesDirectory = imageFilesPath;
        LocationsFilePath = locationsFilePath;
    }

    public void ProduceSeparateFeatures(string pedestrianFeaturesOutputFilePath, string backdropFeaturesOutputFilePath)
    {
        ProduceFeatures(pedestrianFeaturesOutputFilePath, GetPedestrianFeatures);
        ProduceFeatures(backdropFeaturesOutputFilePath, GetBackdropFeatures);
    }

    public void ProduceFeatures(string featuresOutputFilePath)
    {
        using StreamReader reader = new(LocationsFilePath);
        using StreamWriter writer = new(featuresOutputFilePath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine()!;
            Func<string, HogFeaturesData>[] featureExtractors =
            {
                GetPedestrianFeatures,
                GetBackdropFeatures
            };
            foreach (Func<string, HogFeaturesData> extractor in featureExtractors)
            {
                HogFeaturesData feature = extractor(line);
                string hogFeatures = string.Concat($"{feature.Label},", string.Join(',', feature.FeatureVector));
                writer.WriteLine(hogFeatures);
            }
        }
    }

    private void ProduceFeatures(string featuresOutputFilePath, Func<string, HogFeaturesData> featureExtractor)
    {
        using StreamReader reader = new(LocationsFilePath);
        using StreamWriter writer = new(featuresOutputFilePath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine()!;
            HogFeaturesData feature = featureExtractor(line);
            string hogFeatures = string.Concat($"{feature.Label},", string.Join(',', feature.FeatureVector));
            writer.WriteLine(hogFeatures);
        }
    }

    private HogFeaturesData GetPedestrianFeatures(string pedestrianDetails)
    {
        string[] tokens = pedestrianDetails.Split('\t', StringSplitOptions.RemoveEmptyEntries);
        string fileName = tokens[0];
        int topLeftX = int.Parse(tokens[2]);
        int bottomRightX = int.Parse(tokens[4]);
        string imageFilePath = Path.Combine(ImageFilesDirectory, $"{fileName}.png");
        using Image pedestrianImage = Image.FromFile(imageFilePath).Crop(topLeftX, Patch.TopY, bottomRightX, Patch.BottomY);

        return GetFeatures(pedestrianImage, true);
    }

    private HogFeaturesData GetBackdropFeatures(string pedestrianDetails)
    {
        string[] tokens = pedestrianDetails.Split('\t', StringSplitOptions.RemoveEmptyEntries);
        int topLeftX = int.Parse(tokens[2]);
        string imageFilePath = Path.Combine(ImageFilesDirectory, $"{tokens[0]}.png");
        using Image image = Image.FromFile(imageFilePath);
        int randomLeftX = random.Next(image.Width);
        while (Math.Abs(topLeftX - randomLeftX) < Patch.Width / 3)
        {
            randomLeftX = random.Next(image.Width - Patch.Width);
        }

        using Image backdropImage = image.Crop(randomLeftX, Patch.TopY, randomLeftX + Patch.Width, Patch.BottomY);
        return GetFeatures(backdropImage, false);
    }

    public static HogFeaturesData GetFeatures(Image image, bool isPedestrian)
    {
        Bitmap resizedImage = image.Resize(new Size(64, 128));
        float[] hog = ComputeHog(resizedImage);
        return new HogFeaturesData()
        {
            Label = isPedestrian,
            FeatureVector = hog,
        };
    }

    private static float[] ComputeHog(Bitmap image)
    {
        int width = image.Width;
        int height = image.Height;

        // Compute gradient magnitudes and orientations
        float[,] magnitudes = new float[width, height];
        float[,] orientations = new float[width, height];

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                double dx = image.GetPixel(x, y + 1).Grayscale() - image.GetPixel(x, y - 1).Grayscale();
                if (dx == 0)
                    dx = 0.00001;

                double dy = image.GetPixel(x - 1, y).Grayscale() - image.GetPixel(x + 1, y).Grayscale();
                magnitudes[x, y] = (float)Math.Sqrt(dx * dx + dy * dy);
                orientations[x, y] = (float)Math.Abs(Math.Pow(Math.Atan2(dy, dx), -1));
                if (orientations[x, y] >= 180)
                    orientations[x, y] = 179.9f;
            }
        }

        // Compute histogram of gradients for each cell
        int cellIndex = 0;
        int cellSize = 8;
        float[] hog = new float[9 * (width / cellSize) * (height / cellSize)];
        int[] bins = new int[10];
        for (int i = 0; i < 10; i++)
        {
            bins[i] = 20 * i;
        }

        for (int x = 0; x < width - cellSize; x += cellSize)
        {
            for (int y = 0; y < height - cellSize; y += cellSize)
            {
                float[] histogram = new float[9];

                for (int i = x; i < x + cellSize; i++)
                {
                    for (int j = y; j < y + cellSize; j++)
                    {
                        double index = orientations[i, j] / 20;
                        int leftBinIndex = (int)Math.Floor(index);
                        int rightBinIndex = (int)Math.Ceiling(index);
                        histogram[leftBinIndex] += ((bins[rightBinIndex] - orientations[i, j]) / 20) * magnitudes[i, j];
                        if (rightBinIndex >= 9)
                            continue;
                        histogram[rightBinIndex] += ((orientations[i, j] - bins[leftBinIndex]) / 20) * magnitudes[i, j];
                    }
                }

                // Concatenate histogram into the HOG feature vector
                Array.Copy(histogram, 0, hog, cellIndex * 9, 9);
                cellIndex++;
            }
        }

        int blockCount = 2;
        int blockSize = blockCount * cellSize;
        int blockStride = (blockCount - 1) * cellSize;
        int numBlocksX = (width - blockSize) / blockStride + 1;
        int numBlocksY = (height - blockSize) / blockStride + 1;
        int hogSizeForBlockSize = 9 * blockCount;
        int pixelSize = hogSizeForBlockSize * blockCount;
        float[] normalizedHog = new float[numBlocksX * numBlocksY * pixelSize];
        int hogIndex = 0;
        for (int i = 0; i < numBlocksX; i++)
        {
            for (int j = 0; j < numBlocksY; j++)
            {
                float[] block = new float[pixelSize];
                Array.Copy(hog, 9 * (i * 8 + j), block, 0,
                    length: hogSizeForBlockSize);
                Array.Copy(hog, 9 * (i + 1) * 8 + j, block, hogSizeForBlockSize,
                    length: hogSizeForBlockSize);
                float divisor = (float)Math.Sqrt(block.Sum(value => Math.Pow(value, 2)));
                if (divisor != 0)
                {
                    for (int k = 0; k < block.Length; k++)
                    {
                        block[k] /= divisor;
                    }
                }

                Array.Copy(block, 0, normalizedHog, hogIndex * pixelSize,
                    length: pixelSize);
                hogIndex++;
            }
        }

        return normalizedHog;
    }


    public static HogFeaturesData GetFeatures(bool isPedestrian, Image image)
    {
        Bitmap resizedImage = image.Resize(new Size(64, 128));
        float[] hog = Extract(resizedImage);
        return new HogFeaturesData()
        {
            Label = isPedestrian,
            FeatureVector = hog,
        };
    }

    public static float[] Extract(Bitmap image)
    {
        // Convert the input image to grayscale
        Bitmap grayImage = Grayscale(image);

        // Calculate the gradients
        float[,] dx = GradientX(grayImage);
        float[,] dy = GradientY(grayImage);

        // Calculate the magnitude and orientation
        float[,] magnitude = Magnitude(dx, dy);
        float[,] orientation = Orientation(dx, dy);

        // Calculate the histogram of gradients in 8x8 cells
        int width = grayImage.Width / CellSize;
        int height = grayImage.Height / CellSize;
        int bins = BinSize;
        float[] histogram = new float[width * height * bins];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < CellSize; k++)
                {
                    for (int l = 0; l < CellSize; l++)
                    {
                        int x = i * CellSize + k;
                        int y = j * CellSize + l;
                        double middleBin = orientation[x, y] / 20;
                        int leftBinIndex = (int)Math.Floor(middleBin);
                        int rightBinIndex = (int)Math.Ceiling(middleBin);
                        histogram[leftBinIndex] += (((rightBinIndex + 1) * 20 - orientation[x, y]) / 20) * magnitude[x, y];
                        if (rightBinIndex >= 9)
                            continue;

                        histogram[rightBinIndex] += ((orientation[x, y] - (leftBinIndex + 1) * 20) / 20) * magnitude[x, y];
                    }
                }
            }
        }

        // Normalize the gradients in 16x16 cells
        int blockWidth = BlockSize / CellSize;
        int blockHeight = BlockSize / CellSize;
        int numBlocksX = width - blockWidth + 1;
        int numBlocksY = height - blockHeight + 1;
        float[] features = new float[numBlocksX * numBlocksY * blockWidth * blockHeight * bins];
        int index = 0;
        for (int i = 0; i < numBlocksX; i++)
        {
            for (int j = 0; j < numBlocksY; j++)
            {
                float blockSum = 0;
                for (int k = i; k < i + blockWidth; k++)
                {
                    for (int l = j; l < j + blockHeight; l++)
                    {
                        for (int m = 0; m < bins; m++)
                        {
                            blockSum += histogram[(k * height + l) * bins + m] * histogram[(k * height + l) * bins + m];
                        }
                    }
                }
                float blockNorm = (float)Math.Sqrt(blockSum + 0.001f);
                for (int k = i; k < i + blockWidth; k++)
                {
                    for (int l = j; l < j + blockHeight; l++)
                    {
                        for (int m = 0; m < bins; m++)
                        {
                            features[index++] = histogram[(k * height + l) * bins + m] / blockNorm;
                        }
                    }
                }
            }
        }

        return features;
    }

    private static Bitmap Grayscale(Bitmap image)
    {
        Bitmap grayImage = new(image.Width, image.Height);
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                Color color = image.GetPixel(i, j);
                int gray = (int)(color.R * 0.3f + color.G * 0.59f + color.B * 0.11f);
                grayImage.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
            }
        }
        return grayImage;
    }


    private static float[,] GradientX(Bitmap image)
    {
        float[,] dx = new float[image.Width, image.Height];
        for (int i = 1; i < image.Width - 1; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                dx[i, j] = (image.GetPixel(i + 1, j).R - image.GetPixel(i - 1, j).R) / 2.0f;
            }
        }
        return dx;
    }

    private static float[,] GradientY(Bitmap image)
    {
        float[,] dy = new float[image.Width, image.Height];
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 1; j < image.Height - 1; j++)
            {
                dy[i, j] = (image.GetPixel(i, j + 1).R - image.GetPixel(i, j - 1).R) / 2.0f;
            }
        }
        return dy;
    }

    private static float[,] Magnitude(float[,] dx, float[,] dy)
    {
        float[,] magnitude = new float[dx.GetLength(0), dx.GetLength(1)];
        for (int i = 0; i < dx.GetLength(0); i++)
        {
            for (int j = 0; j < dx.GetLength(1); j++)
            {
                magnitude[i, j] = (float)Math.Sqrt(dx[i, j] * dx[i, j] + dy[i, j] * dy[i, j]);
            }
        }
        return magnitude;
    }

    private static float[,] Orientation(float[,] dx, float[,] dy)
    {
        float[,] orientation = new float[dx.GetLength(0), dx.GetLength(1)];
        for (int i = 0; i < dx.GetLength(0); i++)
        {
            for (int j = 0; j < dx.GetLength(1); j++)
            {
                orientation[i, j] = (float)((Math.Atan2(dy[i, j], dx[i, j]) * 180 / Math.PI + 180) % 360);
            }
        }
        return orientation;
    }
}
