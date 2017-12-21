

using System;
using System.Collections.ObjectModel;
using System.IO;

namespace EasyMap.Geometries
{
    [Serializable]
    public class BoundingBox : IEquatable<BoundingBox>
    {
        private Point _max;
        private Point _min;

        public BoundingBox(double minX, double minY, double maxX, double maxY)
        {
            _min = new Point(minX, minY);
            _max = new Point(maxX, maxY);
            CheckMinMax();
        }

        internal BoundingBox(BinaryReader br)
        {
            var bytes = new byte[32];
            br.Read(bytes, 0, 32);

            var doubles = new double[4];
            Buffer.BlockCopy(bytes, 0, doubles, 0, 32);

            _min = new Point(doubles[0], doubles[1]);
            _max = new Point(doubles[2], doubles[3]);
        }

        public BoundingBox(Point lowerLeft, Point upperRight)
            : this(lowerLeft.X, lowerLeft.Y, upperRight.X, upperRight.Y)
        {
        }

        public BoundingBox(Collection<Geometry> objects)
        {
            if (objects == null || objects.Count == 0)
            {
                _min = null;
                _max = null;
                return;
            }
            _min = objects[0].GetBoundingBox().Min.Clone();
            _max = objects[0].GetBoundingBox().Max.Clone();
            CheckMinMax();
            for (int i = 1; i < objects.Count; i++)
            {
                BoundingBox box = objects[i].GetBoundingBox();
                _min.X = Math.Min(box.Min.X, Min.X);
                _min.Y = Math.Min(box.Min.Y, Min.Y);
                _max.X = Math.Max(box.Max.X, Max.X);
                _max.Y = Math.Max(box.Max.Y, Max.Y);
            }
        }

        public BoundingBox(Collection<BoundingBox> objects)
        {
            if (objects.Count == 0)
            {
                _max = null;
                _min = null;
            }
            else
            {
                _min = objects[0].Min.Clone();
                _max = objects[0].Max.Clone();
                for (int i = 1; i < objects.Count; i++)
                {
                    _min.X = Math.Min(objects[i].Min.X, Min.X);
                    _min.Y = Math.Min(objects[i].Min.Y, Min.Y);
                    _max.X = Math.Max(objects[i].Max.X, Max.X);
                    _max.Y = Math.Max(objects[i].Max.Y, Max.Y);
                }
            }
        }

        public Point Min
        {
            get { return _min; }
            set
            {
                _min = value;
                _centroid = null;
            }
        }

        public Point Max
        {
            get { return _max; }
            set
            {
                _max = value;
                _centroid = null;
            }
        }

        public Double Left
        {
            get { return _min.X; }
        }

        public Double Right
        {
            get { return _max.X; }
        }

        public Double Top
        {
            get { return _max.Y; }
        }

        public Double Bottom
        {
            get { return _min.Y; }
        }

        public Point TopLeft
        {
            get { return new Point(Left, Top); }
        }

        public Point TopRight
        {
            get { return new Point(Right, Top); }
        }

        public Point BottomLeft
        {
            get { return new Point(Left, Bottom); }
        }

        public Point BottomRight
        {
            get { return new Point(Right, Bottom); }
        }

        public double Width
        {
            get { return Math.Abs(_max.X - _min.X); }
        }

        public double Height
        {
            get { return Math.Abs(_max.Y - _min.Y); }
        }

        public uint LongestAxis
        {
            get
            {
                Point boxdim = Max - Min;
                uint la = 0; // longest axis
                double lav = 0; // longest axis length
                for (uint ii = 0; ii < 2; ii++)
                {
                    if (boxdim[ii] > lav)
                    {
                        la = ii;
                        lav = boxdim[ii];
                    }
                }
                return la;
            }
        }

        public bool IsValid
        {
            get
            {
                return (!_min.IsEmpty() && !_max.IsEmpty() && _min != null && _max != null && !double.IsNaN(_min.X) && !double.IsNaN(_max.X) &&
                        !double.IsNaN(_min.Y) && !double.IsNaN(_max.Y));
            }
        }

