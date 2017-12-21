using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EasyMap
{
    public partial class ServerSettingForm : MyForm
    {
        public ServerSettingForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = ipTextBox1.Text;
            string[] list = ip.Split('.');
            bool flag=true;
            foreach(string txt in list)
            {
                int data=0;
                if(!Int32.TryParse(txt,out data))
                {
                    flag=false;
                    break;
                }
                if(data>255||data<0)
                {
                    flag=false;
                    break;
                }
            }
            if (!flag||list.Length!=4)
            {
                MessageBox.Show("请正确设置服务器IP地址。");
                
                ipTextBox1.Focus();
                return;
            }
            Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "ServerConnect", "IP", ipTextBox1.Text.Trim().Replace(" ", ""));
            Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "ServerConnect", "Port", numericUpDown1.Value.ToString());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ServerSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCanel_Click(null, null);
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Command._ConnectServer = false;
            Close();
        }
    }
}
