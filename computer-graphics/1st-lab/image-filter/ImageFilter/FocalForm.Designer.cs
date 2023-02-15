namespace ImageFilter
{
    partial class FocalForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSaveSecond = new System.Windows.Forms.Button();
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.targetPictureBox = new System.Windows.Forms.PictureBox();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonColorSpaceSwitcher = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSource = new System.Windows.Forms.Button();
            this.buttonSourceSwitcher = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTarget = new System.Windows.Forms.Button();
            this.buttonTargetSwitcher = new System.Windows.Forms.Button();
            this.checkBoxPreserveContrast = new System.Windows.Forms.CheckBox();
            this.resultPictureBoxTwo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLabelOne = new System.Windows.Forms.Button();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLabelTwo = new System.Windows.Forms.Button();
            this.buttonConvertAll = new System.Windows.Forms.Button();
            this.openSourceImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.openTargetImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBoxTwo)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.buttonSaveSecond, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.sourcePictureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.targetPictureBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.resultPictureBox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonColorSpaceSwitcher, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxPreserveContrast, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.resultPictureBoxTwo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1138, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonSaveSecond
            // 
            this.buttonSaveSecond.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.buttonSaveSecond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSaveSecond.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSaveSecond.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSaveSecond.Location = new System.Drawing.Point(862, 546);
            this.buttonSaveSecond.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.buttonSaveSecond.Name = "buttonSaveSecond";
            this.buttonSaveSecond.Size = new System.Drawing.Size(266, 48);
            this.buttonSaveSecond.TabIndex = 14;
            this.buttonSaveSecond.Text = "Save";
            this.buttonSaveSecond.UseVisualStyleBackColor = false;
            this.buttonSaveSecond.Click += new System.EventHandler(this.buttonSaveSecond_Click);
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.sourcePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourcePictureBox.Location = new System.Drawing.Point(15, 75);
            this.sourcePictureBox.Margin = new System.Windows.Forms.Padding(15);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(254, 450);
            this.sourcePictureBox.TabIndex = 0;
            this.sourcePictureBox.TabStop = false;
            // 
            // targetPictureBox
            // 
            this.targetPictureBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.targetPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetPictureBox.Location = new System.Drawing.Point(299, 75);
            this.targetPictureBox.Margin = new System.Windows.Forms.Padding(15);
            this.targetPictureBox.Name = "targetPictureBox";
            this.targetPictureBox.Size = new System.Drawing.Size(254, 450);
            this.targetPictureBox.TabIndex = 1;
            this.targetPictureBox.TabStop = false;
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.resultPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPictureBox.Location = new System.Drawing.Point(583, 75);
            this.resultPictureBox.Margin = new System.Windows.Forms.Padding(15);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(254, 450);
            this.resultPictureBox.TabIndex = 2;
            this.resultPictureBox.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSave.Location = new System.Drawing.Point(578, 546);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(264, 48);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonColorSpaceSwitcher
            // 
            this.buttonColorSpaceSwitcher.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonColorSpaceSwitcher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonColorSpaceSwitcher.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonColorSpaceSwitcher.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonColorSpaceSwitcher.Location = new System.Drawing.Point(10, 6);
            this.buttonColorSpaceSwitcher.Margin = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.buttonColorSpaceSwitcher.Name = "buttonColorSpaceSwitcher";
            this.buttonColorSpaceSwitcher.Size = new System.Drawing.Size(264, 48);
            this.buttonColorSpaceSwitcher.TabIndex = 6;
            this.buttonColorSpaceSwitcher.Text = "Switch Color Space";
            this.buttonColorSpaceSwitcher.UseVisualStyleBackColor = false;
            this.buttonColorSpaceSwitcher.Click += new System.EventHandler(this.buttonColorSpaceSwitcher_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.buttonSource, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSourceSwitcher, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 543);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(278, 54);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // buttonSource
            // 
            this.buttonSource.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSource.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSource.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSource.Location = new System.Drawing.Point(3, 3);
            this.buttonSource.Name = "buttonSource";
            this.buttonSource.Size = new System.Drawing.Size(216, 48);
            this.buttonSource.TabIndex = 0;
            this.buttonSource.Text = "Choose Source";
            this.buttonSource.UseVisualStyleBackColor = false;
            this.buttonSource.Click += new System.EventHandler(this.buttonSource_Click);
            // 
            // buttonSourceSwitcher
            // 
            this.buttonSourceSwitcher.BackColor = System.Drawing.Color.LightSeaGreen;
            this.buttonSourceSwitcher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSourceSwitcher.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSourceSwitcher.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSourceSwitcher.Location = new System.Drawing.Point(225, 3);
            this.buttonSourceSwitcher.Name = "buttonSourceSwitcher";
            this.buttonSourceSwitcher.Size = new System.Drawing.Size(50, 48);
            this.buttonSourceSwitcher.TabIndex = 1;
            this.buttonSourceSwitcher.Text = "LAB";
            this.buttonSourceSwitcher.UseVisualStyleBackColor = false;
            this.buttonSourceSwitcher.Click += new System.EventHandler(this.buttonSourceSwitcher_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.buttonTarget, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonTargetSwitcher, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(287, 543);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(278, 54);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // buttonTarget
            // 
            this.buttonTarget.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTarget.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonTarget.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonTarget.Location = new System.Drawing.Point(3, 3);
            this.buttonTarget.Name = "buttonTarget";
            this.buttonTarget.Size = new System.Drawing.Size(216, 48);
            this.buttonTarget.TabIndex = 0;
            this.buttonTarget.Text = "Choose Target";
            this.buttonTarget.UseVisualStyleBackColor = false;
            this.buttonTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // buttonTargetSwitcher
            // 
            this.buttonTargetSwitcher.BackColor = System.Drawing.Color.LightSeaGreen;
            this.buttonTargetSwitcher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTargetSwitcher.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonTargetSwitcher.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonTargetSwitcher.Location = new System.Drawing.Point(225, 3);
            this.buttonTargetSwitcher.Name = "buttonTargetSwitcher";
            this.buttonTargetSwitcher.Size = new System.Drawing.Size(50, 48);
            this.buttonTargetSwitcher.TabIndex = 1;
            this.buttonTargetSwitcher.Text = "LAB";
            this.buttonTargetSwitcher.UseVisualStyleBackColor = false;
            this.buttonTargetSwitcher.Click += new System.EventHandler(this.buttonTargetSwitcher_Click);
            // 
            // checkBoxPreserveContrast
            // 
            this.checkBoxPreserveContrast.AutoSize = true;
            this.checkBoxPreserveContrast.Checked = true;
            this.checkBoxPreserveContrast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPreserveContrast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxPreserveContrast.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxPreserveContrast.Location = new System.Drawing.Point(304, 3);
            this.checkBoxPreserveContrast.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.checkBoxPreserveContrast.Name = "checkBoxPreserveContrast";
            this.checkBoxPreserveContrast.Size = new System.Drawing.Size(244, 54);
            this.checkBoxPreserveContrast.TabIndex = 11;
            this.checkBoxPreserveContrast.Text = "Preserve Contrast";
            this.checkBoxPreserveContrast.UseVisualStyleBackColor = true;
            this.checkBoxPreserveContrast.CheckedChanged += new System.EventHandler(this.checkBoxPreserveContrast_CheckedChanged);
            // 
            // resultPictureBoxTwo
            // 
            this.resultPictureBoxTwo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.resultPictureBoxTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPictureBoxTwo.Location = new System.Drawing.Point(867, 75);
            this.resultPictureBoxTwo.Margin = new System.Windows.Forms.Padding(15);
            this.resultPictureBoxTwo.Name = "resultPictureBoxTwo";
            this.resultPictureBoxTwo.Size = new System.Drawing.Size(256, 450);
            this.resultPictureBoxTwo.TabIndex = 12;
            this.resultPictureBoxTwo.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.buttonLabelOne, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonConvert, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(571, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(278, 54);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // buttonLabelOne
            // 
            this.buttonLabelOne.BackColor = System.Drawing.Color.Honeydew;
            this.buttonLabelOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLabelOne.Enabled = false;
            this.buttonLabelOne.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonLabelOne.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonLabelOne.Location = new System.Drawing.Point(225, 3);
            this.buttonLabelOne.Name = "buttonLabelOne";
            this.buttonLabelOne.Size = new System.Drawing.Size(50, 48);
            this.buttonLabelOne.TabIndex = 15;
            this.buttonLabelOne.Text = "LAB";
            this.buttonLabelOne.UseVisualStyleBackColor = false;
            // 
            // buttonConvert
            // 
            this.buttonConvert.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonConvert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConvert.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonConvert.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonConvert.Location = new System.Drawing.Point(3, 3);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(216, 48);
            this.buttonConvert.TabIndex = 14;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = false;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Controls.Add(this.buttonLabelTwo, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonConvertAll, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(855, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(280, 54);
            this.tableLayoutPanel5.TabIndex = 16;
            // 
            // buttonLabelTwo
            // 
            this.buttonLabelTwo.BackColor = System.Drawing.Color.Honeydew;
            this.buttonLabelTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLabelTwo.Enabled = false;
            this.buttonLabelTwo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonLabelTwo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonLabelTwo.Location = new System.Drawing.Point(227, 3);
            this.buttonLabelTwo.Name = "buttonLabelTwo";
            this.buttonLabelTwo.Size = new System.Drawing.Size(50, 48);
            this.buttonLabelTwo.TabIndex = 16;
            this.buttonLabelTwo.Text = "HSL";
            this.buttonLabelTwo.UseVisualStyleBackColor = false;
            // 
            // buttonConvertAll
            // 
            this.buttonConvertAll.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonConvertAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConvertAll.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonConvertAll.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonConvertAll.Location = new System.Drawing.Point(3, 3);
            this.buttonConvertAll.Name = "buttonConvertAll";
            this.buttonConvertAll.Size = new System.Drawing.Size(218, 48);
            this.buttonConvertAll.TabIndex = 14;
            this.buttonConvertAll.Text = "Convert Both";
            this.buttonConvertAll.UseVisualStyleBackColor = false;
            this.buttonConvertAll.Click += new System.EventHandler(this.buttonConvertAll_Click);
            // 
            // openSourceImageDialog
            // 
            this.openSourceImageDialog.FileName = "openFileDialog1";
            this.openSourceImageDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            // 
            // openTargetImageDialog
            // 
            this.openTargetImageDialog.FileName = "openFileDialog1";
            this.openTargetImageDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // FocalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1138, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FocalForm";
            this.Text = "Image Merger";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBoxTwo)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox sourcePictureBox;
        private PictureBox targetPictureBox;
        private PictureBox resultPictureBox;
        private Button buttonSave;
        private OpenFileDialog openSourceImageDialog;
        private OpenFileDialog openTargetImageDialog;
        private Button buttonColorSpaceSwitcher;
        private TableLayoutPanel tableLayoutPanel2;
        private Button buttonSource;
        private Button buttonSourceSwitcher;
        private TableLayoutPanel tableLayoutPanel3;
        private Button buttonTarget;
        private Button buttonTargetSwitcher;
        private CheckBox checkBoxPreserveContrast;
        private SaveFileDialog saveFileDialog;
        private Button buttonSaveSecond;
        private PictureBox resultPictureBoxTwo;
        private TableLayoutPanel tableLayoutPanel4;
        private Button buttonConvert;
        private Button buttonLabelOne;
        private TableLayoutPanel tableLayoutPanel5;
        private Button buttonLabelTwo;
        private Button buttonConvertAll;
    }
}