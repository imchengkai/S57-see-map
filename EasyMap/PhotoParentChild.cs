using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMapServer
{
    class PhotoParentChild
    {
        private string _ParentName;
        private string _ChildName;

        public string ChildName
        {
            get { return _ChildName; }
            set { _ChildName = value; }
        }

        public string ParentName
        {
            get { return _ParentName; }
            set { _ParentName = value; }
        }
    }
}
