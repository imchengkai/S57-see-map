using System;
using System.Drawing;
using EasyMap.Data;
using EasyMap.Geometries;
using EasyMap.Rendering.Thematics;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public class BasicLineSymbolizer : LineSymbolizer
    {
        protected override void OnRenderInternal(Map map, LineString linestring, Graphics g)
        {
            g.DrawLines(Line, /*LimitValues(*/linestring.TransformToImage(map)/*)*/);
        }

    }

    [Serializable]
    public class BasicLineSymbolizerWithOffset : LineSymbolizer
    {
        public float Offset { get; set; }

        protected override void OnRenderInternal(Map map, LineString linestring, Graphics g)
        {
            var pts = /*LimitValues(*/ linestring.TransformToImage(map) /*)*/;
            g.DrawLines(Line, pts);
        }

    }

}
