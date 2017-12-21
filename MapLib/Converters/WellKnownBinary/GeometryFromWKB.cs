
using System;
using System.Diagnostics;
using System.IO;
using EasyMap.Geometries;

namespace EasyMap.Converters.WellKnownBinary
{
    public class GeometryFromWKB
    {
        public static Geometry Parse(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(ms))
                {
                    return Parse(reader);
                }
            }
        }

        public static Geometry Parse(BinaryReader reader)
        {
            Byte byteOrder = reader.ReadByte();

            UInt32 type = ReadUInt32(reader, (WkbByteOrder) byteOrder);

            switch ((WKBGeometryType) type)
            {
                case WKBGeometryType.wkbPoint:
                    return CreateWKBPoint(reader, (WkbByteOrder) byteOrder);

                case WKBGeometryType.wkbLineString:
                    return CreateWKBLineString(reader, (WkbByteOrder) byteOrder);

                case WKBGeometryType.wkbPolygon:
                    return CreateWKBPolygon(reader, (WkbByteOrder) byteOrder);

                case WKBGeometryType.wkbMultiPoint:
                    return CreateWKBMultiPoint(reader, (WkbByteOrder) byteOrder);

                case WKBGeometryType.wkbMultiLineString:
                    return CreateWKBMultiLineString(reader, (WkbByteOrder) byteOrder);

                case WKBGeometryType.wkbMultiPolygon:
                    return CreateWKBMultiPolygon(reader, (WkbByteOrder) byteOrder);

                case WKBGeometryType.wkbGeometryCollection:
                    return CreateWKBGeometryCollection(reader, (WkbByteOrder) byteOrder);

                default:
                    if (!Enum.IsDefined(typeof (WKBGeometryType), type))
                        throw new ArgumentException("Geometry type not recognized");
                    else
                        throw new NotSupportedException("Geometry type '" + type + "' not supported");
            }
        }

        private static Point CreateWKBPoint(BinaryReader reader, WkbByteOrder byteOrder)
        {
            return new Point(ReadDouble(reader, byteOrder), ReadDouble(reader, byteOrder));
        }

        private static Point[] ReadCoordinates(BinaryReader reader, WkbByteOrder byteOrder)
        {
            int numPoints = (int) ReadUInt32(reader, byteOrder);

            Point[] coords = new Point[numPoints];

            for (int i = 0; i < numPoints; i++)
            {
                coords[i] = new Point(ReadDouble(reader, byteOrder), ReadDouble(reader, byteOrder));
            }
            return coords;
        }

        private static LineString CreateWKBLineString(BinaryReader reader, WkbByteOrder byteOrder)
        {
            LineString l = new LineString();
            Point[] arrPoint = ReadCoordinates(reader, byteOrder);
            for (int i = 0; i < arrPoint.Length; i++)
                l.Vertices.Add(arrPoint[i]);

            return l;
        }

        private static LinearRing CreateWKBLinearRing(BinaryReader reader, WkbByteOrder byteOrder)
        {
            LinearRing l = new LinearRing();

            Point[] arrPoint = ReadCoordinates(reader, byteOrder);
            for (int i = 0; i < arrPoint.Length; i++)
                l.Vertices.Add(arrPoint[i]);

            if (l.Vertices[0].X != l.Vertices[l.Vertices.Count - 1].X ||
                l.Vertices[0].Y != l.Vertices[l.Vertices.Count - 1].Y)
                l.Vertices.Add(new Point(l.Vertices[0].X, l.Vertices[0].Y));
            return l;
        }

        private static Polygon CreateWKBPolygon(BinaryReader reader, WkbByteOrder byteOrder)
        {
            int numRings = (int) ReadUInt32(reader, byteOrder);

            Debug.Assert(numRings >= 1, "Number of rings in polygon must be 1 or more.");

            Polygon shell = new Polygon(CreateWKBLinearRing(reader, byteOrder));

            for (int i = 0; i < (numRings - 1); i++)
                shell.InteriorRings.Add(CreateWKBLinearRing(reader, byteOrder));

            return shell;
        }

        private static MultiPoint CreateWKBMultiPoint(BinaryReader reader, WkbByteOrder byteOrder)
        {
            int numPoints = (int) ReadUInt32(reader, byteOrder);

            MultiPoint points = new MultiPoint();

            for (int i = 0; i < numPoints; i++)
            {
                reader.ReadByte();
                ReadUInt32(reader, byteOrder);
                points.Points.Add(CreateWKBPoint(reader, byteOrder));
            }
            return points;
        }

        private static MultiLineString CreateWKBMultiLineString(BinaryReader reader, WkbByteOrder byteOrder)
        {
            int numLineStrings = (int) ReadUInt32(reader, byteOrder);

            MultiLineString mline = new MultiLineString();

            for (int i = 0; i < numLineStrings; i++)
            {
                reader.ReadByte();
                ReadUInt32(reader, byteOrder);

                mline.LineStrings.Add(CreateWKBLineString(reader, byteOrder));
            }

            return mline;
        }

        private static MultiPolygon CreateWKBMultiPolygon(BinaryReader reader, WkbByteOrder byteOrder)
        {
            int numPolygons = (int) ReadUInt32(reader, byteOrder);

            MultiPolygon polygons = new MultiPolygon();

            for (int i = 0; i < numPolygons; i++)
            {
                reader.ReadByte();
                ReadUInt32(reader, byteOrder);

                polygons.Polygons.Add(CreateWKBPolygon(reader, byteOrder));
            }

            return polygons;
        }

        private static Geometry CreateWKBGeometryCollection(BinaryReader reader, WkbByteOrder byteOrder)
        {
            int numGeometries = (int) ReadUInt32(reader, byteOrder);

            GeometryCollection geometries = new GeometryCollection();

            for (int i = 0; i < numGeometries; i++)
            {
                geometries.Collection.Add(Parse(reader));
            }

            return geometries;
        }

        private static uint ReadUInt32(BinaryReader reader, WkbByteOrder byteOrder)
        {
            if (byteOrder == WkbByteOrder.Xdr)
            {
                byte[] bytes = BitConverter.GetBytes(reader.ReadUInt32());
                Array.Reverse(bytes);
                return BitConverter.ToUInt32(bytes, 0);
            }
            else if (byteOrder == WkbByteOrder.Ndr)
                return reader.ReadUInt32();
            else
                throw new ArgumentException("Byte order not recognized");
        }

        private static double ReadDouble(BinaryReader reader, WkbByteOrder byteOrder)
        {
            if (byteOrder == WkbByteOrder.Xdr)
            {
                byte[] bytes = BitConverter.GetBytes(reader.ReadDouble());
                Array.Reverse(bytes);
                return BitConverter.ToDouble(bytes, 0);
            }
            else if (byteOrder == WkbByteOrder.Ndr)
                return reader.ReadDouble();
            else
                throw new ArgumentException("Byte order not recognized");
        }
    }
}