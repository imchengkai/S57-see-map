using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Geometries;
using EasyMap.Forms;
using System.IO;
using CoorCon;

namespace EasyMap
{
    public partial class InputPolygonSelectForm : MyForm
    {
        public InputPolygonSelectForm()
        {
            InitializeComponent();
        }
        private void InputGeometryForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton3.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            radioButton1.Checked = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //手动绘制图形
            if (radioButton1.Checked == true)
            { 
                //跳转到主界面进行绘制
            }
            //导入界址点坐标
            else if (radioButton2.Checked == true)
            {
                InputGeometryForm form = new InputGeometryForm();
                form.Show(this);
                this.Visible = false;
            }
            //导入SHP文件
            else if (radioButton3.Checked == true)
            { 
            
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

    }
}
