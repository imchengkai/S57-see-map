

#if !DotSpatialProjections

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyMap.Geometries;

namespace ProjNet.CoordinateSystems.Transformations
{
    public class GeometryTransform
    {
        public static BoundingBox TransformBox(BoundingBox box, IMathTransform transform)
        {
            if (box == null)
                return null;
            Point[] corners = new Point[4];
            var ll = box.Min.ToDoubleArray();
            var ur = box.Max.ToDoubleArray();
            var llTrans = transform.Transform(ll);
            var urTrans = transform.Transform(ur);
            corners[0] = new Point(llTrans);                //lower left
            corners[2] = new Point(llTrans[0], urTrans[1]); //upper left
            corners[1] = new Point(urTrans);                //upper right
            corners[3] = new Point(urTrans[0], llTrans[1]); //lower right

            BoundingBox result = corners[0].GetBoundingBox();
            for (int i = 1; i < 4; i++)
                result = result.Join(corners[i].GetBoundingBox());
            return result;
        }

        public static Geometry TransformGeometry(Geometry g, IMathTransform transform)
        {
            if (g == null)
                return null;
            if (g is Point)
                return TransformPoint(g as Point, transform);
            if (g is LineString)
                return TransformLineString(g as LineString, transform);
            if (g is Polygon)
                return TransformPolygon(g as Polygon, transform);
            if (g is MultiPoint)
                return TransformMultiPoint(g as MultiPoint, transform);
            if (g is MultiLineString)
                return TransformMultiLineString(g as MultiLineString, transform);
            if (g is MultiPolygon)
                return TransformMultiPolygon(g as MultiPolygon, transform);
            if (g is GeometryCollection)
                return TransformGeometryCollection(g as GeometryCollection, transform);
            throw new ArgumentException("Could not transform geometry type '" + g.GetType() + "'");
        }

        public static Point TransformPoint(Point p, IMathTransform transform)
        {
            try
            {
                return new Point(transform.Transform(p.ToDoubleArray()));
            }
            catch
            {
                return null;
            }
        }

        public static LineString TransformLineString(LineString l, IMathTransform transform)
        {
            try
            {
                List<double[]> points = new List<double[]>();

                for (int i = 0; i < l.Vertices.Count; i++)
                    points.Add(new double[2] { l.Vertices[i].X, l.Vertices[i].Y });

                return new LineString(transform.TransformList(points));
            }
            catch
            {
                return null;
            }
        }

        public static LinearRing TransformLinearRing(LinearRing r, IMathTransform transform)
        {
            try
            {
                List<double[]> points = new List<double[]>();

                for (int i = 0; i < r.Vertices.Count; i++)
                    points.Add(new double[2] { r.Vertices[i].X, r.Vertices[i].Y });

                return new LinearRing(transform.TransformList(points));
            }
            catch
            {
                return null;
            }
        }

        public static Polygon TransformPolygon(Polygon p, IMathTransform transform)
        {
            Polygon pOut = new Polygon(TransformLinearRing(p.ExteriorRing, transform));
            pOut.InteriorRings = new Collection<LinearRing>();
            for (int i = 0; i < p.InteriorRings.Count; i++)
                pOut.InteriorRings.Add(TransformLinearRing(p.InteriorRings[i], transform));
            return pOut;
        }

        public static MultiPoint TransformMultiPoint(MultiPoint points, IMathTransform transform)
        {
            List<double[]> pts = new List<double[]>();
            for (int i = 0; i < points.NumGeometries; i++)
                pts.Add(new double[2] { points[i].X, points[i].Y });

            return new MultiPoint(transform.TransformList(pts));
        }

        public static MultiLineString TransformMultiLineString(MultiLineString lines, IMathTransform transform)
        {
            MultiLineString lOut = new MultiLineString();
            lOut.LineStrings = new Collection<LineString>(); //Pre-inialize array size for better performance
            for (int i = 0; i < lines.LineStrings.Count; i++)
                lOut.LineStrings.Add(TransformLineString(lines[i], transform));
            return lOut;
        }

        public static MultiPolygon TransformMultiPolygon(MultiPolygon polys, IMathTransform transform)
        {
            MultiPolygon pOut = new MultiPolygon();
            pOut.Polygons = new Collection<Polygon>();
            for (int i = 0; i < polys.NumGeometries; i++)
                pOut.Polygons.Add(TransformPolygon(polys[i], transform));
            return pOut;
        }

        public static GeometryCollection TransformGeometryCollection(GeometryCollection geoms, IMathTransform transform)
        {
            GeometryCollection gOut = new GeometryCollection();
            gOut.Collection = new Collection<Geometry>(); //Pre-inialize array size for better performance
            for (int i = 0; i < geoms.Collection.Count; i++)
                gOut.Collection.Add(TransformGeometry(geoms[i], transform));
            return gOut;
        }
    }
}

#endif
