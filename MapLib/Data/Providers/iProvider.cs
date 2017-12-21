

using System;
using System.Collections.ObjectModel;
using EasyMap.Geometries;

namespace EasyMap.Data.Providers
{
    public interface IProvider : IDisposable
    {
        string ConnectionID { get; }

        bool IsOpen { get; }

        int SRID { get; set; }

        Collection<Geometry> GetGeometriesInView(BoundingBox bbox);

        Collection<uint> GetObjectIDsInView(BoundingBox bbox);

        Geometry GetGeometryByID(uint oid);

        void ExecuteIntersectionQuery(Geometry geom, FeatureDataSet ds);

        void ExecuteIntersectionQuery(BoundingBox box, FeatureDataSet ds);

        int GetFeatureCount();

        FeatureDataRow GetFeature(uint rowId);

        BoundingBox GetExtents();

        void Open();

        void Close();
    }
}
