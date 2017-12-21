using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using EasyMap.Geometries;
using System.Drawing;
using EasyMap.Forms;
using EasyMap.Data.Providers;
using EasyMap.Layers;
using EsayMap;
using EasyMap.Styles;
using EasyMap.Properties;
namespace EasyMap.UI.Forms
{
    public partial class MapEditImage : PictureBox
    {

        private LineString mline = new LineString();
        private Polygon marea = new Polygon();
        private float _R = 0;
        private Image oldimage = null;
        private double oldlength = 0;
        private MapImage _MainMapImage = null;
        private bool _DrawEnd = false;
        public delegate void SetLengthEvent(double totalLength, double currentLength);
        public delegate void SetAreaEvent(double area);
        public delegate void AfterDefineAreaEvent(SELECTION_TYPE type, Polygon area, double r);
        public delegate void AfterDefineAreaEvent1(SELECTION_TYPE type,EasyMap.Geometries.Point area, double r);

        public event SetLengthEvent SetLength;
        public event SetAreaEvent SetArea;
        public event AfterDefineAreaEvent AfterDefineArea;
        public event AfterDefineAreaEvent1 AfterDefineArea1;
        private SELECTION_TYPE _SelectionType;

        public SELECTION_TYPE SelectionType
        {
            get { return _SelectionType; }
            set { _SelectionType = value; }
        }

        public MapImage MainMapImage
        {
            get { return _MainMapImage; }
            set { _MainMapImage = value; }
        }

        public MapEditImage()
        {
            InitializeComponent();
            Initial();
        }

        public MapEditImage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Initial()
        {
            Initial(SELECTION_TYPE.NONE);
        }
        public void Initial(SELECTION_TYPE selecttype)
        {
            SelectionType = selecttype;
            //测量点清空
            mline.Vertices.Clear();
            //测量面积清空
            marea.ExteriorRing.Vertices.Clear(); ;
            _R = 0;
            Refresh();
        }

