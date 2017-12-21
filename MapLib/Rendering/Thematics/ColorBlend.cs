

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyMap.Rendering.Thematics
{
    public class ColorBlend
    {
        private Color[] _Colors;

        private float[] _Positions;
        private float _maximum = float.NaN;
        private float _minimum = float.NaN;

        internal ColorBlend()
        {
        }

        public ColorBlend(Color[] colors, float[] positions)
        {
            _Colors = colors;
            Positions = positions;
        }

        public Color[] Colors
        {
            get { return _Colors; }
            set { _Colors = value; }
        }

        public float[] Positions
        {
            get { return _Positions; }
            set
            {
                _Positions = value;
                if (value == null)
                    _minimum = _maximum = float.NaN;
                else
                {
                    _minimum = value[0];
                    _maximum = value[value.GetUpperBound(0)];
                }
            }
        }

        public Color GetColor(float pos)
        {
            if (float.IsNaN(_minimum))
                throw (new ArgumentException("Positions not set"));
            if (_Colors.Length != _Positions.Length)
                throw (new ArgumentException("Colors and Positions arrays must be of equal length"));
            if (_Colors.Length < 2)
                throw (new ArgumentException("At least two colors must be defined in the ColorBlend"));
            /*
            if (_Positions[0] != 0f)
                throw (new ArgumentException("First position value must be 0.0f"));
            if (_Positions[_Positions.Length - 1] != 1f)
                throw (new ArgumentException("Last position value must be 1.0f"));
            if (pos > 1 || pos < 0) pos -= (float) Math.Floor(pos);
             */
            int i = 1;
            while (i < _Positions.Length && _Positions[i] < pos)
                i++;
            float frac = (pos - _Positions[i - 1]) / (_Positions[i] - _Positions[i - 1]);
            frac = Math.Max(frac, 0.0f);
            frac = Math.Min(frac, 1.0f);
            int R = (int)Math.Round((_Colors[i - 1].R * (1 - frac) + _Colors[i].R * frac));
            int G = (int)Math.Round((_Colors[i - 1].G * (1 - frac) + _Colors[i].G * frac));
            int B = (int)Math.Round((_Colors[i - 1].B * (1 - frac) + _Colors[i].B * frac));
            int A = (int)Math.Round((_Colors[i - 1].A * (1 - frac) + _Colors[i].A * frac));
            return Color.FromArgb(A, R, G, B);
        }

        public LinearGradientBrush ToBrush(Rectangle rectangle, float angle)
        {
            LinearGradientBrush br = new LinearGradientBrush(rectangle, Color.Black, Color.Black, angle, true);
            System.Drawing.Drawing2D.ColorBlend cb = new System.Drawing.Drawing2D.ColorBlend();
            cb.Colors = _Colors;
            float[] positions = new float[_Positions.Length];
            float range = _maximum - _minimum;
            for (int i = 0; i < _Positions.Length; i++)
                positions[i] = (_Positions[i] - _minimum) / range;
            cb.Positions = positions;
            br.InterpolationColors = cb;
            return br;
        }

        #region Predefined color scales

        public static ColorBlend Rainbow7
        {
            get
            {
                ColorBlend cb = new ColorBlend();
                cb._Positions = new float[7];
                for (int i = 1; i < 7; i++)
                    cb.Positions[i] = i / 6f;
                cb.Colors = new[]
                                {
                                    Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo,
                                    Color.Violet
                                };
                return cb;
            }
        }

        public static ColorBlend Rainbow5
        {
            get
            {
                return new ColorBlend(
                    new[] { Color.Red, Color.Yellow, Color.Green, Color.Cyan, Color.Blue },
                    new[] { 0f, 0.25f, 0.5f, 0.75f, 1f });
            }
        }

        public static ColorBlend BlackToWhite
        {
            get { return new ColorBlend(new[] { Color.Black, Color.White }, new[] { 0f, 1f }); }
        }

        public static ColorBlend WhiteToBlack
        {
            get { return new ColorBlend(new[] { Color.White, Color.Black }, new[] { 0f, 1f }); }
        }

        public static ColorBlend RedToGreen
        {
            get { return new ColorBlend(new[] { Color.Red, Color.Green }, new[] { 0f, 1f }); }
        }

        public static ColorBlend GreenToRed
        {
            get { return new ColorBlend(new[] { Color.Green, Color.Red }, new[] { 0f, 1f }); }
        }

        public static ColorBlend BlueToGreen
        {
            get { return new ColorBlend(new[] { Color.Blue, Color.Green }, new[] { 0f, 1f }); }
        }

        public static ColorBlend GreenToBlue
        {
            get { return new ColorBlend(new[] { Color.Green, Color.Blue }, new[] { 0f, 1f }); }
        }

        public static ColorBlend RedToBlue
        {
            get { return new ColorBlend(new[] { Color.Red, Color.Blue }, new[] { 0f, 1f }); }
        }

        public static ColorBlend BlueToRed
        {
            get { return new ColorBlend(new[] { Color.Blue, Color.Red }, new[] { 0f, 1f }); }
        }

        #endregion

        #region Constructor helpers

        public static ColorBlend TwoColors(Color fromColor, Color toColor)
        {
            return new ColorBlend(new[] { fromColor, toColor }, new[] { 0f, 1f });
        }

        public static ColorBlend ThreeColors(Color fromColor, Color middleColor, Color toColor)
        {
            return new ColorBlend(new[] { fromColor, middleColor, toColor }, new[] { 0f, 0.5f, 1f });
        }

        #endregion
    }
}
