
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public class RasterPointSymbolizer : PointSymbolizer
    {
        private static readonly Bitmap DefaultSymbol = new Bitmap(2, 2);

        private ImageAttributes _imageAttributes;

        public Image Symbol { get; set; }

        public ImageAttributes ImageAttributes
        {
            get
            {
                return _imageAttributes;
            }
            set
            {
                _imageAttributes = value;
            }
        }

        public override Size Size
        {
            get
            {

                var size = Symbol == null ? DefaultSymbol.Size : Symbol.Size;
                return new Size((int)(Scale * size.Width), (int)(Scale * size.Height));
            }
            set
            {
            }
        }

        internal override void OnRenderInternal(PointF pt, Graphics g)
        {
            Image symbol = Symbol ?? DefaultSymbol;

            if (ImageAttributes == null)
            {
                if (Scale == 1f)
                {
                    lock (symbol)
                    {
                        g.DrawImageUnscaled(symbol, (int)(pt.X), (int)(pt.Y));
                    }
                }
                else
                {
                    float width = symbol.Width * Scale;
                    float height = symbol.Height * Scale;
                    lock (symbol)
                    {
                        g.DrawImage(
                            symbol,
                            (int)pt.X,
                            (int)pt.Y,
                            width,
                            height);
                    }
                }
            }
            else
            {
                float width = symbol.Width * Scale;
                float height = symbol.Height * Scale;
                int x = (int)(pt.X);
                int y = (int)(pt.Y);
                g.DrawImage(
                    symbol,
                    new Rectangle(x, y, (int)width, (int)height),
                    0,
                    0,
                    symbol.Width,
                    symbol.Height,
                    GraphicsUnit.Pixel,
                    ImageAttributes);
            }
        }
    }
}
