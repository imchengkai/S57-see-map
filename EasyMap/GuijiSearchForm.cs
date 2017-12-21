using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyMap.UI.Forms
{
    public partial class GuijiSearchForm : MyForm
    {
        private string _InputContext = "";
        private DateTime _dateBegin = new DateTime();
        private DateTime _dateOff = new DateTime();
        public DateTime dateBegin;
        //{
        //    get { return _dateBegin; }
        //    set { _dateBegin = value; }
        //}
        public DateTime dateOff;
        //{
        //    get { return _dateOff; }
        //    set { _dateOff = value; }
        //}
        public string InputContext
        {
            get { return _InputContext; }
            set { _InputContext = value; }
        }

        public GuijiSearchForm(string name, DateTime dateBegin, DateTime dateOff)
        {
            InitializeComponent();
            if (dateBegin != new DateTime())
            {
                date_begin.Value = dateBegin;
            }
            else
            {
                date_begin.Value = DateTime.Now.AddDays(-1);
            }
            if (dateOff != new DateTime())
            {
                date_off.Value = dateOff;
            }
            else
            {
                date_off.Value = DateTime.Now;
            }
            textBox1.Text = name;
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
            dateBegin = date_begin.Value;
            dateOff = date_off.Value;
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
