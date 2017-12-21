

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EasyMap.Geometries
{
    [Serializable]
    public class GeometryCollection : Geometry, IGeometryCollection, IEnumerable<Geometry>
    {
        private IList<Geometry> _Geometries;

        public GeometryCollection()
        {
            _Geometries = new Collection<Geometry>();
        }

        public virtual Geometry this[int index]
        {
            get { return _Geometries[index]; }
        }

        public virtual IList<Geometry> Collection
        {
            get { return _Geometries; }
            set { _Geometries = value; }
        }

        #region IEnumerable<Geometry> Members

        public virtual IEnumerator<Geometry> GetEnumerator()
        {
            foreach (Geometry g in Collection)
                yield return g;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Geometry g in Collection)
                yield return g;
        }

        #endregion

        #region IGeometryCollection Members

        public virtual int NumGeometries
        {
            get { return _Geometries.Count; }
        }

        public virtual Geometry Geometry(int N)
        {
            return _Geometries[N];
        }

        public override bool IsEmpty()
        {
            if (_Geometries == null)
                return true;
            for (int i = 0; i < _Geometries.Count; i++)
                if (!_Geometries[i].IsEmpty())
                    return false;
            return true;
        }

        public override int Dimension
        {
            get
            {
                int dim = 0;
                for (int i = 0; i < Collection.Count; i++)
                    dim = (dim < Collection[i].Dimension ? Collection[i].Dimension : dim);
                return dim;
            }
        }

        public override BoundingBox GetBoundingBox()
        {
            if (Collection.Count == 0)
                return null;
            BoundingBox b = this[0].GetBoundingBox();
            for (int i = 0; i < Collection.Count; i++)
                b = b.Join(Collection[i].GetBoundingBox());
            return b;
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

        public bool Equals(GeometryCollection g)
        {
            if (g == null)
                return false;
            if (g.Collection.Count != Collection.Count)
                return false;
            for (int i = 0; i < g.Collection.Count; i++)
                if (!g.Collection[i].Equals((Geometry)Collection[i]))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < _Geometries.Count; i++)
                hash = hash ^ _Geometries[i].GetHashCode();
            return hash;
        }

        public new GeometryCollection Clone()
        {
            GeometryCollection geoms = new GeometryCollection();
            for (int i = 0; i < _Geometries.Count; i++)
                geoms.Collection.Add((Geometry)_Geometries[i].Clone());
            return geoms;
        }

        public override GeometryType2 GeometryType
        {
            get
            {
                return GeometryType2.GeometryCollection;
            }
        }
    }
}
