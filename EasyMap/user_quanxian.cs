using EasyMap;
using EasyMap.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Tax
{
    public partial class user_quanxian : MyForm
    {
        public string QujieTable { get; set; }
        public string JiedaoTable { get; set; }
        public decimal MapId { get; set; }
        public delegate void DoubleClickObjectEvent(BoundingBox box, decimal layerid, decimal objectid);
        public string user_name;

        public user_quanxian(string userName)
        {
            user_name = userName;
            InitializeComponent();
        }

        private DataTable Search()
        {
            string sql = null;

            string sql_quanxian = SqlHelper.GetSql("SelectByUserName");
            sql_quanxian = sql_quanxian.Replace("@name", user_name);
            DataTable table = SqlHelper.Select(sql_quanxian, null);
            if (user_name == "tudi")
            {
                sql = SqlHelper.GetSql("SelectQuanxian_tudi");
            }
            else if (user_name == "shuiwu")
            {
                sql = SqlHelper.GetSql("SelectQuanxian_shuiwu");//街道用户权限在管理员权限以下，管理员没有的权限，街道用户不可设置
            }
            else if (user_name == "guanliyuan")
            {
                sql = SqlHelper.GetSql("SelectQuanxian");
            }
            else
            {
                if (table.Rows.Count > 0)
                {
                    if (table.Rows[0]["权限"].ToString() == "土地用户")
                    {
                        sql = SqlHelper.GetSql("SelectQuanxian_tudi");
                    }
                    //else if (table.Rows[0]["权限"].ToString() == "税务用户")
                    else if (table.Rows[0]["权限"].ToString().Substring(table.Rows[0]["权限"].ToString().Length-2,2) == "街道")//街道用户
                    {
                        sql = SqlHelper.GetSql("SelectQuanxian_shuiwu");
                    }
                    else
                    {
                        sql = SqlHelper.GetSql("SelectQuanxian");//管理员
                    }
                }
                else
                {
                    sql = SqlHelper.GetSql("SelectQuanxian");//管理员
                }
            }
            return SqlHelper.Select(sql, null);
        }

        private string GetValue(DataRow row, string col)
        {
            if (row[col] == null)
            {
                return string.Empty;
            }
            return row[col].ToString();
        }

        private void SetTree()
        {
            DataTable table = new DataTable();
            table = Search();
            treeView1.Nodes[0].Nodes.Clear();

            string nodename = string.Empty;
            TreeNode node = null;
            TreeNode subnode = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                if (i == 0 || table.Rows[i]["父节点名称"].ToString() != table.Rows[i - 1]["父节点名称"].ToString())
                {
                    nodename = GetValue(row, "节点名称");
                    node = new TreeNode(nodename);
                    node.Tag = GetValue(row, "编号") + "," + GetValue(row, "节点名称");
                    treeView1.Nodes[0].Nodes.Add(node);
                }
                else
                {
                    subnode = new TreeNode(GetValue(row, "节点名称"));
                    subnode.Tag = GetValue(row, "编号") + "," + GetValue(row, "节点名称");
                    node.Nodes.Add(subnode);
                }
                DataTable quanxianUser = getQuanxianUser();
                foreach (DataRow rowQuanxian in quanxianUser.Rows)
                {
                    if (node != null)
                    {
                        if (rowQuanxian["节点名称"].ToString() == node.Text)
                        {
                            node.Checked = true;
                        }
                    }
                    if (subnode != null)
                    {
                        if (rowQuanxian["节点名称"].ToString() == subnode.Text)
                        {
                            subnode.Checked = true;
                        }
                    }
                }
            }
        }

        //获取用户权限
        private DataTable getQuanxianUser()
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            user user = new user();
            DataTable quanxian = new DataTable();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                List<SqlParameter> param = new List<SqlParameter>();
                string sql;
                //if (user_name == "tudi")//土地用户
                //{
                //    sql = SqlHelper.GetSql("SelectUserQuanxian_tudi");
                //}
                //else 
                if (user_name == "shuiwu")//街道用户
                {
                    sql = SqlHelper.GetSql("SelectUserQuanxian_shuiwu");
                }
                else if (user_name == "guanliyuan")//管理员
                {
                    sql = SqlHelper.GetSql("SelectUserQuanxian_guanli");
                }
                else
                {
                    param.Add(new SqlParameter("@name", user_name));
                    sql = SqlHelper.GetSql("SelectUserQuanxian");
                    sql.Replace("@name", user_name);
                }
                quanxian = SqlHelper.Select(conn, tran, sql, param);
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
            return quanxian;
        }

        //获取所有权限名称
        private string getQuanxianName()
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            user user = new user();
            string quanxian = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@name", user._userName));
                string sql = SqlHelper.GetSql("SelectByUserName");
                sql.Replace("@name", user._userName);
                DataTable table = SqlHelper.Select(conn, tran, sql, param);
                quanxian = table.Rows[0]["权限"].ToString();
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
            return quanxian;
        }
        private void TaxViewForm_Load(object sender, EventArgs e)
        {
            SetTree();
        }
        private object DeserializeObject(byte[] pBytes)
        {
            try
            {
                object newOjb = null;
                if (pBytes == null)
                {
                    return newOjb;
                }
                System.IO.MemoryStream memory = new System.IO.MemoryStream(pBytes);
                memory.Position = 0;
                BinaryFormatter formatter = new BinaryFormatter();
                newOjb = formatter.Deserialize(memory);
                memory.Close();
                memory.Dispose();
                memory = null;
                return newOjb;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        //父级复选框选中，子级复选框全部选中
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Checked == true)
                {
                    if (e.Node.Text == "权限")
                    {
                        foreach (TreeNode node in treeView1.Nodes[0].Nodes)
                        {
                            node.Checked = true;
                            foreach (TreeNode subNode in node.Nodes)
                            {
                                subNode.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode node in treeView1.Nodes[0].Nodes)
                        {
                            if (e.Node == node)
                            {
                                foreach (TreeNode subNode in node.Nodes)
                                {
                                    subNode.Checked = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (e.Node.Text == "权限")
                    {
                        foreach (TreeNode node in treeView1.Nodes[0].Nodes)
                        {
                            node.Checked = false;
                            foreach (TreeNode subNode in node.Nodes)
                            {
                                subNode.Checked = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeNode node in treeView1.Nodes[0].Nodes)
                        {
                            if (e.Node == node)
                            {
                                foreach (TreeNode subNode in node.Nodes)
                                {
                                    subNode.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }
        //保存权限信息
        private void save_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            List<string> list = new List<string>();
            conn = SqlHelper.GetConnection();
            conn.Open();
            try
            {
                ////土地权限修改
                //if (user_name == "tudi")
                //{
                //    tran = conn.BeginTransaction();
                //    string sqlDel = SqlHelper.GetSql("delTudiQuanxian");
                //    SqlHelper.Delete(conn, tran, sqlDel, null);
                //    string sql = SqlHelper.GetSql("updateTudiQuanxian");

                //    List<SqlParameter> param = new List<SqlParameter>();
                //    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                //    {
                //        if (subNode.Checked == true)
                //        {
                //            param = new List<SqlParameter>();
                //            param.Add(new SqlParameter("@节点名称", subNode.Text));
                //            sql.Replace("@节点名称", subNode.Text);
                //            SqlHelper.Update(conn, tran, sql, param);
                //        } 
                //    }
                //    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                //    {
                //        foreach (TreeNode node in subNode.Nodes)
                //        {
                //            if (node.Checked == true)
                //            {
                //                List<SqlParameter>  param1 = new List<SqlParameter>();
                //                param1.Add(new SqlParameter("@节点名称", node.Text));
                //                sql.Replace("@节点名称", node.Text);
                //                SqlHelper.Update(conn, tran, sql, param1);
                //            }
                //        }
                //    }
                //    tran.Commit();
                //}
                //管理员权限修改
                if (user_name == "guanliyuan")
                {
                    tran = conn.BeginTransaction();
                    string sqlDel = SqlHelper.GetSql("delGuanliyuanQuanxian");
                    SqlHelper.Delete(conn, tran, sqlDel, null);
                    string sql = SqlHelper.GetSql("updateGuanliyuanQuanxian");
                    List<SqlParameter> param = new List<SqlParameter>();
                    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                    {
                        if (subNode.Checked == true)
                        {
                            param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@节点名称", subNode.Text));
                            sql.Replace("@节点名称", subNode.Text);
                            SqlHelper.Update(conn, tran, sql, param);
                        }
                    }
                    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                    {
                        foreach (TreeNode node in subNode.Nodes)
                        {
                            if (node.Checked == true)
                            {
                                List<SqlParameter> param1 = new List<SqlParameter>();
                                param1.Add(new SqlParameter("@节点名称", node.Text));
                                sql.Replace("@节点名称", node.Text);
                                SqlHelper.Update(conn, tran, sql, param1);
                            }
                        }
                    }
                    tran.Commit();
                }
                //税务权限修改
                else if (user_name == "shuiwu")
                {
                    tran = conn.BeginTransaction();
                    string sqlDel = SqlHelper.GetSql("delShuiwuQuanxian");
                    SqlHelper.Delete(conn, tran, sqlDel, null);
                    string sql = SqlHelper.GetSql("updateShuiwuQuanxian");
                    List<SqlParameter> param = new List<SqlParameter>();
                    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                    {
                        if (subNode.Checked == true)
                        {
                            param = new List<SqlParameter>();
                            param.Add(new SqlParameter("@节点名称", subNode.Text));
                            sql.Replace("@节点名称", subNode.Text);
                            SqlHelper.Update(conn, tran, sql, param);
                        }
                    }
                    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                    {
                        foreach (TreeNode node in subNode.Nodes)
                        {
                            if (node.Checked == true)
                            {
                                List<SqlParameter> param1 = new List<SqlParameter>();
                                param1.Add(new SqlParameter("@节点名称", node.Text));
                                sql.Replace("@节点名称", node.Text);
                                SqlHelper.Update(conn, tran, sql, param1);
                            }
                        }
                    }
                    tran.Commit();
                }
                else
                {
                    List<SqlParameter> paramDel = new List<SqlParameter>();
                    paramDel.Add(new SqlParameter("@name", user_name));
                    string delSql = SqlHelper.GetSql("DeleteUserQuanxian");
                    delSql.Replace("@name", user_name);
                    SqlHelper.Delete(conn, tran, delSql, paramDel);
                    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                    {
                        if (subNode.Checked == true)
                        {
                            tran = conn.BeginTransaction();
                            List<SqlParameter> paramSel = new List<SqlParameter>();
                            paramSel.Add(new SqlParameter("@name", subNode.Text));
                            string selSql = SqlHelper.GetSql("SelectNoByQuanxianName");
                            DataTable table = SqlHelper.Select(conn, tran, selSql, paramSel);

                            List<SqlParameter> paramInsert = new List<SqlParameter>();
                            paramInsert.Add(new SqlParameter("@name", user_name));
                            paramInsert.Add(new SqlParameter("@no", table.Rows[0]["编号"].ToString()));
                            string InsertSql = SqlHelper.GetSql("InsertUserQuanxian");
                            InsertSql.Replace("@no", table.Rows[0]["编号"].ToString());
                            InsertSql.Replace("@name", user_name);
                            SqlHelper.Insert(conn, tran, InsertSql, paramInsert);

                            tran.Commit();
                        }
                    }
                    foreach (TreeNode subNode in treeView1.Nodes[0].Nodes)
                    {
                        foreach (TreeNode node in subNode.Nodes)
                        {
                            if (node.Checked == true)
                            {
                                tran = conn.BeginTransaction();
                                List<SqlParameter> paramSel = new List<SqlParameter>();
                                paramSel.Add(new SqlParameter("@name", node.Text));
                                string selSql = SqlHelper.GetSql("SelectNoByQuanxianName");
                                DataTable table = SqlHelper.Select(conn, tran, selSql, paramSel);

                                List<SqlParameter> paramInsert = new List<SqlParameter>();
                                paramInsert.Add(new SqlParameter("@name", user_name));
                                paramInsert.Add(new SqlParameter("@no", table.Rows[0]["编号"].ToString()));
                                string InsertSql = SqlHelper.GetSql("InsertUserQuanxian");
                                InsertSql.Replace("@no", table.Rows[0]["编号"].ToString());
                                InsertSql.Replace("@name", user_name);
                                SqlHelper.Insert(conn, tran, InsertSql, paramInsert);

                                tran.Commit();
                            }
                        }
                    }
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
            this.Close();
            MessageBox.Show("保存成功！");
        }
    }
}
