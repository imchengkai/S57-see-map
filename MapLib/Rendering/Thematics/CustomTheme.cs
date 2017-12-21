

using EasyMap.Data;
using EasyMap.Styles;

namespace EasyMap.Rendering.Thematics
{
    public class CustomTheme : ITheme
    {
        #region Delegates

        public delegate IStyle GetStyleMethod(FeatureDataRow dr);

        #endregion

        private IStyle _DefaultStyle;

        private GetStyleMethod _getStyleDelegate;


        public CustomTheme(GetStyleMethod getStyleMethod)
        {
            _getStyleDelegate = getStyleMethod;
        }

        public IStyle DefaultStyle
        {
            get { return _DefaultStyle; }
            set { _DefaultStyle = value; }
        }

        public GetStyleMethod StyleDelegate
        {
            get { return _getStyleDelegate; }
            set { _getStyleDelegate = value; }
        }

        #region ITheme Members

        public IStyle GetStyle(FeatureDataRow row)
        {
            IStyle style = _getStyleDelegate(row);
            if (style != null)
                return style;
            else
                return _DefaultStyle;
        }

        #endregion
    }
}
