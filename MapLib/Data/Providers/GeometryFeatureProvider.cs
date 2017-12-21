
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using EasyMap.Geometries;

namespace EasyMap.Data.Providers
{
    public class GeometryFeatureProvider : FilterProvider, IProvider, IDisposable
    {
        private readonly FeatureDataTable _features;
        private int _SRID = -1;

        #region constructors

        public GeometryFeatureProvider(IEnumerable<Geometry> geometries)
        {
            _features = new FeatureDataTable();
            foreach (Geometry geom in geometries)
            {
                FeatureDataRow fdr = _features.NewRow();
                fdr.Geometry = geom;
                _features.AddRow(fdr);
            }
            _features.TableCleared += HandleFeaturesCleared;
        }

        public GeometryFeatureProvider(FeatureDataTable features)
        {
            _features = features;
            _features.TableCleared += HandleFeaturesCleared;
        }

        public GeometryFeatureProvider(Geometry geometry)
        {
            _features = new FeatureDataTable();
            FeatureDataRow fdr = _features.NewRow();
            fdr.Geometry = geometry;
            _features.AddRow(fdr);
            _features.TableCleared += HandleFeaturesCleared;
        }

        #endregion

        private void HandleFeaturesCleared(object sender, DataTableClearEventArgs e)
        {
        }

        public FeatureDataTable Features
        {
            get { return _features; }
        }

        #region IProvider Members

        public Collection<Geometry> GetGeometriesInView(BoundingBox bbox)
        {
            Collection<Geometry> list = new Collection<Geometry>();

            foreach (FeatureDataRow fdr in _features.Rows)
                if (!fdr.Geometry.IsEmpty())
                    if (FilterDelegate == null || FilterDelegate(fdr))
                    {
                        if (fdr.Geometry.GetBoundingBox().Intersects(bbox))
                            list.Add(fdr.Geometry);
                    }
            return list;
        }

        public Collection<uint> GetObjectIDsInView(BoundingBox bbox)
        {
            Collection<uint> list = new Collection<uint>();
            for (int i = 0; i < _features.Rows.Count; i++)
                if ((_features.Rows[i] as FeatureDataRow).Geometry.GetBoundingBox().Intersects(bbox))
                    list.Add((uint)i);
            return list;
        }

        public Geometry GetGeometryByID(uint oid)
        {
            return (_features.Rows[(int)oid] as FeatureDataRow).Geometry;
        }

        public void ExecuteIntersectionQuery(Geometry geom, FeatureDataSet ds)
        {
            FeatureDataTable fdt = new FeatureDataTable();
            fdt = _features.Clone();

            foreach (FeatureDataRow fdr in _features)
                if (FilterDelegate == null || FilterDelegate(fdr))
                {
                    if (fdr.Geometry.GetBoundingBox().Intersects(geom))
                    {
                        fdt.LoadDataRow(fdr.ItemArray, false);
                        (fdt.Rows[fdt.Rows.Count - 1] as FeatureDataRow).Geometry = fdr.Geometry;
                    }
                }

            ds.Tables.Add(fdt);
        }

        public void ExecuteIntersectionQuery(BoundingBox box, FeatureDataSet ds)
        {
            FeatureDataTable fdt = new FeatureDataTable();
            fdt = _features.Clone();

            foreach (FeatureDataRow fdr in _features)
                if (fdr.Geometry != null)
                {
                    if (FilterDelegate == null || FilterDelegate(fdr))
                    {
                        if (fdr.Geometry.GetBoundingBox().Intersects(box))
                        {
                            fdt.LoadDataRow(fdr.ItemArray, false);
                            (fdt.Rows[fdt.Rows.Count - 1] as FeatureDataRow).Geometry = fdr.Geometry;
                        }
                    }
                }

            ds.Tables.Add(fdt);
        }

        public int GetFeatureCount()
        {
            return _features.Rows.Count;
        }

        public FeatureDataRow GetFeature(uint rowId)
        {
            if (FilterDelegate == null || FilterDelegate(_features[(int)rowId]))
                return _features[(int)rowId];

            return null;
        }

        public BoundingBox GetExtents()
        {
            if (_features.Rows.Count == 0)
                return null;
            BoundingBox box = null;
            foreach (FeatureDataRow fdr in _features.Rows)
                if (fdr.Geometry != null && !fdr.Geometry.IsEmpty())
                    box = box == null ? fdr.Geometry.GetBoundingBox() : box.Join(fdr.Geometry.GetBoundingBox());

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
            _features.Dispose();
        }

        #endregion
    }
}
