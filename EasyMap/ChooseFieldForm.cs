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
    public partial class ChooseFieldForm : MyForm
    {
        private List<string> _Fields = new List<string>();
        private string _SelectField;

        public string SelectField
        {
            get { return _SelectField; }
            set { _SelectField = value; }
        }

        public List<string> Fields
        {
            get { return _Fields; }
            set 
            { 
                _Fields = value;
                comboBox1.Items.Clear();
                foreach (string field in value)
                {
                    comboBox1.Items.Add(field);
                }
            }
        }
        public ChooseFieldForm()
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
            SelectField = comboBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
