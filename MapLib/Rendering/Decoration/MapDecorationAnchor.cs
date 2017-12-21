namespace EasyMap.Rendering.Decoration
{
    public enum MapDecorationAnchor
    {
        Default = RightBottom,

        LeftTop = MapDecorationAnchorFlags.Left | MapDecorationAnchorFlags.Top,

        LeftCenter = MapDecorationAnchorFlags.Left | MapDecorationAnchorFlags.VerticalCenter,

        LeftBottom = MapDecorationAnchorFlags.Left | MapDecorationAnchorFlags.Bottom,

        RightTop = MapDecorationAnchorFlags.Right | MapDecorationAnchorFlags.Top,

        RightBottom = MapDecorationAnchorFlags.Right | MapDecorationAnchorFlags.Bottom,

        RightCenter = MapDecorationAnchorFlags.Right | MapDecorationAnchorFlags.VerticalCenter,

        CenterTop = MapDecorationAnchorFlags.HorizontalCenter | MapDecorationAnchorFlags.Top,

        CenterBottom = MapDecorationAnchorFlags.HorizontalCenter | MapDecorationAnchorFlags.Bottom,

        Center = MapDecorationAnchorFlags.HorizontalCenter | MapDecorationAnchorFlags.VerticalCenter,
    }
}
