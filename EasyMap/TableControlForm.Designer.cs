namespace EasyMap
{
    partial class TableControlForm
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
            this.comTables = new System.Windows.Forms.ComboBox();
            this.columnsList = new EasyMap.Controls.MyDataGridView();
            this.tablename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.visible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnOk = new EasyMap.Controls.MyButton();
            this.btnClose = new EasyMap.Controls.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.columnsList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目：";
            // 
            // comTables
            // 
            this.comTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTables.FormattingEnabled = true;
            this.comTables.Location = new System.Drawing.Point(56, 33);
            this.comTables.Name = "comTables";
            this.comTables.Size = new System.Drawing.Size(219, 20);
            this.comTables.TabIndex = 1;
            this.comTables.SelectedIndexChanged += new System.EventHandler(this.comTables_SelectedIndexChanged);
            // 
            // columnsList
            // 
            this.columnsList.AllowUserToAddRows = false;
            this.columnsList.AllowUserToDeleteRows = false;
            this.columnsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.columnsList.AutoRowHeight = false;
            this.columnsList.BackColor1 = System.Drawing.Color.White;
            this.columnsList.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.columnsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.columnsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tablename,
            this.columnname,
            this.comment,
            this.visible});
            this.columnsList.Location = new System.Drawing.Point(17, 62);
            this.columnsList.Name = "columnsList";
            this.columnsList.RowTemplate.Height = 21;
            this.columnsList.ShowLineNo = true;
            this.columnsList.Size = new System.Drawing.Size(258, 164);
            this.columnsList.TabIndex = 2;
            // 
            // tablename
            // 
            this.tablename.HeaderText = "Column1";
            this.tablename.Name = "tablename";
            this.tablename.Visible = false;
            // 
            // columnname
            // 
            this.columnname.HeaderText = "Column1";
            this.columnname.Name = "columnname";
            this.columnname.Visible = false;
            // 
            // comment
            // 
            this.comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.comment.HeaderText = "属性";
            this.comment.Name = "comment";
            // 
            // visible
            // 
            this.visible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.visible.HeaderText = "可见性";
            this.visible.Name = "visible";
            this.visible.Width = 47;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(114, 233);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(200, 233);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TableControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.columnsList);
            this.Controls.Add(this.comTables);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(292, 266);
            this.Name = "TableControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性控制";
            ((System.ComponentModel.ISupportInitialize)(this.columnsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comTables;
        private EasyMap.Controls.MyDataGridView columnsList;
        private EasyMap.Controls.MyButton btnOk;
        private EasyMap.Controls.MyButton btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn tablename;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnname;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn visible;
    }
}