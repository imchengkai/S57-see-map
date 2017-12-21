using System;
using System.Collections.Generic;
using System.Text;
using EasyMap.Layers;
namespace EasyMap.Data
{
    [Serializable]
    public class PropertyData
    {
        private string _PropertyName = "";
        private string _Data = "";
        private string _PropertyType = "0";
        private int _No = 0;

        public int No
        {
            get { return _No; }
            set { _No = value; }
        }

        public string PropertyType
        {
            get { return _PropertyType; }
            set { _PropertyType = value; }
        }

        public string Data
        {
            get { return _Data; }
            set 
            {
                _Data = value; 
            }
        }

        public string PropertyName
        {
            get { return _PropertyName; }
            set { _PropertyName = value; }
        }
    }
}
