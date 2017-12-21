

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using EasyMap.Geometries;

namespace EasyMap.Utilities.SpatialIndexing
{
    public struct Heuristic
    {
        public int maxdepth;

        public int minerror;

        public int mintricnt;

        public int tartricnt;
    }


    public class QuadTree : IDisposable
    {
        private BoundingBox _box;

        private QuadTree _child0;
        private QuadTree _child1;

        public int NodeCount
        {
            get
            {
                if (_child0 != null)
                    return _child0.NodeCount + _child1.NodeCount;

                return _objList != null ? _objList.Count : 0;
            }
        }

        public bool IsPrunable { get { return NodeCount == 0; } }

        private readonly uint _depth;

        public uint? _ID;

        private List<BoxObjects> _objList;

        public QuadTree(List<BoxObjects> objList, uint depth, Heuristic heurdata)
        {
            _depth = depth;

            _box = objList[0].Box;
            for (int i = 0; i < objList.Count; i++)
                _box = _box.Join(objList[i].Box);

            if (depth < heurdata.maxdepth && objList.Count > heurdata.mintricnt &&
                (objList.Count > heurdata.tartricnt || ErrorMetric(_box) > heurdata.minerror))
            {
                var objBuckets = new List<BoxObjects>[2]; // buckets of geometries
                objBuckets[0] = new List<BoxObjects>();
                objBuckets[1] = new List<BoxObjects>();

                uint longaxis = _box.LongestAxis; // longest axis
                double geoavg = 0; // geometric average - midpoint of ALL the objects

                double frac = 1.0f / objList.Count;
                for (int i = 0; i < objList.Count; i++)
                    geoavg += objList[i].Box.GetCentroid()[longaxis] * frac;

                for (int i = 0; i < objList.Count; i++)
                    objBuckets[geoavg > objList[i].Box.GetCentroid()[longaxis] ? 1 : 0].Add(objList[i]);

                if (objBuckets[0].Count == 0 || objBuckets[1].Count == 0)
                {
                    _child0 = null;
                    _child1 = null;
                    _objList = objList;
                }
                else
                {
                    objList.Clear();

                    _child0 = new QuadTree(objBuckets[0], depth + 1, heurdata);
                    _child1 = new QuadTree(objBuckets[1], depth + 1, heurdata);
                }
            }
            else
            {
                _child0 = null;
                _child1 = null;
                _objList = objList;
            }
        }

        private const double SplitRatio = 0.55d;

        private static void SplitBoundingBox(BoundingBox input, out BoundingBox out1, out BoundingBox out2)
        {
            double range;

            /* -------------------------------------------------------------------- */
            /*      Split in X direction.                                           */
            /* -------------------------------------------------------------------- */
            if ((input.Width) > (input.Height))
            {
                range = input.Width * SplitRatio;

                out1 = new BoundingBox(input.BottomLeft.Clone(), new Point(input.Left + range, input.Top));
                out2 = new BoundingBox(new Point(input.Right - range, input.Bottom), input.TopRight.Clone());
            }

                /* -------------------------------------------------------------------- */
            /*      Otherwise split in Y direction.                                 */
            /* -------------------------------------------------------------------- */
            else
            {
                range = input.Height * SplitRatio;

                out1 = new BoundingBox(input.BottomLeft.Clone(), new Point(input.Right, input.Bottom + range));
                out2 = new BoundingBox(new Point(input.Left, input.Top - range), input.TopRight.Clone());
            }
        }

        public void AddNode(BoxObjects o, Heuristic h)
        {
            /* -------------------------------------------------------------------- */
            /*      If there are subnodes, then consider whether this object        */
            /*      will fit in them.                                               */
            /* -------------------------------------------------------------------- */
            if (_child0 != null && _depth < h.maxdepth)
            {
                if (_child0.Box.Contains(o.Box.GetCentroid()))
                    _child0.AddNode(o, h);
                else if (_child1.Box.Contains(o.Box.GetCentroid()))
                    _child1.AddNode(o, h);
                return;
            }

            /* -------------------------------------------------------------------- */
            /*      Otherwise, consider creating two subnodes if could fit into     */
            /*      them, and adding to the appropriate subnode.                    */
            /* -------------------------------------------------------------------- */
            if (h.maxdepth > _depth && !IsLeaf)
            {
                BoundingBox half1, half2;
                SplitBoundingBox(Box, out half1, out half2);


                if (half1.Contains(o.Box.GetCentroid()))
                {
                    _child0 = new QuadTree(half1, _depth + 1);
                    _child1 = new QuadTree(half2, _depth + 1);
                    _child0.AddNode(o, h);
                    return;
                }
                if (half2.Contains(o.Box.GetCentroid()))
                {
                    _child0 = new QuadTree(half1, _depth + 1);
                    _child1 = new QuadTree(half2, _depth + 1);
                    _child1.AddNode(o, h);
                    return;
                }
            }

            /* -------------------------------------------------------------------- */
            /*      If none of that worked, just add it to this nodes list.         */
            /* -------------------------------------------------------------------- */

            if (_objList == null)
                _objList = new List<BoxObjects>();

            if (!Box.Contains(o.Box))
                Box = Box.Join(o.Box);

            _objList.Add(o);

        }


