
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using EasyMap.Geometries;
using Point = EasyMap.Geometries.Point;

namespace EasyMap.Rendering.Symbolizer
{
    [Serializable]
    public class ListPointSymbolizer : Collection<PointSymbolizer>, IPointSymbolizer
    {
        private Size _size;

        #region Collection<T> overrides
        protected override void ClearItems()
        {
            base.ClearItems();
            _size = new Size();
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            _size = new Size();
        }

        protected override void InsertItem(int index, PointSymbolizer item)
        {
            base.InsertItem(index, item);
            _size = new Size();
        }

        protected override void SetItem(int index, PointSymbolizer item)
        {
            base.SetItem(index, item);
            _size = new Size();
        }
        #endregion

        public void Render(Map map, IPuntal points, Graphics g)
        {
            foreach (var pointSymbolizer in Items)
                pointSymbolizer.Render(map, points, g);
        }

        public PointF Offset
        {
            get { return PointF.Empty; }
            set { }
        }

        public float Rotation
        {
            get { return 0f; }
            set { }
        }

        public Size Size
        {
            get
            {
                if (!_size.IsEmpty)
                    foreach (PointSymbolizer pointSymbolizer in Items)
                    {
                        var scale = pointSymbolizer.Scale;
                        var size = pointSymbolizer.Size;
                        var width = (int)Math.Max(_size.Width, scale * size.Width);
                        var height = (int)Math.Max(_size.Height, scale * size.Height);
                        _size = new Size(width, height);
                    }
                return _size;
            }
            set
            {
            }
        }

        public float Scale
        {
            get
            {
                return 1;
            }
            set { }
        }


        public void Begin(Graphics g, Map map, int aproximateNumberOfGeometries)
        {
        }

        public void Symbolize(Graphics g, Map map)
        {
        }

        public void End(Graphics g, Map map)
        {
        }
    }
}