        public override void Refresh()
        {
            if (MainMapImage == null)
            {
                return;
            }
            oldimage = MainMapImage.Image;
            Image = (Bitmap)oldimage.Clone();

            if (oldimage != null)
            {
                //恢复地图图像
                Image = (Bitmap)oldimage.Clone();
                //绘制直线
                Graphics g = Graphics.FromImage(Image);
                if (SelectionType == SELECTION_TYPE.RECTANGLE && marea.ExteriorRing.Vertices.Count>=2)
                {
                    Pen pen = new Pen(Color.Blue, 2);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                    System.Drawing.PointF point1 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[0]);
                    System.Drawing.PointF point2 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[1]);
                    float x = Math.Min(point1.X, point2.X);
                    float y = Math.Min(point1.Y, point2.Y);
                    float w = Math.Abs(point1.X - point2.X);
                    float h = Math.Abs(point1.Y-point2.Y);
                    g.DrawRectangle(pen, x, y, w, h);
                }
                else if (SelectionType == SELECTION_TYPE.CIRCLE && marea.ExteriorRing.Vertices.Count>=2)
                {
                    Pen pen = new Pen(Color.Blue, 2);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                    System.Drawing.PointF point1 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[0]);
                    System.Drawing.PointF point2 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[1]);
                    float dx = (float)(point1.X - point2.X);
                    float dy = (float)(point1.Y - point2.Y);
                    float r = (float)Math.Sqrt(dx * dx + dy * dy);
                    float x = (float)(point1.X) - r;
                    float y = (float)(point1.Y) - r;
                    float w = 2 * r;
                    float h = 2 * r;
                    if (w > 0)
                    {
                        g.DrawArc(pen, x, y, w, h, 0, 360);
                    }
                    dx = (float)(marea.ExteriorRing.Vertices[0].X - marea.ExteriorRing.Vertices[1].X);
                    dy = (float)(marea.ExteriorRing.Vertices[0].Y - marea.ExteriorRing.Vertices[1].Y);
                    _R = (float)Math.Sqrt(dx * dx + dy * dy);
                }
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureLength)
                {
                    Pen pen = new Pen(Color.Blue, 2);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                    for (int i = 1; i < mline.Vertices.Count; i++)
                    {
                        System.Drawing.PointF point1 = MainMapImage.Map.WorldToImage(mline.Vertices[i - 1]);
                        System.Drawing.PointF point2 = MainMapImage.Map.WorldToImage(mline.Vertices[i]);
                        g.DrawLine(pen, point1, point2);
                    }
                }
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea
                    || MainMapImage.ActiveTool==MapImage.Tools.DefineArea
                    || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint
                    || MainMapImage.ActiveTool==MapImage.Tools.ZoomArea
                    || SelectionType == SELECTION_TYPE.POLYGON)
                {
                    Pen pen = new Pen(Color.Yellow,2);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                    int index = marea.ExteriorRing.Vertices.Count;
                    if (MainMapImage.ActiveTool == MapImage.Tools.ZoomArea && index==2)
                    {
                        System.Drawing.PointF point1 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[0]);
                        System.Drawing.PointF point2 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[1]);
                        int width=(int)Math.Abs(point1.X-point2.X);
                        int height=(int)Math.Abs(point1.Y-point2.Y);
                        g.DrawRectangle(pen, point1.X, point1.Y, width, height);
                    }
                    else
                    {
                        for (int i = 1; i < marea.ExteriorRing.Vertices.Count; i++)
                        {
                            System.Drawing.PointF point1 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[i - 1]);
                            System.Drawing.PointF point2 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[i]);
                            g.DrawLine(pen, point1, point2);
                        }
                        if (index > 2)
                        {
                            System.Drawing.PointF point1 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[0]);
                            System.Drawing.PointF point2 = MainMapImage.Map.WorldToImage(marea.ExteriorRing.Vertices[index - 1]);
                            g.DrawLine(pen, point1, point2);
                        }
                    }
                }
                g.Dispose();
            }
            base.Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                if (!_DrawEnd)
                {
                    if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea
                        ||MainMapImage.ActiveTool==MapImage.Tools.DefineArea
                        || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint
                        || MainMapImage.ActiveTool == MapImage.Tools.ZoomArea)
                    {
                        if (MainMapImage.ActiveTool == MapImage.Tools.ZoomArea)
                        {
                            this.Visible = false;
                            if (AfterDefineArea != null)
                            {
                                AfterDefineArea(SelectionType,null, _R);
                            }
                            return;
                        }
                        if (marea.ExteriorRing.Vertices.Count > 0)
                        {
                            marea.ExteriorRing.Vertices.RemoveAt(marea.ExteriorRing.Vertices.Count - 1);
                        }
                        else
                        {

                            this.Visible = false;
                            if (AfterDefineArea != null)
                            {
                                AfterDefineArea(SelectionType, null, _R);
                            }
                            return;
                        }
                        if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea)
                        {
                            if (SetArea != null)
                            {
                                SetArea(marea.Area);
                            }
                        }
                        else if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea
                            || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint)
                        {
                            Refresh();

                            Polygon newArea = null;
                            string text = null;
                            if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea)
                            {
                                InputStringForm form = new InputStringForm("请输入区域名称：", "");

                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    //text = form.InputContext;
                                    marea.Text = form.InputContext;
                                }
                                else
                                {
                                    this.Visible = false;
                                    if (AfterDefineArea != null)
                                    {
                                        AfterDefineArea(SelectionType, null, _R);
                                    }
                                    return;
                                }
                                //取得当前图层
                                VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
                                //取得图层数据源
                                GeometryProvider provider = (GeometryProvider)layer.DataSource;
                                //添加当前定义的多边形
                                newArea = marea.Clone();
                                newArea.Text = marea.Text;
                                newArea.ID = marea.ID;
                                provider.Geometries.Add(newArea);
                                //强制地图刷新
                                MainMapImage.Refresh();
                            }
                            if (AfterDefineArea != null)
                            {
                                AfterDefineArea(SelectionType, marea, _R);
                            }
                            if (newArea != null)
                            {
                                newArea.ID = marea.ID;
                            }
                            //重新初始化多边形
                            marea = new Polygon(); 
                            this.Visible = false;
                        }
                    }

                    else if(MainMapImage.ActiveTool == MapImage.Tools.MeasureLength)
                    {
                        mline.Vertices.RemoveAt(mline.Vertices.Count - 1);
                        //设置测量窗口的信息
                        if (SetLength != null)
                        {
                            SetLength(mline.Length, mline.Length - oldlength);
                        }
                    }
                    Refresh();
                    _DrawEnd = true;
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (_DrawEnd)
                {
                    Initial(SelectionType);
                }
                _DrawEnd = false;
                //将图像坐标转化为地图坐标
                EasyMap.Geometries.Point WorldPos = MainMapImage.Map.ImageToWorld(new System.Drawing.Point(e.X, e.Y));
                //保存上一次的长度，以便计算出最后一段直线的长度
                oldlength = mline.Length;
                if (SelectionType == SELECTION_TYPE.CIRCLETEMP)
                {
                    //添加救援点
                    //double radius = 10;
                    //double deltaDegrees = 2;
                    //double deltaRadians = (Math.PI / 180) * deltaDegrees;
                    EasyMap.Geometries.Point marea = new Geometries.Point();
                    marea = new EasyMap.Geometries.Point(WorldPos.X, WorldPos.Y);
                        EasyMap.Geometries.Point newArea = new Geometries.Point();
                    //string text = null;
                    if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea)
                    {
                        InputStringForm form = new InputStringForm("请输入救援力量名称：", "");

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            //text = form.InputContext;
                            marea.Text = form.InputContext;
                        }
                        else
                        {
                            this.Visible = false;
                            if (AfterDefineArea1 != null)
                            {
                                AfterDefineArea1(SelectionType, null, _R);
                            }
                            return;
                        }
                        //取得当前图层
                        VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
                        //取得图层数据源
                        GeometryProvider provider = (GeometryProvider)layer.DataSource;
                        //添加当前定义的多边形
                        marea.ID = MapDBClass.GetObjectId(MainMapImage.Map.MapId, layer.ID);
                        newArea = marea.Clone();
                        newArea.Text = marea.Text;
                        newArea.ID = marea.ID;
                        provider.Geometries.Add(newArea);

                        //强制地图刷新
                        MainMapImage.Refresh();
                    }

                    if (AfterDefineArea1 != null)
                    {
                        AfterDefineArea1(SelectionType, marea, _R);
                    }
                    //强制地图刷新
                    MainMapImage.Refresh(); newArea.ID = marea.ID;
                    //重新初始化多边形
                    //marea = new Polygon();
                    marea = new Geometries.Point(); 
                    this.Visible = false;
                    ////强制地图刷新
                    MainMapImage.Refresh();
                }
                else if (SelectionType == SELECTION_TYPE.PROBLEMPOINT)
                {
                    EasyMap.Geometries.Point marea = new Geometries.Point();
                    marea = new EasyMap.Geometries.Point(WorldPos.X, WorldPos.Y);
                    EasyMap.Geometries.Point newArea = new Geometries.Point();
                    if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea)
                    {
                        InputStringForm form = new InputStringForm("请输入遇难点名称：", "");

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            //text = form.InputContext;
                            marea.Text = form.InputContext;
                        }
                        else
                        {
                            this.Visible = false;
                            if (AfterDefineArea1 != null)
                            {
                                AfterDefineArea1(SelectionType, null, _R);
                            }
                            return;
                        }
                        //取得当前图层
                        VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
                        //取得图层数据源
                        GeometryProvider provider = (GeometryProvider)layer.DataSource;
                        marea.ID = MapDBClass.GetObjectId(MainMapImage.Map.MapId, layer.ID);
                        //添加当前定义的多边形
                        newArea = marea.Clone();
                        newArea.Text = marea.Text;
                        newArea.ID = marea.ID;
                        provider.Geometries.Add(newArea);

                        //强制地图刷新
                        MainMapImage.Refresh();
                    }

                    if (AfterDefineArea1 != null)
                    {
                        AfterDefineArea1(SelectionType, marea, _R);
                    }
                    //强制地图刷新
                    MainMapImage.Refresh(); 
                    newArea.ID = marea.ID;
                    marea = new Geometries.Point();
                    this.Visible = false;
                    ////强制地图刷新
                    MainMapImage.Refresh();
                }
                else if (SelectionType == SELECTION_TYPE.RECTANGLE
                    || SelectionType == SELECTION_TYPE.CIRCLE)
                {
                    marea.ExteriorRing.Vertices.Add(WorldPos);
                    marea.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(WorldPos.X, WorldPos.Y));
                    if (marea.ExteriorRing.Vertices.Count > 2)
                    {
                        _DrawEnd = true;
                        this.Visible = false;
                        if (SelectionType == SELECTION_TYPE.RECTANGLE)
                        {
                            double x1 = marea.ExteriorRing.Vertices[0].X;
                            double y1 = marea.ExteriorRing.Vertices[0].Y;
                            double x3 = marea.ExteriorRing.Vertices[1].X;
                            double y3 = marea.ExteriorRing.Vertices[1].Y;
                            double x2 = x1;
                            double y2 = y3;
                            double x4 = x3;
                            double y4 = y1;
                            marea.ExteriorRing.Vertices.Clear();
                            marea.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(x1, y1));
                            marea.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(x2, y2));
                            marea.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(x3, y3));
                            marea.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(x4, y4));
                        }
                        if (AfterDefineArea != null)
                        {
                            AfterDefineArea(SelectionType, marea, _R);
                        }
                        //Polygon newArea = null;
                        //if (SelectionType == SELECTION_TYPE.CIRCLE)
                        //{
                        //    InputStringForm form = new InputStringForm("请输入区域名称：", "");

                        //    if (form.ShowDialog() == DialogResult.OK)
                        //    {
                        //        //text = form.InputContext;
                        //        marea.Text = form.InputContext;
                        //    }
                        //    else
                        //    {
                        //        this.Visible = false;
                        //        if (AfterDefineArea != null)
                        //        {
                        //            AfterDefineArea(SelectionType, null, _R);
                        //        }
                        //        return;
                        //    }
                        //    //取得当前图层
                        //    VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
                        //    //取得图层数据源
                        //    GeometryProvider provider = (GeometryProvider)layer.DataSource;
                        //    //添加当前定义的多边形
                        //    newArea = marea.Clone();
                        //    newArea.Text = "";
                        //    newArea.ID = marea.ID;
                        //    provider.Geometries.Add(newArea);
                        //    //强制地图刷新
                        //    MainMapImage.Refresh();
                        //}
                        return;
                    }
                }
                else if (SelectionType == SELECTION_TYPE.CIRCLE_RADIO)
                {
                    marea.ExteriorRing.Vertices.Add(WorldPos);
                    XYInputForm form = new XYInputForm();
                    form.txtX.Enabled = false;
                    form.txtY.Enabled = false;
                    form.XX = marea.ExteriorRing.Vertices[0].X;
                    form.YY = marea.ExteriorRing.Vertices[0].Y;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        marea.ExteriorRing.Vertices.Add(WorldPos);
                        marea.ExteriorRing.Vertices.Add(WorldPos);
                        _R = (float)form.R;
                        if (AfterDefineArea != null)
                        {
                            AfterDefineArea(SelectionType, marea, _R);
                        }
                    }
                    _DrawEnd = true;
                    this.Visible = false;
                    return;
                }
                //如果是测量长度
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureLength)
                {
                    //添加地图坐标
                    mline.Vertices.Add(WorldPos);
                    //如果是第一次添加，那么需要追加一个点，以便在鼠标移动的时候更改这个点
                    if (mline.Vertices.Count < 2)
                    {
                        mline.Vertices.Add(new EasyMap.Geometries.Point(WorldPos.X, WorldPos.Y));
                    }
                }
                //如果是测量面积
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea
                    || MainMapImage.ActiveTool == MapImage.Tools.DefineArea
                    || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint
                    || MainMapImage.ActiveTool == MapImage.Tools.ZoomArea
                    || SelectionType == SELECTION_TYPE.POLYGON)
                {
                    if (MainMapImage.ActiveTool == MapImage.Tools.ZoomArea
                        && marea.ExteriorRing.Vertices.Count == 2)
                    {
                        this.Visible = false;
                        if (AfterDefineArea != null)
                        {
                            AfterDefineArea(SelectionType, marea, _R);
                        }
                        return;
                    }
                    //添加地图坐标
                    marea.ExteriorRing.Vertices.Add(WorldPos);
                    //如果是第一次添加，那么需要追加一个点，以便在鼠标移动的时候更改这个点
                    if (marea.ExteriorRing.Vertices.Count < 2)
                    {
                        marea.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(WorldPos.X, WorldPos.Y));
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_DrawEnd)
            {
                return;
            }
            //转化为地图坐标
            EasyMap.Geometries.Point WorldPos = MainMapImage.Map.ImageToWorld(new System.Drawing.Point(e.X, e.Y));
            //显示当前地图坐标
            if (oldimage != null)
            {
                if (SelectionType == SELECTION_TYPE.RECTANGLE
                    || SelectionType == SELECTION_TYPE.CIRCLE)
                {
                    //修改最后一个点坐标
                    int count = marea.ExteriorRing.Vertices.Count;
                    if (count > 1)
                    {
                        marea.ExteriorRing.Vertices[count - 1] = WorldPos;
                    }
                    Refresh();
                }
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureLength)
                {
                    //修改最后一个点坐标
                    int count = mline.Vertices.Count;
                    if (count > 1)
                    {
                        mline.Vertices[count - 1] = WorldPos;
                        //设置测量窗口的信息
                        if (SetLength != null)
                        {
                            SetLength(mline.Length, mline.Length - oldlength);
                        }
                    }
                    Refresh();
                }
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea
                    || MainMapImage.ActiveTool == MapImage.Tools.DefineArea
                    || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint
                    || MainMapImage.ActiveTool == MapImage.Tools.ZoomArea
                    || SelectionType == SELECTION_TYPE.POLYGON)
                {
                    int index = marea.ExteriorRing.Vertices.Count;
                    if (index > 1)
                    {
                        marea.ExteriorRing.Vertices[index - 1] = WorldPos;
                    }
                    if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea)
                    {
                        //设置测量窗口的信息
                        if (SetArea != null)
                        {
                            SetArea(marea.Area);
                        }
                    }
                    Refresh();
                }
            }
        }

    }
}
