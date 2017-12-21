using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;
using OSGeo.OSR;

namespace EasyMap
{
    public class ZoneChange
    {
        private string _ZoneSrcProj = "PROJCS[\"Xian 1980 / 3-degree Gauss-Kruger zone srczone\",GEOGCS[\"Xian 1980\",DATUM[\"Xian 1980\",SPHEROID[\"Xian 1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",srczone500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",srcdegree],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private string _ZoneDestProj = "PROJCS[\"Xian 1980 / 3-degree Gauss-Kruger zone destzone\",GEOGCS[\"Xian 1980\",DATUM[\"Xian 1980\",SPHEROID[\"Xian 1980\",6378140.0,298.257]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"False_Easting\",destzone500000.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",destdegree],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",0.0],UNIT[\"Meter\",1.0]]";
        private int _ZoneSrc = 40;
        private int _ZoneDest = 41;

        public int ZoneDest
        {
            get { return _ZoneDest; }
            set 
            { 
                _ZoneDest = value;
                ZoneDestProj = ZoneDestProj.Replace("destzone", value.ToString());
                int degree = value * 3;
                ZoneDestProj = ZoneDestProj.Replace("destdegree", degree.ToString());
            }
        }

        public int ZoneSrc
        {
            get { return _ZoneSrc; }
            set 
            { 
                _ZoneSrc = value;
                ZoneSrcProj = ZoneSrcProj.Replace("srczone", value.ToString());
                int degree = value * 3;
                ZoneSrcProj = ZoneSrcProj.Replace("srcdegree", degree.ToString());
            }
        }

        public string ZoneDestProj
        {
            get { return _ZoneDestProj; }
            set { _ZoneDestProj = value; }
        }

        public string ZoneSrcProj
        {
            get { return _ZoneSrcProj; }
            set { _ZoneSrcProj = value; }
        }

        public ZoneChange(int zonesrc, int zonedest)
        {
            ZoneDest = zonedest;
            ZoneSrc = zonesrc;
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
                string srcproj = ZoneSrcProj;
                src.ImportFromWkt(ref srcproj);
                SpatialReference dest = new SpatialReference("");
                string destproj = ZoneDestProj;
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
