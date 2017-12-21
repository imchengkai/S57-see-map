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
    public partial class ZoomVisibleSetForm : MyForm
    {
        private double _MaxVisible;
        private double _MinVisible;

        public double MinVisible
        {
            get { return _MinVisible; }
            set { _MinVisible = value; maskedTextBox1.Text = value.ToString(); }
        }

        public double MaxVisible
        {
            get { return _MaxVisible; }
            set { _MaxVisible = value; maskedTextBox2.Text = value.ToString(); }
        }
        public ZoomVisibleSetForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            double v1 = 0;
            double v2 = 0;
            double.TryParse(maskedTextBox1.Text.Replace(" ",""), out v1);
            double.TryParse(maskedTextBox2.Text.Replace(" ",""), out v2);
            MaxVisible = Math.Max(v1, v2);
            MinVisible = Math.Min(v1, v2);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
