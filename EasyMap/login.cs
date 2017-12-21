
using System;
using System.Data;
using System.Drawing;
using System.Web.Security;
using System.Windows.Forms;

namespace EasyMap
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txt_name.Text;
            string password = txt_password.Text;
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");

            string sql = SqlHelper.GetSql("SelectLoginUser");
            sql = sql.Replace("@name", userName);
            sql = sql.Replace("@password", password);
            DataTable table = SqlHelper.Select(sql, null);
            if (table.Rows.Count > 0)
            {
                user loginUser = new user() { };
                loginUser.userName = userName;
                loginUser.password = password;
                loginUser.quanxian = table.Rows[0]["权限"].ToString();
                //AreaData d = new AreaData() { };
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            return;
        }

        // 按键处理
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                string userName = txt_name.Text;
                string password = txt_password.Text;
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
                string sql = SqlHelper.GetSql("SelectLoginUser");
                sql = sql.Replace("@name", userName);
                sql = sql.Replace("@password", password);
                DataTable table = SqlHelper.Select(sql, null);
                if (table.Rows.Count > 0)
                {
                    user loginUser = new user() { };
                    loginUser.userName = userName;
                    loginUser.password = password;
                    loginUser.quanxian = table.Rows[0]["权限"].ToString();
                    //AreaData d = new AreaData() { };
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                }
            }
        }

        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }

        }
    }
}