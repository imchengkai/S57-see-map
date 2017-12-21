using System;
using System.Collections.ObjectModel;
using System.IO;
using EasyMap.Geometries;

namespace EasyMap.Converters.WellKnownText
{
    public class GeometryFromWKT
    {
        public static Geometry Parse(string wellKnownText)
        {
            StringReader reader = new StringReader(wellKnownText);
            return Parse(reader);
        }

        public static Geometry Parse(TextReader reader)
        {
            WktStreamTokenizer tokenizer = new WktStreamTokenizer(reader);

            return ReadGeometryTaggedText(tokenizer);
        }

        private static Collection<Point> GetCoordinates(WktStreamTokenizer tokenizer)
        {
            Collection<Point> coordinates = new Collection<Point>();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken == "EMPTY")
                return coordinates;

            Point externalCoordinate = new Point();
            Point internalCoordinate = new Point();
            externalCoordinate.X = GetNextNumber(tokenizer);
            externalCoordinate.Y = GetNextNumber(tokenizer);
            coordinates.Add(externalCoordinate);
            nextToken = GetNextCloserOrComma(tokenizer);
            while (nextToken == ",")
            {
                internalCoordinate = new Point();
                internalCoordinate.X = GetNextNumber(tokenizer);
                internalCoordinate.Y = GetNextNumber(tokenizer);
                coordinates.Add(internalCoordinate);
                nextToken = GetNextCloserOrComma(tokenizer);
            }
            return coordinates;
        }


        private static double GetNextNumber(WktStreamTokenizer tokenizer)
        {
            tokenizer.NextToken();
            return tokenizer.GetNumericValue();
        }

        private static string GetNextEmptyOrOpener(WktStreamTokenizer tokenizer)
        {
            tokenizer.NextToken();
            string nextWord = tokenizer.GetStringValue();
            if (nextWord == "EMPTY" || nextWord == "(")
                return nextWord;

            throw new Exception("Expected 'EMPTY' or '(' but encountered '" + nextWord + "'");
        }

        private static string GetNextCloserOrComma(WktStreamTokenizer tokenizer)
        {
            tokenizer.NextToken();
            string nextWord = tokenizer.GetStringValue();
            if (nextWord == "," || nextWord == ")")
            {
                return nextWord;
            }
            throw new Exception("Expected ')' or ',' but encountered '" + nextWord + "'");
        }

        private static string GetNextCloser(WktStreamTokenizer tokenizer)
        {
            string nextWord = GetNextWord(tokenizer);
            if (nextWord == ")")
                return nextWord;

            throw new Exception("Expected ')' but encountered '" + nextWord + "'");
        }

        private static string GetNextWord(WktStreamTokenizer tokenizer)
        {
            TokenType type = tokenizer.NextToken();
            string token = tokenizer.GetStringValue();
            if (type == TokenType.Number)
                throw new Exception("Expected a number but got " + token);
            else if (type == TokenType.Word)
                return token.ToUpper();
            else if (token == "(")
                return "(";
            else if (token == ")")
                return ")";
            else if (token == ",")
                return ",";

            throw new Exception("Not a valid symbol in WKT format.");
        }

        private static Geometry ReadGeometryTaggedText(WktStreamTokenizer tokenizer)
        {
            tokenizer.NextToken();
            string type = tokenizer.GetStringValue().ToUpper();
            Geometry geometry = null;
            switch (type)
            {
                case "POINT":
                    geometry = ReadPointText(tokenizer);
                    break;
                case "LINESTRING":
                    geometry = ReadLineStringText(tokenizer);
                    break;
                case "MULTIPOINT":
                    geometry = ReadMultiPointText(tokenizer);
                    break;
                case "MULTILINESTRING":
                    geometry = ReadMultiLineStringText(tokenizer);
                    break;
                case "POLYGON":
                    geometry = ReadPolygonText(tokenizer);
                    break;
                case "MULTIPOLYGON":
                    geometry = ReadMultiPolygonText(tokenizer);
                    break;
                case "GEOMETRYCOLLECTION":
                    geometry = ReadGeometryCollectionText(tokenizer);
                    break;
                default:
                    throw new Exception(String.Format(Map.NumberFormatEnUs, "Geometrytype '{0}' is not supported.",
                                                      type));
            }
            return geometry;
        }

