namespace CV_4_WF
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.functionsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.iterationsNUD = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.algorithmsComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAverageValue = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radioButtonShowInGraph = new System.Windows.Forms.RadioButton();
            this.radioButtonShowAverageRes = new System.Windows.Forms.RadioButton();
            this.radioButtonsPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNUD)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.radioButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(150, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(822, 637);
            this.panel1.TabIndex = 0;
            // 
            // functionsComboBox
            // 
            this.functionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.functionsComboBox.FormattingEnabled = true;
            this.functionsComboBox.Location = new System.Drawing.Point(4, 29);
            this.functionsComboBox.Name = "functionsComboBox";
            this.functionsComboBox.Size = new System.Drawing.Size(140, 21);
            this.functionsComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Function:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(4, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Iterations:";
            // 
            // iterationsNUD
            // 
            this.iterationsNUD.Location = new System.Drawing.Point(4, 115);
            this.iterationsNUD.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.iterationsNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iterationsNUD.Name = "iterationsNUD";
            this.iterationsNUD.Size = new System.Drawing.Size(140, 20);
            this.iterationsNUD.TabIndex = 4;
            this.iterationsNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startButton.Location = new System.Drawing.Point(4, 626);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(140, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(4, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Algorithm:";
            // 
            // algorithmsComboBox
            // 
            this.algorithmsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmsComboBox.FormattingEnabled = true;
            this.algorithmsComboBox.Location = new System.Drawing.Point(4, 72);
            this.algorithmsComboBox.Name = "algorithmsComboBox";
            this.algorithmsComboBox.Size = new System.Drawing.Size(140, 21);
            this.algorithmsComboBox.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBoxAverageValue);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(150, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(822, 637);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(163, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Average value";
            // 
            // textBoxAverageValue
            // 
            this.textBoxAverageValue.Location = new System.Drawing.Point(162, 23);
            this.textBoxAverageValue.Name = "textBoxAverageValue";
            this.textBoxAverageValue.ReadOnly = true;
            this.textBoxAverageValue.Size = new System.Drawing.Size(100, 20);
            this.textBoxAverageValue.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.result});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.Size = new System.Drawing.Size(153, 631);
            this.dataGridView1.TabIndex = 0;
            // 
            // result
            // 
            this.result.HeaderText = "F(x)";
            this.result.Name = "result";
            this.result.ReadOnly = true;
            // 
            // radioButtonShowInGraph
            // 
            this.radioButtonShowInGraph.AutoSize = true;
            this.radioButtonShowInGraph.Location = new System.Drawing.Point(13, 13);
            this.radioButtonShowInGraph.Name = "radioButtonShowInGraph";
            this.radioButtonShowInGraph.Size = new System.Drawing.Size(93, 17);
            this.radioButtonShowInGraph.TabIndex = 8;
            this.radioButtonShowInGraph.TabStop = true;
            this.radioButtonShowInGraph.Text = "Show in graph";
            this.radioButtonShowInGraph.UseVisualStyleBackColor = true;
            // 
            // radioButtonShowAverageRes
            // 
            this.radioButtonShowAverageRes.AutoSize = true;
            this.radioButtonShowAverageRes.Location = new System.Drawing.Point(13, 36);
            this.radioButtonShowAverageRes.Name = "radioButtonShowAverageRes";
            this.radioButtonShowAverageRes.Size = new System.Drawing.Size(122, 17);
            this.radioButtonShowAverageRes.TabIndex = 9;
            this.radioButtonShowAverageRes.TabStop = true;
            this.radioButtonShowAverageRes.Text = "Show average result";
            this.radioButtonShowAverageRes.UseVisualStyleBackColor = true;
            // 
            // radioButtonsPanel
            // 
            this.radioButtonsPanel.Controls.Add(this.radioButtonShowInGraph);
            this.radioButtonsPanel.Controls.Add(this.radioButtonShowAverageRes);
            this.radioButtonsPanel.Location = new System.Drawing.Point(4, 141);
            this.radioButtonsPanel.Name = "radioButtonsPanel";
            this.radioButtonsPanel.Size = new System.Drawing.Size(140, 66);
            this.radioButtonsPanel.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.radioButtonsPanel);
            this.Controls.Add(this.algorithmsComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.iterationsNUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.functionsComboBox);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Biologically inspired algorithms";
            ((System.ComponentModel.ISupportInitialize)(this.iterationsNUD)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.radioButtonsPanel.ResumeLayout(false);
            this.radioButtonsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox functionsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown iterationsNUD;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox algorithmsComboBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn result;
        private System.Windows.Forms.RadioButton radioButtonShowInGraph;
        private System.Windows.Forms.RadioButton radioButtonShowAverageRes;
        private System.Windows.Forms.Panel radioButtonsPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAverageValue;
    }
}

