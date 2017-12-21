

using System;
namespace EasyMap.Geometries
{
    [Serializable]
    public abstract class Surface : Geometry, IPolygonal
    {
        public abstract double Area { get; }

        public virtual Point Centroid
        {
            get { return GetBoundingBox().GetCentroid(); }
        }

        public abstract Point PointOnSurface { get; }

        public override int Dimension
        {
            get { return 2; }
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.Surface;
            }
        }

    }
}
