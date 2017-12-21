using System;
using System.Drawing;
using EasyMap.Geometries;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public class BasicPolygonSymbolizer : PolygonSymbolizer
    {
        public BasicPolygonSymbolizer()
        {
            Outline = new Pen(Utility.RandomKnownColor(), 1);
        }

        public Pen Outline { get; set; }

        protected override void OnRenderInternal(Map map, Polygon polygon, Graphics g)
        {
            var pts = /*LimitValues(*/polygon.TransformToImage(map)/*)*/;

            if (UseClipping)
                pts = VectorRenderer.ClipPolygon(pts, map.Size.Width, map.Size.Height);

            if (Fill != null)
                g.FillPolygon(Fill, pts);

            if (Outline != null)
                g.DrawPolygon(Outline, pts);
        }
    }

    [Serializable]
    public class PolygonSymbolizerUsingLineSymbolizer : PolygonSymbolizer
    {
        public PolygonSymbolizerUsingLineSymbolizer()
        {
            Outline = new BasicLineSymbolizer();
        }
        public LineSymbolizer Outline { get; set; }

        protected override void OnRenderInternal(Map map, Polygon polygon, Graphics g)
        {
            var pts = /*LimitValues(*/polygon.TransformToImage(map)/*)*/;

            if (UseClipping)
                pts = VectorRenderer.ClipPolygon(pts, map.Size.Width, map.Size.Height);

            if (Fill != null)
                g.FillPolygon(Fill, pts);

            if (Outline != null)
            {
                Outline.Render(map, polygon.ExteriorRing, g);
                foreach (var ls in polygon.InteriorRings)
                    Outline.Render(map, ls, g);
            }
        }

        public override void Begin(Graphics g, Map map, int aproximateNumberOfGeometries)
        {
            Outline.Begin(g, map, aproximateNumberOfGeometries);
            base.Begin(g, map, aproximateNumberOfGeometries);
        }

        public override void Symbolize(Graphics g, Map map)
        {
            Outline.Symbolize(g, map);
            base.Symbolize(g, map);
        }

        public override void End(Graphics g, Map map)
        {
            Outline.End(g, map);
            base.End(g, map);
        }
    }
}
