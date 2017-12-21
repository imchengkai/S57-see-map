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
    public partial class ZoomListForm : MyForm
    {
        private string _IniFile = "ClientSetting.ini";
        private string _Section = "ZoomSettings";
        private string _KeyCount = "Count";
        private string _Key = "Zoom";
        private string[] _DefaultZoomList = {   "1:1,000"
                                                ,"1:10,000"
                                                ,"1:25,000"
                                                ,"1:100,000"
                                                ,"1:250,000"
                                                ,"1:500,000"
                                                ,"1:750,000"
                                                ,"1:1,000,000"
                                                ,"1:3,000,000"
                                                ,"1:10,000,000"};

        public ZoomListForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            LoadZoom();
        }

        public List<string> LoadZoom()
        {
            List<string> ret = new List<string>();
            string scount = Common.IniReadValue(_IniFile, _Section, _KeyCount);
            int count = 0;
            int.TryParse(scount, out count);
            if (count == 0)
            {
                foreach (string szoom in _DefaultZoomList)
                {
                    int row = myDataGridView1.Rows.Add();
                    myDataGridView1.Rows[row].Cells[0].Value = szoom;
                    ret.Add(szoom);
                }
            }
            else
            {
                for (int i = 1; i <= count; i++)
                {
                    string szoom = Common.IniReadValue(_IniFile, _Section, _Key + i);
                    int row = myDataGridView1.Rows.Add();
                    myDataGridView1.Rows[row].Cells[0].Value = szoom;
                    ret.Add(szoom);
                }
            }
            ret.Add("<自定义此列表...>");
            return ret;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myDataGridView1.RowCount; i++)
            {
                string szoom = myDataGridView1.GetGridValue(i, 0).ToString().Trim();
                if (szoom == "")
                {
                    continue;
                }
                szoom = szoom.Replace(",", "").Replace("：", ":");
                string[] list = szoom.Split(':');
                double zoom = 0;
                if (list.Length < 2)
                {
                    double.TryParse(szoom, out zoom);
                }
                else
                {
                    double zoom1, zoom2;
                    double.TryParse(list[0], out zoom1);
                    double.TryParse(list[1], out zoom2);
                    if (zoom1 != 0)
                    {
                        zoom = zoom2 / zoom1;
                    }
                }
                if (zoom <= 0)
                {
                    myDataGridView1.ClearSelection();
                    myDataGridView1.Rows[i].Selected = true;
                    MessageBox.Show("比例尺设置错误。请重新设置(例如：1:1000或者1000)。");
                    return;
                }
                myDataGridView1.Rows[i].Cells[1].Value = zoom;
            }
            myDataGridView1.Sort(myDataGridView1.Columns[1], ListSortDirection.Ascending);
            int index = 0;
            for (int i = 0; i < myDataGridView1.RowCount; i++)
            {
                string szoom = myDataGridView1.GetGridValue(i, 0).ToString().Trim();
                if (szoom == "")
                {
                    continue;
                }
                index++;
                Common.IniWriteValue(_IniFile, _Section, _Key + index, szoom);
            }
            Common.IniWriteValue(_IniFile, _Section, _KeyCount, index.ToString());
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
