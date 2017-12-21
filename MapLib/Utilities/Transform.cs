using System.Drawing;
using EasyMap.Geometries;
using Point = EasyMap.Geometries.Point;

namespace EasyMap.Utilities
{
    /// <summary>
    /// 坐标变换
    /// </summary>
    public class Transform
    {
        /// <summary>
        /// 按照地图将指定点变换为显示坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static PointF WorldtoMap(Point p, Map map)
        {
            //if (map.MapTransform != null && !map.MapTransform.IsIdentity)
            //	map.MapTransform.TransformPoints(new System.Drawing.PointF[] { p });
            PointF result = new System.Drawing.Point();
            double height = (map.Zoom * map.Size.Height) / map.Size.Width;
            double left = map.Center.X - map.Zoom * 0.5;
            double top = map.Center.Y + height * 0.5 * map.PixelAspectRatio;
            result.X = (float)((p.X - left) / map.PixelWidth);
            result.Y = (float)((top - p.Y) / map.PixelHeight);
            if (double.IsNaN(result.X) || double.IsNaN(result.Y))
                result = PointF.Empty;
            return result;
        }

        /// <summary>
        /// 按照地图将指定点变换为地图坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Point MapToWorld(PointF p, Map map)
        {
            if (map.Center.IsEmpty() || double.IsNaN(map.MapHeight))
            {
                return new Point(0, 0);
            }
            else
            {
                Point ul = new Point(map.Center.X - map.Zoom * .5, map.Center.Y + map.MapHeight * .5);
                return new Point(ul.X + p.X * map.PixelWidth,
                                 ul.Y - p.Y * map.PixelHeight);
            }
        }
    }
}