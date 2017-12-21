using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoSettings
{
    [Serializable]
    public class PhotoData
    {
        private int _PhotoId = 0;
        private string _Name;
        private string _FileName;
        private double _MinX = 0;
        private double _MinY = 0;
        private double _MaxX = 0;
        private double _MaxY = 0;
        private double _Transform1 = 0;
        private double _Transform2 = 0;
        private double _Transform3 = 0;
        private double _Transform4 = 0;
        private double _Transform5 = 0;
        private double _Transform6 = 0;
        private int _Width = 0;
        private int _Height = 0;

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public double Transform6
        {
            get { return _Transform6; }
            set { _Transform6 = value; }
        }

        public double Transform5
        {
            get { return _Transform5; }
            set { _Transform5 = value; }
        }

        public double Transform4
        {
            get { return _Transform4; }
            set { _Transform4 = value; }
        }

        public double Transform3
        {
            get { return _Transform3; }
            set { _Transform3 = value; }
        }

        public double Transform2
        {
            get { return _Transform2; }
            set { _Transform2 = value; }
        }

        public double Transform1
        {
            get { return _Transform1; }
            set { _Transform1 = value; }
        }


        public double MaxY
        {
            get { return _MaxY; }
            set { _MaxY = value; }
        }

        public double MaxX
        {
            get { return _MaxX; }
            set { _MaxX = value; }
        }

        public double MinY
        {
            get { return _MinY; }
            set { _MinY = value; }
        }

        public double MinX
        {
            get { return _MinX; }
            set { _MinX = value; }
        }

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int PhotoId
        {
            get { return _PhotoId; }
            set { _PhotoId = value; }
        }
    }
}
