
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using EasyMap.Geometries;
using EasyMap.Utilities;
using Point = EasyMap.Geometries.Point;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public abstract class PointSymbolizer : IPointSymbolizer
    {
        private float _scale = 1f;

        public PointF Offset { get; set; }

        public float Rotation { get; set; }

        public abstract Size Size
        {
            get;
            set;
        }


        public virtual float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                if (value <= 0)
                    return;
                _scale = value;
            }
        }

        private SizeF GetOffset()
        {
            var size = Size;
            var result = new SizeF(Offset.X - Scale * (size.Width * 0.5f), Offset.Y - Scale * (size.Height * 0.5f));
            return result;
        }



        protected void RenderPoint(Map map, Point point, Graphics g)
        {
            if (point == null)
                return;


            PointF pp = Transform.WorldtoMap(point, map);
            pp = PointF.Add(pp, GetOffset());

            if (Rotation != 0f && !Single.IsNaN(Rotation))
            {
                Matrix startingTransform = g.Transform.Clone();

                Matrix transform = g.Transform;
                PointF rotationCenter = pp;
                transform.RotateAt(Rotation, rotationCenter);

                g.Transform = transform;

                OnRenderInternal(pp, g);

                g.Transform = startingTransform;
            }
            else
            {
                OnRenderInternal(pp, g);
            }
        }

        [Obsolete]
        public void Render(Map map, MultiPoint points, Graphics g)
        {
            if (points == null)
                return;

            foreach (Point point in points)
                Render(map, point, g);
        }

        internal abstract void OnRenderInternal(PointF pt, Graphics g);

        public virtual IPointSymbolizer ToRasterPointSymbolizer()
        {
            var bitmap = new Bitmap(Size.Width, Size.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                OnRenderInternal(new PointF(Size.Width * 0.5f, Size.Height * 0.5f), g);
            }

            return new RasterPointSymbolizer
            {
                Offset = Offset,
                Rotation = Rotation,
                Scale = Scale,
                ImageAttributes = new ImageAttributes(),
                Symbol = bitmap
            };
        }

        public void Render(Map map, IPuntal geometry, Graphics graphics)
        {
            var mp = geometry as MultiPoint;
            if (mp != null)
            {
                foreach (Point point in mp.Points)
                    RenderPoint(map, point, graphics);
                return;
            }
            RenderPoint(map, geometry as Point, graphics);

        }

        public void Begin(Graphics g, Map map, int aproximateNumberOfGeometries)
        {
        }

        public void Symbolize(Graphics g, Map map)
        {
        }

        public void End(Graphics g, Map map)
        {
        }
    }
}
