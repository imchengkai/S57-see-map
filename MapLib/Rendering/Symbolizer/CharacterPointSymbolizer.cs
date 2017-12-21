
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public class CharacterPointSymbolizer : PointSymbolizer
    {
        private string _text;

        private int _characterIndex;

        public CharacterPointSymbolizer()
        {
            Font = new Font("Wingdings", 12);
            CharacterIndex = 0xa5;
            Foreground = Brushes.Firebrick;
            Halo = 0;
            HaloBrush = Brushes.Transparent;
            StringFormat = new StringFormat(StringFormatFlags.NoClip) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.None };
        }

        public Font Font { get; set; }

        public Brush Foreground { get; set; }

        public int Halo { get; set; }

        public Brush HaloBrush { get; set; }

        public int CharacterIndex
        {
            get { return _characterIndex; }
            set
            {
                _characterIndex = value;
                _text = Convert.ToString(Convert.ToChar(_characterIndex));
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                _text = value;

                if (_text.Length > 1)
                    _characterIndex = -1;
                else
                {
                    _characterIndex = Convert.ToInt32(Convert.ToChar(_text[0]));
                }
            }
        }

        public StringFormat StringFormat { get; set; }

        public override Size Size
        {
            get
            {
                return Size.Empty;
                /*
                var bmp = new Bitmap(1,1);
                using (var g = Graphics.FromImage(bmp))
                {
                    var sizef = Rendering.VectorRenderer.SizeOfString(g, _text, Font);
                    return sizef.ToSize();
                }
                 */

            }
            set
            {
            }
        }

        public override float Scale
        {
            get
            {
                return 1;
            }
            set { }
        }

        internal override void OnRenderInternal(PointF pt, Graphics g)
        {
            if (Halo > 0)
            {
                GraphicsPath path = new GraphicsPath(FillMode.Winding);
                path.AddString(_text, Font.FontFamily, (int)Font.Style, Font.Size, pt, StringFormat);
                g.DrawPath(new Pen(HaloBrush, 2 * Halo), path);
                g.FillPath(Foreground, path);
            }
            else
            {
                g.DrawString(_text, Font, Foreground, pt, StringFormat);
            }
        }
    }
}
