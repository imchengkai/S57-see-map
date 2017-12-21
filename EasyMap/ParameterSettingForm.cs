using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyMap
{
    public partial class ParameterSettingForm : MyForm
    {
        private const string _SettingFileName = "Setting.ini";
        private const string _Section = "Setting";
        private const string _Key = "ProjectDocPath";

        public ParameterSettingForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtProjectDocPath.Text = Common.IniReadValue(_SettingFileName, _Section, _Key);
        }

        private void SaveSettings()
        {
            Common.IniWriteValue(_SettingFileName, _Section, _Key, txtProjectDocPath.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtProjectDocPath.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtProjectDocPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
