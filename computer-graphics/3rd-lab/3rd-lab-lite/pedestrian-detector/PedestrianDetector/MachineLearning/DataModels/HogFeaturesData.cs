using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector.MachineLearning.DataModels;
public class HogFeaturesData
{
    [LoadColumn(0)]
    public bool Label { get; set; }

    [LoadColumn(1, 3780)]
    [VectorType(3780)]
    [ColumnName("Features")]
    public float[] FeatureVector { get; set; } = new float[3780];
}
