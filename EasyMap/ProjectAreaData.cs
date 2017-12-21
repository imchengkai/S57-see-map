using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;

namespace EasyMap
{
    public class ProjectAreaData
    {
        private string _AreaName;
        private Geometry _AreaObject;

        public Geometry AreaObject
        {
            get { return _AreaObject; }
            set { _AreaObject = value; }
        }

        public string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }
    }
}
