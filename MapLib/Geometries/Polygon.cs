

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using EasyMap.Utilities;
using EasyMap.Data;

namespace EasyMap.Geometries
{
    [Serializable]
    public class Polygon : Surface
    {
        private LinearRing _ExteriorRing;
        private IList<LinearRing> _InteriorRings;

        public Point Center
        {
            get
            {
                Point minpoint = new Point(ExteriorRing.Vertices[0].X, ExteriorRing.Vertices[0].Y);
                Point maxpoint = new Point(ExteriorRing.Vertices[0].X, ExteriorRing.Vertices[0].Y);

                foreach (Point point in ExteriorRing.Vertices)
                {
                    if (point.X < minpoint.X)
                        minpoint.X = point.X;
                    if (point.Y < minpoint.Y)
                        minpoint.Y = point.Y;
                    if (point.X > maxpoint.X)
                        maxpoint.X = point.X;
                    if (point.Y > maxpoint.Y)
                        maxpoint.Y = point.Y;
                }
                Point center = new Point(0, 0);
                center.X = (minpoint.X + maxpoint.X) / 2;
                center.Y = (minpoint.Y + maxpoint.Y) / 2;
                return center;
            }
        }

        public Polygon(LinearRing exteriorRing, IList<LinearRing> interiorRings)
        {
            _ExteriorRing = exteriorRing;
            _InteriorRings = interiorRings ?? new Collection<LinearRing>();
        }

        public Polygon(LinearRing exteriorRing)
            : this(exteriorRing, new Collection<LinearRing>())
        {
        }

        public Polygon()
            : this(new LinearRing(), new Collection<LinearRing>())
        {
        }

        public LinearRing ExteriorRing
        {
            get { return _ExteriorRing; }
            set { _ExteriorRing = value; }
        }

        public IList<LinearRing> InteriorRings
        {
            get { return _InteriorRings; }
            set { _InteriorRings = value; }
        }

        public int NumInteriorRing
        {
            get { return _InteriorRings.Count; }
        }

        public override double Area
        {
            get
            {
                double area = 0.0;
                area += _ExteriorRing.Area;
                bool extIsClockwise = _ExteriorRing.IsCCW();
                for (int i = 0; i < _InteriorRings.Count; i++)
                    if (_InteriorRings[i].IsCCW() != extIsClockwise)
                        area -= _InteriorRings[i].Area;
                    else
                        area += _InteriorRings[i].Area;
                return area;
            }
        }

        public override Point Centroid
        {
            get { return ExteriorRing.GetBoundingBox().GetCentroid(); }
        }

        public override Point PointOnSurface
        {
            get { throw new NotImplementedException(); }
        }

        public LinearRing InteriorRing(int N)
        {
            return _InteriorRings[N];
        }

        public PointF[] TransformToImage(Map map)
        {
            int vertices = _ExteriorRing.Vertices.Count;
            for (int i = 0; i < _InteriorRings.Count; i++)
                vertices += _InteriorRings[i].Vertices.Count;

            PointF[] v = new PointF[vertices];
            for (int i = 0; i < _ExteriorRing.Vertices.Count; i++)
                v[i] = Transform.WorldtoMap(_ExteriorRing.Vertices[i], map);
            int j = _ExteriorRing.Vertices.Count;
            for (int k = 0; k < _InteriorRings.Count; k++)
            {
                for (int i = 0; i < _InteriorRings[k].Vertices.Count; i++)
                    v[j + i] = Transform.WorldtoMap(_InteriorRings[k].Vertices[i], map);
                j += _InteriorRings[k].Vertices.Count;
            }
            return v;
        }

        public override BoundingBox GetBoundingBox()
        {
            if (_ExteriorRing == null || _ExteriorRing.Vertices.Count == 0) return null;
            BoundingBox bbox = new BoundingBox(_ExteriorRing.Vertices[0], _ExteriorRing.Vertices[0]);
            for (int i = 1; i < _ExteriorRing.Vertices.Count; i++)
            {
                bbox.Min.X = Math.Min(_ExteriorRing.Vertices[i].X, bbox.Min.X);
                bbox.Min.Y = Math.Min(_ExteriorRing.Vertices[i].Y, bbox.Min.Y);
                bbox.Max.X = Math.Max(_ExteriorRing.Vertices[i].X, bbox.Max.X);
                bbox.Max.Y = Math.Max(_ExteriorRing.Vertices[i].Y, bbox.Max.Y);
            }
            return bbox;
        }

