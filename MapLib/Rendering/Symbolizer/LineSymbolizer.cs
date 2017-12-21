using System.Drawing;
using System.Drawing.Drawing2D;
using EasyMap.Geometries;

namespace EasyMap.Rendering.Symbolizer
{
    public abstract class LineSymbolizer : ILineSymbolizer
    {
        protected LineSymbolizer()
        {
            Line = new Pen(Utility.RandomKnownColor(), 1);
        }

        public void Render(Map map, ILineal lineal, Graphics g)
        {
            var ms = lineal as MultiLineString;
            if (ms != null)
            {
                foreach (LineString lineString in ms.LineStrings)
                    OnRenderInternal(map, lineString, g);
                return;
            }
            OnRenderInternal(map, (LineString)lineal, g);
        }

        protected abstract void OnRenderInternal(Map map, LineString lineString, Graphics graphics);

        public static GraphicsPath LineStringToPath(LineString lineString, Map map)
        {
            var gp = new GraphicsPath(FillMode.Alternate);
            gp.AddLines(lineString.TransformToImage(map));
            return gp;
        }

        public Pen Line { get; set; }

        #region ISymbolizer implementation

        public virtual void Begin(Graphics g, Map map, int aproximateNumberOfGeometries)
        {
        }

        public virtual void Symbolize(Graphics g, Map map)
        {
        }

        public virtual void End(Graphics g, Map map)
        {
        }

        #endregion
    }
}
