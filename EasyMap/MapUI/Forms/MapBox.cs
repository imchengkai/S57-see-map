#define EnableMetafileClipboardSupport

#if UseMapBox || UseMapBoxAsMapImage
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EasyMap.Layers;
using System.Drawing.Imaging;
using System.Diagnostics;

using GeoPoint = EasyMap.Geometries.Point;
using Geometry = EasyMap.Geometries.Geometry;
using BoundingBox = EasyMap.Geometries.BoundingBox;
using System.Threading;

namespace EasyMap.Forms
{
    [DesignTimeVisible(true)]
#if UseMapBoxAsMapImage
    public class MapImage : Control
#else
    public class MapBox : Control
#endif
    {
        #region PreviewModes enumerator
        public enum PreviewModes
        {
            Best,
            Fast
        }
        #endregion

        #region Position enumerators
        private enum XPosition
        {
            Center = 0,
            Right = 1,
            Left = -1
        }

        private enum YPosition
        {
            Center = 0,
            Top = -1,
            Bottom = 1
        }
        #endregion

        #region Tools enumerator
        public enum Tools
        {
            Pan,
            ZoomIn,
            ZoomOut,

            QueryBox,

            Query = QueryBox,

            QueryGeometry,

            ZoomWindow,
            DrawPoint,
            DrawLine,
            DrawPolygon,
            None
        }
        #endregion

        #region Events
        public delegate void MouseEventHandler(GeoPoint worldPos, MouseEventArgs imagePos);
        public new event MouseEventHandler MouseMove;
        public new event MouseEventHandler MouseDown;
        public new event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseDrag;

        public event EventHandler MapRefreshed;

        public delegate void MapZoomHandler(double zoom);
        public event MapZoomHandler MapZoomChanged;
        public event MapZoomHandler MapZooming;

        public delegate void MapQueryHandler(Data.FeatureDataTable data);
        public event MapQueryHandler MapQueried;


        public delegate void MapCenterChangedHandler(GeoPoint center);
        public event MapCenterChangedHandler MapCenterChanged;

        public delegate void ActiveToolChangedHandler(Tools tool);
        public event ActiveToolChangedHandler ActiveToolChanged;

        public delegate void GeometryDefinedHandler(Geometry geometry);

        public event GeometryDefinedHandler GeometryDefined;


        #endregion

        private static int _defaultColorIndex;

        private static readonly Color[] DefaultColors =
            new[]
                {
                    Color.DarkRed,
                    Color.DarkGreen, 
                    Color.DarkBlue, 
                    Color.Orange, 
                    Color.Cyan, 
                    Color.Black, 
                    Color.Purple, 
                    Color.Yellow,
                    Color.LightBlue, 
                    Color.Fuchsia
                };
        private const float MinDragScalingBeforeRegen = 0.3333f;
        private const float MaxDragScalingBeforeRegen = 3f;

        private readonly ProgressBar _progressBar;

#if DEBUG
        private readonly Stopwatch _watch = new Stopwatch();
#endif

        private double _wheelZoomMagnitude = 2;
        private Tools _activeTool;
        private double _fineZoomFactor = 10;
        private Map _map;
        private int _queryLayerIndex;
        private Point _dragStartPoint;
        private Point _dragEndPoint;
        private Bitmap _dragImage;
        private Rectangle _rectangle = Rectangle.Empty;
        private bool _dragging;
        private readonly SolidBrush _rectangleBrush = new SolidBrush(Color.FromArgb(210, 244, 244, 244));
        private readonly Pen _rectanglePen = new Pen(Color.FromArgb(244, 244, 244), 1);

        private float _scaling;
        private Image _image = new Bitmap(1, 1);
        private Image _imageBackground = new Bitmap(1, 1);
        private Image _imageStatic = new Bitmap(1, 1);
        private Image _imageVariable = new Bitmap(1, 1);
        private BoundingBox _imageBoundingBox = new BoundingBox(0, 0, 1, 1);
        private int _imageGeneration = 0;

        private PreviewModes _previewMode;
        private bool _isRefreshing;
        private EasyMap.Geometries.Point[] _pointArray;
        private bool _showProgress;
        private bool _zoomToPointer = false;
        private bool _setActiveToolNoneDuringRedraw = true;
        private bool _shiftButtonDragRectangleZoom = false;
        private bool _focusOnHover = false;
        private bool _panOnClick = true;
        private float _queryGrowFactor = 5f;

        readonly IMessageFilter _mousePreviewFilter = null;

        public static void RandomizeLayerColors(VectorLayer layer)
        {
            layer.Style.EnableOutline = true;
            layer.Style.Fill = new SolidBrush(Color.FromArgb(80, DefaultColors[_defaultColorIndex % DefaultColors.Length]));
            layer.Style.Outline = new Pen(Color.FromArgb(100, DefaultColors[(_defaultColorIndex + ((int)(DefaultColors.Length * 0.5))) % DefaultColors.Length]), 1f);
            _defaultColorIndex++;
        }

        [Description("Define if the progress Bar is shown")]
        [Category("Appearance")]
        public bool ShowProgressUpdate
        {
            get
            {
                return _showProgress;
            }

            set
            {
                _showProgress = value;
                _progressBar.Visible = _showProgress;
            }
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

        [Description("Sets whether the mapcontrol should automatically grab focus when mouse is hovering the control")]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool TakeFocusOnHover
        {
            get
            {
                return _focusOnHover;
            }
            set
            {
                _focusOnHover = value;
            }
        }

        [Description("Sets whether the mouse wheel should zoom to the pointer location")]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool ZoomToPointer
        {
            get { return _zoomToPointer; }
            set { _zoomToPointer = value; }
        }

        [Description("Sets ActiveTool to None (and changing cursor) while redrawing the map")]
        [DefaultValue(true)]
        [Category("Behavior")]
        public bool SetToolsNoneWhileRedrawing
        {
            get { return _setActiveToolNoneDuringRedraw; }
            set { _setActiveToolNoneDuringRedraw = value; }
        }

        [Description("Gets or sets the number of pixels by which a bounding box around the query point should be \"grown\" prior to perform the query")]
        [DefaultValue(5)]
        [Category("Behavior")]
        public float QueryGrowFactor
        {
            get { return _queryGrowFactor; }
            set
            {
                if (value < 0) value = 0;
                _queryGrowFactor = value;
            }
        }


        [Description("The color of selecting rectangle.")]
        [Category("Appearance")]
        public Color SelectionBackColor
        {
            get { return _rectangleBrush.Color; }
            set
            {
                _rectangleBrush.Color = value;
            }
        }

        [Description("The map image currently visualized.")]
        [Category("Appearance")]
        public Image Image
        {
            get
            {

                GetImagesAsyncEnd(null);
                return _image;
            }
        }

        [Description("The color of selectiong rectangle frame.")]
        [Category("Appearance")]
        public Color SelectionForeColor
        {
            get { return _rectanglePen.Color; }
            set
            {
                _rectanglePen.Color = value;
            }
        }

        [Description("The amount which a single movement of the mouse wheel zooms by.")]
        [DefaultValue(2)]
        [Category("Behavior")]
        public double WheelZoomMagnitude
        {
            get { return _wheelZoomMagnitude; }
            set { _wheelZoomMagnitude = value; }
        }

        [Description("Mode used to create preview image while panning or zooming.")]
        [DefaultValue(PreviewModes.Best)]
        [Category("Behavior")]
        public PreviewModes PreviewMode
        {
            get { return _previewMode; }
            set
            {
                if (!_dragging)
                    _previewMode = value;
            }
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

        [Description("Enables shortcut to rectangle-zoom by holding down shift-button and drag rectangle")]
        [DefaultValue(false)]
        [Category("Behavior")]
        public bool EnableShiftButtonDragRectangleZoom
        {
            get { return _shiftButtonDragRectangleZoom; }
            set { _shiftButtonDragRectangleZoom = value; }
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
                    VariableLayerCollection.VariableLayerCollectionRequery += HandleVariableLayersRequery;
                    _map.MapNewTileAvaliable += HandleMapNewTileAvaliable;
                    Refresh();
                }
            }
        }

        public int QueryLayerIndex
        {
            get { return _queryLayerIndex; }
            set { _queryLayerIndex = value; }
        }

        public Tools ActiveTool
        {
            get { return _activeTool; }
            set
            {
                var check = (value != _activeTool);
                _activeTool = value;

                SetCursor();

                _pointArray = null;

                if (check && ActiveToolChanged != null)
                    ActiveToolChanged(value);
            }
        }

#pragma warning disable 1587
#if DEBUG
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan LastRefreshTime { get; set; }
#endif

#if UseMapBoxAsMapImage
       public MapImage()
#else
        public MapBox()
#endif
#pragma warning restore 1587
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            base.DoubleBuffered = true;
            _map = new Map(ClientSize);
            VariableLayerCollection.VariableLayerCollectionRequery += HandleVariableLayersRequery;
            _map.MapNewTileAvaliable += HandleMapNewTileAvaliable;

            _activeTool = Tools.None;
            LostFocus += HandleMapBoxLostFocus;


            _progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Location = new Point(2, 2),
                Size = new Size(50, 10)
            };
            Controls.Add(_progressBar);
            _progressBar.Visible = false;

