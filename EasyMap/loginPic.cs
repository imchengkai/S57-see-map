using EasyMap;
using EasyMap.Geometries;
using EasyMap.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap;
using EasyMap.Data.Providers;
using MapLib.Utilities;
using System.Data.SqlClient;
using ControlLib;
using System.Drawing.Printing;
using System.Collections;
using System.Resources;
using System.Reflection;

namespace Tax
{
    public partial class loginPic : MyForm
    {
        public loginPic()
        {
            InitializeComponent();
            //axShockwaveFlash1.Visible = true;
            //axShockwaveFlash1.Movie = AppDomain.CurrentDomain.BaseDirectory + "earth.swf";
            //axShockwaveFlash1.Play();
            //axShockwaveFlash1.BringToFront();
        }
        public void loginPic_Load(object sender, EventArgs e)
        {

        }
        private void axShockwaveFlash1_MouseCaptureChanged(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         // 按键处理
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
        // 按键处理
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }
    }
}
