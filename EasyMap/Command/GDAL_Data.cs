using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;
using System.Drawing;
using System.Collections;

namespace EasyMap
{
    [Serializable]
    public class GDAL_Data
    {
        private BoundingBox _DisplayBbox;
        private Size _DisplaySize;
        private List<System.Drawing.Point> _BitmapBR=new List<System.Drawing.Point>();
        private List<System.Drawing.Point> _BitmapTL = new List<System.Drawing.Point>();
        private double _PixelWidth = 0;
        private double _PixelHeight = 0;
        private double _MapMinX = 0;
        private double _MapMaxY = 0;
        private List<string> _FileList = new List<string>();
        private List<int> _OverViewLevel=new List<int>();
        private List<int> _OverViewScale = new List<int>();

        public List<int> OverViewScale
        {
            get { return _OverViewScale; }
            set { _OverViewScale = value; }
        }

        public List<int> OverViewLevel
        {
            get { return _OverViewLevel; }
            set { _OverViewLevel = value; }
        }
        

        public List<string> FileList
        {
            get { return _FileList; }
            set { _FileList = value; }
        }

        public double MapMaxY
        {
            get { return _MapMaxY; }
            set { _MapMaxY = value; }
        }

        public double MapMinX
        {
            get { return _MapMinX; }
            set { _MapMinX = value; }
        }

        public double PixelHeight
        {
            get { return _PixelHeight; }
            set { _PixelHeight = value; }
        }

        public double PixelWidth
        {
            get { return _PixelWidth; }
            set { _PixelWidth = value; }
        }

        public List<System.Drawing.Point> BitmapTL
        {
            get { return _BitmapTL; }
            set { _BitmapTL = value; }
        }

        public List<System.Drawing.Point> BitmapBR
        {
            get { return _BitmapBR; }
            set { _BitmapBR = value; }
        }

        public Size DisplaySize
        {
            get { return _DisplaySize; }
            set { _DisplaySize = value; }
        }

        public BoundingBox DisplayBbox
        {
            get { return _DisplayBbox; }
            set { _DisplayBbox = value; }
        }
    }
}
