

using System;
namespace EasyMap.Geometries
{
    [Serializable]
    public abstract class MultiCurve : GeometryCollection, ILineal
    {
        public override int Dimension
        {
            get { return 1; }
        }

        public abstract bool IsClosed { get; }

        public abstract double Length { get; }

        public new abstract int NumGeometries { get; }

        public new abstract Geometry Geometry(int N);

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiCurve;
            }
        }
        public abstract bool IsSelect(Point p);
    }
}
