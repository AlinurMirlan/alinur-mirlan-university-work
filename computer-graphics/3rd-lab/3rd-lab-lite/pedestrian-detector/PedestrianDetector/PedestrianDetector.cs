using PedestrianDetector.MachineLearning.DataModels;
using PedestrianDetector.MachineLearning.Predictors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector;
public class PedestrianDetector
{
    private const int OverlapThreshold = 16;

    public string ClassifierModelPath { get; set; }
    public string? ImagesDirectory { get; set; }

    public PedestrianDetector(string modelPath, string? imagesDirectory = null)
    {
        ClassifierModelPath = modelPath;
        ImagesDirectory = imagesDirectory;
    }

    public Image GetDetectedImage(Image image)
    {
        Predictor predictor = new(ClassifierModelPath);
        LinkedList<int> boundingBoxes = new();
        LinkedList<int> finalBoxes = new();
        for (int i = 0; i < image.Width - Patch.Width; i += 4)
        {
            using Image patch = image.Crop(i, Patch.TopY, i + Patch.Width, Patch.BottomY);
            HogFeaturesData feautres = HogDescriptor.GetFeatures(patch, true);
            PedestrianPredictionData prediction = predictor.Predict(feautres);
            if (!prediction.PredictedLabel)
                continue;

            var lastBox = boundingBoxes.Last;
            boundingBoxes.AddLast(i);
            if (lastBox is not null && i - lastBox.Value <= OverlapThreshold)
                continue;

            finalBoxes.AddLast(i);
        }

        if (boundingBoxes.Count == 0)
            return image;

        using Graphics graphics = Graphics.FromImage(image);
        Pen pen = new(Color.LightGreen, 2);
        foreach (int leftX in finalBoxes)
        {
            graphics.DrawRectangle(pen, leftX, Patch.TopY, Patch.Width, Patch.Height);
        }

        return image;
    }

    public void ProduceLocaitons(string locationPath)
    {
        if (ImagesDirectory is null)
            throw new InvalidOperationException("Images are not set.");

        using StreamWriter writer = new(locationPath);
        Predictor predictor = new(ClassifierModelPath);
        foreach (string filePath in Directory.EnumerateFiles(ImagesDirectory))
        {
            if (Path.GetExtension(filePath) != ".png")
                continue;

            using Image image = Image.FromFile(filePath);
            LinkedList<int> boundingBoxes = new();
            for (int i = 0; i < image.Width - Patch.Width; i += 4)
            {
                using Image patch = image.Crop(i, Patch.TopY, i + Patch.Width, Patch.BottomY);
                HogFeaturesData feautres = HogDescriptor.GetFeatures(patch, true);
                PedestrianPredictionData prediction = predictor.Predict(feautres);
                if (!prediction.PredictedLabel)
                    continue;

                var lastBox = boundingBoxes.Last;
                boundingBoxes.AddLast(i);
                if (lastBox is not null && i - lastBox.Value <= OverlapThreshold)
                    continue;

                string fileName = Path.GetFileNameWithoutExtension(filePath);
                writer.WriteLine($"{fileName}\t{Patch.TopY}\t{i}\t{Patch.BottomY}\t{i + Patch.Width}");
            }
        }
    }
    

    public ClassifierStatistics ProduceLocaitons(string locationPath, string correctLocationsPath)
    {
        if (ImagesDirectory is null)
            throw new InvalidOperationException("Images are not set.");
        using StreamWriter writer = new(locationPath);
        Dictionary<string, LinkedList<int>> correctLeftXs = GetCorrectLocationsDictionary(correctLocationsPath);
        int correctPredictions = 0;
        int totalPredictions = 0;
        Predictor predictor = new(ClassifierModelPath);
        foreach (string filePath in Directory.EnumerateFiles(ImagesDirectory))
        {
            if (Path.GetExtension(filePath) != ".png")
                continue;

            using Image image = Image.FromFile(filePath);
            LinkedList<int> boundingBoxes = new();
            for (int i = 0; i < image.Width - Patch.Width; i += 4)
            {
                using Image patch = image.Crop(i, Patch.TopY, i + Patch.Width, Patch.BottomY);
                HogFeaturesData feautres = HogDescriptor.GetFeatures(patch, true);
                PedestrianPredictionData prediction = predictor.Predict(feautres);
                if (!prediction.PredictedLabel)
                    continue;

                var lastBox = boundingBoxes.Last;
                boundingBoxes.AddLast(i);
                if (lastBox is not null && i - lastBox.Value <= OverlapThreshold)
                    continue;

                totalPredictions++;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (correctLeftXs.TryGetValue(fileName, out LinkedList<int>? coordinates))
                {
                    foreach (int leftX in coordinates)
                    {
                        if (Math.Abs(leftX - i) <= Patch.Width / 2)
                        {
                            correctPredictions++;
                            break;
                        }
                    }
                }

                writer.WriteLine($"{fileName}\t{Patch.TopY}\t{i}\t{Patch.BottomY}\t{i + Patch.Width}");
            }
        }

        return new ClassifierStatistics(
            Recall: (double)correctPredictions / correctLeftXs.Count,
            Precision: (double)correctPredictions / totalPredictions);
    }

    private static Dictionary<string, LinkedList<int>> GetCorrectLocationsDictionary(string correctLocationsPath)
    {
        Dictionary<string, LinkedList<int>> dictionary = new();
        using StreamReader reader = new(correctLocationsPath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine()!;
            string[] tokens = line.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            string fileName = tokens[0];
            int topLeftX = int.Parse(tokens[2]);
            if (!dictionary.ContainsKey(fileName))
                dictionary[fileName] = new LinkedList<int>();

            dictionary[fileName].AddLast(topLeftX);
        }

        return dictionary;
    }
}

public record ClassifierStatistics(double Recall, double Precision);