        public new Polygon Clone()
        {
            Polygon p = new Polygon();
            p.ExteriorRing = (LinearRing)_ExteriorRing.Clone();
            for (int i = 0; i < _InteriorRings.Count; i++)
                p.InteriorRings.Add(_InteriorRings[i].Clone() as LinearRing);
            return p;
        }

        #region "Inherited methods from abstract class Geometry"
        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.Polygon;
            }
        }

        public bool Equals(Polygon p)
        {
            if (p == null)
                return false;
            if (!p.ExteriorRing.Equals(ExteriorRing))
                return false;
            if (p.InteriorRings.Count != InteriorRings.Count)
                return false;
            for (int i = 0; i < p.InteriorRings.Count; i++)
                if (!p.InteriorRings[i].Equals(InteriorRings[i]))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = ExteriorRing.GetHashCode();
            ;
            for (int i = 0; i < InteriorRings.Count; i++)
                hash = hash ^ InteriorRings[i].GetHashCode();
            return hash;
        }

        public override bool IsEmpty()
        {
            return (ExteriorRing == null) || (ExteriorRing.Vertices.Count == 0);
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

        public bool InPoly(Polygon geom)
        {
            foreach (Point p in geom._ExteriorRing.Vertices)
            {
                if (!InPoly(p))
                {
                    return false;
                }
            }
            return true;
        }

        public bool InPoly(Point p)
        {
            List<Point> poly = new List<Point>();
            foreach (Point point in _ExteriorRing.Vertices)
            {
                poly.Add(point);
            }
            int i = 0, f = 0;
            double xi = 0, a = 0, b = 0, c = 0;
            Point ps, pe;
            for (i = 0; i < poly.Count; i++)
            {
                ps = poly[i];
                if (i < poly.Count - 1)
                {
                    pe = poly[i + 1];
                }
                else
                {
                    pe = poly[0];
                }
                GetStdLine(ps, pe, ref a, ref b, ref c);
                if (a != 0)
                {
                    xi = 0 - ((b * p.Y + c) / a);
                    if (xi == p.X)
                    {
                        return true;
                    }
                    else if (xi < p.X)
                    {
                        f = f + Sgn(pe.Y - p.Y) - Sgn(ps.Y - p.Y);
                    }
                }
            }
            return f != 0;
        }

        private void GetStdLine(Point ps, Point pe, ref double a, ref double b, ref double c)
        {
            double xs, ys, xe, ye;
            double p1, p2;
            xs = ps.X;
            ys = ps.Y;
            xe = pe.X;
            ye = pe.Y;
            p1 = (xs * ye);
            p2 = (xe * ys);
            if (p1 == p2)
            {
                if (xs == 0)
                {
                    if (xe == 0)
                    {
                        a = 1;
                        b = 0;
                        c = 0;
                    }
                    else if (ys == 0)
                    {
                        a = ye;
                        b = 0 - xe;
                        c = 0;
                    }
                }
                else if (ye == 0)
                {
                    if (ys == 0)
                    {
                        a = 0;
                        b = 1;
                        c = 0;
                    }
                    else if (xe == 0)
                    {
                        a = 0 - ys;
                        b = xs;
                        c = 0;
                    }
                }
            }
            else
            {
                a = (ys - ye) / (p1 - p2);
                c = 1;
                if (ys == 0)
                {
                    if (ye == 0)
                    {
                        b = 1;
                        c = 0;
                    }
                    else
                    {
                        b = 0 - ((a * xe + 1) / ye);
                    }
                }
                else
                {
                    b = 0 - ((a * xs + 1) / ys);
                }
            }
        }

        private int Sgn(double a)
        {
            if (a == 0)
            {
                return 0;
            }
            else if (a < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
