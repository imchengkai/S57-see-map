

using System;

namespace EasyMap.Geometries
{
    [Serializable]
    public class Point3D : Point
    {
        private double _z;

        public Point3D(double x, double y, double z)
            : base(x, y)
        {
            _z = z;
        }

        public Point3D(Point p, double z)
            : base(p.X, p.Y)
        {
            _z = z;
        }

        public Point3D()
            : this(NullOrdinate, NullOrdinate, NullOrdinate)
        {
        }

        public Point3D(double[] point)
            : base(point[0], point[1])
        {
            if (point.Length != 3)
                throw new Exception("Only 3 dimensions are supported for points");

            _z = point[2];
        }

        public double Z
        {
            get
            {
                if (!IsEmptyPoint)
                    return _z;
                throw new ApplicationException("Point is empty");
            }
            set
            {
                _z = value;
                if (IsEmptyPoint)
                    SetIsEmpty = false;
            }
        }

        public override double this[uint index]
        {
            get
            {
                if (index == 2)
                {
                    if (IsEmptyPoint)
                        throw new ApplicationException("Point is empty");
                    return Z;
                }
                return base[index];
            }
            set
            {
                if (index == 2)
                {
                    Z = value;
                }
                else base[index] = value;
            }
        }

        public override int NumOrdinates
        {
            get { return 3; }
        }

        #region Operators

        public static Point3D operator +(Point3D v1, Point3D v2)
        {
            return new Point3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }


        public static Point3D operator -(Point3D v1, Point3D v2)
        {
            return new Point3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Point3D operator *(Point3D m, double d)
        {
            return new Point3D(m.X * d, m.Y * d, m.Z * d);
        }

        #endregion

        #region "Inherited methods from abstract class Geometry"

        public bool Equals(Point3D p)
        {
            return base.Equals(p) && p.Z == _z;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _z.GetHashCode();
        }

        public override double Distance(Geometry geom)
        {
            if (geom.GetType() == typeof(Point3D))
            {
                Point3D p = geom as Point3D;
                return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2) + Math.Pow(Z - p.Z, 2));
            }
            return base.Distance(geom);
        }

        #endregion

        public new double[] ToDoubleArray()
        {
            return new[] { X, Y, Z };
        }

        public new Point3D Clone()
        {
            return new Point3D(X, Y, Z);
        }

        public bool Equals(Point3D p1, Point3D p2)
        {
            return (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z);
        }

        public virtual int CompareTo(Point3D other)
        {
            if (X < other.X || X == other.X && Y < other.Y || X == other.X && Y == other.Y && Z < other.Z)
                return -1;
            if (X > other.X || X == other.X && Y > other.Y || X == other.X && Y == other.Y && Z > other.Z)
                return 1;
            return 0;
        }
    }
}
