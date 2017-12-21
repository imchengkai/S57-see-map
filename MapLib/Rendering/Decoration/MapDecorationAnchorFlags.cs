using System;

namespace EasyMap.Rendering.Decoration
{
    [Flags]
    public enum MapDecorationAnchorFlags
    {
        None = 0,

        Left = 1,

        Top = 2,

        Right = 4,

        Bottom = 8,

        VerticalCenter = 16,

        HorizontalCenter = 32,

        Vertical = Top | VerticalCenter | Bottom,

        Horizontal = Left | HorizontalCenter | Right
    }


}
