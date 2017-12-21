using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PhotoSettings;

namespace EasyMap
{
    [Serializable]
    public class AreaData
    {
        private RectangleF _Area;
        private string _Name;
        private PhotoData _PhotoData;

        public PhotoData PhotoData
        {
            get { return _PhotoData; }
            set { _PhotoData = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public RectangleF Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
    }
}
