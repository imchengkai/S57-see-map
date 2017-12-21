
using System;

namespace EasyMap.Styles
{
    [Serializable]
    public class Style : IStyle
    {
        private double _maxVisible;
        private double _minVisible;
        private bool _visible;

        public Style()
        {
            _minVisible = 0;
            _maxVisible = double.MaxValue;
            _visible = true;
        }

        #region IStyle Members

        public double MinVisible
        {
            get { return _minVisible; }
            set { _minVisible = value; }
        }

        public double MaxVisible
        {
            get { return _maxVisible; }
            set { _maxVisible = value; }
        }

        public bool Enabled
        {
            get { return _visible; }
            set { _visible = value; }
        }

        #endregion
    }
}