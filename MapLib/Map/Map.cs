

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using EasyMap.Geometries;
using EasyMap.Layers;
using EasyMap.Rendering;
using EasyMap.Rendering.Decoration;
using EasyMap.Utilities;
using Point = EasyMap.Geometries.Point;
using System.Drawing.Imaging;
using EasyMap.Data.Providers;
using System.Collections.ObjectModel;
using EsayMap;


namespace EsayMap
{

    public enum SELECTION_TYPE
    {
        NONE,
        RECTANGLE,
        CIRCLE_RADIO,
        CIRCLE,
        POLYGON,
        CIRCLETEMP,
        PROBLEMPOINT,
        PROBLEMAREA
    }
}

namespace EasyMap
{
    public class Map : IDisposable
    {
        public static NumberFormatInfo NumberFormatEnUs = new CultureInfo("en-US", false).NumberFormat;

        public bool DisposeLayersOnDispose = true;
        private ILayer _CurrentLayer = null;
        private string _MapName = "";
        private string _Comment = "";
        private decimal _MapId = 0;

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        public string MapName
        {
            get { return _MapName; }
            set { _MapName = value; }
        }

        public ILayer CurrentLayer
        {
            get { return _CurrentLayer; }
            set { _CurrentLayer = value; }
        }

        public Map()
            : this(new Size(640, 480))
        {
        }

        public Map(Size size)
        {
            Size = size;
            _Layers = new LayerCollection();
            _backgroundLayers = new LayerCollection();
            _backgroundLayers.ListChanged += _Layers_ListChanged;
            _variableLayers = new VariableLayerCollection(_Layers);
            BackColor = Color.Transparent;
            _MaximumZoom = double.MaxValue;
            _MinimumZoom = 0;
            _MapTransform = new Matrix();
            MapTransformInverted = new Matrix();
            _Center = new Point(0, 0);
            _Zoom = 1;
            _PixelAspectRatio = 1.0;
        }

        void _Layers_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (e.ListChangedType == System.ComponentModel.ListChangedType.ItemAdded)
            {

                ILayer l = _backgroundLayers[e.NewIndex];
                if (l is ITileAsyncLayer)
                {
                    ((ITileAsyncLayer)l).MapNewTileAvaliable += MapNewTileAvaliableHandler;
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (DisposeLayersOnDispose)
            {
                if (Layers != null)
                {
                    foreach (Layer layer in Layers)
                        if (layer is IDisposable)
                            ((IDisposable)layer).Dispose();
                }
                if (BackgroundLayer != null)
                {
                    foreach (Layer layer in BackgroundLayer)
                        if (layer is IDisposable)
                            ((IDisposable)layer).Dispose();
                }
                if (VariableLayers != null)
                {
                    foreach (Layer layer in VariableLayers)
                        if (layer is IDisposable)
                            ((IDisposable)layer).Dispose();
                }
            }
            Layers.Clear();
        }

        #endregion

        #region Events

        #region Delegates

        public delegate void LayersChangedEventHandler();

        public delegate void MapRenderedEventHandler(Graphics g);

        public delegate void MapRenderingEventHandler(Graphics g);

        public delegate void MapViewChangedHandler();



        #endregion

        [Obsolete("This event is never invoked since it has been made impossible to change the LayerCollection for a map instance.")]
#pragma warning disable 67
        public event LayersChangedEventHandler LayersChanged;
#pragma warning restore 67

        public event MapViewChangedHandler MapViewOnChange;


        public event MapRenderedEventHandler MapRendering;

        public event MapRenderedEventHandler MapRendered;

        public event EventHandler<LayerRenderingEventArgs> LayerRendering;

        public event EventHandler<LayerRenderingEventArgs> LayerRenderedEx;

        [Obsolete("Use LayerRenderedEx")]
        public event EventHandler LayerRendered;

        public event MapNewTileAvaliabledHandler MapNewTileAvaliable;

        #endregion

        #region Methods

        public Image GetMap()
        {
            Image img = new Bitmap(Size.Width, Size.Height);
            Graphics g = Graphics.FromImage(img);
            RenderMap(g);
            g.Dispose();
            return img;
        }

        public Image GetMap(int resolution)
        {
            Image img = new Bitmap(Size.Width, Size.Height);
            ((Bitmap)img).SetResolution(resolution, resolution);
            Graphics g = Graphics.FromImage(img);
            RenderMap(g);
            g.Dispose();
            return img;

        }

        public Metafile GetMapAsMetafile()
        {
            return GetMapAsMetafile(String.Empty);
        }

        public Metafile GetMapAsMetafile(string metafileName)
        {
            Metafile metafile;
            Bitmap bm = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bm))
            {
                IntPtr hdc = g.GetHdc();
                using (MemoryStream stream = new MemoryStream())
                {
                    metafile = new Metafile(stream, hdc, new RectangleF(0, 0, Size.Width, Size.Height),
                                            MetafileFrameUnit.Pixel, EmfType.EmfPlusDual);

                    using (Graphics metafileGraphics = Graphics.FromImage(metafile))
                    {
                        metafileGraphics.PageUnit = GraphicsUnit.Pixel;
                        metafileGraphics.TransformPoints(CoordinateSpace.Page, CoordinateSpace.Device,
                                                         new[] { new PointF(Size.Width, Size.Height) });

                        RenderMap(metafileGraphics);
                    }

                    if (!String.IsNullOrEmpty(metafileName))
                        File.WriteAllBytes(metafileName, stream.ToArray());
                }
                g.ReleaseHdc(hdc);
            }
            return metafile;
        }

