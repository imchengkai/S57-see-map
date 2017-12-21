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
    public partial class ProjectForm : MyForm
    {
        public ProjectForm()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control obj in groupBox2.Controls)
            {
                if (obj is TextBox)
                {
                    ((TextBox)obj).Text = "";
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnClear_Click(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnClear_Click(sender, e);
        }

        private bool CheckInput()
        {
            if (txtYear.Text.Trim() == "")
            {
                MessageBox.Show("请设置项目年度。");
                txtYear.Focus();
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("请设置项目名称。");
                txtName.Focus();
                return false;
            }
            
            return true;
        }
    }
}
