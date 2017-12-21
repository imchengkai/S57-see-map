namespace EasyMap.Geometries
{
    public interface IGeometryClassifier
    {
        GeometryType2 GeometryType { get; }
    }

    public interface IUndefined : IGeometryClassifier
    { }

    public interface IPuntal : IGeometryClassifier
    {
    }
    public interface ILineal : IGeometryClassifier
    {
    }
    public interface IPolygonal : IGeometryClassifier
    {
    }
}
