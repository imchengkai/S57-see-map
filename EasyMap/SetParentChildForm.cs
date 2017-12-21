using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyMap
{
    public partial class SetParentChildForm : MyForm
    {
        DataTable ParentChildTable = null;
        DataTable PhotoList = null;

        public SetParentChildForm(decimal mapid)
        {
            InitializeComponent();

            DataTable roottable = MapDBClass.GetTifInformation(mapid);
            //查询影像图结构
            PhotoList = MapDBClass.GetPhotoList();
            ParentChildTable = MapDBClass.GetChildList();
            treeView1.Nodes.Clear();
            if (roottable != null && roottable.Rows.Count == 1)
            {
                TreeNode rootnode = new TreeNode(roottable.Rows[0]["Name"].ToString()) { Tag = roottable.Rows[0]["PhotoId"] };
                treeView1.Nodes.Add(rootnode);
                AddNode(rootnode);
                treeView1.SelectedNode = rootnode;
            }
        }

        private void AddNode(TreeNode parentnode)
        {
            int parentid = (int)parentnode.Tag;
            var data = ParentChildTable.Select("ParentName='" + parentnode.Text + "'");
            if (data != null)
            {
                foreach (var rec in data)
                {
                    TreeNode node = new TreeNode(rec["Name"].ToString()) { Tag = rec["PhotoId"] };
                    parentnode.Nodes.Add(node);
                    AddNode(node);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int id = (int)treeView1.SelectedNode.Tag;
            var data = PhotoList.Select("1=1").ToList();
            foreach (TreeNode node in treeView1.Nodes)
            {
                RemoveIdFromList(node, data);
            }
            myDataGridView1.Rows.Clear();
            data.ForEach(row =>
                {
                    var rowid = myDataGridView1.Rows.Add();
                    myDataGridView1.Rows[rowid].Cells["name"].Value = row["Name"];
                    myDataGridView1.Rows[rowid].Cells["filename"].Value = row["FileName"];
                    myDataGridView1.Rows[rowid].Cells["PhotoId"].Value = row["PhotoId"];
                });
        }

        private void RemoveIdFromList(TreeNode node, List<DataRow> list)
        {
            int id = (int)node.Tag;
            list.RemoveAll(data => { return (int)data["PhotoId"] == id; });
            foreach (TreeNode subnode in node.Nodes)
            {
                RemoveIdFromList(subnode, list);
            }
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void myDataGridView1_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void myDataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            //取得拖动的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            int id = (int)moveNode.Tag;
            var data = PhotoList.Select("PhotoId=" + id);
            if (data != null && data.Length == 1)
            {
                int rowid = myDataGridView1.Rows.Add();
                myDataGridView1.Rows[rowid].Cells["Name"].Value = data[0]["Name"];
                myDataGridView1.Rows[rowid].Cells["FileName"].Value = data[0]["FileName"];
                myDataGridView1.Rows[rowid].Cells["PhotoId"].Value = data[0]["PhotoId"];
                foreach (TreeNode subnode in moveNode.Nodes)
                {
                    id = (int)subnode.Tag;
                    data = PhotoList.Select("PhotoId=" + id);
                    if (data != null && data.Length == 1)
                    {
                        rowid = myDataGridView1.Rows.Add();
                        myDataGridView1.Rows[rowid].Cells["Name"].Value = data[0]["Name"];
                        myDataGridView1.Rows[rowid].Cells["FileName"].Value = data[0]["FileName"];
                        myDataGridView1.Rows[rowid].Cells["PhotoId"].Value = data[0]["PhotoId"];
                    }
                }
            }
            if (moveNode.Parent == null)
            {
                treeView1.Nodes.Clear();
            }
            else
            {
                moveNode.Parent.Nodes.Remove(moveNode);
            }
        }

        private void myDataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                myDataGridView1.DoDragDrop("test", DragDropEffects.Move);
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            Point p = treeView1.PointToClient(new Point(e.X, e.Y));
            TreeViewHitTestInfo index = treeView1.HitTest(p);
            var parentnodes=treeView1.Nodes;
            if (index.Node != null)
            {
                parentnodes = index.Node.Nodes;
            }
            for (int i = myDataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if (myDataGridView1.Rows[i].Selected)
                {
                    TreeNode node = new TreeNode(myDataGridView1.Rows[i].Cells["Name"].Value.ToString());
                    node.Tag = myDataGridView1.Rows[i].Cells["PhotoId"].Value;
                    parentnodes.Add(node);
                }
            }
            for (int i = myDataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if (myDataGridView1.Rows[i].Selected)
                {
                    myDataGridView1.Rows.RemoveAt(i);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveParentChild(TreeNode node, SqlConnection conn, SqlTransaction tran)
        {
            string sql = SqlHelper.GetSql("InsertPhotoChild");
            foreach (TreeNode subnode in node.Nodes)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("ParentName", node.Text));
                param.Add(new SqlParameter("ChildName", subnode.Text));
                SqlHelper.Insert(conn, tran, sql, param);
                SaveParentChild(subnode, conn, tran);
            }
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
                string deletesql = SqlHelper.GetSql("DeletePhotoChildAll");
                SqlHelper.Delete(conn, tran, deletesql, null);
                foreach (TreeNode node in treeView1.Nodes)
                {
                    SaveParentChild(node, conn, tran);
                }
                tran.Commit();
                conn.Close();
                MessageBox.Show("保存成功。");
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
            }
        }

    }
}
