
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using EasyMap.Data;
using EasyMap.Geometries;
using EasyMap.Layers;
using Point = EasyMap.Geometries.Point;
using EasyMapServer;
using System.Collections.Generic;
using System.Collections;
using PhotoSettings;
using EasyMap.Properties;
using System.Drawing.Imaging;
using System.Threading;
using EasyMap.Controls;
using EasyMap.Data.Providers;
using EasyMap.Styles;

namespace EasyMap.Forms
{
    /// <summary>
    /// 地图显示控件
    /// </summary>
    [DesignTimeVisible(true)]
    public class MapImage : PictureBox
    {
        #region Tools enum

        /// <summary>
        /// 地图动作定义
        /// </summary>
        public enum Tools
        {
            Pan,
            ZoomIn,
            ZoomOut,
            ZoomArea,
            Query,
            Select,
            PanOrQuery,
            MeasureLength,
            MeasureArea,
            DefineArea,
            DefinePricePoint,
            SelectPoint,
            None
        }
        //图像刷新后事件
        public delegate void AfterRefreshEvent(object sender);
        public event AfterRefreshEvent AfterRefresh;
        public delegate void BeforeRefreshEvent(object sender);
        public event BeforeRefreshEvent BeforeRefresh;

        #endregion

        private Tools _activetool;
        private double _fineZoomFactor = 10;
        private bool _isCtrlPressed;
        private Map _map;
        private int _queryLayerIndex;

        private double _wheelZoomMagnitude = 2;
        private System.Drawing.Point _mousedrag;
        private bool _mousedragging;
        private Image _mousedragImg;
        private bool _panOnClick;
        private bool _selectOnClick;
        private bool _zoomOnDblClick;
        private Image _dragImg1, _dragImg2, _dragImgSupp;

        private Bitmap _staticMap;
        private Bitmap _variableMap;

        private bool _panOrQueryIsPan;
        private bool _zoomToPointer = false;
        private bool _MyRefresh = false;
        //保存选择的多边形
        private MyGeometryList _SelectObjects = new MyGeometryList();
        private List<ILayer> _SelectLayers = new List<ILayer>();

        //保存地图是否打开
        private bool _IsOpened = false;
        //保存地图上鼠标移动的最后一个点坐标
        private EasyMap.Geometries.Point _LastWorldPos = null;
        private Tools _LastOption = Tools.Pan;
        private bool _NeedSave = true;
        private bool _RequestFromServer = false;
        private bool _HaveTif = false;
        private bool _PickCoordinate = false;
        private IContainer components;
        private List<AreaData> _AreaList;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private float _ZoomInSeed = 0.1F;
        private float _ZoomOutSeed = 0.01F;

        private ToolTip toolTip1;
        private List<PhotoData> _FindAreaList;
        public static double bottomLeftX = 0;
        public static double bottomLeftY =0;
        public static double topLeftY =0;
        public static double topRigthX = 0;
        public static double minX = 0;
        public static double minY = 0;
        public static double maxX = 0;
        public static double maxY = 0;
        public static double aa = 0;
        
