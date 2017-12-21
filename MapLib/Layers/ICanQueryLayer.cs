using EasyMap.Data;
using EasyMap.Geometries;

namespace EasyMap.Layers
{
    public interface ICanQueryLayer : ILayer
    {
        void ExecuteIntersectionQuery(BoundingBox box, FeatureDataSet ds);

        void ExecuteIntersectionQuery(Geometry geometry, FeatureDataSet ds);

        bool IsQueryEnabled { get; set; }
    }
}
