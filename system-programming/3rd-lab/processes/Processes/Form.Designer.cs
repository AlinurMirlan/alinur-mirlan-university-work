namespace Processes
{
    partial class ProcessesTracker
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.processColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processorTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threadCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handleCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminateProcessButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.terminateAllButton = new System.Windows.Forms.Button();
            this.addProcessButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.processColumn,
            this.memoryColumn,
            this.processorTimeColumn,
            this.threadCountColumn,
            this.handleCountColumn,
            this.ProcessId,
            this.terminateProcessButton});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView, 2);
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 64);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(970, 545);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellClick);
            // 
            // processColumn
            // 
            this.processColumn.HeaderText = "Process";
            this.processColumn.MinimumWidth = 6;
            this.processColumn.Name = "processColumn";
            // 
            // memoryColumn
            // 
            this.memoryColumn.HeaderText = "Memory Usage";
            this.memoryColumn.MinimumWidth = 6;
            this.memoryColumn.Name = "memoryColumn";
            // 
            // processorTimeColumn
            // 
            this.processorTimeColumn.HeaderText = "Processor Time";
            this.processorTimeColumn.MinimumWidth = 6;
            this.processorTimeColumn.Name = "processorTimeColumn";
            // 
            // threadCountColumn
            // 
            this.threadCountColumn.HeaderText = "Thread Count";
            this.threadCountColumn.MinimumWidth = 6;
            this.threadCountColumn.Name = "threadCountColumn";
            // 
            // handleCountColumn
            // 
            this.handleCountColumn.HeaderText = "Handle Count";
            this.handleCountColumn.MinimumWidth = 6;
            this.handleCountColumn.Name = "handleCountColumn";
            // 
            // ProcessId
            // 
            this.ProcessId.HeaderText = "Process Id";
            this.ProcessId.MinimumWidth = 6;
            this.ProcessId.Name = "ProcessId";
            this.ProcessId.Visible = false;
            // 
            // terminateProcessButton
            // 
            this.terminateProcessButton.HeaderText = "Terminate";
            this.terminateProcessButton.MinimumWidth = 6;
            this.terminateProcessButton.Name = "terminateProcessButton";
            this.terminateProcessButton.UseColumnTextForButtonValue = true;
            // 
            // terminateAllButton
            // 
            this.terminateAllButton.BackColor = System.Drawing.SystemColors.Control;
            this.terminateAllButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminateAllButton.Location = new System.Drawing.Point(491, 3);
            this.terminateAllButton.Name = "terminateAllButton";
            this.terminateAllButton.Size = new System.Drawing.Size(482, 55);
            this.terminateAllButton.TabIndex = 2;
            this.terminateAllButton.Text = "Terminate All";
            this.terminateAllButton.UseVisualStyleBackColor = false;
            this.terminateAllButton.Click += new System.EventHandler(this.TerminateAllButton_Click);
            // 
            // addProcessButton
            // 
            this.addProcessButton.BackColor = System.Drawing.SystemColors.Control;
            this.addProcessButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addProcessButton.Location = new System.Drawing.Point(3, 3);
            this.addProcessButton.Name = "addProcessButton";
            this.addProcessButton.Size = new System.Drawing.Size(482, 55);
            this.addProcessButton.TabIndex = 1;
            this.addProcessButton.Text = "Add Process(es)";
            this.addProcessButton.UseVisualStyleBackColor = false;
            this.addProcessButton.Click += new System.EventHandler(this.AddProcessButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.addProcessButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.terminateAllButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(976, 612);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "EXE Files (*.exe)|*.exe";
            this.openFileDialog.Multiselect = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            // 
            // ProcessesTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 612);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "ProcessesTracker";
            this.Text = "Processes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView;
        private Button terminateAllButton;
        private Button addProcessButton;
        private TableLayoutPanel tableLayoutPanel1;
        private OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private DataGridViewTextBoxColumn processColumn;
        private DataGridViewTextBoxColumn memoryColumn;
        private DataGridViewTextBoxColumn processorTimeColumn;
        private DataGridViewTextBoxColumn threadCountColumn;
        private DataGridViewTextBoxColumn handleCountColumn;
        private DataGridViewTextBoxColumn ProcessId;
        private DataGridViewButtonColumn terminateProcessButton;
    }
}