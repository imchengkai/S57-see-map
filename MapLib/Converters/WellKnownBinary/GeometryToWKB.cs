using System;
using System.IO;
using EasyMap.Geometries;

namespace EasyMap.Converters.WellKnownBinary
{
    public class GeometryToWKB
    {
        public static byte[] Write(Geometry g)
        {
            return Write(g, WkbByteOrder.Ndr);
        }
        public static byte[] Write(Geometry g, WkbByteOrder wkbByteOrder)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write((byte) wkbByteOrder);

            WriteType(g, bw, wkbByteOrder);

            WriteGeometry(g, bw, wkbByteOrder);

            return ms.ToArray();
        }

        private static void WriteUInt32(UInt32 value, BinaryWriter writer, WkbByteOrder byteOrder)
        {
            if (byteOrder == WkbByteOrder.Xdr)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                Array.Reverse(bytes);
                writer.Write(BitConverter.ToUInt32(bytes, 0));
            }
            else
                writer.Write(value);
        }

        private static void WriteDouble(double value, BinaryWriter writer, WkbByteOrder byteOrder)
        {
            if (byteOrder == WkbByteOrder.Xdr)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                Array.Reverse(bytes);
                writer.Write(BitConverter.ToDouble(bytes, 0));
            }
            else
                writer.Write(value);
        }

        #region Methods

        private static void WriteType(Geometry geometry, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            switch (geometry.GetType().FullName)
            {
                case "EasyMap.Geometries.Point":
                    WriteUInt32((uint) WKBGeometryType.wkbPoint, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.LineString":
                    WriteUInt32((uint) WKBGeometryType.wkbLineString, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.Polygon":
                    WriteUInt32((uint) WKBGeometryType.wkbPolygon, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.MultiPoint":
                    WriteUInt32((uint) WKBGeometryType.wkbMultiPoint, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.MultiLineString":
                    WriteUInt32((uint) WKBGeometryType.wkbMultiLineString, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.MultiPolygon":
                    WriteUInt32((uint) WKBGeometryType.wkbMultiPolygon, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.GeometryCollection":
                    WriteUInt32((uint) WKBGeometryType.wkbGeometryCollection, bWriter, byteorder);
                    break;
                default:
                    throw new ArgumentException("Invalid Geometry Type");
            }
        }

        private static void WriteGeometry(Geometry geometry, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            switch (geometry.GetType().FullName)
            {
                case "EasyMap.Geometries.Point":
                    WritePoint((Point) geometry, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.LineString":
                    LineString ls = (LineString) geometry;
                    WriteLineString(ls, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.Polygon":
                    WritePolygon((Polygon) geometry, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.MultiPoint":
                    WriteMultiPoint((MultiPoint) geometry, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.MultiLineString":
                    WriteMultiLineString((MultiLineString) geometry, bWriter, byteorder);
                    break;
                case "EasyMap.Geometries.MultiPolygon":
                    WriteMultiPolygon((MultiPolygon) geometry, bWriter, byteorder);
                    break;
                    //Write the Geometrycollection.
                case "EasyMap.Geometries.GeometryCollection":
                    WriteGeometryCollection((GeometryCollection) geometry, bWriter, byteorder);
                    break;
                    //If the type is not of the above 7 throw an exception.
                default:
                    throw new ArgumentException("Invalid Geometry Type");
            }
        }

        private static void WritePoint(Point point, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            WriteDouble(point.X, bWriter, byteorder);
            WriteDouble(point.Y, bWriter, byteorder);
        }


        private static void WriteLineString(LineString ls, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            WriteUInt32((uint) ls.Vertices.Count, bWriter, byteorder);

            foreach (Point p in ls.Vertices)
                WritePoint(p, bWriter, byteorder);
        }

        private static void WritePolygon(Polygon poly, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            int numRings = poly.InteriorRings.Count + 1;

            WriteUInt32((uint) numRings, bWriter, byteorder);

            WriteLineString(poly.ExteriorRing, bWriter, byteorder);

            foreach (LinearRing lr in poly.InteriorRings)
                WriteLineString(lr, bWriter, byteorder);
        }

        private static void WriteMultiPoint(MultiPoint mp, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            WriteUInt32((uint) mp.Points.Count, bWriter, byteorder);

            foreach (Point p in mp.Points)
            {
                bWriter.Write((byte) byteorder);
                WriteUInt32((uint) WKBGeometryType.wkbPoint, bWriter, byteorder);
                WritePoint(p, bWriter, byteorder);
            }
        }

        private static void WriteMultiLineString(MultiLineString mls, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            WriteUInt32((uint) mls.LineStrings.Count, bWriter, byteorder);

            foreach (LineString ls in mls.LineStrings)
            {
                bWriter.Write((byte) byteorder);
                WriteUInt32((uint) WKBGeometryType.wkbLineString, bWriter, byteorder);
                WriteLineString(ls, bWriter, byteorder);
            }
        }

        private static void WriteMultiPolygon(MultiPolygon mp, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            WriteUInt32((uint) mp.Polygons.Count, bWriter, byteorder);

            foreach (Polygon poly in mp.Polygons)
            {
                bWriter.Write((byte) byteorder);
                WriteUInt32((uint) WKBGeometryType.wkbPolygon, bWriter, byteorder);
                WritePolygon(poly, bWriter, byteorder);
            }
        }


        private static void WriteGeometryCollection(GeometryCollection gc, BinaryWriter bWriter, WkbByteOrder byteorder)
        {
            int numGeometries = gc.NumGeometries;

            WriteUInt32((uint) numGeometries, bWriter, byteorder);

            for (int i = 0; i < numGeometries; i++)
            {
                bWriter.Write((byte) byteorder);
                WriteType(gc[i], bWriter, byteorder);
                WriteGeometry(gc[i], bWriter, byteorder);
            }
        }

        #endregion
    }
}