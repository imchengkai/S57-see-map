using System;
using System.Collections.Generic;
using System.Drawing;

namespace EasyMap.Rendering.Symbolizer
{
    public static class Utility
    {
        private static readonly Random RNG = new Random(998762);
        private static readonly List<KnownColor> _knownColors;

        static Utility()
        {
            _knownColors = new List<KnownColor>();
            _knownColors.AddRange((KnownColor[])Enum.GetValues(typeof(KnownColor)));

            _knownColors.RemoveAll(x => (int)x < 27);
        }

        public static Color RandomKnownColor()
        {
            return Color.FromKnownColor(_knownColors[RNG.Next(0, _knownColors.Count - 1)]);
        }

        public static float ScaleSizeToDeviceUnits(float size, GraphicsUnit unit, Graphics g)
        {
            if (unit == g.PageUnit)
                return size;

            switch (unit)
            {
                case GraphicsUnit.Point:
                    size *= g.DpiY / 72f;
                    break;
                case GraphicsUnit.Display:
                    size *= g.DpiY / (g.DpiY < 100 ? 72f : 100f);
                    break;
                case GraphicsUnit.Document:
                    size *= g.DpiY / 300;
                    break;
                case GraphicsUnit.Inch:
                    size *= g.DpiY;
                    break;
                case GraphicsUnit.Millimeter:
                    size *= g.DpiY / 25.4f;
                    break;
                case GraphicsUnit.World:
                    size *= g.DpiY / g.PageScale;
                    break;
                /*
            case GraphicsUnit.Pixel:
            default:
                break;
             */
            }
            return (float)Math.Round(size, MidpointRounding.AwayFromZero);

        }
    }
}
