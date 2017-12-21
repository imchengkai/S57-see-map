
using System.Drawing;
using EasyMap.Geometries;
using Point = EasyMap.Geometries.Point;

namespace EasyMap.Rendering.Symbolizer
{
    public interface IPointSymbolizer : ISymbolizer<IPuntal>
    {
        PointF Offset { get; set; }

        float Rotation { get; set; }

        Size Size { get; set; }

        float Scale { get; set; }


    }
}
