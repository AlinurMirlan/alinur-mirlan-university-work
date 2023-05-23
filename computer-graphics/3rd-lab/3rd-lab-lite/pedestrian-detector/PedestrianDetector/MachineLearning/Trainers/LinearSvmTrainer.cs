using Microsoft.ML.Trainers;
using Microsoft.ML;
using PedestrianDetector.MachineLearning.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PedestrianDetector.MachineLearning.Trainers;

public class LinearSvmTrainer : TrainerBase<LinearBinaryModelParameters>
{
    public LinearSvmTrainer(string modelPath) : base(modelPath)
    {
        Name = "Linear SVM";
        _model = MlContext.BinaryClassification.Trainers.LinearSvm(labelColumnName: "Label");
    }
}