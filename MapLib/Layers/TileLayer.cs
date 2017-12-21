using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using BruTile;
using BruTile.Cache;
using EasyMap.Geometries;
using EasyMap.Rendering;

namespace EasyMap.Layers
{

    public class TileLayer : Layer
    {
        #region Fields

        protected readonly ImageAttributes _imageAttributes = new ImageAttributes();
        protected readonly ITileSource _source;
        protected readonly MemoryCache<Bitmap> _bitmaps = new MemoryCache<Bitmap>(100, 200);
        protected FileCache _fileCache = null;
        protected ImageFormat _ImageFormat = null;
        protected readonly bool _showErrorInTile = true;
        InterpolationMode _interpolationMode = InterpolationMode.HighQualityBicubic;

        #endregion

        #region Properties

        public override BoundingBox Envelope
        {
            get
            {
                return new BoundingBox(
                    _source.Schema.Extent.MinX,
                    _source.Schema.Extent.MinY,
                    _source.Schema.Extent.MaxX,
                    _source.Schema.Extent.MaxY);
            }
        }

        public InterpolationMode InterpolationMode
        {
            get { return _interpolationMode; }
            set { _interpolationMode = value; }
        }

        #endregion

        #region Constructors

        public TileLayer(ITileSource tileSource, string layerName)
            : this(tileSource, layerName, new Color(), true, null)
        {
        }

        public TileLayer(ITileSource tileSource, string layerName, Color transparentColor, bool showErrorInTile)
            : this(tileSource, layerName, transparentColor, showErrorInTile, null)
        {
        }

        public TileLayer(ITileSource tileSource, string layerName, Color transparentColor, bool showErrorInTile, string fileCacheDir)
        {
            _source = tileSource;
            LayerName = layerName;
            if (!transparentColor.IsEmpty)
                _imageAttributes.SetColorKey(transparentColor, transparentColor);
            _showErrorInTile = showErrorInTile;

#if !PocketPC
            _imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
#endif
            if (!string.IsNullOrEmpty(fileCacheDir))
            {
                _fileCache = new BruTile.Cache.FileCache(fileCacheDir, "png");
                _ImageFormat = ImageFormat.Png;
            }
        }

        public TileLayer(ITileSource tileSource, string layerName, Color transparentColor, bool showErrorInTile, BruTile.Cache.FileCache fileCache, ImageFormat imgFormat)
        {
            _source = tileSource;
            LayerName = layerName;
            if (!transparentColor.IsEmpty)
                _imageAttributes.SetColorKey(transparentColor, transparentColor);
            _showErrorInTile = showErrorInTile;

#if !PocketPC
            _imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
#endif
            _fileCache = fileCache;
            _ImageFormat = imgFormat;
        }

        #endregion

        System.Collections.Hashtable _cacheTiles = new System.Collections.Hashtable();

        #region Public methods

        public override void Render(Graphics graphics, Map map,RenderType rendertype)
        {
            Bitmap bmp = new Bitmap(map.Size.Width, map.Size.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = InterpolationMode;
            g.Transform = graphics.Transform.Clone();

            Extent extent = new Extent(map.Envelope.Min.X, map.Envelope.Min.Y, map.Envelope.Max.X, map.Envelope.Max.Y);
            int level = BruTile.Utilities.GetNearestLevel(_source.Schema.Resolutions, map.PixelSize);
            IList<TileInfo> tiles = _source.Schema.GetTilesInView(extent, level);

            IList<WaitHandle> waitHandles = new List<WaitHandle>();

            foreach (TileInfo info in tiles)
            {
                if (_bitmaps.Find(info.Index) != null) continue;
                if (_fileCache != null && _fileCache.Exists(info.Index))
                {
                    _bitmaps.Add(info.Index, GetImageFromFileCache(info) as Bitmap);
                    continue;
                }

                AutoResetEvent waitHandle = new AutoResetEvent(false);
                waitHandles.Add(waitHandle);
                ThreadPool.QueueUserWorkItem(GetTileOnThread, new object[] { _source.Provider, info, _bitmaps, waitHandle });
            }

            foreach (WaitHandle handle in waitHandles)
                handle.WaitOne();

            foreach (TileInfo info in tiles)
            {
                Bitmap bitmap = _bitmaps.Find(info.Index);
                if (bitmap == null) continue;

                PointF min = map.WorldToImage(new Geometries.Point(info.Extent.MinX, info.Extent.MinY));
                PointF max = map.WorldToImage(new Geometries.Point(info.Extent.MaxX, info.Extent.MaxY));

                min = new PointF((float)Math.Round(min.X), (float)Math.Round(min.Y));
                max = new PointF((float)Math.Round(max.X), (float)Math.Round(max.Y));

                try
                {
                    g.DrawImage(bitmap,
                        new Rectangle((int)min.X, (int)max.Y, (int)(max.X - min.X), (int)(min.Y - max.Y)),
                        0, 0, _source.Schema.Width, _source.Schema.Height,
                        GraphicsUnit.Pixel,
                        _imageAttributes);
                }
                catch (Exception ee)
                {
                    /*GDI+ Hell*/
                }

            }

            graphics.Transform = new Matrix();
            graphics.DrawImageUnscaled(bmp, 0, 0);
            graphics.Transform = g.Transform;

            g.Dispose();
        }

        #endregion

        #region Private methods

        private void GetTileOnThread(object parameter)
        {
            object[] parameters = (object[])parameter;
            if (parameters.Length != 4) throw new ArgumentException("Three parameters expected");
            ITileProvider tileProvider = (ITileProvider)parameters[0];
            TileInfo tileInfo = (TileInfo)parameters[1];
            MemoryCache<Bitmap> bitmaps = (MemoryCache<Bitmap>)parameters[2];
            AutoResetEvent autoResetEvent = (AutoResetEvent)parameters[3];


            byte[] bytes;
            try
            {
                bytes = tileProvider.GetTile(tileInfo);
                Bitmap bitmap = new Bitmap(new MemoryStream(bytes));
                bitmaps.Add(tileInfo.Index, bitmap);
                if (_fileCache != null)
                {
                    AddImageToFileCache(tileInfo, bitmap);
                }
            }
            catch (WebException ex)
            {
                if (_showErrorInTile)
                {
                    Bitmap bitmap = new Bitmap(_source.Schema.Width, _source.Schema.Height);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.DrawString(ex.Message, new Font(FontFamily.GenericSansSerif, 12), new SolidBrush(Color.Black),
                        new RectangleF(0, 0, _source.Schema.Width, _source.Schema.Height));
                    bitmaps.Add(tileInfo.Index, bitmap);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                autoResetEvent.Set();
            }
        }

        protected void AddImageToFileCache(TileInfo tileInfo, Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, _ImageFormat);
            ms.Seek(0, SeekOrigin.Begin);
            byte[] data = new byte[ms.Length];
            ms.Read(data, 0, data.Length);
            ms.Dispose();
            _fileCache.Add(tileInfo.Index, data);
        }

        protected Image GetImageFromFileCache(TileInfo info)
        {
            MemoryStream ms = new MemoryStream(_fileCache.Find(info.Index));
            Image img = Image.FromStream(ms);
            ms.Dispose();
            return img;
        }
        #endregion
    }
}

