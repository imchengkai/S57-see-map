using EasyMap.Controls;
namespace EasyMap
{
    partial class FieldDefineForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.txtinput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkInput = new System.Windows.Forms.CheckBox();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.comType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new EasyMap.Controls.MyButton();
            this.btnClose = new EasyMap.Controls.MyButton();
            this.button3 = new EasyMap.Controls.MyButton();
            this.button4 = new EasyMap.Controls.MyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new EasyMap.Controls.MyDataGridView();
            this.fieldname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fieldtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowunselect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowinput = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputlist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllowVisible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isnewrow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(65, 12);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "图层类型：";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.chkVisible);
            this.groupBox1.Controls.Add(this.txtinput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkInput);
            this.groupBox1.Controls.Add(this.chkSelect);
            this.groupBox1.Controls.Add(this.comType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 286);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 96);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段设定";
            // 
            // chkVisible
            // 
            this.chkVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkVisible.AutoSize = true;
            this.chkVisible.Location = new System.Drawing.Point(543, 74);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Size = new System.Drawing.Size(48, 16);
            this.chkVisible.TabIndex = 7;
            this.chkVisible.Text = "显示";
            this.chkVisible.UseVisualStyleBackColor = true;
            this.chkVisible.Click += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtinput
            // 
            this.txtinput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtinput.Enabled = false;
            this.txtinput.Location = new System.Drawing.Point(222, 44);
            this.txtinput.MaxLength = 1000;
            this.txtinput.Name = "txtinput";
            this.txtinput.Size = new System.Drawing.Size(368, 21);
            this.txtinput.TabIndex = 6;
            this.txtinput.Leave += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "允许输入的列表项目，使用【，】分割";
            // 
            // chkInput
            // 
            this.chkInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInput.AutoSize = true;
            this.chkInput.Enabled = false;
            this.chkInput.Location = new System.Drawing.Point(440, 74);
            this.chkInput.Name = "chkInput";
            this.chkInput.Size = new System.Drawing.Size(96, 16);
            this.chkInput.TabIndex = 4;
            this.chkInput.Text = "允许自由输入";
            this.chkInput.UseVisualStyleBackColor = true;
            this.chkInput.Click += new System.EventHandler(this.txtName_TextChanged);
            // 
            // chkSelect
            // 
            this.chkSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelect.AutoSize = true;
            this.chkSelect.Enabled = false;
            this.chkSelect.Location = new System.Drawing.Point(368, 74);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(72, 16);
            this.chkSelect.TabIndex = 3;
            this.chkSelect.Text = "允许不选";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.Click += new System.EventHandler(this.txtName_TextChanged);
            // 
            // comType
            // 
            this.comType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comType.FormattingEnabled = true;
            this.comType.Items.AddRange(new object[] {
            "文本",
            "整数",
            "小数",
            "日期",
            "列表"});
            this.comType.Location = new System.Drawing.Point(494, 21);
            this.comType.Name = "comType";
            this.comType.Size = new System.Drawing.Size(96, 20);
            this.comType.TabIndex = 2;
            this.comType.SelectedIndexChanged += new System.EventHandler(this.comType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(429, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "属性类型：";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(70, 21);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(353, 21);
            this.txtName.TabIndex = 1;
            this.txtName.Leave += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "属性名称：";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(451, 389);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(532, 389);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.Enabled = false;
            this.button3.Image = global::EasyMap.Properties.Resources.Delete;
            this.button3.Location = new System.Drawing.Point(3, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button4.Enabled = false;
            this.button4.Image = global::EasyMap.Properties.Resources.PropertySheet;
            this.button4.Location = new System.Drawing.Point(3, 103);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 23);
            this.button4.TabIndex = 11;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Location = new System.Drawing.Point(585, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(32, 247);
            this.panel1.TabIndex = 12;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "评估报告图层",
            "监测点图层",
            "交易案例图层",
            "宗地信息图层",
            "房屋售价图层",
            "房屋租赁图层",
            "其他图层"});
            this.comboBox1.Location = new System.Drawing.Point(78, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoRowHeight = false;
            this.dataGridView1.BackColor1 = System.Drawing.Color.White;
            this.dataGridView1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldname,
            this.fieldtype,
            this.allowunselect,
            this.allowinput,
            this.inputlist,
            this.no,
            this.AllowVisible,
            this.isnewrow,
            this.oldname});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(6, 30);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowLineNo = true;
            this.dataGridView1.Size = new System.Drawing.Size(579, 247);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // fieldname
            // 
            this.fieldname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.fieldname.HeaderText = "属性名称";
            this.fieldname.Name = "fieldname";
            this.fieldname.ReadOnly = true;
            this.fieldname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fieldname.Width = 42;
            // 
            // fieldtype
            // 
            this.fieldtype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.fieldtype.HeaderText = "属性类型";
            this.fieldtype.Name = "fieldtype";
            this.fieldtype.ReadOnly = true;
            this.fieldtype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fieldtype.Width = 42;
            // 
            // allowunselect
            // 
            this.allowunselect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.allowunselect.HeaderText = "允许不选";
            this.allowunselect.Name = "allowunselect";
            this.allowunselect.ReadOnly = true;
            this.allowunselect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.allowunselect.Width = 42;
            // 
            // allowinput
            // 
            this.allowinput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.allowinput.HeaderText = "允许输入";
            this.allowinput.Name = "allowinput";
            this.allowinput.ReadOnly = true;
            this.allowinput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.allowinput.Width = 42;
            // 
            // inputlist
            // 
            this.inputlist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.inputlist.FillWeight = 150F;
            this.inputlist.HeaderText = "输入内容项目";
            this.inputlist.Name = "inputlist";
            this.inputlist.ReadOnly = true;
            this.inputlist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // no
            // 
            this.no.HeaderText = "no";
            this.no.Name = "no";
            this.no.ReadOnly = true;
            this.no.Visible = false;
            // 
            // AllowVisible
            // 
            this.AllowVisible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.AllowVisible.HeaderText = "显示控制";
            this.AllowVisible.Name = "AllowVisible";
            this.AllowVisible.ReadOnly = true;
            this.AllowVisible.Width = 61;
            // 
            // isnewrow
            // 
            this.isnewrow.HeaderText = "Column1";
            this.isnewrow.Name = "isnewrow";
            this.isnewrow.ReadOnly = true;
            this.isnewrow.Visible = false;
            // 
            // oldname
            // 
            this.oldname.HeaderText = "Column1";
            this.oldname.Name = "oldname";
            this.oldname.ReadOnly = true;
            this.oldname.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.Label1);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 417);
            this.panel2.TabIndex = 14;
            // 
            // FieldDefineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(620, 417);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(620, 365);
            this.Name = "FieldDefineForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性定义";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FieldDefineForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private MyDataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkInput;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.ComboBox comType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private MyButton btnOk;
        private MyButton btnClose;
        private System.Windows.Forms.TextBox txtinput;
        private System.Windows.Forms.Label label4;
        private MyButton button3;
        private MyButton button4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldname;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn allowunselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn allowinput;
        private System.Windows.Forms.DataGridViewTextBoxColumn inputlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllowVisible;
        private System.Windows.Forms.DataGridViewTextBoxColumn isnewrow;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldname;
    }
}