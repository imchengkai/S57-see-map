using EasyMap.Controls;
namespace EasyMap
{
    partial class TifSplitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TifSplitForm));
            this.btnSplit = new EasyMap.Controls.MyButton();
            this.btnBrowser = new EasyMap.Controls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.numRow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numCol = new System.Windows.Forms.NumericUpDown();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnClose = new EasyMap.Controls.MyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCol)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSplit
            // 
            this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplit.Location = new System.Drawing.Point(255, 39);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 23);
            this.btnSplit.TabIndex = 0;
            this.btnSplit.Text = "拆分(&S)";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.Location = new System.Drawing.Point(255, 6);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnBrowser.TabIndex = 1;
            this.btnBrowser.Text = "浏览(&B)...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "影像图：";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(74, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(175, 21);
            this.textBox1.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "TIF 文件|*.tif";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "行数：";
            // 
            // numRow
            // 
            this.numRow.Location = new System.Drawing.Point(74, 40);
            this.numRow.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numRow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRow.Name = "numRow";
            this.numRow.Size = new System.Drawing.Size(62, 21);
            this.numRow.TabIndex = 5;
            this.numRow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "列数：";
            // 
            // numCol
            // 
            this.numCol.Location = new System.Drawing.Point(189, 40);
            this.numCol.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCol.Name = "numCol";
            this.numCol.Size = new System.Drawing.Size(63, 21);
            this.numCol.TabIndex = 5;
            this.numCol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(17, 70);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(232, 23);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(256, 70);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.numCol);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numRow);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnBrowser);
            this.panel1.Controls.Add(this.btnSplit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 105);
            this.panel1.TabIndex = 8;
            // 
            // TifSplitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(344, 132);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(344, 132);
            this.Name = "TifSplitForm";
            this.Text = "影像图拆分";
            ((System.ComponentModel.ISupportInitialize)(this.numRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCol)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyButton btnSplit;
        private MyButton btnBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCol;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private MyButton btnClose;
        private System.Windows.Forms.Panel panel1;
    }
}