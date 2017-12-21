using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Forms;
using EasyMap.Layers;
using EasyMap.Data.Providers;
using EasyMap.Geometries;
using System.Collections.ObjectModel;

namespace EasyMap
{
    public partial class AreaTipForm : MyForm
    {
        //输入或者选中的宗地名称
        private string _AreaName;
        //选择的多边形
        private Geometry _SelectGeometry;
        //选中的图层
        private VectorLayer _SelectLayer;

        public VectorLayer SelectLayer
        {
            get { return _SelectLayer; }
            set { _SelectLayer = value; }
        }

        public Geometry SelectGeometry
        {
            get { return _SelectGeometry; }
            set { _SelectGeometry = value; }
        }

        //地图对象，以便遍历地图中符合输入名称的多边形
        private Map _Map;

        public Map Map
        {
            get { return _Map; }
            set { _Map = value; }
        }

        public string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }
        
        public AreaTipForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 遍历地图中符合输入宗地名称的多边形，添加到列表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AreaTipForm_Load(object sender, EventArgs e)
        {
            string txt=AreaName.ToLower();
            DataTable table = new DataTable();
            table.Columns.Add("areaname");
            table.Columns.Add("layer");
            List<Geometry> geos = new List<Geometry>();
            List<VectorLayer> layers = new List<VectorLayer>();
            for (int i = 0; i < Map.Layers.Count; i++)
            {
                if (Map.Layers[i] is VectorLayer)
                {
                    VectorLayer layer = Map.Layers[i] as VectorLayer;
                    GeometryProvider provider = (GeometryProvider)layer.DataSource;
                    //循环图层中的多边形
                    foreach (Geometry geom in provider.Geometries)
                    {
                        if (geom.Text.ToLower().Contains(txt))
                        {
                            table.Rows.Add(new object[] { geom.Text, layer.LayerName });
                            geos.Add(geom);
                            layers.Add(layer);
                        }
                    }
                }
            }
            myDataGridView1.DataSource = table;
            for (int i = 0; i < geos.Count; i++)
            {
                myDataGridView1.Rows[i].Cells[0].Tag = geos[i];
                myDataGridView1.Rows[i].Cells[1].Tag = layers[i];
            }
            geos.Clear();
            layers.Clear();
            //如果查询的数据多余1条，则按名称排序后，默认选中第一条
            if (myDataGridView1.RowCount >0)
            {
                label1.Text = "记录数：" + myDataGridView1.RowCount;
                //myDataGridView1.Sort(myDataGridView1.Columns[0], ListSortDirection.Ascending);
                myDataGridView1.Rows[0].Selected = true;
            }
            //如果查询的数据只有1条或者没有查询到，则触发确定按钮
            if (myDataGridView1.RowCount <= 1)
            {
                if (myDataGridView1.RowCount==1&&myDataGridView1.GetGridValue(0, 0).ToString() == AreaName
                    || myDataGridView1.RowCount<=0)
                {
                    btnOk_Click(null, null);
                }
            }
        }

        /// <summary>
        /// 确定处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            //如果没有选择宗地，则返回空串，否则，返回选中的宗地名称以及选中的元素和图层
            if (myDataGridView1.SelectedRows == null || myDataGridView1.SelectedRows.Count == 0)
            {
                AreaName = "";
            }
            else
            {
                AreaName = myDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                SelectGeometry = myDataGridView1.SelectedRows[0].Cells[0].Tag as Geometry;
                SelectLayer = myDataGridView1.SelectedRows[0].Cells[1].Tag as VectorLayer;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 取消处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