        private QuadTree()
        {
        }

        private QuadTree(BoundingBox box, uint depth)
        {
            _box = box;
            _depth = depth;
        }

        #region Read/Write index to/from a file

        private const double INDEXFILEVERSION = 1.0;

        public static QuadTree FromFile(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                if (br.ReadDouble() != INDEXFILEVERSION) //Check fileindex version
                {
                    fs.Close();
                    fs.Dispose();
                    throw new ObsoleteFileFormatException(
                        "Invalid index file version. Please rebuild the spatial index by either deleting the index");
                }
                var node = ReadNode(0, br);
                return node;
            }
        }

        private static QuadTree ReadNode(uint depth, BinaryReader br)
        {
            var bbox = new BoundingBox(br);
            var node = new QuadTree(bbox, depth);

            var isLeaf = br.ReadBoolean();
            if (isLeaf)
            {
                var featureCount = br.ReadInt32();
                node._objList = new List<BoxObjects>();
                for (int i = 0; i < featureCount; i++)
                {
                    var box = new BoxObjects();
                    box.Box = new BoundingBox(br);
                    box.ID = (uint)br.ReadInt32();
                    node._objList.Add(box);
                }
            }
            else
            {
                node.Child0 = ReadNode(depth + 1, br);
                node.Child1 = ReadNode(depth + 1, br);
            }
            return node;
        }

        public void SaveIndex(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Create))
            using (var bw = new BinaryWriter(fs))
            {
                bw.Write(INDEXFILEVERSION); //Save index version
                SaveNode(this, bw);
            }
        }

        private static void SaveNode(QuadTree node, BinaryWriter sw)
        {
            sw.Write(node.Box.Min.X);
            sw.Write(node.Box.Min.Y);
            sw.Write(node.Box.Max.X);
            sw.Write(node.Box.Max.Y);
            sw.Write(node.IsLeaf);
            if (node.IsLeaf || node.Child0 == null)
            {
                if (node._objList == null)
                {
                    sw.Write(0);
                    return;
                }

                sw.Write(node._objList.Count); //Write number of features at node
                for (int i = 0; i < node._objList.Count; i++) //Write each featurebox
                {
                    sw.Write(node._objList[i].Box.Min.X);
                    sw.Write(node._objList[i].Box.Min.Y);
                    sw.Write(node._objList[i].Box.Max.X);
                    sw.Write(node._objList[i].Box.Max.Y);
                    sw.Write(node._objList[i].ID);
                }
            }
            else if (!node.IsLeaf) //Save next node
            {
                SaveNode(node.Child0, sw);
                SaveNode(node.Child1, sw);
            }
        }

        public static QuadTree CreateRootNode(BoundingBox b)
        {
            return new QuadTree(b, 0);
        }

        internal class ObsoleteFileFormatException : Exception
        {
            public ObsoleteFileFormatException(string message)
                : base(message)
            {
            }
        }

        #endregion

        public bool IsLeaf
        {
            get { return (_objList != null); }
        }


        public BoundingBox Box
        {
            get { return _box; }
            set { _box = value; }
        }

        public QuadTree Child0
        {
            get { return _child0; }
            set { _child0 = value; }
        }

        public QuadTree Child1
        {
            get { return _child1; }
            set { _child1 = value; }
        }

        public uint Depth
        {
            get { return _depth; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _child0 = null;
            _child1 = null;
            _objList = null;
        }

        #endregion

        public static double ErrorMetric(BoundingBox box)
        {
            Point temp = new Point(1, 1) + (box.Max - box.Min);
            return temp.X * temp.Y;
        }

        public Collection<uint> Search(BoundingBox box)
        {
            Collection<uint> objectlist = new Collection<uint>();
            IntersectTreeRecursive(box, this, ref objectlist);
            return objectlist;
        }

        private static void IntersectTreeRecursive(BoundingBox box, QuadTree node, ref Collection<uint> list)
        {
            if (node.IsLeaf) //Leaf has been reached
            {
                foreach (var boxObject in node._objList)
                {
                    if (box == null || box.Intersects(boxObject.Box))
                        list.Add(boxObject.ID);

                }
                /*
                for (int i = 0; i < node._objList.Count; i++)
                {
                    list.Add(node._objList[i].ID);
                }
                */
            }
            else
            {
                if (box == null || node.Box.Intersects(box))
                {
                    if (node.Child0 != null)
                        IntersectTreeRecursive(box, node.Child0, ref list);
                    if (node.Child1 != null)
                        IntersectTreeRecursive(box, node.Child1, ref list);
                }
            }
        }

        #region Nested type: BoxObjects

        public struct BoxObjects
        {
            public BoundingBox Box;

            public uint ID;
        }

        #endregion
    }
}
