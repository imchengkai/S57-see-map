

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using EasyMap.Geometries;

namespace EasyMap.Data
{
    [Serializable()]
    public class FeatureDataSet : DataSet
    {
        private FeatureTableCollection _FeatureTables;

        public FeatureDataSet()
        {
            InitClass();
            CollectionChangeEventHandler schemaChangedHandler = new CollectionChangeEventHandler(SchemaChanged);
            Relations.CollectionChanged += schemaChangedHandler;
            InitClass();
        }

        protected FeatureDataSet(SerializationInfo info, StreamingContext context)
        {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null))
            {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new StringReader(strSchema)));
                if ((ds.Tables["FeatureTable"] != null))
                {
                    Tables.Add(new FeatureDataTable(ds.Tables["FeatureTable"]));
                }
                DataSetName = ds.DataSetName;
                Prefix = ds.Prefix;
                Namespace = ds.Namespace;
                Locale = ds.Locale;
                CaseSensitive = ds.CaseSensitive;
                EnforceConstraints = ds.EnforceConstraints;
                Merge(ds, false, MissingSchemaAction.Add);
            }
            else
            {
                InitClass();
            }
            GetSerializationData(info, context);
            CollectionChangeEventHandler schemaChangedHandler = new CollectionChangeEventHandler(SchemaChanged);
            Relations.CollectionChanged += schemaChangedHandler;
        }

        public new FeatureTableCollection Tables
        {
            get { return _FeatureTables; }
        }

        public new FeatureDataSet Clone()
        {
            FeatureDataSet cln = ((FeatureDataSet)(base.Clone()));
            return cln;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            DataSetName = ds.DataSetName;
            Prefix = ds.Prefix;
            Namespace = ds.Namespace;
            Locale = ds.Locale;
            CaseSensitive = ds.CaseSensitive;
            EnforceConstraints = ds.EnforceConstraints;
            Merge(ds, false, MissingSchemaAction.Add);
        }

        protected override XmlSchema GetSchemaSerializable()
        {
            MemoryStream stream = new MemoryStream();
            WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return XmlSchema.Read(new XmlTextReader(stream), null);
        }


        private void InitClass()
        {
            _FeatureTables = new FeatureTableCollection();
            Prefix = "";
            Namespace = "http://tempuri.org/FeatureDataSet.xsd";
            Locale = new CultureInfo("en-US");
            CaseSensitive = false;
            EnforceConstraints = true;
        }

        private bool ShouldSerializeFeatureTable()
        {
            return false;
        }

        private void SchemaChanged(object sender, CollectionChangeEventArgs e)
        {
            if ((e.Action == CollectionChangeAction.Remove))
            {
            }
        }
    }

    public delegate void FeatureDataRowChangeEventHandler(object sender, FeatureDataRowChangeEventArgs e);

    [DebuggerStepThrough()]
    [Serializable()]
    public class FeatureDataTable : DataTable, IEnumerable
    {
        public FeatureDataTable()
            : base()
        {
            InitClass();
        }

        public FeatureDataTable(DataTable table)
            : base(table.TableName)
        {
            if (table.DataSet != null)
            {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive))
                {
                    CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
                {
                    Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace))
                {
                    Namespace = table.Namespace;
                }
            }

            Prefix = table.Prefix;
            MinimumCapacity = table.MinimumCapacity;
            DisplayExpression = table.DisplayExpression;
        }

        [Browsable(false)]
        public int Count
        {
            get { return base.Rows.Count; }
        }

        public FeatureDataRow this[int index]
        {
            get { return (FeatureDataRow)base.Rows[index]; }
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return base.Rows.GetEnumerator();
        }

        #endregion

        public event FeatureDataRowChangeEventHandler FeatureDataRowChanged;

        public event FeatureDataRowChangeEventHandler FeatureDataRowChanging;

        public event FeatureDataRowChangeEventHandler FeatureDataRowDeleted;

        public event FeatureDataRowChangeEventHandler FeatureDataRowDeleting;

        public void AddRow(FeatureDataRow row)
        {
            base.Rows.Add(row);
        }

        public new FeatureDataTable Clone()
        {
            FeatureDataTable cln = ((FeatureDataTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }

        protected override DataTable CreateInstance()
        {
            return new FeatureDataTable();
        }

        internal void InitVars()
        {
        }

        private void InitClass()
        {
        }

        public new FeatureDataRow NewRow()
        {
            return (FeatureDataRow)base.NewRow();
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new FeatureDataRow(builder);
        }

        protected override Type GetRowType()
        {
            return typeof(FeatureDataRow);
        }

        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if ((FeatureDataRowChanged != null))
            {
                FeatureDataRowChanged(this, new FeatureDataRowChangeEventArgs(((FeatureDataRow)(e.Row)), e.Action));
            }
        }

        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            base.OnRowChanging(e);
            if ((FeatureDataRowChanging != null))
            {
                FeatureDataRowChanging(this, new FeatureDataRowChangeEventArgs(((FeatureDataRow)(e.Row)), e.Action));
            }
        }

        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            base.OnRowDeleted(e);
            if ((FeatureDataRowDeleted != null))
            {
                FeatureDataRowDeleted(this, new FeatureDataRowChangeEventArgs(((FeatureDataRow)(e.Row)), e.Action));
            }
        }

        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            base.OnRowDeleting(e);
            if ((FeatureDataRowDeleting != null))
            {
                FeatureDataRowDeleting(this, new FeatureDataRowChangeEventArgs(((FeatureDataRow)(e.Row)), e.Action));
            }
        }


        public void RemoveRow(FeatureDataRow row)
        {
            base.Rows.Remove(row);
        }
    }

    [Serializable()]
    public class FeatureTableCollection : List<FeatureDataTable>
    {
    }

    [DebuggerStepThrough()]
    [Serializable()]
    public class FeatureDataRow : DataRow
    {

        private Geometry _Geometry;

        internal FeatureDataRow(DataRowBuilder rb)
            : base(rb)
        {
        }

        public Geometry Geometry
        {
            get { return _Geometry; }
            set { _Geometry = value; }
        }

        public bool IsFeatureGeometryNull()
        {
            return Geometry == null;
        }

        public void SetFeatureGeometryNull()
        {
            Geometry = null;
        }
    }

    [DebuggerStepThrough()]
    public class FeatureDataRowChangeEventArgs : EventArgs
    {
        private DataRowAction eventAction;
        private FeatureDataRow eventRow;

        public FeatureDataRowChangeEventArgs(FeatureDataRow row, DataRowAction action)
        {
            eventRow = row;
            eventAction = action;
        }

        public FeatureDataRow Row
        {
            get { return eventRow; }
        }

        public DataRowAction Action
        {
            get { return eventAction; }
        }
    }
}
