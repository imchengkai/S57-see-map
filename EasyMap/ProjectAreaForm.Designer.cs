namespace EasyMap
{
    partial class ProjectAreaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectAreaForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.propertyList = new EasyMap.Controls.MyDataGridView();
            this.property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.comProject = new System.Windows.Forms.ComboBox();
            this.btnOpenProjectDocPath = new EasyMap.Controls.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.propertyList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "宗地名称：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(75, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 12);
            this.lblName.TabIndex = 1;
            // 
            // propertyList
            // 
            this.propertyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyList.AutoRowHeight = false;
            this.propertyList.BackColor1 = System.Drawing.Color.White;
            this.propertyList.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.propertyList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.propertyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.propertyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.property,
            this.value});
            this.propertyList.ColumnSpanSettings = ((System.Collections.Generic.List<string>)(resources.GetObject("propertyList.ColumnSpanSettings")));
            this.propertyList.Location = new System.Drawing.Point(14, 75);
            this.propertyList.Name = "propertyList";
            this.propertyList.RowTemplate.Height = 21;
            this.propertyList.ShowLineNo = true;
            this.propertyList.Size = new System.Drawing.Size(464, 182);
            this.propertyList.TabIndex = 2;
            // 
            // property
            // 
            this.property.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.property.HeaderText = "属性";
            this.property.Name = "property";
            this.property.ReadOnly = true;
            this.property.Width = 54;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.HeaderText = "值";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "项目名称：";
            // 
            // comProject
            // 
            this.comProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProject.FormattingEnabled = true;
            this.comProject.Location = new System.Drawing.Point(77, 49);
            this.comProject.Name = "comProject";
            this.comProject.Size = new System.Drawing.Size(372, 20);
            this.comProject.TabIndex = 4;
            this.comProject.SelectedIndexChanged += new System.EventHandler(this.comProject_SelectedIndexChanged);
            // 
            // btnOpenProjectDocPath
            // 
            this.btnOpenProjectDocPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenProjectDocPath.Image = global::EasyMap.Properties.Resources.BlogOpenExisting;
            this.btnOpenProjectDocPath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenProjectDocPath.Location = new System.Drawing.Point(455, 47);
            this.btnOpenProjectDocPath.Name = "btnOpenProjectDocPath";
            this.btnOpenProjectDocPath.Size = new System.Drawing.Size(23, 23);
            this.btnOpenProjectDocPath.TabIndex = 5;
            this.btnOpenProjectDocPath.UseVisualStyleBackColor = true;
            this.btnOpenProjectDocPath.Click += new System.EventHandler(this.btnOpenProjectDocPath_Click);
            // 
            // ProjectAreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 272);
            this.Controls.Add(this.btnOpenProjectDocPath);
            this.Controls.Add(this.comProject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.propertyList);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(298, 272);
            this.Name = "ProjectAreaForm";
            this.Text = "宗地信息";
            this.Load += new System.EventHandler(this.ProjectAreaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.propertyList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private EasyMap.Controls.MyDataGridView propertyList;
        private System.Windows.Forms.DataGridViewTextBoxColumn property;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comProject;
        private EasyMap.Controls.MyButton btnOpenProjectDocPath;
    }
}