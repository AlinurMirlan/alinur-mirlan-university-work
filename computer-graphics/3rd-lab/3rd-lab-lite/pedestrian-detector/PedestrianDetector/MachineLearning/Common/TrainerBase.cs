using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PedestrianDetector.MachineLearning.DataModels;

namespace PedestrianDetector.MachineLearning.Common;

/// <summary>
/// Base class for Trainers.
/// This class exposes methods for training, evaluating and saving ML Models.
/// Classes that inherit this class need to assing concrete model and name.
/// </summary>
public abstract class TrainerBase<TParameters> : ITrainerBase
    where TParameters : class
{
    public string Name { get; protected set; } = "Unspecified";

    protected string ModelPath { get; }

    protected readonly MLContext MlContext;

    protected IDataView? _trainData;
    protected ITrainerEstimator<BinaryPredictionTransformer<TParameters>, TParameters>? _model;
    protected ITransformer? _trainedModel;

    protected TrainerBase(string modelPath)
    {
        ModelPath = modelPath;
        MlContext = new MLContext(111);
    }

    /// <summary>
    /// Train model on defined data.
    /// </summary>
    /// <param name="trainingFileName"></param>
    public void Fit(string trainingFileName)
    {
        if (!File.Exists(trainingFileName))
        {
            throw new FileNotFoundException($"File {trainingFileName} doesn't exist.");
        }

        _trainData = LoadAndPrepareData(trainingFileName);
        var dataProcessPipeline = BuildDataProcessingPipeline();
        var trainingPipeline = dataProcessPipeline.Append(_model);

        _trainedModel = trainingPipeline.Fit(_trainData);
    }

    /// <summary>
    /// Save Model in the file.
    /// </summary>
    public void Save()
    {
        if (_trainData is null)
            throw new InvalidOperationException("Train data is not set.");

        MlContext.Model.Save(_trainedModel, _trainData.Schema, ModelPath);
    }

    /// <summary>
    /// Feature engeneering and data pre-processing.
    /// </summary>
    /// <returns>Data Processing Pipeline.</returns>
    private EstimatorChain<NormalizingTransformer> BuildDataProcessingPipeline()
    {
        var dataProcessPipeline = MlContext.Transforms
           .NormalizeMinMax("Features")
           .AppendCacheCheckpoint(MlContext);

        return dataProcessPipeline;
    }

    private IDataView LoadAndPrepareData(string trainingFileName)
    {
        var trainingDataView = MlContext.Data
                                .LoadFromTextFile<HogFeaturesData>
                                  (trainingFileName, hasHeader: false, separatorChar: ',');
        return trainingDataView;
    }
}