        [Bindable(false), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<AreaData> AreaList
        {
            get 
            {
                if (_AreaList == null)
                {
                    _AreaList = new List<AreaData>();
                }
                return _AreaList; 
            }
            set 
            {
                //if (value == null)
                //{
                //    _AreaList = new List<AreaData>();
                //}
                //else
                //{
                    _AreaList = value;
                //}
            }
        }
        [Bindable(false), Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PhotoData> FindAreaList
        {
            get 
            {
                if (_FindAreaList == null)
                {
                    _FindAreaList = new List<PhotoData>();
                }
                return _FindAreaList; 
            }
            set 
            {
                //if (value == null)
                //{
                //    _FindAreaList = new List<PhotoData>();
                //}
                //else
                //{
                    _FindAreaList = value;
                //}
            }
        }
        private string _ClickAreaName = "";

        public bool PickCoordinate
        {
            get { return _PickCoordinate; }
            set { _PickCoordinate = value; }
        }

        public bool HaveTif
        {
            get { return _HaveTif; }
            set { _HaveTif = value; }
        }

        public bool ShowShiQu { get; set; }

        public bool RequestFromServer
        {
            get { return _RequestFromServer; }
            set { _RequestFromServer = value; }
        }

        public bool NeedSave
        {
            get { return _NeedSave; }
            set { _NeedSave = value; }
        }

        public MyGeometryList SelectObjects
        {
            get { return _SelectObjects; }
            set { _SelectObjects = value; }
        }

        public List<ILayer> SelectLayers
        {
            get
            {
                List<ILayer> layers = new List<ILayer>();
                foreach (ILayer layer in Map.Layers)
                {
                    if (layer is VectorLayer && layer.Enabled)
                    {
                        GeometryProvider datasource = ((VectorLayer)layer).DataSource as GeometryProvider;
                        foreach (Geometry geom in SelectObjects)
                        {
                            if (datasource.Geometries.Contains(geom))
                            {
                                layers.Add(layer);
                                break;
                            }
                        }
                    }
                }

                return layers;
            }
        }

        //internal MyGeometryList SelectPolygons
        //{
        //    get { return _SelectPolygons; }
        //    set { _SelectPolygons = value; }
        //}
        

        //internal MyGeometryList SelectLine
        //{
        //    get { return _SelectLine; }
        //    set { _SelectLine = value; }
        //}
        

        //internal MyGeometryList SelectMultiLine
        //{
        //    get { return _SelectMultiLine; }
        //    set { _SelectMultiLine = value; }
        //}
        

        //internal MyGeometryList SelectPoint
        //{
        //    get { return _SelectPoint; }
        //    set { _SelectPoint = value; }
        //}
        

        public void ClearSelectAll()
        {
            SelectObjects.Clear();
            SelectLayers.Clear();
            //_SelectPolygon.Clear();
            //_SelectPolygons.Clear();
            //_SelectLine.Clear();
            //_SelectMultiLine.Clear();
            //_SelectPoint.Clear();
        }

        public bool IsOpened
        {
            get { return _IsOpened; }
            set { _IsOpened = value; }
        }

        public EasyMap.Geometries.Point LastWorldPos
        {
            get { return _LastWorldPos; }
            set { _LastWorldPos = value; }
        }

        public Tools LastOption
        {
            get { return _LastOption; }
            set { _LastOption = value; }
        }

        public bool MyRefresh
        {
            get { return _MyRefresh; }
            set { _MyRefresh = value; }
        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        public MapImage()
        {
            timer.Enabled = false;
            timer.Interval = 800;
            timer.Tick += new EventHandler(timer_Tick);
            toolTip1 = new ToolTip();
            toolTip1.IsBalloon = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            _map = new Map(Size);
            _activetool = Tools.None;
            base.MouseWheel += new System.Windows.Forms.MouseEventHandler(MapImage_Wheel);
            base.MouseMove += new System.Windows.Forms.MouseEventHandler(MapImage_MouseMove);
            base.MouseUp += new System.Windows.Forms.MouseEventHandler(MapImage_MouseUp);
            base.MouseDown += new System.Windows.Forms.MouseEventHandler(MapImage_MouseDown);
            base.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MapImage_DblClick);
            base.MouseClick += new System.Windows.Forms.MouseEventHandler(MapImage_MouseClick);
            VariableLayerCollection.VariableLayerCollectionRequery += this.VariableLayersRequery;
            Cursor = Cursors.Cross;
            DoubleBuffered = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            Point point = timer.Tag as Point;
            float scale=1;
            if (point.X>_mousedrag.X)
            {
                scale = (float)Math.Pow(1 / (float)(point.X - _mousedrag.X), 0.5);
            }
            else if(point.X<_mousedrag.X)
            {
                scale = 1 + (float)(_mousedrag.X - point.X) * 0.1f;
            }
            if (_map != null)
            {
                double scaleBase = 1 + (_wheelZoomMagnitude / (10 * (_isCtrlPressed ? _fineZoomFactor : 1)));

                _map.Zoom /= scale;

                if (MapZoomChanged != null)
                    MapZoomChanged(sender, _map.Zoom);

                RequestFromServer = true;
                Refresh();
                this.Focus();
            }
        }

        public void Reset()
        {
            _fineZoomFactor = 10;
            _wheelZoomMagnitude = 2;
            _zoomToPointer = false;
            _MyRefresh = false;
            ClearSelectAll();
            _IsOpened = false;
            _LastWorldPos = null;
            _LastOption = Tools.Pan;
            _NeedSave = true;
            _RequestFromServer = false;
            _HaveTif = false;
            _PickCoordinate = false;
            _ClickAreaName = "";
            FindAreaList.Clear();
            AreaList.Clear();
        }


        protected override void Dispose(bool disposing)
        {
            VariableLayerCollection.VariableLayerCollectionRequery -= this.VariableLayersRequery;
            base.Dispose(disposing);
        }
        [Description("The amount which a single movement of the mouse wheel zooms by.")]
        [DefaultValue(-2)]
        [Category("Behavior")]
        public double WheelZoomMagnitude
        {
            get { return _wheelZoomMagnitude; }
            set { _wheelZoomMagnitude = value; }
        }

        [Description("The amount which the WheelZoomMagnitude is divided by " +
                     "when the Control key is pressed. A number greater than 1 decreases " +
                     "the zoom, and less than 1 increases it. A negative number reverses it.")]
        [DefaultValue(10)]
        [Category("Behavior")]
        public double FineZoomFactor
        {
            get { return _fineZoomFactor; }
            set { _fineZoomFactor = value; }
        }

        [Description("Sets whether the mouse wheel should zoom to the pointer location")]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool ZoomToPointer
        {
            get { return _zoomToPointer; }
            set { _zoomToPointer = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
                if (_map != null)
                {
                    VariableLayerCollection.VariableLayerCollectionRequery += new VariableLayerCollectionRequeryHandler(VariableLayersRequery);
                    Refresh();
                }
            }
        }

        private void VariableLayersRequery(object sender, EventArgs e)
        {
            lock (_map)
            {
                if (_mousedragging) return;
                _variableMap = GetMap(_map.VariableLayers, LayerCollectionType.Variable);
            }
            UpdateImage();
        }

        public int QueryLayerIndex
        {
            get { return _queryLayerIndex; }
            set { _queryLayerIndex = value; }
        }


        public Tools ActiveTool
        {
            get { return _activetool; }
            set
            {
                bool fireevent = (value != _activetool);
                _activetool = value;
                if (value == Tools.ZoomIn)
                {
                }
                else if (value == Tools.ZoomOut)
                {
                }
                else if (value == Tools.Pan)
                    Cursor = Cursors.NoMove2D;
                else if (value == Tools.Select)
                    Cursor = Cursors.Arrow;
                else
                    Cursor = Cursors.Cross;
                if (fireevent)
                    if (ActiveToolChanged != null)
                        ActiveToolChanged(value);

                // Check if settings collide
                if (value != Tools.None && ZoomOnDblClick)
                    ZoomOnDblClick = false;
                if (value != Tools.Pan && PanOnClick)
                    PanOnClick = false;
            }
        }

        public Cursor GetCursor()
        {
            if (_activetool == Tools.Pan)
                return Cursors.NoMove2D;
            else if (_activetool == Tools.Select)
                return Cursors.Arrow;
            else
                return Cursors.Cross;
        }


        [Description("Sets whether the \"go-to-cursor-on-click\" feature is enabled or not (even if enabled it works only if the active tool is Pan)")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool PanOnClick
        {
            get { return _panOnClick; }
            set
            {
                ActiveTool = Tools.Pan;
                _panOnClick = value;

            }
        }
        [Description("Sets whether the \"go-to-cursor-on-click\" feature is enabled or not (even if enabled it works only if the active tool is Select)")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool SelectOnClick
        {
            get { return _selectOnClick; }
            set
            {
                ActiveTool = Tools.Select;
                _selectOnClick = value;

            }
        }
        [Description("Sets whether the \"go-to-cursor-and-zoom-in-on-double-click\" feature is enable or not. This only works if no tool is currently active.")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool ZoomOnDblClick
        {
            get { return _zoomOnDblClick; }
            set { 
                if (value)
                    ActiveTool = Tools.None;
                _zoomOnDblClick = value;
            }
        }

        public override void Refresh()
        {
            if (!MyRefresh)
            {
                if (BeforeRefresh != null)
                {
                    BeforeRefresh(this);
                }
                if (_map != null)
                {
                    _map.Size = Size;
                    _staticMap = GetMap(_map.Layers, LayerCollectionType.Static);
                    _variableMap = GetMap(_map.VariableLayers, LayerCollectionType.Variable);

                    UpdateImage();
                    base.Refresh();
                    if (MapRefreshed != null)
                        MapRefreshed(this, null);
                }
                if (AfterRefresh != null)
                {
                    AfterRefresh(this);
                }
            }
            MyRefresh = false;
            RequestFromServer = false;
            _ClickAreaName = "";
        }

        private Bitmap GetMap(LayerCollection layers, LayerCollectionType layerCollectionType)
        {
            if ((layers == null || layers.Count == 0||Height<=0||Width<=0))
                return null;

            AreaList.Clear();
            Bitmap retval = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(retval);

            #region - 向服务端请求绘制影像图 -
            if (HaveTif)
            {
                if (RequestFromServer)
                {
                    TifData.Map = _map;
                    List<Hashtable> filelist = TifData.GetTifFileList(ShowShiQu);
                    Size size = _map.Size;
                    BoundingBox displayBbox = _map.Envelope;
                    GDAL_Data gdaldata = new GDAL_Data();
                    gdaldata.DisplayBbox = displayBbox;
                    gdaldata.DisplaySize = size;
                    gdaldata.MapMaxY = _map.Envelope.Max.Y;
                    gdaldata.MapMinX = _map.Envelope.Min.X;
                    gdaldata.PixelHeight = _map.PixelHeight;
                    gdaldata.PixelWidth = _map.PixelWidth;
                    foreach (Hashtable table in filelist)
                    {
                        PhotoData data = table["PhotoData"] as PhotoData;
                        int overviewlevel = (int)table["overviewlevel"];
                        int overviewzoom = (int)table["overviewzoom"];
                        Size _imagesize = new Size(data.Width, data.Height);
                        double right = 0, left = 0, top = 0, bottom = 0;
                        double dblW, dblH;

                        double[] geoTrans = new double[6];
                        geoTrans[0] = data.Transform1;
                        geoTrans[1] = data.Transform2;
                        geoTrans[2] = data.Transform3;
                        geoTrans[3] = data.Transform4;
                        geoTrans[4] = data.Transform5;
                        geoTrans[5] = data.Transform6;
                        GeoTransform _geoTransform = new GeoTransform(geoTrans);
                        // image pixels
                        dblW = _imagesize.Width;
                        dblH = _imagesize.Height;

                        left = _geoTransform.EnvelopeLeft(dblW, dblH);
                        right = _geoTransform.EnvelopeRight(dblW, dblH);
                        top = _geoTransform.EnvelopeTop(dblW, dblH);
                        bottom = _geoTransform.EnvelopeBottom(dblW, dblH);

                        BoundingBox _envelope = new BoundingBox(left, bottom, right, top);
                        if ((displayBbox.Left > _envelope.Right) || (displayBbox.Right < _envelope.Left)
                            || (displayBbox.Top < _envelope.Bottom) || (displayBbox.Bottom > _envelope.Top))
                            continue;
                        left = Math.Max(displayBbox.Left, _envelope.Left);
                        top = Math.Min(displayBbox.Top, _envelope.Top);
                        right = Math.Min(displayBbox.Right, _envelope.Right);
                        bottom = Math.Max(displayBbox.Bottom, _envelope.Bottom);
                        BoundingBox trueImageBbox = new BoundingBox(left, bottom, right, top);

                        // convert ground coordinates to map coordinates to figure out where to place the bitmap
                        System.Drawing.Point bitmapBR = new System.Drawing.Point((int)_map.WorldToImage(trueImageBbox.BottomRight).X + 1,
                                             (int)_map.WorldToImage(trueImageBbox.BottomRight).Y + 1);
                        System.Drawing.Point bitmapTL = new System.Drawing.Point((int)_map.WorldToImage(trueImageBbox.TopLeft).X,
                                             (int)_map.WorldToImage(trueImageBbox.TopLeft).Y);
                        gdaldata.BitmapBR.Add(bitmapBR);
                        gdaldata.BitmapTL.Add(bitmapTL);
                        gdaldata.FileList.Add(data.FileName);
                        gdaldata.OverViewLevel.Add(overviewlevel);
                        gdaldata.OverViewScale.Add(overviewzoom);
                    }
                    int i = 0;
                    if (Command._ConnectServer && Command.ConnectServer() && gdaldata.FileList.Count > 0)
                    {
                        Command.MapMade = false;
                        Command.SendDrawPhotoData(gdaldata);
                        while (!Command.MapMade)
                        {
                            i++;
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }
                        i = i + 1;
                        if (Command.MadeMap != null)
                        {
                            g.DrawImage(Command.MadeMap, new PointF(0, 0));
                        }
                    }
                }
                else if (Command._ConnectServer)
                {
                    if (Command.MadeMap != null)
                    {
                        g.DrawImage(Command.MadeMap, new System.Drawing.Point(0, 0));
                    }
                }
            }

            #endregion

            _map.RenderMap(g, layerCollectionType);
            //绘制查询的地区标识
            int index=65;
            foreach (PhotoData data in FindAreaList)
            {
                try
                {
                    double dx = (data.MinX + data.MaxX) / 2;
                    double dy = (data.MinY + data.MaxY) / 2;
                    PointF point = _map.WorldToImage(new EasyMap.Geometries.Point(dx, dy), true);

                    point.X -= Resources.bollon_normal.Width / 2;
                    point.Y -= Resources.bollon_normal.Height;
                    PointF point1 = new PointF(point.X + Resources.bollon_normal.Width / 2 + 3, point.Y + Resources.bollon_normal.Height / 2);
                    g.DrawImage(Resources.bollon_normal, point);
                    string txt = new string((char)index, 1);
                    index++;
                    StringFormat formart = new StringFormat();
                    formart.LineAlignment = StringAlignment.Center;
                    formart.Alignment = StringAlignment.Center;
                    g.DrawString(txt, new Font("", 9), Brushes.White, point1, formart);
                    AreaData areadata = new AreaData();
                    areadata.Name = data.Name;
                    areadata.PhotoData = data;
                    areadata.Area = new RectangleF(point, Resources.bollon_normal.Size);
                    AreaList.Add(areadata);
                   
                }
                catch { continue; }
            }

            g.Dispose();

            if (layerCollectionType == LayerCollectionType.Variable)
                retval.MakeTransparent(_map.BackColor);

            return retval;

        }

        /// <summary>
        /// 取得鼠标下的区域
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public AreaData GetArea(PointF point)
        {
            foreach (AreaData data in AreaList)
            {
                if (data.Area.Contains(point))
                {
                    return data;
                }
            }
            return null;
        }

        private void UpdateImage()
        {
            if (!(_staticMap == null && _variableMap == null))
            {
                if (Width > 0 && Height > 0)
                {
                    Bitmap bmp = new Bitmap(Width, Height);
                    Graphics g = Graphics.FromImage(bmp);
                    if (_staticMap != null)
                        g.DrawImageUnscaled(_staticMap, 0, 0);
                    if (_variableMap != null)
                        g.DrawImageUnscaled(_variableMap, 0, 0);
                    g.Dispose();

                    Image = bmp;
                }
            }
            else
            {
                if (Width > 0 && Height > 0)
                {
                    Bitmap bmp = new Bitmap(Width, Height);
                    Image = bmp;
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _isCtrlPressed = e.Control;

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _isCtrlPressed = e.Control;

            base.OnKeyUp(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (!Focused)
            {
                bool isFocused = Focus();
            }

            base.OnMouseHover(e);
        }

        private void MapImage_Wheel(object sender, MouseEventArgs e)
        {
            VectorLayer.zoom = _map.Zoom;
            MainForm.upDown = 0;
            //各街道操作员超出权限范围后，不能缩小
            if (MainForm.maxZoom != 0)
            {
                double x = _LastWorldPos.X;
                double y = _LastWorldPos.Y;
                if (e.Delta > 0 && Map.Zoom > MainForm.maxZoom)
                {
                   //缩小超过范围，不可再缩小
                    return;
                }
                if (!(maxX.Equals(0) && minX.Equals(0) && maxY.Equals(0) && minY.Equals(0)))
                {
                    if (e.Delta < 0 && (x > maxX + 100 || x < minX - 100 || y > maxY + 100 || y < minY - 100))
                    {
                        //不在权限范围内不可放大
                        return;
                    }
                }
            }
            //if (false)
            //{
            if (timer.Enabled == false)
            {
                _mousedrag = e.Location;
                _mousedragImg = Image.Clone() as Image;
                timer.Tag = new Point(e.Location.X, e.Location.Y);
            }
            timer.Enabled = false;
            timer.Enabled = true;
            Point point = timer.Tag as Point;
            point.X += e.Delta / 60;
            point.Y += e.Delta / 60;

            
            Image img = new Bitmap(Size.Width, Size.Height);
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Transparent);
            float scale = 1;
            if (point.X > _mousedrag.X)
            {
                scale = (float)Math.Pow(1 / (float)(point.X - _mousedrag.X), 0.5);
            }
            else if (point.X < _mousedrag.X)
            {
                scale = 1 + (float)(_mousedrag.X - point.X) * 0.1f;
            }
                RectangleF rect = new RectangleF(0, 0, Width, Height);
                if (_map.Zoom / scale < _map.MinimumZoom)
                    scale = (float)Math.Round(_map.Zoom / _map.MinimumZoom, 4);
                rect.Width *= scale;
                rect.Height *= scale;
                rect.Offset(Width / 2f - rect.Width / 2f, Height / 2f - rect.Height / 2);
                g.DrawImage(_mousedragImg, rect);
                g.Dispose();
                Image = img;
            //}
            return;
            //if (_map != null)
            //{
            //    if (_zoomToPointer)
            //        _map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
            //    if (_mousedragImg == null)
            //    {
            //        _mousedragImg = Image.Clone() as Image;
            //    }
            //    double scale = (e.Delta / 120.0);
            //    double scaleBase = 1 + (_wheelZoomMagnitude / (10 * (_isCtrlPressed ? _fineZoomFactor : 1)));
                
            //    _map.Zoom *= Math.Pow(scaleBase, scale);

            //    if (MapZoomChanged != null)
            //        MapZoomChanged(sender, _map.Zoom);

            //    if (_zoomToPointer)
            //    {
            //        int NewCenterX = (this.Width / 2) + ((this.Width / 2) - e.X);
            //        int NewCenterY = (this.Height / 2) + ((this.Height / 2) - e.Y);

            //        _map.Center = _map.ImageToWorld(new System.Drawing.Point(NewCenterX, NewCenterY), true);

            //        if (MapCenterChanged != null)
            //            MapCenterChanged(sender, _map.Center);
            //    }

            //    Refresh();
            //}

        }

        private void MapImage_MouseDown(object sender, MouseEventArgs e)
        {
            _panOrQueryIsPan = false;
            if (_map != null)
            {
                AreaData areadata = GetArea(e.Location);
                if (areadata != null)
                {
                    if (areadata.PhotoData.MinX == areadata.PhotoData.MaxX && areadata.PhotoData.MinY == areadata.PhotoData.MaxY)
                    {
                        return; ;
                    }
                    PhotoData photodata = areadata.PhotoData;
                    _ClickAreaName = photodata.Name;
                    BoundingBox box = new BoundingBox(photodata.MinX, photodata.MinY, photodata.MaxX, photodata.MaxY);
                    RequestFromServer = true;
                    Map.ZoomToBox(box);
                    Refresh();
                    if (MapZoomChanged != null)
                    {
                        MapZoomChanged(sender, _map.Zoom);
                    }
                    return;
                }
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle) //dragging
                    _mousedrag = e.Location;
                if (MouseDown != null)
                    MouseDown(sender,_map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true), e);
            }
        }


        void MapImage_MouseClick(object sender, MouseEventArgs e)
        {
            //如果点击的是已知区域标识，则地图扩大到该区域范围显示
            //AreaData areadata = GetArea(e.Location);
            //if (areadata != null)
            //{

            //    if (areadata.PhotoData.MinX == areadata.PhotoData.MaxX && areadata.PhotoData.MinY == areadata.PhotoData.MaxY)
            //    {
            //        return; ;
            //    }
            //    PhotoData photodata = areadata.PhotoData;
            //    _ClickAreaName = photodata.Name;
            //    BoundingBox box = new BoundingBox(photodata.MinX, photodata.MinY, photodata.MaxX, photodata.MaxY);
            //    RequestFromServer = true;
            //    Map.ZoomToBox(box);
            //    Refresh();
            //    if (MapZoomChanged != null)
            //    {
            //        MapZoomChanged(sender, _map.Zoom);
            //    }
            //}
        }

        private void MapImage_DblClick(object sender, MouseEventArgs e)
        {
            if (_map != null && ActiveTool == Tools.None)
            {
                double scaleBase = 1d + (Math.Abs(_wheelZoomMagnitude) / 10d);
                if (_zoomOnDblClick && e.Button == MouseButtons.Left)
                {
                    _map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
                    if (MapCenterChanged != null) { MapCenterChanged(sender, _map.Center); }
                    _map.Zoom /= scaleBase;
                    if (MapZoomChanged != null) { MapZoomChanged(sender, _map.Zoom); }
                    Refresh();
                }
                else if (_zoomOnDblClick && e.Button == MouseButtons.Right)
                {
                    _map.Zoom *= scaleBase;
                    if (MapZoomChanged != null) { MapZoomChanged(sender, _map.Zoom); }
                    Refresh();
                }
            }
        }

        private void MapImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_map != null)
            {
                Point p = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);

                if (MouseMove != null)
                    MouseMove(sender,p, e);

                if (Image != null && e.Location != _mousedrag && !_mousedragging && (e.Button == MouseButtons.Left|| e.Button == MouseButtons.Middle))
                {
                    _mousedragImg = Image.Clone() as Image;
                    _mousedragging = true;
                    _dragImg1 = new Bitmap(Size.Width, Size.Height);
                    _dragImg2 = new Bitmap(Size.Width, Size.Height);
                }

                if (_mousedragging)
                {
                    if (MouseDrag != null)
                        MouseDrag(sender, p, e);

                    if (ActiveTool == Tools.Pan || ActiveTool == Tools.PanOrQuery)
                    {
                        Graphics g = Graphics.FromImage(_dragImg1);
                        g.Clear(Color.Transparent);
                        g.DrawImageUnscaled(_mousedragImg,
                                            new System.Drawing.Point(e.Location.X - _mousedrag.X,
                                                                     e.Location.Y - _mousedrag.Y));
                        g.Dispose();
                        _dragImgSupp = _dragImg2;
                        _dragImg2 = _dragImg1;
                        _dragImg1 = _dragImgSupp;
                        Image = _dragImg2;
                        _panOrQueryIsPan = true;
                        base.Refresh();

                    }
                    else if (ActiveTool == Tools.ZoomIn || ActiveTool == Tools.ZoomOut)
                    {
                        Image img = new Bitmap(Size.Width, Size.Height);
                        Graphics g = Graphics.FromImage(img);
                        g.Clear(Color.Transparent);
                        float scale;
                        if (ActiveTool == Tools.ZoomIn)
                        {
                            if (e.Y - _mousedrag.Y < 0) //Zoom out
                                scale = (float)Math.Pow(1 / (float)(_mousedrag.Y - e.Y), _ZoomInSeed);
                            else if (e.Y - _mousedrag.Y > 0)//Zoom in
                                scale = (float)Math.Pow(1 / (float)(e.Y - _mousedrag.Y), _ZoomInSeed);
                            else scale = 1;
                        }
                        else
                        {
                            if (e.Y - _mousedrag.Y < 0) //Zoom out
                                scale = 1 + (_mousedrag.Y - e.Y) * _ZoomOutSeed;
                            else //Zoom in
                                scale = 1 + (e.Y - _mousedrag.Y) * _ZoomOutSeed;
                        }
                        RectangleF rect = new RectangleF(0, 0, Width, Height);
                        if (_map.Zoom / scale < _map.MinimumZoom)
                            scale = (float)Math.Round(_map.Zoom / _map.MinimumZoom, 4);
                        rect.Width *= scale;
                        rect.Height *= scale;
                        rect.Offset(Width / 2f - rect.Width / 2f, Height / 2f - rect.Height / 2);
                        g.DrawImage(_mousedragImg, rect);
                        g.Dispose();
                        Image = img;
                        if (MapZooming != null)
                            MapZooming(sender, scale);
                    }
                }
                else
                {
                    bool find = false;
                    foreach (AreaData data in AreaList)
                    {
                        if (data.Area.Contains(e.Location))
                        {
                            find = true;
                            Cursor = Cursors.Arrow;
                            if (!toolTip1.Active)
                            {
                                toolTip1.Active = true;
                                toolTip1.Show(data.Name, this, (int)data.Area.Left-5, (int)data.Area.Top-75);
                            }
                            if (ShowAreaTip != null)
                            {
                                ShowAreaTip(data, e);
                            }
                            break;
                        }
                    }
                    if (!find)
                    {
                        Cursor = GetCursor();
                        if (toolTip1.Active)
                        {
                            toolTip1.Hide(this);
                            toolTip1.Active = false;
                        }
                    }
                }
            }
            this.Focus();
        }

        private void MapImage_MouseUp(object sender, MouseEventArgs e)
        {
            //if (PickCoordinate)
            //{
            //    _mousedragging = false;
            //    return;
            //}
            
            if (_map != null)
            {
                int len=1;
                if (MouseUp != null)
                    //return;
                    MouseUp(sender, _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true), e);
                //if (Math.Abs(_mousedrag.X - e.X) < len && Math.Abs(_mousedrag.Y - e.Y) < len)
                //{
                //    _mousedragging = false;
                //    return;
                //}
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    //if (bottomLeftX < minX - 500 || bottomLeftY < minY - 500 || topRigthX > maxX + 500 || topLeftY > maxY + 500)
                    //{
                    //    MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;
                    //    Refresh();
                    //    return;
                    //}
                    if (ActiveTool == Tools.ZoomOut)
                    {
                        RequestFromServer = true;
                        double scale = 1.2;
                        if (!_mousedragging)
                        {
                            _map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
                            if (MapCenterChanged != null)
                                MapCenterChanged(sender,_map.Center);
                        }
                        else
                        {
                            if (e.Y - _mousedrag.Y < 0) //Zoom out
                                scale = 1 + (_mousedrag.Y - e.Y) * _ZoomOutSeed;
                            else //Zoom in
                                scale = 1 + (e.Y - _mousedrag.Y) * _ZoomOutSeed;
                        }
                        _map.Zoom *= 1 / scale;
                        if (MapZoomChanged != null)
                            MapZoomChanged(sender, _map.Zoom);
                        _mousedragging = false;
                        Refresh();
                    }
                    else if (ActiveTool == Tools.ZoomIn)
                    {
                        RequestFromServer = true;
                        double scale = 5F/6F;
                        if (!_mousedragging)
                        {
                            _map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
                            if (MapCenterChanged != null)
                                MapCenterChanged(sender, _map.Center);
                        }
                        else
                        {
                            if (e.Y - _mousedrag.Y < 0) //Zoom out
                                scale = (float)Math.Pow(1 / (float)(_mousedrag.Y - e.Y), _ZoomInSeed);
                            else if (e.Y - _mousedrag.Y > 0)//Zoom in
                                scale = (float)Math.Pow(1 / (float)(e.Y - _mousedrag.Y), _ZoomInSeed);
                            else
                                scale = 1;
                        }
                        _map.Zoom *= 1F / scale;
                        if (MapZoomChanged != null)
                            MapZoomChanged(sender, _map.Zoom);
                        _mousedragging = false;
                        Refresh();
                    }
                    else if (ActiveTool == Tools.Pan || (ActiveTool == Tools.PanOrQuery && _panOrQueryIsPan))
                    {
                        //aa = 1;
                        //if (_LastWorldPos.X < minX || _LastWorldPos.Y < minY || _LastWorldPos.X > maxY || _LastWorldPos.Y > maxX)
                        //{
                        //    Refresh();
                        //    return;
                        //}

                        if (true)//如果超出权限范围，不可移动
                        {
                            MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;
                            //System.Drawing.Point pnt1 = new System.Drawing.Point(Width / 2 + (_mousedrag.X - e.Location.X),
                            //                                                          Height / 2 + (_mousedrag.Y - e.Location.Y));
                            //_map.Center = _map.ImageToWorld(pnt1, true);
                            //if (MapCenterChanged != null)
                            //    MapCenterChanged(sender, _map.Center);
                            //MapImage.bottomLeftX = Map.Envelope.BottomLeft.X +e.X;//界面最下端x坐标
                            //MapImage.bottomLeftY = Map.Envelope.BottomLeft.Y+ e.Y;
                            //MapImage.topLeftY = Map.Envelope.TopLeft.Y - e.Y;
                            //MapImage.topRigthX = Map.Envelope.TopRight.X - e.X;
                            //if (bottomLeftX < minX - 500 || bottomLeftY < minY - 500 || topRigthX > maxX + 500 || topLeftY > maxY + 500)
                            //{
                            //    //MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;
                            //    //_map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
                            //    //if (MapCenterChanged != null)
                            //    //    MapCenterChanged(sender, _map.Center);
                            //    Refresh();
                            //    return;
                            //}
                            if (Math.Abs(_mousedrag.X - e.Location.X) == 0 && Math.Abs(_mousedrag.Y - e.Location.Y) == 0)
                            {
                                _mousedragging = false;
                                return;
                            }

                            //MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;//界面最下端x坐标
                            //MapImage.bottomLeftY = Map.Envelope.BottomLeft.Y;
                            //MapImage.topLeftY = Map.Envelope.TopLeft.Y;
                            //MapImage.topRigthX = Map.Envelope.TopRight.X;
                            //if (bottomLeftX < minX - 500 || bottomLeftY < minY - 500 || topRigthX > maxX + 500 || topLeftY > maxY + 500)
                            //{
                            //    MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;
                            //    Refresh();
                            //    return;
                            //}
                            RequestFromServer = true;
                            if (_mousedragging)
                            {
                                //System.Drawing.Point pnt = new System.Drawing.Point(Width / 2 + (_mousedrag.X - e.Location.X),
                                //                                                    Height / 2 + (_mousedrag.Y - e.Location.Y));
                                //_map.Center = _map.ImageToWorld(pnt, true);
                                if (MapCenterChanged != null)
                                    MapCenterChanged(sender, _map.Center);
                            }
                            else if (_panOnClick && !_zoomOnDblClick)
                            {
                                _map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
                                if (MapCenterChanged != null)
                                    MapCenterChanged(sender, _map.Center);
                            }
                            //MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;//界面最下端x坐标
                            //MapImage.bottomLeftY = Map.Envelope.BottomLeft.Y;
                            //MapImage.topLeftY = Map.Envelope.TopLeft.Y;
                            //MapImage.topRigthX = Map.Envelope.TopRight.X;
                            _mousedragging = false;

                            //MapImage.bottomLeftX = Map.Envelope.BottomLeft.X + e.X;//界面最下端x坐标
                            //MapImage.bottomLeftY = Map.Envelope.BottomLeft.Y + e.Y;
                            //MapImage.topLeftY = Map.Envelope.TopLeft.Y - e.Y;
                            //MapImage.topRigthX = Map.Envelope.TopRight.X - e.X;
                            //if (bottomLeftX < minX - 500 || bottomLeftY < minY - 500 || topRigthX > maxX + 500 || topLeftY > maxY + 500)
                            //{
                            //    //MapImage.bottomLeftX = Map.Envelope.BottomLeft.X;
                            //    //_map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);
                            //    //if (MapCenterChanged != null)
                            //    //    MapCenterChanged(sender, _map.Center);
                            //    Refresh();
                            //    return;
                            //}
                            System.Drawing.Point pnt = new System.Drawing.Point(Width / 2 + (_mousedrag.X - e.Location.X),
                                                                                    Height / 2 + (_mousedrag.Y - e.Location.Y));
                            _map.Center = _map.ImageToWorld(pnt, true);
                        }
                        Refresh();
                    }
                    else if (ActiveTool == Tools.Query || (ActiveTool == Tools.PanOrQuery && !_panOrQueryIsPan))
                    {
                        if (_queryLayerIndex < 0)
                            MessageBox.Show("No active layer to query");
                        else if (_queryLayerIndex < _map.Layers.Count)
                            QueryLayer(_map.Layers[_queryLayerIndex], new PointF(e.X, e.Y));
                        else if(_queryLayerIndex - Map.Layers.Count < _map.VariableLayers.Count)
                            QueryLayer(_map.VariableLayers[_queryLayerIndex - Map.Layers.Count], new PointF(e.X, e.Y));
                        else
                            MessageBox.Show("No active layer to query");
                    }
                }

                if (_mousedragImg != null)
                {
                    _mousedragImg.Dispose();
                    _mousedragImg = null;
                }

                _mousedragging = false;
            }
        }

