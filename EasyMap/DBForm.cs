using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EasyMap.Forms;
using EasyMap.Layers;
using System.Collections;
using EasyMap.Geometries;
using EasyMap.Data.Providers;
using EasyMap.Controls;
using ControlLib;

namespace EasyMap
{
    public partial class DBForm : MyForm
    {
        private decimal _MapId = 0;
        private Hashtable _PropertyType = new Hashtable();
        private List<string> _PropertyName = new List<string>();
        public delegate void SelectObjectEvent(VectorLayer layer, Geometry geom);
        public event SelectObjectEvent SelectObject;
        private MyGeometryList _SelectObjects = new MyGeometryList();
        private List<ILayer> _SelectLayers = new List<ILayer>();
        public delegate void OpenTuDiZhengByDihao(string bianhao);
        public event OpenTuDiZhengByDihao OpenTuDiZheng;

        public List<ILayer> SelectLayers
        {
            get { return _SelectLayers; }
            set { _SelectLayers = value; }
        }

        public MyGeometryList SelectObjects
        {
            get { return _SelectObjects; }
            set { _SelectObjects = value; }
        }

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

        public DBForm(decimal mapid)
        {
            InitializeComponent();
        }

        public void Initial(decimal mapid)
        {
            MapId = mapid;
            AddLayerButton();
            dataGridView2.Rows.Clear();
            button2_Click(null, null);
            dataGridView1_SelectionChanged(null, null);
        }

