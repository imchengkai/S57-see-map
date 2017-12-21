

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using EasyMap.Geometries;
using EasyMap.Rendering.Symbolizer;
using EasyMap.Utilities;
using Point = EasyMap.Geometries.Point;
using System.Runtime.CompilerServices;
using EasyMap.Styles;
using System.Drawing.Text;

namespace EasyMap.Rendering
{
    public enum RenderType
    {
        All,
        Text,
        Symbol,
        Select
    }
    public static class VectorRenderer
    {
        internal const float ExtremeValueLimit = 1E+8f;
        internal const float NearZero = 1E-30f; // 1/Infinity

        static VectorRenderer()
        {
            SizeOfString = SizeOfStringCeiling;
        }

        private static readonly Bitmap Defaultsymbol = new Bitmap(2, 2);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawMultiLineString(Graphics g, MultiLineString lines, Pen pen, Map map, float offset, VectorStyle style, RenderType renderType)
        {
            for (int i = 0; i < lines.LineStrings.Count; i++)
            {
                lines.LineStrings[i].Visible = lines.Visible;
                lines.LineStrings[i].Select = lines.Select;
                DrawLineString(g, lines.LineStrings[i], pen, map, offset, style, renderType);
            }
        }

        private static PointF[] OffsetRight(PointF[] points, float offset)
        {
            int length = points.Length;
            PointF[] newPoints = new PointF[(length - 1) * 2];

            float space = (offset * offset / 4) + 1;

            if (length > 2)
            {
                int counter = 0;
                float a, b, x = 0, y = 0, c;
                for (int i = 0; i < length - 1; i++)
                {
                    b = -(points[i + 1].X - points[i].X);
                    if (b != 0)
                    {
                        a = points[i + 1].Y - points[i].Y;
                        c = a / b;
                        y = 2 * (float)Math.Sqrt(space / (c * c + 1));
                        y = b < 0 ? y : -y;
                        x = c * y;

                        if (offset < 0)
                        {
                            y = -y;
                            x = -x;
                        }

                        newPoints[counter] = new PointF(points[i].X + x, points[i].Y + y);
                        newPoints[counter + 1] = new PointF(points[i + 1].X + x, points[i + 1].Y + y);
                    }
                    else
                    {
                        newPoints[counter] = new PointF(points[i].X + x, points[i].Y + y);
                        newPoints[counter + 1] = new PointF(points[i + 1].X + x, points[i + 1].Y + y);
                    }
                    counter += 2;
                }

                return newPoints;
            }
            return points;
        }


