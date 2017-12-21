

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using EasyMap.Utilities;

namespace EasyMap.Geometries
{
    [Serializable]
    public class LineString : Curve
    {
        private IList<Point> _Vertices;

        private decimal _ID = 0;

        public decimal ID
        {
            get
            {
                return _ID; ;
            }
            set
            {
                _ID = value;
            }
        }
        public LineString(IList<Point> vertices)
        {
            _Vertices = vertices;
        }

        public LineString()
            : this(new Collection<Point>())
        {
        }

        public LineString(IEnumerable<double[]> points)
        {
            Collection<Point> vertices = new Collection<Point>();

            foreach (double[] point in points)
                vertices.Add(new Point(point));

            _Vertices = vertices;
        }

        public virtual IList<Point> Vertices
        {
            get { return _Vertices; }
            set { _Vertices = value; }
        }

        public override Point StartPoint
        {
            get
            {
                if (_Vertices.Count == 0)
                    throw new ApplicationException("No startpoint found: LineString has no vertices.");
                return _Vertices[0];
            }
        }

        public override Point EndPoint
        {
            get
            {
                if (_Vertices.Count == 0)
                    throw new ApplicationException("No endpoint found: LineString has no vertices.");
                return _Vertices[_Vertices.Count - 1];
            }
        }

        public override bool IsRing
        {
            get { return (IsClosed && IsSimple()); }
        }

        public override double Length
        {
            get
            {
                if (Vertices.Count < 2)
                    return 0;
                double sum = 0;
                for (int i = 1; i < Vertices.Count; i++)
                    sum += Vertices[i].Distance(Vertices[i - 1]);
                return sum;
            }
        }

        #region OpenGIS Methods

        public virtual int NumPoints
        {
            get { return _Vertices.Count; }
        }

        public Point Point(int N)
        {
            return _Vertices[N];
        }

        #endregion

        public PointF[] TransformToImage(Map map)
        {
            PointF[] v = new PointF[_Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++)
                v[i] = Transform.WorldtoMap(_Vertices[i], map);
            return v;
        }

        public override Point Value(double t)
        {
            throw new NotImplementedException();
        }

        public override BoundingBox GetBoundingBox()
        {
            if (Vertices == null || Vertices.Count == 0)
                return null;
            BoundingBox bbox = new BoundingBox(Vertices[0], Vertices[0]);
            for (int i = 1; i < Vertices.Count; i++)
            {
                bbox.Min.X = Vertices[i].X < bbox.Min.X ? Vertices[i].X : bbox.Min.X;
                bbox.Min.Y = Vertices[i].Y < bbox.Min.Y ? Vertices[i].Y : bbox.Min.Y;
                bbox.Max.X = Vertices[i].X > bbox.Max.X ? Vertices[i].X : bbox.Max.X;
                bbox.Max.Y = Vertices[i].Y > bbox.Max.Y ? Vertices[i].Y : bbox.Max.Y;
            }
            return bbox;
        }

        public new LineString Clone()
        {
            LineString l = new LineString();
            for (int i = 0; i < _Vertices.Count; i++)
                l.Vertices.Add(_Vertices[i].Clone());
            return l;
        }

