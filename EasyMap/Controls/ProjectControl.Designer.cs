using System.Windows.Forms;
namespace EasyMap.Controls
{
    partial class ProjectControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("项目");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comFindText = new System.Windows.Forms.ComboBox();
            this.btnSearch = new EasyMap.Controls.MyButton();
            this.projectTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.btnModify = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.myDataGridView1 = new EasyMap.Controls.MyDataGridView();
            this.year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectListTip = new EasyMap.Controls.MyDataGridView();
            this.projectname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectListTip)).BeginInit();
            this.SuspendLayout();
            // 
            // comFindText
            // 
            this.comFindText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comFindText.FormattingEnabled = true;
            this.comFindText.Location = new System.Drawing.Point(4, 4);
            this.comFindText.Name = "comFindText";
            this.comFindText.Size = new System.Drawing.Size(224, 20);
            this.comFindText.TabIndex = 0;
            this.comFindText.SelectedIndexChanged += new System.EventHandler(this.comFindText_SelectedIndexChanged);
            this.comFindText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comFindText_KeyDown);
            this.comFindText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comFindText_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = global::EasyMap.Properties.Resources.FindDialog;
            this.btnSearch.Location = new System.Drawing.Point(234, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(21, 21);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // projectTree
            // 
            this.projectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectTree.ContextMenuStrip = this.contextMenuStrip1;
            this.projectTree.HideSelection = false;
            this.projectTree.Location = new System.Drawing.Point(4, 52);
            this.projectTree.Name = "projectTree";
            treeNode1.Name = "ノード0";
            treeNode1.Text = "项目";
            this.projectTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.projectTree.Size = new System.Drawing.Size(251, 368);
            this.projectTree.TabIndex = 2;
            this.projectTree.Click += new System.EventHandler(this.projectTree_Click);
            this.projectTree.DoubleClick += new System.EventHandler(this.projectTree_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnModify,
            this.btnDelete,
            this.toolStripMenuItem1,
            this.btnOpenDoc});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 120);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // btnAdd
            // 
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(152, 22);
            this.btnAdd.Text = "项目管理";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(152, 22);
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(152, 22);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // btnOpenDoc
            // 
            this.btnOpenDoc.Name = "btnOpenDoc";
            this.btnOpenDoc.Size = new System.Drawing.Size(152, 22);
            this.btnOpenDoc.Text = "打开项目文档";
            this.btnOpenDoc.Click += new System.EventHandler(this.btnOpenDoc_Click);
            // 
            // myDataGridView1
            // 
            this.myDataGridView1.AllowUserToAddRows = false;
            this.myDataGridView1.AllowUserToDeleteRows = false;
            this.myDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myDataGridView1.AutoRowHeight = false;
            this.myDataGridView1.BackColor1 = System.Drawing.Color.White;
            this.myDataGridView1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.myDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myDataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.myDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.myDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.year,
            this.area});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.myDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.myDataGridView1.Location = new System.Drawing.Point(4, 28);
            this.myDataGridView1.Name = "myDataGridView1";
            this.myDataGridView1.ReadOnly = true;
            this.myDataGridView1.RowHeadersVisible = false;
            this.myDataGridView1.RowTemplate.Height = 21;
            this.myDataGridView1.ShowLineNo = true;
            this.myDataGridView1.Size = new System.Drawing.Size(251, 18);
            this.myDataGridView1.TabIndex = 3;
            this.myDataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.myDataGridView1_ColumnHeaderMouseClick);
            // 
            // year
            // 
            this.year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.year.HeaderText = "年份";
            this.year.Name = "year";
            this.year.ReadOnly = true;
            this.year.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // area
            // 
            this.area.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.area.HeaderText = "地区";
            this.area.Name = "area";
            this.area.ReadOnly = true;
            this.area.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // projectListTip
            // 
            this.projectListTip.AllowUserToAddRows = false;
            this.projectListTip.AllowUserToDeleteRows = false;
            this.projectListTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectListTip.AutoRowHeight = false;
            this.projectListTip.BackColor1 = System.Drawing.Color.White;
            this.projectListTip.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.projectListTip.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.projectListTip.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.projectListTip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projectListTip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.projectname,
            this.projectid});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.projectListTip.DefaultCellStyle = dataGridViewCellStyle4;
            this.projectListTip.Location = new System.Drawing.Point(4, 54);
            this.projectListTip.MultiSelect = false;
            this.projectListTip.Name = "projectListTip";
            this.projectListTip.ReadOnly = true;
            this.projectListTip.RowHeadersVisible = false;
            this.projectListTip.RowTemplate.Height = 21;
            this.projectListTip.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.projectListTip.ShowLineNo = true;
            this.projectListTip.Size = new System.Drawing.Size(224, 237);
            this.projectListTip.TabIndex = 4;
            this.projectListTip.Visible = false;
            this.projectListTip.DoubleClick += new System.EventHandler(this.projectListTip_DoubleClick);
            // 
            // projectname
            // 
            this.projectname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.projectname.HeaderText = "项目名称";
            this.projectname.Name = "projectname";
            this.projectname.ReadOnly = true;
            this.projectname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // projectid
            // 
            this.projectid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.projectid.HeaderText = "项目编号";
            this.projectid.Name = "projectid";
            this.projectid.ReadOnly = true;
            this.projectid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.projectid.Width = 59;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(4, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(251, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.myDataGridView1);
            this.Controls.Add(this.projectListTip);
            this.Controls.Add(this.projectTree);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.comFindText);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(258, 423);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectListTip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comFindText;
        private MyButton btnSearch;
        private TreeView projectTree;
        private MyDataGridView myDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn year;
        private System.Windows.Forms.DataGridViewTextBoxColumn area;
        private System.Windows.Forms.ToolStripMenuItem btnAdd;
        private System.Windows.Forms.ToolStripMenuItem btnModify;
        private System.Windows.Forms.ToolStripMenuItem btnDelete;
        private MyDataGridView projectListTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectname;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectid;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem btnOpenDoc;
        private ComboBox comboBox1;
        public ContextMenuStrip contextMenuStrip1;
    }
}