        private void QueryLayer(ILayer layer, PointF pt)
        {
            if (layer is ICanQueryLayer)
            {
                ICanQueryLayer queryLayer = layer as ICanQueryLayer;

                BoundingBox bbox =
                        _map.ImageToWorld(pt, true).GetBoundingBox().Grow(_map.PixelSize*5);
                FeatureDataSet ds = new FeatureDataSet();
                queryLayer.ExecuteIntersectionQuery(bbox, ds);
                if (MapQueried != null)
                {
                    if (ds.Tables.Count > 0)
                        MapQueried(ds.Tables[0]);
                    else
                        MapQueried(new FeatureDataTable());
                }
                if (MapQueriedDataSet != null)
                    MapQueriedDataSet(ds);
            }
        }

        #region Events

        #region Delegates

        /// <summary>
        /// 地图动作变化事件
        /// </summary>
        /// <param name="tool"></param>
        public delegate void ActiveToolChangedHandler(Tools tool);

        /// <summary>
        /// 地图中心点变化事件
        /// </summary>
        /// <param name="center"></param>
        public delegate void MapCenterChangedHandler(object sender,Point center);

        [Obsolete]
        public delegate void MapQueryHandler(FeatureDataTable data);

        
        public delegate void MapQueryDataSetHandler(FeatureDataSet data);