        public void MapNewTileAvaliableHandler(TileLayer sender, BoundingBox bbox, Bitmap bm, int sourceWidth, int sourceHeight, ImageAttributes imageAttributes)
        {
            var e = MapNewTileAvaliable;
            if (e != null)
                e(sender, bbox, bm, sourceWidth, sourceHeight, imageAttributes);
        }

        public void RenderMap(Graphics g)
        {
            OnMapRendering(g);

            if (g == null)
                throw new ArgumentNullException("g", "Cannot render map with null graphics object!");

            VariableLayerCollection.Pause = true;

            if ((Layers == null || Layers.Count == 0) && (BackgroundLayer == null || BackgroundLayer.Count == 0) && (_variableLayers == null || _variableLayers.Count == 0))
                throw new InvalidOperationException("No layers to render");

            lock (MapTransform)
            {
                g.Transform = MapTransform.Clone();
            }
            g.Clear(BackColor);
            g.PageUnit = GraphicsUnit.Pixel;


            ILayer[] layerList;
            if (_backgroundLayers != null && _backgroundLayers.Count > 0)
            {
                layerList = new ILayer[_backgroundLayers.Count];
                _backgroundLayers.CopyTo(layerList, 0);
                foreach (ILayer layer in layerList)
                {
                    OnLayerRendering(layer, LayerCollectionType.Background);
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this,RenderType.Symbol);
                    OnLayerRendered(layer, LayerCollectionType.Background);
                }
                foreach (ILayer layer in layerList)
                {
                    OnLayerRendering(layer, LayerCollectionType.Background);
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Text);
                    OnLayerRendered(layer, LayerCollectionType.Background);
                }
                foreach (ILayer layer in layerList)
                {
                    OnLayerRendering(layer, LayerCollectionType.Background);
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Select);
                    OnLayerRendered(layer, LayerCollectionType.Background);
                }
            }

            if (_Layers != null && _Layers.Count > 0)
            {
                layerList = new ILayer[_Layers.Count];
                _Layers.CopyTo(layerList, 0);

                foreach (ILayer layer in layerList)
                {
                    OnLayerRendering(layer, LayerCollectionType.Static);
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Symbol);
                    OnLayerRendered(layer, LayerCollectionType.Static);
                }
                foreach (ILayer layer in layerList)
                {
                    OnLayerRendering(layer, LayerCollectionType.Static);
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Text);
                    OnLayerRendered(layer, LayerCollectionType.Static);
                }
                foreach (ILayer layer in layerList)
                {
                    OnLayerRendering(layer, LayerCollectionType.Static);
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Select);
                    OnLayerRendered(layer, LayerCollectionType.Static);
                }
            }

            if (_variableLayers != null && _variableLayers.Count > 0)
            {
                layerList = new ILayer[_variableLayers.Count];
                _variableLayers.CopyTo(layerList, 0);
                foreach (ILayer layer in layerList)
                {
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Symbol);
                }
                foreach (ILayer layer in layerList)
                {
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Text);
                }
                foreach (ILayer layer in layerList)
                {
                    if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                        layer.Render(g, this, RenderType.Select);
                }
            }

            RenderDisclaimer(g);

            foreach (var mapDecoration in _decorations)
            {
                mapDecoration.Render(g, this);
            }
            VariableLayerCollection.Pause = false;

            OnMapRendered(g);
        }

        protected virtual void OnMapRendering(Graphics g)
        {
            var e = MapRendering;
            if (e != null) e(g);
        }
        protected virtual void OnMapRendered(Graphics g)
        {
            var e = MapRendered;
            if (e != null) e(g); //Fire render event
        }

        protected virtual void OnLayerRendering(ILayer layer, LayerCollectionType layerCollectionType)
        {
            var e = LayerRendering;
            if (e != null) e(this, new LayerRenderingEventArgs(layer, layerCollectionType));
        }

        protected virtual void OnLayerRendered(ILayer layer, LayerCollectionType layerCollectionType)
        {
#pragma warning disable 612,618
            var e = LayerRendered;
#pragma warning restore 612,618
            if (e != null) e(this, EventArgs.Empty);

            var eex = LayerRenderedEx;
            if (eex != null) eex(this, new LayerRenderingEventArgs(layer, layerCollectionType));
        }


        public void RenderMap(Graphics g, LayerCollectionType layerCollectionType)
        {
            RenderMap(g, layerCollectionType, true);
        }

        public void RenderMap(Graphics g, LayerCollectionType layerCollectionType, bool drawMapDecorations)
        {
            if (g == null)
                throw new ArgumentNullException("g", "Cannot render map with null graphics object!");

            VariableLayerCollection.Pause = true;

            LayerCollection lc = null;
            switch (layerCollectionType)
            {
                case LayerCollectionType.Static:
                    lc = Layers;
                    break;
                case LayerCollectionType.Variable:
                    lc = VariableLayers;
                    break;
                case LayerCollectionType.Background:
                    lc = BackgroundLayer;
                    break;
            }

            if (lc == null || lc.Count == 0)
                throw new InvalidOperationException("No layers to render");

            Matrix transform = g.Transform;
            lock (MapTransform)
            {
                g.Transform = MapTransform.Clone();
            }
            //g.Clear(BackColor);
            g.PageUnit = GraphicsUnit.Pixel;


            ILayer[] layerList = new ILayer[lc.Count];
            lc.CopyTo(layerList, 0);
            foreach (ILayer layer in layerList)
            {
                if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                    layer.Render(g, this,RenderType.Symbol);
            }
            foreach (ILayer layer in layerList)
            {
                if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                    layer.Render(g, this, RenderType.Select);
            }
            foreach (ILayer layer in layerList)
            {
                if (layer.Enabled && layer.MaxVisible >= Zoom && layer.MinVisible < Zoom)
                    layer.Render(g, this,RenderType.Text);
            }

            g.Transform = transform;

            if (layerCollectionType == LayerCollectionType.Static)
            {
                RenderDisclaimer(g);
                if (drawMapDecorations)
                {
                    foreach (var mapDecoration in Decorations)
                    {
                        mapDecoration.Render(g, this);
                    }
                }
            }

            VariableLayerCollection.Pause = false;

        }

        public Map Clone()
        {
            Map clone = null;
            lock (MapTransform)
            {
                clone = new Map()
                {
                    BackColor = BackColor,
                    Center = Center,
                    Disclaimer = Disclaimer,
                    DisclaimerFont = DisclaimerFont,
                    DisclaimerLocation = DisclaimerLocation,
                    MapTransform = MapTransform.Clone(),
                    MaximumZoom = MaximumZoom,
                    MinimumZoom = MinimumZoom,
                    PixelAspectRatio = PixelAspectRatio,
                    Size = Size,
                    Zoom = Zoom,
                    DisposeLayersOnDispose = false
                };

            }
            if (clone != null)
            {
                foreach (var lay in BackgroundLayer)
                    clone.BackgroundLayer.Add(lay);
                foreach (var dec in Decorations)
                    clone.Decorations.Add(dec);
                foreach (var lay in Layers)
                    clone.Layers.Add(lay);
                foreach (var lay in VariableLayers)
                    clone.VariableLayers.Add(lay);
            }
            return clone;
        }

        private void RenderDisclaimer(Graphics g)
        {

            StringFormat sf;
            if (!String.IsNullOrEmpty(_disclaimer))
            {
                SizeF size = VectorRenderer.SizeOfString(g, _disclaimer, _disclaimerFont);
                size.Width = (Single)Math.Ceiling(size.Width);
                size.Height = (Single)Math.Ceiling(size.Height);
                switch (DisclaimerLocation)
                {
                    case 0: //Right-Bottom
                        sf = new StringFormat();
                        sf.Alignment = StringAlignment.Far;
                        g.DrawString(Disclaimer, DisclaimerFont, Brushes.Black,
                            g.VisibleClipBounds.Width,
                            g.VisibleClipBounds.Height - size.Height - 2, sf);
                        break;
                    case 1: //Right-Top
                        sf = new StringFormat();
                        sf.Alignment = StringAlignment.Far;
                        g.DrawString(Disclaimer, DisclaimerFont, Brushes.Black,
                            g.VisibleClipBounds.Width, 0f, sf);
                        break;
                    case 2: //Left-Top
                        g.DrawString(Disclaimer, DisclaimerFont, Brushes.Black, 0f, 0f);
                        break;
                    case 3://Left-Bottom
                        g.DrawString(Disclaimer, DisclaimerFont, Brushes.Black, 0f,
                            g.VisibleClipBounds.Height - size.Height - 2);
                        break;
                }
            }
        }

        public IEnumerable<ILayer> FindLayer(string layername)
        {
            foreach (ILayer l in Layers)
                if (l.LayerName.Contains(layername))
                    yield return l;
        }

        public ILayer GetLayerByName(string name)
        {
            for (int i = 0; i < _Layers.Count; i++)
                if (String.Equals(_Layers[i].LayerName, name, StringComparison.InvariantCultureIgnoreCase))
                    return _Layers[i];

            return null;
        }

        public void ZoomToExtents()
        {
            ZoomToBox(GetExtents());
        }

        public void ZoomToBox(BoundingBox bbox)
        {
            if (bbox != null && bbox.IsValid)
            {

                _Zoom = bbox.Width; //Set the private center value so we only fire one MapOnViewChange event
                if (Envelope.Height < bbox.Height)
                    _Zoom *= bbox.Height / Envelope.Height;
                if (_Zoom < _MinimumZoom)
                    _Zoom = _MinimumZoom;
                if (_Zoom > _MaximumZoom)
                    _Zoom = _MaximumZoom;
                Center = bbox.GetCentroid();

            }
        }

        public PointF WorldToImage(Point p, bool careAboutMapTransform)
        {
            PointF pTmp = Transform.WorldtoMap(p, this);
            lock (MapTransform)
            {
                if (careAboutMapTransform && !MapTransform.IsIdentity)
                {
                    PointF[] pts = new PointF[] { pTmp };
                    MapTransform.TransformPoints(pts);
                    pTmp = pts[0];
                }
            }
            return pTmp;
        }

        public PointF WorldToImage(Point p)
        {
            return WorldToImage(p, false);
        }

        public Point ImageToWorld(PointF p)
        {
            return ImageToWorld(p, false);
        }
        public Point ImageToWorld(PointF p, bool careAboutMapTransform)
        {
            lock (MapTransform)
            {
                if (careAboutMapTransform && !MapTransform.IsIdentity)
                {
                    PointF[] pts = new PointF[] { p };
                    MapTransformInverted.TransformPoints(pts);
                    p = pts[0];
                }
            }
            return Transform.MapToWorld(p, this);
        }

        #endregion

        #region Properties

        private readonly List<IMapDecoration> _decorations = new List<IMapDecoration>();

        private Color _BackgroundColor;
        private Point _Center;
        private readonly LayerCollection _Layers;
        private readonly LayerCollection _backgroundLayers;
        private readonly VariableLayerCollection _variableLayers;
        private Matrix _MapTransform;
        private double _MaximumZoom;
        private double _MinimumZoom;
        private double _PixelAspectRatio = 1.0;
        private Size _Size;
        private double _Zoom;
        internal Matrix MapTransformInverted;

        public IList<IMapDecoration> Decorations
        {
            get { return _decorations; }
        }

        public BoundingBox Envelope
        {
            get
            {
                if (double.IsNaN(MapHeight) || double.IsInfinity(MapHeight))
                    return new BoundingBox(0, 0, 0, 0);

                Point ll = new Point(Center.X - Zoom * .5, Center.Y - MapHeight * .5);
                Point ur = new Point(Center.X + Zoom * .5, Center.Y + MapHeight * .5);
                PointF ptfll = WorldToImage(ll, true);
                ptfll = new PointF(Math.Abs(ptfll.X), Math.Abs(Size.Height - ptfll.Y));
                if (!ptfll.IsEmpty)
                {
                    ll.X = ll.X - ptfll.X * PixelWidth;
                    ll.Y = ll.Y - ptfll.Y * PixelHeight;
                    ur.X = ur.X + ptfll.X * PixelWidth;
                    ur.Y = ur.Y + ptfll.Y * PixelHeight;
                }
                return new BoundingBox(ll, ur);

            }
        }

        public Matrix MapTransform
        {
            get { return _MapTransform; }
            set
            {
                _MapTransform = value;
                if (_MapTransform.IsInvertible)
                {
                    MapTransformInverted = value.Clone();
                    MapTransformInverted.Invert();
                }
                else
                    MapTransformInverted.Reset();
            }
        }

        public LayerCollection Layers
        {
            get { return _Layers; }
        }

        public LayerCollection BackgroundLayer
        {
            get { return _backgroundLayers; }
        }

        public VariableLayerCollection VariableLayers
        {
            get { return _variableLayers; }
        }

        public Color BackColor
        {
            get { return _BackgroundColor; }
            set
            {
                _BackgroundColor = value;
                if (MapViewOnChange != null)
                    MapViewOnChange();
            }
        }

        public Point Center
        {
            get { return _Center; }
            set
            {
                _Center = value;
                if (MapViewOnChange != null)
                    MapViewOnChange();
            }
        }

        public double Zoom
        {
            get { return _Zoom; }
            set
            {
                if (value < _MinimumZoom)
                    _Zoom = _MinimumZoom;
                else if (value > _MaximumZoom)
                    _Zoom = _MaximumZoom;
                else
                    _Zoom = value;
                if (MapViewOnChange != null)
                    MapViewOnChange();
            }
        }

        public double PixelSize
        {
            get { return Zoom / Size.Width; }
        }

        public double PixelWidth
        {
            get { return PixelSize; }
        }

        public double PixelHeight
        {
            get { return PixelSize * _PixelAspectRatio; }
        }

        public double PixelAspectRatio
        {
            get { return _PixelAspectRatio; }
            set
            {
                if (_PixelAspectRatio <= 0)
                    throw new ArgumentException("Invalid Pixel Aspect Ratio");
                _PixelAspectRatio = value;
            }
        }

        public double MapHeight
        {
            get { return (Zoom * Size.Height) / Size.Width * PixelAspectRatio; }
        }

        public Size Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public double MinimumZoom
        {
            get { return _MinimumZoom; }
            set
            {
                if (value < 0)
                    throw (new ArgumentException("Minimum zoom must be 0 or more"));
                _MinimumZoom = value;
            }
        }

        public double MaximumZoom
        {
            get { return _MaximumZoom; }
            set
            {
                if (value <= 0)
                    throw (new ArgumentException("Maximum zoom must larger than 0"));
                _MaximumZoom = value;
            }
        }

        public BoundingBox GetExtents()
        {
            if ((Layers == null || Layers.Count == 0) &&
                (VariableLayers == null || VariableLayers.Count == 0) &&
                (BackgroundLayer == null || BackgroundLayer.Count == 0))
                throw (new InvalidOperationException("No layers to zoom to"));

            BoundingBox bbox = null;

            ExtendBoxForCollection(Layers, ref bbox);
            ExtendBoxForCollection(VariableLayers, ref bbox);
            ExtendBoxForCollection(BackgroundLayer, ref bbox);

            return bbox;
        }

        private static void ExtendBoxForCollection(LayerCollection layersCollection, ref BoundingBox bbox)
        {
            foreach (ILayer l in layersCollection)
            {

                BoundingBox bb;
                try
                {
                    bb = l.Envelope;
                }
                catch (Exception)
                {
                    bb = new BoundingBox(-20037508.342789, -20037508.342789, 20037508.342789, 20037508.342789);
                }

                if (bb != null)
                    bbox = bbox == null ? bb : bbox.Join(bb);

            }
        }

        #endregion

        #region Disclaimer

        private String _disclaimer;
        public String Disclaimer
        {
            get { return _disclaimer; }
            set
            {
                if (String.IsNullOrEmpty(_disclaimer))
                {
                    _disclaimer = value;
                    if (_disclaimerFont == null)
                        _disclaimerFont = new Font(FontFamily.GenericSansSerif, 8f);
                }
            }
        }

        private Font _disclaimerFont;
        public Font DisclaimerFont
        {
            get { return _disclaimerFont; }
            set
            {
                if (value == null) return;
                _disclaimerFont = value;
            }
        }

        private Int32 _disclaimerLocation;
        public Int32 DisclaimerLocation
        {
            get { return _disclaimerLocation; }
            set { _disclaimerLocation = value % 4; }
        }

        #endregion





        public void AddLayer(ILayer layer)
        {
            int index = 0;
            while (index < _Layers.Count && _Layers[index].Type == layer.Type)
            {
                index++;
            }
            if (index > 0 && _Layers[index - 1].Type == layer.Type)
            {
                if (index + 1 >= _Layers.Count)
                {
                    _Layers.Add(layer);
                }
                else
                {
                    _Layers.Insert(index + 1, layer);
                }
            }
            else
            {
                if (layer.Type == VectorLayer.LayerType.PhotoLayer)
                {
                    _Layers.Insert(0, layer);
                }
                else if (layer.Type == VectorLayer.LayerType.BaseLayer)
                {
                    index = 0;
                    while (index < _Layers.Count && _Layers[index].Type == VectorLayer.LayerType.PhotoLayer)
                    {
                        index++;
                    }
                    if (index + 1 >= _Layers.Count)
                    {
                        _Layers.Add(layer);
                    }
                    else
                    {
                        _Layers.Insert(index + 1, layer);
                    }
                }
                else
                {
                    _Layers.Add(layer);
                }
            }
        }

        public void MoveLayer(ILayer srclayer, ILayer destlayer)
        {
            int index = 0;
            _Layers.Remove(srclayer);
            foreach (ILayer layer in _Layers)
            {
                if (layer == destlayer)
                {
                    break;
                }
                index++;
            }
            _Layers.Insert(index, srclayer);

        }

        /// <summary>
        /// 按照指定的区域检索视图范围内在指定区域内的元素
        /// </summary>
        /// <param name="windowSize">视图窗口尺寸</param>
        /// <param name="area">指定区域</param>
        /// <param name="selecttype">选择方式</param>
        /// <param name="r">圆半径</param>
        /// <param name="include">包含方式</param>
        /// <param name="allow_select_not_edit">是否包含非编辑图层</param>
        /// <returns></returns>
        public List<object> PickUpObject(Size windowSize,Polygon area, SELECTION_TYPE selecttype, double r,bool include,bool allow_select_not_edit)
        {
            if (selecttype == SELECTION_TYPE.CIRCLE||selecttype==SELECTION_TYPE.CIRCLE_RADIO)
            {
                return GetSelectObjectInCircle(windowSize, area.ExteriorRing.Vertices[0], r, include, allow_select_not_edit);
            }
            else if (selecttype == SELECTION_TYPE.RECTANGLE || selecttype == SELECTION_TYPE.POLYGON)
            {
                return GetSelectObjectInPolygon(windowSize, area, include, allow_select_not_edit);
            }
            else if (selecttype == SELECTION_TYPE.CIRCLETEMP)
            {
                return GetSelectObjectInPoint(windowSize, area.ExteriorRing.Vertices[0], r, include, allow_select_not_edit);
            }
            return null;
        }
        public List<object> PickUpObject1(Size windowSize, Point area, SELECTION_TYPE selecttype, double r, bool include, bool allow_select_not_edit)
        {
            if (selecttype == SELECTION_TYPE.CIRCLETEMP)
            {
                return GetSelectObjectInPoint(windowSize, area, r, include, allow_select_not_edit);
            }
            if (selecttype == SELECTION_TYPE.PROBLEMPOINT)
            {
                return GetSelectObjectInPoint(windowSize, area, r, include, allow_select_not_edit);
            }
            return null;
        }
        public List<object> GetSelectObjectInCircle(Size windowSize, Point center, double r, bool include, bool allow_select_not_edit)
        {
            List<object> ret = new List<object>();
            foreach (ILayer layer in Layers)
            {
                if (layer == null || !(layer is VectorLayer) || !layer.Enabled || this.Zoom < layer.MinVisible || this.Zoom > layer.MaxVisible || (!allow_select_not_edit && !layer.AllowEdit))
                {
                    continue;
                }

                VectorLayer vlayer = layer as VectorLayer;
                //取得图层数据源
                GeometryProvider provider = (GeometryProvider)vlayer.DataSource;
                Point p1 = ImageToWorld(new PointF(0, 0));
                Point p2 = ImageToWorld(new PointF(windowSize.Width, windowSize.Height));
                BoundingBox box = new BoundingBox(p1, p2);
                Collection<Geometry> geoms = provider.GetGeometriesInView(box);
                //循环图层中的多边形
                foreach (Geometry geom in geoms)
                {
                    if (CheckObjectInCircle(center, r, geom, include))
                    {
                        ret.Add(layer);
                        ret.Add(geom);
                    }
                }
            }
            return ret;
        }

        public List<object> GetSelectObjectInPoint(Size windowSize, Point center, double r, bool include, bool allow_select_not_edit)
        {
            List<object> ret = new List<object>();
            foreach (ILayer layer in Layers)
            {
                if (layer == null || !(layer is VectorLayer) || !layer.Enabled || this.Zoom < layer.MinVisible || this.Zoom > layer.MaxVisible || (!allow_select_not_edit && !layer.AllowEdit))
                {
                    continue;
                }

                VectorLayer vlayer = layer as VectorLayer;
                //取得图层数据源
                GeometryProvider provider = (GeometryProvider)vlayer.DataSource;
                Point p1 = ImageToWorld(new PointF(0, 0));
                Point p2 = ImageToWorld(new PointF(windowSize.Width, windowSize.Height));
                BoundingBox box = new BoundingBox(p1, p2);
                Collection<Geometry> geoms = provider.GetGeometriesInView(box);
                //循环图层中的多边形
                foreach (Geometry geom in geoms)
                {
                    if (CheckObjectInCircle(center, r, geom, include))
                    {
                        ret.Add(layer);
                        ret.Add(geom);
                    }
                }
            }
            return ret;
        }

        private double GetLength(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private bool CheckObjectInCircle(Point center, double r,Geometry geom,bool include)
        {
            if (geom is Polygon)
            {
                Polygon polygon = (Polygon)geom;
                if (include)
                {
                    foreach (Point point in polygon.ExteriorRing.Vertices)
                    {
                        if (GetLength(point, center) >= r)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (Point point in polygon.ExteriorRing.Vertices)
                    {
                        if (GetLength(point, center) < r)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            else if (geom is MultiPolygon)
            {
                MultiPolygon polygons = (MultiPolygon)geom;
                if (include)
                {
                    foreach (Polygon polygon in polygons.Polygons)
                    {
                        foreach (Point point in polygon.ExteriorRing.Vertices)
                        {
                            if (GetLength(point, center) >= r)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (Polygon polygon in polygons.Polygons)
                    {
                        foreach (Point point in polygon.ExteriorRing.Vertices)
                        {
                            if (GetLength(point, center) < r)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            else if (geom is Point)
            {
                Point p=geom as Point;
                return GetLength(p, center) < r;
            }
            else if (geom is LineString)
            {
                LineString line = geom as LineString;
                if (include)
                {
                    foreach (Point point in line.Vertices)
                    {
                        if (GetLength(point, center) >= r)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (Point point in line.Vertices)
                    {
                        if (GetLength(point, center) < r)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            else if (geom is LinearRing)
            {
                LinearRing line = geom as LinearRing;
                if (include)
                {
                    foreach (Point point in line.Vertices)
                    {
                        if (GetLength(point, center) >= r)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (Point point in line.Vertices)
                    {
                        if (GetLength(point, center) < r)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            else if (geom is MultiLineString)
            {
                MultiLineString lines = geom as MultiLineString;

                if (include)
                {
                    foreach (LineString line in lines.LineStrings)
                    {
                        foreach (Point point in line.Vertices)
                        {
                            if (GetLength(point, center) >= r)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (LineString line in lines.LineStrings)
                    {
                        foreach (Point point in line.Vertices)
                        {
                            if (GetLength(point, center) < r)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
            }
            return false;
        }

        private List<object> GetSelectObjectInPolygon(Size windowSize, Polygon area, bool include, bool allow_select_not_edit)
        {
            List<object> ret = new List<object>();
            foreach (ILayer layer in Layers)
            {
                if (layer == null || !(layer is VectorLayer) || !layer.Enabled || this.Zoom < layer.MinVisible || this.Zoom > layer.MaxVisible || (!allow_select_not_edit && !layer.AllowEdit))
                {
                    continue;
                }

                VectorLayer vlayer = layer as VectorLayer;
                //取得图层数据源
                GeometryProvider provider = (GeometryProvider)vlayer.DataSource;
                Point p1 = ImageToWorld(new PointF(0, 0));
                Point p2 = ImageToWorld(new PointF(windowSize.Width, windowSize.Height));
                BoundingBox box = new BoundingBox(p1, p2);
                ICollection<Geometry> geoms = provider.GetGeometriesInView(box);
                if (windowSize == null)
                {
                    geoms = provider.Geometries;
                }
                else
                {
                    p1 = ImageToWorld(new PointF(0, 0));
                    p2 = ImageToWorld(new PointF(windowSize.Width, windowSize.Height));
                    box = new BoundingBox(p1, p2);
                    geoms = provider.GetGeometriesInView(box);
                }
                //循环图层中的多边形
                foreach (Geometry geom in geoms)
                {
                    bool find = false;
                    if (geom is Polygon)
                    {
                        Polygon polygon = (Polygon)geom;
                        if (include)
                        {
                            find = true;
                            foreach (Point point in polygon.ExteriorRing.Vertices)
                            {
                                if (!area.InPoly(point))
                                {
                                    find = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            find = false;
                            foreach (Point point in polygon.ExteriorRing.Vertices)
                            {
                                if (area.InPoly(point))
                                {
                                    find = true;
                                }
                            }
                        }
                    }
                    else if (geom is MultiPolygon)
                    {
                        MultiPolygon polygons = (MultiPolygon)geom;
                        if (include)
                        {
                            find = true;
                            foreach (Polygon polygon in polygons.Polygons)
                            {
                                foreach (Point point in polygon.ExteriorRing.Vertices)
                                {
                                    if (!area.InPoly(point))
                                    {
                                        find = false;
                                        break;
                                    }
                                }
                                if (!find)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            find = false;
                            foreach (Polygon polygon in polygons.Polygons)
                            {
                                foreach (Point point in polygon.ExteriorRing.Vertices)
                                {
                                    if (area.InPoly(point))
                                    {
                                        find = true;
                                        break;
                                    }
                                }
                                if (find)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (geom is Point)
                    {
                        find = area.InPoly(geom as Point);
                    }
                    else if (geom is LineString)
                    {
                        LineString line = geom as LineString;
                        if (include)
                        {
                            find = true;
                            foreach (Point point in line.Vertices)
                            {
                                if (!area.InPoly(point))
                                {
                                    find = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            find = false;
                            foreach (Point point in line.Vertices)
                            {
                                if (area.InPoly(point))
                                {
                                    find = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if (geom is LinearRing)
                    {
                        LinearRing line = geom as LinearRing;
                        if (include)
                        {
                            find = true;
                            foreach (Point point in line.Vertices)
                            {
                                if (!area.InPoly(point))
                                {
                                    find = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            find = false;
                            foreach (Point point in line.Vertices)
                            {
                                if (area.InPoly(point))
                                {
                                    find = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if (geom is MultiLineString)
                    {
                        MultiLineString lines = geom as MultiLineString;

                        if (include)
                        {
                            find = true;
                            foreach (LineString line in lines.LineStrings)
                            {
                                foreach (Point point in line.Vertices)
                                {
                                    if (area.InPoly(point))
                                    {
                                        find = false;
                                        break;
                                    }
                                }
                                if (!find)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            find = false;
                            foreach (LineString line in lines.LineStrings)
                            {
                                foreach (Point point in line.Vertices)
                                {
                                    if (area.InPoly(point))
                                    {
                                        find = true;
                                        break;
                                    }
                                }
                                if (find)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (find)
                    {
                        ret.Add(layer);
                        ret.Add(geom);
                    }
                }
            }
            return ret;
        }

        public List<object> PickUpObject(System.Drawing.Point ImagePos, Size windowSize, Size pointFlg)
        {
            EasyMap.Geometries.Point WorldPos = ImageToWorld(new System.Drawing.Point(ImagePos.X, ImagePos.Y), true);
            bool find = false;
            VectorLayer findlayer = null;
            Geometry findobject = null;
            List<object> ret = new List<object>();
            VectorLayer layer = null;
            List<Geometry> findlist = new List<Geometry>();
            List<VectorLayer> findlayers = new List<VectorLayer>();
            for (int j = 0; j < 3; j++)
            {
                LayerCollection Layers_new = new LayerCollection();
                for (int i = 0; i < Layers.Count; i++)
                {
                    if (Layers[i].LayerName != null)
                    {
                        if (Layers[i].LayerName.IndexOf("土地") >= 0 
                            || Layers[i].LayerName.IndexOf("区界") >= 0 || Layers[i].LayerName.IndexOf("街道") >= 0
                            || Layers[i].LayerName.IndexOf("注记") >= 0)
                        {
                            Layers_new.Add(Layers[i]);
                        }
                    }
                }
                //for (int i = 0; i < Layers_new.Count; i++)
                for (int i = Layers_new.Count-1; i >-1; i--)
                {
                    if (!(Layers[i] is VectorLayer))
                    {
                        continue;
                    }
                    //取得当前活动图层
                    //VectorLayer layer = (VectorLayer)GetCurrentLayer();
                    layer = Layers_new[i] as VectorLayer;
                    if (layer == null)
                    {
                        continue;
                    }
                    if (!layer.Enabled)
                    {
                        continue;
                    }
                    if (layer.MaxVisible < this.Zoom || layer.MinVisible > this.Zoom)
                    {
                        continue;
                    }
                    PointF pp1 = new PointF(1, 1);
                    PointF pp2 = new PointF(5 + layer.Style.Outline.Width, 5 + layer.Style.Outline.Width);
                    EasyMap.Geometries.Point p11 = ImageToWorld(pp1);
                    EasyMap.Geometries.Point p12 = ImageToWorld(pp2);
                    Environment.SetEnvironmentVariable("STEP", (p12.X - p11.X).ToString());
                    //取得图层数据源
                    GeometryProvider provider = (GeometryProvider)layer.DataSource;
                    EasyMap.Geometries.Point p1 = ImageToWorld(new PointF(0, 0));
                    EasyMap.Geometries.Point p2 = ImageToWorld(new PointF(windowSize.Width, windowSize.Height));
                    BoundingBox box = new BoundingBox(p1, p2);
                    Collection<Geometry> geoms = provider.GetGeometriesInView(box);
                    //循环图层中的多边形
                    foreach (Geometry geom in geoms)
                    {
                        if (geom is Polygon && j == 2)
                        {
                            Polygon polygon = (Polygon)geom;
                            if (polygon.InPoly(WorldPos))
                            {
                                findlist.Add(geom);
                                findlayers.Add(layer);
                                //SelectObject(geom);
                                //findobject = geom;
                                //findlayer = layer;
                                find = true;
                                continue;
                            }
                        }
                        else if (geom is MultiPolygon && j == 2)
                        {
                            MultiPolygon polygons = (MultiPolygon)geom;
                            if (polygons.InPoly(WorldPos))
                            {
                                findlist.Add(geom);
                                findlayers.Add(layer);
                                //SelectObject(geom);
                                //findobject = geom;
                                //findlayer = layer;
                                find = true;
                                continue;
                            }
                        }
                        else if (geom is EasyMap.Geometries.Point && j == 0)
                        {
                            EasyMap.Geometries.Point point = (EasyMap.Geometries.Point)geom;
                            PointF p = WorldToImage(point);
                            float x = p.X - pointFlg.Width / 2;
                            float y = p.Y - pointFlg.Height / 2;
                            float x2 = x + pointFlg.Width;
                            float y2 = y + pointFlg.Height;
                            if (x < ImagePos.X && x2 > ImagePos.X && y < ImagePos.Y && y2 > ImagePos.Y)
                            {
                                findobject = geom;
                                findlayer = layer;
                                find = true;
                                break;
                            }
                        }
                        else if (geom is LineString && j == 1)
                        {
                            LineString line = geom as LineString;
                            if (line.IsSelect(WorldPos))
                            {
                                findobject = geom;
                                findlayer = layer;
                                find = true;
                                break;
                            }
                        }
                        else if (geom is LinearRing && j == 1)
                        {
                            LinearRing line = geom as LinearRing;
                            if (line.IsSelect(WorldPos))
                            {
                                findobject = geom;
                                findlayer = layer;
                                find = true;
                                break;
                            }
                        }
                        else if (geom is MultiLineString && j == 1)
                        {
                            MultiLineString line = geom as MultiLineString;
                            if (line.IsSelect(WorldPos))
                            {
                                findobject = geom;
                                findlayer = layer;
                                find = true;
                                break;
                            }
                        }
                    }
                    if (find && j != 2)
                    {
                        break;
                    }
                }
            }
            if (findobject == null && findlist.Count > 0)
            {
                int index = 0;
                BoundingBox minbox = findlist[0].GetBoundingBox();
                for (int i = 1; i < findlist.Count; i++)
                {
                    if (minbox.Contains(findlist[i].GetBoundingBox()))
                    {
                        minbox = findlist[i].GetBoundingBox();
                        index = i;
                    }
                }
                findobject = findlist[index];
                findlayer = findlayers[index];
            }
            ret.Add(find);
            ret.Add(findobject);
            ret.Add(findlayer);
            return ret;
        }
    }

    public class LayerRenderingEventArgs : EventArgs
    {
        public readonly ILayer Layer;

        public readonly LayerCollectionType LayerCollectionType;

        public LayerRenderingEventArgs(ILayer layer, LayerCollectionType layerCollectionType)
        {
            Layer = layer;
            LayerCollectionType = layerCollectionType;
        }
    }

    
}
