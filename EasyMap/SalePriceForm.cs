using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace EasyMap
{
    public partial class SalePriceForm : MyForm
    {
        private decimal _MapId = 0;
        private decimal _LayerId = 0;
        private decimal _ObjectId = 0;
        private DataTable _datatable = null;
        private bool _AllowEdit = false;

        public bool AllowEdit
        {
            get { return _AllowEdit; }
            set 
            {
                _AllowEdit = value;
                dataGridView1.ReadOnly = !value;
                panel1.Enabled = value;
                btnOk.Enabled = value;
            }
        }

        public decimal ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        public decimal LayerId
        {
            get { return _LayerId; }
            set { _LayerId = value; }
        }

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

        public SalePriceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initial()
        {
            dataGridView1.Rows.Clear();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        public void Initial(decimal mapid, decimal layerid, decimal objectid)
        {
            _MapId = mapid;
            _LayerId = layerid;
            _ObjectId = objectid;
            _datatable = MapDBClass.GetSalePrice(mapid, layerid, objectid);
            for (int i = _datatable.Columns.Count - 1; i >= 0; i--)
            {
                bool find = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Columns[j].DataPropertyName == _datatable.Columns[i].ColumnName)
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    _datatable.Columns.RemoveAt(i);
                }
            }
            SetData(_datatable);
        }

        private void SetData(DataTable table)
        {
            dataGridView1.Rows.Clear();
            if(table.Rows.Count<=0)
            {
                return;
            }
            dataGridView1.RowCount=table.Rows.Count+1;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = table.Rows[i][0];
                dataGridView1.Rows[i].Cells[1].Value = table.Rows[i][1];
                dataGridView1.Rows[i].Cells[2].Value = table.Rows[i][2];
                dataGridView1.Rows[i].Cells[3].Value = table.Rows[i][3];
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            List<Hashtable> data = new List<Hashtable>();
            int index = 0;
            bool change = _datatable.Rows.Count != dataGridView1.RowCount-1;
            if (!change)
            {
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (GetValue(dataGridView1, i, j) != _datatable.Rows[i][j].ToString())
                        {
                            change = true;
                            break;
                        }
                    }
                }
            }
            if (!change)
            {
                return;
            }
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells["SaleDate"].Value != null)
                {
                    string sdate = dataGridView1.Rows[i].Cells["SaleDate"].Value.ToString();
                    string saleprice = dataGridView1.Rows[i].Cells["SalePrice"].Value.ToString();
                    string price = dataGridView1.Rows[i].Cells["Price"].Value.ToString();
                    string updatedate = dataGridView1.Rows[i].Cells["updatedate"].Value.ToString();
                    Hashtable table = new Hashtable();
                    table.Add("SaleDate", sdate);
                    table.Add("SalePrice", saleprice);
                    table.Add("Price", price);
                    table.Add("No", index);
                    table.Add("Updatedate", updatedate);
                    data.Add(table);
                    index++;
                }
            }
            //保存价格数据
            MapDBClass.UpdateSalePrice(_MapId, _LayerId, _ObjectId, data);
            Initial(_MapId, _LayerId, _ObjectId);
        }

        private void SalePriceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(null, null);
            }
        }

        private string GetValue(DataGridView view,int row,int col)
        {
            if(view==null||row>=view.RowCount||col>=view.ColumnCount)
            {
                return "";
            }
            if(view.Rows[row].Cells[col].Value==null)
            {
                return "";
            }
            return view.Rows[row].Cells[col].Value.ToString();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                DateTime date;
                if (!DateTime.TryParse(GetValue(dataGridView1, e.RowIndex, e.ColumnIndex), out date))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date.Year.ToString() + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0');
                }
            }
            else if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                decimal num = 0;
                if (!decimal.TryParse(GetValue(dataGridView1, e.RowIndex, e.ColumnIndex), out num))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
        }

        private void paste_Click(object sender, EventArgs e)
        {
            Common.PasteToGrid(dataGridView1, true);
        }
    }
}
