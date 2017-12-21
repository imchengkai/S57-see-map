

using System;
using System.Collections.Generic;

namespace EasyMap.Geometries
{
    [Serializable]
    public class LinearRing : LineString
    {
        private decimal _ID = 0;

        public LinearRing(IList<Point> vertices)
            : base(vertices)
        {
        }

        public LinearRing()
            : base()
        {
        }

        public LinearRing(IEnumerable<double[]> points)
            : base(points)
        {
        }

        public double Area
        {
            get
            {
                if (Vertices.Count < 3)
                    return 0;
                double sum = 0;
                double ax = Vertices[0].X;
                double ay = Vertices[0].Y;
                for (int i = 1; i < Vertices.Count - 1; i++)
                {
                    double bx = Vertices[i].X;
                    double by = Vertices[i].Y;
                    double cx = Vertices[i + 1].X;
                    double cy = Vertices[i + 1].Y;
                    sum += ax * by - ay * bx +
                           ay * cx - ax * cy +
                           bx * cy - cx * by;
                }
                return Math.Abs(-sum / 2);
            }
        }

        public new LinearRing Clone()
        {
            LinearRing l = new LinearRing();
            for (int i = 0; i < Vertices.Count; i++)
                l.Vertices.Add(Vertices[i].Clone());
            return l;
        }

        public bool IsCCW()
        {
            Point hip, p, prev, next;
            int hii, i;
            int nPts = Vertices.Count;

            if (nPts < 4) return false;

            hip = Vertices[0];
            hii = 0;
            for (i = 1; i < nPts; i++)
            {
                p = Vertices[i];
                if (p.Y > hip.Y)
                {
                    hip = p;
                    hii = i;
                }
            }
            int iPrev = hii - 1;
            if (iPrev < 0) iPrev = nPts - 2;
            int iNext = hii + 1;
            if (iNext >= nPts) iNext = 1;
            prev = Vertices[iPrev];
            next = Vertices[iNext];
            double prev2x = prev.X - hip.X;
            double prev2y = prev.Y - hip.Y;
            double next2x = next.X - hip.X;
            double next2y = next.Y - hip.Y;
            double disc = next2x * prev2y - next2y * prev2x;

            if (disc == 0.0)
            {
                return (prev.X > next.X);
            }
            else
            {
                return (disc > 0.0);
            }
        }

        public bool IsPointWithin(Point p)
        {
            bool c = false;
            for (int i = 0, j = Vertices.Count - 1; i < Vertices.Count; j = i++)
            {
                if ((((Vertices[i].Y <= p.Y) && (p.Y < Vertices[j].Y)) ||
                     ((Vertices[j].Y <= p.Y) && (p.Y < Vertices[i].Y))) &&
                    (p.X <
                     (Vertices[j].X - Vertices[i].X) * (p.Y - Vertices[i].Y) / (Vertices[j].Y - Vertices[i].Y) +
                     Vertices[i].X))
                    c = !c;
            }
            return c;
        }
    }
}
