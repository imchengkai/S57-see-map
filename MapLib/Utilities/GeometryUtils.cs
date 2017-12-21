using EasyMap;
using EasyMap.Geometries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace MapLib.Utilities
{
    public class GeometryUtils
    {
        private static bool PolygnIsIn(IList<EasyMap.Geometries.Point> source, IList<EasyMap.Geometries.Point> dest)
        {
            List<PointF> p1 = new List<PointF>();
            source.ToList().ForEach(p => { p1.Add(new PointF((float)p.X, (float)p.Y)); });
            GraphicsPath gp1 = new GraphicsPath();
            gp1.AddPolygon(p1.ToArray());
            Region region = new Region(gp1);
            bool ret = true;
            foreach (EasyMap.Geometries.Point p in dest)
            {
                if (ret && !region.IsVisible((float)p.X, (float)p.Y))
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        private static bool IsIn2(Polygon area, Geometry geom, bool include)
        {

            bool find = false;
            if (geom is Polygon)
            {
                Polygon polygon = (Polygon)geom;
                if (include)
                {
                    find = true;
                    foreach (EasyMap.Geometries.Point point in polygon.ExteriorRing.Vertices)
                    {
                        if (!area.InPoly(point))
                        {
                            find = false;
                            break;
                        }
                    }
                }
                else
                {
                    find = false;
                    foreach (EasyMap.Geometries.Point point in polygon.ExteriorRing.Vertices)
                    {
                        if (area.InPoly(point))
                        {
                            find = true;
                            break;
                        }
                    }
                }
            }
            else if (geom is MultiPolygon)
            {
                MultiPolygon polygons = (MultiPolygon)geom;
                if (include)
                {
                    find = true;
                    foreach (Polygon polygon in polygons.Polygons)
                    {
                        foreach (EasyMap.Geometries.Point point in polygon.ExteriorRing.Vertices)
                        {
                            if (!area.InPoly(point))
                            {
                                find = false;
                                break;
                            }
                        }
                        if (!find)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    find = false;
                    foreach (Polygon polygon in polygons.Polygons)
                    {
                        foreach (EasyMap.Geometries.Point point in polygon.ExteriorRing.Vertices)
                        {
                            if (area.InPoly(point))
                            {
                                find = true;
                                break;
                            }
                        }
                        if (find)
                        {
                            break;
                        }
                    }
                }
            }
            else if (geom is EasyMap.Geometries.Point)
            {
                find = area.InPoly(geom as EasyMap.Geometries.Point);
            }
            else if (geom is LineString)
            {
                LineString line = geom as LineString;
                if (include)
                {
                    find = true;
                    foreach (EasyMap.Geometries.Point point in line.Vertices)
                    {
                        if (!area.InPoly(point))
                        {
                            find = false;
                            break;
                        }
                    }
                }
                else
                {
                    find = false;
                    foreach (EasyMap.Geometries.Point point in line.Vertices)
                    {
                        if (area.InPoly(point))
                        {
                            find = true;
                            break;
                        }
                    }
                }
            }
            else if (geom is LinearRing)
            {
                LinearRing line = geom as LinearRing;
                if (include)
                {
                    find = true;
                    foreach (EasyMap.Geometries.Point point in line.Vertices)
                    {
                        if (!area.InPoly(point))
                        {
                            find = false;
                            break;
                        }
                    }
                }
                else
                {
                    find = false;
                    foreach (EasyMap.Geometries.Point point in line.Vertices)
                    {
                        if (area.InPoly(point))
                        {
                            find = true;
                            break;
                        }
                    }
                }
            }
            else if (geom is MultiLineString)
            {
                MultiLineString lines = geom as MultiLineString;

                if (include)
                {
                    find = true;
                    foreach (LineString line in lines.LineStrings)
                    {
                        foreach (EasyMap.Geometries.Point point in line.Vertices)
                        {
                            if (area.InPoly(point))
                            {
                                find = false;
                                break;
                            }
                        }
                        if (!find)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    find = false;
                    foreach (LineString line in lines.LineStrings)
                    {
                        foreach (EasyMap.Geometries.Point point in line.Vertices)
                        {
                            if (area.InPoly(point))
                            {
                                find = true;
                                break;
                            }
                        }
                        if (find)
                        {
                            break;
                        }
                    }
                }
            }
            return find;
        }

        private static bool IsIn1(Geometry source, Geometry dest)
        {
            if (!source.GetBoundingBox().Contains(dest.GetBoundingBox()))
            {
                return false;
            }
            if (source is Polygon)
            {
                Polygon sourcepoly = source as Polygon;
                if (dest is Polygon)
                {
                    Polygon destpoly = dest as Polygon;
                    if (PolygnIsIn(sourcepoly.ExteriorRing.Vertices, destpoly.ExteriorRing.Vertices))
                    {
                        return true;
                    }
                }
                else if (dest is MultiPolygon)
                {
                    MultiPolygon destpoly = dest as MultiPolygon;
                    List<EasyMap.Geometries.Point> list = new List<EasyMap.Geometries.Point>();
                    destpoly.Polygons.ToList().ForEach(p => { list.AddRange(p.ExteriorRing.Vertices); });
                    if (PolygnIsIn(sourcepoly.ExteriorRing.Vertices, list))
                    {
                        return true;
                    }
                }
            }
            else if (source is MultiPolygon)
            {
                MultiPolygon sourcepoly = source as MultiPolygon;
                List<EasyMap.Geometries.Point> sourcelist = new List<EasyMap.Geometries.Point>();
                for (int i = 0; i < sourcepoly.Polygons.Count; i++)
                {
                    if (IsIn1(sourcepoly.Polygons[i], dest))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool PolygnIsIn(List<PointF> dest, GpcPolygon polygon)
        {
            GraphicsPath gp2 = new GraphicsPath();
            gp2.AddPolygon(dest.ToArray());
            List<PointF> dps = new List<PointF>();
            List<PointF> sps = new List<PointF>();
            polygon.Contour.ToList().ForEach(a => { dps.AddRange(a.ToPoints()); });
            polygon.Contour.ToList().ForEach(a => { sps.AddRange(a.ToPoints()); });
            sps.ForEach(a =>
            {
                int index = dps.FindIndex(d => { return a.X == d.X && a.Y == d.Y; });
                if (index >= 0)
                {
                    dps.RemoveAt(index);
                }
            });
            bool find = true;
            while (find && dps.Count > 0)
            {
                find = false;
                for (int j = 1; j < dps.Count; j++)
                {
                    if (dps[0].X == dps[j].X && dps[0].Y == dps[j].Y)
                    {
                        find = true;
                        dps.RemoveAt(j);
                        dps.RemoveAt(0);
                        break;
                    }
                }
            }
            return dps.Count == 0;
        }

        //public static bool PolygnIsIn(List<PointF> source, List<PointF> dest)
        //{
        //    GpcPolygon polygon = Intersection(source, dest);
        //    return PolygnIsIn(dest, polygon);
        //}

        public static GpcPolygon Intersection(List<List<PointF>> source, List<List<PointF>> dest)
        {
            GraphicsPath gp1 = new GraphicsPath();
            source.ForEach(a => { gp1.AddPolygon(a.ToArray()); });
            GpcPolygon polygonA = new GpcPolygon(gp1);
            GraphicsPath gp2 = new GraphicsPath();
            dest.ForEach(a => { gp2.AddPolygon(a.ToArray()); });
            GpcPolygon polygonB = new GpcPolygon(gp2);
            GpcPolygon polygon = polygonA.Clip(GpcOperation.Intersection, polygonB);
            return polygon;
        }
        public static GpcPolygon Intersection(EasyMap.Geometries.Point[] source, EasyMap.Geometries.Point[] dest)
        {
            GraphicsPath gp1 = new GraphicsPath();
            //source.ForEach(a => { gp1.AddPolygon(a.ToArray()); });
            GpcPolygon polygonA = new GpcPolygon(source);
            GraphicsPath gp2 = new GraphicsPath();
            //dest.ForEach(a => { gp2.AddPolygon(a.ToArray()); });
            GpcPolygon polygonB = new GpcPolygon(dest);
            GpcPolygon polygon = polygonA.Clip(GpcOperation.Intersection, polygonB);
            return polygon;
        }
        private static List<GpcPolygon> ConvertGpcPolygon(Geometry geom)
        {
            List<GpcPolygon> list = new List<GpcPolygon>();
            if (geom is Polygon)
            {
                Polygon polygon = geom as Polygon;
                List<PointF> ps = (from p in polygon.ExteriorRing.Vertices select new PointF((float)p.X, (float)p.Y)).ToList();
                GraphicsPath gp1 = new GraphicsPath();
                gp1.AddPolygon(ps.ToArray());
                GpcPolygon pp = new GpcPolygon(gp1);
                list.Add(pp);
            }
            else if(geom is MultiPolygon)
            {
                MultiPolygon polygons = geom as MultiPolygon;
                foreach (Polygon polygon in polygons.Polygons)
                {
                    List<PointF> ps = (from p in polygon.ExteriorRing.Vertices select new PointF((float)p.X, (float)p.Y)).ToList();
                    GraphicsPath gp1 = new GraphicsPath();
                    gp1.AddPolygon(ps.ToArray());
                    GpcPolygon pp = new GpcPolygon(gp1);
                    list.Add(pp);
                }
            }
            return list;
        }

        private static List<List<PointF>> ConvertToPointF(Geometry geom)
        {
            List<List<PointF>> list = new List<List<PointF>>();

            if (geom is Polygon)
            {
                Polygon polygon = geom as Polygon;
                list.Add((from p in polygon.ExteriorRing.Vertices select new PointF((float)p.X, (float)p.Y)).ToList());
            }
            else if (geom is MultiPolygon)
            {
                MultiPolygon polygons = geom as MultiPolygon;
                foreach (Polygon polygon in polygons.Polygons)
                {
                    list.Add((from p in polygon.ExteriorRing.Vertices select new PointF((float)p.X, (float)p.Y)).ToList());
                }
            }
            else if (geom is EasyMap.Geometries.Point)
            {
                EasyMap.Geometries.Point p = (EasyMap.Geometries.Point)geom;
                PointF p1 = new PointF(float.Parse(p.X.ToString()), (float)p.Y);
                List<PointF> lp = new List<PointF>();
                lp.Add(p1);
                list.Add(lp);
            }
            return list;
        }
        //新加
        private static EasyMap.Geometries.Point[] ConvertToPointF1(Geometry geom)
        {
            List<EasyMap.Geometries.Point[]> listAll = new List<EasyMap.Geometries.Point[]>();
            EasyMap.Geometries.Point[] list = null;

            if (geom is Polygon)
            {
                Polygon polygon = geom as Polygon;
                list = new EasyMap.Geometries.Point[polygon.ExteriorRing.Vertices.Count];
                for (int i = 0; i < polygon.ExteriorRing.Vertices.Count; i++)
                {
                    EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(polygon.ExteriorRing.Vertices[i].X, polygon.ExteriorRing.Vertices[i].Y);
                    list[i] = point;
                    //if (obj is GeoPoint)
                    //{
                    //    GeoPoint point = obj as GeoPoint;
                    //    if (point.IsAreaPriceMonitor)
                    //    {
                    //        if (area.InPoly(point))
                    //        {
                    //            list.Add(point);
                    //        }
                    //    }
                    //}
                }
                listAll.Add(list);
                //list[1] = new EasyMap.Geometries.Point(polygon, 1);
            }
            else if (geom is MultiPolygon)
            {
                MultiPolygon polygons = geom as MultiPolygon;
                //foreach (Polygon polygon in polygons.Polygons)
                //{
                int n = 0;
                for (int i = 0; i < polygons.Polygons.Count; i++)
                {
                    Polygon polygon = polygons.Polygons[i] as Polygon;
                    for (int j = 0; j < polygon.ExteriorRing.Vertices.Count; j++)
                    {
                        EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(polygon.ExteriorRing.Vertices[i].X, polygon.ExteriorRing.Vertices[i].Y);
                        //list.Add((from p in polygon.ExteriorRing.Vertices select new PointF((float)p.X, (float)p.Y)).ToList());
                        if (point != null)
                        {
                            n++;
                        }
                    }
                }
                list = new EasyMap.Geometries.Point[n];
                n = 0;
                for (int i = 0; i < polygons.Polygons.Count; i++)
                {
                    Polygon polygon = polygons.Polygons[i] as Polygon;
                    for (int j = 0; j < polygon.ExteriorRing.Vertices.Count; j++)
                    {
                        EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(polygon.ExteriorRing.Vertices[j].X, polygon.ExteriorRing.Vertices[j].Y);
                        //list.Add((from p in polygon.ExteriorRing.Vertices select new PointF((float)p.X, (float)p.Y)).ToList());
                        if (point != null)
                        {
                            list[n] = point;
                            n++;
                        }
                    }
                    listAll.Add(list);
                }
            }
            else if (geom is EasyMap.Geometries.Point)
            {
                list = new EasyMap.Geometries.Point[1];
                EasyMap.Geometries.Point point = (EasyMap.Geometries.Point)geom;
                list[0] = point;
                listAll.Add(list);
                //list[1] = new EasyMap.Geometries.Point(polygon, 1);
            }
            return list;
        }
        public static Geometry Intersection(Geometry source, Geometry dest)
        {
            List<List<PointF>> source_polygons = ConvertToPointF(source);
            List<List<PointF>> dest_polygons =  ConvertToPointF(dest);
            GpcPolygon polygon = Intersection(source_polygons, dest_polygons);
            if (polygon.Contour.Length == 0)
            {
                return null;
            }
            if (polygon.Contour.Length > 2)
            {
                MultiPolygon ret = new MultiPolygon();
                polygon.Contour.ToList().ForEach(
                    a =>
                    {
                        Polygon subret = new Polygon();
                        IList<EasyMap.Geometries.Point> vertices = subret.ExteriorRing.Vertices;
                        a.Vertex.ToList().ForEach(p => { vertices.Add(new EasyMap.Geometries.Point(p.X, p.Y)); });
                        ret.Polygons.Add(subret);
                    });
                return ret;
            }
            else
            {
                Polygon ret = new Polygon();
                IList<EasyMap.Geometries.Point> vertices = ret.ExteriorRing.Vertices;
                polygon.Contour[0].Vertex.ToList().ForEach(p => { vertices.Add(new EasyMap.Geometries.Point(p.X, p.Y)); });
                return ret;
            }
        }
        public static Geometry Intersection1(Geometry source, Geometry dest)
        {
            //List<List<PointF>> source_polygons = ConvertToPointF(source);
            //List<List<PointF>> dest_polygons = ConvertToPointF(dest);
            EasyMap.Geometries.Point[] source_polygons = ConvertToPointF1(source);
            EasyMap.Geometries.Point[] dest_polygons = ConvertToPointF1(dest);
            if (source_polygons == null || dest_polygons == null)
            {
                return null;
            }
            if (dest is EasyMap.Geometries.Point)
            { 
                return dest;
            }
            GpcPolygon polygon = Intersection(source_polygons, dest_polygons);
            if (polygon.Contour.Length == 0)
            {
                return null;
            }
            if (polygon.Contour.Length > 2)
            {
                MultiPolygon ret = new MultiPolygon();
                polygon.Contour.ToList().ForEach(
                    a =>
                    {
                        Polygon subret = new Polygon();
                        IList<EasyMap.Geometries.Point> vertices = subret.ExteriorRing.Vertices;
                        a.Vertex.ToList().ForEach(p => { vertices.Add(new EasyMap.Geometries.Point(p.X, p.Y)); });
                        ret.Polygons.Add(subret);
                    });
                return ret;
            }
            else
            {
                Polygon ret = new Polygon();
                IList<EasyMap.Geometries.Point> vertices = ret.ExteriorRing.Vertices;
                polygon.Contour[0].Vertex.ToList().ForEach(p => { vertices.Add(new EasyMap.Geometries.Point(p.X, p.Y)); });
                return ret;
            }
        }
        public static bool IsIn(Geometry source, Geometry dest,bool flag,out double area)
        {
            area = 0;
            Geometry geom = Intersection(source, dest);
            if (geom == null)
            {
                return false;
            }
            double destarea = 0;
            if (geom is Polygon)
            {
                area = (geom as Polygon).Area;
            }
            else if (geom is MultiPolygon)
            {
                area = (geom as MultiPolygon).Area;
            }
            if (dest is Polygon)
            {
                destarea = (dest as Polygon).Area;
            }
            else if (dest is MultiPolygon)
            {
                destarea = (dest as MultiPolygon).Area;
            }
            if(destarea==0)
            {
                return true;
            }
            return true;
            return (area / destarea) < (1 / 1000) || (area / destarea)>(990/1000);
        }
        //新加20160927  刘
        public static Map map = null;
        public static bool IsIn(Geometry source, Geometry dest, bool flag, out double area, Map map1)
        {
            map = map1;
            area = 0;
            Geometry geom = Intersection1(source, dest);
            if (geom == null)
            {
                return false;
            }
            double destarea = 0;
            if (geom is Polygon)
            {
                area = (geom as Polygon).Area;
            }
            else if (geom is MultiPolygon)
            {
                area = (geom as MultiPolygon).Area;
            }
            else if (geom is EasyMap.Geometries.Point)
            {
                area = 0;
            }
            if (dest is Polygon)
            {
                destarea = (dest as Polygon).Area;
            }
            else if (dest is MultiPolygon)
            {
                destarea = (dest as MultiPolygon).Area;
            }
            else if (dest is EasyMap.Geometries.Point)
            {
                destarea = 0;
            }
            if (destarea == 0)
            {
                return true;
            }
            return true;
            return (area / destarea) < (1 / 1000) || (area / destarea) > (990 / 1000);
        }
    }
}