        #region "Inherited methods from abstract class Geometry"

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.LineString;
            }
        }

        public bool Equals(LineString l)
        {
            if (l == null)
                return false;
            if (l.Vertices.Count != Vertices.Count)
                return false;
            for (int i = 0; i < l.Vertices.Count; i++)
                if (!l.Vertices[i].Equals(Vertices[i]))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < Vertices.Count; i++)
                hash = hash ^ Vertices[i].GetHashCode();
            return hash;
        }

        public override bool IsEmpty()
        {
            return _Vertices == null || _Vertices.Count == 0;
        }

        public override bool IsSimple()
        {
            Collection<Point> verts = new Collection<Point>();

            for (int i = 0; i < _Vertices.Count; i++)
                if (0 != verts.IndexOf(_Vertices[i]))
                    verts.Add(_Vertices[i]);

            return (verts.Count == _Vertices.Count - (IsClosed ? 1 : 0));
        }

        public override Geometry Boundary()
        {
            throw new NotImplementedException();
        }

        public override double Distance(Geometry geom)
        {
            if (geom is Point)
            {
                IList<Point> coord0 = Vertices;
                Point coord = geom as Point;
                double minDist = double.MaxValue;
                for (int i = 0; i < coord0.Count - 1; i++)
                {
                    double dist = CGAlgorithms.DistancePointLine(coord, coord0[i], coord0[i + 1]);
                    if (dist < minDist)
                    {
                        minDist = dist;
                    }
                }
                return minDist;
            }
            else if (geom is LineString)
            {
                IList<Point> coord0 = Vertices;
                IList<Point> coord1 = (geom as LineString).Vertices;
                double _minDistance = double.MaxValue;
                for (int i = 0; i < coord0.Count - 1; i++)
                {
                    for (int j = 0; j < coord1.Count - 1; j++)
                    {
                        double dist = CGAlgorithms.DistanceLineLine(
                                                        coord0[i], coord0[i + 1],
                                                        coord1[j], coord1[j + 1]);
                        if (dist < _minDistance)
                        {
                            _minDistance = dist;
                        }
                    }
                }
                return _minDistance;
            }

            throw new NotImplementedException();
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

        #endregion

        public override bool IsSelect(Point p)
        {
            double step = 2;
            string sstep = Environment.GetEnvironmentVariable("STEP");
            double.TryParse(sstep, out step);
            if (_Vertices.Count < 2)
            {
                return false;
            }
            for (int i = 0; i < _Vertices.Count-1; i++)
            {
                double y = (p.X - _Vertices[i].X) * (_Vertices[i + 1].Y - _Vertices[i].Y) / (_Vertices[i + 1].X - _Vertices[i].X) + _Vertices[i].Y;
                if (Math.Abs(p.Y - y) < step)
                {
                    if (_Vertices[i + 1].Y > y && _Vertices[i].Y < y)
                    {
                        return true;
                    }
                    if (_Vertices[i + 1].Y < y && _Vertices[i].Y > y)
                    {
                        return true;
                    }
                }
                double x = (p.Y - _Vertices[i].Y) * (_Vertices[i + 1].X - _Vertices[i].X) / (_Vertices[i + 1].Y - _Vertices[i].Y) + _Vertices[i].X;
                if (Math.Abs(p.X - x) < step)
                {
                    if (_Vertices[i + 1].X > x && _Vertices[i].X < x)
                    {
                        return true;
                    }
                    if (_Vertices[i + 1].X < x && _Vertices[i].X > x)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public double GetYByX(double x)
        {
            double y = 0;
            for (int i = 0; i < _Vertices.Count - 1; i++)
            {
                y = (x - _Vertices[i].X) * (_Vertices[i + 1].Y - _Vertices[i].Y) / (_Vertices[i + 1].X - _Vertices[i].X) + _Vertices[i].Y;
                if (_Vertices[i + 1].Y >= y && _Vertices[i].Y <= y)
                {
                    return y;
                }
                if (_Vertices[i + 1].Y <= y && _Vertices[i].Y >= y)
                {
                    return y;
                }
            }
            return y;
        }

        public double GetXByY(double y)
        {
            double x = 0;
            for (int i = 0; i < _Vertices.Count - 1; i++)
            {
                x = (y - _Vertices[i].Y) * (_Vertices[i + 1].X - _Vertices[i].X) / (_Vertices[i + 1].Y - _Vertices[i].Y) + _Vertices[i].X;
                if (_Vertices[i + 1].X >= x && _Vertices[i].X <= x)
                {
                    return x;
                }
                if (_Vertices[i + 1].X <= x && _Vertices[i].X >= x)
                {
                    return x;
                }
            }
            return x;
        }

        public float GetYByX(PointF[] points, float x)
        {
            float y = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                y = (x - points[i].X) * (points[i + 1].Y - points[i].Y) / (points[i + 1].X - points[i].X) + points[i].Y;
                if (points[i + 1].Y >= y && points[i].Y <= y)
                {
                    return y;
                }
                if (points[i + 1].Y <= y && points[i].Y >= y)
                {
                    return y;
                }
            }
            return y;
        }
        public float GetXByY(PointF[] points, float y)
        {
            float x = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                x = (y - points[i].Y) * (points[i + 1].X - points[i].X) / (points[i + 1].Y - points[i].Y) + points[i].X;
                if (points[i + 1].X >= x && points[i].X <= x)
                {
                    return x;
                }
                if (points[i + 1].X <= x && points[i].X >= x)
                {
                    return x;
                }
            }
            return x;
        }
        /*旅顺南路写接到名字，道路弯曲特殊处理使用
          20170110 liu 
         */
        public float GetYByXNew(PointF[] points, float x)
        {
            float y = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                if (points[i + 1].X >= x && points[i].X <= x)
                {
                    if (points[i + 1].X - x > x - points[i].X)
                    {
                        return points[i + 1].Y;
                    }
                    else
                    {
                        return points[i].Y;
                    }
                }
                if (points[i + 1].X <= x && points[i].X >= x)
                {
                    return points[i].Y;
                }
            }
            return y;
        }

        /*旅顺南路写接到名字，道路弯曲特殊处理使用
          20170110 liu 
         */
        public float GetXByYNew(PointF[] points, float y)
        {
            float x = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                if (points[i + 1].Y >= y && points[i].Y <= y)
                {
                    if (points[i + 1].Y - y > y - points[i].Y)
                    {
                        return points[i + 1].X;
                    }
                    else
                    {
                        return points[i].X;
                    }
                }
                if (points[i + 1].Y <= y && points[i].Y >= y)
                {
                    return points[i].X;
                }
            }
            return x;
        }
    }
}
