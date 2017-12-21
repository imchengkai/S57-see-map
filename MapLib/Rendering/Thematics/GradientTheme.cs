

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using EasyMap.Data;
using EasyMap.Styles;

namespace EasyMap.Rendering.Thematics
{
    public abstract class GradientThemeBase : ITheme
    {
        private ColorBlend _fillColorBlend;
        private ColorBlend _lineColorBlend;
        private double _max;
        private IStyle _maxStyle;
        private double _min;
        private IStyle _minStyle;
        private ColorBlend _textColorBlend;

        protected GradientThemeBase(double minValue, double maxValue, IStyle minStyle, IStyle maxStyle)
        {
            _min = minValue;
            _max = maxValue;
            _maxStyle = maxStyle;
            _minStyle = minStyle;
        }

        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public IStyle MinStyle
        {
            get { return _minStyle; }
            set { _minStyle = value; }
        }

        public IStyle MaxStyle
        {
            get { return _maxStyle; }
            set { _maxStyle = value; }
        }

        public ColorBlend TextColorBlend
        {
            get { return _textColorBlend; }
            set { _textColorBlend = value; }
        }

        public ColorBlend LineColorBlend
        {
            get { return _lineColorBlend; }
            set { _lineColorBlend = value; }
        }

        public ColorBlend FillColorBlend
        {
            get { return _fillColorBlend; }
            set { _fillColorBlend = value; }
        }


        protected VectorStyle CalculateVectorStyle(VectorStyle min, VectorStyle max, double value)
        {
            VectorStyle style = new VectorStyle();
            double dFrac = Fraction(value);
            float fFrac = Convert.ToSingle(dFrac);
            style.Enabled = (dFrac > 0.5 ? min.Enabled : max.Enabled);
            style.EnableOutline = (dFrac > 0.5 ? min.EnableOutline : max.EnableOutline);
            if (_fillColorBlend != null)
                style.Fill = new SolidBrush(_fillColorBlend.GetColor(fFrac));
            else if (min.Fill != null && max.Fill != null)
                style.Fill = InterpolateBrush(min.Fill, max.Fill, value);

            if (min.Line != null && max.Line != null)
                style.Line = InterpolatePen(min.Line, max.Line, value);
            if (_lineColorBlend != null)
                style.Line.Color = _lineColorBlend.GetColor(fFrac);

            if (min.Outline != null && max.Outline != null)
                style.Outline = InterpolatePen(min.Outline, max.Outline, value);
            style.MinVisible = InterpolateDouble(min.MinVisible, max.MinVisible, value);
            style.MaxVisible = InterpolateDouble(min.MaxVisible, max.MaxVisible, value);
            style.Symbol = (dFrac > 0.5 ? min.Symbol : max.Symbol);
            style.SymbolOffset = (dFrac > 0.5 ? min.SymbolOffset : max.SymbolOffset);
            style.SymbolScale = InterpolateFloat(min.SymbolScale, max.SymbolScale, value);
            return style;
        }

        protected LabelStyle CalculateLabelStyle(LabelStyle min, LabelStyle max, double value)
        {
            LabelStyle style = new LabelStyle();
            style.CollisionDetection = min.CollisionDetection;
            style.Enabled = InterpolateBool(min.Enabled, max.Enabled, value);
            float fontSize = InterpolateFloat(min.Font.Size, max.Font.Size, value);
            style.Font = new Font(min.Font.FontFamily, fontSize, min.Font.Style);
            if (min.BackColor != null && max.BackColor != null)
                style.BackColor = InterpolateBrush(min.BackColor, max.BackColor, value);

            if (_textColorBlend != null)
                style.ForeColor = _lineColorBlend.GetColor(Convert.ToSingle(Fraction(value)));
            else
                style.ForeColor = InterpolateColor(min.ForeColor, max.ForeColor, value);
            if (min.Halo != null && max.Halo != null)
                style.Halo = InterpolatePen(min.Halo, max.Halo, value);

            style.MinVisible = InterpolateDouble(min.MinVisible, max.MinVisible, value);
            style.MaxVisible = InterpolateDouble(min.MaxVisible, max.MaxVisible, value);
            style.Offset = new PointF(InterpolateFloat(min.Offset.X, max.Offset.X, value),
                                      InterpolateFloat(min.Offset.Y, max.Offset.Y, value));
            return style;
        }

        protected double Fraction(double attr)
        {
            if (attr < _min) return 0;
            if (attr > _max) return 1;
            return (attr - _min) / (_max - _min);
        }

