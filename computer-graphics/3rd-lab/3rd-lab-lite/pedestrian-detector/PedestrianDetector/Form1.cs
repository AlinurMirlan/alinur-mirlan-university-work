using Microsoft.ML.Data;
using PedestrianDetector.MachineLearning.Trainers;

namespace PedestrianDetector
{
    public partial class Form1 : Form
    {
        private string? trainingImagesDirectory;
        private string? trainingLocationsFile;
        private string? testImagesDirectory;
        private string? classifierModelFile;
        private string? correctLocationsPath;
        private LinearSvmTrainer? trainer;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTrainDir_Click(object sender, EventArgs e)
        {
            if (folderTrainImgs.ShowDialog() == DialogResult.OK)
            {
                trainingImagesDirectory = folderTrainImgs.SelectedPath;
            }
        }

        private void buttonTrainLocation_Click(object sender, EventArgs e)
        {
            if (openLocationFile.ShowDialog() == DialogResult.OK)
            {
                trainingLocationsFile = openLocationFile.FileName;
            }
        }

        private void buttonSaveClassifier_Click(object sender, EventArgs e)
        {
            if (trainingImagesDirectory is null || trainingLocationsFile is null)
            {
                return;
            }

            if (saveClassifierDialog.ShowDialog() == DialogResult.OK)
            {
                string classifierFile = saveClassifierDialog.FileName;
                HogDescriptor hogDescriptor = new(trainingImagesDirectory, trainingLocationsFile);
                string trainingFileName = "features.csv";
                hogDescriptor.ProduceFeatures(trainingFileName);

                trainer = new(classifierFile);
                trainer.Fit(trainingFileName);
                trainer.Save();
            }

        }

        private void buttonTestImg_Click(object sender, EventArgs e)
        {
            if (folderTestImgs.ShowDialog() == DialogResult.OK)
            {
                testImagesDirectory = folderTestImgs.SelectedPath;
            }
        }

        private void buttonClassifier_Click(object sender, EventArgs e)
        {
            if (openClassifierFile.ShowDialog() == DialogResult.OK)
            {
                classifierModelFile = openClassifierFile.FileName;
            }
        }

        private void buttonSaveLocation_Click(object sender, EventArgs e)
        {
            if (classifierModelFile is null || testImagesDirectory is null)
            {
                return;
            }

            if (saveLocationsDialog.ShowDialog() == DialogResult.OK)
            {
                string locationsFile = saveLocationsDialog.FileName;
                PedestrianDetector detector = new(classifierModelFile, testImagesDirectory);
                if (correctLocationsPath is null)
                {
                    detector.ProduceLocaitons(locationsFile);
                    return;
                }

                ClassifierStatistics statistics = detector.ProduceLocaitons(locationsFile, correctLocationsPath);
                statisticsGridView[0, 0].Value = statistics.Precision;
                statisticsGridView[1, 0].Value = statistics.Recall;
            }

        }

        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            if (openCorrectLocationFile.ShowDialog() == DialogResult.OK)
            {
                correctLocationsPath = openCorrectLocationFile.FileName;
            }
        }

        private void buttonDetectPedestrian_Click(object sender, EventArgs e)
        {
            if (classifierModelFile is null)
                return;

            if (openImageFile.ShowDialog() == DialogResult.OK)
            {
                string imageFileName = openImageFile.FileName;
                PedestrianDetector detector = new(classifierModelFile);
                Image originalImage = Image.FromFile(imageFileName);
                pictureBoxOriginal.Image = originalImage;
                Image image = detector.GetDetectedImage((Image)originalImage.Clone());
                pictureBoxDetection.Image = image;
            }

        }

        private void buttonChooseModel_Click(object sender, EventArgs e)
        {
            if (openClassifierFile.ShowDialog() == DialogResult.OK)
            {
                classifierModelFile = openClassifierFile.FileName;
            }
        }
    }
}