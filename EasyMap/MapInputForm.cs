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
    public partial class MapInputForm : MyForm
    {
        private string _MapName = "";
        private string _MapComment = "";
        private DialogResult _Result = DialogResult.Cancel;

        public DialogResult Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        public string MapComment
        {
            get { return _MapComment; }
            set { _MapComment = value; }
        }

        public string MapName
        {
            get { return _MapName; }
            set { _MapName = value; }
        }
        public MapInputForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入地图名称。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataTable table = MapDBClass.GetMapList();
            for(int i=0;i<table.Rows.Count;i++)
            {
                if (table.Rows[i]["MapName"].ToString() == textBox1.Text.Trim())
                {
                    MessageBox.Show("地图名称重复，请重新输入。","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    textBox1.SelectAll();
                    textBox1.Focus();
                    return;
                }
            }
            MapName = textBox1.Text;
            MapComment = textBox2.Text;
            Result = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }

        private void MapInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2_Click(null, null);
            }
        }
    }
}