            _mousePreviewFilter = new MouseWheelGrabber(this);
            Application.AddMessageFilter(_mousePreviewFilter);

        }

        protected override void Dispose(bool disposing)
        {
            VariableLayerCollection.VariableLayerCollectionRequery -= HandleVariableLayersRequery;
            _map.MapNewTileAvaliable -= HandleMapNewTileAvaliable;
            LostFocus -= HandleMapBoxLostFocus;

            if (_mousePreviewFilter != null)
                Application.RemoveMessageFilter(_mousePreviewFilter);

            base.Dispose(disposing);
        }

        #region event handling
        private void HandleMapBoxLostFocus(object sender, EventArgs e)
        {
            if (!_dragging) return;

            _dragging = false;
            Invalidate(ClientRectangle);
        }

        private void HandleVariableLayersRequery(object sender, EventArgs e)
        {
            Image oldRef;
            lock (_map)
            {
                if (_dragging) return;
                oldRef = _imageVariable;
                _imageVariable = GetMap(_map, _map.VariableLayers, LayerCollectionType.Variable, _map.Envelope);
            }

            UpdateImage(false);
            if (oldRef != null)
                oldRef.Dispose();

            Invalidate();
            Application.DoEvents();
        }

        void HandleMapNewTileAvaliable(TileLayer sender, BoundingBox box, Bitmap bm, int sourceWidth, int sourceHeight, ImageAttributes imageAttributes)
        {
            lock (_imageBackground)
            {
                try
                {
                    PointF min = _map.WorldToImage(new GeoPoint(box.Min.X, box.Min.Y));
                    PointF max = _map.WorldToImage(new GeoPoint(box.Max.X, box.Max.Y));

                    min = new PointF((float)Math.Round(min.X), (float)Math.Round(min.Y));
                    max = new PointF((float)Math.Round(max.X), (float)Math.Round(max.Y));

                    if (IsDisposed == false)
                    {
                        Graphics g = Graphics.FromImage(_imageBackground);

                        g.DrawImage(bm,
                            new Rectangle((int)min.X, (int)max.Y, (int)(max.X - min.X), (int)(min.Y - max.Y)),
                            0, 0,
                            sourceWidth, sourceHeight,
                            GraphicsUnit.Pixel,
                            imageAttributes);

                        g.Dispose();
                        UpdateImage(false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }


        }

        #endregion

        private Image GetMap(Map map, LayerCollection layers, LayerCollectionType layerCollectionType, BoundingBox extent)
        {

            if ((layers == null || layers.Count == 0 || Width == 0 || Height == 0))
            {
                if (layerCollectionType == LayerCollectionType.Background)
                    return new Bitmap(1, 1);
                return null;
            }

            var retval = new Bitmap(Width, Height);

            using (var g = Graphics.FromImage(retval))
            {
                map.RenderMap(g, layerCollectionType, false);
            }

            if (layerCollectionType == LayerCollectionType.Variable)
                retval.MakeTransparent(_map.BackColor);

            return retval;
        }


        private void GetImagesAsync(BoundingBox extent)
        {
            Map safeMap = null;
            lock (_map)
            {
                safeMap = _map.Clone();
                _imageVariable = GetMap(safeMap, _map.VariableLayers, LayerCollectionType.Variable, extent);
                _imageStatic = GetMap(safeMap, _map.Layers, LayerCollectionType.Static, extent);
                _imageBackground = GetMap(safeMap, _map.BackgroundLayer, LayerCollectionType.Background, extent);
            }
        }

        class GetImageEndResult
        {
            public Tools? Tool { get; set; }
            public BoundingBox bbox { get; set; }
            public int generation { get; set; }
        }
        private void GetImagesAsyncEnd(GetImageEndResult res)
        {
            if (res == null || res.generation < _imageGeneration)
                return;

#if DEBUG
            Debug.WriteLine(string.Format("{2}: {0} - {1}", res.generation, res.bbox, DateTime.Now));
#endif

            if ((_setActiveToolNoneDuringRedraw || ShowProgressUpdate) && InvokeRequired)
            {
                try
                {
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        GetImagesAsyncEnd(res);
                    }));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                try
                {
                    var oldRef = _image;
                    if (Width > 0 && Height > 0)
                    {

                        var bmp = new Bitmap(Width, Height);

                        lock (_map)
                        {
                            using (var g = Graphics.FromImage(bmp))
                            {
                                if (_imageBackground != null)
                                {
                                    try
                                    {
                                        g.DrawImageUnscaled(_imageBackground, 0, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }
                                }

                                if (_imageStatic != null)
                                {
                                    try
                                    {
                                        g.DrawImageUnscaled(_imageStatic, 0, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }

                                }

                                if (_imageVariable != null)
                                {
                                    try
                                    {
                                        g.DrawImageUnscaled(_imageVariable, 0, 0);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }
                                }

                                g.Dispose();
                            }



                            _image = bmp;
                            _imageBoundingBox = res.bbox;
                        }
                    }

                    if (res.Tool.HasValue)
                    {
                        if (_setActiveToolNoneDuringRedraw)
                            ActiveTool = res.Tool.Value;

                        _dragEndPoint = new Point(0, 0);
                        _isRefreshing = false;

                        if (_setActiveToolNoneDuringRedraw)
                            Enabled = true;

                        if (ShowProgressUpdate)
                        {
                            _progressBar.Enabled = false;
                            _progressBar.Visible = false;
                        }
                    }

                    if (oldRef != null)
                        oldRef.Dispose();

                    Invalidate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

#if DEBUG
                _watch.Stop();
                LastRefreshTime = _watch.Elapsed;
#endif

                try
                {
                    if (MapRefreshed != null)
                    {
                        MapRefreshed(this, null);
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.ToString());
                }
            }
        }

        private void UpdateImage(bool forceRefresh)
        {
            if (((_imageStatic == null && _imageVariable == null && _imageBackground == null) && !forceRefresh) ||
                (Width == 0 || Height == 0)) return;

            BoundingBox bbox = _map.Envelope;
            if (forceRefresh) // && _isRefreshing == false)
            {
                _isRefreshing = true;
                Tools oldTool = ActiveTool;
                if (_setActiveToolNoneDuringRedraw)
                {
                    ActiveTool = Tools.None;
                    Enabled = false;
                }

                if (ShowProgressUpdate)
                {
                    _progressBar.Visible = true;
                    _progressBar.Enabled = true;
                }

                int generation = ++_imageGeneration;
                ThreadPool.QueueUserWorkItem(
                    delegate
                    {
                        GetImagesAsync(bbox);
                        GetImagesAsyncEnd(new GetImageEndResult { Tool = oldTool, bbox = bbox, generation = generation });
                    });
            }
            else
            {
                GetImagesAsyncEnd(new GetImageEndResult { Tool = null, bbox = bbox, generation = _imageGeneration });
            }
        }


        private void SetCursor()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(SetCursor));
                return;
            }

            if (_activeTool == Tools.None)
                Cursor = Cursors.Default;
            if (_activeTool == Tools.Pan)
                Cursor = Cursors.Hand;
            else if (_activeTool == Tools.Query || _activeTool == Tools.QueryGeometry)
                Cursor = Cursors.Help;
            else if (_activeTool == Tools.ZoomIn || _activeTool == Tools.ZoomOut || _activeTool == Tools.ZoomWindow)
                Cursor = Cursors.Cross;
            else if (_activeTool == Tools.DrawPoint || _activeTool == Tools.DrawPolygon || _activeTool == Tools.DrawLine)
                Cursor = Cursors.Cross;
        }


        public override void Refresh()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(Refresh));
                    return;
                }
