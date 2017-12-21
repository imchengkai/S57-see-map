namespace EasyMap
{
    partial class ParameterForm
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
            this.myButton1 = new EasyMap.Controls.MyButton();
            this.myButton2 = new EasyMap.Controls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // myButton1
            // 
            this.myButton1.Location = new System.Drawing.Point(226, 227);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(75, 23);
            this.myButton1.TabIndex = 0;
            this.myButton1.Text = "确定";
            this.myButton1.UseVisualStyleBackColor = true;
            this.myButton1.Click += new System.EventHandler(this.myButton1_Click);
            // 
            // myButton2
            // 
            this.myButton2.Location = new System.Drawing.Point(307, 227);
            this.myButton2.Name = "myButton2";
            this.myButton2.Size = new System.Drawing.Size(75, 23);
            this.myButton2.TabIndex = 1;
            this.myButton2.Text = "取消";
            this.myButton2.UseVisualStyleBackColor = true;
            this.myButton2.Click += new System.EventHandler(this.myButton2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "空间坐标X轴上的平移参数dx（米）：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "空间坐标Y轴上的平移参数dy（米）：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "空间坐标Z轴上的平移参数dz（米）：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "K是缩放尺度K（ppm）：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "X轴旋转尺度wx（分）：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Y轴旋转尺度wy（分）：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "Z轴旋转尺度wz（分）：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(225, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 21);
            this.textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(225, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(157, 21);
            this.textBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(225, 93);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(157, 21);
            this.textBox3.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(225, 120);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(157, 21);
            this.textBox4.TabIndex = 12;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(225, 147);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(157, 21);
            this.textBox5.TabIndex = 13;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(225, 174);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(157, 21);
            this.textBox6.TabIndex = 14;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(225, 199);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(157, 21);
            this.textBox7.TabIndex = 15;
            // 
            // ParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myButton2);
            this.Controls.Add(this.myButton1);
            this.LoadDefaultValues = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParameterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "导入参数";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MyButton myButton1;
        private Controls.MyButton myButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;

    }
}