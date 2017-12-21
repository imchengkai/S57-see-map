namespace EasyMap
{
    partial class AreaTipForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaTipForm));
            this.myDataGridView1 = new EasyMap.Controls.MyDataGridView();
            this.areaname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new EasyMap.Controls.MyButton();
            this.btnOk = new EasyMap.Controls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // myDataGridView1
            // 
            this.myDataGridView1.AllowUserToAddRows = false;
            this.myDataGridView1.AllowUserToDeleteRows = false;
            this.myDataGridView1.AutoRowHeight = false;
            this.myDataGridView1.BackColor1 = System.Drawing.Color.White;
            this.myDataGridView1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.myDataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.myDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.areaname,
            this.layer});
            this.myDataGridView1.ColumnSpanSettings = ((System.Collections.Generic.List<string>)(resources.GetObject("myDataGridView1.ColumnSpanSettings")));
            this.myDataGridView1.Location = new System.Drawing.Point(6, 36);
            this.myDataGridView1.MultiSelect = false;
            this.myDataGridView1.Name = "myDataGridView1";
            this.myDataGridView1.ReadOnly = true;
            this.myDataGridView1.RowHeaderTexts = ((System.Collections.Generic.List<string>)(resources.GetObject("myDataGridView1.RowHeaderTexts")));
            this.myDataGridView1.RowTemplate.Height = 21;
            this.myDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.myDataGridView1.ShowLineNo = true;
            this.myDataGridView1.Size = new System.Drawing.Size(286, 198);
            this.myDataGridView1.TabIndex = 0;
            this.myDataGridView1.DoubleClick += new System.EventHandler(this.btnOk_Click);
            // 
            // areaname
            // 
            this.areaname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.areaname.DataPropertyName = "areaname";
            this.areaname.HeaderText = "编号";
            this.areaname.Name = "areaname";
            this.areaname.ReadOnly = true;
            this.areaname.Width = 54;
            // 
            // layer
            // 
            this.layer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.layer.DataPropertyName = "layer";
            this.layer.HeaderText = "图层";
            this.layer.Name = "layer";
            this.layer.ReadOnly = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(217, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(136, 240);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 3;
            // 
            // AreaTipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(298, 272);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.myDataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(298, 272);
            this.Name = "AreaTipForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "宗地选择";
            this.Load += new System.EventHandler(this.AreaTipForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyMap.Controls.MyDataGridView myDataGridView1;
        private EasyMap.Controls.MyButton btnCancel;
        private EasyMap.Controls.MyButton btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaname;
        private System.Windows.Forms.DataGridViewTextBoxColumn layer;
    }
}