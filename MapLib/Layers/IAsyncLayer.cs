using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace EasyMap.Layers
{
    public delegate void MapNewTileAvaliabledHandler(TileLayer sender, EasyMap.Geometries.BoundingBox bbox, Bitmap bm, int sourceWidth, int sourceHeight, ImageAttributes imageAttributes);

    public interface ITileAsyncLayer
    {

        event MapNewTileAvaliabledHandler MapNewTileAvaliable;
    }
}
