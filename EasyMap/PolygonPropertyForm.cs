using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EasyMap.Data;
using System.Data.SqlClient;
using EasyMap.Geometries;
using EasyMap.Layers;
using System.IO;
using System.Drawing.Imaging;
using EasyMap.Controls;
using EasyMap.Data.Providers;

namespace EasyMap
{
    public partial class PolygonPropertyForm : MyForm
    {
        private decimal _MapId = 0;
        private decimal _LayerId = 0;
        private int _type= 7;
        private Geometry _Object;
        private DataTable _table = null;
        private DataTable _datatable = null;
        public delegate void AfterSaveEvent(Geometry obj);
        public event AfterSaveEvent AfterSave;
        private bool _Selected = false;
        private string _UpdateDate = "";
        private List<ILayer> _SelectLayers = new List<ILayer>();
        private MyGeometryList _SelectObjects = new MyGeometryList();
        private ILayer _EditLayer;
        public delegate void SelectObjectEvent(VectorLayer layer, Geometry geom);
        public event SelectObjectEvent SelectObject;

        public ILayer EditLayer
        {
            get { return _EditLayer; }
            set 
            { 
                _EditLayer = value;

                bool allowedit = GetCurrentLayer() != null && GetCurrentLayer() == _EditLayer;

                dataGridView1.ReadOnly = !allowedit;
                btnOk.Enabled = allowedit;
                txtMessage.ReadOnly = !allowedit;
                textBox1.ReadOnly = !allowedit;
                paste.Enabled = allowedit;
            }
        }

        public MyGeometryList SelectObjects
        {
            get { return _SelectObjects; }
            set { _SelectObjects = value; }
        }

        public List<ILayer> SelectLayers
        {
            get { return _SelectLayers; }
            set { _SelectLayers = value; }
        }

        public PolygonPropertyForm(decimal mapid)
        {
            InitializeComponent();
            _MapId = mapid;
        }

        /// <summary>
        /// 根据给定的地图ID图层ID和元素ID取得属性信息
        /// </summary>
        /// <param name="mapid">地图ID</param>
        /// <param name="layerid">图层ID</param>
        /// <param name="objectid">元素ID</param>
        public void Initial(ILayer editlayer)
        {
            EditLayer = editlayer;
            if (SelectObjects.Count > 1&&splitContainer2.SplitterDistance==0)
            {
                pictureBox1_Click(null, null);
            }
            AddTreeNode();
        }

