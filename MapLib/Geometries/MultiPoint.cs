

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace EasyMap.Geometries
{
    public class MultiPoint : GeometryCollection, IPuntal
    {
        private IList<Point> _Points;
        public MultiPoint()
        {
            _Points = new Collection<Point>();
        }

        public MultiPoint(IEnumerable<double[]> points)
        {
            _Points = new Collection<Point>();
            foreach (double[] point in points)
                _Points.Add(new Point(point[0], point[1]));
        }

        public new Point this[int n]
        {
            get { return _Points[n]; }
        }

        public IList<Point> Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        public override int NumGeometries
        {
            get { return _Points.Count; }
        }

        public override int Dimension
        {
            get { return 0; }
        }

        public new Point Geometry(int N)
        {
            return _Points[N];
        }

        public override bool IsEmpty()
        {
            return (_Points != null && _Points.Count == 0);
        }

        public override bool IsSimple()
        {
            throw new NotImplementedException();
        }

        public override Geometry Boundary()
        {
            return null;
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

        public override BoundingBox GetBoundingBox()
        {
            if (_Points == null || _Points.Count == 0)
                return null;
            BoundingBox bbox = new BoundingBox(_Points[0], _Points[0]);
            for (int i = 1; i < _Points.Count; i++)
            {
                bbox.Min.X = _Points[i].X < bbox.Min.X ? _Points[i].X : bbox.Min.X;
                bbox.Min.Y = _Points[i].Y < bbox.Min.Y ? _Points[i].Y : bbox.Min.Y;
                bbox.Max.X = _Points[i].X > bbox.Max.X ? _Points[i].X : bbox.Max.X;
                bbox.Max.Y = _Points[i].Y > bbox.Max.Y ? _Points[i].Y : bbox.Max.Y;
            }
            return bbox;
        }

        public new MultiPoint Clone()
        {
            MultiPoint geoms = new MultiPoint();
            for (int i = 0; i < _Points.Count; i++)
                geoms.Points.Add(_Points[i].Clone());
            return geoms;
        }

        public override IEnumerator<Geometry> GetEnumerator()
        {
            foreach (Point p in _Points)
                yield return p;
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.MultiPoint;
            }
        }

    }
}