        #region IEquatable<BoundingBox> Members

        public bool Equals(BoundingBox other)
        {
            if (other == null) return false;
            return Left == other.Left && Right == other.Right && Top == other.Top && Bottom == other.Bottom;
        }

        #endregion

        public void Offset(Point vector)
        {
            _min += vector;
            _max += vector;
        }

        public bool CheckMinMax()
        {
            bool wasSwapped = false;
            if (!_min.IsEmpty() && !_max.IsEmpty())
            {
                if (_min.X > _max.X)
                {
                    double tmp = _min.X;
                    _min.X = _max.X;
                    _max.X = tmp;
                    wasSwapped = true;
                }
                if (_min.Y > _max.Y)
                {
                    double tmp = _min.Y;
                    _min.Y = _max.Y;
                    _max.Y = tmp;
                    wasSwapped = true;
                }
            }
            return wasSwapped;
        }

        public bool Intersects(BoundingBox box)
        {
            return !(box.Min.X > Max.X ||
                     box.Max.X < Min.X ||
                     box.Min.Y > Max.Y ||
                     box.Max.Y < Min.Y);
        }

        public bool Intersects(Geometry g)
        {
            return Touches(g);
        }

        public bool Touches(BoundingBox r)
        {
            for (uint cIndex = 0; cIndex < 2; cIndex++)
            {
                if ((Min[cIndex] > r.Min[cIndex] && Min[cIndex] < r.Min[cIndex]) ||
                    (Max[cIndex] > r.Max[cIndex] && Max[cIndex] < r.Max[cIndex]))
                    return true;
            }
            return false;
        }

        public bool Touches(Geometry s)
        {
            if (s is Point) return Touches(s as Point);
            throw new NotImplementedException("Touches: Not implemented on this geometry type");
        }

        public bool Contains(BoundingBox r)
        {
            for (uint cIndex = 0; cIndex < 2; cIndex++)
                if (Min[cIndex] > r.Min[cIndex] || Max[cIndex] < r.Max[cIndex]) return false;

            return true;
        }

        public bool Contains(Geometry s)
        {
            if (s is Point) return Contains(s as Point);
            throw new NotImplementedException("Contains: Not implemented on these geometries");
        }

        public bool Touches(Point p)
        {
            for (uint cIndex = 0; cIndex < 2; cIndex++)
            {
                if ((Min[cIndex] > p[cIndex] && Min[cIndex] < p[cIndex]) ||
                    (Max[cIndex] > p[cIndex] && Max[cIndex] < p[cIndex]))
                    return true;
            }
            return false;
        }

        public double GetArea()
        {
            return Width * Height;
        }

        public double GetIntersectingArea(BoundingBox r)
        {
            uint cIndex;
            for (cIndex = 0; cIndex < 2; cIndex++)
                if (Min[cIndex] > r.Max[cIndex] || Max[cIndex] < r.Min[cIndex]) return 0.0;

            double ret = 1.0;
            double f1, f2;

            for (cIndex = 0; cIndex < 2; cIndex++)
            {
                f1 = Math.Max(Min[cIndex], r.Min[cIndex]);
                f2 = Math.Min(Max[cIndex], r.Max[cIndex]);
                ret *= f2 - f1;
            }
            return ret;
        }

        public BoundingBox Join(BoundingBox box)
        {
            if (box == null)
                return Clone();

            return new BoundingBox(Math.Min(Min.X, box.Min.X), Math.Min(Min.Y, box.Min.Y),
                                   Math.Max(Max.X, box.Max.X), Math.Max(Max.Y, box.Max.Y));
        }

        public static BoundingBox Join(BoundingBox box1, BoundingBox box2)
        {
            if (box1 == null && box2 == null)
                return null;
            if (box1 == null)
                return box2.Clone();

            return box1.Join(box2);
        }

