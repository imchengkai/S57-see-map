using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Layers;
using EasyMap.Controls;
using System.Data.SqlClient;
using EasyMap.Data.Providers;
using EasyMap.Geometries;
using Report;
using EasyMap.UI.Forms;

namespace EasyMap
{
    public partial class MultAreaDataSearchForm : MyForm
    {
        private decimal _MapId;
        private List<ILayer> _LayersId;
        private MyGeometryList _ObjectsId;
        private string _ReportName = "";
        private decimal _ReportId = decimal.MinusOne;
        //数值型字段类型定义序列
        private string[] _NumericalType = { "127", "106", "62", "56", "108", "52", "48" };
        public delegate void SetMapAreaColorEvent(List<PriceColorData> colors, List<AreaColor> datalist);
        public event SetMapAreaColorEvent SetMapAreaColor;
        private bool triggerevent = true;
        public MultAreaDataSearchForm(decimal mapId, List<ILayer> layersId, MyGeometryList objectsId)
        {
            InitializeComponent();
            Initial(mapId, layersId, objectsId);
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        public void Initial(decimal mapid, List<ILayer> layerid, MyGeometryList objectid)
        {
            _LayersId = layerid;
            _ObjectsId = objectid;
            _MapId = mapid;
            layerTree.Nodes.Clear();
            geomTree.Nodes.Clear();
            gridDataList.Columns.Clear();
            for (char i = 'A'; i <= 'Z'; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = i.ToString();
                gridDataList.Columns.Add(col);
            }
            gridDataList.DataSource=new DataTable();
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = "区域";
            column.Name = "objectid";
            column.DataPropertyName = "objectid";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridDataList.Columns.Add(column);
            if (gridDataList.RowCount <= 0)
            {
                lblTip.Text = "查询到符合指定条件的记录：0";
            }
            else
            {
                lblTip.Text = "查询到符合指定条件的记录：" + (gridDataList.RowCount - 1);
            }
            foreach (ILayer layer in layerid)
            {
                if (layer is VectorLayer)
                {
                    TreeNode node = new TreeNode(layer.LayerName);
                    node.Tag = layer;
                    layerTree.Nodes.Add(node);
                }
            }
        }

        private void gridDataList_CurrentCellChanged(object sender, EventArgs e)
        {
            btnFirst.Enabled = gridDataList.CurrentCell != null && gridDataList.CurrentCell.RowIndex > 0;
            btnPrevious.Enabled = btnFirst.Enabled;
            btnLast.Enabled = gridDataList.CurrentCell != null && gridDataList.CurrentCell.RowIndex < gridDataList.RowCount - 1;
            btnNext.Enabled = btnLast.Enabled;
            if (gridDataList.CurrentCell == null)
            {
                txtNo.Text = "0";
            }
            else
            {
                txtNo.Text = (gridDataList.CurrentCell.RowIndex + 1).ToString();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            int col = gridDataList.CurrentCell.ColumnIndex;
            gridDataList.ClearSelection();
            gridDataList.Rows[0].Cells[col].Selected = true;
            gridDataList.CurrentCell = gridDataList.Rows[0].Cells[col];
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int row = gridDataList.CurrentCell.RowIndex;
            int col = gridDataList.CurrentCell.ColumnIndex;
            gridDataList.ClearSelection();
            gridDataList.Rows[row - 1].Cells[col].Selected = true;
            gridDataList.CurrentCell = gridDataList.Rows[row - 1].Cells[col];
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int row = gridDataList.CurrentCell.RowIndex;
            int col = gridDataList.CurrentCell.ColumnIndex;
            gridDataList.ClearSelection();
            gridDataList.Rows[row + 1].Cells[col].Selected = true;
            gridDataList.CurrentCell = gridDataList.Rows[row + 1].Cells[col];
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            int row = gridDataList.RowCount - 1;
            int col = gridDataList.CurrentCell.ColumnIndex;
            gridDataList.ClearSelection();
            gridDataList.Rows[row].Cells[col].Selected = true;
            gridDataList.CurrentCell = gridDataList.Rows[row].Cells[col];
        }

        private void txtNo_Leave(object sender, EventArgs e)
        {
            int row = gridDataList.CurrentCell.RowIndex;
            int col = gridDataList.CurrentCell.ColumnIndex;
            if (!int.TryParse(txtNo.Text.Trim(), out row))
            {
                txtNo.Text = row.ToString();
                return;
            }
            row--;
            gridDataList.ClearSelection();
            gridDataList.Rows[row].Cells[col].Selected = true;
            gridDataList.CurrentCell = gridDataList.Rows[row].Cells[col];
        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNo_Leave(null, null);
            }
        }

        /// <summary>
        /// 图层树选择后，向属性树中添加或者删除属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layerTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                TreeNode node = new TreeNode(e.Node.Text);
                node.Tag = e.Node.Tag;
                geomTree.Nodes.Add(node);
                DataTable table = GetProperties(e.Node.Tag as ILayer);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string name = GetPropertyName(table, i);
                    string type = GetPropertyType(table, i);
                    if (_NumericalType.Contains(type))
                    {
                        TreeNode subnode = new TreeNode(name);
                        subnode.Tag = type;
                        node.Nodes.Add(subnode);
                    }
                }
            }
            else
            {
                foreach (TreeNode node in geomTree.Nodes)
                {
                    if (node.Tag == e.Node.Tag)
                    {
                        node.Checked = false;
                        geomTree.Nodes.Remove(node);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 取得图层表名
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private string GetTableName(ILayer layer)
        {
            string tablename = "t_" + _MapId + "_" + layer.ID;
            return tablename;
        }

        /// <summary>
        /// 取得图层字段列表
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private DataTable GetProperties(ILayer layer)
        {
            string tablename = GetTableName(layer);
            string sql = SqlHelper.GetSql("SelectColumnNameWithoutKey");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("tablename", tablename));
            DataTable table = SqlHelper.Select(sql, param);
            return table;
        }

        /// <summary>
        /// 取得字段名
        /// </summary>
        /// <param name="table"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetPropertyName(DataTable table, int index)
        {
            if (table != null&&table.Rows.Count>index)
            {
                return table.Rows[index]["name"].ToString();
            }
            return "";
        }

        /// <summary>
        /// 取得字段类型
        /// </summary>
        /// <param name="table"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string GetPropertyType(DataTable table, int index)
        {
            if (table != null && table.Rows.Count > index)
            {
                return table.Rows[index]["type"].ToString();
            }
            return "";
        }

        /// <summary>
        /// 生成查询SQL
        /// </summary>
        /// <returns></returns>
        private string MakeSql(MyDataGridView view)
        {
            string sql = "";
            view.DataSource = null;
            view.Columns.Clear();
            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.DataPropertyName = "layerid";
            c.Visible = false;
            view.Columns.Add(c);
            c = new DataGridViewTextBoxColumn();
            c.DataPropertyName = "objectid";
            c.Visible = false;
            view.Columns.Add(c);
            c = new DataGridViewTextBoxColumn();
            c.DataPropertyName = "区域名称";
            c.HeaderText = "区域名称";
            view.Columns.Add(c);
            foreach (TreeNode node in geomTree.Nodes)
            {
                ILayer layer=node.Tag as ILayer;
                int row = view.Rows.Add();
                foreach (TreeNode subnode in node.Nodes)
                {
                    if (subnode.Checked)
                    {
                        int col=-1;
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            for (int j = 0; j < view.ColumnCount; j++)
                            {
                                if (view.GetGridValue(i, j).ToString() == subnode.Text)
                                {
                                    col = j;
                                    break;
                                }
                            }
                            if (col >= 0)
                            {
                                break;
                            }
                        }
                        if (col < 0)
                        {
                            DataGridViewTextBoxColumn column=new DataGridViewTextBoxColumn();
                            column.DataPropertyName = subnode.Text;
                            column.Tag="["+subnode.Text+"]";
                            column.HeaderText = subnode.Text;
                            col = view.Columns.Add(column);
                        }
                        view.Rows[row].Cells[col].Value = subnode.Text;
                        view.Rows[row].Cells[col].Tag = "["+GetTableName(layer) + "].[" + subnode.Text+"]";

                    }
                }
            }
            int index = 0;
            foreach (TreeNode node in geomTree.Nodes)
            {
                bool find = false;
                foreach (TreeNode subnode in node.Nodes)
                {
                    if (subnode.Checked)
                    {
                        find = true;
                        break;
                    }
                }
                if (find)
                {
                    string subsql = "";
                    VectorLayer layer=node.Tag as VectorLayer;
                    for (int i = 3; i < view.ColumnCount; i++)
                    {
                        if (subsql != "")
                        {
                            subsql += ",";
                        }
                        if (view.Rows[index].Cells[i].Tag != null)
                        {
                            subsql += view.Rows[index].Cells[i].Tag.ToString() + " as " + view.Columns[i].Tag.ToString();
                        }
                        else
                        {
                            subsql += "NULL as " + view.Columns[i].Tag.ToString();
                        }
                    }
                    if (subsql != "")
                    {
                        string tablename = GetTableName(layer);
                        subsql = "select " + tablename + ".layerid," + tablename + ".objectid,isnull(t_object.name,'') as 区域名称," + subsql + " from " + tablename;
                        //subsql += " and propertydate='" + dateTimePicker1.Value.ToShortDateString() + "'";
                        GeometryProvider datasource = layer.DataSource as GeometryProvider;
                        string values = "";
                        foreach (Geometry geom in _ObjectsId)
                        {
                            if (datasource.Geometries.Contains(geom))
                            {
                                if (values != "")
                                {
                                    values += ",";
                                }
                                values += geom.ID;
                            }
                        }
                        if (radioButton2.Checked)
                        {
                            subsql += " left join t_object on ";
                            subsql += tablename + ".mapid=t_object.mapid ";
                            subsql += " and " + tablename + ".layerid=t_object.layerid ";
                            subsql += " and " + tablename + ".objectid=t_object.objectid ";
                            subsql+=" where " + tablename + ".mapid=" + _MapId;
                            subsql += " and " + tablename + ".layerid=" + layer.ID;
                            string sdate =Common.ConvertDateToString(dateTimePicker1.Value);
                            subsql += " and " + tablename + ".propertydate='" + sdate + "'";
                            if (values != "")
                            {
                                subsql += " and " + tablename + ".objectid in (" + values + ")";
                            }
                            else
                            {
                                subsql = "";
                            }
                        }
                        else
                        {
                            subsql += " left join t_object on ";
                            subsql += tablename + ".mapid=t_object.mapid ";
                            subsql += " and " + tablename + ".layerid=t_object.layerid ";
                            subsql += " and " + tablename + ".objectid=t_object.objectid ";
                            subsql += " inner join (select mapid,layerid,objectid,max(propertydate) as propertydate from " + tablename;
                            subsql += " where " + tablename + ".mapid=" + _MapId;
                            subsql += " and " + tablename + ".layerid=" + layer.ID;
                            if (values != "")
                            {
                                subsql += " and " + tablename + ".objectid in (" + values + ")";
                                subsql += " group by mapid,layerid,objectid) t";
                                subsql += " on " + tablename + ".mapid=t.mapid";
                                subsql += " and " + tablename + ".layerid=t.layerid";
                                subsql += " and " + tablename + ".objectid=t.objectid";
                                subsql += " and " + tablename + ".propertydate=t.propertydate";
                            }
                            else
                            {
                                subsql = "";
                            }

                        }
                    }
                    if (subsql != "")
                    {
                        if (sql != "")
                        {
                            sql += "union all\r\n";
                        }
                        sql += subsql + "\r\n";
                    }
                }
                index++;
            }
            return sql;
        }

        private void geomTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!triggerevent)
            {
                return;
            }
            triggerevent = false;
            if (e.Node.Tag is ILayer)
            {
                foreach (TreeNode subnode in e.Node.Nodes)
                {
                    subnode.Checked = e.Node.Checked;
                }
            }
            else
            {
                bool chk = true;
                foreach (TreeNode subnode in e.Node.Parent.Nodes)
                {
                    if (!subnode.Checked)
                    {
                        chk = false;
                    }
                }
                if (e.Node.Parent.Checked != chk)
                {
                    e.Node.Parent.Checked = chk;
                }
            }
            triggerevent = true;
        }

        /// <summary>
        /// 查询处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = MakeSql(gridDataList);
            if (sql == "")
            {
                MessageBox.Show("请设置查询区域和属性。");
                return;
            }
            DataTable table = SqlHelper.Select(sql, null);
            //计算合计
            object[] objs = new object[table.Columns.Count];
            objs[2] = "合计：";
            btnPrice.DropDownItems.Clear();
            for (int i = 3; i < gridDataList.ColumnCount; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text=gridDataList.Columns[i].HeaderText;
                item.CheckOnClick = true;
                item.Click += btnPrice_ButtonClick;
                btnPrice.DropDownItems.Add(item);
                decimal count = 0;
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    decimal num = 0;
                    if (decimal.TryParse(table.Rows[j][i].ToString(), out num))
                    {
                        count += num;
                    }
                }
                objs[i] = count;
            }
            table.Rows.Add(objs);
            gridDataList.DataSource = table;
            //设定提示信息
            if (gridDataList.RowCount <= 0)
            {
                lblTip.Text = "查询到符合指定条件的记录：0";
            }
            else
            {
                lblTip.Text = "查询到符合指定条件的记录：" + (gridDataList.RowCount - 1);
            }
            gridDataList_CurrentCellChanged(null, null);
        }

        /// <summary>
        /// 重新设置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            Initial(_MapId, _LayersId, _ObjectsId);
        }

        /// <summary>
        /// 导出数据到CSV文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Common.ExportDataGridViewToCsv(gridDataList, saveFileDialog1.FileName);
        }


        /// <summary>
        /// 图形报表处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            ReportForm report = new ReportForm();
            report.SQL = MakeSql(new MyDataGridView());
            report.XAxisColumnName = "区域名称";
            report.Title = "走势图";
            report.GraphType = ReportForm.ReportGraphType.Line;
            report.Show();
        }

        /// <summary>
        /// 地块颜色区分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrice_ButtonClick(object sender, EventArgs e)
        {
            //取得选择字段
            string field="";
            //设置列表中按钮状态
            if (sender is ToolStripMenuItem)
            {
                foreach (ToolStripMenuItem item in btnPrice.DropDownItems)
                {
                    if (item != sender)
                    {
                        item.Checked = false;
                    }
                    else
                    {
                        item.Checked = true;
                        field = item.Text;
                    }
                }
            }
            else
            {
                foreach (ToolStripMenuItem item in btnPrice.DropDownItems)
                {
                    if (item.Checked)
                    {
                        field = item.Text;
                        break;
                    }
                }
                if (field == "")
                {
                    btnPrice.ShowDropDown();
                    return;
                }
            }
            if (SetMapAreaColor == null)
            {
                return;
            }
            //取得字段在表格中列索引
            int col = -1;
            for (int i = 0; i < gridDataList.ColumnCount; i++)
            {
                if (gridDataList.Columns[i].HeaderText == field)
                {
                    col = i;
                    break;
                }
            }
            if (col < 0)
            {
                return;
            }
            //取得该列最大最小值
            decimal min = decimal.MaxValue;
            decimal max = decimal.MinValue;
            for (int i = 0; i < gridDataList.RowCount-1; i++)
            {
                decimal val = 0;
                if (decimal.TryParse(gridDataList.GetGridValue(i, col).ToString(), out val))
                {
                    min = Math.Min(min, val);
                    max = Math.Max(max, val);
                }
            }
            if (gridDataList.RowCount <= 1)
            {
                min = 0;
                max = 0;
            }
            //打开地价区间设置窗口
            //PriceColorSettingForm form = new PriceColorSettingForm(_MapId);
            //form.MaxPrice = max;
            //form.MinPrice = min;
            //DialogResult ret = form.ShowDialog(this);
            ////窗口确认后，宗地填充色重绘
            //if (ret == DialogResult.OK)
            //{
            //    List<AreaColor> datasource = new List<AreaColor>();
            //    for (int i = 0; i < gridDataList.RowCount; i++)
            //    {
            //        decimal val = 0;
            //        decimal layerid = 0;
            //        decimal objectid = 0;
            //        if (decimal.TryParse(gridDataList.GetGridValue(i, col).ToString(), out val)
            //            && decimal.TryParse(gridDataList.GetGridValue(i, 0).ToString(), out layerid)
            //            && decimal.TryParse(gridDataList.GetGridValue(i, 1).ToString(), out objectid))
            //        {
            //            AreaColor areaColor = new AreaColor();
            //            areaColor.MapId = _MapId;
            //            areaColor.LayerId = layerid;
            //            areaColor.ObjectId = objectid;
            //            areaColor.Number = val;
            //            datasource.Add(areaColor);
            //        }
            //    }
            //    SetMapAreaColor(form.PriceColorList, datasource);
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (_ReportName == "")
            {
                InputStringForm form = new InputStringForm("请输入报表名称", "");
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                _ReportName = form.InputContext.Trim();
                bool find = true;
                while ((_ReportName != "" && find)||_ReportName=="")
                {
                    if (_ReportName == "")
                    {
                        MessageBox.Show("报表名称必须设定，请重新输入。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        form = new InputStringForm("请输入报表名称", "");
                        if (form.ShowDialog() != DialogResult.OK)
                        {
                            _ReportName = "";
                            return;
                        }
                        _ReportName = form.InputContext.Trim();
                    }
                    find = false;
                    string sql = SqlHelper.GetSql("SelectReportName");
                    DataTable table = SqlHelper.Select(sql, null);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["ReportName"].ToString() == _ReportName)
                        {
                            find = true;
                            MessageBox.Show("报表名称重复，请重新输入。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            form = new InputStringForm("请输入报表名称", "");
                            if (form.ShowDialog() != DialogResult.OK)
                            {
                                _ReportName = "";
                                return;
                            }
                            _ReportName = form.InputContext.Trim();
                            break;
                        }
                    }
                }
            }
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                decimal id = SaveReport(conn, tran);
                if (radioButton2.Checked)
                {
                    SaveReportParameter(conn, tran, id);
                }
                _ReportId = id;
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                ShowErrorForm form = new ShowErrorForm(ex);
                form.ShowDialog(this);
            }
        }


        /// <summary>
        /// 取得报表最大ID
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        private decimal GetMaxReportId(SqlConnection conn, SqlTransaction tran)
        {
            string sql = SqlHelper.GetSql("SelectMaxReportId");
            DataTable table = SqlHelper.Select(conn, tran, sql, null);
            return (decimal)table.Rows[0][0];
        }

        /// <summary>
        /// 保存报表主表
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        private decimal SaveReport(SqlConnection conn, SqlTransaction tran)
        {
            decimal id = _ReportId;
            if (id == decimal.MinusOne)
            {
                id = GetMaxReportId(conn, tran);
            }
            else
            {
                string deletesql = SqlHelper.GetSql("DeleteReport");
                List<SqlParameter> param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("Id", id));
                SqlHelper.Delete(conn, tran, deletesql, param1);
            }
            string sql = SqlHelper.GetSql("InsertReport");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("Id", id));
            param.Clear();
            param.Add(new SqlParameter("Id", id));
            param.Add(new SqlParameter("ReportName", _ReportName));
            string reportsql = MakeSql(new MyDataGridView());
            if (radioButton2.Checked)
            {
                string sdate = Common.ConvertDateToString(dateTimePicker1.Value);
                reportsql = reportsql.Replace("'" + sdate + "'", "@propertydate");
            }
            param.Add(new SqlParameter("Sql", reportsql));
            param.Add(new SqlParameter("XColumn", "区域名称"));
            SqlHelper.Insert(conn, tran, sql, param);
            return id;
        }

        /// <summary>
        /// 保存报表参数
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        private void SaveReportParameter(SqlConnection conn, SqlTransaction tran, decimal id)
        {
            string deletesql = SqlHelper.GetSql("DeleteReportParameter");
            List<SqlParameter> param1 = new List<SqlParameter>();
            param1.Add(new SqlParameter("Id", id));
            SqlHelper.Delete(conn, tran, deletesql, param1);
            string sql = SqlHelper.GetSql("InsertReportParameter");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Clear();
            param.Add(new SqlParameter("Id", id));
            param.Add(new SqlParameter("ParameterId", 1));
            param.Add(new SqlParameter("Parameter", "propertydate"));
            param.Add(new SqlParameter("ParameterName", "查询日期"));
            param.Add(new SqlParameter("ParameterType", "3"));
            SqlHelper.Insert(conn, tran, sql, param);
        }

        private void MultAreaDataSearchForm_ResizeEnd(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }
    }
}