        /// <summary>
        /// 比例变化事件
        /// </summary>
        /// <param name="zoom"></param>
        public delegate void MapZoomHandler(object sender, double zoom);

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="WorldPos"></param>
        /// <param name="ImagePos"></param>
        public delegate void MouseEventHandler(object sender,Point WorldPos, MouseEventArgs ImagePos);

        public delegate void ShowAreaTipHandler(AreaData areaData, MouseEventArgs e);

        #endregion

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        public new event MouseEventHandler MouseMove;
        public new event ShowAreaTipHandler ShowAreaTip;
        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        public new event MouseEventHandler MouseDown;

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>		
        public new event MouseEventHandler MouseUp;

        /// <summary>
        /// 鼠标多痛事件
        /// </summary>
        public event MouseEventHandler MouseDrag;

        /// <summary>
        /// 地图刷新事件
        /// </summary>
        public event EventHandler MapRefreshed;

        /// <summary>
        /// 比例变化事件
        /// </summary>
        public event MapZoomHandler MapZoomChanged;

        /// <summary>
        /// 比例变化前事件
        /// </summary>
        public event MapZoomHandler MapZooming;

        public event MapQueryHandler MapQueried;

        public event MapQueryDataSetHandler MapQueriedDataSet;

        /// <summary>
        /// 地图中心点变化事件
        /// </summary>
        public event MapCenterChangedHandler MapCenterChanged;

        /// <summary>
        /// 地图动作事件
        /// </summary>
        public event ActiveToolChangedHandler ActiveToolChanged;

        #endregion

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }    }
}
