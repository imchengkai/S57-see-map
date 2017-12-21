using System.Drawing;

namespace EasyMap.Rendering.Decoration
{
    public interface IMapDecoration
    {
        void Render(Graphics g, Map map);
    }
}