// Copyright 2007 - Rory Plaire (codekaizen@gmail.com)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using EasyMap.Controls;
using EsayMap;
namespace EasyMap
{
    partial class login
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
			
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btnLogin = new EasyMap.Controls.MyButton();
            this.close = new EasyMap.Controls.MyButton();
            this.SuspendLayout();
            // 
            // txt_name
            // 
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.Name = "txt_name";
            // 
            // txt_password
            // 
            resources.ApplyResources(this.txt_password, "txt_password");
            this.txt_password.Name = "txt_password";
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.ForeColor = System.Drawing.Color.Transparent;
            this.btnLogin.Image = global::EasyMap.Properties.Resources.µÇÂ¼1;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // close
            // 
            resources.ApplyResources(this.close, "close");
            this.close.BackColor = System.Drawing.Color.Transparent;
            this.close.BackgroundImage = global::EasyMap.Properties.Resources.AdpDiagramDeleteTable;
            this.close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.close.Image = global::EasyMap.Properties.Resources.AdpDiagramDeleteTable;
            this.close.Name = "close";
            this.close.UseVisualStyleBackColor = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.close);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "login";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Login_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_password;
        private MyButton btnLogin;
        private MyButton close;

    }
}

