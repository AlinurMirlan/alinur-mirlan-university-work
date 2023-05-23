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

public class LdSvmTrainer : TrainerBase<LdSvmModelParameters>
{
    public LdSvmTrainer(int treeDepth, string modelPath) : base(modelPath)
    {
        Name = $"LD-SVM with {treeDepth} tree depth";
        _model = MlContext.BinaryClassification.Trainers.LdSvm(labelColumnName: "Label",
                                     treeDepth: treeDepth);
    }
}
