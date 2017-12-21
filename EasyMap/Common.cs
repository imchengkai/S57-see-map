using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EasyMap.Data.Providers;
using EasyMap.Geometries;
using GeoPoint = EasyMap.Geometries.Point;
using EasyMap.Layers;
using System.Drawing;
using EasyMap.Properties;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;
using System.Xml;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using EasyMap.Controls;

namespace EasyMap
{
    class Common
    {

        [DllImport("kernel32")]

        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]

        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("CPUID_Util.dll")]

        private static extern void GetCPUID(StringBuilder reVal);

        /// <summary>
        /// 序列化一个元素，返回二进制数据
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
            {
                return null;
            }
            try
            {
                MemoryStream memory = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memory, pObj);
                memory.Position = 0;
                byte[] read = new byte[memory.Length];
                memory.Read(read, 0, read.Length);
                memory.Close();
                memory.Dispose();
                memory = null;
                return read;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
            return null;
        }

        /// <summary>
        /// 按照序列化后的二进制数据，生成元素
        /// </summary>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public static object DeserializeObject(byte[] pBytes)
        {
            try
            {
                object newOjb = null;
                if (pBytes == null)
                {
                    return newOjb;
                }
                System.IO.MemoryStream memory = new System.IO.MemoryStream(pBytes);
                memory.Position = 0;
                BinaryFormatter formatter = new BinaryFormatter();
                newOjb = formatter.Deserialize(memory);
                memory.Close();
                memory.Dispose();
                memory = null;
                return newOjb;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
            return null;
        }

        /// <summary>
        /// 保存图层中的元素
        /// </summary>
        /// <param name="layer"></param>
        public static void SaveObject(VectorLayer layer)
        {
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            foreach (Geometry geometry in provider.Geometries)
            {
                string type = string.Empty;
                if (geometry is GeoPoint)
                {
                    type = "GeoPoint";
                }
                else if (geometry is Point3D)
                {
                    type = "Point3D";
                }
                else if (geometry is Polygon)
                {
                    type = "Polygon";
                }
                else if (geometry is LineString)
                {
                    type = "LineString";
                }
                else if (geometry is MultiLineString)
                {
                    type = "MultiLineString";
                }
                else if (geometry is MultiPoint)
                {
                    type = "MultiPoint";
                }
                else if (geometry is MultiPolygon)
                {
                    type = "MultiPolygon";
                }
                else
                {
                    continue;
                }
                byte[] data = SerializeObject(geometry);
            }
        }

        /// <summary>
        /// 将序列化的数据转化为元素，并添加到图层中
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="layer"></param>
        public static void Restore(string type, byte[] data, VectorLayer layer)
        {
            object obj = DeserializeObject(data);
            if (obj == null)
            {
                return;
            }
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            if (type == "GeoPoint")
            {
                GeoPoint point = (GeoPoint)obj;
                provider.Geometries.Add(point);
            }
            else if (type == "Point3D")
            {
                Point3D point3D = (Point3D)obj;
                provider.Geometries.Add(point3D);
            }
            else if (type == "Polygon")
            {
                Polygon polygon = (Polygon)obj;
                provider.Geometries.Add(polygon);
            }
            else if (type == "LineString")
            {
                LineString lineString = (LineString)obj;
                provider.Geometries.Add(lineString);
            }
            else if (type == "MultiLineString")
            {
                MultiLineString multiLineString = (MultiLineString)obj;
                provider.Geometries.Add(multiLineString);
            }
            else if (type == "MultiPoint")
            {
                MultiPoint multiPoint = (MultiPoint)obj;
                provider.Geometries.Add(multiPoint);
            }
            else if (type == "MultiPolygon")
            {
                MultiPolygon multiPolygon = (MultiPolygon)obj;
                provider.Geometries.Add(multiPolygon);
            }
        }

        /// <summary>
        /// 保存元素到数据库
        /// </summary>
        /// <param name="geometry"></param>
        public static void AddObjectIntoDB(decimal mapid, decimal layerid, Geometry geometry)
        {
            decimal id = MapDBClass.GetObjectId(mapid, layerid);
            if (geometry is Polygon)
            {
                ((Polygon)geometry).ID = id;
            }
            else if (geometry is EasyMap.Geometries.Point)
            {
                ((EasyMap.Geometries.Point)geometry).ID = id;
            }
            else if (geometry is MultiPolygon)
            {
                ((MultiPolygon)geometry).ID = id;
            }
            else if (geometry is MultiPoint)
            {
                ((MultiPoint)geometry).ID = id;
            }
            else if (geometry is MultiLineString)
            {
                ((MultiLineString)geometry).ID = id;
            }
            else if (geometry is LineString)
            {
                ((LineString)geometry).ID = id;
            }
            else if (geometry is LinearRing)
            {
                ((LinearRing)geometry).ID = id;
            }
            else
            {
                return;
            }
            MapDBClass.InsertObject(mapid, layerid, geometry);
        }

        /// <summary>
        /// 设置图层显示样式
        /// </summary>
        /// <param name="layer"></param>
        public static void SetLayerDefaultStyle(VectorLayer layer)
        {
            layer.Style.Fill = new SolidBrush(Color.FromArgb(0, 255, 255, 255));
            layer.Style.EnableOutline = true;
            layer.Style.Line = Pens.Black;
            layer.Style.Symbol = Resources.Chat;
            layer.Style.PointSymbol = Resources.DiagramChangeToTargetClassic;
            layer.Style.PointSelectSymbol = Resources.Home;
            layer.Style.PointPriceSymbol = Resources.Flag;
            layer.Style.PointPriceSelectSymbol = Resources.DATABASE;
            layer.Style.TextColor = Color.Black;
            layer.Style.TextFont = new Font("", 12);
        }

        /// <summary>
        /// 设置图层显示样式
        /// </summary>
        /// <param name="layer"></param>
        public static void SetLayerStyle(VectorLayer layer, SolidBrush fillBrush, Pen outLinePen, Color textColor, Font textFont, bool enableOutLine, int hatchStyle, Pen linePen, int Penstyle)
        {
            layer.Style.Outline = outLinePen;
            layer.Style.Fill = fillBrush;
            layer.Style.EnableOutline = enableOutLine;
            layer.Style.Line = linePen;
            layer.Style.Outline.DashStyle = layer.Style.Line.DashStyle;
            layer.Style.Symbol = Resources.Chat;
            layer.Style.PointSymbol = Resources.DiagramChangeToTargetClassic;
            layer.Style.PointSelectSymbol = Resources.DiagramChangeToTargetClassicSelect;
            layer.Style.PointPriceSymbol = Resources.Flag;
            layer.Style.PointPriceSelectSymbol = Resources.FlagSelect;
            layer.Style.HatchStyle = hatchStyle;
            layer.Style.TextColor = textColor;
            layer.Style.TextFont = textFont;
            layer.Style.Penstyle = Penstyle;
        }

        /// <summary>
        /// 根据图层显示样式，绘制节点显示的图片
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap DrawIcoByLayerStyle(VectorLayer layer, int width, int height)
        {
            Bitmap map = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(map);
            g.Clear(Color.White);
            Rectangle rect = new Rectangle(0, 0, map.Width, map.Height - (int)layer.Style.Line.Width - 4);
            g.FillRectangle(layer.Style.Fill, rect);
            if (layer.Style.EnableOutline)
            {
                g.DrawRectangle(layer.Style.Outline, new Rectangle(1, 1, rect.Width - 3, rect.Height - 3));
            }
            int y = rect.Bottom + 3;
            g.DrawLine(layer.Style.Line, 0, y, map.Width, y);
            g.Dispose();
            return map;
        }

        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowError(Exception ex)
        {
            ShowErrorForm form = new ShowErrorForm(ex);
            form.ShowDialog();
        }

        /// <summary>
        /// 遍历所有节点设置其图片
        /// </summary>
        /// <param name="node"></param>
        public static void SetNodeImage(TreeNode node)
        {
            if (node.Tag is VectorLayer)
            {
                VectorLayer layer = node.Tag as VectorLayer;
                node.ImageIndex = -1;
                node.SelectedImageIndex = -1;
                node.ImageKey = layer.ID.ToString();
                node.SelectedImageKey = node.ImageKey;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                SetNodeImage(subnode);
            }
        }

        public static String GetCpuID()
        {

            StringBuilder temp = new StringBuilder(255);
            GetCPUID(temp);
            return temp.ToString();
            //try
            //{
            //    //StringBuilder buf = new StringBuilder(255);
            //    //int ret = ReadPhysicalDriveInNT(0, buf, 255);
            //    //if (ret < 0)
            //    //{
            //    //    return buf.ToString().Trim();
            //    //}
            //    //return "";
            //    ManagementClass mc = new ManagementClass("Win32_Processor");
            //    ManagementObjectCollection moc = mc.GetInstances();

            //    String strCpuID = null;
            //    foreach (ManagementObject mo in moc)
            //    {
            //        strCpuID = mo.Properties["ProcessorId"].Value.ToString();
            //        break;
            //    }
            //    return strCpuID;
            //}
            //catch
            //{
            //    return "";
            //}
        }

        public static bool CheckNo()
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string cpuid = GetCpuID();
            byte[] data = System.Text.Encoding.Default.GetBytes(cpuid);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            string no = GetKey();

            return str.ToLower() == no.ToLower();

        }

        public static string GetKey()
        {
            string no = Common.GetXmlData("settings.ini", "Key");
            return no;
        }

        public static string ConvertKey()
        {
            string key=GetKey();
            int len = key.Length / 2;
            string data = "";
            for (int i = len-1; i >= 0; i--)
            {
                data += key.Substring(i, 1);
            }
            for(int i=len;i<key.Length;i++)
            {
                data+=key.Substring(i,1);
            }
            return data;
        }

        public static void SetKey(String key)
        {
            Common.SaveXml("settings.ini", "Key", "Value",key);
        }

        public static string GetXmlData(string filename, string att)
        {
            return IniReadValue(filename, "Setting", att);
        }

        public static void SaveXml(string filename, string nodepath, string att, string value)
        {
            IniWriteValue(filename, "Setting", att, value);
        }

        public static string GetSystemPath()
        {
            string path = "";
            path = Environment.GetFolderPath(Environment.SpecialFolder.System);
            return path;
        }

        public static int Check()
        {
            return 1;
            bool flag = CheckNo();
            if (flag)
            {
                return 1;
            }
            DateTime temp = new DateTime(2012, 9, 30);
            
            string path = GetSystemPath();
            string filename = path + "\\fsat.cnf";
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                string sdate = sr.ReadLine();

                sr.Close();
                int year = 0;
                int month = 0;
                int day = 0;
                year = Int32.Parse(sdate.Substring(0, 4));
                month = Int32.Parse(sdate.Substring(5, 2));
                day = Int32.Parse(sdate.Substring(8));
                DateTime date = new DateTime(year, month, day);
                
                if (DateTime.Now < date)
                {
                    return 2;
                }
                if (temp < date)
                {
                    return 2;
                }
            }
            SaveDate();
            return 0;
        }

        public static void SaveDate()
        {
            string path = GetSystemPath();
            string filename = path + "\\fsat.cnf";
            if (File.Exists(filename))
            {
                StreamReader sr = new StreamReader(filename);
                string sdate = sr.ReadLine();
                sr.Close();
                int year = 0;
                int month = 0;
                int day = 0;
                year = Int32.Parse(sdate.Substring(0, 4));
                month = Int32.Parse(sdate.Substring(5, 2));
                day = Int32.Parse(sdate.Substring(8));
                DateTime date = new DateTime(year, month, day);
                if (DateTime.Now < date)
                {
                    return;
                }
            }
            StreamWriter sw = new StreamWriter(filename);
            sw.WriteLine(DateTime.Now.Year + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Day.ToString().PadLeft(2, '0'));
            sw.Close();
        }

        /// <summary>
        /// 将粘帖板中的文本数据复制到表格中
        /// </summary>
        /// <param name="view"></param>
        /// <param name="allowInsert"></param>
        public static void PasteToGrid(DataGridView view,bool allowInsert)
        {
            string txt = Clipboard.GetText().Trim();
            if (txt == "")
            {
                return;
            }
            string[] lines = txt.Split('\n');
            int row = 0;
            int startcol=0;
            if (view.CurrentCell != null)
            {
                row=view.CurrentCell.RowIndex;
                startcol = view.CurrentCell.ColumnIndex;
            }
            
            if (row + lines.Length > view.RowCount&&allowInsert)
            {
                view.RowCount = row + lines.Length;
            }
            foreach (string line in lines)
            {
                if (row >= view.RowCount)
                {
                    if (!allowInsert)
                    {
                        break;
                    }
                    int row1=view.Rows.Add();
                }
                string[] keys = line.Split('\t');
                int index = 0;
                for (int col = startcol; col < view.ColumnCount-1; col++)
                {
                    if (!view.Columns[col].Visible)
                    {
                        continue;
                    }
                    if (index >= keys.Length)
                    {
                        break;
                    }
                    view.Rows[row].Cells[col].Value = keys[index];
                    index++;
                }
                row++;
            }
        }

        /// <summary>
        /// 保存INI文件内容
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void IniWriteValue(string filename, string Section, string Key, string Value)
        {
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            path = path.Trim();
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            WritePrivateProfileString(Section, Key, Value, path + filename);

        }

        /// <summary>
        /// 读取INI文件内容
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string IniReadValue(string filename, string Section, string Key)
        {
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            path = path.Trim();
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path + filename);
            return temp.ToString();

        }

        /// <summary>
        /// 保存图片到指定格式
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="image"></param>
        public static void SaveImage(string filename, Image image)
        {
            string file = filename.ToLower();
            string name = Path.GetExtension(file);
            if (name == ".jpg")
            {
                image.Save(filename, ImageFormat.Jpeg);
            }
            else if (name == ".png")
            {
                image.Save(filename, ImageFormat.Png);
            }
            else if (name == ".gif")
            {
                image.Save(filename, ImageFormat.Gif);
            }
            else if (name == ".bmp")
            {
                image.Save(filename, ImageFormat.Bmp);
            }
        }

        /// <summary>
        /// 将表格中选择的单元格导出到CSV文件中
        /// </summary>
        /// <param name="view"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool ExportDataGridViewToCsv(MyDataGridView view, string filename)
        {
            int startcol = view.ColumnCount;
            int endcol = -1;
            int startrow = view.RowCount;
            int endrow = -1;
            foreach (DataGridViewCell cell in view.SelectedCells)
            {
                if (cell.ColumnIndex < startcol)
                    startcol = cell.ColumnIndex;
                if (cell.ColumnIndex > endcol)
                    endcol = cell.ColumnIndex;
                if (cell.RowIndex < startrow)
                    startrow = cell.RowIndex;
                if (cell.RowIndex > endrow)
                    endrow = cell.RowIndex;
            }
            if (File.Exists(filename))
            {
                File.SetAttributes(filename, FileAttributes.Normal);
                File.Delete(filename);
            }
            StreamWriter sw = new StreamWriter(filename);
            StringBuilder txt = new StringBuilder();
            for (int col = startcol; col <= endcol; col++)
            {
                txt.Append(view.Columns[col].HeaderText + ",");
            }
            txt = txt.Remove(txt.Length - 1, 1);
            sw.WriteLine(txt.ToString());
            for (int row = startrow; row <= endrow; row++)
            {
                txt = new StringBuilder();
                for (int col = startcol; col <= endcol; col++)
                {
                    if (view.Rows[row].Cells[col].Selected)
                    {
                        txt.Append(view.GetGridValue(row, col) + ",");
                    }
                    else
                    {
                        txt.Append(",");
                    }
                }
                txt = txt.Remove(txt.Length - 1, 1);
                sw.WriteLine(txt.ToString());
            }
            sw.Close();
            return true;
        }
        public static string ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static string ConvertDateToString(DateTime date)
        {
            DateTime d=date;
            if (date == null)
            {
                d = DateTime.Now;
            }
            string sdate = d.Year + "/" + d.Month.ToString().PadLeft(2, '0') + "/" + d.Day.ToString().PadLeft(2, '0');
            return sdate;
        }
    }
}
