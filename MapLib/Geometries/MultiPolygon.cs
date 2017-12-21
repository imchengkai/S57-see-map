

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using EasyMap.Data;

namespace EasyMap.Geometries
{
    [Serializable]
    public class MultiPolygon : MultiSurface
    {
        private IList<Polygon> _Polygons;

        public Point Center
        {
            get
            {
                Point minpoint = new Point(_Polygons[0].ExteriorRing.Vertices[0].X, _Polygons[0].ExteriorRing.Vertices[0].Y);
                Point maxpoint = new Point(_Polygons[0].ExteriorRing.Vertices[0].X, _Polygons[0].ExteriorRing.Vertices[0].Y);
                foreach (Polygon pol in _Polygons)
                {
                    foreach (EasyMap.Geometries.Point point in pol.ExteriorRing.Vertices)
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
                }
                Point center = new Point(0, 0);
                center.X = (minpoint.X + maxpoint.X) / 2;
                center.Y = (minpoint.Y + maxpoint.Y) / 2;
                return center;
            }
        }

        public MultiPolygon()
        {
            _Polygons = new Collection<Polygon>();
        }

        public IList<Polygon> Polygons
        {
            get { return _Polygons; }
            set { _Polygons = value; }
        }

        public new Polygon this[int index]
        {
            get { return _Polygons[index]; }
        }

        public override double Area
        {
            get
            {
                double result = 0;
                for (int i = 0; i < _Polygons.Count; i++)
                    result += _Polygons[i].Area;
                return result;
            }
        }

        public double Length
        {
            get
            {
                double len = 0;
                foreach (Polygon p in Polygons)
                {
                    len += p.ExteriorRing.Length;
                }
                return len;
            }
        }
        public override Point Centroid
        {
            get { throw new NotImplementedException(); }
        }

        public override Point PointOnSurface
        {
            get { throw new NotImplementedException(); }
        }

        public override int NumGeometries
        {
            get { return _Polygons.Count; }
        }

        public override bool IsEmpty()
        {
            if (_Polygons == null || _Polygons.Count == 0)
                return true;
            for (int i = 0; i < _Polygons.Count; i++)
                if (!_Polygons[i].IsEmpty())
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
            return _Polygons[N];
        }

        public override BoundingBox GetBoundingBox()
        {
            if (_Polygons == null || _Polygons.Count == 0)
                return null;
            BoundingBox bbox = Polygons[0].GetBoundingBox();
            for (int i = 1; i < Polygons.Count; i++)
                bbox = bbox.Join(Polygons[i].GetBoundingBox());
            return bbox;
        }

        public new MultiPolygon Clone()
        {
            MultiPolygon geoms = new MultiPolygon();
            for (int i = 0; i < _Polygons.Count; i++)
                geoms.Polygons.Add(_Polygons[i].Clone());
            return geoms;
        }

        public override IEnumerator<Geometry> GetEnumerator()
        {
            foreach (Polygon p in _Polygons)
                yield return p;
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiPolygon;
            }
        }

        public bool InPoly(Point p)
        {
            foreach (Polygon poloygn in _Polygons)
            {
                if (poloygn.InPoly(p))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
