namespace Tax
{
    partial class loginPic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginPic));
            //this.axShockwaveFlash1 = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogin = new EasyMap.Controls.MyButton();
            //((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axShockwaveFlash1
            // 
           // this.axShockwaveFlash1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            //resources.ApplyResources(this.axShockwaveFlash1, "axShockwaveFlash1");
            //this.axShockwaveFlash1.Name = "axShockwaveFlash1";
            //this.axShockwaveFlash1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axShockwaveFlash1.OcxState")));
            //this.axShockwaveFlash1.MouseCaptureChanged += new System.EventHandler(this.axShockwaveFlash1_MouseCaptureChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnLogin);
            //this.panel1.Controls.Add(this.axShockwaveFlash1);
            this.panel1.Name = "panel1";
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.ForeColor = System.Drawing.Color.Transparent;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // loginPic
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BorderWidth = 1;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = false;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "loginPic";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            //((System.ComponentModel.ISupportInitialize)(this.axShockwaveFlash1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.PictureBox picFlash2;
        //private System.Windows.Forms.PictureBox picFlash1;
        private System.Windows.Forms.Panel panel1;
        private EasyMap.Controls.MyButton btnLogin;
    }
}