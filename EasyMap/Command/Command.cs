using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;
using EasyMap.Properties;
using System.Drawing;

namespace EasyMap
{
    public static class Command
    {
        public static bool _ConnectServer = true;
        private static bool _Logout = false;
        private static string _ServerIP;
        private static int _Port;
        private static bool _IsExit = false;
        private static TcpClient client;
        private static BinaryReader br;
        private static BinaryWriter bw;
        public static List<string> CommandList = new List<string>();
        private static bool _IsConnect = false;

        public static bool IsConnect
        {
            get { return Command._IsConnect; }
            set { Command._IsConnect = value; }
        }
        public static bool MapMade = false;
        public static Bitmap MadeMap = null;

        public static bool IsExit
        {
            get { return _IsExit; }
            set { _IsExit = value; }
        }

        public static int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        public static string ServerIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }

        public static bool ConnectServer()
        {
            if (!_ConnectServer)
            {
                return false;
            }
            if (_IsConnect)
            {
                return true;
            }
            while (true)
            {
                SetServerIPAndPort();
                CommandList = new List<string>();
                try
                {
                    Connect();
                    break;
                }
                catch (Exception ex)
                {
                    DealError(ex);
                    ServerSettingForm form = new ServerSettingForm();
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return false;
                    }
                    _Logout = true;
                }
            }
            return true;
        }

        private static void Connect()
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse(ServerIP), Port);
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.IsBackground = true;
            threadReceive.Start();
            _Logout = false;
        }

        /// <summary>
        /// 根据当前程序目录的文本文件‘ServerIPAndPort.txt’内容来设定IP和端口
        /// 格式：127.0.0.1:8885
        /// </summary>
        private static void SetServerIPAndPort()
        {
            ServerIP = Common.IniReadValue(CommandType.SERVER_SETTING_FILENAME,"ServerConnect","IP"); //设定IP
            Int32.TryParse(Common.IniReadValue(CommandType.SERVER_SETTING_FILENAME, "ServerConnect", "Port"), out _Port); //设定端口
        }

        /// <summary>
        /// 处理服务器信息
        /// </summary>
        private static void ReceiveData()
        {
            string receiveString = null;
            while (IsExit == false)
            {
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                    if (receiveString == CommandType.OptionType.DRAW_PHOTO_IMAGE.ToString())
                    {
                        string scount = br.ReadString();
                        int count = 0;
                        Int32.TryParse(scount, out count);
                        if (count > 0)
                        {
                            byte[] buf = br.ReadBytes(count);
                            //MadeMap = (Bitmap)Common.DeserializeObject(buf);
                            MemoryStream ms = new MemoryStream(buf);
                            MadeMap = new Bitmap(ms);
                            ms.Close();
                            ms.Dispose();
                            ms = null;
                        }
                        else
                        {
                            string data = br.ReadString();
                            string[] list = data.Split(',');
                            int width = 0;
                            int height = 0;
                            Int32.TryParse(list[0], out width);
                            Int32.TryParse(list[1], out height);
                            MadeMap = new Bitmap(width,height);
                            //Graphics g = Graphics.FromImage(MadeMap);
                            //g.Clear(Color.Yellow);
                            //g.Dispose();
                        }
                        MapMade = true;
                        continue;
                    }
                    AddCommand(receiveString);
                }
                catch(Exception ex)
                {
                    _Logout = true;
                    //MessageBox.Show(ex.StackTrace);
                    break;
                }
            }
        }


        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="message"></param>
        private static void SendMessage(string message)
        {
            if (!_ConnectServer)
            {
                return;
            }
            if (!_IsConnect)
            {
                _IsConnect = ConnectServer();
                if (!_IsConnect)
                {
                    return;
                }
            }
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch (Exception ex)
            {
                _Logout = true;
                DealError(ex);
            }
        }


        private static void SendMessage(byte[] message)
        {
            if (!_ConnectServer)
            {
                return;
            }
            if (!_IsConnect)
            {
                _IsConnect = ConnectServer();
                if (!_IsConnect)
                {
                    return;
                }
            }
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch (Exception ex)
            {
                _Logout = true;
                DealError(ex);
            }
        }


        private static void DealError(Exception ex)
        {
            MessageBox.Show(Resources.ServerConnectionFaild + ex.Message);
        }

        public static void SendLoginCommand()
        {
            _IsConnect = ConnectServer();
        }

        public static void SendLogoutCommand()
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.LOGOUT.ToString());
            SendMessage(msg.ToString());
        }

        public static void SendGetDemo()
        {
            SendMessage(CommandType.OptionType.GET_DEMO_FLAG.ToString());
        }

        /// <summary>
        /// 发送添加图层消息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        public static void SendAddLayerCommand(decimal mapid, decimal layerid)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.ADD_LAYER.ToString());
            msg.Append("," + mapid.ToString());
            msg.Append("," + layerid.ToString());
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 发送删除图层消息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        public static void SendDeleteLayerCommand(decimal mapid, decimal layerid)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.DELETE_LAYER.ToString());
            msg.Append("," + mapid.ToString());
            msg.Append("," + layerid.ToString());
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 发送变更图层名称消息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="layername"></param>
        public static void SendRenameLayerCommand(decimal mapid, decimal layerid, string layername)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.CHANGE_LAYER_NAME.ToString());
            msg.Append("," + mapid.ToString());
            msg.Append("," + layerid.ToString());
            msg.Append("," + layername);
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 发送添加元素消息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        public static void SendAddObjectCommand(decimal mapid, decimal layerid, decimal objectid)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.ADD_OBJECT.ToString());
            msg.Append("," + mapid.ToString());
            msg.Append("," + layerid.ToString());
            msg.Append("," + objectid.ToString());
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 发送删除元素消息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        public static void SendDeleteObjectCommand(decimal mapid, decimal layerid, decimal objectid)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.DELETE_OBJECT.ToString());
            msg.Append("," + mapid.ToString());
            msg.Append("," + layerid.ToString());
            msg.Append("," + objectid.ToString());
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 发送变更元素名消息
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        /// <param name="objectname"></param>
        public static void SendRenameObjectCommand(decimal mapid, decimal layerid, decimal objectid, string objectname)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.CHANGE_OBJECT_NAME.ToString());
            msg.Append("," + mapid.ToString());
            msg.Append("," + layerid.ToString());
            msg.Append("," + objectid.ToString());
            msg.Append("," + objectname);
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 更改地图名称
        /// </summary>
        /// <param name="mapname"></param>
        public static void SendRenameMapCommand(decimal mapid, string mapname)
        {
            StringBuilder msg = new StringBuilder();
            msg.Append(CommandType.OptionType.CHANGE_MAP_NAME.ToString());
            msg.Append("," + mapid.ToString()+","+mapname);
            SendMessage(msg.ToString());
        }

        /// <summary>
        /// 向命令堆栈追加
        /// </summary>
        /// <param name="command"></param>
        private static void AddCommand(string command)
        {
            //命名解析
            string[] list = command.Split(',');
            if (list.Length <= 0)
            {
                return;
            }
            lock(CommandList)
            {
                CommandList.Add(command);
            }
        }


        public static void SendDrawPhotoData(GDAL_Data data)
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("DisplayBbox.Left=" + data.DisplayBbox.Min.X + "\n");
            buf.Append("DisplayBbox.Top=" + data.DisplayBbox.Min.Y + "\n");
            buf.Append("DisplayBbox.Bottom=" + data.DisplayBbox.Max.Y + "\n");
            buf.Append("DisplayBbox.Right=" + data.DisplayBbox.Max.X + "\n");
            buf.Append("DisplaySize.Width=" + data.DisplaySize.Width + "\n");
            buf.Append("DisplaySize.Height=" + data.DisplaySize.Height + "\n");
            buf.Append("PixelWidth=" + data.PixelWidth + "\n");
            buf.Append("PixelHeight=" + data.PixelHeight + "\n");
            buf.Append("MapMinX=" + data.MapMinX + "\n");
            buf.Append("MapMaxY=" + data.MapMaxY + "\n");
            for (int i = 0; i < data.FileList.Count; i++)
            {
                buf.Append("BitmapBR=" + data.BitmapBR[i].X + "," + data.BitmapBR[i].Y + "\n");
                buf.Append("BitmapTL=" + data.BitmapTL[i].X + "," + data.BitmapTL[i].Y + "\n");
                buf.Append("FileList=" + data.FileList[i].ToString() + "\n");
                buf.Append("OverViewLevel=" + data.OverViewLevel[i].ToString() + "\n");
                buf.Append("OverViewZoom=" + data.OverViewScale[i].ToString() + "\n");
            }
            //byte[] buf=Common.SerializeObject(data);
            //MessageBox.Show(buf.Length.ToString());
            SendMessage(CommandType.OptionType.DRAW_PHOTO_DATA.ToString());
            //SendMessage(buf.Length.ToString());
            SendMessage(buf.ToString());
        }


        public static Bitmap GetDrawPhotoImage()
        {
            return null;
        }
    }
}
