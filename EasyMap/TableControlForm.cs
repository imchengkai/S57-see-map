using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EasyMap
{
    public partial class TableControlForm : MyForm
    {
        private DataTable table;
        private string _TableName;

        public TableControlForm()
        {
            InitializeComponent();
            Initial();
        }

        private void Initial()
        {
            table = SqlHelper.Select(SqlHelper.GetSql("SelectTableControl"), null);
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    comTables.Items.Add(table.Rows[i]["comment"].ToString());
                }
            }
        }

        private void comTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TableName = table.Rows[comTables.SelectedIndex]["tablename"].ToString();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("tablename", _TableName));
            string sql = SqlHelper.GetSql("SelectColumnControl");
            DataTable coltable = SqlHelper.Select(sql, param);
            if (coltable == null || coltable.Rows.Count <= 0)
            {
                SqlConnection conn = null;
                try
                {
                    conn = SqlHelper.GetConnection();
                    conn.Open();
                    param.Clear();
                    param.Add(new SqlParameter("tablename", _TableName));
                    sql = SqlHelper.GetSql("InsertColumnDefault");
                    SqlHelper.Insert(conn, sql, param);
                    conn.Close();
                }
                catch(Exception ex)
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                    Common.ShowError(ex);
                }
            }
            param.Clear();
            param.Add(new SqlParameter("tablename", _TableName));
            sql = SqlHelper.GetSql("SelectColumnControl");
            coltable = SqlHelper.Select(sql, param);
            if (coltable != null)
            {
                for (int i = 0; i < coltable.Rows.Count; i++)
                {
                    int row = columnsList.Rows.Add();
                    columnsList.Rows[row].Cells["tablename"].Value = coltable.Rows[i]["tablename"];
                    columnsList.Rows[row].Cells["columnname"].Value = coltable.Rows[i]["columnname"];
                    columnsList.Rows[row].Cells["visible"].Value = coltable.Rows[i]["visible"].ToString()=="1";
                    columnsList.Rows[row].Cells["comment"].Value = coltable.Rows[i]["comment"];
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出属性控制设置吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                string sql = SqlHelper.GetSql("UpdateColumnControl");
                List<SqlParameter> param = new List<SqlParameter>();
                for (int i = 0; i < columnsList.RowCount; i++)
                {
                    param.Clear();
                    param.Add(new SqlParameter("tablename", columnsList.Rows[i].Cells["tablename"].Value));
                    param.Add(new SqlParameter("columnname", columnsList.Rows[i].Cells["columnname"].Value));
                    param.Add(new SqlParameter("visible", ((bool)columnsList.Rows[i].Cells["visible"].Value)?"1":"0"));
                    param.Add(new SqlParameter("comment", columnsList.Rows[i].Cells["comment"].Value.ToString()));
                    SqlHelper.Update(conn, tran, sql, param);
                }
                tran.Commit();
                conn.Close();
                Close();
            }
            catch(Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                Common.ShowError(ex);
            }
        }
    }
}
