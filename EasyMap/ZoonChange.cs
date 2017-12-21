using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;
using OSGeo.OSR;

namespace EasyMap
{
    public class ZoonChange
    {
        private string _ZoonSrcProj = "PROJCS[\"Xian 1980 / 3-degree Gauss-Kruger zone 40\",GEOGCS[\"Xian 1980\",DATUM[\"Xian 1980\",SPHEROID[\"Xian 1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",40500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",120.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private string _ZoonDestProj = "PROJCS[\"Xian 1980 / 3-degree Gauss-Kruger zone 41\",GEOGCS[\"Xian 1980\",DATUM[\"Xian 1980\",SPHEROID[\"Xian 1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",41500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",123.0],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";

        public string ZoonDestProj
        {
            get { return _ZoonDestProj; }
            set { _ZoonDestProj = value; }
        }

        public string ZoonSrcProj
        {
            get { return _ZoonSrcProj; }
            set { _ZoonSrcProj = value; }
        }

        public Point Change(double x, double y)
        {
            return Change(new Point(x, y));
        }

        public Point Change(Point srcPoint)
        {
            try
            {
                SpatialReference src = new SpatialReference("");
                string srcproj = ZoonSrcProj;
                src.ImportFromWkt(ref srcproj);
                SpatialReference dest = new SpatialReference("");
                string destproj = ZoonDestProj;
                dest.ImportFromWkt(ref destproj);
                CoordinateTransformation ct = new CoordinateTransformation(src, dest);
                double[] p = new double[3];
                p[0] = srcPoint.X;
                p[1] = srcPoint.Y;
                p[2] = 0;
                ct.TransformPoint(p);
                Point destPoint = new Point(p[0], p[1]);
                return destPoint;
            }
            catch
            {
                return srcPoint;
            }
        }
    }
}