        private static MultiPolygon ReadMultiPolygonText(WktStreamTokenizer tokenizer)
        {
            MultiPolygon polygons = new MultiPolygon();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken == "EMPTY")
                return polygons;

            Polygon polygon = ReadPolygonText(tokenizer);
            polygons.Polygons.Add(polygon);
            nextToken = GetNextCloserOrComma(tokenizer);
            while (nextToken == ",")
            {
                polygon = ReadPolygonText(tokenizer);
                polygons.Polygons.Add(polygon);
                nextToken = GetNextCloserOrComma(tokenizer);
            }
            return polygons;
        }

        /// <summary>
        /// 取得多边形区域文字
        /// </summary>
        /// <param name="tokenizer"></param>
        /// <returns></returns>
        private static Polygon ReadPolygonText(WktStreamTokenizer tokenizer)
        {
            Polygon pol = new Polygon();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken == "EMPTY")
                return pol;

            pol.ExteriorRing = new LinearRing(GetCoordinates(tokenizer));
            nextToken = GetNextCloserOrComma(tokenizer);
            while (nextToken == ",")
            {
                //Add holes
                pol.InteriorRings.Add(new LinearRing(GetCoordinates(tokenizer)));
                nextToken = GetNextCloserOrComma(tokenizer);
            }
            return pol;
        }


        private static Point ReadPointText(WktStreamTokenizer tokenizer)
        {
            Point p = new Point();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken == "EMPTY")
                return p;
            p.X = GetNextNumber(tokenizer);
            p.Y = GetNextNumber(tokenizer);
            GetNextCloser(tokenizer);
            return p;
        }

        private static MultiPoint ReadMultiPointText(WktStreamTokenizer tokenizer)
        {
            MultiPoint mp = new MultiPoint();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken == "EMPTY")
                return mp;
            mp.Points.Add(new Point(GetNextNumber(tokenizer), GetNextNumber(tokenizer)));
            nextToken = GetNextCloserOrComma(tokenizer);
            while (nextToken == ",")
            {
                mp.Points.Add(new Point(GetNextNumber(tokenizer), GetNextNumber(tokenizer)));
                nextToken = GetNextCloserOrComma(tokenizer);
            }
            return mp;
        }

        private static MultiLineString ReadMultiLineStringText(WktStreamTokenizer tokenizer)
        {
            MultiLineString lines = new MultiLineString();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken == "EMPTY")
                return lines;

            lines.LineStrings.Add(ReadLineStringText(tokenizer));
            nextToken = GetNextCloserOrComma(tokenizer);
            while (nextToken == ",")
            {
                lines.LineStrings.Add(ReadLineStringText(tokenizer));
                nextToken = GetNextCloserOrComma(tokenizer);
            }
            return lines;
        }

        private static LineString ReadLineStringText(WktStreamTokenizer tokenizer)
        {
            return new LineString(GetCoordinates(tokenizer));
        }

        private static GeometryCollection ReadGeometryCollectionText(WktStreamTokenizer tokenizer)
        {
            GeometryCollection geometries = new GeometryCollection();
            string nextToken = GetNextEmptyOrOpener(tokenizer);
            if (nextToken.Equals("EMPTY"))
                return geometries;
            geometries.Collection.Add(ReadGeometryTaggedText(tokenizer));
            nextToken = GetNextCloserOrComma(tokenizer);
            while (nextToken.Equals(","))
            {
                geometries.Collection.Add(ReadGeometryTaggedText(tokenizer));
                nextToken = GetNextCloserOrComma(tokenizer);
            }
            return geometries;
        }
    }
}