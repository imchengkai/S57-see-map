
using System;
using System.Collections.ObjectModel;
using System.Data;
using EasyMap.Geometries;

namespace EasyMap.Data.Providers
{

    public class DataTablePoint : IProvider, IDisposable
    {
        private string _ConnectionString;
        private string _defintionQuery;
        private bool _IsOpen;
        private string _ObjectIdColumn;
        private int _SRID = -1;
        private DataTable _Table;
        private string _XColumn;
        private string _YColumn;
        public DataTablePoint(DataTable dataTable, string oidColumnName,
                              string xColumn, string yColumn)
        {
            Table = dataTable;
            XColumn = xColumn;
            YColumn = yColumn;
            ObjectIdColumn = oidColumnName;
        }

        /// <summary>
        /// Data table used as the data source.
        /// </summary>
        public DataTable Table
        {
            get { return _Table; }
            set { _Table = value; }
        }


        /// <summary>
        /// Name of column that contains the Object ID
        /// </summary>
        public string ObjectIdColumn
        {
            get { return _ObjectIdColumn; }
            set { _ObjectIdColumn = value; }
        }

        /// <summary>
        /// Name of column that contains X coordinate
        /// </summary>
        public string XColumn
        {
            get { return _XColumn; }
            set { _XColumn = value; }
        }

        /// <summary>
        /// Name of column that contains Y coordinate
        /// </summary>
        public string YColumn
        {
            get { return _YColumn; }
            set { _YColumn = value; }
        }

        /// <summary>
        /// Connectionstring
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        /// <summary>
        /// Definition query used for limiting dataset
        /// </summary>
        public string DefinitionQuery
        {
            get { return _defintionQuery; }
            set { _defintionQuery = value; }
        }

        #region IProvider Members

        /// <summary>
        /// Returns geometries within the specified bounding box
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        public Collection<Geometry> GetGeometriesInView(BoundingBox bbox)
        {
            DataRow[] drow;
            Collection<Geometry> features = new Collection<Geometry>();

            if (Table.Rows.Count == 0)
            {
                return null;
            }

            string strSQL = XColumn + " > " + bbox.Left.ToString(Map.NumberFormatEnUs) + " AND " +
                            XColumn + " < " + bbox.Right.ToString(Map.NumberFormatEnUs) + " AND " +
                            YColumn + " > " + bbox.Bottom.ToString(Map.NumberFormatEnUs) + " AND " +
                            YColumn + " < " + bbox.Top.ToString(Map.NumberFormatEnUs);

            drow = Table.Select(strSQL);

            foreach (DataRow dr in drow)
            {
                features.Add(new Point((double) dr[XColumn], (double) dr[YColumn]));
            }

            return features;
        }

        /// <summary>
        /// Returns geometry Object IDs whose bounding box intersects 'bbox'
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        public Collection<uint> GetObjectIDsInView(BoundingBox bbox)
        {
            DataRow[] drow;
            Collection<uint> objectlist = new Collection<uint>();

            if (Table.Rows.Count == 0)
            {
                return null;
            }

            string strSQL = XColumn + " > " + bbox.Left.ToString(Map.NumberFormatEnUs) + " AND " +
                            XColumn + " < " + bbox.Right.ToString(Map.NumberFormatEnUs) + " AND " +
                            YColumn + " > " + bbox.Bottom.ToString(Map.NumberFormatEnUs) + " AND " +
                            YColumn + " < " + bbox.Top.ToString(Map.NumberFormatEnUs);

            drow = Table.Select(strSQL);

            foreach (DataRow dr in drow)
            {
                objectlist.Add((uint) (int) dr[0]);
            }

            return objectlist;
        }

        /// <summary>
        /// Returns the geometry corresponding to the Object ID
        /// </summary>
        /// <param name="oid">Object ID</param>
        /// <returns>geometry</returns>
        public Geometry GetGeometryByID(uint oid)
        {
            DataRow[] rows;
            Geometry geom = null;

            if (Table.Rows.Count == 0)
            {
                return null;
            }

            string selectStatement = ObjectIdColumn + " = " + oid;

            rows = Table.Select(selectStatement);

            foreach (DataRow dr in rows)
            {
                geom = new Point((double) dr[XColumn], (double) dr[YColumn]);
            }

            return geom;
        }

