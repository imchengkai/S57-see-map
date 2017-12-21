

using EasyMap.Data;
using EasyMap.Styles;

namespace EasyMap.Rendering.Thematics
{
    public interface ITheme
    {
        IStyle GetStyle(FeatureDataRow attribute);
    }
}
