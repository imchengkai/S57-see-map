
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyMap.Converters.WellKnownBinary;
using EasyMap.Converters.WellKnownText;
using EasyMap.Geometries;

namespace EasyMap.Data.Providers
{
    public class GeometryProvider : IProvider, IDisposable
    {
        private IList<Geometry> _Geometries;
        private int _SRID = -1;

        public IList<Geometry> Geometries
        {
            get { return _Geometries; }
            set { _Geometries = value; }
        }

        #region constructors

        public GeometryProvider(IList<Geometry> geometries)
        {
            _Geometries = geometries;
        }

        public GeometryProvider(FeatureDataRow feature)
        {
            _Geometries = new Collection<Geometry>();
            _Geometries.Add(feature.Geometry);
        }

        public GeometryProvider(FeatureDataTable features)
        {
            _Geometries = new Collection<Geometry>();
            for (int i = 0; i < features.Count; i++)
                _Geometries.Add(features[i].Geometry);
        }

        public GeometryProvider(Geometry geometry)
        {
            _Geometries = new Collection<Geometry>();
            _Geometries.Add(geometry);
        }

        public GeometryProvider(byte[] wellKnownBinaryGeometry)
            : this(GeometryFromWKB.Parse(wellKnownBinaryGeometry))
        {
        }

        public GeometryProvider(string wellKnownTextGeometry)
            : this(GeometryFromWKT.Parse(wellKnownTextGeometry))
        {
        }

        #endregion

        #region IProvider Members

        public Collection<Geometry> GetGeometriesInView(BoundingBox bbox)
        {
            Collection<Geometry> list = new Collection<Geometry>();
            for (int i = 0; i < _Geometries.Count; i++)
                if (!_Geometries[i].IsEmpty())
                    if (_Geometries[i].GetBoundingBox().Intersects(bbox))
                        list.Add(_Geometries[i]);
            return list;
        }

        public Collection<uint> GetObjectIDsInView(BoundingBox bbox)
        {
            Collection<uint> list = new Collection<uint>();
            for (int i = 0; i < _Geometries.Count; i++)
                if (_Geometries[i].GetBoundingBox().Intersects(bbox))
                    list.Add((uint)i);
            return list;
        }

        public Geometry GetGeometryByID(uint oid)
        {
            return _Geometries[(int)oid];
        }

        public void ExecuteIntersectionQuery(Geometry geom, FeatureDataSet ds)
        {
            throw new NotSupportedException("Attribute data is not supported by the GeometryProvider.");
        }

        public void ExecuteIntersectionQuery(BoundingBox box, FeatureDataSet ds)
        {
            throw new NotSupportedException("Attribute data is not supported by the GeometryProvider.");
        }

        public int GetFeatureCount()
        {
            return _Geometries.Count;
        }

        public FeatureDataRow GetFeature(uint rowId)
        {
            throw new NotSupportedException("Attribute data is not supported by the GeometryProvider.");
        }

        public BoundingBox GetExtents()
        {
            if (_Geometries.Count == 0)
                return null;
            BoundingBox box = null; // _Geometries[0].GetBoundingBox();
            for (int i = 0; i < _Geometries.Count; i++)
                if (!_Geometries[i].IsEmpty())
                    box = box == null ? _Geometries[i].GetBoundingBox() : box.Join(_Geometries[i].GetBoundingBox());

            return box;
        }

        public string ConnectionID
        {
            get { return String.Empty; }
        }

        public void Open()
        {
        }

        public void Close()
        {
        }

        public bool IsOpen
        {
            get { return true; }
        }

        public int SRID
        {
            get { return _SRID; }
            set { _SRID = value; }
        }

        public void Dispose()
        {
            _Geometries = null;
        }

        #endregion
    }
}
