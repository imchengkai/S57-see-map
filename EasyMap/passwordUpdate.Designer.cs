namespace Tax
{
    partial class passwordUpdate
    {/// <summary>
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.password = new System.Windows.Forms.TextBox();
            this.password_confirm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.user_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MyPrintDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.close);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.password);
            this.panel2.Controls.Add(this.password_confirm);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.user_name);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 208);
            this.panel2.TabIndex = 8;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(116, 52);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(114, 21);
            this.password.TabIndex = 13;
            this.password.UseSystemPasswordChar = true;
            // 
            // password_confirm
            // 
            this.password_confirm.Location = new System.Drawing.Point(116, 83);
            this.password_confirm.Name = "password_confirm";
            this.password_confirm.Size = new System.Drawing.Size(114, 21);
            this.password_confirm.TabIndex = 12;
            this.password_confirm.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "确认密码：";
            // 
            // user_name
            // 
            this.user_name.Location = new System.Drawing.Point(116, 18);
            this.user_name.Name = "user_name";
            this.user_name.ReadOnly = true;
            this.user_name.Size = new System.Drawing.Size(114, 21);
            this.user_name.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "用户名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "新密码：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(3, 210);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(311, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.MyPrintDocument;
            this.printDialog1.UseEXDialog = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "*.csv|*.csv";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(34, 130);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(172, 130);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 15;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // passwordUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 235);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel2);
            this.MinimizeBox = false;
            this.Name = "passwordUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码修改";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Drawing.Printing.PrintDocument MyPrintDocument;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox password_confirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button btnSave;

    }
}