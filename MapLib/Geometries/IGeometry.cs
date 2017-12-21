

#if !DotSpatialProjections
using ProjNet.CoordinateSystems;
#else
using DotSpatial.Projections;
#endif

namespace EasyMap.Geometries
{
    public interface IGeometry : IGeometryClassifier
    {
        #region "Basic Methods on Geometry"

#if !DotSpatialProjections
        ICoordinateSystem SpatialReference { get; set; }
#else
        ProjectionInfo SpatialReference { get; set; }
#endif
        int Dimension { get; }

        Geometry Envelope();

        BoundingBox GetBoundingBox();

        string AsText();

        byte[] AsBinary();

        string ToString();

        bool IsEmpty();

        bool IsSimple();

        Geometry Boundary();

        bool Relate(Geometry other, string intersectionPattern);

        #endregion

        #region "Methods for testing Spatial Relations between geometric objects"

        bool Equals(Geometry geom);

        bool Disjoint(Geometry geom);

        bool Intersects(Geometry geom);

        bool Touches(Geometry geom);

        bool Crosses(Geometry geom);

        bool Within(Geometry geom);

        bool Contains(Geometry geom);

        bool Overlaps(Geometry geom);

        #endregion

        #region "Methods that support Spatial Analysis"

        double Distance(Geometry geom);

        Geometry Buffer(double d);


        Geometry ConvexHull();

        Geometry Intersection(Geometry geom);

        Geometry Union(Geometry geom);

        Geometry Difference(Geometry geom);

        Geometry SymDifference(Geometry geom);

        #endregion
    }
}
