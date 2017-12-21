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
    public partial class passwordUpdate : MyForm
    {
        public passwordUpdate()
        {
            InitializeComponent();
            Init();
        }
        //页面初始化
        private void Init()
        {
            user_name.Text = user._userName;
        }
       
        //保存事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            string new_password = password.Text;
            string new_password_confirm = password_confirm.Text;
            if (new_password == string.Empty)
            {
                MessageBox.Show("请输入新密码！");
                return;
            }
            else if (new_password_confirm == string.Empty)
            {
                MessageBox.Show("请输入确认密码！");
                return;
            }
            else if (new_password_confirm != new_password)
            {
                MessageBox.Show("密码不一致，请重新输入！");
                password.Text = "";
                password_confirm.Text = "";
                return;
            }
            save();
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
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@用户名", user._userName));
                param.Add(new SqlParameter("@密码", FormsAuthentication.HashPasswordForStoringInConfigFile(password.Text, "md5")));
                string sql = SqlHelper.GetSql("UpdateUserPassword");
                sql.Replace("@用户名", user._userName);
                sql.Replace("@密码", password.Text);
                SqlHelper.Update(conn, tran, sql, param);
                tran.Commit(); 
                conn.Close();
                this.Close();
                MessageBox.Show("保存成功,建议重新登录！");
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

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
