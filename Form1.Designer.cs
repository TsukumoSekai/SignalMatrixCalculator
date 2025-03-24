namespace SignalMatrixCalculator
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_GenerateMatrix = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_RowCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ColumnCount = new System.Windows.Forms.TextBox();
            this.textBox_SingleCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_Step = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label_VisitedNodeCount = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label_OperationGuide = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_GenerateMatrix
            // 
            this.button_GenerateMatrix.Location = new System.Drawing.Point(16, 105);
            this.button_GenerateMatrix.Name = "button_GenerateMatrix";
            this.button_GenerateMatrix.Size = new System.Drawing.Size(215, 23);
            this.button_GenerateMatrix.TabIndex = 0;
            this.button_GenerateMatrix.Text = "GenerateMatrix  生成矩阵";
            this.button_GenerateMatrix.UseVisualStyleBackColor = true;
            this.button_GenerateMatrix.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "RowCount     行";
            // 
            // textBox_RowCount
            // 
            this.textBox_RowCount.Location = new System.Drawing.Point(189, 3);
            this.textBox_RowCount.Name = "textBox_RowCount";
            this.textBox_RowCount.Size = new System.Drawing.Size(42, 25);
            this.textBox_RowCount.TabIndex = 2;
            this.textBox_RowCount.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "ColumnCount  列";
            // 
            // textBox_ColumnCount
            // 
            this.textBox_ColumnCount.Location = new System.Drawing.Point(189, 34);
            this.textBox_ColumnCount.Name = "textBox_ColumnCount";
            this.textBox_ColumnCount.Size = new System.Drawing.Size(42, 25);
            this.textBox_ColumnCount.TabIndex = 4;
            this.textBox_ColumnCount.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox_SingleCount
            // 
            this.textBox_SingleCount.Location = new System.Drawing.Point(189, 68);
            this.textBox_SingleCount.Name = "textBox_SingleCount";
            this.textBox_SingleCount.Size = new System.Drawing.Size(42, 25);
            this.textBox_SingleCount.TabIndex = 6;
            this.textBox_SingleCount.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "SignalCount  信号数量";
            // 
            // label_Step
            // 
            this.label_Step.AutoSize = true;
            this.label_Step.Location = new System.Drawing.Point(257, 37);
            this.label_Step.Name = "label_Step";
            this.label_Step.Size = new System.Drawing.Size(39, 15);
            this.label_Step.TabIndex = 7;
            this.label_Step.Text = "Step";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "StartCalculate  开始计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label_VisitedNodeCount
            // 
            this.label_VisitedNodeCount.AutoSize = true;
            this.label_VisitedNodeCount.Location = new System.Drawing.Point(257, 3);
            this.label_VisitedNodeCount.Name = "label_VisitedNodeCount";
            this.label_VisitedNodeCount.Size = new System.Drawing.Size(39, 15);
            this.label_VisitedNodeCount.TabIndex = 9;
            this.label_VisitedNodeCount.Text = "Info";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 163);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(215, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cancel  取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label_OperationGuide
            // 
            this.label_OperationGuide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_OperationGuide.AutoSize = true;
            this.label_OperationGuide.Location = new System.Drawing.Point(12, 449);
            this.label_OperationGuide.Name = "label_OperationGuide";
            this.label_OperationGuide.Size = new System.Drawing.Size(506, 30);
            this.label_OperationGuide.TabIndex = 11;
            this.label_OperationGuide.Text = "LeftMouse：SetSingle；RightMouse：Simulate；MiddleMouse：Ignore\r\n左键：设置信号；右键：模拟；中键：忽略";
            this.label_OperationGuide.Visible = false;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(524, 488);
            this.Controls.Add(this.label_OperationGuide);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_VisitedNodeCount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_Step);
            this.Controls.Add(this.textBox_SingleCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_ColumnCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_RowCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_GenerateMatrix);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_GenerateMatrix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_RowCount;
        private System.Windows.Forms.TextBox textBox_ColumnCount;
        private System.Windows.Forms.TextBox textBox_SingleCount;
        private System.Windows.Forms.Label label_Step;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_VisitedNodeCount;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label_OperationGuide;
    }
}