        private static void DrawTrain(Graphics g, Pen pen, GraphicsPath gp, bool select)
        {
            Pen pen1 = new Pen(Color.Black);
            pen1.Width = pen.Width;
            pen1.DashStyle = DashStyle.Solid;
            g.DrawPath(pen1, gp);

            Pen pen2 = new Pen(select ? Color.Red : Color.White);
            pen2.Width = pen.Width;
            pen2.DashPattern = new float[] { 3, 3, 3, 3 };
            g.DrawPath(pen2, gp);

            Pen pen3 = new Pen(Color.Black);
            pen3.Width = pen.Width;
            pen3.CompoundArray = new float[] { 0, 1f / pen3.Width, 1 - 1f / pen3.Width, 1 };
            g.DrawPath(pen2, gp);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawLineString(Graphics g, LineString line, Pen pen, Map map, VectorStyle style)
        {
            DrawLineString(g, line, pen, map, 0, style, RenderType.All);
        }

        public static float width = 0f;//街道线原始宽度
        public static void DrawLineString(Graphics g, LineString line, Pen pen, Map map, float offset, VectorStyle style, RenderType renderType)
        {
            //解决道路放大后变细 20170102 刘 start
            string layerName = null;
            //获取该线所在图层名字
            for (int i = 0; i < map.Layers.Count; i++)
            {
                if (line.LayerId == map.Layers[i].ID)
                {
                    layerName = map.Layers[i].LayerName;
                }
            }
                //路放大后，宽度加宽
            //if (layerName.Contains("路"))
            //{
            //    if ((style.Outline.Width == width) && map.Zoom <= 1688f && map.Zoom > 932)
            //    {
            //        style.Outline.Width = style.Outline.Width * 1.5f;
            //    }
            //    if ((style.Outline.Width == width) && map.Zoom <= 932f && map.Zoom > 488f)
            //    {
            //        style.Outline.Width = style.Outline.Width + style.Outline.Width;
            //    }
            //    if ((style.Outline.Width == width) && map.Zoom <= 488f && map.Zoom > 288f)
            //    {
            //        style.Outline.Width = style.Outline.Width * 3;
            //    }
            //    if ((style.Outline.Width == width) && map.Zoom <= 288f)
            //    {
            //        style.Outline.Width = style.Outline.Width * 5;
            //    }
            //}
            //end
            if (line.Vertices.Count > 1 && line.Visible)
            {
                #region 画线
                GraphicsPath gp = new GraphicsPath();
                PointF[] points = null;
                if (offset == 0)
                {
                    points = line.TransformToImage(map);
                    gp.AddLines(/*LimitValues(*/points/*, ExtremeValueLimit)*/);
                }
                else
                {
                    points = OffsetRight(/*LimitValues(*/line.TransformToImage(map)/*, ExtremeValueLimit)*/, offset);
                    gp.AddLines(points);
                }
                if (line.Select)
                {
                    if (renderType == RenderType.All || renderType == RenderType.Select)
                    {
                        Pen temppen = pen.Clone() as Pen;
                        temppen.Width += 2;
                        temppen.Color = Color.Red;
                        if (style.Penstyle == 5)
                        {
                            DrawTrain(g, temppen, gp, true);
                        }
                        else
                        {
                            if (style.Penstyle == 6)
                            {
                                Pen temppen1 = new Pen(Color.Pink, pen.Width);
                                g.DrawPath(temppen1, gp);
                            }
                            g.DrawPath(temppen, gp);
                        }
                    }
                }
                else
                {
                    if (renderType == RenderType.All || renderType == RenderType.Symbol)
                    {
                        if (style.Penstyle == 5)
                        {
                            DrawTrain(g, pen, gp, false);
                        }
                        else
                        {
                            if (style.Penstyle == 6)
                            {
                                Pen temppen = new Pen(style.Fill.Color, pen.Width);
                                g.DrawPath(temppen, gp);
                            }
                            g.DrawPath(pen, gp);
                        }
                    }
                }
                #endregion
                //设置高德地图字体 20170102 刘
                System.Drawing.Text.PrivateFontCollection colFont = new System.Drawing.Text.PrivateFontCollection();
                colFont.AddFontFile(Environment.CurrentDirectory + @"\font\regular.ttf");
                Font myFont = null;
                //end
                //绘制文字
                if (line.Text != "" && (renderType == RenderType.Text || renderType == RenderType.All))
                {
                    /* 
                     * 2016-03-01 刘岩 start 修改源代码根据字符个数分割距离绘制文字
                     * 根据线路实际长度和地图比例分割距离来进行绘制文字
                     * 2016-03-03 刘岩 end
                     */
                    double _MapZoom = map.Zoom;

                    double _TempZoom = 30000;
                    bool blZoom = map.Zoom < _TempZoom;

                    int len = 0;
                    if (blZoom)
                    {
                        len = (int)(line.Length * 2 / _MapZoom) + 2;
                    }
                    else
                    {
                        // 原代码
                        len = line.Text.Length + 1;
                    }

                    // 取得X的最大最小坐标
                    PointF minx = points[0];
                    PointF maxx = points[0];
                    // 取得Y的最大最小坐标
                    PointF miny = points[0];
                    PointF maxy = points[0];

                    foreach (PointF p in points)
                    {
                        if (minx.X > p.X)
                        {
                            minx = p;
                        }
                        if (maxx.X < p.X)
                        {
                            maxx = p;
                        }
                        /* 
                        * 2016-03-01 刘岩 start 
                        * 取得Y的最大最小坐标
                        * 2016-03-03 刘岩 end
                        */
                        if (miny.Y > p.Y)
                        {
                            miny = p;
                        }
                        if (maxy.Y < p.Y)
                        {
                            maxy = p;
                        }
                    }
                    // X坐标差
                    float subX = maxx.X - minx.X;
                    // Y坐标差
                    float subY = maxy.Y - miny.Y;
                    // 1：表示操作Y坐标绘制文字 2：表示操作X坐标绘制文字
                    int temp = 0;
                    // 每间隔多少X或Y绘制文字
                    float step = 0;
                    
                    // 取X差和Y差中的最大值
                    if (subY > subX)
                    {
                        step = subY / len;
                        temp = 1;
                    }
                    if (subX >= subY || map.Zoom >= _TempZoom)
                    {
                        step = subX / len;
                        temp = 2;
                    }

                    SizeF size = g.MeasureString(line.Text.Substring(0, 1), line.TextFont);
                    // 操作X坐标
                    if (temp == 2)
                    {
                        float x = minx.X;
                        float x1 = x;
                        float y = line.GetYByX(points, x);
                        float y1 = y;
                        bool flag = true;
                        #region 文字交叉判断，如果交叉，则不绘制文字部分
                        for (int i = 2; i < len; i++)
                        {
                            x1 += step;
                            y1 = line.GetYByX(points, x1);
                            // 原代码
                            // SizeF size1 = g.MeasureString(line.Text.Substring(i - 1, 1), line.TextFont);
                            SizeF size1 = g.MeasureString(line.Text.Substring(0, 1), line.TextFont);
                            if (CheckWordRepeat(x1, x, y1, y, size, size1))
                            {
                                flag = false;
                                break;
                            }
                            size = size1;
                            x = x1;
                            y = y1;
                        }
                        #endregion
                        #region 绘制文字部分
                        if (flag)
                        {
                            x = minx.X;
                            /* 
                             * 2016-03-01 刘岩 start 修改原来只绘制一个文字
                             * 绘制文字为 道路全名
                             * 2016-03-03 刘岩 end
                             */
                            float sizeWidth = size.Width;
                            for (int i = 1; i < len; i++)
                            {
                                x += step;
                                y = line.GetYByX(points, x);

                                if (line.Select)
                                {
                                    if (blZoom)
                                    {
                                        float tempX = 0;
                                        float tempY = 0;
                                        for (int a = 1; a < line.Text.Length + 1; a++)
                                        {
                                            if (a == 1)
                                            {
                                                tempX = x;
                                                tempY = y;
                                            }
                                            else
                                            {
                                                tempX += sizeWidth;
                                                tempY = line.GetYByX(points, tempX);
                                            }
                                            //设置高德地图字体 20170102 刘
                                            myFont = new System.Drawing.Font(colFont.Families[0], line.TextFont.Size, FontStyle.Regular, GraphicsUnit.Point);
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(Color.LightYellow), tempX + 1, tempY + 1);
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(line.SelectColor), tempX, tempY);
                                        }
                                    }
                                    else
                                    {
                                        // 原代码
                                        //设置高德地图字体 20170102 刘
                                        myFont = new System.Drawing.Font(colFont.Families[0], line.TextFont.Size, FontStyle.Regular, GraphicsUnit.Point);
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(Color.LightYellow), x + 1, y + 1);
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(line.SelectColor), x, y);
                                    }
                                }
                                else
                                {
                                    Font font = line.TextFont;
                                    Color color = line.TextColor;
                                    if (style != null)
                                    {
                                        if (style.TextFont != null)
                                        {
                                            font = style.TextFont;
                                        }
                                        if (style.TextColor != Color.Empty)
                                        {
                                            color = style.TextColor;
                                        }
                                    }
                                    /*
                                     * 2016-03-01 刘岩 start 修改原来只绘制一个文字
                                     * 绘制文字为 道路全名
                                     * 2016-03-03 刘岩 end
                                     */
                                    if (blZoom)
                                    {
                                        float tempX = 0;
                                        float tempY = 0;
                                        float ya = y;
                                        for (int a = 1; a < line.Text.Length + 1; a++)
                                        {

                                            //旅顺南路写接到名字，道路弯曲特殊处理使用 20170110 liu 
                                            //start
                                            if (line.Text == "旅顺南路")
                                            {
                                                //end
                                                if (a == 1)
                                                {
                                                    tempX = x;
                                                    //旅顺南路写接到名字，道路弯曲特殊处理使用 20170110 liu 
                                                    tempY = line.GetYByXNew(points, tempX);
                                                    //tempY = line.GetYByX(points, tempX);
                                                }
                                                else
                                                {
                                                    tempX += sizeWidth;
                                                    //旅顺南路写接到名字，道路弯曲特殊处理使用 20170110 liu 
                                                    tempY = line.GetYByXNew(points, tempX);
                                                    //tempY = line.GetYByX(points, tempX);
                                                }

                                            }
                                            else
                                            {
                                                if (a == 1)
                                                {
                                                    tempX = x;
                                                    tempY = line.GetYByX(points, tempX);
                                                }
                                                else
                                                {
                                                    tempX += sizeWidth;
                                                    tempY = line.GetYByX(points, tempX);
                                                }
                                            }
                                            //line.OutlineWidth = line.OutlineWidth + (line.OutlineWidth / 2);
                                            //设置高德地图字体 20170102 刘
                                            myFont = new System.Drawing.Font(colFont.Families[0], font.Size, FontStyle.Regular, GraphicsUnit.Point);
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(Color.LightYellow), tempX - 5, tempY - 3);
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(color), tempX - 6, tempY - 4);
                                        }
                                    }
                                    else
                                    {
                                        // 原代码
                                        //设置高德地图字体 20170102 刘
                                        myFont = new System.Drawing.Font(colFont.Families[0], font.Size, FontStyle.Regular, GraphicsUnit.Point);
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(Color.LightYellow), x - 5, y - 3);
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(color), x - 6, y - 4);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    // 操作Y坐标
                    if (temp == 1)
                    {
                        float y = miny.Y;
                        float y1 = y;
                        float x = line.GetXByY(points, y);
                        float x1 = x;
                        bool flag = true;
                        #region 文字交叉判断，如果交叉，则不绘制文字部分
                        for (int i = 2; i < len; i++)
                        {
                            y1 += step;
                            x1 = line.GetXByY(points, y1);
                            // 原代码
                            // SizeF size1 = g.MeasureString(line.Text.Substring(i - 1, 1), line.TextFont);
                            SizeF size1 = g.MeasureString(line.Text.Substring(0, 1), line.TextFont);

                            if (CheckWordRepeat(y1, y, x1, x, size, size1))
                            {
                                flag = false;
                                break;
                            }
                            size = size1;
                            x = x1;
                            y = y1;
                        }
                        #endregion
                        #region 绘制文字部分
                        if (flag)
                        {
                            y = miny.Y;
                            /* 
                             * 2016-03-01 刘岩 start 修改原来只绘制一个文字
                             * 绘制文字为 道路全名
                             * 2016-03-03 刘岩 end
                             */
                            float sizeWidth = size.Width;
                            for (int i = 1; i < len; i++)
                            {
                                y += step;
                                x = line.GetXByY(points, y);
                                if (line.Select)
                                {
                                    if (blZoom)
                                    {
                                        float tempX = 0;
                                        float tempY = 0;
                                        for (int a = 1; a < line.Text.Length + 1; a++)
                                        {
                                            if (a == 1)
                                            {
                                                tempX = x;
                                                tempY = y;
                                            }
                                            else
                                            {
                                                tempY += sizeWidth;
                                                tempX = line.GetXByY(points, tempY);
                                            }
                                            //设置高德地图字体 20170102 刘
                                            myFont = new System.Drawing.Font(colFont.Families[0], line.TextFont.Size, FontStyle.Regular, GraphicsUnit.Point);
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(Color.LightYellow), tempX + 1, tempY + 1);
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(line.SelectColor), tempX, tempY);
                                        }
                                    }
                                    else
                                    {
                                        // 原代码
                                        //设置高德地图字体 20170102 刘
                                        myFont = new System.Drawing.Font(colFont.Families[0], line.TextFont.Size, FontStyle.Regular, GraphicsUnit.Point);
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(Color.LightGreen), x + 1, y + 1);
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(line.SelectColor), x, y);
                                    }
                                }
                                else
                                {
                                    Font font = line.TextFont;
                                    Color color = line.TextColor;
                                    if (style != null)
                                    {
                                        if (style.TextFont != null)
                                        {
                                            font = style.TextFont;
                                        }
                                        if (style.TextColor != Color.Empty)
                                        {
                                            color = style.TextColor;
                                        }
                                    }

                                    /*
                                     * 2016-03-01 刘岩 start 修改原来只绘制一个文字
                                     * 绘制文字为 道路全名
                                     * 2016-03-03 刘岩 end
                                     */
                                    if (blZoom)
                                    {
                                        float tempX = 0;
                                        float tempY = 0;
                                        for (int a = 1; a < line.Text.Length + 1; a++)
                                        {
                                            if (a == 1)
                                            {
                                                tempX = x;
                                                tempY = y;
                                            }
                                            else
                                            {
                                                tempY += sizeWidth;
                                                tempX = line.GetXByY(points, tempY);
                                            }
                                            //设置高德地图字体 20170102 刘
                                            myFont = new System.Drawing.Font(colFont.Families[0], font.Size, FontStyle.Regular, GraphicsUnit.Point);
                                            //g.RotateTransform(75f);
                                            //添加字体背景颜色 20170101 刘 start
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(Color.LightYellow), tempX - 6, tempY - 7);
                                            //end
                                            g.DrawString(line.Text.Substring(a - 1, 1), myFont, new SolidBrush(color), tempX - 7, tempY - 8);
                                        }
                                    }
                                    else
                                    {
                                        //设置高德地图字体 20170102 刘
                                        myFont = new System.Drawing.Font(colFont.Families[0], font.Size, FontStyle.Regular, GraphicsUnit.Point);
                                        //添加字体背景颜色 20170101 刘 start
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(Color.LightGreen), x - 6, y - 7);
                                        //end
                                        // 原代码
                                        g.DrawString(line.Text.Substring(i - 1, 1), myFont, new SolidBrush(color), x - 7, y - 8);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
        }
        /// <summary>
        /// 文字交叉判断
        /// </summary>
        public static bool CheckWordRepeat(float x1, float x, float y1, float y, SizeF size, SizeF size1)
        {
            if ((x1 >= x && x1 <= x + size.Width) && (y1 >= y && y1 <= y + size.Height)
                                || (x1 + size1.Width >= x && x1 + size1.Width <= x + size.Width) && (y1 >= y && y1 <= y + size.Height)
                                || (x1 >= x && x1 <= x + size.Width) && (y1 + size1.Height >= y && y1 + size1.Height <= y + size.Height)
                                || (x1 + size1.Width >= x && x1 + size1.Width <= x + size.Width) && (y1 + size1.Height >= y && y1 + size1.Height <= y + size.Height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawMultiPolygon(Graphics g, MultiPolygon pols, Brush brush, Pen pen, bool clip, Map map, VectorStyle style, RenderType renderType, string LayerType)
        {

            for (int i = 0; i < pols.Polygons.Count; i++)
            {
                pols.Polygons[i].Select = pols.Select;
                pols.Polygons[i].Visible = pols.Visible;
                DrawPolygon(g, pols.Polygons[i], brush, pen, clip, map, style, renderType, LayerType);
            }
            if (pols.Text != "" && (renderType == RenderType.All || renderType == RenderType.Text))
            {

                if (pols.Select)
                {
                    Font font = pols.TextFont;
                    if (style != null)
                    {
                        if (style.TextFont != null)
                        {
                            font = style.TextFont;
                        }
                    }
                    SizeF size = g.MeasureString(pols.Text, font);
                    EasyMap.Geometries.Point center = GetGravity(pols,LayerType);// pols.Center;
                    PointF p = center.TransformToImage(map);
                    RectangleF rec = new RectangleF(p.X - size.Width / 2, p.Y - size.Height / 2, size.Width, size.Height);
                    g.DrawString(pols.Text, font, new SolidBrush(pols.SelectColor), rec);
                    //g.DrawString(pols.Text, pols.TextFont, new SolidBrush(pols.SelectColor), rec);
                }
                else
                {
                    Font font = pols.TextFont;
                    Color color = pols.TextColor;
                    if (style != null)
                    {
                        if (style.TextFont != null)
                        {
                            font = style.TextFont;
                        }
                        if (style.TextColor != Color.Empty)
                        {
                            color = style.TextColor;
                        }
                    }
                    SizeF size = g.MeasureString(pols.Text, font);
                    EasyMap.Geometries.Point center = GetGravity(pols, LayerType);//pols.Center;
                   //decimal a =  map.CurrentLayer.ID;
                    PointF p = center.TransformToImage(map);
                    RectangleF rec = new RectangleF(p.X - size.Width / 2, p.Y - size.Height / 2, size.Width, size.Height);
                    g.DrawString(pols.Text, font, new SolidBrush(color), rec);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawPolygon(Graphics g, Polygon pol, Brush brush, Pen pen, bool clip, Map map, VectorStyle style, RenderType renderType, string LayerType)
        {
            if (pol.ExteriorRing == null)
                return;
            if (pol.ExteriorRing.Vertices.Count > 2 && pol.Visible)
            {
                GraphicsPath gp = new GraphicsPath();

                if (!clip)
                    gp.AddPolygon(/*LimitValues(*/pol.ExteriorRing.TransformToImage(map)/*, ExtremeValueLimit)*/);
                else
                    DrawPolygonClipped(gp, pol.ExteriorRing.TransformToImage(map), map.Size.Width, map.Size.Height);

                for (int i = 0; i < pol.InteriorRings.Count; i++)
                    if (!clip)
                        gp.AddPolygon(/*LimitValues(*/pol.InteriorRings[i].TransformToImage(map)/*, ExtremeValueLimit)*/);
                    else
                        DrawPolygonClipped(gp, pol.InteriorRings[i].TransformToImage(map), map.Size.Width,
                                           map.Size.Height);

                if (pol.Select)
                {
                    if (renderType == RenderType.Select || renderType == RenderType.All)
                    {
                        //if (brush != null && brush != Brushes.Transparent)
                        //    g.FillPath(brush, gp);
                        g.DrawPath(new Pen(pol.SelectColor, 3), gp);
                    }
                }
                else
                {
                    if (renderType == RenderType.All || renderType == RenderType.Symbol)
                    {
                        if (brush != null && brush != Brushes.Transparent)
                            g.FillPath(brush, gp);
                        if (pen != null)
                            g.DrawPath(pen, gp);
                    }
                }
                if (pol.Text != "" && (renderType == RenderType.All || renderType == RenderType.Text))
                {
                    if (pol.Select)
                    {
                        Font font = pol.TextFont;
                        if (style != null)
                        {
                            if (style.TextFont != null)
                            {
                                font = style.TextFont;
                            }
                        }
                        SizeF size = g.MeasureString(pol.Text, font);
                        EasyMap.Geometries.Point center = GetGravity(pol, LayerType);//.Center;
                        PointF p = center.TransformToImage(map);
                        RectangleF rec = new RectangleF(p.X - size.Width / 2, p.Y - size.Height / 2, size.Width, size.Height);
                        g.DrawString(pol.Text, font, new SolidBrush(pol.SelectColor), rec);
                    }
                    else
                    {
                        Font font = pol.TextFont;
                        Color color = pol.TextColor;
                        if (style != null)
                        {
                            if (style.TextFont != null)
                            {
                                font = style.TextFont;
                            }
                            if (style.TextColor != Color.Empty)
                            {
                                color = style.TextColor;
                            }
                        }
                        SizeF size = g.MeasureString(pol.Text, font);
                        EasyMap.Geometries.Point center = GetGravity(pol, LayerType); //pol.Center;
                        PointF p = center.TransformToImage(map);
                        RectangleF rec = new RectangleF(p.X - size.Width / 2, p.Y - size.Height / 2, size.Width, size.Height);
                        g.DrawString(pol.Text, font, new SolidBrush(color), rec);
                    }
                }
            }
        }

        private static void DrawPolygonClipped(GraphicsPath gp, PointF[] polygon, int width, int height)
        {
            ClipState clipState = DetermineClipState(polygon, width, height);
            if (clipState == ClipState.Within)
            {
                gp.AddPolygon(polygon);
            }
            else if (clipState == ClipState.Intersecting)
            {
                PointF[] clippedPolygon = ClipPolygon(polygon, width, height);
                if (clippedPolygon.Length > 2)
                    gp.AddPolygon(clippedPolygon);
            }
        }

        private static PointF[] LimitValues(PointF[] vertices, float limit)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].X = Math.Max(-limit, Math.Min(limit, vertices[i].X));
                vertices[i].Y = Math.Max(-limit, Math.Min(limit, vertices[i].Y));
            }
            return vertices;
        }

        public delegate SizeF SizeOfStringDelegate(Graphics g, string text, Font font);

        private static SizeOfStringDelegate _sizeOfString;

        public static SizeOfStringDelegate SizeOfString
        {
            get { return _sizeOfString; }
            set
            {
                if (value != null)
                    _sizeOfString = value;
            }
        }

        public static SizeF SizeOfStringBase(Graphics g, string text, Font font)
        {
            return g.MeasureString(text, font);
        }

        public static SizeF SizeOfStringCeiling(Graphics g, string text, Font font)
        {
            SizeF f = g.MeasureString(text, font);
            return new SizeF((float)Math.Ceiling(f.Width), (float)Math.Ceiling(f.Height));
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawLabel(Graphics g, PointF labelPoint, PointF offset, Font font, Color forecolor,
                                     Brush backcolor, Pen halo, float rotation, string text, Map map)
        {
            SizeF fontSize = _sizeOfString(g, text, font); //Calculate the size of the text
            labelPoint.X += offset.X;
            labelPoint.Y += offset.Y; //add label offset
            if (rotation != 0 && !float.IsNaN(rotation))
            {
                g.TranslateTransform(labelPoint.X, labelPoint.Y);
                g.RotateTransform(rotation);
                g.TranslateTransform(-fontSize.Width / 2, -fontSize.Height / 2);
                if (backcolor != null && backcolor != Brushes.Transparent)
                    g.FillRectangle(backcolor, 0, 0, fontSize.Width * 0.74f + 1f, fontSize.Height * 0.74f);
                GraphicsPath path = new GraphicsPath();
                path.AddString(text, font.FontFamily, (int)font.Style, font.Size, new System.Drawing.Point(0, 0), null);
                if (halo != null)
                    g.DrawPath(halo, path);
                g.FillPath(new SolidBrush(forecolor), path);
                g.Transform = map.MapTransform;
            }
            else
            {
                if (backcolor != null && backcolor != Brushes.Transparent)
                    g.FillRectangle(backcolor, labelPoint.X, labelPoint.Y, fontSize.Width * 0.74f + 1,
                                    fontSize.Height * 0.74f);

                GraphicsPath path = new GraphicsPath();

                path.AddString(text, font.FontFamily, (int)font.Style, font.Size, labelPoint, null);
                if (halo != null)
                    g.DrawPath(halo, path);
                g.FillPath(new SolidBrush(forecolor), path);
            }
        }

        private static ClipState DetermineClipState(PointF[] vertices, int width, int height)
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                minX = Math.Min(minX, vertices[i].X);
                minY = Math.Min(minY, vertices[i].Y);
                maxX = Math.Max(maxX, vertices[i].X);
                maxY = Math.Max(maxY, vertices[i].Y);
            }

            if (maxX < 0) return ClipState.Outside;
            if (maxY < 0) return ClipState.Outside;
            if (minX > width) return ClipState.Outside;
            if (minY > height) return ClipState.Outside;
            if (minX > 0 && maxX < width && minY > 0 && maxY < height) return ClipState.Within;
            return ClipState.Intersecting;
        }

        internal static PointF[] ClipPolygon(PointF[] vertices, int width, int height)
        {
            List<PointF> line = new List<PointF>();
            if (vertices.Length <= 1) /* nothing to clip */
                return vertices;

            for (int i = 0; i < vertices.Length - 1; i++)
            {
                float x1 = vertices[i].X;
                float y1 = vertices[i].Y;
                float x2 = vertices[i + 1].X;
                float y2 = vertices[i + 1].Y;

                float deltax = x2 - x1;
                if (deltax == 0)
                {
                    deltax = (x1 > 0) ? -NearZero : NearZero;
                }
                float deltay = y2 - y1;
                if (deltay == 0)
                {
                    deltay = (y1 > 0) ? -NearZero : NearZero;
                }

                float xin;
                float xout;
                if (deltax > 0)
                {
                    xin = 0;
                    xout = width;
                }
                else
                {
                    xin = width;
                    xout = 0;
                }

                float yin;
                float yout;
                if (deltay > 0)
                {
                    yin = 0;
                    yout = height;
                }
                else
                {
                    yin = height;
                    yout = 0;
                }

                float tinx = (xin - x1) / deltax;
                float tiny = (yin - y1) / deltay;

                float tin1;
                float tin2;
                if (tinx < tiny)
                {
                    tin1 = tinx;
                    tin2 = tiny;
                }
                else
                {
                    tin1 = tiny;
                    tin2 = tinx;
                }

                if (1 >= tin1)
                {
                    if (0 < tin1)
                        line.Add(new PointF(xin, yin));

                    if (1 >= tin2)
                    {
                        float toutx = (xout - x1) / deltax;
                        float touty = (yout - y1) / deltay;

                        float tout = (toutx < touty) ? toutx : touty;

                        if (0 < tin2 || 0 < tout)
                        {
                            if (tin2 <= tout)
                            {
                                if (0 < tin2)
                                {
                                    line.Add(tinx > tiny
                                                 ? new PointF(xin, y1 + tinx * deltay)
                                                 : new PointF(x1 + tiny * deltax, yin));
                                }

                                if (1 > tout)
                                {
                                    line.Add(toutx < touty
                                                 ? new PointF(xout, y1 + toutx * deltay)
                                                 : new PointF(x1 + touty * deltax, yout));
                                }
                                else
                                    line.Add(new PointF(x2, y2));
                            }
                            else
                            {
                                line.Add(tinx > tiny ? new PointF(xin, yout) : new PointF(xout, yin));
                            }
                        }
                    }
                }
            }
            if (line.Count > 0)
                line.Add(new PointF(line[0].X, line[0].Y));

            return line.ToArray();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawPoint(Graphics g, Point point, Brush b, float size, PointF offset, Map map, VectorStyle style, RenderType renderType)
        {
            if (point == null || !point.Visible)
                return;

            PointF pp = Transform.WorldtoMap(point, map);
            Matrix startingTransform = g.Transform;

            float width = size;
            float height = size;
            g.FillEllipse(b, (int)pp.X - width / 2 + offset.X,
                        (int)pp.Y - height / 2 + offset.Y, width, height);

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawPoint(IPointSymbolizer symbolizer, Graphics g, Point point, Map map, VectorStyle style, RenderType renderType)
        {
            if (point == null || !point.Visible)
                return;

            symbolizer.Render(map, point, g);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawPoint(Graphics g, Point point, Image symbol, float symbolscale, PointF offset,
                                     float rotation, Map map, VectorStyle style, RenderType renderType)
        {
            if (point == null || !point.Visible)
                return;

            if (symbol == null) //We have no point style - Use a default symbol
                symbol = Defaultsymbol;

            PointF pp = Transform.WorldtoMap(point, map);

            if (renderType == RenderType.All || renderType == RenderType.Symbol)
            {
                if (rotation != 0 && !Single.IsNaN(rotation))
                {
                    Matrix startingTransform = g.Transform.Clone();

                    Matrix transform = g.Transform;
                    PointF rotationCenter = pp;
                    transform.RotateAt(rotation, rotationCenter);

                    g.Transform = transform;
                    if (point.IsAreaPriceMonitor)
                    {
                        g.DrawImageUnscaled(symbol, (int)(pp.X - symbol.Width / 2f + offset.X),
                                            (int)(pp.Y - symbol.Height / 2f + offset.Y));
                    }
                    else
                    {
                        float width = symbol.Width * symbolscale;
                        float height = symbol.Height * symbolscale;
                        g.DrawImage(symbol, (int)pp.X - width / 2 + offset.X * symbolscale,
                                    (int)pp.Y - height / 2 + offset.Y * symbolscale, width, height);
                    }

                    g.Transform = startingTransform;
                }
                else
                {
                    if (symbolscale == 1f)
                    {
                        lock (symbol)
                        {
                            g.DrawImageUnscaled(symbol, (int)(pp.X - symbol.Width / 2f + offset.X),
                                                (int)(pp.Y - symbol.Height / 2f + offset.Y));
                        }
                    }
                    else
                    {
                        float width = symbol.Width * symbolscale;
                        float height = symbol.Height * symbolscale;
                        g.DrawImage(symbol, (int)pp.X - width / 2 + offset.X * symbolscale,
                                    (int)pp.Y - height / 2 + offset.Y * symbolscale, width, height);
                    }
                }
            }
            if (renderType == RenderType.Text || renderType == RenderType.All)
            {
                //if (point.Text != "")
                //{
                //    string text = point.Text;
                //    if (point.Text.Length > 6)
                //    {
                //        text = point.Text.Substring(0, point.Text.Length / 2) + "\n" + point.Text.Substring(point.Text.Length / 2, point.Text.Length - (point.Text.Length / 2));
                //    }
                //    Font font = point.TextFont;
                //    Color color = point.TextColor;
                //    if (style != null)
                //    {
                //        if (style.TextFont != null)
                //        {
                //            font = style.TextFont;
                //        }
                //        if (style.TextColor != Color.Empty)
                //        {
                //            color = style.TextColor;
                //        }
                //    }
                //    EasyMap.Geometries.Point minpoint = new EasyMap.Geometries.Point(point.X, point.Y);
                //    SizeF size = g.MeasureString(text, font);
                //    PointF p = minpoint.TransformToImage(map);
                //    RectangleF rec = new RectangleF(p.X - size.Width / 2, p.Y - size.Height / 2 + symbol.Height, size.Width, size.Height);

                //    if (point.Select)
                //    {
                //        g.DrawString(text, font, new SolidBrush(point.SelectColor), rec);
                //    }
                //    else
                //    {
                //        RectangleF rec1 = new RectangleF((p.X - size.Width / 2)+1, (p.Y - size.Height / 2 + symbol.Height)+1, size.Width, size.Height);
                //        Font font1 = new Font(font.FontFamily, font.Size, FontStyle.Italic);
                //      //Font myFont = null;
                //      //string AppPath = Environment.CurrentDirectory;
                //      //PrivateFontCollection font2 = new PrivateFontCollection();
                //      //  try
                //      //  {
                //      //      font2.AddFontFile(AppPath + @"\font\regular.ttf");//字体的路径及名字 
                //      //      myFont = new Font("GD_Oswald-Regular", 9f);
                //      //  }
                //      //  catch
                //      //  {
                //      //  }\
                //        System.Drawing.Text.PrivateFontCollection colFont = new System.Drawing.Text.PrivateFontCollection();
                //        colFont.AddFontFile(Environment.CurrentDirectory + @"\font\regular.ttf");
                //        Font myFont = new System.Drawing.Font(colFont.Families[0], font.Size, FontStyle.Regular, GraphicsUnit.Point);
                //        Font myFont1 = new System.Drawing.Font(colFont.Families[0], font.Size, FontStyle.Regular, GraphicsUnit.Point);
                //        //阴影
                //        //g.DrawString(text, myFont, new SolidBrush(Color.White), rec1);
                //        g.DrawString(text, myFont, new SolidBrush(color), rec);
                //    }

                //}
                if (point.Text != "")
                {
                    string text = point.Text;
                    if (point.Text.Length > 7)
                    {
                        text = point.Text.Substring(0, point.Text.Length / 2) + "\n" + point.Text.Substring(point.Text.Length / 2, point.Text.Length - (point.Text.Length / 2));
                    }
                    Font font = point.TextFont;
                    Color color = point.TextColor;
                    if (style != null)
                    {
                        if (style.TextFont != null)
                        {
                            font = style.TextFont;
                        }
                        if (style.TextColor != Color.Empty)
                        {
                            color = style.TextColor;
                        }
                    }
                    EasyMap.Geometries.Point minpoint = new EasyMap.Geometries.Point(point.X, point.Y);
                    SizeF size = g.MeasureString(text, font);
                    PointF p = minpoint.TransformToImage(map);
                    RectangleF rec = new RectangleF(p.X - size.Width / 2, p.Y - size.Height / 2 + symbol.Height, size.Width, size.Height);

                    if (point.Select)
                    {
                        g.DrawString(text, font, new SolidBrush(point.SelectColor), rec);
                    }
                    else
                    {
                        g.DrawString(text, font, new SolidBrush(color), rec);
                    }

                }
            }
        }
        public static Font Setfont()
        {
            Font myFont = null;
            string AppPath = Environment.CurrentDirectory;
            try
            {
                PrivateFontCollection font = new  PrivateFontCollection();
                font.AddFontFile(AppPath + @"\font\meiryo.ttc");//字体的路径及名字 
                myFont = new Font("微软雅黑体", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(234)));
            }
            catch
            {
            }
            return myFont;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawMultiPoint(Graphics g, MultiPoint points, Image symbol, float symbolscale, PointF offset,
                                          float rotation, Map map, VectorStyle style, RenderType renderType)
        {
            for (int i = 0; i < points.Points.Count; i++)
                DrawPoint(g, points.Points[i], symbol, symbolscale, offset, rotation, map, style, renderType);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawMultiPoint(IPointSymbolizer symbolizer, Graphics g, MultiPoint points, Map map, VectorStyle style, RenderType renderType)
        {
            symbolizer.Render(map, points, g);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DrawMultiPoint(Graphics g, MultiPoint points, Brush brush, float size, PointF offset, Map map, VectorStyle style, RenderType renderType)
        {
            for (int i = 0; i < points.Points.Count; i++)
            {
                points.Points[i].Visible = points.Visible;
                DrawPoint(g, points.Points[i], brush, size, offset, map, style, renderType);
            }
        }

        #region Nested type: ClipState

        private enum ClipState
        {
            Within,
            Outside,
            Intersecting
        } ;

        #endregion

        public static Point GetGravity(Geometry geom,string layerType)
        {
            List<Point> p = new List<Point>();
            double area = 0;
            Point center = new Point();
            center.X = 0;
            center.Y = 0;
            int n = p.Count;
            if (geom is Polygon)
            {
                p.AddRange((geom as Polygon).ExteriorRing.Vertices);
            }
            else if (geom is MultiPolygon)
            {
                MultiPolygon m = geom as MultiPolygon;
                foreach (Polygon pol in m.Polygons)
                {
                    p.AddRange(pol.ExteriorRing.Vertices);
                }
            }
            else
            {
                return null;
            }
            n = p.Count;
            if (layerType != "AreaInformation")
            {
                for (int i = 0; i < p.Count - 1; i++)
                {
                    area += (p[i].X * p[i + 1].Y - p[i + 1].X * p[i].Y) / 2;
                    //center.X += p[i].X;
                    //center.Y += p[i].Y;
                    center.X += (p[i].X * p[i + 1].Y - p[i + 1].X * p[i].Y) * (p[i].X + p[i + 1].X);
                    center.Y += (p[i].X * p[i + 1].Y - p[i + 1].X * p[i].Y) * (p[i].Y + p[i + 1].Y);
                }
                area += (p[n - 1].X * p[0].Y - p[0].X * p[n - 1].Y) / 2;
                center.X += (p[n - 1].X * p[0].Y - p[0].X * p[n - 1].Y) * (p[n - 1].X + p[0].X);
                center.Y += (p[n - 1].X * p[0].Y - p[0].X * p[n - 1].Y) * (p[n - 1].Y + p[0].Y);

                center.X /= 6 * area;
                center.Y /= 6 * area;
            }
            else
            {
                //宗地图层
                center.X = center.X / p.Count;
                center.Y = center.Y / p.Count;
                double maxX = 0;
                double maxY = 0;
                double minX = 0;
                double minY = 0;

                List<Point> pntlist = new List<Point>();
                for (int i = 0; i < p.Count; i++)
                {
                    Point pointF = new Point();
                    pointF.X = p[i].X;
                    pointF.Y = p[i].Y;
                    pntlist.Add(pointF);
                    if (i == 0)
                    {
                        maxX = p[i].X;
                        maxY = p[i].Y;
                        minX = p[i].X;
                        minY = p[i].Y;
                    }
                    else
                    {
                        if (p[i].X < minX)
                        {
                            minX = p[i].X;
                        }
                        else if (p[i].X > maxX)
                        {
                            maxX = p[i].X;
                        }
                        if (p[i].Y < minY)
                        {
                            minY = p[i].Y;
                        }
                        else if (p[i].Y > maxY)
                        {
                            maxY = p[i].Y;
                        }
                    }
                }
                center.X = (maxX - minX) / 2 + minX;
                center.Y = (maxY - minY) / 2 + minY;
                //PointF pointCenter = new PointF();
                //pointCenter.X = center.X;
                //pointCenter.Y = convert.tofloat(center.Y);
                bool isIn = PointInFeaces(center, pntlist);
                if (!isIn)
                {
                    center = inPoint(center, pntlist, maxX, minX, maxY, minY);
                }
                if (center == null)
                {
                    center.X = (maxX - minX) / 2 + minX;
                    center.Y = (maxY - minY) / 2 + minY;
                }
            }
            //double a = geom.ID;
            return center;
        }
        //找在图形内的点
        public static Point inPoint(Point center, List<Point> pntlist, double maxX, double minX, double maxY, double minY)
        {
            bool isIn = false;
            Point upNew = new Point();//往上移
            upNew.X = center.X;
            upNew.Y = center.Y;
            for (int i = 0; i < 10; i++)
            {
                upNew.X = center.X;
                upNew.Y = (maxY - center.Y) / 10 + upNew.Y;
                isIn = PointInFeaces(upNew, pntlist);
                if (isIn)
                {
                    return upNew;
                }
            }
            Point rightNew = new Point();//往右移
            rightNew.X = center.X;
            rightNew.Y = center.Y;
            for (int i = 0; i < 10; i++)
            {
                rightNew.X = (maxX - center.X) / 10 + rightNew.X;
                rightNew.Y = center.Y;
                isIn = PointInFeaces(rightNew, pntlist);
                if (isIn)
                {
                    return upNew;
                }
            }
            Point leftNew = new Point();//往左移
            leftNew.X = center.X;
            leftNew.Y = center.Y;
            for (int i = 0; i < 10; i++)
            {
                leftNew.X = leftNew.X -(center.X - minX) / 10;
                leftNew.Y = center.Y;
                isIn = PointInFeaces(leftNew, pntlist); 
                if (isIn)
                {
                    return upNew;
                }
            }
            Point downNew = new Point();//往下移
            downNew.X = center.X;
            downNew.Y = center.Y;
            for (int i = 0; i < 10; i++)
            {
                downNew.X = downNew.X - (center.X - minX) / 10;
                downNew.Y = center.Y;
                isIn = PointInFeaces(downNew, pntlist); 
                if (isIn)
                {
                    return upNew;
                }
            }
            int a = 1;
            ////不在图形内
            //Point upNew = new Point();//往上移
            //upNew.X = center.X;
            //upNew.Y = (maxY - center.Y) / 2 + center.Y;
            //bool isIn = PointInFeaces(upNew, pntlist);
            //if (!isIn)
            //{
            //    //不在图形内
            //    Point rightNew = new Point();//往右移
            //    rightNew.X = (maxX - center.X) / 2 + center.X;
            //    rightNew.Y = center.Y;
            //    isIn = PointInFeaces(rightNew, pntlist);
            //    if (!isIn)
            //    {
            //        //不在图形内
            //        Point leftNew = new Point();//往左移
            //        leftNew.X = (center.X - minX) / 2 + minX;
            //        leftNew.Y = center.Y;
            //        isIn = PointInFeaces(leftNew, pntlist);
            //        if (!isIn)
            //        {
            //            //不在图形内
            //            Point downNew = new Point();//往下移
            //            downNew.X = (center.X - minX) / 2 + minX;
            //            downNew.Y = center.Y;
            //            isIn = PointInFeaces(downNew, pntlist);
            //            if (!isIn)
            //            {
            //                isIn = isInPolygon(ref upNew, pntlist, "up", maxX, minX, maxY, minY);
            //                if (!isIn)
            //                {
            //                    isIn = isInPolygon(ref rightNew, pntlist, "right", maxX, minX, maxY, minY);
            //                    if (!isIn)
            //                    {
            //                        isIn = isInPolygon(ref leftNew, pntlist, "left", maxX, minX, maxY, minY);
            //                        if (!isIn)
            //                        {
            //                            isIn = isInPolygon(ref downNew, pntlist, "down", maxX, minX, maxY, minY);
            //                            if (!isIn)
            //                            { 
                                        
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            center.X = leftNew.X;
            //            center.Y = leftNew.Y;
            //        }
            //    }
            //    else
            //    {
            //        center.X = rightNew.X;
            //        center.Y = rightNew.Y;
            //    }
            //}
            //else
            //{
            //    center.X = upNew.X;
            //    center.Y = upNew.Y;
            //}
            return center;
        }
        public static bool isInPolygon(ref Point center, List<Point> pntlist, string location, double maxX, double minX, double maxY, double minY)
        {
            bool isIn = false;
            if (location == "up")
            {
                Point upNew = new Point();//往上移
                upNew.X = center.X;
                upNew.Y = (maxY - center.Y) / 2 + center.Y;
                isIn = PointInFeaces(upNew, pntlist);
                if (isIn)
                {
                    return isIn; 
                }
            }

            if (location == "right")
            {
                Point rightNew = new Point();//往右移
                rightNew.X = (maxX - center.X) / 2 + center.X;
                rightNew.Y = center.Y;
                isIn = PointInFeaces(rightNew, pntlist);
                if (isIn)
                {
                    return isIn;
                }
            }

            if (location == "left")
            {
                Point leftNew = new Point();//往左移
                leftNew.X = (center.X - minX) / 2 + minX;
                leftNew.Y = center.Y;
                isIn = PointInFeaces(leftNew, pntlist);
                if (isIn)
                {
                    return isIn;
                }
            }

            if (location == "down")
            {
                Point downNew = new Point();//往下移
                downNew.X = (center.X - minX) / 2 + minX;
                downNew.Y = center.Y;
                isIn = PointInFeaces(downNew, pntlist);
                if (isIn)
                {
                    return isIn;
                }
            }
            return isIn;
        }
        //判断点是否在多边形内
       //public static bool InPolygon(Polygon polygon, Point point)
       // {
       //     int n = 0;
       //     int count = 0;
       //     //GisSharpBlog.NetTopologySuite.Geometries.LineSegment line;
       //     LineString line;
       //     line.StartPoint.X = point.X;
       //     line.EndPoint.Y = point.Y;
       //     //line.EndPoint.X = -INFINITY;
       //     for (int i = 0; i < n; i++)
       //     {
       //         //得到多边形的一条边
       //         GisSharpBlog.NetTopologySuite.Geometries.LineSegment side;
       //         side.pt1 = polygon[i];
       //         side.pt2 = polygon[(i + 1) % n];
       //         if (IsOnline(point, side))
       //         {
       //             return1;
       //         }
       //         //如果side平行x轴则不作考虑
       //         if (fabs(side.pt1.y - side.pt2.y) < ESP)
       //         {
       //             continue;
       //         }
       //         if (IsOnline(side.pt1, line))
       //         {
       //             if (side.pt1.y > side.pt2.y) count++;
       //         }
       //         else if (IsOnline(side.pt2, line))
       //         {
       //             if (side.pt2.y > side.pt1.y) count++;
       //         }
       //         else if (Intersect(line, side))
       //         {
       //             count++;
       //         }
       //     }
       //     if (count % 2 == 1)
       //     {
       //         return 0;
       //     }
       //     else
       //     {
       //         return 2;
       //     }
       // }
       public static bool PointInFeaces(Point pnt, List<Point> pntlist)
       {
           if (pntlist == null)
           {
               return false;
           }
           int j = 0, cnt = 0;
           for (int i = 0; i < pntlist.Count; i++)
           {
               j = (i == pntlist.Count - 1) ? 0 : j + 1;
               if ((pntlist[i].Y != pntlist[j].Y) && (((pnt.Y >= pntlist[i].Y) && (pnt.Y < pntlist[j].Y)) || ((pnt.Y >= pntlist[j].Y) && (pnt.Y < pntlist[i].Y))) && (pnt.X < (pntlist[j].X - pntlist[i].X) * (pnt.Y - pntlist[i].Y) / (pntlist[j].Y - pntlist[i].Y) + pntlist[i].X)) cnt++;
           }
           return (cnt % 2 > 0) ? true : false;
       }

    }
}