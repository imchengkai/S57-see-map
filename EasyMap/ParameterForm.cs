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
    public partial class ParameterForm : MyForm
    {
        private double[] _Parameters = new double[7];

        public double[] Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }
        public ParameterForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out _Parameters[0])
                || !double.TryParse(textBox2.Text, out _Parameters[1])
                || !double.TryParse(textBox3.Text, out _Parameters[2])
                || !double.TryParse(textBox4.Text, out _Parameters[3])
                || !double.TryParse(textBox5.Text, out _Parameters[4])
                || !double.TryParse(textBox6.Text, out _Parameters[5])
                || !double.TryParse(textBox7.Text, out _Parameters[6]))
            {
                MessageBox.Show("请输入正确的数据。");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
