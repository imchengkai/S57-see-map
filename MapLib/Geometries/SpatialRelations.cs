

using System;

namespace EasyMap.Geometries
{
    public class SpatialRelations
    {
        public static bool Contains(Geometry sourceGeometry, Geometry otherGeometry)
        {
            return (otherGeometry.Within(sourceGeometry));
        }

        public static bool Crosses(Geometry g1, Geometry g2)
        {
            Geometry g = g2.Intersection(g1);
            return (g.Intersection(g1).Dimension < Math.Max(g1.Dimension, g2.Dimension) && !g.Equals(g1) &&
                    !g.Equals(g2));
        }

        public static bool Disjoint(Geometry g1, Geometry g2)
        {
            return !g2.Intersects(g1);
        }

        public static bool Equals(Geometry g1, Geometry g2)
        {
            if (g1 == null && g2 == null)
                return true;
            if (g1 == null || g2 == null)
                return false;
            if (g1.GetType() != g2.GetType())
                return false;
            if (g1 is Point)
                return (g1 as Point).Equals(g2 as Point);
            if (g1 is LineString)
                return (g1 as LineString).Equals(g2 as LineString);
            if (g1 is Polygon)
                return (g1 as Polygon).Equals(g2 as Polygon);
            if (g1 is MultiPoint)
                return (g1 as MultiPoint).Equals(g2 as MultiPoint);
            if (g1 is MultiLineString)
                return (g1 as MultiLineString).Equals(g2 as MultiLineString);
            if (g1 is MultiPolygon)
                return (g1 as MultiPolygon).Equals(g2 as MultiPolygon);
            throw new ArgumentException("The method or operation is not implemented on this geometry type.");
        }


        public static bool Intersects(Geometry g1, Geometry g2)
        {
            throw new NotImplementedException();
        }

        public static bool Overlaps(Geometry g1, Geometry g2)
        {
            throw new NotImplementedException();
        }

        public static bool Touches(Geometry g1, Geometry g2)
        {
            throw new NotImplementedException();
        }

        public static bool Within(Geometry g1, Geometry g2)
        {
            return g1.Contains(g2);
        }
    }
}
