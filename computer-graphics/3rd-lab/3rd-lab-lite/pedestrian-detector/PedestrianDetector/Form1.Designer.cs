namespace PedestrianDetector
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openLocationFile = new OpenFileDialog();
            openClassifierFile = new OpenFileDialog();
            saveTestResults = new SaveFileDialog();
            openCorrectLocationFile = new OpenFileDialog();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            trainingLabel = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            buttonTrainDir = new Button();
            buttonTrainLocation = new Button();
            buttonSaveClassifier = new Button();
            flowLayoutPanel3 = new FlowLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            buttonSaveLocation = new Button();
            flowLayoutPanel4 = new FlowLayoutPanel();
            labelShowStatistics = new Label();
            buttonStatistics = new Button();
            label2 = new Label();
            flowLayoutPanel5 = new FlowLayoutPanel();
            buttonTestImg = new Button();
            buttonClassifier = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            statisticsGridView = new DataGridView();
            Precision = new DataGridViewTextBoxColumn();
            Recall = new DataGridViewTextBoxColumn();
            flowLayoutPanel6 = new FlowLayoutPanel();
            buttonChooseModel = new Button();
            buttonDetectPedestrian = new Button();
            labelDetectPedestrians = new Label();
            pictureBoxOriginal = new PictureBox();
            pictureBoxDetection = new PictureBox();
            folderTrainImgs = new FolderBrowserDialog();
            folderTestImgs = new FolderBrowserDialog();
            saveClassifierDialog = new SaveFileDialog();
            saveLocationsDialog = new SaveFileDialog();
            openImageFile = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)statisticsGridView).BeginInit();
            flowLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDetection).BeginInit();
            SuspendLayout();
            // 
            // openLocationFile
            // 
            openLocationFile.FileName = "openFileDialog1";
            // 
            // openClassifierFile
            // 
            openClassifierFile.Filter = "Zip files (*.zip)|*.zip";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBoxOriginal, 1, 0);
            tableLayoutPanel1.Controls.Add(pictureBoxDetection, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(876, 505);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(tableLayoutPanel4);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel1.Controls.Add(tableLayoutPanel2);
            flowLayoutPanel1.Controls.Add(tableLayoutPanel3);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            tableLayoutPanel1.SetRowSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Size = new Size(432, 499);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Controls.Add(trainingLabel, 0, 0);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(432, 88);
            tableLayoutPanel4.TabIndex = 14;
            // 
            // trainingLabel
            // 
            trainingLabel.AutoSize = true;
            trainingLabel.BackColor = Color.DarkSlateGray;
            trainingLabel.Dock = DockStyle.Fill;
            trainingLabel.Font = new Font("BIZ UDPGothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            trainingLabel.ForeColor = Color.White;
            trainingLabel.Location = new Point(3, 0);
            trainingLabel.Name = "trainingLabel";
            trainingLabel.Padding = new Padding(10, 6, 10, 5);
            trainingLabel.Size = new Size(426, 28);
            trainingLabel.TabIndex = 9;
            trainingLabel.Text = "Training";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(buttonTrainDir);
            flowLayoutPanel2.Controls.Add(buttonTrainLocation);
            flowLayoutPanel2.Controls.Add(buttonSaveClassifier);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(3, 31);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(426, 54);
            flowLayoutPanel2.TabIndex = 3;
            // 
            // buttonTrainDir
            // 
            buttonTrainDir.BackColor = Color.DarkCyan;
            buttonTrainDir.Dock = DockStyle.Fill;
            buttonTrainDir.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonTrainDir.ForeColor = Color.White;
            buttonTrainDir.Location = new Point(3, 3);
            buttonTrainDir.Name = "buttonTrainDir";
            buttonTrainDir.Size = new Size(143, 40);
            buttonTrainDir.TabIndex = 0;
            buttonTrainDir.Text = "images directory";
            buttonTrainDir.UseVisualStyleBackColor = false;
            buttonTrainDir.Click += buttonTrainDir_Click;
            // 
            // buttonTrainLocation
            // 
            buttonTrainLocation.BackColor = Color.DarkCyan;
            buttonTrainLocation.Dock = DockStyle.Fill;
            buttonTrainLocation.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonTrainLocation.ForeColor = Color.White;
            buttonTrainLocation.Location = new Point(152, 3);
            buttonTrainLocation.Name = "buttonTrainLocation";
            buttonTrainLocation.Size = new Size(119, 40);
            buttonTrainLocation.TabIndex = 1;
            buttonTrainLocation.Text = "locations file";
            buttonTrainLocation.UseVisualStyleBackColor = false;
            buttonTrainLocation.Click += buttonTrainLocation_Click;
            // 
            // buttonSaveClassifier
            // 
            buttonSaveClassifier.BackColor = Color.MediumSeaGreen;
            buttonSaveClassifier.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonSaveClassifier.ForeColor = Color.White;
            buttonSaveClassifier.Location = new Point(277, 3);
            buttonSaveClassifier.Name = "buttonSaveClassifier";
            buttonSaveClassifier.Size = new Size(146, 40);
            buttonSaveClassifier.TabIndex = 2;
            buttonSaveClassifier.Text = "save classifier";
            buttonSaveClassifier.UseVisualStyleBackColor = false;
            buttonSaveClassifier.Click += buttonSaveClassifier_Click;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Location = new Point(3, 97);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(0, 0);
            flowLayoutPanel3.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(buttonSaveLocation, 0, 3);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel4, 0, 2);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel5, 0, 1);
            tableLayoutPanel2.Location = new Point(9, 97);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.Size = new Size(406, 171);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // buttonSaveLocation
            // 
            buttonSaveLocation.BackColor = Color.MediumSeaGreen;
            buttonSaveLocation.Dock = DockStyle.Fill;
            buttonSaveLocation.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonSaveLocation.ForeColor = Color.White;
            buttonSaveLocation.Location = new Point(3, 123);
            buttonSaveLocation.Name = "buttonSaveLocation";
            buttonSaveLocation.Size = new Size(400, 45);
            buttonSaveLocation.TabIndex = 2;
            buttonSaveLocation.Text = "save locaitons";
            buttonSaveLocation.UseVisualStyleBackColor = false;
            buttonSaveLocation.Click += buttonSaveLocation_Click;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.Controls.Add(labelShowStatistics);
            flowLayoutPanel4.Controls.Add(buttonStatistics);
            flowLayoutPanel4.Dock = DockStyle.Fill;
            flowLayoutPanel4.Location = new Point(3, 77);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(400, 40);
            flowLayoutPanel4.TabIndex = 5;
            // 
            // labelShowStatistics
            // 
            labelShowStatistics.AutoSize = true;
            labelShowStatistics.BackColor = Color.DarkSlateGray;
            labelShowStatistics.Font = new Font("BIZ UDPGothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelShowStatistics.ForeColor = Color.White;
            labelShowStatistics.Location = new Point(3, 0);
            labelShowStatistics.Name = "labelShowStatistics";
            labelShowStatistics.Padding = new Padding(10, 5, 10, 5);
            labelShowStatistics.Size = new Size(156, 25);
            labelShowStatistics.TabIndex = 3;
            labelShowStatistics.Text = "Show statistics";
            // 
            // buttonStatistics
            // 
            buttonStatistics.BackColor = Color.DarkCyan;
            buttonStatistics.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStatistics.ForeColor = Color.White;
            buttonStatistics.Location = new Point(165, 3);
            buttonStatistics.Name = "buttonStatistics";
            buttonStatistics.Size = new Size(130, 40);
            buttonStatistics.TabIndex = 4;
            buttonStatistics.Text = "locations file";
            buttonStatistics.UseVisualStyleBackColor = false;
            buttonStatistics.Click += buttonStatistics_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.DarkSlateGray;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("BIZ UDPGothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Padding = new Padding(10, 6, 10, 5);
            label2.Size = new Size(400, 28);
            label2.TabIndex = 8;
            label2.Text = "Testing";
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.Controls.Add(buttonTestImg);
            flowLayoutPanel5.Controls.Add(buttonClassifier);
            flowLayoutPanel5.Dock = DockStyle.Fill;
            flowLayoutPanel5.Location = new Point(3, 31);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(400, 40);
            flowLayoutPanel5.TabIndex = 6;
            // 
            // buttonTestImg
            // 
            buttonTestImg.BackColor = Color.DarkCyan;
            buttonTestImg.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonTestImg.ForeColor = Color.White;
            buttonTestImg.Location = new Point(3, 3);
            buttonTestImg.Name = "buttonTestImg";
            buttonTestImg.Size = new Size(152, 40);
            buttonTestImg.TabIndex = 0;
            buttonTestImg.Text = "images directory";
            buttonTestImg.UseVisualStyleBackColor = false;
            buttonTestImg.Click += buttonTestImg_Click;
            // 
            // buttonClassifier
            // 
            buttonClassifier.BackColor = Color.DarkCyan;
            buttonClassifier.Dock = DockStyle.Fill;
            buttonClassifier.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonClassifier.ForeColor = Color.White;
            buttonClassifier.Location = new Point(161, 3);
            buttonClassifier.Name = "buttonClassifier";
            buttonClassifier.Size = new Size(119, 40);
            buttonClassifier.TabIndex = 1;
            buttonClassifier.Text = "classifier file";
            buttonClassifier.UseVisualStyleBackColor = false;
            buttonClassifier.Click += buttonClassifier_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(statisticsGridView, 0, 0);
            tableLayoutPanel3.Controls.Add(flowLayoutPanel6, 0, 2);
            tableLayoutPanel3.Controls.Add(labelDetectPedestrians, 0, 1);
            tableLayoutPanel3.Location = new Point(3, 274);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel3.Size = new Size(310, 237);
            tableLayoutPanel3.TabIndex = 13;
            // 
            // statisticsGridView
            // 
            statisticsGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            statisticsGridView.BackgroundColor = SystemColors.ActiveCaption;
            statisticsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            statisticsGridView.Columns.AddRange(new DataGridViewColumn[] { Precision, Recall });
            statisticsGridView.Location = new Point(3, 3);
            statisticsGridView.Name = "statisticsGridView";
            statisticsGridView.RowHeadersWidth = 51;
            statisticsGridView.RowTemplate.Height = 29;
            statisticsGridView.Size = new Size(304, 53);
            statisticsGridView.TabIndex = 11;
            // 
            // Precision
            // 
            Precision.HeaderText = "Precision";
            Precision.MinimumWidth = 6;
            Precision.Name = "Precision";
            Precision.Width = 125;
            // 
            // Recall
            // 
            Recall.HeaderText = "Recall";
            Recall.MinimumWidth = 6;
            Recall.Name = "Recall";
            Recall.Width = 125;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.Controls.Add(buttonChooseModel);
            flowLayoutPanel6.Controls.Add(buttonDetectPedestrian);
            flowLayoutPanel6.Location = new Point(3, 102);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(176, 92);
            flowLayoutPanel6.TabIndex = 12;
            // 
            // buttonChooseModel
            // 
            buttonChooseModel.BackColor = Color.DarkCyan;
            buttonChooseModel.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonChooseModel.ForeColor = Color.White;
            buttonChooseModel.Location = new Point(3, 3);
            buttonChooseModel.Name = "buttonChooseModel";
            buttonChooseModel.Size = new Size(125, 40);
            buttonChooseModel.TabIndex = 13;
            buttonChooseModel.Text = "Choose model";
            buttonChooseModel.UseVisualStyleBackColor = false;
            buttonChooseModel.Click += buttonChooseModel_Click;
            // 
            // buttonDetectPedestrian
            // 
            buttonDetectPedestrian.BackColor = Color.MediumSeaGreen;
            buttonDetectPedestrian.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDetectPedestrian.ForeColor = Color.White;
            buttonDetectPedestrian.Location = new Point(3, 49);
            buttonDetectPedestrian.Name = "buttonDetectPedestrian";
            buttonDetectPedestrian.Size = new Size(170, 40);
            buttonDetectPedestrian.TabIndex = 12;
            buttonDetectPedestrian.Text = "Detect pedestrian";
            buttonDetectPedestrian.UseVisualStyleBackColor = false;
            buttonDetectPedestrian.Click += buttonDetectPedestrian_Click;
            // 
            // labelDetectPedestrians
            // 
            labelDetectPedestrians.AutoSize = true;
            labelDetectPedestrians.BackColor = Color.DarkSlateGray;
            labelDetectPedestrians.Dock = DockStyle.Bottom;
            labelDetectPedestrians.Font = new Font("BIZ UDPGothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelDetectPedestrians.ForeColor = Color.White;
            labelDetectPedestrians.Location = new Point(3, 74);
            labelDetectPedestrians.Name = "labelDetectPedestrians";
            labelDetectPedestrians.Padding = new Padding(10, 5, 10, 5);
            labelDetectPedestrians.Size = new Size(304, 25);
            labelDetectPedestrians.TabIndex = 13;
            labelDetectPedestrians.Text = "Detect pedestrians";
            // 
            // pictureBoxOriginal
            // 
            pictureBoxOriginal.BackColor = SystemColors.InactiveCaption;
            pictureBoxOriginal.Dock = DockStyle.Fill;
            pictureBoxOriginal.Location = new Point(441, 3);
            pictureBoxOriginal.Name = "pictureBoxOriginal";
            pictureBoxOriginal.Size = new Size(432, 246);
            pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxOriginal.TabIndex = 1;
            pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxDetection
            // 
            pictureBoxDetection.BackColor = SystemColors.InactiveCaption;
            pictureBoxDetection.Dock = DockStyle.Fill;
            pictureBoxDetection.Location = new Point(441, 255);
            pictureBoxDetection.Name = "pictureBoxDetection";
            pictureBoxDetection.Size = new Size(432, 247);
            pictureBoxDetection.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxDetection.TabIndex = 2;
            pictureBoxDetection.TabStop = false;
            // 
            // saveClassifierDialog
            // 
            saveClassifierDialog.Filter = "Zip files (*.zip)|*.zip";
            // 
            // saveLocationsDialog
            // 
            saveLocationsDialog.Filter = "Idl files (*.idl)|*.idl";
            // 
            // openImageFile
            // 
            openImageFile.Filter = "Png files (*.png)|*.png";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(876, 505);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)statisticsGridView).EndInit();
            flowLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxDetection).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private OpenFileDialog openLocationFile;
        private OpenFileDialog openClassifierFile;
        private SaveFileDialog saveTestResults;
        private OpenFileDialog openCorrectLocationFile;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button buttonTrainDir;
        private Button buttonTrainLocation;
        private Button buttonSaveClassifier;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel3;
        private Button buttonTestImg;
        private Button buttonClassifier;
        private Button buttonSaveLocation;
        private DataGridView statisticsGridView;
        private DataGridViewTextBoxColumn Precision;
        private DataGridViewTextBoxColumn Recall;
        private FolderBrowserDialog folderTrainImgs;
        private FolderBrowserDialog folderTestImgs;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label labelShowStatistics;
        private Button buttonStatistics;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel5;
        private SaveFileDialog saveClassifierDialog;
        private SaveFileDialog saveLocationsDialog;
        private Button buttonDetectPedestrian;
        private TableLayoutPanel tableLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel6;
        private Button buttonChooseModel;
        private Label labelDetectPedestrians;
        private OpenFileDialog openImageFile;
        private PictureBox pictureBoxOriginal;
        private PictureBox pictureBoxDetection;
        private TableLayoutPanel tableLayoutPanel4;
        private Label trainingLabel;
    }
}