        private void Search()
        {
            bool allowedit = GetCurrentLayer()!=null&&GetCurrentLayer() == _EditLayer;

            dataGridView1.ReadOnly = !allowedit;
            btnOk.Enabled = allowedit;
            txtMessage.ReadOnly = !allowedit;
            textBox1.ReadOnly = !allowedit;
            paste.Enabled = allowedit;

            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            txtMessage.Text = "";
            _LayerId = GetCurrentLayer().ID;
            _Object = GetSelectObject();
            _type = GetCurrentLayerType();
            _Selected = false;
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            textBox1.Text = _Object.Text;
            txtMessage.Text = _Object.Message;
            _table = MapDBClass.GetPropertyDefine(_type.ToString());
            string sql = "select * from " + MapDBClass.GetPropertyTableName(_MapId, _LayerId) + " where 1<>1";
            DataTable temptable = SqlHelper.Select(sql, null);
            for (int i = 4; i < temptable.Columns.Count; i++)
            {
                bool find = false;
                for (int j = 0; j < _table.Rows.Count; j++)
                {
                    if (temptable.Columns[i].ColumnName.ToLower() == _table.Rows[j]["PropertyName"].ToString().ToLower())
                    {
                        find = true;
                        break;
                    }
                }
                if (find)
                {
                    continue;
                }
                _table.Rows.Add(new object[] { i, _type.ToString(), temptable.Columns[i].ColumnName, temptable.Columns[i].DataType.Name, "0", "0", "", "1" });
            }
            SetDateList();
            textBox2.Text = "";
            textBox3.Text = "";
            Geometry selobject = _Object;
            if (selobject is Polygon)
            {
                Polygon polygon = selobject as Polygon;
                textBox2.Text = String.Format("{0:N5}", polygon.Area);
                textBox3.Text = String.Format("{0:N5}", polygon.ExteriorRing.Length);
            }
            else if (selobject is MultiPolygon)
            {
                MultiPolygon multiPolygon = selobject as MultiPolygon;
                textBox2.Text = String.Format("{0:N5}", multiPolygon.Area);
                textBox3.Text = String.Format("{0:N5}", multiPolygon.Length);
            }
            else if (selobject is LinearRing)
            {
                LinearRing line = selobject as LinearRing;
                textBox3.Text = String.Format("{0:N5}", line.Length);
            }
            else if (selobject is LineString)
            {
                LineString line = selobject as LineString;
                textBox3.Text = String.Format("{0:N5}", line.Length);
            }
            else if (selobject is MultiLineString)
            {
                MultiLineString line = selobject as MultiLineString;
                textBox3.Text = String.Format("{0:N5}", line.Length);
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_Object.Text != textBox1.Text.Trim()||_Object.Message!=txtMessage.Text.Trim())
            {
                _Object.Text = textBox1.Text.Trim();
                _Object.Message = txtMessage.Text.Trim();
                if (AfterSave != null)
                {
                    AfterSave(_Object);
                }
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("请设置日期。");
                comboBox1.Focus();
                return;
            }
            //保存属性列表
            List<PropertyData> properties = new List<PropertyData>();
            DataGridView view=dataGridView1;
            bool change = false;
            if (_datatable.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string colname = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (_datatable.Rows[0][colname].ToString() != GetValue(view, i, 1))
                    {
                        change = true;
                        break;
                    }
                }
            }
            else
            {
                change = true;
            }
            if (!change)
            {
                return;
            }
            if (change)
            {
                string type = "0";
                //如果是保存交易信息
                //循环设置保存属性列表
                for (int i = 0; i < view.Rows.Count; i++)
                {
                    if (view.Rows[i].Cells[0].Value != null)
                    {
                        PropertyData data = new PropertyData();
                        if (view.Rows[i].Cells[1].Value != null)
                        {
                            data.Data = view.Rows[i].Cells[1].Value.ToString();
                        }
                        data.PropertyName = view.Rows[i].Cells[0].Value.ToString();
                        properties.Add(data);
                    }
                }
                //保存属性
                MapDBClass.SaveProperty(_MapId, _LayerId, _Object.ID, comboBox1.Text.Trim(), properties, _UpdateDate);
            }
            bool find = false;
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString() == comboBox1.Text.Trim())
                {
                    find = true;
                    break;
                }
            }
            if (!find)
            {
                comboBox1.Items.Add(comboBox1.Text.Trim());
            }
            comboBox1_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// 初始化日期下拉列表
        /// </summary>
        private void SetDateList()
        {
            comboBox1.Items.Clear();
            DataTable table = MapDBClass.GetPropertyDateList(_MapId, _LayerId, _Object.ID);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i][0].ToString());
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
            else
            {
                comboBox1_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// 日期变更时，重新查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _datatable = MapDBClass.GetProperty(_MapId, _LayerId, _Object.ID, comboBox1.Text.Trim());
            if (_datatable == null)
            {
                return;
            }
            if (_datatable.Rows.Count > 0)
            {
                _UpdateDate = _datatable.Rows[0]["UpdateDate"].ToString();
            }
            else
            {
                _UpdateDate = "";
            }
            if (_datatable.Rows.Count == 0 && checkBox1.Checked)
            {
                if (_Selected)
                {
                    return;
                }
                if (!_Selected&&comboBox1.Items.Count>0)
                {
                    _datatable = MapDBClass.GetProperty(_MapId, _LayerId, _Object.ID, comboBox1.Items[comboBox1.Items.Count - 1].ToString());
                }

            }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[0].ReadOnly = true;
            for (int i = 4; i < _datatable.Columns.Count; i++)
            {
                string colname = _datatable.Columns[i].ColumnName;
                if (colname.ToLower() == "createdate" || colname.ToLower() == "updatedate")
                {
                    continue;
                }
                int row = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = colname;
                if (_datatable.Rows.Count > 0)
                {
                    _Selected = true;
                    dataGridView1.Rows[row].Cells[1].Value = _datatable.Rows[0][i];
                }
                int findrow = FindDefineRow(colname);
                if (findrow >= 0)
                {
                    dataGridView1.Rows[row].Cells[2].Value = _table.Rows[findrow]["DataType"];
                    dataGridView1.Rows[row].Cells[3].Value = _table.Rows[findrow]["AllowUnSelect"];
                    dataGridView1.Rows[row].Cells[4].Value = _table.Rows[findrow]["AllowInput"];
                    dataGridView1.Rows[row].Cells[5].Value = _table.Rows[findrow]["List"];
                    dataGridView1.Rows[row].Visible = _table.Rows[findrow]["AllowVisible"] != null && _table.Rows[findrow]["AllowVisible"].ToString() == "1";
                }
            }
        }

        /// <summary>
        /// 打开日历选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
            monthCalendar1.Focus();
        }

        /// <summary>
        /// 日历失去焦点时隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_Leave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        /// <summary>
        /// 日历选择后设置日期并隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.Visible = false;
            DateTime date = monthCalendar1.SelectionStart;
            comboBox1.Text = Common.ConvertDateToString(date);
            comboBox1_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PolygonPropertyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(null, null);
            }
        }

        /// <summary>
        /// 列表失去焦点时隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_Leave(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            list.Visible = false;
        }

        /// <summary>
        /// 单元格编辑后，数据校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                return;
            }
            DataGridView view = dataGridView1;
            string datatype = GetValue(view,e.RowIndex,2);
            string data = GetValue(view, e.RowIndex, e.ColumnIndex);
            if (datatype == "整数" || datatype==typeof(int).Name)
            {
                int num = 0;
                if (!Int32.TryParse(data, out num))
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
            else if (datatype == "小数" || datatype == typeof(float).Name || datatype==typeof(double).Name)
            {
                double num = 0;
                if (!double.TryParse(data, out num))
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
            else if (datatype == "日期" || datatype==typeof(DateTime).Name)
            {
                DateTime date;
                if (!DateTime.TryParse(data, out date))
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
                else
                {
                    view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Common.ConvertDateToString(date);
                }
            }
        }

        /// <summary>
        /// 日期选择控件选择日期后，将选择的日期赋值给选择的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            MonthCalendar cal = monthCalendar2;
            DataGridView view = dataGridView1;
            cal.Visible = false;
            DateTime date=cal.SelectionStart;
            view.SelectedRows[0].Cells[1].Value = date.Year.ToString() + "/" + date.Month.ToString().PadLeft(2, '0') + "/" + date.Day.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 日期选择控件焦点离开隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar2_Leave(object sender, EventArgs e)
        {
            MonthCalendar cal = sender as MonthCalendar;
            cal.Visible = false;
        }

        /// <summary>
        /// 列表点击时，将选择项目赋值给选择的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_Click(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            DataGridView view = dataGridView1;
            view.SelectedRows[0].Cells[1].Value = list.SelectedItem.ToString();
            list.Visible = false;
        }

        /// <summary>
        /// 单元格点击时处理输入类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            DataGridView view = dataGridView1;
            if (e.ColumnIndex != 1)
            {
                return;
            }
            object obj = view.SelectedRows[0].Cells[1].Value;
            view.Columns[e.ColumnIndex].ReadOnly = false;
            view.SelectedRows[0].Cells[1].Value = obj;
            //列表输入
            if (view.Rows[e.RowIndex].Cells[2].Value.ToString() == "列表")
            {

                ListBox list = listBox1;
                //不可输入的时候，只读设定
                if (view.Rows[e.RowIndex].Cells[4].Value.ToString() != "1")
                {
                    view.Columns[e.ColumnIndex].ReadOnly = true;
                }
                //设置列表内容
                list.Items.Clear();
                list.Items.AddRange(view.Rows[e.RowIndex].Cells[5].Value.ToString().Split(','));
                //不可空的时候，列表第一项填充
                if (view.Rows[e.RowIndex].Cells[3].Value.ToString() != "1")
                {
                    if (list.Items.Count > 0 
                        && (view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value==null
                        || view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()==""))
                    {
                        view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = list.Items[0];
                    }
                }
                Rectangle rect = view.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                list.Left = rect.Left + 2;
                list.Width = rect.Width;
                if (rect.Bottom + list.Height > view.Height)
                {
                    list.Top = rect.Top - list.Height - 2;
                }
                else
                {
                    list.Top = rect.Bottom + 2;
                }
                list.Visible = true;
                list.Focus();
            }
            else if (view.Rows[e.RowIndex].Cells[2].Value.ToString() == "日期"
                || view.Rows[e.RowIndex].Cells[2].Value.ToString()==typeof(DateTime).Name)
            {
                MonthCalendar cal = monthCalendar2;
                Rectangle rect = view.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                cal.Left = splitContainer1.Panel2.Left + rect.Left + view.Left + splitContainer1.Left;
                if (splitContainer1.Top+rect.Bottom + cal.Height > view.Height)
                {
                    cal.Top = rect.Top - cal.Height + view.Top + splitContainer1.Panel2.Top+splitContainer1.Top;
                }
                else
                {
                    cal.Top = rect.Bottom + view.Top + splitContainer1.Panel2.Top+splitContainer1.Top;
                }

                cal.Visible = true;
                cal.Focus();
            }
        }

        private int FindDefineRow(string colname)
        {
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if (_table.Rows[i]["PropertyName"].ToString() == colname)
                {
                    return i;
                }
            }
            return -1;
        }

        private string GetValue(DataGridView view, int row, int col)
        {
            if (view.RowCount <= row || row < 0 || col < 0 || view.ColumnCount <= col)
            {
                return "";
            }
            if (view.Rows[row].Cells[col].Value == null)
            {
                return "";
            }
            return view.Rows[row].Cells[col].Value.ToString();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            Common.PasteToGrid(dataGridView1, false);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.ColumnIndex == -1)
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                e.Paint(e.ClipBounds, e.PaintParts);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Far;
                format.LineAlignment = StringAlignment.Center;
                Rectangle rec = e.CellBounds;
                rec.Width -= 5;
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, Brushes.Black, rec, format);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 根据选择的元素和图层添加树结构
        /// </summary>
        private void AddTreeNode()
        {
            treeView1.Nodes.Clear();
            int index = 1;
            foreach (ILayer layer in SelectLayers)
            {
                if(!(layer is VectorLayer))
                {
                    continue;
                }
                VectorLayer vlayer=layer as VectorLayer;
                TreeNode node = new TreeNode(layer.LayerName);
                node.Tag = layer;
                treeView1.Nodes.Add(node);
                GeometryProvider datasource = vlayer.DataSource as GeometryProvider;
                foreach (Geometry geom in SelectObjects)
                {
                    if (datasource.Geometries.Contains(geom))
                    {
                        string txt = geom.Text.Trim();
                        if (txt == "")
                        {
                            txt = "未定义"+index;
                            index++;
                        }
                        TreeNode subnode = new TreeNode(txt);
                        subnode.Tag = geom;
                        node.Nodes.Add(subnode);
                    }
                }
            }
            for (int i = treeView1.Nodes.Count - 1; i >= 0; i--)
            {
                if (treeView1.Nodes[i].Nodes.Count <= 0)
                {
                    treeView1.Nodes.RemoveAt(i);
                }
            }
            if (treeView1.SelectedNode == null)
            {
                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
                }
            }
            if (treeView1.Nodes.Count <= 0)
            {
                dataGridView1.Rows.Clear();
                comboBox1.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                txtMessage.Text = "";
            }

        }

        /// <summary>
        /// 取得选中的图层
        /// </summary>
        /// <returns></returns>
        private ILayer GetCurrentLayer()
        {
            if (treeView1.SelectedNode == null)
            {
                if (treeView1.Nodes.Count <= 0)
                {
                    return null;
                }
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is ILayer)
            {
                return node.Tag as ILayer;
            }
            return node.Parent.Tag as ILayer;
        }

        /// <summary>
        /// 取得选中的元素
        /// </summary>
        /// <returns></returns>
        private Geometry GetSelectObject()
        {
            if (treeView1.SelectedNode == null)
            {
                if (treeView1.Nodes.Count <= 0 || treeView1.Nodes[0].Nodes.Count<=0)
                {
                    return null;
                }

                treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
            }
            TreeNode node = treeView1.SelectedNode;
            if (node.Tag is ILayer)
            {
                treeView1.SelectedNode = node.Nodes[0];
            }
            node = treeView1.SelectedNode;
            return node.Tag as Geometry;
        }

        /// <summary>
        /// 取得图层类型
        /// </summary>
        /// <returns></returns>
        private int GetCurrentLayerType()
        {
            ILayer layer=GetCurrentLayer();
            string sql="select layertype from t_layer where mapid="+_MapId+" and layerid="+layer.ID;
            DataTable table=SqlHelper.Select(sql,null);
            if(table!=null&&table.Rows.Count>0)
            {
                string sid = table.Rows[0][0].ToString();
                return int.Parse(sid);
            }
            return 0;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectObject != null)
            {
                SelectObject(GetCurrentLayer() as VectorLayer, GetSelectObject());
            }
            Search();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int dis = int.Parse(pictureBox1.Tag.ToString());

            if (dis != 0)
            {
                pictureBox1.Tag = 0;
                splitContainer2.SplitterDistance = dis;
                pictureBox1.Image = EasyMap.Properties.Resources.up;
                pictureBox1.ToolTipText = "收起";
            }
            else
            {
                pictureBox1.Tag = splitContainer1.SplitterDistance;
                splitContainer2.SplitterDistance = 0;
                pictureBox1.Image = EasyMap.Properties.Resources.down;
                pictureBox1.ToolTipText = "展开";
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            btnPaste.Enabled = paste.Enabled;
            btnSave.Enabled = btnOk.Enabled;
        }
    }
}
