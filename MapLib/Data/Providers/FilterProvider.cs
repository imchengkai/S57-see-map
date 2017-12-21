using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMap.Data.Providers
{
    public abstract class FilterProvider
    {
        #region Delegates

        public delegate bool FilterMethod(FeatureDataRow dr);

        #endregion

        public FilterMethod FilterDelegate { get; set; }

    }
}

