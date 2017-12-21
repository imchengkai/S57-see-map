using EasyMap.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyMap
{
    public partial class SetTypeForm : MyForm
    {
        DataTable ParentChildTable = null;
        //DataTable PhotoList = null;
        int gridNum = 1;
        public delegate void SetLayerType(string no,LayerStyleForm form);
        public event SetLayerType setLayerType;
        public delegate void SetLayerStyle(VectorLayer layer, SolidBrush fillBrush, Pen outLinePen, Color textColor, Font textFont, bool enableOutLine, int hatchStyle, Pen LinePen, int Penstyle);
        public event SetLayerStyle setLayerStyle;
        public Map Map
        {
            get;
            set;
        }
        public SetTypeForm(decimal mapid)
        {
            InitializeComponent();

            //DataTable roottable = MapDBClass.GetLayerInformation(mapid);
            //查询影像图结构
            //PhotoList = MapDBClass.GetPhotoList();
            ParentChildTable = MapDBClass.GetLayerInformation(mapid);
            treeView1.Nodes.Clear();
            //if (roottable != null && roottable.Rows.Count == 1)
            //{
            TreeNode rootnode = new TreeNode("基础图层") { Tag = "0" };
            treeView1.Nodes.Add(rootnode);
            AddNode(rootnode);
            treeView1.SelectedNode = rootnode;

            TreeNode rootnode1 = new TreeNode("数据图层") { Tag = "4" };
            treeView1.Nodes.Add(rootnode1);
            AddNode(rootnode1);
            init();//gridview绑定值
        }

        //gridview绑定值
        private void init()
        {
            List<SqlParameter> param = new List<SqlParameter>();
            //绑定myDataGridView1
            param.Clear();
            param.Add(new SqlParameter("LayerTypeNo", "1"));
            DataTable table1= SqlHelper.Select(SqlHelper.GetSql("SelectLayerTypeChild"), param);
            myDataGridView1.Rows.Clear();
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                int row = myDataGridView1.Rows.Add();
                for (int j = 0; j < table1.Columns.Count; j++)
                {
                    string col = table1.Columns[j].ColumnName;
                    for (int k = 0; k < myDataGridView1.ColumnCount; k++)
                    {
                        if (myDataGridView1.Columns[k].DataPropertyName == col)
                        {
                            myDataGridView1.Rows[row].Cells[k].Value = table1.Rows[i][j];
                            break;
                        }
                    }
                }
            }

            //绑定myDataGridView2
            param.Clear();
            param.Add(new SqlParameter("LayerTypeNo", "2"));
            DataTable table2 = SqlHelper.Select(SqlHelper.GetSql("SelectLayerTypeChild"), param);
            myDataGridView2.Rows.Clear();
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                int row = myDataGridView2.Rows.Add();
                for (int j = 0; j < table2.Columns.Count; j++)
                {
                    string col = table2.Columns[j].ColumnName;
                    for (int k = 0; k < myDataGridView2.ColumnCount; k++)
                    {
                        if (myDataGridView2.Columns[k].DataPropertyName == col)
                        {
                            myDataGridView2.Rows[row].Cells[k].Value = table2.Rows[i][j];
                            break;
                        }
                    }
                }
            }

            //绑定myDataGridView3
            param.Clear();
            param.Add(new SqlParameter("LayerTypeNo", "3"));
            DataTable table3 = SqlHelper.Select(SqlHelper.GetSql("SelectLayerTypeChild"), param);
            myDataGridView3.Rows.Clear();
            for (int i = 0; i < table3.Rows.Count; i++)
            {
                int row = myDataGridView3.Rows.Add();
                for (int j = 0; j < table3.Columns.Count; j++)
                {
                    string col = table3.Columns[j].ColumnName;
                    for (int k = 0; k < myDataGridView3.ColumnCount; k++)
                    {
                        if (myDataGridView3.Columns[k].DataPropertyName == col)
                        {
                            myDataGridView3.Rows[row].Cells[k].Value = table3.Rows[i][j];
                            break;
                        }
                    }
                }
            }

            //绑定myDataGridView4
            param.Clear();
            param.Add(new SqlParameter("LayerTypeNo", "4"));
            DataTable table4 = SqlHelper.Select(SqlHelper.GetSql("SelectLayerTypeChild"), param);
            myDataGridView4.Rows.Clear();
            for (int i = 0; i < table4.Rows.Count; i++)
            {
                int row = myDataGridView4.Rows.Add();
                for (int j = 0; j < table4.Columns.Count; j++)
                {
                    string col = table4.Columns[j].ColumnName;
                    for (int k = 0; k < myDataGridView4.ColumnCount; k++)
                    {
                        if (myDataGridView4.Columns[k].DataPropertyName == col)
                        {
                            myDataGridView4.Rows[row].Cells[k].Value = table4.Rows[i][j];
                            break;
                        }
                    }
                }
            }
        }

        //添加左边父节点
        private void AddNode(TreeNode parentnode)
        {
            int parentid = Int32.Parse(parentnode.Tag.ToString());
            var data = ParentChildTable.Select("ParentName='" + parentnode.Text + "'");
            if (data != null)
            {
                foreach (var rec in data)
                {
                    TreeNode node = new TreeNode(rec["LayerName"].ToString()) { Tag = rec["LayerId"] };
                    parentnode.Nodes.Add(node);
                    AddNode(node);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int id = Int32.Parse(treeView1.SelectedNode.Tag.ToString());
            var data = ParentChildTable.Select("1=1").ToList();
            foreach (TreeNode node in treeView1.Nodes)
            {
                RemoveIdFromList(node, data);
            }
            //myDataGridView1.Rows.Clear();
            //data.ForEach(row =>
            //    {
            //        var rowid = myDataGridView1.Rows.Add();
            //        myDataGridView1.Rows[rowid].Cells["LayerName1"].Value = row["LayerName"];
            //        myDataGridView1.Rows[rowid].Cells["LayerId1"].Value = row["LayerId"];
            //    });
        }

        private void RemoveIdFromList(TreeNode node, List<DataRow> list)
        {
            int id = Int32.Parse(node.Tag.ToString());
            list.RemoveAll(data => { return Int32.Parse(data["LayerId"].ToString()) == id; });
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
            gridNum = 1;

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
            gridNum = 1;
            //取得拖动的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            int id = Int32.Parse(moveNode.Tag.ToString());
            var data = ParentChildTable.Select("LayerId=" + id);
            if (data != null && data.Length == 1)
            {
                int rowid = myDataGridView1.Rows.Add();
                myDataGridView1.Rows[rowid].Cells["LayerName1"].Value = data[0]["LayerName"];
                myDataGridView1.Rows[rowid].Cells["LayerId1"].Value = data[0]["LayerId"];
                foreach (TreeNode subnode in moveNode.Nodes)
                {
                    id = Int32.Parse(subnode.Tag.ToString());
                    data = ParentChildTable.Select("LayerId=" + id);
                    if (data != null && data.Length == 1)
                    {
                        rowid = myDataGridView1.Rows.Add();
                        myDataGridView1.Rows[rowid].Cells["LayerName1"].Value = data[0]["LayerName"];
                        myDataGridView1.Rows[rowid].Cells["LayerId1"].Value = data[0]["LayerId"];
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
            gridNum = 1;
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
            if (gridNum == 1)
            {
                for (int i = myDataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView1.Rows[i].Selected)
                    {
                        TreeNode node = new TreeNode(myDataGridView1.Rows[i].Cells["LayerName1"].Value.ToString());
                        node.Tag = myDataGridView1.Rows[i].Cells["LayerId1"].Value;
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
            else if (gridNum == 2)
            {
                for (int i = myDataGridView2.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView2.Rows[i].Selected)
                    {
                        TreeNode node = new TreeNode(myDataGridView2.Rows[i].Cells["LayerName2"].Value.ToString());
                        node.Tag = myDataGridView2.Rows[i].Cells["LayerId2"].Value;
                        parentnodes.Add(node);
                    }
                }
                for (int i = myDataGridView2.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView2.Rows[i].Selected)
                    {
                        myDataGridView2.Rows.RemoveAt(i);
                    }
                }
            }
            else if (gridNum == 3)
            {
                for (int i = myDataGridView3.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView3.Rows[i].Selected)
                    {
                        TreeNode node = new TreeNode(myDataGridView3.Rows[i].Cells["LayerName3"].Value.ToString());
                        node.Tag = myDataGridView3.Rows[i].Cells["LayerId3"].Value;
                        parentnodes.Add(node);
                    }
                }
                for (int i = myDataGridView3.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView3.Rows[i].Selected)
                    {
                        myDataGridView3.Rows.RemoveAt(i);
                    }
                }
            }
            else if (gridNum == 4)
            {
                for (int i = myDataGridView4.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView4.Rows[i].Selected)
                    {
                        TreeNode node = new TreeNode(myDataGridView4.Rows[i].Cells["LayerName4"].Value.ToString());
                        node.Tag = myDataGridView4.Rows[i].Cells["LayerId4"].Value;
                        parentnodes.Add(node);
                    }
                }
                for (int i = myDataGridView4.Rows.Count - 1; i >= 0; i--)
                {
                    if (myDataGridView4.Rows[i].Selected)
                    {
                        myDataGridView4.Rows.RemoveAt(i);
                    }
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
                param.Add(new SqlParameter("LayerName", node.Text));
                param.Add(new SqlParameter("LayerId", subnode.Text));
                SqlHelper.Insert(conn, tran, sql, param);
                SaveParentChild(subnode, conn, tran);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                //清空图层分类关系表
                string deletesql = SqlHelper.GetSql("DeleteLayerTypeChildAll");
                SqlHelper.Delete(conn, tran, deletesql, null);
                //保存新的图层分类
                //街道
                for (int row1 = 0; row1 < myDataGridView1.Rows.Count; row1++)
                {
                    param.Clear();
                    param.Add(new SqlParameter("no", "1"));
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView1.Rows[row1].Cells["LayerId1"].Value));
                    param.Add(new SqlParameter("LayerName", myDataGridView1.Rows[row1].Cells["LayerName1"].Value));
                    param.Add(new SqlParameter("LayerTypeNo", "1"));
                    SqlHelper.Insert(conn, tran, SqlHelper.GetSql("InsertLayerTypeChildAll"), param);
                    //获取街道图层样式
                    param.Clear();
                    param.Add(new SqlParameter("no", "1"));
                    DataTable table= SqlHelper.Select(conn, tran, SqlHelper.GetSql("SelectLayerType"), param);
                    //设置图层样式为统一设置的样式
                    param.Clear();
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView1.Rows[row1].Cells["LayerId1"].Value));
                    param.Add(new SqlParameter("Outline", table.Rows[0]["Outline"]));
                    param.Add(new SqlParameter("Fill", table.Rows[0]["Fill"]));
                    param.Add(new SqlParameter("Line", table.Rows[0]["Line"]));
                    param.Add(new SqlParameter("EnableOutline", table.Rows[0]["EnableOutline"]));
                    param.Add(new SqlParameter("OutlineWidth", table.Rows[0]["OutlineWidth"]));
                    param.Add(new SqlParameter("LineWidth", table.Rows[0]["LineWidth"]));
                    param.Add(new SqlParameter("hatchbrush", table.Rows[0]["hatchbrush"]));
                    param.Add(new SqlParameter("TextColor", table.Rows[0]["TextColor"]));
                    param.Add(new SqlParameter("TextFont", table.Rows[0]["TextFont"]));
                    param.Add(new SqlParameter("Penstyle", table.Rows[0]["Penstyle"]));
                    SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerType"), param);
                    //更新颜色到地图
                    VectorLayer layer = Map.Layers[myDataGridView1.Rows[row1].Cells["LayerName1"].Value.ToString().Trim()] as VectorLayer;
                    if (setLayerStyle != null)
                    {
                        SolidBrush fillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
                        Pen linePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
                        DashStyle linePenDashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
                        Pen outLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
                        bool enableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
                        int hatchbrush = -1;
                        if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
                        {
                            hatchbrush = -1;
                        }
                        int hatchStyle = hatchbrush;
                        Font textFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
                        Color textColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
                        int penstyle = (int)table.Rows[0]["Penstyle"];
                        setLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutline, hatchStyle, linePen, penstyle);
                    }
                }
                //区界
                for (int row2 = 0; row2 < myDataGridView2.Rows.Count; row2++)
                {
                    param.Clear();
                    param.Add(new SqlParameter("no", "1"));
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView2.Rows[row2].Cells["LayerId2"].Value));
                    param.Add(new SqlParameter("LayerName", myDataGridView2.Rows[row2].Cells["LayerName2"].Value));
                    param.Add(new SqlParameter("LayerTypeNo", "2"));
                    SqlHelper.Insert(conn, tran, SqlHelper.GetSql("InsertLayerTypeChildAll"), param);
                    //获取区界图层样式
                    param.Clear();
                    param.Add(new SqlParameter("no", "2"));
                    DataTable table = SqlHelper.Select(conn, tran, SqlHelper.GetSql("SelectLayerType"), param);
                    //设置图层样式为统一设置的样式
                    param.Clear();
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView2.Rows[row2].Cells["LayerId2"].Value));
                    param.Add(new SqlParameter("Outline", table.Rows[0]["Outline"]));
                    param.Add(new SqlParameter("Fill", table.Rows[0]["Fill"]));
                    param.Add(new SqlParameter("Line", table.Rows[0]["Line"]));
                    param.Add(new SqlParameter("EnableOutline", table.Rows[0]["EnableOutline"]));
                    param.Add(new SqlParameter("OutlineWidth", table.Rows[0]["OutlineWidth"]));
                    param.Add(new SqlParameter("LineWidth", table.Rows[0]["LineWidth"]));
                    param.Add(new SqlParameter("hatchbrush", table.Rows[0]["hatchbrush"]));
                    param.Add(new SqlParameter("TextColor", table.Rows[0]["TextColor"]));
                    param.Add(new SqlParameter("TextFont", table.Rows[0]["TextFont"]));
                    param.Add(new SqlParameter("Penstyle", table.Rows[0]["Penstyle"]));
                    SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerType"), param);
                    //更新颜色到地图
                    VectorLayer layer = Map.Layers[myDataGridView2.Rows[row2].Cells["LayerName2"].Value.ToString().Trim()] as VectorLayer;
                    if (setLayerStyle != null)
                    {
                        SolidBrush fillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
                        Pen linePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
                        DashStyle linePenDashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
                        Pen outLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
                        bool enableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
                        int hatchbrush = -1;
                        if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
                        {
                            hatchbrush = -1;
                        }
                        int hatchStyle = hatchbrush;
                        Font textFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
                        Color textColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
                        int penstyle = (int)table.Rows[0]["Penstyle"];
                        setLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutline, hatchStyle, linePen, penstyle);
                    }
                }

                //注记
                for (int row3 = 0; row3 < myDataGridView3.Rows.Count; row3++)
                {
                    param.Clear();
                    param.Add(new SqlParameter("no", "1"));
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView3.Rows[row3].Cells["LayerId3"].Value));
                    param.Add(new SqlParameter("LayerName", myDataGridView3.Rows[row3].Cells["LayerName3"].Value));
                    param.Add(new SqlParameter("LayerTypeNo", "3"));
                    SqlHelper.Insert(conn, tran, SqlHelper.GetSql("InsertLayerTypeChildAll"), param);
                    //获取注记图层样式
                    param.Clear();
                    param.Add(new SqlParameter("no", "3"));
                    DataTable table = SqlHelper.Select(conn, tran, SqlHelper.GetSql("SelectLayerType"), param);
                    //设置图层样式为统一设置的样式
                    param.Clear();
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView3.Rows[row3].Cells["LayerId3"].Value));
                    param.Add(new SqlParameter("Outline", table.Rows[0]["Outline"]));
                    param.Add(new SqlParameter("Fill", table.Rows[0]["Fill"]));
                    param.Add(new SqlParameter("Line", table.Rows[0]["Line"]));
                    param.Add(new SqlParameter("EnableOutline", table.Rows[0]["EnableOutline"]));
                    param.Add(new SqlParameter("OutlineWidth", table.Rows[0]["OutlineWidth"]));
                    param.Add(new SqlParameter("LineWidth", table.Rows[0]["LineWidth"]));
                    param.Add(new SqlParameter("hatchbrush", table.Rows[0]["hatchbrush"]));
                    param.Add(new SqlParameter("TextColor", table.Rows[0]["TextColor"]));
                    param.Add(new SqlParameter("TextFont", table.Rows[0]["TextFont"]));
                    param.Add(new SqlParameter("Penstyle", table.Rows[0]["Penstyle"]));
                    SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerType"), param);
                    //更新颜色到地图
                    VectorLayer layer = Map.Layers[myDataGridView3.Rows[row3].Cells["LayerName3"].Value.ToString().Trim()] as VectorLayer;
                    if (setLayerStyle != null)
                    {
                        SolidBrush fillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
                        Pen linePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
                        DashStyle linePenDashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
                        Pen outLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
                        bool enableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
                        int hatchbrush = -1;
                        if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
                        {
                            hatchbrush = -1;
                        }
                        int hatchStyle = hatchbrush;
                        Font textFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
                        Color textColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
                        int penstyle = (int)table.Rows[0]["Penstyle"];
                        setLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutline, hatchStyle, linePen, penstyle);
                    }
                }

                //宗地
                for (int row4 = 0; row4 < myDataGridView4.Rows.Count; row4++)
                {
                    param.Clear();
                    param.Add(new SqlParameter("no", "1"));
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView4.Rows[row4].Cells["LayerId4"].Value));
                    param.Add(new SqlParameter("LayerName", myDataGridView4.Rows[row4].Cells["LayerName4"].Value));
                    param.Add(new SqlParameter("LayerTypeNo", "4"));
                    SqlHelper.Insert(conn, tran, SqlHelper.GetSql("InsertLayerTypeChildAll"), param);
                    //获取宗地图层样式
                    param.Clear();
                    param.Add(new SqlParameter("no", "4"));
                    DataTable table = SqlHelper.Select(conn, tran, SqlHelper.GetSql("SelectLayerType"), param);
                    //设置图层样式为统一设置的样式
                    param.Clear();
                    param.Add(new SqlParameter("MapId", Map.MapId.ToString()));
                    param.Add(new SqlParameter("LayerId", myDataGridView4.Rows[row4].Cells["LayerId4"].Value));
                    param.Add(new SqlParameter("Outline", table.Rows[0]["Outline"]));
                    param.Add(new SqlParameter("Fill", table.Rows[0]["Fill"]));
                    param.Add(new SqlParameter("Line", table.Rows[0]["Line"]));
                    param.Add(new SqlParameter("EnableOutline", table.Rows[0]["EnableOutline"]));
                    param.Add(new SqlParameter("OutlineWidth", table.Rows[0]["OutlineWidth"]));
                    param.Add(new SqlParameter("LineWidth", table.Rows[0]["LineWidth"]));
                    param.Add(new SqlParameter("hatchbrush", table.Rows[0]["hatchbrush"]));
                    param.Add(new SqlParameter("TextColor", table.Rows[0]["TextColor"]));
                    param.Add(new SqlParameter("TextFont", table.Rows[0]["TextFont"]));
                    param.Add(new SqlParameter("Penstyle", table.Rows[0]["Penstyle"]));
                    SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerType"), param);//更新颜色到地图
                    VectorLayer layer = Map.Layers[myDataGridView4.Rows[row4].Cells["LayerName4"].Value.ToString().Trim()] as VectorLayer;
                    if (setLayerStyle != null)
                    {
                        SolidBrush fillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
                        Pen linePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
                        DashStyle linePenDashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
                        Pen outLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
                        bool enableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
                        int hatchbrush = -1;
                        if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
                        {
                            hatchbrush = -1;
                        }
                        int hatchStyle = hatchbrush;
                        Font textFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
                        Color textColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
                        int penstyle = (int)table.Rows[0]["Penstyle"];
                        setLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutline, hatchStyle, linePen, penstyle);
                    }
                }
                //更新各图层样式为设置的样式

                tran.Commit();
                conn.Close();
                MessageBox.Show("保存成功。");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败。");
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

        private void myDataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            gridNum = 2;
            //取得拖动的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            int id = Int32.Parse(moveNode.Tag.ToString());
            var data = ParentChildTable.Select("LayerId=" + id);
            if (data != null && data.Length == 1)
            {
                int rowid = myDataGridView2.Rows.Add();
                myDataGridView2.Rows[rowid].Cells["LayerName2"].Value = data[0]["LayerName"];
                myDataGridView2.Rows[rowid].Cells["LayerId2"].Value = data[0]["LayerId"];
                foreach (TreeNode subnode in moveNode.Nodes)
                {
                    id = Int32.Parse(subnode.Tag.ToString());
                    data = ParentChildTable.Select("LayerId=" + id);
                    if (data != null && data.Length == 1)
                    {
                        rowid = myDataGridView2.Rows.Add();
                        myDataGridView2.Rows[rowid].Cells["LayerName2"].Value = data[0]["LayerName"];
                        myDataGridView2.Rows[rowid].Cells["LayerId2"].Value = data[0]["LayerId"];
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

        private void myDataGridView2_DragEnter(object sender, DragEventArgs e)
        {
            gridNum = 2;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void myDataGridView3_DragDrop(object sender, DragEventArgs e)
        {
            gridNum = 3;
            //取得拖动的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            int id = Int32.Parse(moveNode.Tag.ToString());
            var data = ParentChildTable.Select("LayerId=" + id);
            if (data != null && data.Length == 1)
            {
                int rowid = myDataGridView3.Rows.Add();
                myDataGridView3.Rows[rowid].Cells["LayerName3"].Value = data[0]["LayerName"];
                myDataGridView3.Rows[rowid].Cells["LayerId3"].Value = data[0]["LayerId"];
                foreach (TreeNode subnode in moveNode.Nodes)
                {
                    id = Int32.Parse(subnode.Tag.ToString());
                    data = ParentChildTable.Select("LayerId=" + id);
                    if (data != null && data.Length == 1)
                    {
                        rowid = myDataGridView3.Rows.Add();
                        myDataGridView3.Rows[rowid].Cells["LayerName3"].Value = data[0]["LayerName"];
                        myDataGridView3.Rows[rowid].Cells["LayerId3"].Value = data[0]["LayerId"];
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

        private void myDataGridView3_DragEnter(object sender, DragEventArgs e)
        {
            gridNum = 3;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void myDataGridView4_DragDrop(object sender, DragEventArgs e)
        {
            gridNum = 4;
            //取得拖动的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            int id = Int32.Parse(moveNode.Tag.ToString());
            var data = ParentChildTable.Select("LayerId=" + id);
            if (data != null && data.Length == 1)
            {
                int rowid = myDataGridView4.Rows.Add();
                myDataGridView4.Rows[rowid].Cells["LayerName4"].Value = data[0]["LayerName"];
                myDataGridView4.Rows[rowid].Cells["LayerId4"].Value = data[0]["LayerId"];
                foreach (TreeNode subnode in moveNode.Nodes)
                {
                    id = Int32.Parse(subnode.Tag.ToString());
                    data = ParentChildTable.Select("LayerId=" + id);
                    if (data != null && data.Length == 1)
                    {
                        rowid = myDataGridView4.Rows.Add();
                        myDataGridView4.Rows[rowid].Cells["LayerName4"].Value = data[0]["LayerName"];
                        myDataGridView4.Rows[rowid].Cells["LayerId4"].Value = data[0]["LayerId"];
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

        private void myDataGridView4_DragEnter(object sender, DragEventArgs e)
        {
            gridNum = 4;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void myDataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            gridNum = 2;
            if (e.Button == MouseButtons.Left)
            {
                myDataGridView2.DoDragDrop("test", DragDropEffects.Move);
            }
        }

        private void myDataGridView3_MouseMove(object sender, MouseEventArgs e)
        {
            gridNum = 3;
            if (e.Button == MouseButtons.Left)
            {
                myDataGridView3.DoDragDrop("test", DragDropEffects.Move);
            }
        }

        private void myDataGridView4_MouseMove(object sender, MouseEventArgs e)
        {
            gridNum = 4;
            if (e.Button == MouseButtons.Left)
            {
                myDataGridView4.DoDragDrop("test", DragDropEffects.Move);
            }
        }

        //街道样式
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //打开样式画面
            LayerStyleForm form = new LayerStyleForm();

            //查询街道原有样式
            string sql = SqlHelper.GetSql("SelectLayerType");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("no", "1"));
            DataTable table = SqlHelper.Select(sql, param);

            //将原有样式显示在调整样式画面中
            form.FillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
            form.LinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
            form.LinePen.DashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
            form.OutLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
            form.EnableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
            int hatchbrush = -1;
            if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
            {
                hatchbrush = -1;
            }
            form.HatchStyle = hatchbrush;
            form.TextFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
            form.TextColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
            form.Penstyle = (int)table.Rows[0]["Penstyle"];
            if (form.ShowDialog() == DialogResult.OK)
            {
                setLayerType("1",form);
            }
        }
        
        //获取表格各列值
        private string GetGridValue(DataGridViewRow row, string col)
        {
            if (row.Cells[col].Value != null)
            {
                return row.Cells[col].Value.ToString();
            }
            return string.Empty;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //打开样式画面
            LayerStyleForm form = new LayerStyleForm();

            //查询街道原有样式
            string sql = SqlHelper.GetSql("SelectLayerType");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("no", "2"));
            DataTable table = SqlHelper.Select(sql, param);

            //将原有样式显示在调整样式画面中
            form.FillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
            form.LinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
            form.LinePen.DashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
            form.OutLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
            form.EnableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
            int hatchbrush = -1;
            if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
            {
                hatchbrush = -1;
            }
            form.HatchStyle = hatchbrush;
            form.TextFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
            form.TextColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
            if (form.ShowDialog() == DialogResult.OK)
            {
                setLayerType("2",form);
            }
        }


        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //打开样式画面
            LayerStyleForm form = new LayerStyleForm();

            //查询街道原有样式
            string sql = SqlHelper.GetSql("SelectLayerType");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("no", "3"));
            DataTable table = SqlHelper.Select(sql, param);

            //将原有样式显示在调整样式画面中
            form.FillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
            form.LinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
            form.LinePen.DashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
            form.OutLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
            form.EnableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
            int hatchbrush = -1;
            if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
            {
                hatchbrush = -1;
            }
            form.HatchStyle = hatchbrush;
            form.TextFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
            form.TextColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
            if (form.ShowDialog() == DialogResult.OK)
            {
                setLayerType("3", form);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //打开样式画面
            LayerStyleForm form = new LayerStyleForm();

            //查询街道原有样式
            string sql = SqlHelper.GetSql("SelectLayerType");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("no", "4"));
            DataTable table = SqlHelper.Select(sql, param);

            //将原有样式显示在调整样式画面中
            form.FillBrush = new SolidBrush(Color.FromArgb((int)table.Rows[0]["Fill"]));
            form.LinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Line"]), (int)table.Rows[0]["LineWidth"]);
            form.LinePen.DashStyle = (DashStyle)((int)table.Rows[0]["Line"]);
            form.OutLinePen = new Pen(Color.FromArgb((int)table.Rows[0]["Outline"]), (int)table.Rows[0]["OutlineWidth"]);
            form.EnableOutline = table.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
            int hatchbrush = -1;
            if (!int.TryParse(table.Rows[0]["hatchbrush"].ToString(), out hatchbrush))
            {
                hatchbrush = -1;
            }
            form.HatchStyle = hatchbrush;
            form.TextFont = (Font)Common.DeserializeObject((byte[])table.Rows[0]["TextFont"]);
            form.TextColor = Color.FromArgb((int)table.Rows[0]["TextColor"]);
            if (form.ShowDialog() == DialogResult.OK)
            {
                setLayerType("4",form);
            }
        }
        //街道 恢复初始状态
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                param.Add(new SqlParameter("no", "1"));
                SqlHelper.Delete(conn, tran, SqlHelper.GetSql("deleteLayerType"), param);//删除街道样式记录
                param.Clear();
                param.Add(new SqlParameter("no", "1"));
                SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerTypeInit"), param);//保存街道样式初始值
                tran.Commit();
                conn.Close();
                MessageBox.Show("成功恢复初始状态。");
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("恢复初始状态失败。");
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
        //区界 恢复初始状态
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            SqlConnection conn = null;
            SqlTransaction tran = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                param.Add(new SqlParameter("no", "2"));
                SqlHelper.Delete(conn, tran, SqlHelper.GetSql("deleteLayerType"), param);//删除区界样式记录
                param.Clear();
                param.Add(new SqlParameter("no", "2"));
                SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerTypeInit"), param);//保存区界样式初始值
                tran.Commit();
                conn.Close();
                MessageBox.Show("成功恢复初始状态。");
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("恢复初始状态失败。");
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
        //注记 恢复初始状态
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                param.Add(new SqlParameter("no", "3"));
                SqlHelper.Delete(conn, tran, SqlHelper.GetSql("deleteLayerType"), param);//删除注记样式记录
                param.Clear();
                param.Add(new SqlParameter("no", "3"));
                SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerTypeInit"), param);//保存注记样式初始值
                tran.Commit();
                conn.Close();
                MessageBox.Show("成功恢复初始状态。");
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("恢复初始状态失败。");
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
        //宗地 恢复初始状态
        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                param.Add(new SqlParameter("no", "4"));
                SqlHelper.Delete(conn, tran, SqlHelper.GetSql("deleteLayerType"), param);//删除宗地样式记录
                param.Clear();
                param.Add(new SqlParameter("no", "4"));
                SqlHelper.Update(conn, tran, SqlHelper.GetSql("updateLayerTypeInit"), param);//保存宗地样式初始值
                tran.Commit();
                conn.Close();
                MessageBox.Show("成功恢复初始状态。");
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("恢复初始状态失败。");
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
        
    

