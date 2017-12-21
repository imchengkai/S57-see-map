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
    public partial class InputKeyForm : MyForm
    {
        public InputKeyForm()
        {
            InitializeComponent();
            textBox1.Text = Common.GetCpuID();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.SetKey(textBox2.Text);
            MessageBox.Show("系统需要重新启动才能验证序号的有效性。");
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