        public static BoundingBox Join(BoundingBox[] boxes)
        {
            if (boxes == null) return null;
            if (boxes.Length == 1) return boxes[0];
            BoundingBox box = boxes[0].Clone();
            for (int i = 1; i < boxes.Length; i++)
                box = box.Join(boxes[i]);
            return box;
        }

        public BoundingBox Grow(double amount)
        {
            BoundingBox box = Clone();
            box.Min.X -= amount;
            box.Min.Y -= amount;
            box.Max.X += amount;
            box.Max.Y += amount;
            box.CheckMinMax();
            return box;
        }

        public BoundingBox Grow(double amountInX, double amountInY)
        {
            BoundingBox box = Clone();
            box.Min.X -= amountInX;
            box.Min.Y -= amountInY;
            box.Max.X += amountInX;
            box.Max.Y += amountInY;
            box.CheckMinMax();
            return box;
        }

        public bool Contains(Point p)
        {
            if (Max.X < p.X)
                return false;
            if (Min.X > p.X)
                return false;
            if (Max.Y < p.Y)
                return false;
            if (Min.Y > p.Y)
                return false;
            return true;
        }

        public virtual double Distance(BoundingBox box)
        {
            double ret = 0.0;
            for (uint cIndex = 0; cIndex < 2; cIndex++)
            {
                double x = 0.0;

                if (box.Max[cIndex] < Min[cIndex]) x = Math.Abs(box.Max[cIndex] - Min[cIndex]);
                else if (Max[cIndex] < box.Min[cIndex]) x = Math.Abs(box.Min[cIndex] - Max[cIndex]);
                ret += x * x;
            }
            return Math.Sqrt(ret);
        }

        public virtual double Distance(Point p)
        {
            double ret = 0.0;

            for (uint cIndex = 0; cIndex < 2; cIndex++)
            {
                if (p[cIndex] < Min[cIndex]) ret += Math.Pow(Min[cIndex] - p[cIndex], 2.0);
                else if (p[cIndex] > Max[cIndex]) ret += Math.Pow(p[cIndex] - Max[cIndex], 2.0);
            }

            return Math.Sqrt(ret);
        }

        private Point _centroid;
        public Point GetCentroid()
        {
            return (_centroid ?? (_centroid = (_min + _max) * .5f));
        }

        public BoundingBox Clone()
        {
            return new BoundingBox(_min.X, _min.Y, _max.X, _max.Y);
        }

        public override string ToString()
        {
            return String.Format(Map.NumberFormatEnUs, "{0},{1} {2},{3}", Min.X, Min.Y, Max.X, Max.Y);
        }

        public override bool Equals(object obj)
        {
            var box = obj as BoundingBox;
            if (obj == null)
                return false;
            return Equals(box);
        }

        public override int GetHashCode()
        {
            return Min.GetHashCode() ^ Max.GetHashCode();
        }

        public BoundingBox Intersection(double[] defaultEnvelope)
        {
            if (defaultEnvelope == null)
                return this;

            System.Diagnostics.Debug.Assert(defaultEnvelope.Length == 4);

            var minX = Min.X > defaultEnvelope[0] ? Min.X : defaultEnvelope[0];
            var minY = Min.Y > defaultEnvelope[1] ? Min.Y : defaultEnvelope[1];
            var maxX = Max.X < defaultEnvelope[2] ? Max.X : defaultEnvelope[2];
            var maxY = Max.Y < defaultEnvelope[3] ? Max.Y : defaultEnvelope[3];

            return new BoundingBox(minX, minY, maxX, maxY);
        }

        public Geometry ToGeometry()
        {
            var linearRing =
                new LinearRing(new[]
                                   {
                                       Min.Clone(), new Point(Min.X, Max.Y), 
                                       Max.Clone(), new Point(Max.X, Min.Y),
                                       Min.Clone()
                                   });
            return new Polygon(linearRing, null);
        }
    }
}
