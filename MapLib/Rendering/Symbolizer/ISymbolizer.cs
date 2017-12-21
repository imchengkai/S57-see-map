using System;
using System.Drawing;
using EasyMap.Data;
using EasyMap.Geometries;
using EasyMap.Rendering.Thematics;

namespace EasyMap.Rendering.Symbolizer
{
    public interface ISymbolizer
    {
        void Begin(Graphics g, Map map, int aproximateNumberOfGeometries);

        void Symbolize(Graphics g, Map map);

        void End(Graphics g, Map map);

        /*
        Image Icon { get; } 
         */
    }

    public interface ISymbolizer<TGeometry> : ISymbolizer
        where TGeometry : class, IGeometryClassifier
    {
        void Render(Map map, TGeometry geometry, Graphics graphics);

    }
}
