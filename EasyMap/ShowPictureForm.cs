using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using EasyMap.Controls;
using EasyMap.Layers;
using EasyMap.Geometries;
using EasyMap.Data.Providers;

namespace EasyMap
{
    public partial class ShowPictureForm : MyForm
    {
        //地图ID
        private decimal _MapId = 0;
        //图层ID
        private decimal _LayerId = 0;
        //元素ID
        private decimal _ObjectId = 0;
        //元素上传图片日期列表
        private List<string> _DateList = null;
        private string _CreateDate = "";
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

                button4.Enabled = allowedit;
                button2.Enabled = allowedit;
                btnDelete.Enabled = allowedit;
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

        public decimal ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        public decimal LayerId
        {
            get { return _LayerId; }
            set { _LayerId = value; }
        }

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

        public ShowPictureForm(decimal mapid)
        {
            InitializeComponent();
            _MapId = mapid;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initial(ILayer editlayer)
        {
            DateList.Items.Clear();
            tabControl1.TabPages.Clear();
            EditLayer = editlayer;
            if (SelectObjects.Count > 1&&splitContainer1.SplitterDistance==0)
            {
                pictureBox1_Click(null, null);
            }
            AddTreeNode();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        public void Search()
        {
            _LayerId = GetCurrentLayer().ID;
            _ObjectId = GetSelectObject().ID;
            //取得元素上传图片日期列表
            _DateList = GetDateList();
            SetDataList(_DateList);
            GetImage(DateList.Text);
        }

        /// <summary>
        /// 取得元素上传图片日期列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetDateList()
        {
            List<string> list = new List<string>();
            DataTable table = MapDBClass.GetPictureDate(_MapId, _LayerId, _ObjectId);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i][0].ToString());
            }
            return list;
        }

        /// <summary>
        /// 设置日期列表到下拉列表中
        /// </summary>
        /// <param name="list"></param>
        private void SetDataList(List<string> list)
        {
            DateList.Items.Clear();
            foreach (string date in list)
            {
                DateList.Items.Add(date);
            }
            //设置默认日期为最近一天
            if (list.Count > 0)
            {
                DateList.Text = list[list.Count - 1];
            }
        }

        /// <summary>
        /// 根据选择的日期索引，从数据库中取得图片
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private void GetImage(string date)
        {
            List<string> comment = new List<string>();
            List<Image> imgs = MapDBClass.GetPicure(_MapId, _LayerId, _ObjectId,date,comment);
            tabControl1.TabPages.Clear();
            int index = 0;
            foreach (Image img in imgs)
            {
                index++;
                TextBox box = new TextBox();
                box.Text = comment[index-1];
                box.Multiline = true;
                box.WordWrap = false;
                box.ScrollBars = ScrollBars.Both;
                box.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                PictureBox pic = new PictureBox();
                pic.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                pic.Image = img;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                TabPage page = new TabPage(index.ToString());
                tabControl1.TabPages.Add(page);
                page.Controls.Add(box);
                page.Controls.Add(pic);
                box.Left = 0;
                box.Top = 0;
                box.Width = page.Width;
                box.Height = 50;
                pic.Left = 0;
                pic.Top = 55;
                pic.Width = page.Width;
                pic.Height = page.Height - pic.Top;
                pic.Refresh();
            }
            btnDelete.Enabled = tabControl1.TabPages.Count > 0&&EditLayer==GetCurrentLayer();
            button4.Enabled = GetCurrentLayer() == _EditLayer;
            button2.Enabled = button4.Enabled;
        }