        /// <summary>
        /// 导出选择的单元格到CSV文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()!=DialogResult.OK)
                return;
            //Common.ExportDataGridViewToCsv(dataGridView1, saveFileDialog1.FileName);
            Utils.ExportDataGridViewToCsv(dataGridView1, saveFileDialog1.FileName, true);
            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, true, Encoding.Default);
            sw.Close();
        }

        /// <summary>
        /// 关闭窗口按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 取得表格数据
        /// </summary>
        /// <param name="view"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private string GetValue(DataGridView view, int row, int col)
        {
            if(view.RowCount<=row||view.ColumnCount<=col||row<0||col<0)
            {
                return "";
            }
            if (view.Rows[row].Cells[col].Value == null)
            {
                return "";
            }
            return view.Rows[row].Cells[col].Value.ToString().Trim();
        }

        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <returns></returns>
        private string CreateWhere()
        {
            string where = " where (";
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                string field = "\""+GetValue(dataGridView2,i, 0)+"\"";
                string condition = GetValue(dataGridView2, i, 1);
                if (condition == "不等于")
                {
                    condition = "<>";
                }
                else if (condition == "大约")
                {
                    condition = " like ";
                }
                string data = GetValue(dataGridView2, i, 2);
                string andor=GetValue(dataGridView2,i,3);
                if (GetValue(dataGridView2,i, 0) == "日期")
                {
                    field = "\"propertydate\"";
                }
                if(andor=="或者")
                {
                    andor=" or  ";
                }
                else
                {
                    andor=" and ";
                }
                if (field == "" || condition == "" || data == "")
                {
                    continue;
                }
                data = data.Replace("'", "''");
                bool isdigital=false;
                string colname = GetValue(dataGridView2, i, 0);
                if (_PropertyType[colname].ToString() == typeof(int).Name
                    || _PropertyType[colname].ToString() == typeof(float).Name
                     || _PropertyType[colname].ToString() == typeof(double).Name)
                {
                    isdigital=true;
                    field="convert(numeric,"+field+")";
                }
                else if (_PropertyType[colname].ToString() == typeof(DateTime).Name)
                {
                    isdigital = true;
                    field = "convert(varchar,"+field+",111)";
                }
                where += field + condition;
                if (isdigital)
                {
                    where += data;
                }
                else
                {
                    if (condition.IndexOf("like") >= 0)
                    {
                        where += "'%" + data + "%'";
                    }
                    else
                    {
                        where += "'" + data + "'";
                    }
                }
                where += "\r\n"+andor;
            }
            if (!where.EndsWith("("))
            {
                if (where.Length > 5)
                {
                    where = where.Substring(0, where.Length - 5);
                }
                where += ") and ";
            }
            else
            {
                where = " where ";
            }
            where += " mapid=" + MapId.ToString() + " and layerid=" + GetCurrentLayerId().ToString();
            return where;
        }

        /// <summary>
        /// 生成查询字段
        /// </summary>
        /// <returns></returns>
        private string CreateSelect(int rowcount)
        {
            if (_PropertyName.Count <= 0)
            {
                return "";
            }
            string select = "select propertyDate as 输入日期,\r\n";
            if (rowcount > 0)
            {
                select = "select top "+rowcount+" propertyDate as 输入日期,\r\n";
            }
            foreach (string key in _PropertyName)
            {
                select += "\"" + key + "\"\r\n,";
            }
            select += "\"ObjectId\"\r\n";
            return select;
        }

        /// <summary>
        /// 查询条件数据输入验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView view = dataGridView2;
            if (_PropertyType[GetValue(view, e.RowIndex, 0)] == null)
            {
                return;
            }
            string datatype = _PropertyType[GetValue(view, e.RowIndex, 0)].ToString();
            if (e.ColumnIndex == 1)
            {
                if (datatype == typeof(int).Name
                    || datatype == typeof(float).Name
                    || datatype == typeof(double).Name)
                {
                    if (GetValue(view, e.RowIndex, e.ColumnIndex) == "大约")
                    {
                        MessageBox.Show("数值类型数据不能使用【大约】来查询。请重新设置");
                        view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }
                }
            }
            if (e.ColumnIndex != 2)
            {
                return;
            }
            if (!_PropertyType.ContainsKey(GetValue(view, e.RowIndex, 0)))
            {
                return;
            }
            
            string data = GetValue(view, e.RowIndex, e.ColumnIndex);
            if (datatype == typeof(int).Name)
            {
                int num = 0;
                if (!Int32.TryParse(data, out num))
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
            else if (datatype == typeof(float).Name||datatype==typeof(double).Name)
            {
                double num = 0;
                if (!double.TryParse(data, out num))
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
            else if (datatype == "日期"||datatype==typeof(DateTime).Name)
            {
                DateTime date;
                if (!DateTime.TryParse(data, out date))
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
                else
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date.Year.ToString() + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0');
                }
            }

        }

        private DataTable Search()
        {
            //dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            _PropertyType.Clear();
            _PropertyName.Clear();
            //dataGridView1.DataSource = null;
            DataGridViewComboBoxColumn col = dataGridView2.Columns[0] as DataGridViewComboBoxColumn;
            decimal layerid = GetCurrentLayerId();
            if (layerid < 0)
            {
                dataGridView1_CurrentCellChanged(null, null);
                return null;
            }
            List<List<string>> list = new List<List<string>>();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                bool flag = true;
                for (int j = 0; j < 3; j++)
                {
                    if (dataGridView2.GetGridValue(i, j).ToString().Trim() == "")
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    List<string> templist = new List<string>();
                    for (int j = 0; j < 4; j++)
                    {
                        templist.Add(dataGridView2.GetGridValue(i, j).ToString().Trim());
                    }
                    list.Add(templist);
                }
            }
            dataGridView2.Rows.Clear();
            col.Items.Clear();
            //取得真实属性表字段
            string sql = "select * from " + MapDBClass.GetPropertyTableName(MapId, layerid) + " where 1<>1";
            DataTable table = SqlHelper.Select(sql, null);
            for (int i = 4; i < table.Columns.Count; i++)
            {
                string colname = table.Columns[i].ColumnName.ToLower();
                string stype = table.Columns[i].DataType.Name;
                colname = table.Columns[i].ColumnName;
                col.Items.Add(colname);
                _PropertyType.Add(colname, stype);
                _PropertyName.Add(colname);
            }
            col.Items.Add("日期");
            _PropertyType.Add("日期", "日期");
            foreach (List<string> templist in list)
            {
                int row = dataGridView2.Rows.Add();
                for (int j = 0; j < 4; j++)
                {
                    dataGridView2.Rows[row].Cells[j].Value=templist[j];
                }
            }

            //取得条件
            string where = CreateWhere();
            //取得检索字段
            string select = CreateSelect(0);
            //取得表名
            string tablename = GetTableName();
            sql = select + "\r\n from \r\n" + tablename + where;
            //查询
            table = SqlHelper.Select(sql, null);
            return table;
        }
        /// <summary>
        /// 检索按钮处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            DataTable table = Search();
            SetDataToGrid(table);
            if (btnSelect.Checked)
            {
                btnSelect_Click(sender, e);
            }
            dataGridView1_CurrentCellChanged(sender, e);
        }

        private void SetDataToGrid(DataTable table)
        {

            //查询内容显示
            dataGridView1.Columns.Clear();
            if (table == null)
            {
                return;
            }
            DataGridViewTextBoxColumn column = null;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                DataColumn col = table.Columns[i];
                column = new DataGridViewTextBoxColumn();
                column.HeaderText = col.ColumnName;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                column.DataPropertyName = col.ColumnName;
                dataGridView1.Columns.Add(column);
            }
            if (column != null)
            {
                column.Visible = false;
            }
            //dataGridView1.Rows.Clear();
            dataGridView1.DataSource = table;
            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    dataGridView1.Rows.Add();
            //    for (int j = 0; j < table.Columns.Count; j++)
            //    {
            //        if (table.Rows[i][j] == null)
            //        {
            //            dataGridView1.Rows[i].Cells[j].Value = "";
            //        }
            //        else
            //        {
            //            dataGridView1.Rows[i].Cells[j].Value = table.Rows[i][j].ToString();
            //        }
            //        if (_PropertyType.ContainsKey(dataGridView1.Columns[j].HeaderText))
            //        {
            //            object datatype = _PropertyType[dataGridView1.Columns[j].HeaderText];
            //            if (datatype.ToString() == typeof(int).Name)
            //            {
            //                int num = 0;
            //                Int32.TryParse(dataGridView1.Rows[i].Cells[j].Value.ToString(), out num);
            //                dataGridView1.Rows[i].Cells[j].Value = num;
            //            }
            //            else if (datatype.ToString() == typeof(float).Name||datatype.ToString()==typeof(double).Name)
            //            {
            //                decimal num = 0;
            //                decimal.TryParse(dataGridView1.Rows[i].Cells[j].Value.ToString(), out num);
            //                dataGridView1.Rows[i].Cells[j].Value = num;
            //            }
            //        }

            //    }
            //}
        }

        /// <summary>
        /// 根据图层类型，取得该查询哪一个表的表名
        /// </summary>
        /// <returns></returns>
        private string GetTableName()
        {
            return MapDBClass.GetPropertyTableName(MapId, GetCurrentLayerId());
        }

        /// <summary>
        /// 日期选择控件失去焦点时，关闭该控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_Leave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        /// <summary>
        /// 日期选择后，关闭日期选择控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime date=monthCalendar1.SelectionStart;
            string val=date.Year.ToString()+"/"+date.Month.ToString().PadLeft(2,'0')+"/"+date.Day.ToString().PadLeft(2,'0');
            dataGridView2.CurrentCell.Value = val;
            monthCalendar1.Visible = false;
        }

        /// <summary>
        /// 当检索数据单元格获得焦点时，如果是日期输入，那么打开日期选择控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2)
            {
                return;
            }
            DataGridView view = dataGridView2;
            if (GetValue(view, e.RowIndex, 0) == "")
            {
                return;
            }
            
            string datatype = _PropertyType[GetValue(view, e.RowIndex, 0)].ToString();
            string data = GetValue(view, e.RowIndex, e.ColumnIndex);
            if (datatype == "日期"||datatype==typeof(DateTime).Name)
            {
                DateTime date;
                if (DateTime.TryParse(data, out date))
                {
                    monthCalendar1.SelectionStart = date;
                }
                Rectangle rect= dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                monthCalendar1.Left =groupBox1.Left+ dataGridView2.Left+ rect.Left+2;
                monthCalendar1.Top = groupBox1.Top+ dataGridView2.Top + rect.Bottom + 2;
                monthCalendar1.Visible = true;
                monthCalendar1.Focus();
            }
        }

        /// <summary>
        /// 列选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                cell.Selected = false;
            }
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Rows[e.RowIndex].Cells[i].Selected = true;
            }
            if (SelectObject != null)
            {
                decimal id = 0;
                string val = GetValue(dataGridView1, e.RowIndex, dataGridView1.ColumnCount - 1);
                if (decimal.TryParse(val, out id))
                {
                    VectorLayer vlayer = GetCurrentLayer() as VectorLayer;
                    GeometryProvider datasource = vlayer.DataSource as GeometryProvider;
                    foreach (Geometry geom in _SelectObjects)
                    {
                        if (geom.ID==id && datasource.Geometries.Contains(geom))
                        {
                            SelectObject(vlayer, geom);
                            break;
                        }
                    }
                    
                }
            }
        }

        /// <summary>
        /// 设置查询条件设置区域可见性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int dis = int.Parse(pictureBox1.Tag.ToString());

            if (dis != 0)
            {
                pictureBox1.Tag = 0;
                splitContainer1.SplitterDistance = dis;
                pictureBox1.Image = EasyMap.Properties.Resources.up;
                自定义查询ToolStripMenuItem.Checked = true;
                pictureBox1.ToolTipText = "收起";
            }
            else
            {
                pictureBox1.Tag = splitContainer1.SplitterDistance;
                splitContainer1.SplitterDistance = 0;
                pictureBox1.Image = EasyMap.Properties.Resources.down;
                自定义查询ToolStripMenuItem.Checked = false;
                pictureBox1.ToolTipText = "展开";
            }
        }

        /// <summary>
        /// 设置翻页按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void DBForm_Shown(object sender, EventArgs e)
        {
            dataGridView1_SelectionChanged(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int col = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[0].Cells[col].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[col];
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[row-1].Cells[col].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[row - 1].Cells[col];
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[row + 1].Cells[col].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[row + 1].Cells[col];
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount - 1;
            int col = dataGridView1.CurrentCell.ColumnIndex;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[row].Cells[col].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
        }

        private void txtNo_Leave(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            int col = dataGridView1.CurrentCell.ColumnIndex;
            if (!int.TryParse(txtNo.Text.Trim(), out row))
            {
                txtNo.Text = row.ToString();
                return;
            }
            row--;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[row].Cells[col].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNo_Leave(null, null);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

            btnFirst.Enabled = dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.RowIndex > 0;
            btnPrevious.Enabled = btnFirst.Enabled;
            btnLast.Enabled = dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.RowIndex < dataGridView1.RowCount - 1;
            btnNext.Enabled = btnLast.Enabled;
            if (dataGridView1.CurrentCell == null)
            {
                txtNo.Text = "0";
            }
            else
            {
                txtNo.Text = (dataGridView1.CurrentCell.RowIndex + 1).ToString();
            }
            lblMsg.Text = "(" + SelectObjects.Count + "/" + dataGridView1.RowCount + "已选择)";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            btnSelect.Checked = true;
            btnAll.Checked = false;
            全部显示ToolStripMenuItem.Checked = false;
            仅显示选中元素数据ToolStripMenuItem.Checked = true;
            ILayer layer = GetCurrentLayer();
            if (layer is VectorLayer)
            {
                VectorLayer vlayer = layer as VectorLayer;
                GeometryProvider datasource = vlayer.DataSource as GeometryProvider;
                string id="";
                foreach (Geometry geom in _SelectObjects)
                {
                    if (datasource.Geometries.Contains(geom))
                    {
                        id+=geom.ID+",";
                    }
                }

                //dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                _PropertyType.Clear();
                _PropertyName.Clear();
                DataGridViewComboBoxColumn col = dataGridView2.Columns[0] as DataGridViewComboBoxColumn;
                col.Items.Clear();
                decimal layerid = GetCurrentLayerId();
                if (layerid < 0)
                {
                    dataGridView1_CurrentCellChanged(null, null);
                    return;
                }
                //取得真实属性表字段
                string sql = "select * from " + MapDBClass.GetPropertyTableName(MapId, layerid) + " where 1<>1";
                DataTable table = SqlHelper.Select(sql, null);
                for (int i = 4; i < table.Columns.Count; i++)
                {
                    string colname = table.Columns[i].ColumnName.ToLower();
                    string stype = table.Columns[i].DataType.Name;
                    colname = table.Columns[i].ColumnName;
                    col.Items.Add(colname);
                    _PropertyType.Add(colname, stype);
                    _PropertyName.Add(colname);
                }
                col.Items.Add("日期");
                _PropertyType.Add("日期", "日期");
                dataGridView2.Rows.Clear();

                //取得条件
                string where = CreateWhere();
                //取得检索字段
                string select = CreateSelect(0);
                //取得表名
                string tablename = GetTableName();
                sql = select + "\r\n from \r\n" + tablename + where;
                if (id != "")
                {
                    sql += " and ObjectId in (" + id.Substring(0, id.Length - 1) + ")";
                }
                //查询
                table = SqlHelper.Select(sql, null);
                dataGridView1.DataSource = table;
            }
            dataGridView1_CurrentCellChanged(sender, e);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            btnAll.Checked = true;
            btnSelect.Checked = false;
            全部显示ToolStripMenuItem.Checked = true;
            仅显示选中元素数据ToolStripMenuItem.Checked = false;
            button2_Click(sender, e);
        }

        /// <summary>
        /// 向工具条中田间图层按钮
        /// </summary>
        private void AddLayerButton()
        {
            foreach (ILayer layer in SelectLayers)
            {
                if (layer == null)
                {
                    continue;
                }
                bool find = false;
                for(int i=3;i<toolStrip2.Items.Count;i++)
                {
                    ToolStripItem item=toolStrip2.Items[i];
                    ILayer temp = item.Tag as ILayer;
                    if (temp.ID == layer.ID)
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    ToolStripButton btn=new ToolStripButton();
                    btn.Text=layer.LayerName;
                    btn.Tag=layer;
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Click += btn_Click;
                    toolStrip2.Items.Add(btn);
                    ToolStripMenuItem item = new ToolStripMenuItem(btn.Text);
                    item.CheckOnClick = true;
                    item.Tag = layer;
                    item.Click += new EventHandler(item_Click);
                    contextMenuStrip1.Items.Add(item);
                }
            }
            for (int i=toolStrip2.Items.Count-1;i>=3;i--)
            {
                ToolStripItem item=toolStrip2.Items[i];
                ILayer temp = item.Tag as ILayer;
                bool find = false;
                foreach (ILayer layer in SelectLayers)
                {
                    if (layer.ID == temp.ID)
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    toolStrip2.Items.Remove(item);
                    foreach (ToolStripItem item1 in contextMenuStrip1.Items)
                    {
                        if (item1 is ToolStripMenuItem)
                        {
                            ToolStripMenuItem submenu = item1 as ToolStripMenuItem;
                            if (temp != null && temp == submenu.Tag)
                            {
                                contextMenuStrip1.Items.Remove(submenu);
                                break;
                            }
                        }
                    }
                }
            }
            bool findcheck = false;
            for(int i=3;i<toolStrip2.Items.Count;i++)
            {
                ToolStripItem item = toolStrip2.Items[i];
                ToolStripButton btn = item as ToolStripButton;
                if (btn.Checked)
                {
                    findcheck = true;
                    break;
                }
            }
            if (!findcheck && toolStrip2.Items.Count > 3)
            {
                ToolStripButton btn = toolStrip2.Items[3] as ToolStripButton;
                btn.Checked = true;
                foreach (ToolStripItem item in contextMenuStrip1.Items)
                {
                    if (item is ToolStripMenuItem)
                    {
                        ToolStripMenuItem submenu = item as ToolStripMenuItem;
                        if (btn.Tag != null && btn.Tag == submenu.Tag)
                        {
                            submenu.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in contextMenuStrip1.Items)
            {
                if (item is ToolStripMenuItem && item.Tag is ILayer)
                {
                    ToolStripMenuItem submenu1 = item as ToolStripMenuItem;
                    submenu1.Checked = false;
                }
            }
            ToolStripMenuItem submenu = sender as ToolStripMenuItem;
            submenu.Checked = true;
            ToolStripButton selectbtn=null;
            for(int i=3;i<toolStrip2.Items.Count;i++)
            {
                ToolStripButton btn = toolStrip2.Items[i] as ToolStripButton;
                btn.Checked = false;
                if (submenu.Tag != null && submenu.Tag == btn.Tag)
                {
                    btn.Checked = true;
                    selectbtn=btn;
                }
            }
            if (selectbtn != null)
            {
                btn_Click(selectbtn, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 图层按钮点击处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Click(object sender, EventArgs e)
        {
            for(int i=3;i<toolStrip2.Items.Count;i++)
            {
                ToolStripItem item = toolStrip2.Items[i];
                ToolStripButton btn = item as ToolStripButton;
                btn.Checked = false;
            }
            ToolStripButton btnselect = sender as ToolStripButton;
            btnselect.Checked = true;

            foreach (ToolStripItem item in contextMenuStrip1.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem submenu1 = item as ToolStripMenuItem;
                    submenu1.Checked = false;
                    if (item.Tag == btnselect.Tag)
                    {
                        submenu1.Checked = true;
                    }
                }
            }
            button2_Click(null, null);
        }

        /// <summary>
        /// 取得当前选择的图层ID
        /// </summary>
        /// <returns></returns>
        private decimal GetCurrentLayerId()
        {
            ILayer layer = GetCurrentLayer();
            if (layer != null)
            {
                return layer.ID;
            }
            return -1;
        }

        /// <summary>
        /// 取得当前选择的图层
        /// </summary>
        /// <returns></returns>
        private ILayer GetCurrentLayer()
        {
            for (int i=3;i<toolStrip2.Items.Count;i++)
            {
                ToolStripItem item = toolStrip2.Items[i];
                ToolStripButton btn = item as ToolStripButton;
                if (btn.Checked)
                {
                    return (ILayer)btn.Tag;
                }
            }
            if (toolStrip2.Items.Count > 3)
            {
                ((ToolStripButton)toolStrip2.Items[3]).Checked = true;
                return (ILayer)((ToolStripButton)toolStrip2.Items[3]).Tag;
            }
            return null;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                return;
            }
            if (dataGridView1.SelectedRows[0].Index < 0)
            {
                return;
            }
            string bianhao = dataGridView1.GetGridValue(dataGridView1.SelectedRows[0].Index, "预编宗地号").ToString();

            if (dataGridView1 != null)
            {
                OpenTuDiZheng(bianhao);
            }
        }
    }
}
