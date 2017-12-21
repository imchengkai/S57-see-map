

using System;
using EasyMap.Converters.WellKnownBinary;
using EasyMap.Converters.WellKnownText;
#if !DotSpatialProjections
using ProjNet.CoordinateSystems;
using System.Drawing;
using EasyMap.Data;
using System.Collections.Generic;
using EasyMap.Styles;
#else
using DotSpatial.Projections;
#endif
namespace EasyMap.Geometries
{
    [Serializable]
    public abstract class Geometry : IGeometry, IEquatable<Geometry>
    {
        public int StyleType { get; set; }
        public bool EnableOutline { get; set; }
        public int Fill { get; set; }
        public int Line { get; set; }
        public int DashStyle { get; set; }
        public int Outline { get; set; }
        public int HatchStyle { get; set; }
        public int OutlineWidth { get; set; }
        public int Penstyle { get; set; }
        private decimal _ID = 0;
        private string _Text = "";
        private bool _Visible = true;
        private bool _Select = false;
        private Color _SelectColor = Color.Red;
        private Color _SelectFillColor = Color.Transparent;
        private uint _GeometryId;
        private string _Message = "";
        public Image PointSymbol { get; set; }
        public Image PointSelectSymbol { get; set; }
        public decimal LayerId { get; set; }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public uint GeometryId
        {
            get { return _GeometryId; }
            set { _GeometryId = value; }
        }

        public Color SelectFillColor
        {
            get { return _SelectFillColor; }
            set { _SelectFillColor = value; }
        }
        private Color _TextColor = Color.Yellow;
        private Font _TextFont = new Font("", 9);
        private List<PropertyData> _Properties = new List<PropertyData>();

        public List<PropertyData> Properties
        {
            get { return _Properties; }
            set { _Properties = value; }
        }

        public Font TextFont
        {
            get { return _TextFont; }
            set { _TextFont = value; }
        }

        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }

        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

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
        public Color SelectColor
        {
            get { return _SelectColor; }
            set
            {
                _SelectColor = value;

            }
        }

        public bool Select
        {
            get { return _Select; }
            set
            {
                _Select = value;
            }
        }
#if !DotSpatialProjections
        private ICoordinateSystem _SpatialReference;
#else
        private ProjectionInfo _SpatialReference;
#endif
        #region IGeometry Members

#if !DotSpatialProjections
        public ICoordinateSystem SpatialReference
#else
        public ProjectionInfo SpatialReference
#endif
        {
            get { return _SpatialReference; }
            set { _SpatialReference = value; }
        }


        public virtual bool Equals(Geometry other)
        {
            return SpatialRelations.Equals(this, other);
        }

        #endregion

        #region "Basic Methods on Geometry"

        public abstract int Dimension { get; }

        public object UserData { get; set; }


        public Geometry Envelope()
        {
            BoundingBox box = GetBoundingBox();
            Polygon envelope = new Polygon();
            envelope.ExteriorRing.Vertices.Add(box.Min); //minx miny
            envelope.ExteriorRing.Vertices.Add(new Point(box.Max.X, box.Min.Y)); //maxx minu
            envelope.ExteriorRing.Vertices.Add(box.Max); //maxx maxy
            envelope.ExteriorRing.Vertices.Add(new Point(box.Min.X, box.Max.Y)); //minx maxy
            envelope.ExteriorRing.Vertices.Add(envelope.ExteriorRing.StartPoint); //close ring
            return envelope;
        }


        public abstract BoundingBox GetBoundingBox();

        public string AsText()
        {
            return GeometryToWKT.Write(this);
        }

        public byte[] AsBinary()
        {
            return GeometryToWKB.Write(this);
        }

        public override string ToString()
        {
            return AsText();
        }

        public abstract bool IsEmpty();

        public abstract bool IsSimple();

        public abstract Geometry Boundary();

        public static Geometry GeomFromText(string WKT)
        {
            return GeometryFromWKT.Parse(WKT);
        }

        public static Geometry GeomFromWKB(byte[] WKB)
        {
            return GeometryFromWKB.Parse(WKB);
        }

        public virtual GeometryType2 GeometryType
        {
            get { return GeometryType2.Geometry; }
        }

        #endregion

        #region "Methods for testing Spatial Relations between geometric objects"

        public virtual bool Disjoint(Geometry geom)
        {
            return SpatialRelations.Disjoint(this, geom);
        }

        public virtual bool Intersects(Geometry geom)
        {
            return SpatialRelations.Intersects(this, geom);
        }

        public virtual bool Touches(Geometry geom)
        {
            return SpatialRelations.Touches(this, geom);
        }

        public virtual bool Crosses(Geometry geom)
        {
            return SpatialRelations.Crosses(this, geom);
        }

        public virtual bool Within(Geometry geom)
        {
            return SpatialRelations.Within(this, geom);
        }

        public virtual bool Contains(Geometry geom)
        {
            return SpatialRelations.Contains(this, geom);
        }

        public virtual bool Overlaps(Geometry geom)
        {
            return SpatialRelations.Overlaps(this, geom);
        }


        public bool Relate(Geometry other, string intersectionPattern)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Methods that support Spatial Analysis"

        public abstract double Distance(Geometry geom);

        public abstract Geometry Buffer(double d);


        public abstract Geometry ConvexHull();

        public abstract Geometry Intersection(Geometry geom);

        public abstract Geometry Union(Geometry geom);

        public abstract Geometry Difference(Geometry geom);

        public abstract Geometry SymDifference(Geometry geom);

        #endregion

        public Geometry Clone()
        {
            throw (new ApplicationException("Clone() has not been implemented on derived datatype"));
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                Geometry g = obj as Geometry;
                if (g == null)
                    return false;
                else
                    return Equals(g);
            }
        }

        public override int GetHashCode()
        {
            return AsBinary().GetHashCode();
        }
    }
}
