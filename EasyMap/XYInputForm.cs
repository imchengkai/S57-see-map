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
    public partial class XYInputForm : MyForm
    {
        private double _XX = 0;
        private double _YY = 0;
        private double _R = 0;

        public double R
        {
            get { return _R; }
            set { _R = value; txtR.Text = value.ToString(); }
        }

        public double YY
        {
            get { return _YY; }
            set { _YY = value; txtY.Text = value.ToString(); }
        }

        public double XX
        {
            get { return _XX; }
            set { _XX = value; txtX.Text = value.ToString(); }
        }
        public XYInputForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            double x = 0;
            double y = 0;
            double r = 0;
            if (!double.TryParse(txtX.Text.Replace(",","").Trim(), out x))
            {
                MessageBox.Show("请输入正确的X坐标。");
                txtX.SelectAll();
                txtX.Focus();
                return;
            }
            if (!double.TryParse(txtY.Text.Replace(",", "").Trim(), out y))
            {
                MessageBox.Show("请输入正确的Y坐标。");
                txtY.SelectAll();
                txtY.Focus();
                return;
            }
            if (!double.TryParse(txtR.Text.Replace(",", "").Trim(), out r))
            {
                MessageBox.Show("请输入正确的半径。");
                txtR.SelectAll();
                txtR.Focus();
                return;
            }
            XX = x;
            YY = y;
            R = r;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
