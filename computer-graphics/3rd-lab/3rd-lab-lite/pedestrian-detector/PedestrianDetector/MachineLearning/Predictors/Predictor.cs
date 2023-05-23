using Microsoft.ML;
using PedestrianDetector.MachineLearning.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector.MachineLearning.Predictors;

public class Predictor
{
    protected string ModelPath { get; set; }
    private readonly MLContext _mlContext;

    private ITransformer? _model;

    public Predictor(string modelPath)
    {
        ModelPath = modelPath;
        _mlContext = new MLContext(111);
    }

    /// <summary>
    /// Runs prediction on new data.
    /// </summary>
    /// <param name="newSample">New data sample.</param>
    /// <returns>An object which contains predictions made by model.</returns>
    public PedestrianPredictionData Predict(HogFeaturesData newSample)
    {
        LoadModel();

        var predictionEngine = _mlContext.Model.
            CreatePredictionEngine<HogFeaturesData, PedestrianPredictionData>(_model);

        return predictionEngine.Predict(newSample);
    }

    private void LoadModel()
    {
        if (!File.Exists(ModelPath))
        {
            throw new FileNotFoundException($"File {ModelPath} doesn't exist.");
        }

        using (var stream = new FileStream(ModelPath,
                                     FileMode.Open,
                                 FileAccess.Read,
                                 FileShare.Read))
        {
            _model = _mlContext.Model.Load(stream, out _);
        }

        if (_model == null)
        {
            throw new InvalidOperationException($"Failed to load Model");
        }
    }
}
