﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EasyMap.Rendering.Decoration
{
    [Serializable]
    public abstract class MapDecoration : /* Component, */IMapDecoration
    {
        protected Size _cachedSize;
        protected Rectangle _boundingRectangle;

        protected MapDecoration()
        {
            BackgroundColor = Color.Transparent;
            Enabled = true;
            Opacity = 1;
            Location = new Point(5, 5);
            Padding = new Size(3, 3);
            Anchor = MapDecorationAnchor.RightBottom;

        }

        public bool Enabled { get; set; }


        #region Position

        public MapDecorationAnchor Anchor { get; set; }

        public Point Location { get; set; }

        private Point GetLocation(Map map)
        {
            var clipRect = map.Size;
            var objectSize = Size;

            var offsetX = Location.X;
            var offsetY = Location.Y;

            var anchors = (MapDecorationAnchorFlags)Anchor;
            switch (anchors & MapDecorationAnchorFlags.Horizontal)
            {
                case MapDecorationAnchorFlags.Right:
                    offsetX = clipRect.Width - (Location.X + objectSize.Width);
                    break;
                case MapDecorationAnchorFlags.HorizontalCenter:
                    offsetX = (clipRect.Width - (Location.X + objectSize.Width)) / 2;
                    break;
            }

            switch (anchors & MapDecorationAnchorFlags.Vertical)
            {
                case MapDecorationAnchorFlags.Bottom:
                    offsetY = clipRect.Height - (Location.Y + objectSize.Height);
                    break;
                case MapDecorationAnchorFlags.HorizontalCenter:
                    offsetY = (clipRect.Height - (Location.Y + objectSize.Height)) / 2;
                    break;
            }

            return new Point(offsetX, offsetY);
        }


        public Size Padding { get; set; }

        #endregion

        protected Color OpacityColor(Color color)
        {
            if (color.A != 255)
                return color;

            return Color.FromArgb((int)(_opacity * 255), color);
        }

        public Color BackgroundColor { get; set; }

        private float _opacity;

        public float Opacity
        {
            get { return _opacity; }
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                _opacity = value;
            }
        }

        #region Appearance Border

        public Size BorderMargin { get; set; }

        public Color BorderColor { get; set; }

        public int BorderWidth { get; set; }

        public bool RoundedEdges { get; set; }

        #endregion

        protected abstract Size InternalSize(Graphics g, Map map);

        private void CalcMapDecorationMetrics(Graphics g, Map map)
        {
            _cachedSize = InternalSize(g, map);
            var rect = new Rectangle(Point.Add(GetLocation(map), BorderMargin), _cachedSize);
            _boundingRectangle = Rectangle.Inflate(rect, 2 * BorderMargin.Width, 2 * BorderMargin.Height);
        }

        private Size Size
        {
            get { return Size.Add(_cachedSize, Size.Add(BorderMargin, BorderMargin)); }
        }

        private Region GetClipRegion(Map map)
        {
            return new Region(new Rectangle(Point.Add(GetLocation(map), BorderMargin), _cachedSize));
        }

        private static GraphicsPath CreateRoundedRectangle(Rectangle rectangle, Size margin)
        {
            var gp = new GraphicsPath();

            int x1 = rectangle.Left + margin.Width;
            int x2 = rectangle.Right - margin.Width;
            int y1 = rectangle.Top + margin.Height;
            int y2 = rectangle.Bottom - margin.Height;

            int arcWidth = 2 * margin.Width;
            int arcHeight = 2 * margin.Height;

            if (arcWidth > 0 && arcHeight > 0)
            {

                gp.AddLine(x1, rectangle.Top, x2, rectangle.Top);
                gp.AddArc(x2 - margin.Width, rectangle.Top, arcWidth, arcHeight, 270, 90);
                gp.AddLine(rectangle.Right, y1, rectangle.Right, y2);
                gp.AddArc(x2 - margin.Width, y2 - margin.Height, arcWidth, arcHeight, 0, 90);
                gp.AddLine(x2, rectangle.Bottom, x1, rectangle.Bottom);
                gp.AddArc(rectangle.Left, y2 - margin.Height, arcWidth, arcHeight, 90, 90);
                gp.AddLine(rectangle.Left, y2, rectangle.Left, y1);
                gp.AddArc(rectangle.Left, rectangle.Top, arcWidth, arcWidth, 180, 90);

                gp.CloseFigure();
            }
            else
                gp.AddRectangle(rectangle);
            return gp;
        }

        public void Render(Graphics g, Map map)
        {
            if (!Enabled)
                return;

            OnRendering(g, map);

            GraphicsPath gp;
            if (RoundedEdges && !BorderMargin.IsEmpty)
                gp = CreateRoundedRectangle(_boundingRectangle, BorderMargin);
            else
            {
                gp = new GraphicsPath();
                gp.AddRectangle(_boundingRectangle);
            }
            g.DrawPath(new Pen(OpacityColor(BorderColor), BorderWidth), gp);
            g.FillPath(new SolidBrush(OpacityColor(BackgroundColor)), gp);

            var oldClip = g.Clip;
            g.Clip = GetClipRegion(map);

            OnRender(g, map);

            g.Clip = oldClip;


            OnRendered(g, map);
        }

        protected virtual void OnRender(Graphics g, Map map)
        {
        }
        protected virtual void OnRendering(Graphics g, Map map)
        {
            CalcMapDecorationMetrics(g, map);
        }
        protected virtual void OnRendered(Graphics g, Map map)
        {
        }
    }
}
