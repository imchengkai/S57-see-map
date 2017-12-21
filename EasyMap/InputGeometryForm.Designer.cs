using EasyMap.Controls;
namespace EasyMap
{
    partial class InputGeometryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputGeometryForm));
            this.chkPoint = new System.Windows.Forms.RadioButton();
            this.chkCurve = new System.Windows.Forms.RadioButton();
            this.chkPolygon = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.view = new EasyMap.Controls.MyDataGridView();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Polygon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.paste = new System.Windows.Forms.ToolStripButton();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.ToolStripButton();
            this.btnOk = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkPoint
            // 
            this.chkPoint.AutoSize = true;
            this.chkPoint.Checked = true;
            this.chkPoint.Location = new System.Drawing.Point(92, 7);
            this.chkPoint.Name = "chkPoint";
            this.chkPoint.Size = new System.Drawing.Size(35, 16);
            this.chkPoint.TabIndex = 0;
            this.chkPoint.TabStop = true;
            this.chkPoint.Text = "点";
            this.chkPoint.UseVisualStyleBackColor = true;
            this.chkPoint.CheckedChanged += new System.EventHandler(this.chkPoint_CheckedChanged);
            // 
            // chkCurve
            // 
            this.chkCurve.AutoSize = true;
            this.chkCurve.Location = new System.Drawing.Point(133, 7);
            this.chkCurve.Name = "chkCurve";
            this.chkCurve.Size = new System.Drawing.Size(47, 16);
            this.chkCurve.TabIndex = 1;
            this.chkCurve.Text = "曲线";
            this.chkCurve.UseVisualStyleBackColor = true;
            this.chkCurve.CheckedChanged += new System.EventHandler(this.chkPoint_CheckedChanged);
            // 
            // chkPolygon
            // 
            this.chkPolygon.AutoSize = true;
            this.chkPolygon.Location = new System.Drawing.Point(186, 7);
            this.chkPolygon.Name = "chkPolygon";
            this.chkPolygon.Size = new System.Drawing.Size(59, 16);
            this.chkPolygon.TabIndex = 2;
            this.chkPolygon.Text = "多边形";
            this.chkPolygon.UseVisualStyleBackColor = true;
            this.chkPolygon.CheckedChanged += new System.EventHandler(this.chkPoint_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "输入元素类型：";
            // 
            // view
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.view.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.view.AutoRowHeight = false;
            this.view.BackColor1 = System.Drawing.Color.White;
            this.view.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.view.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.Format = "N6";
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x,
            this.y,
            this.Polygon});
            this.view.Location = new System.Drawing.Point(3, 33);
            this.view.Name = "view";
            this.view.RowTemplate.Height = 21;
            this.view.ShowLineNo = true;
            this.view.Size = new System.Drawing.Size(465, 336);
            this.view.TabIndex = 4;
            this.view.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.view_CellEndEdit);
            this.view.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.view_CellValueChanged);
            // 
            // x
            // 
            this.x.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.Format = "N6";
            dataGridViewCellStyle11.NullValue = null;
            this.x.DefaultCellStyle = dataGridViewCellStyle11;
            this.x.HeaderText = "X坐标";
            this.x.Name = "x";
            this.x.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // y
            // 
            this.y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Format = "N6";
            dataGridViewCellStyle12.NullValue = null;
            this.y.DefaultCellStyle = dataGridViewCellStyle12;
            this.y.HeaderText = "Y坐标";
            this.y.Name = "y";
            this.y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Polygon
            // 
            this.Polygon.HeaderText = "Column1";
            this.Polygon.Name = "Polygon";
            this.Polygon.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.chkPoint);
            this.panel1.Controls.Add(this.chkCurve);
            this.panel1.Controls.Add(this.chkPolygon);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.view);
            this.panel1.Location = new System.Drawing.Point(3, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 372);
            this.panel1.TabIndex = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnAdd,
            this.btnDelete,
            this.paste,
            this.btnImport,
            this.pictureBox1,
            this.btnOk});
            this.toolStrip1.Location = new System.Drawing.Point(3, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(471, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(52, 22);
            this.btnNew.Text = "新建";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 22);
            this.btnAdd.Text = "插入行";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 22);
            this.btnDelete.Text = "删除行";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // paste
            // 
            this.paste.Image = global::EasyMap.Properties.Resources.Paste;
            this.paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(52, 22);
            this.paste.Text = "粘帖";
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // btnImport
            // 
            this.btnImport.Image = global::EasyMap.Properties.Resources.BlogPublish;
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(52, 22);
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.CheckOnClick = true;
            this.pictureBox1.Image = global::EasyMap.Properties.Resources.PickUpStyle;
            this.pictureBox1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 22);
            this.pictureBox1.Text = "坐标获取";
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnOk
            // 
            this.btnOk.Image = global::EasyMap.Properties.Resources.Grammar;
            this.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(52, 22);
            this.btnOk.Text = "应用";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "*.txt|*.txt";
            // 
            // InputGeometryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 430);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(293, 278);
            this.Name = "InputGeometryForm";
            this.ShowInTaskbar = false;
            this.Text = "坐标输入";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputGeometryForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton chkPoint;
        private System.Windows.Forms.RadioButton chkCurve;
        private System.Windows.Forms.RadioButton chkPolygon;
        private System.Windows.Forms.Label label1;
        private MyDataGridView view;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Polygon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton paste;
        private System.Windows.Forms.ToolStripButton btnImport;
        private System.Windows.Forms.ToolStripButton pictureBox1;
        private System.Windows.Forms.ToolStripButton btnOk;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnAdd;
    }
}