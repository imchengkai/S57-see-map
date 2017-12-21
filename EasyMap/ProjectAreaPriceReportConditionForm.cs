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
    public partial class ProjectAreaPriceReportConditionForm : MyForm
    {
        private int _StartYear = 0;
        private int _EndYear = 0;

        public int EndYear
        {
            get { return _EndYear; }
            set { _EndYear = value; numericUpDown2.Value =  value; }
        }

        public int StartYear
        {
            get { return _StartYear; }
            set { _StartYear = value; numericUpDown1.Value = value; }
        }
        public ProjectAreaPriceReportConditionForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            StartYear = (int)numericUpDown1.Value;
            EndYear = (int)numericUpDown2.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ProjectAreaPriceReportConditionForm_Shown(object sender, EventArgs e)
        {
            numericUpDown1.Value = _StartYear;
            numericUpDown2.Value = _EndYear;
        }
    }
}