#if DEBUG
                _watch.Reset();
                _watch.Start();
#endif
                if (_map != null)
                {
                    _map.Size = ClientSize;
                    if ((_map.Layers == null || _map.Layers.Count == 0) &&
                        (_map.BackgroundLayer == null || _map.BackgroundLayer.Count == 0) &&
                        (_map.VariableLayers == null || _map.VariableLayers.Count == 0))
                        _image = null;
                    else
                    {
                        Cursor c = Cursor;
                        if (_setActiveToolNoneDuringRedraw)
                        {
                            Cursor = Cursors.WaitCursor;
                        }
                        UpdateImage(true);
                        if (_setActiveToolNoneDuringRedraw)
                        {
                            Cursor = c;
                        }
                    }

                    base.Refresh();
                    Invalidate();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private static Boolean IsControlPressed { get { return (ModifierKeys & Keys.ControlKey) == Keys.ControlKey; } }


        protected override void OnMouseHover(EventArgs e)
        {
            if (_focusOnHover)
                TestAndGrabFocus();
            base.OnMouseHover(e);
        }

        private void TestAndGrabFocus()
        {
            if (!Focused)
            {
                bool isFocused = Focus();
                Debug.WriteLine("Focused: " + isFocused);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_map != null)
            {
                if (_zoomToPointer)
                    _map.Center = _map.ImageToWorld(new System.Drawing.Point(e.X, e.Y), true);

                double scale = (e.Delta / 120.0);
                double scaleBase = 1 + (_wheelZoomMagnitude / (10 * (IsControlPressed ? _fineZoomFactor : 1)));

                _map.Zoom *= Math.Pow(scaleBase, scale);

                if (MapZoomChanged != null)
                    MapZoomChanged(_map.Zoom);

                if (_zoomToPointer)
                {
                    int NewCenterX = (this.Width / 2) + ((this.Width / 2) - e.X);
                    int NewCenterY = (this.Height / 2) + ((this.Height / 2) - e.Y);

                    _map.Center = _map.ImageToWorld(new System.Drawing.Point(NewCenterX, NewCenterY), true);

                    if (MapCenterChanged != null)
                        MapCenterChanged(_map.Center);
                }
                Invalidate();
                Refresh();
            }
        }

        GeoPoint _dragStartCoord = null;
        double _orgScale = 0;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_map != null)
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle) //dragging
                {
                    _dragStartPoint = e.Location;
                    _dragEndPoint = e.Location;
                    _dragStartCoord = _map.Center;
                    _orgScale = _map.Zoom;
                }

                if (MouseDown != null)
                    MouseDown(_map.ImageToWorld(new Point(e.X, e.Y)), e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_map != null)
            {
                GeoPoint p = _map.ImageToWorld(new Point(e.X, e.Y));

                if (MouseMove != null)
                    MouseMove(p, e);

                bool isStartDrag = _image != null && e.Location != _dragStartPoint && !_dragging &&
                    (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle) &&   //Left of middle button can start drag
                    !(_setActiveToolNoneDuringRedraw && (_activeTool == Tools.DrawLine || _activeTool == Tools.DrawPoint || _activeTool == Tools.DrawPolygon)); //It should not be any of these tools

                if (isStartDrag)
                {
                    _dragging = true;
                }

                if (_dragging)
                {
                    if (MouseDrag != null)
                        MouseDrag(p, e);

                    bool isPanOperation = _activeTool == Tools.Pan && !(_shiftButtonDragRectangleZoom && (Control.ModifierKeys & Keys.Shift) != Keys.None);

                    if ((_activeTool == Tools.DrawLine || _activeTool == Tools.DrawPolygon)
                        && e.Button == System.Windows.Forms.MouseButtons.Left && !_setActiveToolNoneDuringRedraw)
                    {
                        isPanOperation = true;
                    }


                    bool isZoomOperation = _activeTool == Tools.ZoomIn || _activeTool == Tools.ZoomOut;

                    bool isZoomWindowOperation = _activeTool == Tools.ZoomWindow || _activeTool == Tools.Query || _activeTool == Tools.QueryGeometry || (_shiftButtonDragRectangleZoom && (Control.ModifierKeys & Keys.Shift) != Keys.None);

                    if (isPanOperation)
                    {
                        _dragEndPoint = ClipPoint(e.Location);
                        if (_dragStartCoord != null)
                        {
                            _map.Center = new GeoPoint(_dragStartCoord.X - _map.PixelSize * (_dragEndPoint.X - _dragStartPoint.X), _dragStartCoord.Y - _map.PixelSize * (_dragStartPoint.Y - _dragEndPoint.Y));
                            Invalidate(ClientRectangle);
                        }
                    }
                    else if (isZoomOperation)
                    {
                        _dragEndPoint = ClipPoint(e.Location);
                        if (_dragEndPoint.Y - _dragStartPoint.Y < 0) //Zoom out
                            _scaling = (float)Math.Pow(1 / (float)(_dragStartPoint.Y - _dragEndPoint.Y), 0.5);
                        else //Zoom in
                            _scaling = 1 + (_dragEndPoint.Y - _dragStartPoint.Y) * 0.1f;

                        _map.Zoom = _orgScale / _scaling;
                        if (MapZooming != null)
                            MapZooming(_map.Zoom);

                        Invalidate(ClientRectangle);
                    }
                    else if (isZoomWindowOperation)
                    {
                        _dragEndPoint = ClipPoint(e.Location);
                        _rectangle = GenerateRectangle(_dragStartPoint, _dragEndPoint);
                        Invalidate(new Region(ClientRectangle));
                    }
                }
                else
                {
                    if (_activeTool == Tools.DrawPolygon || _activeTool == Tools.DrawLine)
                    {
                        _dragEndPoint = new Point(0, 0);
                        if (_pointArray != null)
                        {
                            _pointArray[_pointArray.GetUpperBound(0)] = Map.ImageToWorld(ClipPoint(e.Location));
                            _rectangle = GenerateRectangle(_dragStartPoint, ClipPoint(e.Location));
                            Invalidate(new Region(ClientRectangle));
                        }
                    }
                }
            }
        }






