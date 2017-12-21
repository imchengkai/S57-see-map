

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyMap.Utilities;
using System.Drawing;

namespace EasyMap.Geometries
{
    [Serializable]
    public class MultiLineString : MultiCurve
    {
        private IList<LineString> _LineStrings;

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
        public MultiLineString()
        {
            _LineStrings = new Collection<LineString>();
        }

        public IList<LineString> LineStrings
        {
            get { return _LineStrings; }
            set { _LineStrings = value; }
        }

        public new LineString this[int index]
        {
            get { return _LineStrings[index]; }
        }

        public override bool IsClosed
        {
            get
            {
                for (int i = 0; i < _LineStrings.Count; i++)
                    if (!_LineStrings[i].IsClosed)
                        return false;
                return true;
            }
        }

        public override double Length
        {
            get
            {
                double l = 0;
                for (int i = 0; i < _LineStrings.Count; i++)
                    l += _LineStrings[i].Length;
                return l;
            }
        }

        public override int NumGeometries
        {
            get { return _LineStrings.Count; }
        }

        public override bool IsEmpty()
        {
            if (_LineStrings == null || _LineStrings.Count == 0)
                return true;

            for (int i = 0; i < _LineStrings.Count; i++)
                if (!_LineStrings[i].IsEmpty())
                    return false;

            return true;
        }

        public override bool IsSimple()
        {
            throw new NotImplementedException();
        }

        public override Geometry Boundary()
        {
            throw new NotImplementedException();
        }

        public override double Distance(Geometry geom)
        {
            if (geom is Point)
            {
                Point coord = geom as Point;
                double minDist = double.MaxValue;
                foreach (var ls in _LineStrings)
                {
                    IList<Point> coord0 = ls.Vertices;
                    for (int i = 0; i < coord0.Count - 1; i++)
                    {
                        double dist = CGAlgorithms.DistancePointLine(coord, coord0[i], coord0[i + 1]);
                        if (dist < minDist)
                        {
                            minDist = dist;
                        }
                    }
                }
                return minDist;
            }
            else if (geom is LineString)
            {
                IList<Point> coord1 = (geom as LineString).Vertices;
                double _minDistance = double.MaxValue;
                foreach (var ls in _LineStrings)
                {
                    IList<Point> coord0 = ls.Vertices;
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

        public override Geometry Geometry(int N)
        {
            return _LineStrings[N];
        }

        public override BoundingBox GetBoundingBox()
        {
            if (_LineStrings == null || _LineStrings.Count == 0)
                return null;
            BoundingBox bbox = _LineStrings[0].GetBoundingBox();
            for (int i = 1; i < _LineStrings.Count; i++)
                bbox = bbox.Join(_LineStrings[i].GetBoundingBox());
            return bbox;
        }

        public new MultiLineString Clone()
        {
            MultiLineString geoms = new MultiLineString();
            for (int i = 0; i < _LineStrings.Count; i++)
                geoms.LineStrings.Add(_LineStrings[i].Clone());
            return geoms;
        }

        public override IEnumerator<Geometry> GetEnumerator()
        {
            foreach (LineString l in _LineStrings)
                yield return l;
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiLineString;
            }
        }

        public override bool IsSelect(Point p)
        {
            foreach (LineString line in _LineStrings)
            {
                if (line.IsSelect(p))
                {
                    return true;
                }
            }
            return false;
        }

        public double GetYByX(double x)
        {
            double y = 0;
            foreach (LineString line in _LineStrings)
            {
                y = line.GetYByX(x);
                if(y!=0)
                {
                    return y;
                }
            }
            return 0;
        }

        public double GetXByY(double y)
        {
            double x = 0;
            foreach (LineString line in _LineStrings)
            {
                x = line.GetXByY(y);
                if (y != 0)
                {
                    return x;
                }
            }
            return 0;
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
    }
}
