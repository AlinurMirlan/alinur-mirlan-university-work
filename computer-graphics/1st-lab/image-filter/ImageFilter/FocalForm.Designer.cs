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
            this.sourcePictureBox = new System.Windows.Forms.PictureBox();
            this.targetPictureBox = new System.Windows.Forms.PictureBox();
            this.resultPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonSource = new System.Windows.Forms.Button();
            this.buttonTarget = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.openSourceImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.openTargetImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.sourcePictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.targetPictureBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.resultPictureBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSource, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonTarget, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 560);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // sourcePictureBox
            // 
            this.sourcePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourcePictureBox.Location = new System.Drawing.Point(15, 15);
            this.sourcePictureBox.Margin = new System.Windows.Forms.Padding(15);
            this.sourcePictureBox.Name = "sourcePictureBox";
            this.sourcePictureBox.Size = new System.Drawing.Size(258, 446);
            this.sourcePictureBox.TabIndex = 0;
            this.sourcePictureBox.TabStop = false;
            // 
            // targetPictureBox
            // 
            this.targetPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetPictureBox.Location = new System.Drawing.Point(303, 15);
            this.targetPictureBox.Margin = new System.Windows.Forms.Padding(15);
            this.targetPictureBox.Name = "targetPictureBox";
            this.targetPictureBox.Size = new System.Drawing.Size(258, 446);
            this.targetPictureBox.TabIndex = 1;
            this.targetPictureBox.TabStop = false;
            // 
            // resultPictureBox
            // 
            this.resultPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPictureBox.Location = new System.Drawing.Point(591, 15);
            this.resultPictureBox.Margin = new System.Windows.Forms.Padding(15);
            this.resultPictureBox.Name = "resultPictureBox";
            this.resultPictureBox.Size = new System.Drawing.Size(258, 446);
            this.resultPictureBox.TabIndex = 2;
            this.resultPictureBox.TabStop = false;
            // 
            // buttonSource
            // 
            this.buttonSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSource.Location = new System.Drawing.Point(20, 496);
            this.buttonSource.Margin = new System.Windows.Forms.Padding(20);
            this.buttonSource.Name = "buttonSource";
            this.buttonSource.Size = new System.Drawing.Size(248, 44);
            this.buttonSource.TabIndex = 3;
            this.buttonSource.Text = "Choose Source";
            this.buttonSource.UseVisualStyleBackColor = true;
            this.buttonSource.Click += new System.EventHandler(this.buttonSource_Click);
            // 
            // buttonTarget
            // 
            this.buttonTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTarget.Location = new System.Drawing.Point(308, 496);
            this.buttonTarget.Margin = new System.Windows.Forms.Padding(20);
            this.buttonTarget.Name = "buttonTarget";
            this.buttonTarget.Size = new System.Drawing.Size(248, 44);
            this.buttonTarget.TabIndex = 4;
            this.buttonTarget.Text = "Choose Target";
            this.buttonTarget.UseVisualStyleBackColor = true;
            this.buttonTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.Location = new System.Drawing.Point(596, 496);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(20);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(248, 44);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            // FocalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 560);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FocalForm";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sourcePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox sourcePictureBox;
        private PictureBox targetPictureBox;
        private PictureBox resultPictureBox;
        private Button buttonSource;
        private Button buttonTarget;
        private Button buttonSave;
        private OpenFileDialog openSourceImageDialog;
        private OpenFileDialog openTargetImageDialog;
    }
}