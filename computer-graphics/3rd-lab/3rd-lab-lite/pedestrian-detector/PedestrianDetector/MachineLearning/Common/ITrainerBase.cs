using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedestrianDetector.MachineLearning.Common;
public interface ITrainerBase
{
    string Name { get; }
    void Fit(string trainingFileName);
    void Save();
}
