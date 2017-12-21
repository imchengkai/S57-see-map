

using System;
namespace EasyMap.Geometries
{
    [Serializable]
    public abstract class MultiSurface : GeometryCollection, IPolygonal
    {
        public abstract double Area { get; }

        public abstract Point Centroid { get; }

        public abstract Point PointOnSurface { get; }

        public override int Dimension
        {
            get { return 2; }
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiSurface;
            }
        }

    }
}
