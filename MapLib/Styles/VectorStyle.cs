

using System.Drawing;
using System.Reflection;
using EasyMap.Geometries;
using EasyMap.Rendering.Symbolizer;
using System;

namespace EasyMap.Styles
{
    public class VectorStyle : Style
    {
        public static readonly Image DefaultSymbol;

        static VectorStyle()
        {
            DefaultSymbol = new Bitmap(2, 2);
        }

        public Pen ClonePen(Pen pen)
        {
            Pen vs = new Pen(pen.Color, pen.Width);
            if(Line.CompoundArray.Length>0)
            vs.CompoundArray = Line.CompoundArray;
            vs.DashOffset = Line.DashOffset;
            vs.DashStyle = Line.DashStyle;
            vs.EndCap = Line.EndCap;
            vs.Alignment = Line.Alignment;
            vs.StartCap = Line.StartCap;
            return vs;
        }

        public VectorStyle Clone()
        {
            VectorStyle vs = null;
            //lock (_symbol)
            //{
                vs = new VectorStyle()
                {
                    _fillStyle = new SolidBrush(Fill.Color),
                    _lineOffset = LineOffset,
                    _lineStyle = ClonePen(Line),
                    _outline = EnableOutline,
                    _outlineStyle = ClonePen(Outline),
                    _PointBrush = new SolidBrush(((SolidBrush)PointColor).Color),
                    _PointSize = PointSize,
                    _symbol = (Symbol != null ? Symbol.Clone() as Image : null),
                    _symbolOffset = new PointF(SymbolOffset.X, SymbolOffset.Y),
                    _symbolRotation = SymbolRotation,
                    _symbolScale = SymbolScale,
                    _PointSymbolizer = PointSymbolizer,
                    _LineSymbolizer = LineSymbolizer,
                    _PolygonSymbolizer = PolygonSymbolizer,

                    _PointSymbol = (PointSymbol != null ? PointSymbol.Clone() as Image : null),
                    _PointSelectSymbol = (PointSelectSymbol != null ? PointSelectSymbol.Clone() as Image : null),
                    _PointPriceSymbol = (PointPriceSymbol != null ? PointPriceSymbol.Clone() as Image : null),
                    _PointPriceSelectSymbol = (PointPriceSelectSymbol != null ? PointPriceSelectSymbol.Clone() as Image : null),
                    _HatchStyle=HatchStyle,
                    _TextColor=TextColor,
                    _TextFont=TextFont,
                    _Penstyle=Penstyle
                };
            //}
            return vs;
        }

        #region Privates

        private SolidBrush _fillStyle;
        private Pen _lineStyle;
        private bool _outline;
        private Pen _outlineStyle;
        private Image _symbol;
        private float _lineOffset;
        private Image _PointSymbol;
        private Image _PointSelectSymbol;
        private Image _PointPriceSymbol;
        private Image _PointPriceSelectSymbol;
        private Font _TextFont;
        private int _HatchStyle = -1;

        public int HatchStyle
        {
            get { return _HatchStyle; }
            set { _HatchStyle = value; }
        }

        private Color _TextColor=Color.Empty;


        #endregion

        public VectorStyle()
        {
            Outline = new Pen(Color.Black, 1);
            Line = new Pen(Color.Black, 1);
            Fill = new SolidBrush(Color.Black);
            EnableOutline = false;
            SymbolScale = 1f;
            PointColor = Brushes.Red;
            PointSize = 10f;
            LineOffset = 0;
        }

        #region Properties

        private PointF _symbolOffset;
        private float _symbolRotation;
        private float _symbolScale;
        private float _PointSize;
        private int _Penstyle=0;
        public int Penstyle 
        {
            get { return _Penstyle; }
            set { _Penstyle = value; }
        }

        private Brush _PointBrush = null;

        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }
        public Font TextFont
        {
            get { return _TextFont; }
            set { _TextFont = value; }
        }
        public Pen Line
        {
            get { return _lineStyle; }
            set { _lineStyle = value; }
        }

        public Pen Outline
        {
            get { return _outlineStyle; }
            set { _outlineStyle = value; }
        }

        public bool EnableOutline
        {
            get { return _outline; }
            set { _outline = value; }
        }

        public SolidBrush Fill
        {
            get { return _fillStyle; }
            set { _fillStyle = value; }
        }

        public Brush PointColor
        {
            get { return _PointBrush; }
            set { _PointBrush = value; }
        }

        public float PointSize
        {
            get { return _PointSize; }
            set { _PointSize = value; }
        }

        public Image Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public float SymbolScale
        {
            get { return _symbolScale; }
            set { _symbolScale = value; }
        }

        public Image PointSymbol
        {
            get { return _PointSymbol; }
            set { _PointSymbol = value; }
        }
        public Image PointSelectSymbol
        {
            get { return _PointSelectSymbol; }
            set { _PointSelectSymbol = value; }
        }
        public Image PointPriceSymbol
        {
            get { return _PointPriceSymbol; }
            set { _PointPriceSymbol = value; }
        }
        public Image PointPriceSelectSymbol
        {
            get { return _PointPriceSelectSymbol; }
            set { _PointPriceSelectSymbol = value; }
        }
        public PointF SymbolOffset
        {
            get { return _symbolOffset; }
            set { _symbolOffset = value; }
        }

        public float SymbolRotation
        {
            get { return _symbolRotation; }
            set { _symbolRotation = value; }
        }

        public float LineOffset
        {
            get { return _lineOffset; }
            set { _lineOffset = value; }
        }

        private IPointSymbolizer _PointSymbolizer;

        public IPointSymbolizer PointSymbolizer 
        {
            get { return _PointSymbolizer; }
            set { _PointSymbolizer = value; }
        }
        private ILineSymbolizer _LineSymbolizer;
        public ILineSymbolizer LineSymbolizer
        {
            get { return _LineSymbolizer; }
            set { _LineSymbolizer = value; }
        }

        private IPolygonSymbolizer _PolygonSymbolizer;
        public IPolygonSymbolizer PolygonSymbolizer
        {
            get { return _PolygonSymbolizer; }
            set { _PolygonSymbolizer = value; }
        }

        #endregion
    }
}