#if EnableMetafileClipboardSupport

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.Clear();
                ClipboardMetafileHelper.PutEnhMetafileOnClipboard(Handle, _map.GetMapAsMetafile());
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

#endif

        private void RegenerateZoomingImage()
        {
            Cursor c = Cursor;
            Cursor = Cursors.WaitCursor;
            _map.Zoom /= _scaling;
            lock (_map)
            {
                _image = _map.GetMap();
            }
            _scaling = 1;
            _dragImage = GenerateDragImage(PreviewModes.Best);
            _dragStartPoint = _dragEndPoint;
            Cursor = c;
        }

        private Bitmap GenerateDragImage(PreviewModes mode)
        {
            if (mode == PreviewModes.Best)
            {
                Cursor c = Cursor;
                Cursor = Cursors.WaitCursor;

                GeoPoint realCenter = _map.Center;
                Bitmap bmp = new Bitmap(_map.Size.Width * 3, _map.Size.Height * 3);
                Graphics g = Graphics.FromImage(bmp);

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            var clone = _image.Clone() as Image;
                            if (clone != null)
                                g.DrawImageUnscaled(clone, _map.Size.Width, _map.Size.Height);
                        }
                        else
                            g.DrawImageUnscaled(GeneratePartialBitmap(realCenter, (XPosition)i, (YPosition)j), (i + 1) * _map.Size.Width, (j + 1) * _map.Size.Height);
                    }
                }
                g.Dispose();
                _map.Center = realCenter;

                Cursor = c;

                return bmp;
            }
            if (_image.PixelFormat != PixelFormat.Undefined)
                return _image.Clone() as Bitmap;
            return null;
        }

        private Bitmap GeneratePartialBitmap(GeoPoint center, XPosition xPos, YPosition yPos)
        {
            double x = center.X, y = center.Y;

            switch (xPos)
            {
                case XPosition.Right:
                    x += _map.Envelope.Width;
                    break;
                case XPosition.Left:
                    x -= _map.Envelope.Width;
                    break;
            }

            switch (yPos)
            {
                case YPosition.Top:
                    y += _map.Envelope.Height;
                    break;
                case YPosition.Bottom:
                    y -= _map.Envelope.Height;
                    break;
            }

            _map.Center = new GeoPoint(x, y);
            return _map.GetMap() as Bitmap;
        }

        private Point ClipPoint(Point p)
        {
            int x = p.X < 0 ? 0 : (p.X > ClientSize.Width ? ClientSize.Width : p.X);
            int y = p.Y < 0 ? 0 : (p.Y > ClientSize.Height ? ClientSize.Height : p.Y);
            return new Point(x, y);
        }

        private static Rectangle GenerateRectangle(Point p1, Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int width = Math.Abs(p2.X - p1.X);
            int height = Math.Abs(p2.Y - p1.Y);

            return new Rectangle(x, y, width, height);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            try
            {
                if (_dragging)
                {
                    if (_activeTool == Tools.ZoomWindow || _activeTool == Tools.Query || (_shiftButtonDragRectangleZoom && (Control.ModifierKeys & Keys.Shift) != Keys.None))
                    {
                        Bitmap patch = (_image as Bitmap).Clone(pe.ClipRectangle, PixelFormat.DontCare);
                        pe.Graphics.DrawImageUnscaled(patch, pe.ClipRectangle);
                        patch.Dispose();

                        if (_rectangle.Width > 0 && _rectangle.Height > 0)
                        {
                            pe.Graphics.FillRectangle(_rectangleBrush, _rectangle);
                            Rectangle border = new Rectangle(_rectangle.X + (int)_rectanglePen.Width / 2, _rectangle.Y + (int)_rectanglePen.Width / 2, _rectangle.Width - (int)_rectanglePen.Width, _rectangle.Height - (int)_rectanglePen.Width);
                            pe.Graphics.DrawRectangle(_rectanglePen, border);
                        }
                    }
                    else if (_activeTool == Tools.Pan || _activeTool == Tools.ZoomIn || _activeTool == Tools.ZoomOut || _activeTool == Tools.DrawLine || _activeTool == Tools.DrawPolygon)
                    {
                        if (_map.Envelope.Equals(_imageBoundingBox))
                        {
                            pe.Graphics.DrawImageUnscaled(_image, 0, 0);
                        }
                        else
                        {
                            PointF ul = PointF.Empty;
                            PointF lr = PointF.Empty;

                            lock (_imageBoundingBox)
                            {
                                ul = _map.WorldToImage(_imageBoundingBox.TopLeft);
                                lr = _map.WorldToImage(_imageBoundingBox.BottomRight);

                                pe.Graphics.DrawImage(_image, RectangleF.FromLTRB(ul.X, ul.Y, lr.X, lr.Y));
                            }
                            if ((Math.Abs(ul.X) > 50 || Math.Abs(ul.Y) > 50) && !_isRefreshing)
                            {
                                int generation = ++_imageGeneration;
                                BoundingBox bbox = _map.Envelope;
                            }
                        }
                    }
                    else if (_activeTool == Tools.ZoomIn || _activeTool == Tools.ZoomOut)
                    {
                        RectangleF rect = new RectangleF(0, 0, _map.Size.Width, _map.Size.Height);

                        if (_map.Zoom / _scaling < _map.MinimumZoom)
                            _scaling = (float)Math.Round(_map.Zoom / _map.MinimumZoom, 4);

                        if (_previewMode == PreviewModes.Best)
                            _scaling *= 3;

                        rect.Width *= _scaling;
                        rect.Height *= _scaling;

                        rect.Offset(_map.Size.Width / 2f - rect.Width / 2, _map.Size.Height / 2f - rect.Height / 2);

                        pe.Graphics.DrawImage(_dragImage, rect);
                    }
                }
                else if (_image != null && _image.PixelFormat != PixelFormat.Undefined)
                {
                    {

                        if (_map.Envelope.Equals(_imageBoundingBox))
                        {
                            pe.Graphics.DrawImageUnscaled(_image, 0, 0);
                        }
                        else
                        {
                            PointF ul = _map.WorldToImage(_imageBoundingBox.TopLeft);
                            PointF lr = _map.WorldToImage(_imageBoundingBox.BottomRight);
                            pe.Graphics.DrawImage(_image, RectangleF.FromLTRB(ul.X, ul.Y, lr.X, lr.Y));
                        }
                    }
                }


                if (_pointArray != null)
                {
                    if (_pointArray.GetUpperBound(0) == 1)
                    {
                        PointF p1 = Map.WorldToImage(_pointArray[0]);
                        PointF p2 = Map.WorldToImage(_pointArray[1]);
                        pe.Graphics.DrawLine(new Pen(Color.Gray, 2F), p1, p2);
                    }
                    else
                    {
                        PointF[] pts = new PointF[_pointArray.Length];
                        for (int i = 0; i < pts.Length; i++)
                            pts[i] = Map.WorldToImage(_pointArray[i]);

                        if (_activeTool == Tools.DrawPolygon)
                        {
                            Color c = Color.FromArgb(127, Color.Gray);
                            pe.Graphics.FillPolygon(new SolidBrush(c), pts);
                            pe.Graphics.DrawPolygon(new Pen(Color.Gray, 2F), pts);
                        }
                        else
                            pe.Graphics.DrawLines(new Pen(Color.Gray, 2F), pts);
                    }
                }


                base.OnPaint(pe);

                /*Draw Floating Map-Decorations*/
                if (_map != null && _map.Decorations != null)
                {
                    foreach (EasyMap.Rendering.Decoration.IMapDecoration md in _map.Decorations)
                    {
                        md.Render(pe.Graphics, _map);
                    }
                }
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.Message);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_map != null)
            {
                if (MouseUp != null)
                    MouseUp(_map.ImageToWorld(new Point(e.X, e.Y)), e);

                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                {
                    if (_activeTool == Tools.ZoomOut)
                    {
                        double scale = 0.5;
                        if (_dragging)
                        {
                            if (e.Y - _dragStartPoint.Y < 0) //Zoom out
                                scale = (float)Math.Pow(1 / (float)(_dragStartPoint.Y - e.Y), 0.5);
                            else //Zoom in
                                scale = 1 + (e.Y - _dragStartPoint.Y) * 0.1;
                        }
                        else
                        {
                            _map.Center = _map.ImageToWorld(new Point(e.X, e.Y));

                            if (MapCenterChanged != null)
                                MapCenterChanged(_map.Center);
                        }

                        _map.Zoom /= scale;

                        if (MapZoomChanged != null)
                            MapZoomChanged(_map.Zoom);
                    }
                    else if (_activeTool == Tools.ZoomIn)
                    {
                        double scale = 2;
                        if (_dragging)
                        {
                            if (e.Y - _dragStartPoint.Y < 0) //Zoom out
                                scale = (float)Math.Pow(1 / (float)(_dragStartPoint.Y - e.Y), 0.5);
                            else //Zoom in
                                scale = 1 + (e.Y - _dragStartPoint.Y) * 0.1;
                        }
                        else
                        {
                            _map.Center = _map.ImageToWorld(new Point(e.X, e.Y));

                            if (MapCenterChanged != null)
                                MapCenterChanged(_map.Center);
                        }

                        _map.Zoom *= 1 / scale;

                        if (MapZoomChanged != null)
                            MapZoomChanged(_map.Zoom);

                    }
                    else if ((_activeTool == Tools.Pan && !(_shiftButtonDragRectangleZoom && (Control.ModifierKeys & Keys.Shift) != Keys.None)) ||
                        (e.Button == System.Windows.Forms.MouseButtons.Left && _dragging && (_activeTool == Tools.DrawLine || _activeTool == Tools.DrawPolygon)))
                    {
                        if (_dragging)
                        {
                            if (MapCenterChanged != null)
                                MapCenterChanged(_map.Center);
                        }
                        else
                        {
                            if (_panOnClick)
                            {
                                _map.Center = _map.ImageToWorld(new Point(e.X, e.Y));

                                if (MapCenterChanged != null)
                                    MapCenterChanged(_map.Center);
                            }
                        }
                    }
                    else if (_activeTool == Tools.Query || _activeTool == Tools.QueryGeometry)
                    {
                        if (_map.Layers.Count > _queryLayerIndex && _queryLayerIndex > -1)
                        {
                            var layer = _map.Layers[_queryLayerIndex] as ICanQueryLayer;
                            if (layer != null)
                            {
                                BoundingBox bounding;
                                bool isPoint = false;
                                if (_dragging)
                                {
                                    GeoPoint lowerLeft;
                                    GeoPoint upperRight;
                                    GetBounds(_map.ImageToWorld(_dragStartPoint), _map.ImageToWorld(_dragEndPoint), out lowerLeft, out upperRight);

                                    bounding = new BoundingBox(lowerLeft, upperRight);
                                }
                                else
                                {
                                    bounding =
                                        _map.ImageToWorld(new Point(e.X, e.Y)).GetBoundingBox().Grow(_map.PixelSize *
                                                                                                     _queryGrowFactor);
                                    isPoint = true;
                                }

                                Data.FeatureDataSet ds = new Data.FeatureDataSet();
                                if (_activeTool == Tools.Query)
                                    layer.ExecuteIntersectionQuery(bounding, ds);
                                else
                                {
                                    Geometry geom;
                                    if (isPoint && QueryGrowFactor == 0)
                                        geom = _map.ImageToWorld(new Point(e.X, e.Y));
                                    else
                                        geom = bounding.ToGeometry();
                                    layer.ExecuteIntersectionQuery(geom, ds);
                                }

                                if (ds.Tables.Count > 0)
                                    if (MapQueried != null) MapQueried(ds.Tables[0]);
                                    else if (MapQueried != null) MapQueried(new Data.FeatureDataTable());
                            }

                        }
                        else
                            MessageBox.Show("No active layer to query");
                    }
                    else if (_activeTool == Tools.ZoomWindow || (_shiftButtonDragRectangleZoom && (Control.ModifierKeys & Keys.Shift) != Keys.None))
                    {
                        if (_rectangle.Width > 0 && _rectangle.Height > 0)
                        {
                            GeoPoint lowerLeft;
                            GeoPoint upperRight;
                            GetBounds(_map.ImageToWorld(_dragStartPoint), _map.ImageToWorld(_dragEndPoint), out lowerLeft, out upperRight);
                            _dragEndPoint.X = 0;
                            _dragEndPoint.Y = 0;

                            _map.ZoomToBox(new BoundingBox(lowerLeft, upperRight));

                            if (MapZoomChanged != null)
                                MapZoomChanged(_map.Zoom);

                        }
                    }
                    else if (_activeTool == Tools.DrawPoint)
                    {
                        if (GeometryDefined != null)
                        {
                            GeometryDefined(Map.ImageToWorld(new PointF(e.X, e.Y)));
                        }
                    }
                    else if (_activeTool == Tools.DrawPolygon || _activeTool == Tools.DrawLine)
                    {
                        if (_pointArray == null)
                        {
                            _pointArray = new EasyMap.Geometries.Point[2];
                            _pointArray[0] = Map.ImageToWorld(e.Location);
                            _pointArray[1] = Map.ImageToWorld(e.Location);
                        }
                        else
                        {
                            EasyMap.Geometries.Point[] temp = new EasyMap.Geometries.Point[_pointArray.GetUpperBound(0) + 2];
                            for (int i = 0; i <= _pointArray.GetUpperBound(0); i++)
                                temp[i] = _pointArray[i];

                            temp[temp.GetUpperBound(0)] = Map.ImageToWorld(e.Location);
                            _pointArray = temp;
                        }
                    }
                }


                if (_dragging)
                {
                    _dragging = false;
                    if (_activeTool == Tools.Query)
                        Invalidate(_rectangle);
                    if (_activeTool == Tools.ZoomWindow || _activeTool == Tools.Query)
                        _rectangle = Rectangle.Empty;

                    Refresh();
                }
                else if (_activeTool == Tools.ZoomIn || _activeTool == Tools.ZoomOut || _activeTool == Tools.Pan)
                {
                    Refresh();
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (_activeTool == Tools.DrawPolygon)
            {
                if (GeometryDefined != null)
                {
                    Geometries.LinearRing extRing = new Geometries.LinearRing();
                    for (int i = 0; i < _pointArray.GetUpperBound(0); i++)
                        extRing.Vertices.Add(_pointArray[i]);

                    extRing.Vertices.Add(_pointArray[0]);

                    GeometryDefined(new Geometries.Polygon(extRing));
                }
                ActiveTool = Tools.None;
            }
            else if (_activeTool == Tools.DrawLine)
            {
                if (GeometryDefined != null)
                {
                    Geometries.LineString line = new Geometries.LineString();
                    for (int i = 0; i <= _pointArray.GetUpperBound(0); i++)
                        line.Vertices.Add(_pointArray[i]);

                    GeometryDefined(line);
                }
                ActiveTool = Tools.None;
            }
        }


        private static void GetBounds(GeoPoint p1, GeoPoint p2,
            out GeoPoint lowerLeft, out GeoPoint upperRight)
        {
            lowerLeft = new GeoPoint(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            upperRight = new GeoPoint(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));

            Debug.WriteLine("p1: " + p1);
            Debug.WriteLine("p2: " + p2);
            Debug.WriteLine("lowerLeft: " + lowerLeft);
            Debug.WriteLine("upperRight: " + upperRight);
        }

        class MouseWheelGrabber : IMessageFilter
        {
            [DllImport("user32.dll")]
            public static extern IntPtr WindowFromPoint(Point lpPoint);
            [DllImport("user32.dll")]
            public static extern bool GetCursorPos(out Point lpPoint);

            public static IntPtr GetWindowUnderCursor()
            {
                Point ptCursor = new Point();

                if (!(GetCursorPos(out ptCursor)))
                    return IntPtr.Zero;

                return WindowFromPoint(ptCursor);
            }

            private MapBox redirectHandle = null;
            public MouseWheelGrabber(MapBox redirectHandle)
            {
                this.redirectHandle = redirectHandle;
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x020A)
                {
                    int delta = ((int)(m.WParam.ToInt64() & 0xFFFF0000) >> 16);
                    Point pt = redirectHandle.PointToClient(new Point(m.LParam.ToInt32()));
                    if (redirectHandle.ClientRectangle.Contains(pt))
                    {
                        IntPtr hWnd = GetWindowUnderCursor();
                        if (hWnd == redirectHandle.Handle)
                        {
                            redirectHandle.OnMouseWheel(new MouseEventArgs(System.Windows.Forms.MouseButtons.Middle, 0, pt.X, pt.Y, delta));
                            return true;
                        }
                    }

                }

                return false;
            }

        }
    }



