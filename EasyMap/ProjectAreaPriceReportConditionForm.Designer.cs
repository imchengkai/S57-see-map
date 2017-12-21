namespace EasyMap
{
    partial class ProjectAreaPriceReportConditionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.myButton1 = new EasyMap.Controls.MyButton();
            this.myButton2 = new EasyMap.Controls.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目开始年度：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(107, 37);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(65, 21);
            this.numericUpDown1.TabIndex = 1;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(281, 37);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(65, 21);
            this.numericUpDown2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "～ 项目结束年度：";
            // 
            // myButton1
            // 
            this.myButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.myButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.myButton1.Location = new System.Drawing.Point(268, 71);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(75, 23);
            this.myButton1.TabIndex = 4;
            this.myButton1.Text = "取消(&C)";
            this.myButton1.UseVisualStyleBackColor = true;
            this.myButton1.Click += new System.EventHandler(this.myButton1_Click);
            // 
            // myButton2
            // 
            this.myButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.myButton2.Location = new System.Drawing.Point(187, 71);
            this.myButton2.Name = "myButton2";
            this.myButton2.Size = new System.Drawing.Size(75, 23);
            this.myButton2.TabIndex = 5;
            this.myButton2.Text = "确定(&O)";
            this.myButton2.UseVisualStyleBackColor = true;
            this.myButton2.Click += new System.EventHandler(this.myButton2_Click);
            // 
            // ProjectAreaPriceReportConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.myButton1;
            this.ClientSize = new System.Drawing.Size(357, 100);
            this.Controls.Add(this.myButton2);
            this.Controls.Add(this.myButton1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(357, 100);
            this.Name = "ProjectAreaPriceReportConditionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "项目年度设置";
            this.Shown += new System.EventHandler(this.ProjectAreaPriceReportConditionForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private EasyMap.Controls.MyButton myButton1;
        private EasyMap.Controls.MyButton myButton2;
    }
}