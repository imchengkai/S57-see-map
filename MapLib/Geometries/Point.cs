

using System;
using System.Drawing;
using EasyMap.Utilities;

namespace EasyMap.Geometries
{
    [Serializable]
    public class Point : Geometry, IComparable<Point>, IPuntal
    {
        private double _x;
        private double _y;
        private bool _IsAreaPriceMonitor = false;

        public bool IsAreaPriceMonitor
        {
            get { return _IsAreaPriceMonitor; }
            set { _IsAreaPriceMonitor = value; }
        }
        public static readonly double NullOrdinate = Double.NaN;

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Point()
            : this(NullOrdinate, NullOrdinate) //: this(0d, 0d)
        {
        }

        public Point(double[] point)
        {
            if (point.Length < 2)
                throw new Exception("Only 2 dimensions are supported for points");

            _x = point[0];
            _y = point[1];
        }

        protected bool SetIsEmpty
        {
            set
            {
                if (value)
                    _x = NullOrdinate;// _isEmpty = value;
                else
                {
                    _x = 0d;
                    _y = 0d;
                }
            }
        }

        public double X
        {
            get
            {
                if (!IsEmptyPoint)
                    return _x;
                throw new ApplicationException("Point is empty");
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                if (!IsEmptyPoint)
                    return _y;
                throw new ApplicationException("Point is empty");
            }
            set
            {
                _y = value;
            }
        }

        public virtual double this[uint index]
        {
            get
            {
                if (IsEmptyPoint)
                    throw new ApplicationException("Point is empty");
                if (index == 0)
                    return X;
                if
                    (index == 1)
                    return Y;
                throw (new Exception("Point index out of bounds"));
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else
                    throw (new Exception("Point index out of bounds"));
            }
        }

        public virtual int NumOrdinates
        {
            get { return 2; }
        }

        #region IComparable<Point> Members

        public virtual int CompareTo(Point other)
        {
            if (X < other.X || X == other.X && Y < other.Y)
                return -1;
            if (X > other.X || X == other.X && Y > other.Y)
                return 1;
            return 0;
        }

        #endregion

        public double[] ToDoubleArray()
        {
            return new[] { _x, _y };
        }

        public static Point FromDMS(double longDegrees, double longMinutes, double longSeconds,
                                    double latDegrees, double latMinutes, double latSeconds)
        {
            return new Point(longDegrees + longMinutes / 60 + longSeconds / 3600,
                             latDegrees + latMinutes / 60 + latSeconds / 3600);
        }

        public Point AsPoint()
        {
            return new Point(_x, _y);
        }

        public PointF TransformToImage(Map map)
        {
            return Transform.WorldtoMap(this, map);
        }

        public new Point Clone()
        {
            return new Point(X, Y);
        }

        #region Operators

        public static Point operator +(Point v1, Point v2)
        {
            return new Point(v1.X + v2.X, v1.Y + v2.Y);
        }


        public static Point operator -(Point v1, Point v2)
        {
            return new Point(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Point operator *(Point m, double d)
        {
            return new Point(m.X * d, m.Y * d);
        }

        #endregion

        #region "Inherited methods from abstract class Geometry"

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.Point;
            }
        }

        public override int Dimension
        {
            get { return 0; }
        }

        public virtual bool Equals(Point p)
        {
            return p != null && p.X == _x && p.Y == _y && IsEmptyPoint == p.IsEmptyPoint;
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode() ^ IsEmptyPoint.GetHashCode();
        }

        protected bool IsEmptyPoint { get { return double.IsNaN(_x) || double.IsNaN(_y); } }

        public override bool IsEmpty()
        {
            return IsEmptyPoint;
        }

        public override bool IsSimple()
        {
            return true;
        }

        public override Geometry Boundary()
        {
            return null;
        }

        public override double Distance(Geometry geom)
        {
            if (geom.GetType() == typeof(Point))
            {
                var p = geom as Point;
                return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));
            }
            else if (geom is LineString)
            {
                return geom.Distance(this);
            }
            else if (geom is MultiLineString)
            {
                return geom.Distance(this);
            }
            else
                throw new Exception("The method or operation is not implemented for this geometry type.");
        }

        public double Distance(BoundingBox box)
        {
            return box.Distance(this);
        }

        public override Geometry Buffer(double d)
        {
            throw new NotImplementedException();
        }

        public override Geometry ConvexHull()
        {
            throw new NotImplementedException();
        }

        public override Geometry Intersection(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public override Geometry Union(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public override Geometry Difference(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public override Geometry SymDifference(Geometry geom)
        {
            throw new NotImplementedException();
        }

        public override BoundingBox GetBoundingBox()
        {
            return new BoundingBox(X, Y, X, Y);
        }

        public bool Touches(BoundingBox box)
        {
            return box.Touches(this);
        }

        public override bool Touches(Geometry geom)
        {
            if (geom is Point && Equals(geom)) return true;
            throw new NotImplementedException("Touches not implemented for this feature type");
        }

        public bool Intersects(BoundingBox box)
        {
            return box.Contains(this);
        }

        public override bool Contains(Geometry geom)
        {
            return false;
        }

        #endregion
    }
}
