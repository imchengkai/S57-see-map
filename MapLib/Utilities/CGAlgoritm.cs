using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;

namespace EasyMap.Utilities
{
   
        public static class CGAlgorithms
        {
            public const int Clockwise = -1;
            public const int Right = Clockwise;

            public const int CounterClockwise = 1;
            public const int Left = CounterClockwise;

            public const int Collinear = 0;
            public const int Straight = Collinear;


            public static Point ClosestPoint(Point p, Point LineSegFrom, Point LineSegTo)
            {
                var factor = ProjectionFactor(p, LineSegFrom, LineSegTo);
                if (factor > 0 && factor < 1)
                    return Project(p, LineSegFrom, LineSegTo);
                var dist0 = LineSegFrom.Distance(p);
                var dist1 = LineSegTo.Distance(p);
                return dist0 < dist1 ? LineSegFrom : LineSegTo;
            }

            public static Point Project(Point p, Point LineSegFrom, Point LineSegTo)
            {
                if (p.Equals(LineSegFrom) || p.Equals(LineSegTo))
                    return new Point(p.X, p.Y);

                var r = ProjectionFactor(p, LineSegFrom,  LineSegTo);
                Point coord = new Point { X = LineSegFrom.X + r * (LineSegTo.X - LineSegFrom.X), Y = LineSegFrom.Y + r * (LineSegTo.Y - LineSegFrom.Y) };
                return coord;
            }

            public static double ProjectionFactor(Point p, Point LineSegFrom, Point LineSegTo)
            {
                if (p.Equals(LineSegFrom)) return 0.0;
                if (p.Equals(LineSegTo)) return 1.0;

                var dx = LineSegTo.X - LineSegFrom.X;
                var dy = LineSegTo.Y - LineSegFrom.Y;
                var len2 = dx * dx + dy * dy;
                var r = ((p.X - LineSegFrom.X) * dx + (p.Y - LineSegFrom.Y) * dy) / len2;
                return r;
            }


            public static double DistancePointLine(Point p, Point A, Point B)
            {
                if (A.Equals(B))
                    return p.Distance(A);


                double r = ((p.X - A.X) * (B.X - A.X) + (p.Y - A.Y) * (B.Y - A.Y))
                            /
                            ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));

                if (r <= 0.0) return p.Distance(A);
                if (r >= 1.0) return p.Distance(B);



                double s = ((A.Y - p.Y) * (B.X - A.X) - (A.X - p.X) * (B.Y - A.Y))
                            /
                            ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));

                return Math.Abs(s) * Math.Sqrt(((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y)));
            }
            public static double DistancePointLinePerpendicular(Point p, Point A, Point B)
            {

                double s = ((A.Y - p.Y) * (B.X - A.X) - (A.X - p.X) * (B.Y - A.Y))
                            /
                            ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));

                return Math.Abs(s) * Math.Sqrt(((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y)));
            }

           /// <summary>
           /// 计算线间距离
           /// </summary>
           /// <param name="A"></param>
           /// <param name="B"></param>
           /// <param name="C"></param>
           /// <param name="D"></param>
           /// <returns></returns>
            public static double DistanceLineLine(Point A, Point B, Point C, Point D)
            {
               
                if (A.Equals(B))
                    return DistancePointLine(A, C, D);
                if (C.Equals(D))
                    return DistancePointLine(D, A, B);

                double r_top = (A.Y - C.Y) * (D.X - C.X) - (A.X - C.X) * (D.Y - C.Y);
                double r_bot = (B.X - A.X) * (D.Y - C.Y) - (B.Y - A.Y) * (D.X - C.X);

                double s_top = (A.Y - C.Y) * (B.X - A.X) - (A.X - C.X) * (B.Y - A.Y);
                double s_bot = (B.X - A.X) * (D.Y - C.Y) - (B.Y - A.Y) * (D.X - C.X);

                if ((r_bot == 0) || (s_bot == 0))
                    return Math.Min(DistancePointLine(A, C, D),
                            Math.Min(DistancePointLine(B, C, D),
                            Math.Min(DistancePointLine(C, A, B),
                            DistancePointLine(D, A, B))));


                double s = s_top / s_bot;
                double r = r_top / r_bot;

                if ((r < 0) || (r > 1) || (s < 0) || (s > 1))
                    //no intersection
                    return Math.Min(DistancePointLine(A, C, D),
                            Math.Min(DistancePointLine(B, C, D),
                            Math.Min(DistancePointLine(C, A, B),
                            DistancePointLine(D, A, B))));

                return 0.0; //intersection exists
            }

            /// <summary>
            /// 计算多点围成的图形的面积
            /// </summary>
            /// <param name="ring"></param>
            /// <returns></returns>
            public static double SignedArea(Point[] ring)
            {
                if (ring.Length < 3)
                    return 0.0;

                double sum = 0.0;
                for (int i = 0; i < ring.Length - 1; i++)
                {
                    double bx = ring[i].X;
                    double by = ring[i].Y;
                    double cx = ring[i + 1].X;
                    double cy = ring[i + 1].Y;
                    sum += (bx + cx) * (cy - by);
                }
                return -sum / 2.0;
            }

            /// <summary>
            /// 计算多点间距离
            /// </summary>
            /// <param name="pts"></param>
            /// <returns></returns>
            public static double Length(IList<Point> pts)
            {
                if (pts.Count < 1)
                    return 0.0;

                double sum = 0.0;
                for (int i = 1; i < pts.Count; i++)
                    sum += pts[i].Distance(pts[i - 1]);

                return sum;
            }
        }
}
