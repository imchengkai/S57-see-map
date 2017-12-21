namespace EasyMap
{
    partial class ParameterSettingForm
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
            this.txtProjectDocPath = new System.Windows.Forms.TextBox();
            this.myButton1 = new EasyMap.Controls.MyButton();
            this.btnOk = new EasyMap.Controls.MyButton();
            this.btnClose = new EasyMap.Controls.MyButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目文档路径：";
            // 
            // txtProjectDocPath
            // 
            this.txtProjectDocPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectDocPath.BackColor = System.Drawing.Color.White;
            this.txtProjectDocPath.Location = new System.Drawing.Point(112, 37);
            this.txtProjectDocPath.Name = "txtProjectDocPath";
            this.txtProjectDocPath.ReadOnly = true;
            this.txtProjectDocPath.Size = new System.Drawing.Size(236, 21);
            this.txtProjectDocPath.TabIndex = 1;
            // 
            // myButton1
            // 
            this.myButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myButton1.Font = new System.Drawing.Font("宋体", 2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myButton1.Location = new System.Drawing.Point(354, 37);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(24, 23);
            this.myButton1.TabIndex = 2;
            this.myButton1.Text = "...";
            this.myButton1.UseVisualStyleBackColor = true;
            this.myButton1.Click += new System.EventHandler(this.myButton1_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(222, 76);
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
            this.btnClose.Location = new System.Drawing.Point(303, 76);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ParameterSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(393, 110);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.myButton1);
            this.Controls.Add(this.txtProjectDocPath);
            this.Controls.Add(this.label1);
            this.LoadDefaultValues = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 110);
            this.Name = "ParameterSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统参数设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProjectDocPath;
        private EasyMap.Controls.MyButton myButton1;
        private EasyMap.Controls.MyButton btnOk;
        private EasyMap.Controls.MyButton btnClose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}