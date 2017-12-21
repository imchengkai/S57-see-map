using EasyMap.Controls;
namespace EasyMap
{
    partial class ShowPictureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowPictureForm));
            this.label1 = new System.Windows.Forms.Label();
            this.DateList = new System.Windows.Forms.ComboBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.button1 = new EasyMap.Controls.MyButton();
            this.button3 = new EasyMap.Controls.MyButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnOpenPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button5 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button4 = new System.Windows.Forms.ToolStripButton();
            this.button2 = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "日期：";
            // 
            // DateList
            // 
            this.DateList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DateList.FormattingEnabled = true;
            this.DateList.Location = new System.Drawing.Point(57, 7);
            this.DateList.Name = "DateList";
            this.DateList.Size = new System.Drawing.Size(165, 20);
            this.DateList.TabIndex = 1;
            this.DateList.SelectedValueChanged += new System.EventHandler(this.DateList_SelectedValueChanged);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(57, 34);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 7;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            this.monthCalendar1.Leave += new System.EventHandler(this.monthCalendar1_Leave);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Image = global::EasyMap.Properties.Resources.CalendarInsert;
            this.button1.Location = new System.Drawing.Point(228, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 22);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(177, 319);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "关闭(&C)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "*.JPEG|*.jpg|*.PNG|*.png|*.GIF|*.gif|*.BMP|*.bmp";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "选择图片";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.monthCalendar1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.DateList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(264, 352);
            this.panel1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl1.Location = new System.Drawing.Point(12, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 308);
            this.tabControl1.TabIndex = 8;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenPicture,
            this.btnSave,
            this.btnDelete1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // btnOpenPicture
            // 
            this.btnOpenPicture.Image = global::EasyMap.Properties.Resources.FileOpen;
            this.btnOpenPicture.Name = "btnOpenPicture";
            this.btnOpenPicture.Size = new System.Drawing.Size(133, 22);
            this.btnOpenPicture.Text = "导入图片...";
            this.btnOpenPicture.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::EasyMap.Properties.Resources.SaveAndClose;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 22);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDelete1
            // 
            this.btnDelete1.Image = global::EasyMap.Properties.Resources.Delete;
            this.btnDelete1.Name = "btnDelete1";
            this.btnDelete1.Size = new System.Drawing.Size(133, 22);
            this.btnDelete1.Text = "删除";
            this.btnDelete1.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainer1.ContextMenuStrip = this.contextMenuStrip1;
            this.splitContainer1.Location = new System.Drawing.Point(6, 28);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(210, 217);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(264, 356);
            this.splitContainer1.SplitterDistance = 0;
            this.splitContainer1.TabIndex = 9;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(264, 0);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(221, 256);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(0, 0);
            this.button5.TabIndex = 20;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button4,
            this.button2,
            this.btnDelete,
            this.pictureBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(276, 25);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button4
            // 
            this.button4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.button4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button4.Image = global::EasyMap.Properties.Resources.FileOpen;
            this.button4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 22);
            this.button4.Text = "打开图片";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.button2.Image = global::EasyMap.Properties.Resources.SaveAndClose;
            this.button2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 22);
            this.button2.Text = "保存";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::EasyMap.Properties.Resources.Delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pictureBox1.Image = global::EasyMap.Properties.Resources.down;
            this.pictureBox1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 22);
            this.pictureBox1.Tag = "120";
            this.pictureBox1.Text = "toolStripButton4";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ShowPictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton = this.button3;
            this.ClientSize = new System.Drawing.Size(276, 396);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowPictureForm";
            this.ShowInTaskbar = false;
            this.Text = "图片";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShowPictureForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DateList;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private MyButton button1;
        private MyButton button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem btnOpenPicture;
        private System.Windows.Forms.ToolStripMenuItem btnSave;
        private System.Windows.Forms.ToolStripMenuItem btnDelete1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton button4;
        private System.Windows.Forms.ToolStripButton button2;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton pictureBox1;
    }
}