using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMap
{
    public class AreaColor
    {
        private decimal _MapId;
        private decimal _LayerId;
        private decimal _ObjectId;
        private decimal _Number;

        public decimal Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        public decimal ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        public decimal LayerId
        {
            get { return _LayerId; }
            set { _LayerId = value; }
        }

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

    }
}
