namespace TreasureSeeker
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.ButtonSeekTreasure = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.ButtonMedianFilter = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ButtonGreyWorldFilter = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxResult, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.47369F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1058, 604);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(10, 10);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(10, 10, 5, 10);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(514, 520);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBoxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxResult.Location = new System.Drawing.Point(534, 10);
            this.pictureBoxResult.Margin = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(514, 520);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult.TabIndex = 2;
            this.pictureBoxResult.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.ButtonGreyWorldFilter, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.ButtonOpen, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ButtonSeekTreasure, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.ButtonSave, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 543);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1052, 58);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.BackColor = System.Drawing.Color.DarkCyan;
            this.ButtonOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonOpen.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonOpen.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonOpen.Location = new System.Drawing.Point(3, 3);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(204, 52);
            this.ButtonOpen.TabIndex = 0;
            this.ButtonOpen.Text = "Open Image";
            this.ButtonOpen.UseVisualStyleBackColor = false;
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // ButtonSeekTreasure
            // 
            this.ButtonSeekTreasure.BackColor = System.Drawing.Color.DarkCyan;
            this.ButtonSeekTreasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSeekTreasure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSeekTreasure.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonSeekTreasure.Location = new System.Drawing.Point(213, 3);
            this.ButtonSeekTreasure.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.ButtonSeekTreasure.Name = "ButtonSeekTreasure";
            this.ButtonSeekTreasure.Size = new System.Drawing.Size(197, 52);
            this.ButtonSeekTreasure.TabIndex = 1;
            this.ButtonSeekTreasure.Text = "Find Treasure";
            this.ButtonSeekTreasure.UseVisualStyleBackColor = false;
            this.ButtonSeekTreasure.Click += new System.EventHandler(this.ButtonSeekTreasure_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel3.Controls.Add(this.textBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.ButtonMedianFilter, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(430, 3);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(197, 52);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // textBox
            // 
            this.textBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox.Location = new System.Drawing.Point(3, 12);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(23, 27);
            this.textBox.TabIndex = 0;
            // 
            // ButtonMedianFilter
            // 
            this.ButtonMedianFilter.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ButtonMedianFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonMedianFilter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonMedianFilter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonMedianFilter.Location = new System.Drawing.Point(32, 3);
            this.ButtonMedianFilter.Name = "ButtonMedianFilter";
            this.ButtonMedianFilter.Size = new System.Drawing.Size(162, 46);
            this.ButtonMedianFilter.TabIndex = 1;
            this.ButtonMedianFilter.Text = "Median Filter";
            this.ButtonMedianFilter.UseVisualStyleBackColor = false;
            this.ButtonMedianFilter.Click += new System.EventHandler(this.ButtonMedianFilter_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ButtonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonSave.Location = new System.Drawing.Point(843, 3);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(206, 52);
            this.ButtonSave.TabIndex = 3;
            this.ButtonSave.Text = "Save Image";
            this.ButtonSave.UseVisualStyleBackColor = false;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All file" +
    "s (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";
            // 
            // ButtonGreyWorldFilter
            // 
            this.ButtonGreyWorldFilter.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ButtonGreyWorldFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonGreyWorldFilter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonGreyWorldFilter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ButtonGreyWorldFilter.Location = new System.Drawing.Point(633, 3);
            this.ButtonGreyWorldFilter.Name = "ButtonGreyWorldFilter";
            this.ButtonGreyWorldFilter.Size = new System.Drawing.Size(204, 52);
            this.ButtonGreyWorldFilter.TabIndex = 4;
            this.ButtonGreyWorldFilter.Text = "Grey World Filter";
            this.ButtonGreyWorldFilter.UseVisualStyleBackColor = false;
            this.ButtonGreyWorldFilter.Click += new System.EventHandler(this.ButtonGreyWorldFilter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 604);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox;
        private PictureBox pictureBoxResult;
        private OpenFileDialog openFileDialog;
        private TableLayoutPanel tableLayoutPanel2;
        private Button ButtonOpen;
        private Button ButtonSeekTreasure;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox textBox;
        private Button ButtonMedianFilter;
        private Button ButtonSave;
        private SaveFileDialog saveFileDialog;
        private Button ButtonGreyWorldFilter;
    }
}