        /// <summary>
        /// Throws NotSupportedException. 
        /// </summary>
        /// <param name="geom"></param>
        /// <param name="ds">FeatureDataSet to fill data into</param>
        public void ExecuteIntersectionQuery(Geometry geom, FeatureDataSet ds)
        {
            throw new NotSupportedException("ExecuteIntersectionQuery(Geometry) is not supported by the DataTablePoint.");
            //When relation model has been implemented the following will complete the query
            /*
            ExecuteIntersectionQuery(geom.GetBoundingBox(), ds);
            if (ds.Tables.Count > 0)
            {
                for(int i=ds.Tables[0].Count-1;i>=0;i--)
                {
                    if (!geom.Intersects(ds.Tables[0][i].Geometry))
                        ds.Tables.RemoveAt(i);
                }
            }
            */
        }

        /// <summary>
        /// Retrieves all features within the given BoundingBox.
        /// </summary>
        /// <param name="bbox">Bounds of the region to search.</param>
        /// <param name="ds">FeatureDataSet to fill data into</param>
        public void ExecuteIntersectionQuery(BoundingBox bbox, FeatureDataSet ds)
        {
            DataRow[] rows;

            if (Table.Rows.Count == 0)
            {
                return;
            }

            string statement = XColumn + " > " + bbox.Left.ToString(Map.NumberFormatEnUs) + " AND " +
                               XColumn + " < " + bbox.Right.ToString(Map.NumberFormatEnUs) + " AND " +
                               YColumn + " > " + bbox.Bottom.ToString(Map.NumberFormatEnUs) + " AND " +
                               YColumn + " < " + bbox.Top.ToString(Map.NumberFormatEnUs);

            rows = Table.Select(statement);

            FeatureDataTable fdt = new FeatureDataTable(Table);

            foreach (DataColumn col in Table.Columns)
            {
                fdt.Columns.Add(col.ColumnName, col.DataType, col.Expression);
            }

            foreach (DataRow dr in rows)
            {
                fdt.ImportRow(dr);
                FeatureDataRow fdr = fdt.Rows[fdt.Rows.Count - 1] as FeatureDataRow;
                fdr.Geometry = new Point((double) dr[XColumn], (double) dr[YColumn]);
            }

            ds.Tables.Add(fdt);
        }

        /// <summary>
        /// Returns the number of features in the dataset
        /// </summary>
        /// <returns>Total number of features</returns>
        public int GetFeatureCount()
        {
            return Table.Rows.Count;
        }

        /// <summary>
        /// Returns a datarow based on a RowID
        /// </summary>
        /// <param name="rowId"></param>
        /// <returns>datarow</returns>
        public FeatureDataRow GetFeature(uint rowId)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Computes the full extents of the data source as a 
        /// <see cref="BoundingBox"/>.
        /// </summary>
        /// <returns>
        /// A BoundingBox instance which minimally bounds all the features
        /// available in this data source.
        /// </returns>
        public BoundingBox GetExtents()
        {
            if (Table.Rows.Count == 0)
            {
                return null;
            }

            BoundingBox box;

            double minX = Double.PositiveInfinity,
                   minY = Double.PositiveInfinity,
                   maxX = Double.NegativeInfinity,
                   maxY = Double.NegativeInfinity;

            foreach (DataRowView dr in Table.DefaultView)
            {
                if (minX > (double) dr[XColumn]) minX = (double) dr[XColumn];
                if (maxX < (double) dr[XColumn]) maxX = (double) dr[XColumn];
                if (minY > (double) dr[YColumn]) minY = (double) dr[YColumn];
                if (maxY < (double) dr[YColumn]) maxY = (double) dr[YColumn];
            }

            box = new BoundingBox(minX, minY, maxX, maxY);

            return box;
        }

        /// <summary>
        /// Gets the connection ID of the datasource.
        /// </summary>
        public string ConnectionID
        {
            get { return _ConnectionString; }
        }

        /// <summary>
        /// Opens the datasource.
        /// </summary>
        public void Open()
        {
            _IsOpen = true;
        }

        /// <summary>
        /// Closes the datasource.
        /// </summary>
        public void Close()
        {
            _IsOpen = false;
        }

        /// <summary>
        /// Gets true if the datasource is currently open.
        /// </summary>
        public bool IsOpen
        {
            get { return _IsOpen; }
        }

        /// <summary>
        /// The spatial reference ID (CRS)
        /// </summary>
        public int SRID
        {
            get { return _SRID; }
            set { _SRID = value; }
        }

        #endregion

        #region Disposers and finalizers

        private bool disposed = false;

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~DataTablePoint()
        {
            Dispose();
        }

        #endregion
    }
}