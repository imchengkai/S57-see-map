using System.Collections.Generic;
namespace EasyMap
{
    partial class AreaDefineForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaDefineForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnDelete = new EasyMap.Controls.MyButton();
            this.btnSave = new EasyMap.Controls.MyButton();
            this.myDataGridView1 = new EasyMap.Controls.MyDataGridView();
            this.areaname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel2.Controls.Add(this.btnSave);
            this.splitContainer1.Panel2.Controls.Add(this.myDataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(653, 393);
            this.splitContainer1.SplitterDistance = 217;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(217, 393);
            this.treeView1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(273, 367);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(354, 367);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // myDataGridView1
            // 
            this.myDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myDataGridView1.AutoRowHeight = false;
            this.myDataGridView1.BackColor1 = System.Drawing.Color.White;
            this.myDataGridView1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.myDataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.myDataGridView1.ColumnHeadersHeight = 40;
            this.myDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.myDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.areaname,
            this.x1,
            this.y1,
            this.x2,
            this.y2,
            this.Column1});
            this.myDataGridView1.ColumnSpanSettings = ((System.Collections.Generic.List<string>)(resources.GetObject("myDataGridView1.ColumnSpanSettings")));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.myDataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.myDataGridView1.Location = new System.Drawing.Point(3, 3);
            this.myDataGridView1.Name = "myDataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.myDataGridView1.RowHeadersWidth = 40;
            this.myDataGridView1.RowHeaderTexts = ((System.Collections.Generic.List<string>)(resources.GetObject("myDataGridView1.RowHeaderTexts")));
            this.myDataGridView1.RowTemplate.Height = 21;
            this.myDataGridView1.ShowLineNo = true;
            this.myDataGridView1.Size = new System.Drawing.Size(426, 358);
            this.myDataGridView1.TabIndex = 0;
            // 
            // areaname
            // 
            this.areaname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.areaname.HeaderText = "名称";
            this.areaname.Name = "areaname";
            // 
            // x1
            // 
            this.x1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.x1.HeaderText = "X1";
            this.x1.Name = "x1";
            this.x1.Width = 42;
            // 
            // y1
            // 
            this.y1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.y1.HeaderText = "Y1";
            this.y1.Name = "y1";
            this.y1.Width = 42;
            // 
            // x2
            // 
            this.x2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.x2.HeaderText = "X2";
            this.x2.Name = "x2";
            this.x2.Width = 42;
            // 
            // y2
            // 
            this.y2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.y2.HeaderText = "Y2";
            this.y2.Name = "y2";
            this.y2.Width = 42;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // AreaDefineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 420);
            this.Controls.Add(this.splitContainer1);
            this.MinimizeBox = false;
            this.Name = "AreaDefineForm";
            this.Text = "行政区域设置";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private Controls.MyButton btnDelete;
        private Controls.MyButton btnSave;
        private Controls.MyDataGridView myDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaname;
        private System.Windows.Forms.DataGridViewTextBoxColumn x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn y1;
        private System.Windows.Forms.DataGridViewTextBoxColumn x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn y2;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}