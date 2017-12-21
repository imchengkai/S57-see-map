

using System.Drawing;
using EasyMap.Geometries;
using EasyMap.Rendering;

namespace EasyMap.Layers
{
    public interface ILayer
    {
        int SortNo { get; set; }
        decimal ID { get; set; }
        double MinVisible { get; set; }

        double MaxVisible { get; set; }

        bool Enabled { get; set; }

        string LayerName { get; set; }

        BoundingBox Envelope { get; }


        int SRID { get; set; }
        bool NeedSave { get; set; }
        bool AllowEdit { get; set; }

        void Render(Graphics g, Map map,RenderType rendertype);


        EasyMap.Layers.VectorLayer.LayerType Type
        {
            get;
            set;
        }

    }
}
