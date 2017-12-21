using EasyMap.Controls;
namespace EasyMap
{
    partial class DBForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBForm));
            this.dataGridView1 = new EasyMap.Controls.MyDataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new EasyMap.Controls.MyDataGridView();
            this.field = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.condition = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.自定义查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.全部显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仅显示选中元素数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.txtNo = new System.Windows.Forms.ToolStripTextBox();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAll = new System.Windows.Forms.ToolStripButton();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.lblMsg = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.pictureBox1 = new System.Windows.Forms.ToolStripButton();
            this.button2 = new System.Windows.Forms.ToolStripButton();
            this.button1 = new System.Windows.Forms.ToolStripButton();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowLineNo = true;
            this.dataGridView1.Size = new System.Drawing.Size(302, 322);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 0);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件设定";
            // 
            // dataGridView2
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoRowHeight = false;
            this.dataGridView2.BackColor1 = System.Drawing.Color.White;
            this.dataGridView2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.ColumnHeadersHeight = 31;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.field,
            this.condition,
            this.data,
            this.Column1});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView2.Location = new System.Drawing.Point(6, 20);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.ShowLineNo = true;
            this.dataGridView2.Size = new System.Drawing.Size(293, 0);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEnter);
            // 
            // field
            // 
            this.field.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.field.HeaderText = "字段";
            this.field.Name = "field";
            this.field.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.field.Width = 39;
            // 
            // condition
            // 
            this.condition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.condition.HeaderText = "条件";
            this.condition.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<",
            "<=",
            "不等于",
            "大约"});
            this.condition.Name = "condition";
            this.condition.Width = 39;
            // 
            // data
            // 
            this.data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.data.HeaderText = "查询数据";
            this.data.Name = "data";
            this.data.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "条件关系";
            this.Column1.Items.AddRange(new object[] {
            "或者",
            "并且"});
            this.Column1.Name = "Column1";
            this.Column1.Width = 42;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "CSV 文件|*.csv";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(37, 131);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 5;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            this.monthCalendar1.Leave += new System.EventHandler(this.monthCalendar1_Leave);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.ContextMenuStrip = this.contextMenuStrip1;
            this.splitContainer1.Location = new System.Drawing.Point(11, 28);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(260, 247);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(305, 343);
            this.splitContainer1.SplitterDistance = 0;
            this.splitContainer1.TabIndex = 7;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自定义查询ToolStripMenuItem,
            this.导出ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.全部显示ToolStripMenuItem,
            this.仅显示选中元素数据ToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 104);
            // 
            // 自定义查询ToolStripMenuItem
            // 
            this.自定义查询ToolStripMenuItem.CheckOnClick = true;
            this.自定义查询ToolStripMenuItem.Image = global::EasyMap.Properties.Resources.FindDialog;
            this.自定义查询ToolStripMenuItem.Name = "自定义查询ToolStripMenuItem";
            this.自定义查询ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.自定义查询ToolStripMenuItem.Text = "自定义查询";
            this.自定义查询ToolStripMenuItem.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Image = global::EasyMap.Properties.Resources.ExportExcel;
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(181, 6);
            // 
            // 全部显示ToolStripMenuItem
            // 
            this.全部显示ToolStripMenuItem.Checked = true;
            this.全部显示ToolStripMenuItem.CheckOnClick = true;
            this.全部显示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.全部显示ToolStripMenuItem.Image = global::EasyMap.Properties.Resources.FooterInsertGallery;
            this.全部显示ToolStripMenuItem.Name = "全部显示ToolStripMenuItem";
            this.全部显示ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.全部显示ToolStripMenuItem.Text = "全部显示";
            this.全部显示ToolStripMenuItem.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // 仅显示选中元素数据ToolStripMenuItem
            // 
            this.仅显示选中元素数据ToolStripMenuItem.CheckOnClick = true;
            this.仅显示选中元素数据ToolStripMenuItem.Image = global::EasyMap.Properties.Resources.FootnotesEndnotesShow;
            this.仅显示选中元素数据ToolStripMenuItem.Name = "仅显示选中元素数据ToolStripMenuItem";
            this.仅显示选中元素数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.仅显示选中元素数据ToolStripMenuItem.Text = "仅显示选中元素数据";
            this.仅显示选中元素数据ToolStripMenuItem.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 6);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 29);
            this.panel1.TabIndex = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFirst,
            this.btnPrevious,
            this.txtNo,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator1,
            this.btnAll,
            this.btnSelect,
            this.lblMsg});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(325, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = global::EasyMap.Properties.Resources.MailMergeGoToFirstRecord;
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 22);
            this.btnFirst.Text = "toolStripButton1";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Image = global::EasyMap.Properties.Resources.MailMergeGoToPreviousRecord;
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(23, 22);
            this.btnPrevious.Text = "toolStripButton2";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // txtNo
            // 
            this.txtNo.AutoSize = false;
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(50, 25);
            this.txtNo.Leave += new System.EventHandler(this.txtNo_Leave);
            this.txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNo_KeyDown);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::EasyMap.Properties.Resources.MailMergeGoToNextRecord;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "toolStripButton3";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = global::EasyMap.Properties.Resources.MailMergeGotToLastRecord;
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 22);
            this.btnLast.Text = "toolStripButton4";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAll
            // 
            this.btnAll.Checked = true;
            this.btnAll.CheckOnClick = true;
            this.btnAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAll.Image = global::EasyMap.Properties.Resources.FooterInsertGallery;
            this.btnAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(23, 22);
            this.btnAll.Text = "toolStripButton5";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.CheckOnClick = true;
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelect.Image = global::EasyMap.Properties.Resources.FootnotesEndnotesShow;
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(23, 22);
            this.btnSelect.Text = "toolStripButton6";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(96, 22);
            this.lblMsg.Text = "toolStripLabel1";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pictureBox1,
            this.button2,
            this.button1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(325, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.pictureBox1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pictureBox1.Image = global::EasyMap.Properties.Resources.down;
            this.pictureBox1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 22);
            this.pictureBox1.Tag = "120";
            this.pictureBox1.Text = "展开";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button2.Image = global::EasyMap.Properties.Resources.FindDialog;
            this.button2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 22);
            this.button2.Text = "查询";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button1.Image = global::EasyMap.Properties.Resources.ExportExcel;
            this.button1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 22);
            this.button1.Text = "导出";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(280, 281);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(0, 0);
            this.button3.TabIndex = 11;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // DBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(325, 382);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBForm";
            this.ShowInTaskbar = false;
            this.Text = "数据查询";
            this.Shown += new System.EventHandler(this.DBForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyDataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private MyDataGridView dataGridView2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripTextBox txtNo;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAll;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStripLabel lblMsg;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripMenuItem 自定义查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 全部显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仅显示选中元素数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridViewComboBoxColumn field;
        private System.Windows.Forms.DataGridViewComboBoxColumn condition;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.ToolStripButton pictureBox1;
        private System.Windows.Forms.ToolStripButton button2;
        private System.Windows.Forms.ToolStripButton button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}