

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
#if !DotSpatialProjections
using ProjNet.CoordinateSystems.Transformations;
#else
using DotSpatial.Projections;
#endif
using EasyMap.Data;
using EasyMap.Data.Providers;
using EasyMap.Geometries;
using EasyMap.Rendering;
using EasyMap.Rendering.Thematics;
using EasyMap.Styles;
using Point = EasyMap.Geometries.Point;
using System.Collections.Generic;
using System.Collections;

namespace EasyMap.Layers
{
    public class VectorLayer : Layer, ICanQueryLayer, IDisposable
    {
        private bool _clippingEnabled;
        private bool _isQueryEnabled = true;
        private IProvider _dataSource;
        private SmoothingMode _smoothingMode;
        private ITheme _theme;
        private LayerType _Type = LayerType.BaseLayer;
        private Hashtable _PriceTable = new Hashtable();
        private Hashtable _GeometryColor = new Hashtable();
        public static double zoom = 0.0;

        public Hashtable GeometryColor
        {
            get { return _GeometryColor; }
            set { _GeometryColor = value; }
        }

        public Hashtable PriceTable
        {
            get { return _PriceTable; }
            set { _PriceTable = value; }
        }

        public LayerType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public enum LayerType : int
        {
            PhotoLayer = 8,
            BaseLayer = 0,
            ReportLayer = 1,
            MotionLayer = 2,
            SaleLayer = 3,
            AreaInformation = 4,
            Pricelayer = 5,
            HireLayer = 6,
            OtherLayer = 7
        }

        public VectorLayer(string layername, LayerType layertype)
            : base(new VectorStyle())
        {
            LayerName = layername;
            SmoothingMode = SmoothingMode.AntiAlias;
            this.Type = layertype;
        }

        public VectorLayer(string layername, IProvider dataSource, LayerType layertype)
            : this(layername, layertype)
        {
            _dataSource = dataSource;
        }
        public Dictionary<string, ITheme> Themes
        {
            get;
            set;
        }



        public ITheme Theme
        {
            get { return _theme; }
            set { _theme = value; }
        }

        public bool ClippingEnabled
        {
            get { return _clippingEnabled; }
            set { _clippingEnabled = value; }
        }

        public SmoothingMode SmoothingMode
        {
            get { return _smoothingMode; }
            set { _smoothingMode = value; }
        }

        public IProvider DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public new VectorStyle Style
        {
            get { return base.Style as VectorStyle; }
            set { base.Style = value; }
        }

        public override BoundingBox Envelope
        {
            get
            {
                if (DataSource == null)
                    return null;
                    //throw (new ApplicationException("DataSource property not set on layer '" + LayerName + "'"));
                BoundingBox box = null;
                lock (_dataSource)
                {
                    bool wasOpen = DataSource.IsOpen;
                    if (!wasOpen)
                        DataSource.Open();
                    box = DataSource.GetExtents();
                    if (!wasOpen) //Restore state
                        DataSource.Close();
                }
                if (CoordinateTransformation != null)
#if !DotSpatialProjections
                {
                    var boxTrans = GeometryTransform.TransformBox(box, CoordinateTransformation.MathTransform);
                    return boxTrans.Intersection(CoordinateTransformation.TargetCS.DefaultEnvelope);
                }

#else
                    return GeometryTransform.TransformBox(box, CoordinateTransformation.Source, CoordinateTransformation.Target);
#endif
                return box;
            }
        }