#if EnableMetafileClipboardSupport
    public class ClipboardMetafileHelper
    {
        [DllImport("user32.dll")]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("user32.dll")]
        static extern bool CloseClipboard();

        [DllImport("gdi32.dll")]
        static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNull);

        [DllImport("gdi32.dll")]
        static extern bool DeleteEnhMetaFile(IntPtr hemf);

        /*
        [DllImport("gdi32")]
        static extern int GetEnhMetaFileBits(int hemf, int cbBuffer, byte[] lpbBuffer);
        */
        static public bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf)
        {
            bool bResult = false;
            IntPtr hEmf = mf.GetHenhmetafile();

            if (!hEmf.Equals(new IntPtr(0)))
            {
                IntPtr hEmf2 = CopyEnhMetaFile(hEmf, new IntPtr(0));
                if (!hEmf2.Equals(new IntPtr(0)))
                {
                    if (OpenClipboard(hWnd))
                    {
                        if (EmptyClipboard())
                        {
                            IntPtr hRes = SetClipboardData(14 /*CF_ENHMETAFILE*/, hEmf2);
                            bResult = hRes.Equals(hEmf2);
                            CloseClipboard();
                        }
                    }
                }
                DeleteEnhMetaFile(hEmf);
            }
            return bResult;
        }
    }
#endif
}
#endif