        protected bool InterpolateBool(bool min, bool max, double attr)
        {
            double frac = Fraction(attr);
            if (frac > 0.5) return max;
            return min;
        }

        protected float InterpolateFloat(float min, float max, double attr)
        {
            return Convert.ToSingle((max - min) * Fraction(attr) + min);
        }

        protected double InterpolateDouble(double min, double max, double attr)
        {
            return (max - min) * Fraction(attr) + min;
        }

        protected SolidBrush InterpolateBrush(Brush min, Brush max, double attr)
        {
            if (!(min is SolidBrush && max is SolidBrush))
                throw (new ArgumentException("Only SolidBrush brushes are supported in GradientTheme"));
            return new SolidBrush(InterpolateColor((min as SolidBrush).Color, (max as SolidBrush).Color, attr));
        }

        protected Pen InterpolatePen(Pen min, Pen max, double attr)
        {
            if (min.PenType != PenType.SolidColor || max.PenType != PenType.SolidColor)
                throw (new ArgumentException("Only SolidColor pens are supported in GradientTheme"));
            Pen pen = new Pen(InterpolateColor(min.Color, max.Color, attr), InterpolateFloat(min.Width, max.Width, attr));
            double frac = Fraction(attr);
            pen.MiterLimit = InterpolateFloat(min.MiterLimit, max.MiterLimit, attr);
            pen.StartCap = (frac > 0.5 ? max.StartCap : min.StartCap);
            pen.EndCap = (frac > 0.5 ? max.EndCap : min.EndCap);
            pen.LineJoin = (frac > 0.5 ? max.LineJoin : min.LineJoin);
            pen.DashStyle = (frac > 0.5 ? max.DashStyle : min.DashStyle);
            if (min.DashStyle == DashStyle.Custom && max.DashStyle == DashStyle.Custom)
                pen.DashPattern = (frac > 0.5 ? max.DashPattern : min.DashPattern);
            pen.DashOffset = (frac > 0.5 ? max.DashOffset : min.DashOffset);
            pen.DashCap = (frac > 0.5 ? max.DashCap : min.DashCap);
            if (min.CompoundArray.Length > 0 && max.CompoundArray.Length > 0)
                pen.CompoundArray = (frac > 0.5 ? max.CompoundArray : min.CompoundArray);
            pen.Alignment = (frac > 0.5 ? max.Alignment : min.Alignment);
            return pen;
        }

        protected Color InterpolateColor(Color minCol, Color maxCol, double attr)
        {
            double frac = Fraction(attr);

            if (frac == 1)
                return maxCol;

            if (frac == 0)
                return minCol;

            double r = (maxCol.R - minCol.R) * frac + minCol.R;
            double g = (maxCol.G - minCol.G) * frac + minCol.G;
            double b = (maxCol.B - minCol.B) * frac + minCol.B;
            double a = (maxCol.A - minCol.A) * frac + minCol.A;
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            if (a > 255) a = 255;

            return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
        }

        public virtual IStyle GetStyle(FeatureDataRow row)
        {
            double attr;
            try
            {
                attr = GetAttributeValue(row);
            }
            catch
            {
                throw new ApplicationException(
                    "Invalid Attribute type in Gradient Theme - Couldn't parse attribute (must be numerical)");
            }
            if (_minStyle.GetType() != _maxStyle.GetType())
                throw new ArgumentException("MinStyle and MaxStyle must be of the same type");
            switch (MinStyle.GetType().FullName)
            {
                case "EasyMap.Styles.VectorStyle":
                    return CalculateVectorStyle(MinStyle as VectorStyle, MaxStyle as VectorStyle, attr);
                case "EasyMap.Styles.LabelStyle":
                    return CalculateLabelStyle(MinStyle as LabelStyle, MaxStyle as LabelStyle, attr);
                default:
                    throw new ArgumentException(
                        "Only SharpMap.Styles.VectorStyle and SharpMap.Styles.LabelStyle are supported for the gradient theme");
            }
        }

        protected abstract double GetAttributeValue(FeatureDataRow row);
    }

    public class GradientTheme : GradientThemeBase
    {
        private string _columnName;


        public GradientTheme(string columnName, double minValue, double maxValue, IStyle minStyle, IStyle maxStyle)
            : base(minValue, maxValue, minStyle, maxStyle)
        {
            _columnName = columnName;

        }

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        protected override double GetAttributeValue(FeatureDataRow row)
        {
            return Convert.ToDouble(row[_columnName]);
        }
    }
}
