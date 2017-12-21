

using System;
namespace EasyMap.Geometries
{
    [Serializable]
    public abstract class Curve : Geometry, ILineal
    {
        public override int Dimension
        {
            get { return 1; }
        }

        public abstract double Length { get; }

        public abstract Point StartPoint { get; }

        public abstract Point EndPoint { get; }

        public bool IsClosed
        {
            get { return (StartPoint.Equals(EndPoint)); }
        }

        public abstract bool IsRing { get; }

        public abstract Point Value(double t);

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.Curve;
            }
        }
        public abstract bool IsSelect(Point p);
    }
}
