using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyMap.UI.Forms
{
    public partial class InputStringForm : MyForm
    {
        private string _InputContext = "";

        public string InputContext
        {
            get { return _InputContext; }
            set { _InputContext = value; }
        }

        public InputStringForm(string title,string defaultValue)
        {
            InitializeComponent();
            label1.Text = title;
            textBox1.Text = defaultValue;
            textBox1.SelectAll();
            textBox1.Focus();
            InputContext = defaultValue;
            DialogResult = DialogResult.Cancel;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnCancel_Click(sender, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender, null);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            InputContext = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
