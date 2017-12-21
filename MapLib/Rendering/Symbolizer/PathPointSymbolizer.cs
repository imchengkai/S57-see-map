
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public class PathPointSymbolizer : PointSymbolizer
    {

        public static PathPointSymbolizer CreateCircle(Pen line, Brush fill, float size)
        {
            return CreateEllipse(line, fill, size, size);
        }


        public static PathPointSymbolizer CreateEllipse(Pen line, Brush fill, float a, float b)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, a, b);
            return new PathPointSymbolizer(
                new[] { new PathDefinition { Line = line, Fill = fill, Path = path } });
        }

        public static PathPointSymbolizer CreateRectangle(Pen line, Brush fill, float width, float height)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(-0.5f * width, -0.5f * height, width, height));
            return new PathPointSymbolizer(
                new[] { new PathDefinition { Line = line, Fill = fill, Path = path } });
        }

        public static PathPointSymbolizer CreateSquare(Pen line, Brush fill, float size)
        {

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(-0.5f * size, -0.5f * size, size, size));
            return new PathPointSymbolizer(
                new[] { new PathDefinition { Line = line, Fill = fill, Path = path } });
        }

        public static PathPointSymbolizer CreateTriangle(Pen line, Brush fill, float size)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(new[]
                                {
                                    new PointF(-0.5f*size, size/3f), new PointF(0, 2f*size/3f),
                                    new PointF(0.5f*size, size/3f), new PointF(-0.5f*size, size/3f),
                                }
                );
            return new PathPointSymbolizer(
                new[] { new PathDefinition { Line = line, Fill = fill, Path = path } });
        }

        [Serializable]
        public class PathDefinition
        {
            public Pen Line { get; set; }

            public Brush Fill { get; set; }

            public GraphicsPath Path { get; set; }
        }


        private readonly PathDefinition[] _paths;


        public PathPointSymbolizer(PathDefinition[] paths)
        {
            _paths = paths;
        }

        public override Size Size
        {
            get
            {
                var size = new Size();
                foreach (PathDefinition pathDefinition in _paths)
                {
                    var bounds = pathDefinition.Path.GetBounds();
                    size = new Size(Math.Max(size.Width, (int)bounds.Width),
                                    Math.Max(size.Height, (int)bounds.Height));
                }
                return size;
            }
            set
            {
            }
        }

        internal override void OnRenderInternal(PointF pt, Graphics g)
        {
            var f = new SizeF(pt);
            foreach (var pathDefinition in _paths)
            {
                var ppts = pathDefinition.Path.PathPoints;
                var pptsnew = new PointF[pathDefinition.Path.PointCount];
                for (int i = 0; i < pptsnew.Length; i++)
                    pptsnew[i] = PointF.Add(ppts[i], f);

                GraphicsPath ptmp = new GraphicsPath(pptsnew, pathDefinition.Path.PathTypes, pathDefinition.Path.FillMode);
                if (pathDefinition.Fill != null)
                    g.FillPath(pathDefinition.Fill, ptmp);
                if (pathDefinition.Line != null)
                    g.DrawPath(pathDefinition.Line, ptmp);

            }
        }
    }
}
