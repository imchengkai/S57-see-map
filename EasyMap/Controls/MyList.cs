using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;

namespace EasyMap.Controls
{
    public class MyGeometryList : IList<Geometry>
    {
        private List<Geometry> _List = new List<Geometry>();
        #region IList<Geometry> メンバ

        public int IndexOf(Geometry item)
        {
            return _List.IndexOf(item);
        }

        public void Insert(int index, Geometry item)
        {
            _List.Insert(index, item);
            item.Select = true;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < _List.Count)
            {
                _List[index].Select = false;
                _List.RemoveAt(index);
            }
        }

        public Geometry this[int index]
        {
            get
            {
                return _List[index];
            }
            set
            {
                _List[index] = value;
            }
        }

        #endregion

        #region ICollection<Geometry> メンバ

        public void Add(Geometry item)
        {
            item.Select = true;
            _List.Add(item);
        }

        public void AddRange(MyGeometryList item)
        {
            for (int i = 0; i < item.Count; i++)
            {
                Add(item[i]);
            }
        }

        public void Clear()
        {
            foreach (Geometry geom in _List)
            {
                geom.Select = false;
            }
            _List.Clear();
        }

        public bool Contains(Geometry item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(Geometry[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Geometry item)
        {
            item.Select = false;
            return _List.Remove(item);
        }

        #endregion

        #region IEnumerable<Geometry> メンバ

        public IEnumerator<Geometry> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        #endregion

        #region IEnumerable メンバ

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        #endregion
    }
}
