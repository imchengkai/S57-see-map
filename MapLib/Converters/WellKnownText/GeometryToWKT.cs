using System;
using System.IO;
using EasyMap.Geometries;

namespace EasyMap.Converters.WellKnownText
{
    public class GeometryToWKT
    {
        #region Methods

        public static string Write(IGeometry geometry)
        {
            StringWriter sw = new StringWriter();
            Write(geometry, sw);
            return sw.ToString();
        }

        public static void Write(IGeometry geometry, StringWriter writer)
        {
            AppendGeometryTaggedText(geometry, writer);
        }

        private static void AppendGeometryTaggedText(IGeometry geometry, StringWriter writer)
        {
            if (geometry == null)
                throw new NullReferenceException("Cannot write Well-Known Text: geometry was null");
            ;
            if (geometry is Point)
            {
                Point point = geometry as Point;
                AppendPointTaggedText(point, writer);
            }
            else if (geometry is LineString)
                AppendLineStringTaggedText(geometry as LineString, writer);
            else if (geometry is Polygon)
                AppendPolygonTaggedText(geometry as Polygon, writer);
            else if (geometry is MultiPoint)
                AppendMultiPointTaggedText(geometry as MultiPoint, writer);
            else if (geometry is MultiLineString)
                AppendMultiLineStringTaggedText(geometry as MultiLineString, writer);
            else if (geometry is MultiPolygon)
                AppendMultiPolygonTaggedText(geometry as MultiPolygon, writer);
            else if (geometry is GeometryCollection)
                AppendGeometryCollectionTaggedText(geometry as GeometryCollection, writer);
            else
                throw new NotSupportedException("Unsupported Geometry implementation:" + geometry.GetType().Name);
        }

        private static void AppendPointTaggedText(Point coordinate, StringWriter writer)
        {
            writer.Write("POINT ");
            AppendPointText(coordinate, writer);
        }

        private static void AppendLineStringTaggedText(LineString lineString, StringWriter writer)
        {
            writer.Write("LINESTRING ");
            AppendLineStringText(lineString, writer);
        }

        private static void AppendPolygonTaggedText(Polygon polygon, StringWriter writer)
        {
            writer.Write("POLYGON ");
            AppendPolygonText(polygon, writer);
        }

        private static void AppendMultiPointTaggedText(MultiPoint multipoint, StringWriter writer)
        {
            writer.Write("MULTIPOINT ");
            AppendMultiPointText(multipoint, writer);
        }
        private static void AppendMultiLineStringTaggedText(MultiLineString multiLineString, StringWriter writer)
        {
            writer.Write("MULTILINESTRING ");
            AppendMultiLineStringText(multiLineString, writer);
        }

        private static void AppendMultiPolygonTaggedText(MultiPolygon multiPolygon, StringWriter writer)
        {
            writer.Write("MULTIPOLYGON ");
            AppendMultiPolygonText(multiPolygon, writer);
        }

        private static void AppendGeometryCollectionTaggedText(GeometryCollection geometryCollection,
                                                               StringWriter writer)
        {
            writer.Write("GEOMETRYCOLLECTION ");
            AppendGeometryCollectionText(geometryCollection, writer);
        }

        private static void AppendPointText(Point coordinate, StringWriter writer)
        {
            if (coordinate == null || coordinate.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                AppendCoordinate(coordinate, writer);
                writer.Write(")");
            }
        }

        private static void AppendCoordinate(Point coordinate, StringWriter writer)
        {
            for (uint i = 0; i < coordinate.NumOrdinates; i++)
                writer.Write(WriteNumber(coordinate[i]) + (i < coordinate.NumOrdinates - 1 ? " " : ""));
        }

        private static string WriteNumber(double d)
        {
            return d.ToString(Map.NumberFormatEnUs);
        }

        private static void AppendLineStringText(LineString lineString, StringWriter writer)
        {
            if (lineString == null || lineString.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < lineString.NumPoints; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendCoordinate(lineString.Vertices[i], writer);
                }
                writer.Write(")");
            }
        }

        private static void AppendPolygonText(Polygon polygon, StringWriter writer)
        {
            if (polygon == null || polygon.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                AppendLineStringText(polygon.ExteriorRing, writer);
                for (int i = 0; i < polygon.InteriorRings.Count; i++)
                {
                    writer.Write(", ");
                    AppendLineStringText(polygon.InteriorRings[i], writer);
                }
                writer.Write(")");
            }
        }

        private static void AppendMultiPointText(MultiPoint multiPoint, StringWriter writer)
        {
            if (multiPoint == null || multiPoint.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < multiPoint.Points.Count; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendCoordinate(multiPoint[i], writer);
                }
                writer.Write(")");
            }
        }

        private static void AppendMultiLineStringText(MultiLineString multiLineString, StringWriter writer)
        {
            if (multiLineString == null || multiLineString.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < multiLineString.LineStrings.Count; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendLineStringText(multiLineString[i], writer);
                }
                writer.Write(")");
            }
        }

        private static void AppendMultiPolygonText(MultiPolygon multiPolygon, StringWriter writer)
        {
            if (multiPolygon == null || multiPolygon.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < multiPolygon.Polygons.Count; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendPolygonText(multiPolygon[i], writer);
                }
                writer.Write(")");
            }
        }

        private static void AppendGeometryCollectionText(GeometryCollection geometryCollection, StringWriter writer)
        {
            if (geometryCollection == null || geometryCollection.IsEmpty())
                writer.Write("EMPTY");
            else
            {
                writer.Write("(");
                for (int i = 0; i < geometryCollection.Collection.Count; i++)
                {
                    if (i > 0)
                        writer.Write(", ");
                    AppendGeometryTaggedText(geometryCollection[i], writer);
                }
                writer.Write(")");
            }
        }

        #endregion
    }
}