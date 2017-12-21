

namespace EasyMap.Geometries
{
    public interface IGeometryCollection : IGeometry
    {
        int NumGeometries { get; }

        Geometry Geometry(int N);
    }
}
