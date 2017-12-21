

using System.Drawing;
using System.Drawing.Drawing2D;
using EasyMap.Rendering.Thematics;

namespace EasyMap.Styles
{
    public class LabelStyle : Style
    {
        #region HorizontalAlignmentEnum enum

        public enum HorizontalAlignmentEnum : short
        {
            Left = 0,
            Right = 2,
            Center = 1
        }

        #endregion

        #region VerticalAlignmentEnum enum

        public enum VerticalAlignmentEnum : short
        {
            Bottom = 0,
            Top = 2,
            Middle = 1
        }

        #endregion

        private Brush _BackColor;
        private SizeF _CollisionBuffer;
        private bool _CollisionDetection;

        private Font _Font;

        private Color _ForeColor;
        private Pen _Halo;
        private HorizontalAlignmentEnum _HorisontalAlignment;
        private PointF _Offset;
        private VerticalAlignmentEnum _VerticalAlignment;
        private float _rotation;
        private bool _ignoreLength;

        public LabelStyle()
        {
            _Font = new Font("Times New Roman", 12f);
            _Offset = new PointF(0, 0);
            _CollisionDetection = false;
            _CollisionBuffer = new Size(0, 0);
            _ForeColor = Color.Black;
            _HorisontalAlignment = HorizontalAlignmentEnum.Center;
            _VerticalAlignment = VerticalAlignmentEnum.Middle;
        }

        internal StringFormat GetStringFormat()
        {
            var r = (StringFormat)StringFormat.GenericTypographic.Clone();
            switch (HorizontalAlignment)
            {
                case HorizontalAlignmentEnum.Center:
                    r.Alignment = StringAlignment.Center;
                    break;
                case HorizontalAlignmentEnum.Left:
                    r.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignmentEnum.Right:
                    r.Alignment = StringAlignment.Far;
                    break;
            }
            switch (VerticalAlignment)
            {
                case VerticalAlignmentEnum.Middle:
                    r.LineAlignment = StringAlignment.Center;
                    break;
                case VerticalAlignmentEnum.Top:
                    r.LineAlignment = StringAlignment.Near;
                    break;
                case VerticalAlignmentEnum.Bottom:
                    r.LineAlignment = StringAlignment.Far;
                    break;
            }
            return r;
        }

        public Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        public Brush BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }

        [System.ComponentModel.Category("Uneditable")]
        [System.ComponentModel.Editor()]
        public Pen Halo
        {
            get { return _Halo; }
            set
            {
                _Halo = value;
                if (_Halo != null)
                    _Halo.LineJoin = LineJoin.Round;
            }
        }


        [System.ComponentModel.Category("Uneditable")]
        public PointF Offset
        {
            get { return _Offset; }
            set { _Offset = value; }
        }

        [System.ComponentModel.Category("Collision Detection")]
        public bool CollisionDetection
        {
            get { return _CollisionDetection; }
            set { _CollisionDetection = value; }
        }

        [System.ComponentModel.Category("Collision Detection")]
        public SizeF CollisionBuffer
        {
            get { return _CollisionBuffer; }
            set { _CollisionBuffer = value; }
        }

        [System.ComponentModel.Category("Alignment")]
        public HorizontalAlignmentEnum HorizontalAlignment
        {
            get { return _HorisontalAlignment; }
            set { _HorisontalAlignment = value; }
        }

        [System.ComponentModel.Category("Alignment")]
        public VerticalAlignmentEnum VerticalAlignment
        {
            get { return _VerticalAlignment; }
            set { _VerticalAlignment = value; }
        }

        [System.ComponentModel.Category("Alignment")]
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value % 360f; }
        }

        [System.ComponentModel.Category("Alignment")]
        public bool IgnoreLength
        {
            get { return _ignoreLength; }
            set { _ignoreLength = value; }
        }

    }
}
