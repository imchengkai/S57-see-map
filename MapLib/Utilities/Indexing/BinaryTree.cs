

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EasyMap.Utilities.Indexing
{
#if !MONO
    [Serializable]
    internal class Node<T, U> where T : IComparable<T>
    {
        public BinaryTree<T, U>.ItemValue Item;
        public Node<T, U> LeftNode;
        public Node<T, U> RightNode;

        public Node()
            : this(default(T), default(U), null, null)
        {
        }

        public Node(T item, U itemIndex)
            : this(item, itemIndex, null, null)
        {
        }

        public Node(BinaryTree<T, U>.ItemValue value)
            : this(value.Value, value.Id, null, null)
        {
        }

        public Node(T item, U itemIndex, Node<T, U> right, Node<T, U> left)
        {
            RightNode = right;
            LeftNode = left;
            Item = new BinaryTree<T, U>.ItemValue();
            Item.Value = item;
            Item.Id = itemIndex;
        }

        public static bool operator >(Node<T, U> lhs, Node<T, U> rhs)
        {
            int res = lhs.Item.Value.CompareTo(rhs.Item.Value);
            return res > 0;
        }

        public static bool operator <(Node<T, U> lhs, Node<T, U> rhs)
        {
            int res = lhs.Item.Value.CompareTo(rhs.Item.Value);
            return res < 0;
        }
    }

    [Serializable]
    public class BinaryTree<T, U> where T : IComparable<T>
    {
        private readonly Node<T, U> root;

        public BinaryTree()
        {
            root = new Node<T, U>();
        }

        #region Read/Write index to/from a file

        /*
		private const double INDEXFILEVERSION = 1.0;

		public void SaveToFile(string filename)
		{
			System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
			System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
			bw.Write(INDEXFILEVERSION); //Save index version
			bw.Write(typeof(T).ToString());
			bw.Write(typeof(U).ToString());
			bw.Close();
			fs.Close();
		}
		*/

        #endregion

        public void Add(params ItemValue[] items)
        {
            Array.ForEach(items, Add);
        }

        public void Add(ItemValue item)
        {
            Add(new Node<T, U>(item.Value, item.Id), root);
        }

        private void Add(Node<T, U> newNode, Node<T, U> root)
        {
            if (newNode > root)
            {
                if (root.RightNode == null)
                {
                    root.RightNode = newNode;
                    return;
                }
                Add(newNode, root.RightNode);
            }

            if (newNode < root)
            {
                if (root.LeftNode == null)
                {
                    root.LeftNode = newNode;
                    return;
                }
                Add(newNode, root.LeftNode);
            }
        }

        public void TraceTree()
        {
            TraceInOrder(root.RightNode);
        }

        private void TraceInOrder(Node<T, U> root)
        {
            if (root.LeftNode != null)
                TraceInOrder(root.LeftNode);

            Trace.WriteLine(root.Item.ToString());

            if (root.RightNode != null)
                TraceInOrder(root.RightNode);
        }

        #region IEnumerables

        public IEnumerable<ItemValue> InOrder
        {
            get { return ScanInOrder(root.RightNode); }
        }

        public IEnumerable<ItemValue> Between(T min, T max)
        {
            return ScanBetween(min, max, root.RightNode);
        }

        public IEnumerable<ItemValue> StartsWith(string str)
        {
            return ScanString(str.ToUpper(), root.RightNode);
        }

        public IEnumerable<ItemValue> Find(T value)
        {
            return ScanFind(value, root.RightNode);
        }

        private IEnumerable<ItemValue> ScanFind(T value, Node<T, U> root)
        {
            if (root.Item.Value.CompareTo(value) > 0)
            {
                if (root.LeftNode != null)
                {
                    if (root.LeftNode.Item.Value.CompareTo(value) > 0)
                        foreach (ItemValue item in ScanFind(value, root.LeftNode))
                        {
                            yield return item;
                        }
                }
            }

            if (root.Item.Value.CompareTo(value) == 0)
                yield return root.Item;

            if (root.Item.Value.CompareTo(value) < 0)
            {
                if (root.RightNode != null)
                {
                    if (root.RightNode.Item.Value.CompareTo(value) > 0)
                        foreach (ItemValue item in ScanFind(value, root.RightNode))
                        {
                            yield return item;
                        }
                }
            }
        }

        private IEnumerable<ItemValue> ScanString(string val, Node<T, U> root)
        {
            if (root.Item.Value.ToString().Substring(0, val.Length).ToUpper().CompareTo(val) > 0)
            {
                if (root.LeftNode != null)
                {
                    if (root.LeftNode.Item.Value.ToString().ToUpper().StartsWith(val))
                        foreach (ItemValue item in ScanString(val, root.LeftNode))
                        {
                            yield return item;
                        }
                }
            }

            if (root.Item.Value.ToString().ToUpper().StartsWith(val))
                yield return root.Item;

            if (root.Item.Value.ToString().CompareTo(val) < 0)
            {
                if (root.RightNode != null)
                {
                    if (root.RightNode.Item.Value.ToString().Substring(0, val.Length).ToUpper().CompareTo(val) > 0)
                        foreach (ItemValue item in ScanString(val, root.RightNode))
                        {
                            yield return item;
                        }
                }
            }
        }

        private IEnumerable<ItemValue> ScanBetween(T min, T max, Node<T, U> root)
        {
            if (root.Item.Value.CompareTo(min) > 0)
            {
                if (root.LeftNode != null)
                {
                    if (root.LeftNode.Item.Value.CompareTo(min) > 0)
                        foreach (ItemValue item in ScanBetween(min, max, root.LeftNode))
                        {
                            yield return item;
                        }
                }
            }

            if (root.Item.Value.CompareTo(min) > 0 && root.Item.Value.CompareTo(max) < 0)
                yield return root.Item;

            if (root.Item.Value.CompareTo(max) < 0)
            {
                if (root.RightNode != null)
                {
                    if (root.RightNode.Item.Value.CompareTo(min) > 0)
                        foreach (ItemValue item in ScanBetween(min, max, root.RightNode))
                        {
                            yield return item;
                        }
                }
            }
        }

        private IEnumerable<ItemValue> ScanInOrder(Node<T, U> root)
        {
            if (root.LeftNode != null)
            {
                foreach (ItemValue item in ScanInOrder(root.LeftNode))
                {
                    yield return item;
                }
            }

            yield return root.Item;

            if (root.RightNode != null)
            {
                foreach (ItemValue item in ScanInOrder(root.RightNode))
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region Nested type: ItemValue

        public struct ItemValue
        {
            public U Id;

            public T Value;

            public ItemValue(T value, U id)
            {
                Value = value;
                Id = id;
            }
        }

        #endregion
    }
#endif
}