        /// <summary>
        /// 日历选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
            monthCalendar1.Focus();
        }

        /// <summary>
        /// 日历失去焦点，日历隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_Leave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        /// <summary>
        /// 日历选择后隐藏设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime date=monthCalendar1.SelectionStart;
            monthCalendar1.Visible = false;
            DateList.Items.Add(Common.ConvertDateToString(date));
            DateList.SelectedIndex = DateList.Items.Count - 1;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 打开选择图片窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (string filename in openFileDialog1.FileNames)
            {
                if (!File.Exists(openFileDialog1.FileName))
                {
                    MessageBox.Show("选择的图片文件不存在，请重新选择。");
                    return;
                }
            }
            int index = tabControl1.TabPages.Count;
            int oldindex = index;
            foreach (string filename in openFileDialog1.FileNames)
            {
                index++;
                TabPage page = new TabPage(index.ToString());
                page.Tag = filename;
                tabControl1.TabPages.Add(page);
                PictureBox pic = new PictureBox();
                pic.Image = new Bitmap(filename);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                TextBox box = new TextBox();
                box.Multiline = true;
                box.WordWrap = false;
                box.ScrollBars = ScrollBars.Both;
                page.Controls.Add(box);
                page.Controls.Add(pic);
                box.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                pic.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                box.Left = 0;
                box.Top = 0;
                box.Width = page.Width;
                box.Height = 50;
                pic.Left = 0;
                pic.Top = 55;
                pic.Width = page.Width;
                pic.Height = page.Height - pic.Top;
                pic.Dock = DockStyle.Fill;
                pic.Refresh();
            }
            tabControl1.SelectedIndex = oldindex;
            btnDelete.Enabled = tabControl1.TabPages.Count > 0;
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (DateList.Text == "")
            {
                MessageBox.Show("请设置日期。");
                DateList.Focus();
                return;
            }
            for (int index = 0; index < tabControl1.TabPages.Count; index++)
            {
                if (tabControl1.TabPages[index].Tag != null)
                {
                    MemoryStream stream = new MemoryStream();
                    PictureBox pic = tabControl1.TabPages[index].Controls[1] as PictureBox;
                    TextBox box = tabControl1.TabPages[index].Controls[0] as TextBox;
                    pic.Image.Save(stream, ImageFormat.Jpeg);
                    stream.Flush();
                    MapDBClass.SavePicture(_MapId, _LayerId, _ObjectId, DateList.Text, index + 1, stream.GetBuffer(), box.Text);
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                    tabControl1.TabPages[index].Tag = null;
                }
                else
                {
                    TextBox box = tabControl1.TabPages[index].Controls[0] as TextBox;
                    MapDBClass.UpdatePictureComment(_MapId, _LayerId, _ObjectId, DateList.Text, index + 1, box.Text);
                }
            }
        }

        /// <summary>
        /// 日历选择后，重新查询图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateList_SelectedValueChanged(object sender, EventArgs e)
        {
            GetImage(DateList.Text);
        }

        private void ShowPictureForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button3_Click(null, null);
            }
        }

        /// <summary>
        /// 图片删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex+1;
            MapDBClass.DeletePicture(_MapId, _LayerId, _ObjectId, DateList.Text, index);
            tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
            index=1;
            foreach (TabPage page in tabControl1.TabPages)
            {
                page.Text = index.ToString();
                index++;
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
                if (!(layer is VectorLayer))
                {
                    continue;
                }
                VectorLayer vlayer = layer as VectorLayer;
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
                            txt = "未定义" + index;
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
                DateList.Items.Clear();
                tabControl1.TabPages.Clear();
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
                if (treeView1.Nodes.Count <= 0 || treeView1.Nodes[0].Nodes.Count <= 0)
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
            ILayer layer = GetCurrentLayer();
            string sql = "select layertype from t_layer where mapid=" + _MapId + " and layerid=" + layer.ID;
            DataTable table = SqlHelper.Select(sql, null);
            if (table != null && table.Rows.Count > 0)
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
                splitContainer1.SplitterDistance = dis;
                pictureBox1.Image = EasyMap.Properties.Resources.up;
                pictureBox1.ToolTipText = "收起";
            }
            else
            {
                pictureBox1.Tag = splitContainer1.SplitterDistance;
                splitContainer1.SplitterDistance = 0;
                pictureBox1.Image = EasyMap.Properties.Resources.down;
                pictureBox1.ToolTipText = "展开";
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            btnOpenPicture.Enabled = button4.Enabled;
            btnSave.Enabled = button2.Enabled;
            btnDelete1.Enabled = btnDelete.Enabled;
        }

    }
}
