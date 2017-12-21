

namespace EasyMap.Styles
{
    public interface IStyle
    {
        double MinVisible { get; set; }

        double MaxVisible { get; set; }

        bool Enabled { get; set; }
    }
}