        public override int SRID
        {
            get
            {
                if (DataSource == null)
                    throw (new ApplicationException("DataSource property not set on layer '" + LayerName + "'"));

                return DataSource.SRID;
            }
            set { DataSource.SRID = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (DataSource != null)
                DataSource.Dispose();
        }

        #endregion

        public override void Render(Graphics g, Map map,RenderType rendertype)
        {
            if (map.Center == null)
                throw (new ApplicationException("Cannot render map. View center not specified"));

            g.SmoothingMode = SmoothingMode;
            BoundingBox envelope = map.Envelope; //View to render
            if (CoordinateTransformation != null)
            {
#if !DotSpatialProjections
                if (ReverseCoordinateTransformation != null)
                {
                    envelope = GeometryTransform.TransformBox(envelope, ReverseCoordinateTransformation.MathTransform);
                }
                else
                {
                    CoordinateTransformation.MathTransform.Invert();
                    envelope = GeometryTransform.TransformBox(envelope, CoordinateTransformation.MathTransform);
                    CoordinateTransformation.MathTransform.Invert();
                }
#else
                envelope = GeometryTransform.TransformBox(envelope, CoordinateTransformation.Target, CoordinateTransformation.Source);
#endif
            }


            if (DataSource == null)
                throw (new ApplicationException("DataSource property not set on layer '" + LayerName + "'"));





            if (Theme != null)
                RenderInternal(g, map, envelope, Theme);
            else
                RenderInternal(g, map, envelope, rendertype);


            base.Render(g, map, rendertype);
        }

        protected void RenderInternal(Graphics g, Map map, BoundingBox envelope, ITheme theme)
        {
            FeatureDataSet ds = new FeatureDataSet();
            lock (_dataSource)
            {
                DataSource.Open();
                DataSource.ExecuteIntersectionQuery(envelope, ds);
                DataSource.Close();
            }

            foreach (FeatureDataTable features in ds.Tables)
            {


                if (CoordinateTransformation != null)
                    for (int i = 0; i < features.Count; i++)
#if !DotSpatialProjections
                        features[i].Geometry = GeometryTransform.TransformGeometry(features[i].Geometry,
                                                                                    CoordinateTransformation.
                                                                                        MathTransform);
#else
                    features[i].Geometry = GeometryTransform.TransformGeometry(features[i].Geometry,
                                                                                CoordinateTransformation.Source,
                                                                                CoordinateTransformation.Target);

#endif

                if (Style.EnableOutline)
                {
                    for (int i = 0; i < features.Count; i++)
                    {
                        FeatureDataRow feature = features[i];
                        VectorStyle outlineStyle = Theme.GetStyle(feature) as VectorStyle;
                        if (outlineStyle == null) continue;
                        if (!(outlineStyle.Enabled && outlineStyle.EnableOutline)) continue;
                        if (!(outlineStyle.MinVisible <= map.Zoom && map.Zoom <= outlineStyle.MaxVisible)) continue;
                        outlineStyle = outlineStyle.Clone();

                        if (feature.Geometry is LineString)
                        {
                            VectorRenderer.DrawLineString(g, feature.Geometry as LineString, outlineStyle.Outline,
                                                                map, outlineStyle.LineOffset,Style,RenderType.All);
                        }
                        else if (feature.Geometry is MultiLineString)
                        {
                            VectorRenderer.DrawMultiLineString(g, feature.Geometry as MultiLineString,
                                                                outlineStyle.Outline, map, outlineStyle.LineOffset, Style, RenderType.All);
                        }
                    }
                }

                for (int i = 0; i < features.Count; i++)
                {
                    FeatureDataRow feature = features[i];
                    VectorStyle style = Theme.GetStyle(feature) as VectorStyle;
                    if (style == null) continue;
                    if (!style.Enabled) continue;
                    if (!(style.MinVisible <= map.Zoom && map.Zoom <= style.MaxVisible)) continue;
                    RenderGeometry(g, map, feature.Geometry, style.Clone(),RenderType.All);
                }
            }
        }

        protected void RenderInternal(Graphics g, Map map, BoundingBox envelope, RenderType rendertype)
        {
            if (!Style.Enabled) return;

            VectorStyle vStyle = Style.Clone();
            Collection<Geometry> geoms;
            lock (_dataSource)
            {
                bool alreadyOpen = DataSource.IsOpen;

                if (!alreadyOpen) { DataSource.Open(); }

                geoms = DataSource.GetGeometriesInView(envelope);
                Console.Out.WriteLine(string.Format("Layer {0}, NumGeometries {1}", LayerName, geoms.Count));

                if (!alreadyOpen) { DataSource.Close(); }
            }
            if (CoordinateTransformation != null)
                for (int i = 0; i < geoms.Count; i++)
#if !DotSpatialProjections
                    geoms[i] = GeometryTransform.TransformGeometry(geoms[i], CoordinateTransformation.MathTransform);
#else
                    geoms[i] = GeometryTransform.TransformGeometry(geoms[i], CoordinateTransformation.Source, CoordinateTransformation.Target);
#endif
            if (vStyle.LineSymbolizer != null)
            {
                vStyle.LineSymbolizer.Begin(g, map, geoms.Count);
            }
            VectorRenderer.width = vStyle.Outline.Width;
            for (int i = 0; i < geoms.Count; i++)
            {
                if (geoms[i] != null)
                {
                    RenderGeometry(g, map, geoms[i], vStyle, rendertype);
                }
            }

            if (vStyle.LineSymbolizer != null)
            {
                vStyle.LineSymbolizer.Symbolize(g, map);
                vStyle.LineSymbolizer.End(g, map);
            }
        }

        protected void RenderGeometry(Graphics g, Map map, Geometry feature, VectorStyle layerstyle, RenderType renderType)
        {
            
            VectorStyle style = layerstyle;
            if (feature.StyleType == 1)
            {
                style = new VectorStyle();
                style.EnableOutline = feature.EnableOutline;
                style.Fill = new SolidBrush(Color.FromArgb(feature.Fill));
                style.Line = new Pen(Color.Black);
                if (feature.DashStyle >= 0 && feature.DashStyle <= 4)
                {
                    style.Line.DashStyle = (DashStyle)(feature.DashStyle);
                }
                else
                {
                    style.Line.DashStyle = DashStyle.Solid;
                }
                style.Outline = new Pen(Color.FromArgb(feature.Outline), feature.OutlineWidth);
                style.Outline.DashStyle = style.Line.DashStyle;
                style.HatchStyle = feature.HatchStyle;
                style.Penstyle = feature.Penstyle;

            }
            if (feature.PointSymbol != null)
            {
                style.PointSymbol = feature.PointSymbol;
            }
            if (feature.PointSelectSymbol != null)
            {
                style.PointSelectSymbol = feature.PointSelectSymbol;
            }
            if (style.Penstyle == 6)
            {
                style.Outline.CompoundArray = new float[] { 0, 2f / style.Outline.Width, 1 - 2f / style.Outline.Width, 1 };
            }
            GeometryType2 geometryType = feature.GeometryType;
            string key = this.ID.ToString() + "_" + feature.ID.ToString();
            switch (geometryType)
            {
                case GeometryType2.Polygon:
                    Brush brush = style.Fill;
                    if (PriceTable.ContainsKey(key))
                    {
                        brush = PriceTable[key] as SolidBrush;
                    }
                    else if (GeometryColor.ContainsKey(feature))
                    {
                        brush = new SolidBrush( (Color)GeometryColor[feature]);
                    }
                    else if (style.HatchStyle >= 0)
                    {
                        brush = new HatchBrush((HatchStyle)style.HatchStyle, style.Outline.Color, style.Fill.Color);
                    }
                    if (style.EnableOutline)
                        VectorRenderer.DrawPolygon(g, (Polygon)feature, brush, style.Outline, _clippingEnabled,
                                                   map, style, renderType, Type.ToString());
                    else
                        VectorRenderer.DrawPolygon(g, (Polygon)feature, brush, null, _clippingEnabled, map, style, renderType, Type.ToString());
                    break;
                case GeometryType2.MultiPolygon:
                    Brush brush1 = style.Fill;
                    if (PriceTable.ContainsKey(key))
                    {
                        brush1 = PriceTable[key] as SolidBrush;
                    }
                    else if (GeometryColor.ContainsKey(feature))
                    {
                        brush1 = new SolidBrush((Color)GeometryColor[feature]);
                    }
                    else if (style.HatchStyle >= 0)
                    {
                        brush1 = new HatchBrush((HatchStyle)style.HatchStyle, style.Outline.Color, style.Fill.Color);
                    }

                    if (style.EnableOutline)
                        VectorRenderer.DrawMultiPolygon(g, (MultiPolygon)feature, brush1, style.Outline,
                                                        _clippingEnabled, map, style, renderType, Type.ToString());
                    else
                        VectorRenderer.DrawMultiPolygon(g, (MultiPolygon)feature, brush1, null, _clippingEnabled,
                                                        map, style, renderType, Type.ToString());
                    break;
                case GeometryType2.LineString:
                    if (style.LineSymbolizer != null)
                    {
                        style.LineSymbolizer.Render(map, (LineString)feature, g);
                        return;
                    }
                    //VectorRenderer.zoom = zoom;
                    VectorRenderer.DrawLineString(g, (LineString)feature, style.Outline, map, style.LineOffset, style, renderType);
                    return;
                case GeometryType2.MultiLineString:
                    if (style.LineSymbolizer != null)
                    {
                        style.LineSymbolizer.Render(map, (MultiLineString)feature, g);
                        return;
                    }
                    VectorRenderer.DrawMultiLineString(g, (MultiLineString)feature, style.Outline, map, style.LineOffset, style, renderType);
                    break;
                case GeometryType2.Point:
                    if (style.PointSymbolizer != null)
                    {
                        VectorRenderer.DrawPoint(style.PointSymbolizer, g, (Point)feature, map, style, renderType);
                        return;
                    }

                    if (style.Symbol != null || style.PointColor == null)
                    {
                        Image image = style.Symbol;
                        Point point = (Point)feature;
                        if (style.PointSymbol != null)
                        {
                            image = style.PointSymbol;
                            if (point.Select && style.PointSelectSymbol != null)
                            {
                                image = style.PointSelectSymbol;
                            }
                        }
                        if (point.IsAreaPriceMonitor)
                        {
                            if (style.PointPriceSymbol != null)
                            {
                                image = style.PointPriceSymbol;
                            }
                            if (point.Select && style.PointPriceSelectSymbol != null)
                            {
                                image = style.PointPriceSelectSymbol;
                            }
                        }
                        VectorRenderer.DrawPoint(g, (Point)feature, image, style.SymbolScale, style.SymbolOffset,
                                                 style.SymbolRotation, map, style, renderType);
                        return;
                    }
                    VectorRenderer.DrawPoint(g, (Point)feature, style.PointColor, style.PointSize, style.SymbolOffset, map, style, renderType);

                    break;
                case GeometryType2.MultiPoint:
                    if (style.PointSymbolizer != null)
                    {
                        VectorRenderer.DrawMultiPoint(style.PointSymbolizer, g, (MultiPoint)feature, map, style, renderType);
                    }
                    if (style.Symbol != null || style.PointColor == null)
                    {
                        VectorRenderer.DrawMultiPoint(g, (MultiPoint)feature, style.Symbol, style.SymbolScale,
                                                  style.SymbolOffset, style.SymbolRotation, map, style, renderType);
                    }
                    else
                    {
                        VectorRenderer.DrawMultiPoint(g, (MultiPoint)feature, style.PointColor, style.PointSize, style.SymbolOffset, map, style, renderType);
                    }
                    break;
                case GeometryType2.GeometryCollection:
                    foreach (Geometry geom in (GeometryCollection)feature)
                        RenderGeometry(g, map, geom, style,renderType);
                    break;
                default:
                    break;
            }
        }

        #region Implementation of ICanQueryLayer

        public void ExecuteIntersectionQuery(BoundingBox box, FeatureDataSet ds)
        {
            if (CoordinateTransformation != null)
            {
#if !DotSpatialProjections
                if (ReverseCoordinateTransformation != null)
                {
                    box = GeometryTransform.TransformBox(box, ReverseCoordinateTransformation.MathTransform);
                }
                else
                {
                    CoordinateTransformation.MathTransform.Invert();
                    box = GeometryTransform.TransformBox(box, CoordinateTransformation.MathTransform);
                    CoordinateTransformation.MathTransform.Invert();
                }
#else
                box = GeometryTransform.TransformBox(box, CoordinateTransformation.Target, CoordinateTransformation.Source);
#endif
            }

            lock (_dataSource)
            {
                _dataSource.Open();
                _dataSource.ExecuteIntersectionQuery(box, ds);
                _dataSource.Close();
            }
        }

        public void ExecuteIntersectionQuery(Geometry geometry, FeatureDataSet ds)
        {
            if (CoordinateTransformation != null)
            {
#if !DotSpatialProjections
                if (ReverseCoordinateTransformation != null)
                {
                    geometry = GeometryTransform.TransformGeometry(geometry, ReverseCoordinateTransformation.MathTransform);
                }
                else
                {
                    CoordinateTransformation.MathTransform.Invert();
                    geometry = GeometryTransform.TransformGeometry(geometry, CoordinateTransformation.MathTransform);
                    CoordinateTransformation.MathTransform.Invert();
                }
#else
                geometry = GeometryTransform.TransformGeometry(geometry, CoordinateTransformation.Target, CoordinateTransformation.Source);
#endif
            }

            lock (_dataSource)
            {
                _dataSource.Open();
                _dataSource.ExecuteIntersectionQuery(geometry, ds);
                _dataSource.Close();
            }
        }

        public bool IsQueryEnabled
        {
            get { return _isQueryEnabled; }
            set { _isQueryEnabled = value; }
        }

        #endregion
    }
}
