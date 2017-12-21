using ControlLib;
using EasyMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;

namespace Tax
{
    public partial class addUser : MyForm
    {
        public string layerId = "";//街道图层id
        public addUser(string layerId)
        {
            this.layerId = layerId;
            InitializeComponent();
            Init();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@name", "%"));
            Query(param);
        }
        //页面初始化
        private void Init()
        {
            //权限下拉框
            SqlConnection conn = null;
            SqlTransaction tran = null;
            user user = new user();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@name", user._userName));
                string sql = SqlHelper.GetSql("SelectQuanxianByUserName");
                sql.Replace("@name", user._userName);
                DataTable table = SqlHelper.Select(conn, tran, sql, param);
                string quanxian = table.Rows[0]["权限"].ToString();
                param.Clear();
                param.Add(new SqlParameter("layerId", layerId));
                DataTable table1 = SqlHelper.Select(conn, tran, SqlHelper.GetSql("SelectObjectByLayerId"), param);

                if (quanxian == "超级管理员")
                {
                    //user_quanxian.Items.Add("土地用户管理员");
                    user_quanxian.Items.Add("管理员");
                    //user_quanxian.Items.Add("税务用户");
                    //user_quanxian.Items.Add("土地用户");
                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(table1.Rows[i]["Name"].ToString()))
                        {
                            user_quanxian.Items.Add(table1.Rows[i]["Name"].ToString());
                        }
                    }
                }
                else if (quanxian == "管理员")
                {
                    //user_quanxian.Items.Add("税务用户");
                    for (int i = 0; i < table1.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(table1.Rows[i]["Name"].ToString()))
                        {
                            user_quanxian.Items.Add(table1.Rows[i]["Name"].ToString());
                        }
                    }
                }
                //else if (quanxian == "土地用户管理员")
                //{
                //    user_quanxian.Items.Add("土地用户");
                //    for (int i = 0; i < table1.Rows.Count; i++)
                //    {
                //        if (!string.IsNullOrEmpty(table1.Rows[i]["Name"].ToString()))
                //        {
                //            user_quanxian.Items.Add(table1.Rows[i]["Name"].ToString());
                //        }
                //    }
                //}
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
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //修改个人权限
        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow data = grid1.CurrentRow;
            if (data == null)
            {
                MessageBox.Show("请选择一行！");
            }
            user_quanxian quanxian = new user_quanxian(data.Cells["userName"].Value.ToString());
            quanxian.StartPosition = FormStartPosition.CenterScreen;
            quanxian.Show(this);
        }
        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("是否删除此条数据？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (ret != DialogResult.Yes)
            {
                return;
            }
            DataGridViewRow data = grid1.CurrentRow;
            if (data == null)
            {
                MessageBox.Show("请选择一行！");
            }
            string userName = data.Cells["userName"].Value.ToString();
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                //删除用户
                List<SqlParameter> param0 = new List<SqlParameter>();
                param0.Add(new SqlParameter("@name", userName));
                string sql = SqlHelper.GetSql("DeleteUser");
                sql.Replace("@name", userName);
                SqlHelper.Delete(conn, tran, sql, param0);
                //删除对应的权限
                List<SqlParameter> paramDel = new List<SqlParameter>();
                paramDel.Add(new SqlParameter("@name", userName));
                string sqlDel = SqlHelper.GetSql("DeleteUserQuanxian");
                sql.Replace("@name", userName);
                SqlHelper.Delete(conn, tran, sqlDel, paramDel);
                tran.Commit();
                MessageBox.Show("删除成功");
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
                MessageBox.Show(ex.Message);
            }
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@name", "%"));
            Query(param);
        }
        //保存事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = user_name.Text;
            if (name == string.Empty)
            {
                MessageBox.Show("请输入用户名！");
                return;
            }
            save();
        }
        //查询事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            if (user_name.Text == string.Empty)
            {
                param.Add(new SqlParameter("@name", "%"));
            }
            else
            {
                param.Add(new SqlParameter("@name", "%" + user_name.Text + "%"));
            }
            Query(param);
        }
        //根据查询条件进行查询
        private void Query(List<SqlParameter> param)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                string sql = SqlHelper.GetSql("SelectUser");
                sql.Replace("@name", param[0].Value.ToString());
                //获取当前用户权限
                List<SqlParameter> param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@name", user._userName));
                string sql1 = SqlHelper.GetSql("SelectQuanxianByUserName");
                sql1.Replace("@name", user._userName);
                DataTable table1 = SqlHelper.Select(conn, tran, sql1, param1);
                string quanxian = table1.Rows[0]["权限"].ToString();
                if (quanxian == "管理员")
                {
                    sql.Replace("@quanxian", "税务用户");
                    param.Add(new SqlParameter("@quanxian", "%街道"));
                }
                //else if (quanxian == "土地用户管理员")
                //{
                //    sql.Replace("@quanxian", "土地用户");
                //    param.Add(new SqlParameter("@quanxian", "土地用户"));
                //}
                else
                {
                    sql.Replace("@quanxian", "%");
                    param.Add(new SqlParameter("@quanxian", "%"));
                }
                DataTable table = SqlHelper.Select(conn, tran, sql, param);
                grid1.Rows.Clear();
                int startrow = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int row = grid1.Rows.Add();
                    grid1.Rows[row].Cells[0].Value = table.Rows[i]["用户名"];
                    grid1.Rows[row].Cells[1].Value = table.Rows[i]["权限"];
                }
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
                MessageBox.Show(ex.Message);
            }
            conn.Close();
        }
        //保存
        private void save()
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                List<SqlParameter> param0 = new List<SqlParameter>();
                param0.Add(new SqlParameter("@name", this.user_name.Text.ToString()));
                string selectSql = SqlHelper.GetSql("SelectByUserName");
                DataTable table = SqlHelper.Select(conn, tran, selectSql, param0);
                List<SqlParameter> param = new List<SqlParameter>();
                string quanxian = user_quanxian.Text.ToString();
                if (quanxian == string.Empty)
                {
                    MessageBox.Show("请选择用户权限");
                    conn.Close();
                    return;
                }
                param.Add(new SqlParameter("@name", user_name.Text));
                param.Add(new SqlParameter("@quanxian", quanxian));
                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("该用户已存在，请重新输入！");
                    conn.Close();
                    return;
                }
                else
                {
                    //保存用户
                    string sql = SqlHelper.GetSql("InsertUser");
                    sql.Replace("@name", user_name.Text);
                    sql.Replace("@quanxian", quanxian);
                    //加密
                    string password = FormsAuthentication.HashPasswordForStoringInConfigFile("123", "md5");
                    sql.Replace("@password", password);
                    param.Add(new SqlParameter("@password", password));
                    SqlHelper.Insert(conn, tran, sql, param);
                    //保存权限信息
                    string sql1 = SqlHelper.GetSql("InsertUserQuanxianByQuanxian");
                    List<SqlParameter> param_quanxian = new List<SqlParameter>();
                    param_quanxian.Add(new SqlParameter("@name", user_name.Text));
                    sql1.Replace("@name", user_name.Text);
                    //if (quanxian == "土地用户管理员" || quanxian == "税务用户管理员")
                    if (quanxian == "管理员")
                    {
                        param_quanxian.Add(new SqlParameter("@管理员", "1%"));
                        param_quanxian.Add(new SqlParameter("@土地用户", "%"));
                        param_quanxian.Add(new SqlParameter("@税务用户", "%"));
                        sql1.Replace("@管理员", "1%");
                        sql1.Replace("@土地用户", "%");
                        sql1.Replace("@税务用户", "%");
                    }
                    //else if (quanxian == "土地用户")
                    //{
                    //    param_quanxian.Add(new SqlParameter("@管理员", "%"));
                    //    param_quanxian.Add(new SqlParameter("@土地用户", "1%"));
                    //    param_quanxian.Add(new SqlParameter("@税务用户", "%"));
                    //    sql1.Replace("@土地用户", "1%");
                    //    sql1.Replace("@管理员", "%");
                    //    sql1.Replace("@税务用户", "%");
                    //}
                    else if (quanxian.Substring(quanxian.Length-2,2) == "街道")//各街道用户、税务用户
                    {
                        param_quanxian.Add(new SqlParameter("@管理员", "%"));
                        param_quanxian.Add(new SqlParameter("@土地用户", "%"));
                        param_quanxian.Add(new SqlParameter("@税务用户", "1%"));
                        sql1.Replace("@税务用户", "1");
                        sql1.Replace("@管理员", "%");
                        sql1.Replace("@土地用户", "%");
                    }
                    SqlHelper.Insert(conn, tran, sql1, param_quanxian);
                    tran.Commit();
                }
                MessageBox.Show("保存成功！");
                user_name.Text = "";
                user_quanxian.Text = "";
                List<SqlParameter> param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@name", "%"));
                conn.Close();
                Query(param1);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void update_tudiUser_Click(object sender, EventArgs e)
        {
            user_quanxian quanxian = new user_quanxian("tudi");
            quanxian.StartPosition = FormStartPosition.CenterParent;
            quanxian.Show();
        }

        private void update_shuiwuUser_Click(object sender, EventArgs e)
        {
            user_quanxian quanxian = new user_quanxian("shuiwu");
            quanxian.StartPosition = FormStartPosition.CenterParent;
            quanxian.Show();
        }

        private void grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow data = grid1.CurrentRow;
            if (data == null)
            {
                MessageBox.Show("请选择一行！");
            }
            user_quanxian quanxian = new user_quanxian(data.Cells["userName"].Value.ToString());
            quanxian.StartPosition = FormStartPosition.CenterScreen;
            quanxian.Show(this);
        }
    }
}
