using System;
using System.Collections.Generic;
using EasyMap.Geometries;

namespace EasyMap.Rendering.Symbolizer
{
    public class CohenSutherlandLineClipping
    {
        [Flags]
        private enum OutsideClipCodes
        {
            Inside = 0,
            Left = 1,
            Right = 2,
            Bottom = 4,
            Top = 8
        }

        private readonly double _xmin, _xmax, _ymin, _ymax;

        public CohenSutherlandLineClipping(double xmin, double ymin, double xmax, double ymax)
        {
            _xmin = xmin;
            _ymin = ymin;
            _xmax = xmax;
            _ymax = ymax;
        }

        private OutsideClipCodes ComputeClipCode(Point point, out double x, out double y)
        {
            x = point.X;
            y = point.Y;
            return ComputeClipCode(x, y);
        }

        private OutsideClipCodes ComputeClipCode(double x, double y)
        {
            var result = OutsideClipCodes.Inside;
            if (x < _xmin) result |= OutsideClipCodes.Left;
            if (x > _xmax) result |= OutsideClipCodes.Right;
            if (y < _ymin) result |= OutsideClipCodes.Bottom;
            if (y > _ymax) result |= OutsideClipCodes.Top;
            return result;
        }

        public MultiLineString ClipLineString(LineString lineString)
        {
            var lineStrings = new List<LineString>();

            var clippedVertices = new List<Point>();

            var vertices = lineString.Vertices;
            var count = vertices.Count;

            double x0, y0;
            OutsideClipCodes oc0Initial;
            var oc0 = oc0Initial = ComputeClipCode(vertices[0], out x0, out y0);

            if (oc0 == OutsideClipCodes.Inside)
                clippedVertices.Add(vertices[0]);

            double x1old = double.NaN, y1old = double.NaN;

            for (var i = 1; i < count; i++)
            {
                double x1, y1;
                OutsideClipCodes oc1Initial;
                var oc1 = oc1Initial = ComputeClipCode(vertices[i], out x1, out y1);
                var x1Orig = x1;
                var y1Orig = y1;

                var accept = false;

                while (true)
                {
                    if ((oc0 | oc1) == OutsideClipCodes.Inside)
                    {
                        accept = true;
                        break;
                    }

                    if ((oc0 & oc1) != OutsideClipCodes.Inside)
                    {
                        break;
                    }

                    double x = double.NaN, y = double.NaN;

                    var ocOut = oc0 != OutsideClipCodes.Inside ? oc0 : oc1;

                    if ((ocOut & OutsideClipCodes.Top) == OutsideClipCodes.Top)
                    {
                        x = x0 + (x1 - x0) * (_ymax - y0) / (y1 - y0);
                        y = _ymax;
                    }
                    else if ((ocOut & OutsideClipCodes.Bottom) == OutsideClipCodes.Bottom)
                    {
                        x = x0 + (x1 - x0) * (_ymin - y0) / (y1 - y0);
                        y = _ymin;
                    }
                    else if ((ocOut & OutsideClipCodes.Right) == OutsideClipCodes.Right)
                    {
                        y = y0 + (y1 - y0) * (_xmax - x0) / (x1 - x0);
                        x = _xmax;
                    }
                    else if ((ocOut & OutsideClipCodes.Left) == OutsideClipCodes.Left)
                    {
                        y = y0 + (y1 - y0) * (_xmin - x0) / (x1 - x0);
                        x = _xmin;
                    }
                    if (oc0 == ocOut)
                    {
                        x0 = x;
                        y0 = y;
                        oc0 = ComputeClipCode(x, y);
                    }
                    else
                    {
                        x1 = x;
                        y1 = y;
                        oc1 = ComputeClipCode(x, y);
                    }
                }

                if (accept)
                {
                    if (oc0Initial != oc0)
                        clippedVertices.Add(new Point(x0, y0));

                    if (x1old != x1 || y1old != y1)
                        clippedVertices.Add(new Point(x1, y1));

                    if (oc1Initial != OutsideClipCodes.Inside)
                    {
                        if (clippedVertices.Count > 0)
                        {
                            lineStrings.Add(new LineString(clippedVertices));
                            clippedVertices = new List<Point>();
                        }
                    }
                }
                x0 = x1old = x1Orig;
                y0 = y1old = y1Orig;
                oc0 = oc0Initial = oc1Initial;
            }

            if (clippedVertices.Count > 0)
                lineStrings.Add(new LineString(clippedVertices));

            return new MultiLineString { LineStrings = lineStrings };
        }


        public MultiLineString ClipLineString(MultiLineString lineStrings)
        {
            var clippedLineStrings = new List<LineString>();


            foreach (LineString s in lineStrings)
                clippedLineStrings.AddRange(ClipLineString(s).LineStrings);


            return new MultiLineString { LineStrings = clippedLineStrings };
        }

    }
}
