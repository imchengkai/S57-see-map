using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Report;
using EasyMap.UI.Forms;

namespace EasyMap
{
    public partial class DataSearchForm : MyForm
    {
        private decimal _MapId;
        private decimal _LayerId;
        private decimal _ObjectId;
        private string _TableName;
        private string _ReportName = "";
        private decimal _ReportId = decimal.MinusOne;
        //数值型字段类型定义序列
        private string[] _NumericalType = { "127", "106", "62", "56", "108", "52", "48" };

        public delegate void SetMapAreaColorEvent(List<PriceColorData> colors,List<AreaColor> datalist);
        public event SetMapAreaColorEvent SetMapAreaColor;

        public DataSearchForm(decimal mapid,decimal layerid,decimal objectid)
        {
            InitializeComponent();
            Initial(mapid, layerid, objectid);
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        public void Initial(decimal mapid, decimal layerid, decimal objectid)
        {
            myTree1.Nodes[0].Nodes.Clear();
            myTree1.Nodes[0].Checked = false;
            gridDataList.Columns.Clear();
            gridDataList.DataSource = new DataTable();
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = "日期";
            column.Name = "propertydate";
            column.DataPropertyName = "propertydate";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridDataList.Columns.Add(column);
            lblTip.Text = "查询到符合指定条件的记录：" + (gridDataList.RowCount-1);
            _MapId = mapid;
            _LayerId = layerid;
            _ObjectId = objectid;
            _TableName = "t_" + _MapId + "_" + _LayerId;
            string sql = SqlHelper.GetSql("SelectColumnNameWithoutKey");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("tablename", _TableName));
            DataTable table = SqlHelper.Select(sql, param);
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    TreeNode subnode = new TreeNode();
                    subnode.Text = table.Rows[i]["name"].ToString();
                    subnode.Tag = table.Rows[i]["type"].ToString();
                    myTree1.Nodes[0].Nodes.Add(subnode);
                }
            }
            sql = "select min(propertydate),max(propertydate) from "+_TableName;
            sql += " where MapId=" + _MapId + " and LayerId=" + _LayerId + " and ObjectId=" + _ObjectId;
            table = SqlHelper.Select(sql, null);
            if (table != null && table.Rows.Count >= 1)
            {
                string sdate1 = table.Rows[0][0].ToString();
                string sdate2 = table.Rows[0][1].ToString();
                DateTime date1 = DateTime.Now;
                DateTime date2 = DateTime.Now;
                if (!DateTime.TryParse(sdate1, out date1))
                {
                    date1 = DateTime.Now;
                }
                if (!DateTime.TryParse(sdate2, out date2))
                {
                    date2 = DateTime.Now;
                }
                dateTimePicker1.Value = date1;
                dateTimePicker2.Value = date2;
            }
        }

        /// <summary>
        /// 生成查询用SQL
        /// </summary>
        /// <returns></returns>
        private string MakeSql()
        {
            string sql = "";
            sql += "select\r\n";
            sql += "propertydate\r\n";
            for(int i=1;i<gridDataList.ColumnCount;i++)
            {
                DataGridViewTextBoxColumn column = gridDataList.Columns[i] as DataGridViewTextBoxColumn;
                sql += "," + column.Name + "\r\n";
            }
            sql += "from " + _TableName + "\r\n";
            sql += "where MapId="+_MapId+"\r\n";
            sql += "and LayerId="+_LayerId+"\r\n";
            sql += "and ObjectId=" + _ObjectId + "\r\n";
            sql += "and propertydate>='" + Common.ConvertDateToString(dateTimePicker1.Value) + "'\r\n";
            sql += "and propertydate<='" + Common.ConvertDateToString(dateTimePicker2.Value) + "'\r\n";
            sql += "order by propertydate\r\n";
            return sql;
        }

        /// <summary>
        /// 生成报表用SQL
        /// </summary>
        /// <returns></returns>
        private string MakeReportSql()
        {
            string sql = "";
            sql += "select\r\n";
            sql += "propertydate as 日期\r\n";
            for (int i = 1; i < gridDataList.ColumnCount; i++)
            {
                DataGridViewTextBoxColumn column = gridDataList.Columns[i] as DataGridViewTextBoxColumn;
                if (column.Tag != null && _NumericalType.Contains(column.Tag.ToString()))
                {
                    sql += "," + column.Name + "\r\n";
                }
            }
            sql += "from " + _TableName + "\r\n";
            sql += "where MapId=" + _MapId + "\r\n";
            sql += "and LayerId=" + _LayerId + "\r\n";
            sql += "and ObjectId=" + _ObjectId + "\r\n";
            sql += "and propertydate>='" + Common.ConvertDateToString(dateTimePicker1.Value) + "'\r\n";
            sql += "and propertydate<='" + Common.ConvertDateToString(dateTimePicker2.Value) + "'\r\n";
            sql += "order by propertydate\r\n";
            return sql;
        }

        /// <summary>
        /// 生成用于保存报表用SQL
        /// </summary>
        /// <returns></returns>
        private string MakeReportToSaveSql()
        {
            string sql = "";
            sql += "select\r\n";
            sql += "propertydate as 日期\r\n";
            for (int i = 1; i < gridDataList.ColumnCount; i++)
            {
                DataGridViewTextBoxColumn column = gridDataList.Columns[i] as DataGridViewTextBoxColumn;
                if (column.Tag != null && _NumericalType.Contains(column.Tag.ToString()))
                {
                    sql += "," + column.Name + "\r\n";
                }
            }
            sql += "from " + _TableName + "\r\n";
            sql += "where MapId=@MapId\r\n";
            sql += "and LayerId=@LayerId\r\n";
            sql += "and ObjectId=@ObjectId\r\n";
            sql += "and propertydate>=@StartDate\r\n";
            sql += "and propertydate<=@EndDate\r\n";
            sql += "order by propertydate\r\n";
            return sql;
        }

        /// <summary>
        /// 属性树选择时添加或者删除动态表格列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myTree1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == myTree1.Nodes[0])
            {
                gridDataList.Columns.Clear();
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = "日期";
                column.Name = "propertydate";
                column.DataPropertyName = "propertydate";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                gridDataList.Columns.Add(column);
                foreach (TreeNode subnode in myTree1.Nodes[0].Nodes)
                {
                    subnode.Checked = e.Node.Checked;
                }
            }
            else
            {
                if (e.Node.Checked)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.HeaderText = e.Node.Text;
                    column.Name="["+e.Node.Text+"]";
                    column.DataPropertyName = e.Node.Text;
                    column.Tag = e.Node.Tag;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    gridDataList.Columns.Add(column);
                }
                else
                {
                    for (int i = gridDataList.ColumnCount - 1; i >= 0;i-- )
                    {
                        if (gridDataList.Columns[i].Name == e.Node.Text
                            || gridDataList.Columns[i].Name == "["+e.Node.Text+"]")
                        {
                            gridDataList.Columns.RemoveAt(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 查询处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (gridDataList.Columns.Count <= 0)
            {
                MessageBox.Show("请先选择需要查询的属性。");
                return;
            }
            string sql = MakeSql();
            DataTable table = SqlHelper.Select(sql, null);
            if (table != null)
            {
                object[] objs = new object[table.Columns.Count];
                objs[0] = "合计：";
                for (int i = 1; i < gridDataList.ColumnCount; i++)
                {
                    if (gridDataList.Columns[i].Tag != null && _NumericalType.Contains(gridDataList.Columns[i].Tag.ToString()))
                    {
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
                }
                table.Rows.Add(objs);
            }

            gridDataList.DataSource = table;
            gridDataList_CurrentCellChanged(null, null);
            lblTip.Text = "查询到符合指定条件的记录：" + (gridDataList.RowCount-1);
            if (gridDataList.RowCount <= 0)
            {
                MessageBox.Show("没有查询到符合指定条件的任何数据。");
            }
        }

        /// <summary>
        /// 重新初始化界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            Initial(_MapId, _LayerId, _ObjectId);
        }

        /// <summary>
        /// 导出表格数据
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
            report.SQL = MakeReportSql();
            report.XAxisColumnName = "日期";
            report.Title = "走势图";
            report.GraphType = ReportForm.ReportGraphType.Line;
            report.Show();
        }


        /// <summary>
        /// 将当前查询保存为自定义报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                while ((_ReportName != "" && find) || _ReportName == "")
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
                SaveReportParameter(conn, tran, id);
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
            param.Add(new SqlParameter("Sql", MakeReportToSaveSql()));
            param.Add(new SqlParameter("XColumn", "日期"));
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
            param.Add(new SqlParameter("Parameter", "StartDate"));
            param.Add(new SqlParameter("ParameterName", "开始日期"));
            param.Add(new SqlParameter("ParameterType", "3"));
            SqlHelper.Insert(conn, tran, sql, param);

            param.Clear();
            param.Add(new SqlParameter("Id", id));
            param.Add(new SqlParameter("ParameterId", 2));
            param.Add(new SqlParameter("Parameter", "EndDate"));
            param.Add(new SqlParameter("ParameterName", "结束日期"));
            param.Add(new SqlParameter("ParameterType", "3"));
            SqlHelper.Insert(conn, tran, sql, param);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
