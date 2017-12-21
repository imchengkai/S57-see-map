
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using GeoPoint = EasyMap.Geometries.Point;
using EasyMap.Geometries;
using EasyMap.Layers;
using EasyMap.Forms;
using EasyMap.Data.Providers;

using EasyMap.Properties;
using System.Runtime.Serialization.Formatters.Binary;
using EasyMap.UI.Forms;
using System.Diagnostics;
using System.Net.Sockets;
using OSGeo.GDAL;
using EasyMap.Extensions.Data;
using PhotoSettings;
using EasyMapServer;
using System.Collections;
using EasyMap.Controls;
using DragControl;
using Report;
using System.Data.SqlClient;
using EsayMap;
using Crom.Controls.Docking;
using EasyMap.Styles;
using Tax;
using MapLib.Utilities;
//using ESRI.ArcGIS.Geodatabase;
//using ESRI.ArcGIS.DataSourcesFile;
using System.Data.OleDb;
using ESRI.ArcGIS;
using ESRI.ArcGIS.ADF;
//using ESRI.ArcGIS.esriSystem;

namespace EasyMap
{
    public partial class MainForm : MyForm
    {
        #region - �������� -

        private const string MAINFORMGUID = "a6402b80-2ebd-4fd3-8930-024a6201d001";
        private const string LAYERGUID = "fe4d6143-1934-4df3-9fca-be8ddfe19007";
        private const string POLYGONPROPERTYFORMGUID = "096b52a7-5f4b-44ee-ab77-9830ec717002";
        private const string SHOWPICTUREFORMGUID = "3d8466c1-e406-4e47-b744-6915afe6e003";
        private const string DBFORMGUID = "1a957c12-df87-4a63-b8a4-ed485a203004";
        private const string TAXFORMGUID = "1da6e328-d158-47de-a4ea-14172c287006";
        private const string LAYERGUID2 = "0a3f4468-080b-404e-b012-997b93ed2005";
        public const string TAX_LAYER_NAME = "˰�񼶱�ͼ��";
        public const string ZONG_DI_LAYER_NAME = "�����ڵ�ͼ��";
        public const string SHUIWU_LAYER_NAME = "˰���ڵ�ͼ��";
        public const string JIE_DAO_LAYER_NAME = "�ֵ�ͼ��";
        public const string QU_JIE_LAYER_NAME = "����ͼ��";
        public const string JIE_FANG_LAYER_NAME = "�ַ�ͼ��";
        public const string BOAT_LAYER_NAME = "����ע��";
        public const string RESCUE_LAYER_NAME = "��Ԯ����ע��";
        public const string RESCUE_BOAT_LAYER_NAME = "��Ԯ��������ע��";
        public const string RESCUE_WURENJI_LAYER_NAME = "��Ԯ�������˻�ע��";
        public const string PROBLEM_LAYER_NAME = "���ѵ�ע��";
        Dictionary<string, ILayerFactory> _layerFactoryCatalog = new Dictionary<string, ILayerFactory>();
        private bool _triggerEvent = true;
        //��������
        private MeasureForm measureForm = null;
        private DockableFormInfo mapInfo = null;

        private DockableFormInfo mapInfo1 = null;
        //���Դ���
        private DockableFormInfo propertyFormInfo = null;
        //��ʾͼƬ����
        private DockableFormInfo pictureFormInfo = null;
        //��ʾ��Ͻ����
        private DockableFormInfo taxFormInfo = null;
        //����༭����
        private InputGeometryForm inputGeometryForm = null;
        //���ݲ�ѯ����
        private DockableFormInfo dbFormInfo = null;
        //������Ϣ
        private BoatSettings boatForm = null;
        //�Ѿȶ���Ϣ
        private RescueSettings rescueBoatForm = null;
        //���˻���Ԯ��Ϣ
        private WurenjiSettings wurenjiSettings = null;
        //�ɳ��ѾȶӴ���
        private SendRescueBoat sendRescueBoat = null;
        //�������Ѵ���
        private ProblemBoatSetting problemBoatSetting = null;
        private MapImage MainMapImage = null;
        private PictureBox picFlash = null;
        private MyToolTipControl MapToolTip = null;
        private MyTree LayerView = null;
        private MapEditImage MyMapEditImage = null;
        private CommandExecute exec = null;
        private bool _NeedDeleteOldTifInfo = true;
        private int _MaxZoom = 1;
        private double _Seed = 1;
        private List<double> _ZoomList = new List<double>();
        private EasyMap.Geometries.Point _LastWorldPos;
        private bool _SelectObjectConfirm = false;
        private Hashtable _GeomPriceTable = new Hashtable();
        private List<PriceColorData> _PriceTable = new List<PriceColorData>();
        private Geometry _LastSelectedGeometry = null;
        private ILayer _EditLayer = null;
        private DockableFormInfo _LayerFormTempInfo = null;
        private DockableFormInfo _LayerFormInfo = null;
        private DockableFormInfo _ProjectFormInfo = null;
        private bool _IsDemo = false;
        private int _ReportCount = 0;

        private Hashtable ColorTable = new Hashtable();
        private Hashtable FontColorTable = new Hashtable();
        private Hashtable TranspraentTable = new Hashtable();
        private Color UndefineColor = Color.Red;
        //��¼ȫ��ͼ�ı���
        private string zoomAll = null;
        //��¼��һ��ѡ��ĵؿ�
        private List<object> lastRet = new List<object>();
        //Ȩ��
        public string userQuanxian = ""; 

        //�ֵ���������------------���ֵ���Ȩ�ޣ�ֻ�����ڽֵ���Χ�Ŵ���С
        public static double maxZoom = 0;
        public static double maxX = 0;
        public static double minX = 0;
        public static double maxY = 0;
        public static double minY = 0;
        public static double downX = 0;
        public static double downY = 0;
        public static int upDown = 0;//0:�������£�1����������

        public bool IsDemo
        {
            get { return _IsDemo; }
            set { _IsDemo = value; SqlHelper.IsDemo = false; }
        }
        #endregion

        public MainForm()
        {
            login login = new login();
            login.StartPosition = FormStartPosition.CenterParent;
            if (login.ShowDialog() != DialogResult.OK)
            {
                this.Close();
                return;
            }
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
            
            }
            Initial();
            Command.SendGetDemo();
            _NeedDeleteOldTifInfo = true;
            MapDBClass.Initial();
            //ע��Shapeͼ����
            registerLayerFactories();
            MainMapImage = MainMapImage1;
            picFlash = picFlash1;
            MapToolTip = myToolTipControl1;
            LayerView = LayerView1;
            MyMapEditImage = MyMapEditImage1;
            LayerView1.MySelectedNode = LayerView1.Nodes[0];
            LayerView2.MySelectedNode = LayerView2.Nodes[0];
            //Process[] allproc = Process.GetProcesses();
            //foreach (Process proc in allproc)
            //{
            //    if (proc.MainWindowTitle == "EasyMapMain")
            //    {
            //        proc.Kill();
            //        break;
            //    }
            //}
            mapControl1.Parent = MainMapImage1;
            waiting1.Parent = MainMapImage1;
            Command.SendLoginCommand();
            if (!Command.IsConnect)
            {
                Close();
                return;
            }
            //axShockwaveFlash1.VisibleChanged += axShockwaveFlash1_VisibleChanged;
            exec = new CommandExecute();
            timer3.Enabled = true;
            TifData.Initial();
            //treeView1.Nodes.Add(TifData._PhotoRoot);
            new MoveControl(label1);
            label1.Parent = MainMapImage;
            int count = 0;
            Int32.TryParse(Common.IniReadValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem", "Count"), out count);
            for (int i = 0; i < count; i++)
            {
                FindAreaTextBox.Items.Add(Common.IniReadValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem", "Item" + i));
            }
            _ReportCount = ReportToolStripMenuItem.DropDownItems.Count;
            LoadCustomReport();
            ZoomListForm form = new ZoomListForm();
            ZoomtoolStripTextBox.Items.Clear();
            List<string> listitems = form.LoadZoom();
            string[] items = new string[listitems.Count];
            listitems.CopyTo(items);
            ZoomtoolStripTextBox.Items.AddRange(items);

            timer6.Interval = 6000;//����ˢ�´���ʱ��
            DataTable table = MapDBClass.SelectRange();
            if (table.Rows.Count>0)
            {
                if (!string.IsNullOrEmpty(table.Rows[0]["range1"].ToString()))
                {
                    timer6.Interval = int.Parse(table.Rows[0]["range1"].ToString());//����ˢ�´���ʱ��
                }
            }
            //����Ȩ��
            //setQuanxian();
            //loginPic pic = new loginPic();
            //pic.BorderWidth = 0;
            //pic.ShowDialog();
        }
        private List<string> quanxianList = new List<string>(); 
        /// <summary>
        /// Ȩ������
        /// </summary>
        private void setQuanxian() 
        {
            user loginUser = new user();
            string userName = loginUser.userName;
            string password = loginUser.password;
            string sql1 = SqlHelper.GetSql("SelectByUserName");
                sql1 = sql1.Replace("@name", userName);
            DataTable table1 = SqlHelper.Select(sql1, null);
            this.userQuanxian = table1.Rows[0]["Ȩ��"].ToString();
            //����ȫͼ��������
            if (!string.IsNullOrEmpty(zoomAll))
            {
                string[] zoom = zoomAll.Split(':');
                if (zoom.Length == 1)
                {
                    maxZoom = double.Parse(zoom[0]);
                }
                else
                {
                    maxZoom = double.Parse(zoom[1]);
                }
            }
            if (table1.Rows[0]["Ȩ��"].ToString() == "��������Ա")
            {
                info.Visible = false;
                quanxian.Visible = true;
                //����ԱToolStripMenuItem.Visible = true;
                ���ĵؿ�������ע��ToolStripMenuItem.Visible = true;
                //���˰����ϢToolStripMenuItem.Visible = true;
                Ӱ��ͼ�㼶����ToolStripMenuItem.Visible = true;
            }
            //else if (table1.Rows[0]["Ȩ��"].ToString() == "�����û�����Ա")
            //{
            //    quanxian.Visible = true;
            //    //�����û�Ȩ������ToolStripMenuItem.Visible = true;
            //}
            else if (table1.Rows[0]["Ȩ��"].ToString() == "����Ա")
            {
                info.Visible = false;
                quanxian.Visible = true;
            }
            else if (table1.Rows[0]["Ȩ��"].ToString().Substring(table1.Rows[0]["Ȩ��"].ToString().Length - 2, 2) == "�ֵ�")
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Clear();
                param.Add(new SqlParameter("jiedaoName", table1.Rows[0]["Ȩ��"].ToString()));

                DataTable tableBidui = SqlHelper.Select(SqlHelper.GetSql("SelectCheckedDataByJiedao"), param);
                if (tableBidui.Rows.Count > 0)
                {
                    info.Visible = true;
                }
                //����ԱToolStripMenuItem.Visible = false;
                //˰���û�Ȩ������ToolStripMenuItem.Visible = false;
                decimal objectid = 0;
                decimal mapId = 0;
                decimal layerId = 0;

                //List<SqlParameter> param = new List<SqlParameter>();
                //��ѯ�ýֵ���objectid 
                param.Clear();
                param.Add(new SqlParameter("name", table1.Rows[0]["Ȩ��"].ToString()));
                DataTable tableObject = SqlHelper.Select(SqlHelper.GetSql("SelectObject"), param);
                objectid = decimal.Parse(tableObject.Rows[0]["ObjectId"].ToString());//�ֵ�id
                layerId = decimal.Parse(tableObject.Rows[0]["LayerId"].ToString());//�ֵ�id
                mapId = decimal.Parse(tableObject.Rows[0]["MapId"].ToString());//�ֵ�id
                //���л�·�ֵ�ͼ
                string tablename = string.Format("t_{0}_{1}", mapId, layerId);
                string GetGeomtrySql = SqlHelper.GetSql("GetGeomtry").Replace("@table", tablename);
                param.Clear();
                param.Add(new SqlParameter("mapid", mapId));
                param.Add(new SqlParameter("layerid", layerId));
                param.Add(new SqlParameter("objectid", objectid));
                DataTable GetGeomtryTable = SqlHelper.Select(GetGeomtrySql, param);
                if (GetGeomtryTable.Rows.Count <= 0) return;
                byte[] data = (byte[])GetGeomtryTable.Rows[0]["ObjectData"];
                Geometry geometry = (Geometry)Common.DeserializeObject(data);
                BoundingBox box = geometry.GetBoundingBox();
                taxForm_DoubleClickObject(box, layerId, objectid);
                maxX = box.Right;
                maxY = box.Top;
                minX = box.Left;
                minY = box.Bottom;
                //����ֵ���������
                if (!string.IsNullOrEmpty(ZoomtoolStripTextBox.Text))
                {
                    string[] zoom = ZoomtoolStripTextBox.Text.Split(':');
                    if (zoom.Length == 1)
                    {
                        maxZoom = double.Parse(zoom[0]);
                    }
                    else
                    {
                        maxZoom = double.Parse(zoom[1]);
                    }
                }
            }

            string sql = SqlHelper.GetSql("SelectUserQuanxianByName");
            sql = sql.Replace("@name", userName);
            DataTable table = SqlHelper.Select(sql, null);
            List<string> list = new List<string>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i]["�ڵ�����"].ToString());
            }
            quanxianList = list;
            //û��ʹ��Ȩ��ѡ��
            if (!list.Contains("ѡ��Ԫ��"))
            {
                chooseToolStripMenuItem.Enabled = false;
                //SelecttoolStripButton1.Enabled = false;
                ѡ��ToolStripMenuItem.Enabled = false;
                SelecttoolStripButton1.Enabled = false;
            }
            if (!list.Contains("ѡ�񹤾�"))
            {
                btnSelect.Enabled = false;
            }
            if (!list.Contains("���ο�ѡ��"))
            {
                btnRectangel.Enabled = false;
            }
            if (!list.Contains("����Բ��ѡ��"))
            {
                btnFreeCircle.Enabled = false;
            }
            if (!list.Contains("����뾶Բ��ѡ��"))
            {
                btnCircle.Enabled = false;
            }
            if (!list.Contains("��������ѡ��"))
            {
                btnFree.Enabled = false;
            }
            if (!list.Contains("�ƶ���ͼ"))
            {
                PanToolStripButton.Enabled = false;
                �ƶ�ToolStripMenuItem.Enabled = false;
                MoveToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("��С"))
            {
                //ZoomInModeToolStripButton.Enabled = false;
                ZoomInModeToolStripButton.Enabled = false;
                ����ToolStripMenuItem.Enabled = false;
                ZoomToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�Ŵ�"))
            {
                ZoomOutModeToolStripButton.Enabled = false;
                �Ŵ�ToolStripMenuItem.Enabled = false;
                ZoomOutToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�ֲ�����"))
            {
                ZoomAreatoolStripButton.Enabled = false;
                ZoomAreatoolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("ȫ����ʾ"))
            {
                ZoomToExtentsToolStripButton.Enabled = false;
                ȫͼ��ʾToolStripMenuItem.Enabled = false;
                ZoomToExtentsToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�����н�ͼ"))
            {
                btnLoadShiQu.Enabled = false;
            }
            if (!list.Contains("˰��չ��"))
            {
                btnTax.Enabled = false;
            }
            if (!list.Contains("��˰�ʼ�����ɫ"))
            {
                btnShowPriceColor.Enabled = false;
            }
            if (!list.Contains("�༭ͼ��"))
            {
                btnEditLayerList.Enabled = false;

                btnEditLayer.Enabled = false;
            }
            if (!list.Contains("���ݲ�ѯ"))
            {
                btnDataSearch.Enabled = false;
                SearchToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�����ͼ"))
            {
                SaveMapToolStripMenuItem.Enabled = false;
                SaveToolStripButton.Enabled = false;
            }
            if (!list.Contains("�رյ�ͼ"))
            {
                CloseMapToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("��ӡ����"))
            {
                printsetuptoolStripMenuItem14.Enabled = false;
            }
            if (!list.Contains("��ӡԤ��"))
            {
                PrintPreviewToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("��ӡ"))
            {
                PrintToolStripMenuItem.Enabled = false;
                PrintToolStripButton.Enabled = false;
            }
            if (!list.Contains("������ͼ"))
            {
                pic_out.Enabled = false;
            }
            if (!list.Contains("�½�ͼ��"))
            {
                InsertLayerToolStripMenuItem.Enabled = false;
                AddNewRandomGeometryLayer.Enabled = false;
            }
            if (!list.Contains("�½�����ͼ��"))
            {
                ����ͼ��ToolStripMenuItem1.Enabled = false;
                toolStripMenuItem6.Enabled = false;
            }
            if (!list.Contains("�½�˰�񼶱�ͼ��"))
            {
                toolStripMenuItem8.Enabled = false;
            }
            if (!list.Contains("�½��ڵ���Ϣͼ��"))
            {
                �ڵ���Ϣͼ��ToolStripMenuItem1.Enabled = false;
                toolStripMenuItem10.Enabled = false;
            }
            if (!list.Contains("�½�����ͼ��"))
            {
                ����ͼ��ToolStripMenuItem1.Enabled = false;
                toolStripMenuItem13.Enabled = false;
            }
            if (!list.Contains("��ͼ��"))
            {
                OpenToolStripMenuItem.Enabled = false;
                AddLayerToolStripButton.Enabled = false;
            }
            if (!list.Contains("��˰�񼶱�ͼ��"))
            {
                ����ͼ��ToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�򿪻���ͼ��"))
            {
                ����ͼ��ToolStripMenuItem2.Enabled = false;
                ����ͼ��ToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("���ڵ���Ϣͼ��"))
            {
                �ڵ���Ϣͼ��ToolStripMenuItem2.Enabled = false;
                �ڵ���Ϣͼ��ToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("������ͼ��"))
            {
                ����ͼ��ToolStripMenuItem2.Enabled = false;
                ����ͼ��ToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�½�����"))
            {
                InsertAreaToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("ɾ��ͼ��"))
            {
                NewMapToolStripMenuItem.Enabled = false;
                RemoveLayerToolStripButton.Enabled = false;
                DeleteLayerToolStripMenuItem1.Enabled = false;
                RemoveLayerToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("ɾ������"))
            {
                DeleteAreaToolStripMenuItem1.Enabled = false;
            }
            if (!list.Contains("ͼ����ͼ"))
            {
                btnLayerView.Enabled = false;
            }
            if (!list.Contains("������ͼ"))
            {
                btnDataView.Enabled = false;
            }
            if (!list.Contains("������ͼ"))
            {
                btnPropertyView.Enabled = false;
                PropertytoolStripButton2.Enabled = false;
            }
            if (!list.Contains("ͼƬ��ͼ"))
            {
                btnPictureView.Enabled = false;
            }
            if (!list.Contains("��Ͻ��ͼ"))
            {
                btnProjectView.Enabled = false;
            }
            if (!list.Contains("��������ѡҪ��"))
            {
                btnZoomToSelectObjects.Enabled = false;
            }
            if (!list.Contains("�����ѡҪ��"))
            {
                btnClearSelectObjects.Enabled = false;
            }
            if (!list.Contains("�����ͼ�������"))
            {
                btnClearAreaPriceFill.Enabled = false;
            }
            if (!list.Contains("��������"))
            {
                CopyXYToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("����"))
            {
                MeasureToolStripMenuItem.Enabled = false;
                MesuretoolStripSplitButton1.Enabled = false;
            }
            if (!list.Contains("�������"))
            {
                mesureLengthToolStripMenuItem1.Enabled = false;
                ��������ToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�������"))
            {
                mesureAreaToolStripMenuItem1.Enabled = false;
                �������ToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("���궨λ"))
            {
                XYtoolStripMenuItem.Enabled = false;
                InputDatatoolStripButton1.Enabled = false;
                toolStripDropDownButton1.Enabled = false;
            }
            if (!list.Contains("������ʾ��ʽ"))
            {
                btnSetObjectStyle.Enabled = false;
            }
            if (!list.Contains("ɾ����ʽ"))
            {
                btnDeleteObjectStyle.Enabled = false;
            }
            
            if (!list.Contains("��Ƭ"))
            {
                PictureToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("����"))
            {
                PropertyToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�Զ��屨��"))
            {
                CustomReportMenuItem2.Enabled = false;
            }
            if (!list.Contains("�ļ�"))
            {
                FileToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�༭"))
            {
                EditToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("��ͼ"))
            {
                ViewToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("����"))
            {
                //ͼ������
                MoveUpToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("����"))
            {
                //ͼ������
                MoveDownToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("���ŵ�ͼ�㷶Χ"))
            {
                //���ŵ�ͼ�㷶Χ
                btnZoomToLayer.Enabled = false;
            }
            if (!list.Contains("ɾ��Ԫ��"))
            {
                //ɾ��Ԫ��
                DeleteObjectToolStripMenuItem.Enabled = false;
                DeletePolygonToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("�ɼ�������"))
            {
                //�ɼ�������
                VisibleToolStripMenuItem.Enabled = false;
            }
            if (!list.Contains("����ͼ��"))
            {
                //����ͼ��
                OutPutShp.Enabled = false;
            }
            if (!list.Contains("�ϲ�������ͼ��"))
            {
                //�ϲ�������ͼ��
                MergeShp.Enabled = false;
            }
        }

        Form mapform = new Form();
        /// <summary>
        /// ��������ʼ��
        /// </summary>
        private void Initial()
        {
            projectControl1.Visible = false;
            LayerView1.Visible = false;
            LayerView2.Visible = false;
            //axShockwaveFlash1.Movie = AppDomain.CurrentDomain.BaseDirectory + "earth.swf";
            mapform.Text = "��ͼ";
            mapform.Bounds = this.ClientRectangle;// new Rectangle(MapPanel1.Left, MapPanel1.Top, MapPanel1.Width, MapPanel1.Height);
            mapform.SizeChanged += mapform_SizeChanged;
            mapform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            mapform.Controls.Add(MapPanel1);
            MapPanel1.Dock = DockStyle.Fill;
            mapform.Controls.Add(MapPanel2);
            MapPanel2.Left = MapPanel1.Left;
            MapPanel2.Top = MapPanel1.Bottom;
            MapPanel2.Width = MapPanel1.Width;
            MapPanel2.Height = 0;
            mapform.Icon = this.Icon;
            mapInfo = dockContainer1.Add(mapform, zAllowedDock.All, new Guid(MAINFORMGUID));
            dockContainer1.DockForm(mapInfo, DockStyle.Fill, zDockMode.Inner);
            //axShockwaveFlash1.BringToFront();
            //����Ȩ��
            //setQuanxian();
        }

        #region - �¼� -

        #region - ͼ�����¼� -

        private void LayerView1_Enter(object sender, EventArgs e)
        {
            //return;
            LayerView = sender as MyTree;
            if (LayerView == LayerView1)
            {
                MainMapImage = MainMapImage1;
                picFlash = picFlash1;
                MapToolTip = myToolTipControl1;
                MyMapEditImage = MyMapEditImage1;
                MapDBClass.IsCompareMap = false;
            }
            else
            {
                MainMapImage = MainMapImage2;
                picFlash = picFlash2;
                MapToolTip = myToolTipControl2;
                MyMapEditImage = MyMapEditImage2;
                MapDBClass.IsCompareMap = true;
            }
            SetToolBarStatus();
        }

        /// <summary>
        /// ���Ľڵ�����ǰ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //����Ǹ��ڵ㣬���ֹ��������
            if (e.Node.Tag == null && e.Node != LayerView.Nodes[0])
            {
                e.CancelEdit = true;
                return;
            }
        }

        /// <summary>
        /// ���Ľڵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }
            //����ͼ������  
            //�ڵ�ͼ��/˰���ڵ�ͼ�����Ʋ����޸�
            //if (e.Node.Text == "�����ڵ�ͼ��" || e.Node.Text == "˰���ڵ�ͼ��")
            //{
            //    MessageBox.Show("�����ڵ�ͼ�㡢˰���ڵ�ͼ�����Ʋ����޸ģ�");
            //    Geometry geometry = e.Node.Tag as Geometry;
            //    geometry.Text = e.Label;
            //    if (MainMapImage.NeedSave && MainMapImage.Map.CurrentLayer.NeedSave)
            //    {
            //        MapDBClass.UpdateObject(MainMapImage.Map.MapId, geometry);
            //        Command.SendRenameObjectCommand(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, geometry.ID, e.Label);
            //    }
            //    MainMapImage.Refresh();
            //}
            //else
            if (e.Node.Text == Resources.PhotoLayer)
            {
                MessageBox.Show("Ӱ��ͼ�����Ʋ����޸ģ�");
                Geometry geometry = e.Node.Tag as Geometry;
                geometry.Text = e.Label;
                if (MainMapImage.NeedSave && MainMapImage.Map.CurrentLayer.NeedSave)
                {
                    MapDBClass.UpdateObject(MainMapImage.Map.MapId, geometry);
                    Command.SendRenameObjectCommand(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, geometry.ID, e.Label);
                }
                MainMapImage.Refresh();
            }
            else if (e.Node == LayerView.Nodes[0])
            {
                if (MainMapImage.NeedSave)
                {
                    MapDBClass.UpdateMapName(MainMapImage.Map.MapId, e.Label);
                    Command.SendRenameMapCommand(MainMapImage.Map.MapId, e.Label);
                }
            }
            else if (e.Node.Tag is VectorLayer)
            {
                ((ILayer)e.Node.Tag).LayerName = e.Label;
                VectorLayer layer = (VectorLayer)e.Node.Tag;
                if (MainMapImage.NeedSave && layer.NeedSave)
                {
                    MapDBClass.UpdateLayer(MainMapImage.Map.MapId, layer);
                    Command.SendRenameLayerCommand(MainMapImage.Map.MapId, layer.ID, e.Label);
                }
                foreach (ToolStripButton btn in btnEditLayerList.DropDownItems)
                {
                    if (btn.Tag == layer)
                    {
                        btn.Text = layer.LayerName;
                        break;
                    }
                }
                if (btnEditLayerList.Tag == layer)
                {
                    btnEditLayerList.Text = layer.LayerName;
                }
            }
            else if (e.Node.Tag is Geometry)
            {
                Geometry geometry = e.Node.Tag as Geometry;
                geometry.Text = e.Label;
                if (MainMapImage.NeedSave && MainMapImage.Map.CurrentLayer.NeedSave)
                {
                    MapDBClass.UpdateObject(MainMapImage.Map.MapId, geometry);
                    Command.SendRenameObjectCommand(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, geometry.ID, e.Label);
                }
                MainMapImage.Refresh();
            }
        }

        /// <summary>
        /// ��ʾ����ͼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!_triggerEvent)
            {
                return;
            }
            _triggerEvent = false;
            CheckNode(e.Node);
            CheckParentNode(e.Node);
            bool yingxiang = true;
            if (e.Node.Parent != null)
            {
                yingxiang = e.Node.Text == Resources.PhotoLayer || e.Node.Parent.Text == Resources.PhotoLayer;
            }
            else
            {
                yingxiang = e.Node.Text == Resources.PhotoLayer;
            }
            if (e.Node.Checked)
            {
                if (btnLoadPicture.Checked)
                {
                    _triggerEvent = true;
                    return;
                }
                if (yingxiang)
                {
                    btnLoadPicture.Checked = true;
                    btnLoadPicture_Click(null,null);
                    //btnLoadPicture.Checked = true;

                    //if (!Command.IsConnect)
                    //{
                    //    Command.SendLoginCommand();
                    //    if (!Command.IsConnect)
                    //    {
                    //        btnLoadPicture.Checked = false;
                    //        MessageBox.Show("�������ӵ���������");
                    //        return;
                    //    }
                    //}
                    ////if (btnLoadShiQu.Checked)
                    ////{
                    ////    btnLoadShiQu.Checked = false;
                    ////    btnLoadShiQu_Click(null, null);
                    ////}
                    //DataTable table = MapDBClass.GetTifInformation(MainMapImage.Map.MapId);
                    //double width = 0;
                    //mapControl1.Visible = false;
                    //projectControl1.Initial(MainMapImage.Map.MapId);
                    //if (table != null && table.Rows.Count > 0)
                    //{
                    //    double minx = (double)table.Rows[0]["MinX"];
                    //    double miny = (double)table.Rows[0]["MinY"];
                    //    double maxx = (double)table.Rows[0]["MaxX"];
                    //    double maxy = (double)table.Rows[0]["MaxY"];
                    //    width = (int)table.Rows[0]["Width"];
                    //    BoundingBox box = new BoundingBox(minx, miny, maxx, maxy);
                    //    GdalRasterLayer layer = new GdalRasterLayer(box);
                    //    MainMapImage.Map.Layers.Add(layer);
                    //    MainMapImage.HaveTif = true;
                    //    MainMapImage.ShowShiQu = false;
                    //    mapControl1.Visible = true;
                    //}
                    //SetToolBarStatus(); ZoomToExtentsToolStripButton_Click(null, null);
                    //if (mapControl1.Visible)
                    //{
                    //    _MaxZoom = (int)(width / Command.MadeMap.Width);
                    //    _Seed = MainMapImage.Map.Zoom / (width / Command.MadeMap.Width);
                    //    if (_MaxZoom > 32)
                    //    {
                    //        MainMapImage.Map.Zoom = 32 * _Seed;
                    //        MainMapImage.Refresh();
                    //        _MaxZoom = 32;
                    //    }
                    //    else
                    //    {
                    //        MainMapImage.Map.Zoom = _MaxZoom * _Seed;
                    //        MainMapImage.Refresh();
                    //    }
                    //    _ZoomList.Clear();
                    //    double temp = _MaxZoom;
                    //    while (temp >= 1)
                    //    {
                    //        _ZoomList.Add(temp * _Seed);
                    //        temp /= 2;
                    //    }
                    //    temp = 1;
                    //    int levelcount = TifData.GetLevelCount();
                    //    for (int i = 0; i < levelcount; i++)
                    //    {
                    //        for (int j = 1; j <= 6; j++)
                    //        {
                    //            _ZoomList.Add(_ZoomList[_ZoomList.Count - 1] / 2);
                    //        }
                    //    }
                    //    mapControl1.LevelCount = _ZoomList.Count - 1;
                    //}
                    //if (MainMapImage.HaveTif)
                    //{
                    //    PutTextColor();
                    //    MainMapImage.RequestFromServer = true;
                    //    MainMapImage.Refresh();
                    //    MainMapImage_MapZoomChanged(null, MainMapImage.Map.Zoom);
                    //}
                    //else
                    //{
                    //    RestoreTextColor();
                    //    MainMapImage.HaveTif = false;
                    //    mapControl1.Visible = false;
                    //    MainMapImage.Refresh();
                    //}
                }
            }
            else if (e.Node.Tag is ILayer && e.Node.Checked)
            {
                MainMapImage.Map.CurrentLayer = (ILayer)e.Node.Tag;
                ClearSelectObject();
                if (MainMapImage.Map.Zoom < MainMapImage.Map.CurrentLayer.MinVisible
                    || MainMapImage.Map.Zoom > MainMapImage.Map.CurrentLayer.MaxVisible)
                {
                    MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible;
                    BoundingBox box = MainMapImage.Map.CurrentLayer.Envelope;
                    EasyMap.Geometries.Point center = new EasyMap.Geometries.Point();
                    center.X = (box.Left + box.Right) / 2;
                    center.Y = (box.Top + box.Bottom) / 2;
                    MainMapImage.Map.Center = center;
                    MainMapImage.RequestFromServer = true;
                    MainMapImage.Refresh();
                    MainMapImage_MapZoomChanged(MainMapImage, MainMapImage.Map.Zoom);
                }
            }
            else if (yingxiang)
            {
                //if (!btnLoadPicture.Checked)
                //{
                //    return;
                //}
                btnLoadPicture.Checked = false;
                RestoreTextColor();
                MainMapImage.HaveTif = false;
                mapControl1.Visible = false;
                MainMapImage.Refresh();
            }
            MainMapImage.Refresh();
            _triggerEvent = true;
        }

        /// <summary>
        /// ͼ��ڵ��϶���ʼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// �ڵ��϶���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// ͼ��ڵ��϶��ͷŴ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_DragDrop(object sender, DragEventArgs e)
        {
            //ȡ���϶��Ľڵ�
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            //ȡ��Ŀ��ڵ�
            System.Drawing.Point pt = ((TreeView)(sender)).PointToClient(new System.Drawing.Point(e.X, e.Y));
            TreeNode targetNode = LayerView.GetNodeAt(pt);
            if (targetNode.Parent != moveNode.Parent)
            {
                return;
            }
            MainMapImage.Map.MoveLayer(moveNode.Tag as ILayer, targetNode.Tag as ILayer);
            TreeNode parent = targetNode.Parent;
            //�����µĽڵ�
            TreeNode node = (TreeNode)moveNode.Clone();
            //�����µĽڵ�
            moveNode.Parent.Nodes.Insert(targetNode.Index, node);
            //ɾ��ԭ�нڵ�
            moveNode.Remove();
            ResetLayerSort(parent);
            MainMapImage.Refresh();
        }

        /// <summary>
        /// ���õ�ǰ�ͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LayerView.MySelectedNode = LayerView.SelectedNode;
            if (LayerView.MySelectedNode == null)
            {
                return;
            }
            if (LayerView.MySelectedNode != LayerView.Nodes[0])
            {
                if (LayerView.MySelectedNode.Tag is VectorLayer)
                {
                    MainMapImage.Map.CurrentLayer = (ILayer)LayerView.MySelectedNode.Tag;
                }
                else if (LayerView.MySelectedNode.Tag is Geometry)
                {
                    MainMapImage.Map.CurrentLayer = (ILayer)LayerView.MySelectedNode.Parent.Tag;
                    //ClearSelectObject();
                    //SelectObject(LayerView.MySelectedNode.Tag);
                    //MainMapImage.Refresh();
                }
            }
            //SetToolBarStatus();
            //SetToolForm();
        }

        /// <summary>
        /// ����ͼ�������ʽ
        /// </summary>
        /// <param name="node"></param>
        private void LayerView_NodeImageClick(TreeNode node)
        {
            if (node.Tag is VectorLayer)
            {
                VectorLayer layer = node.Tag as VectorLayer;
                LayerStyleForm form = new LayerStyleForm();
                form.FillBrush = layer.Style.Fill;
                form.LinePen = layer.Style.Line;
                form.OutLinePen = layer.Style.Outline;
                form.EnableOutline = layer.Style.EnableOutline;
                form.HatchStyle = layer.Style.HatchStyle;
                form.TextFont = layer.Style.TextFont;
                form.TextColor = layer.Style.TextColor;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SetLayerStyle(layer, form.FillBrush, form.OutLinePen, form.TextColor, form.TextFont, form.EnableOutline, form.HatchStyle, form.LinePen, form.Penstyle, node);
                    if (MainMapImage.NeedSave && layer.NeedSave)
                    {
                        MapDBClass.UpdateLayer(MainMapImage.Map.MapId, layer);
                    }
                    MainMapImage.Refresh();
                }
            }
        }

        private void LayerView1_SelectionChange(TreeNode node)
        {
            if (LayerView.MySelectedNode == null || _triggerEvent == false)
            {
                return;
            }
            if (LayerView.MySelectedNode != LayerView.Nodes[0])
            {
                if (LayerView.MySelectedNode.Tag is VectorLayer)
                {
                    //MainMapImage.Map.CurrentLayer = (ILayer)LayerView.MySelectedNode.Tag;
                    ////ClearSelectObject();
                    //if (node.Checked)
                    //{
                    //    //if (MainMapImage.Map.Zoom < MainMapImage.Map.CurrentLayer.MinVisible
                    //    //    || MainMapImage.Map.Zoom > MainMapImage.Map.CurrentLayer.MaxVisible)
                    //    //{
                    //        BoundingBox box = MainMapImage.Map.CurrentLayer.Envelope;
                    //        if (box != null)
                    //        {
                    //            //MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible;
                    //            if (box.Width > 0 && box.Height > 0)
                    //            {
                    //                MainMapImage.Map.ZoomToBox(box);
                    //            }
                    //            EasyMap.Geometries.Point center = new EasyMap.Geometries.Point();
                    //            center.X = (box.Left + box.Right) / 2;
                    //            center.Y = (box.Top + box.Bottom) / 2;
                    //            MainMapImage.Map.Center = center;
                    //            MainMapImage.RequestFromServer = true;
                    //            MainMapImage.Refresh();
                    //        }
                    //    //}
                    //}
                }
                else if (LayerView.MySelectedNode.Tag is Geometry)
                {
                    MainMapImage.Map.CurrentLayer = (ILayer)LayerView.MySelectedNode.Parent.Tag;
                    //ClearSelectObject();
                    //SelectObject(LayerView.MySelectedNode.Tag);
                    //MainMapImage.Refresh();
                }
            }
            SetToolBarStatus();
            SetToolForm();
        }

        #endregion

        #region - ��ͼ�¼� -

        /// <summary>
        /// ��ͼ���ڳߴ���ʱ����ͼ����ˢ�´���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mapform_SizeChanged(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        /// <summary>
        /// �ڵ�ͼˢ�º���
        /// </summary>
        private void MainMapImage_AfterRefresh(object sender)
        {
            waiting1.Visible = false;
            MainToolStrip.Enabled = true;
            MapContextMenu.Enabled = true;
            menuStrip1.Enabled = true;
            LayerContextMenu.Enabled = true;
            LayerView1.Enabled = true;
            LayerView2.Enabled = true;
            MainMapImage1.Enabled = true;
            MainMapImage2.Enabled = true;
            //����ͼ��ˢ��
            MyMapEditImage.Refresh();
            //for (int i = MainMapImage.Controls.Count - 1; i >= 0; i--)
            //{
            //    if (MainMapImage.Controls[i] is MyFlag)
            //    {
            //        MainMapImage.Controls.RemoveAt(i);
            //    }
            //}
            //List<PhotoData> list = TifData.GetFlags();
            //foreach (PhotoData data in list)
            //{
            //    MyFlag flag = new MyFlag();
            //    flag.Photo = data;
            //    double dx = (data.MinX + data.MaxX) / 2;
            //    double dy = (data.MinY + data.MaxY) / 2;
            //    PointF point = MainMapImage.Map.WorldToImage(new EasyMap.Geometries.Point(dx, dy), true);
            //    flag.Left = (int)point.X;
            //    flag.Top = (int)point.Y;
            //    MainMapImage.Controls.Add(flag);
            //    flag.BringToFront();
            //    flag.Parent = MainMapImage;
            //}
            SetMapControlLevel();

        }
        //0->�ޣ�1->��갴ס��2->�ƶ���ָ�,3->�ƶ�
        private static int IsMove = 0;
        private MapImage.Tools tool = new MapImage.Tools();
        /// <summary>
        /// ����ƶ�ʱ����ʾ��ͼ����
        /// </summary>
        /// <param name="WorldPos"></param>
        /// <param name="ImagePos"></param>
        private void MainMapImage_MouseMove(object sender, EasyMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            //���ֵ�Ȩ�ޡ�������Χ�����ƶ�
            if (!string.IsNullOrEmpty(userQuanxian) && userQuanxian.Substring(userQuanxian.Length - 2, 2) == "�ֵ�")
            {
                MapImage.bottomLeftX = MainMapImage.Map.Envelope.BottomLeft.X;//�������¶�x����
                MapImage.bottomLeftY = MainMapImage.Map.Envelope.BottomLeft.Y;
                MapImage.topLeftY = MainMapImage.Map.Envelope.TopLeft.Y;
                MapImage.topRigthX = MainMapImage.Map.Envelope.TopRight.X;
                MapImage.minX = minX;
                MapImage.minY =minY;
                MapImage.maxX =maxX;
                MapImage.maxY =maxY;
                //if (bottomLeftX < minX - 500 || bottomLeftY < minY - 500 || topRigthX > maxX + 500 || topLeftY >maxY+500)
                //{ 
                
                //}
                //MainMapImage.Map.ZoomToBox(box);
                // && (bottomLeftX > minX - 500 && bottomLeftY > minY - 500 && topRigthX < maxX + 500 && topLeftY < maxY + 500)
                if (IsMove == 1 && SelecttoolStripButton1.Checked && ImagePos.Button == MouseButtons.Left && MainMapImage.ActiveTool != MapImage.Tools.DefineArea
                    && (WorldPos.X > minX && WorldPos.Y > minY
                    && WorldPos.X < maxX && WorldPos.Y < maxY))
                {
                    IsMove = 3;
                    MainMapImage1.ActiveTool = MapImage.Tools.Pan;
                    MainMapImage2.ActiveTool = MapImage.Tools.Pan;
                    MainMapImage.ActiveTool = MapImage.Tools.Pan;
                }
                else if (IsMove == 2 && SelecttoolStripButton1.Checked && ImagePos.Button == MouseButtons.None && MainMapImage.ActiveTool != MapImage.Tools.DefineArea)
                {
                    IsMove = 0;
                    MainMapImage1.ActiveTool = MapImage.Tools.Select;
                    MainMapImage2.ActiveTool = MapImage.Tools.Select;
                    MainMapImage.ActiveTool = MapImage.Tools.Select;
                }
            }
            else if (!string.IsNullOrEmpty(userQuanxian) && !string.IsNullOrEmpty(ZuoBiaoLabel.Text))
            {
                string[] a = ZuoBiaoLabel.Text.Split(':');
                string[] b = a[1].Split(' ');
                double x = double.Parse(b[1].Substring(0, b[1].Length - 1));
                if (IsMove == 1 && SelecttoolStripButton1.Checked && ImagePos.Button == MouseButtons.Left && MainMapImage.ActiveTool != MapImage.Tools.DefineArea
                && (WorldPos.X > x + 3 || WorldPos.X < x - 3))
                {
                    IsMove = 3;
                    MainMapImage1.ActiveTool = MapImage.Tools.Pan;
                    MainMapImage2.ActiveTool = MapImage.Tools.Pan;
                    MainMapImage.ActiveTool = MapImage.Tools.Pan;
                }
                else if (IsMove == 2 && SelecttoolStripButton1.Checked && ImagePos.Button == MouseButtons.None && MainMapImage.ActiveTool != MapImage.Tools.DefineArea)
                {
                    IsMove = 0;
                    MainMapImage1.ActiveTool = MapImage.Tools.Select;
                    MainMapImage2.ActiveTool = MapImage.Tools.Select;
                    MainMapImage.ActiveTool = MapImage.Tools.Select;
                }
            }
            else
            {
                if (IsMove == 1 && SelecttoolStripButton1.Checked && ImagePos.Button == MouseButtons.Left && MainMapImage.ActiveTool != MapImage.Tools.DefineArea)
                {
                    IsMove = 3;
                    MainMapImage1.ActiveTool = MapImage.Tools.Pan;
                    MainMapImage2.ActiveTool = MapImage.Tools.Pan;
                    MainMapImage.ActiveTool = MapImage.Tools.Pan;
                }
                else if (IsMove == 2 && SelecttoolStripButton1.Checked && ImagePos.Button == MouseButtons.None && MainMapImage.ActiveTool != MapImage.Tools.DefineArea)
                {
                    IsMove = 0;
                    MainMapImage1.ActiveTool = MapImage.Tools.Select;
                    MainMapImage2.ActiveTool = MapImage.Tools.Select;
                    MainMapImage.ActiveTool = MapImage.Tools.Select;
                }
            }
            if (btnMapToImage.Checked)
            {
                if (label1.Tag != null)
                {
                    System.Drawing.Point p = (System.Drawing.Point)label1.Tag;
                    label1.Width = ImagePos.Location.X - p.X;
                    label1.Height = ImagePos.Location.Y - p.Y;
                }
            }
            MapImage img = sender as MapImage;
            ZuoBiaoLabel.Text = String.Format(Resources.WorldPosFormat, WorldPos.X, WorldPos.Y);
            img.LastWorldPos = WorldPos;
        }

        /// <summary>
        /// ���ò�������
        /// </summary>
        /// <param name="totalLength"></param>
        /// <param name="currentLength"></param>
        private void mapEditImage1_SetLength(double totalLength, double currentLength)
        {
            if (measureForm != null && !measureForm.IsDisposed)
            {
                measureForm.SetLength(totalLength, currentLength);
            }
        }

        /// <summary>
        /// ���ò������
        /// </summary>
        /// <param name="area"></param>
        private void mapEditImage1_SetArea(double area)
        {
            if (measureForm != null && !measureForm.IsDisposed)
            {
                measureForm.SetArea(area);
            }
        }

        /// <summary>
        /// �����ѡ�ж����
        /// </summary>
        /// <param name="WorldPos"></param>
        /// <param name="ImagePos"></param>
        private void MainMapImage_MouseDown(object sender, EasyMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            //downX = WorldPos.X;
            //downY = WorldPos.Y;
            IsMove = 1;
            //if (ImagePos.Button == MouseButtons.Right)
            //{
            //    List<object> ret1 = MainMapImage.Map.PickUpObject(new System.Drawing.Point(ImagePos.X, ImagePos.Y), MainMapImage.Size, Resources.Flag.Size);
            //    //�Ҽ�ѡ��֮ǰѡ�еĵؿ飬�����˵��ı�
            //    bool isLast = true;
            //    if (ret1.Count == lastRet.Count)
            //    {
            //        for (int i = 0; i < ret1.Count; i++)
            //        {
            //            if (!lastRet[i].Equals(ret1[i]))
            //                isLast = false;
            //        }
            //    }
            //    else
            //    {
            //        isLast = false;
            //    }
            //    if (isLast && (MainMapImage.Map.CurrentLayer.LayerName == ZONG_DI_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName == "˰���ڵ�ͼ��" || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(ZONG_DI_LAYER_NAME)==0))
            //    {
            //        if (ImagePos.Button == MouseButtons.Right)
            //        {
            //            //ѡ��һ��ؿ飬����Ҽ�
            //            ѡ��ToolStripMenuItem.Visible = false;
            //            �ƶ�ToolStripMenuItem.Visible = false;
            //            ����ToolStripMenuItem.Visible = false;
            //            �Ŵ�ToolStripMenuItem.Visible = false;
            //            ȫͼ��ʾToolStripMenuItem.Visible = false;
            //            ZoomAreatoolStripMenuItem.Visible = false;
            //            btnClearAreaPriceFill.Visible = false;
            //            MeasureToolStripMenuItem.Visible = false;
            //            //btnSetObjectStyle.Visible = false;
            //            //btnDeleteObjectStyle.Visible = false;
            //            ����֤��ϢToolStripMenuItem.Visible = true;
            //            ˰����ϢToolStripMenuItem.Visible = true;
            //            TreeNode node_zongdi = FindNode("�ڵ���Ϣͼ��");

            //            foreach (TreeNode node in node_zongdi.Nodes)
            //            {
            //                if (node.Text == "�����ڵ�ͼ��" || node.Text.IndexOf(ZONG_DI_LAYER_NAME) == 0)
            //                {
            //                    if (node.Checked == true)
            //                    {
            //                        ����֤��ϢToolStripMenuItem.Visible = true;
            //                    }
            //                }
            //                if (node.Text == "˰���ڵ�ͼ��")
            //                {
            //                    if (node.Checked == false)
            //                    {
            //                        ˰����ϢToolStripMenuItem.Visible = false;
            //                    }
            //                }
            //            }
            //            //if (node_zongdi.Nodes["�����ڵ�ͼ��"].Checked == false)
            //            //{
            //            //    ����֤��ϢToolStripMenuItem.Visible = false;
            //            //}
            //            //if (node_zongdi.Nodes["˰���ڵ�ͼ��"].Checked == false)
            //            //{
            //            //    ˰����ϢToolStripMenuItem.Visible = false;
            //            //}
            //        }
            //    }
            //    else
            //    {
            //        ѡ��ToolStripMenuItem.Visible = true;
            //        �ƶ�ToolStripMenuItem.Visible = true;
            //        ����ToolStripMenuItem.Visible = true;
            //        �Ŵ�ToolStripMenuItem.Visible = true;
            //        ȫͼ��ʾToolStripMenuItem.Visible = true;
            //        ZoomAreatoolStripMenuItem.Visible = true;
            //        btnClearAreaPriceFill.Visible = true;
            //        MeasureToolStripMenuItem.Visible = true;
            //        //btnSetObjectStyle.Visible = true;
            //        //btnDeleteObjectStyle.Visible = true;
            //        ����֤��ϢToolStripMenuItem.Visible = false;
            //        ˰����ϢToolStripMenuItem.Visible = false;
            //    }
            //}
            //if (sender == MainMapImage1)
            //{
            //    LayerView1_Enter(LayerView1, EventArgs.Empty);
            //}
            //else
            //{
            //    LayerView1_Enter(LayerView2, EventArgs.Empty);
            //}
            //MapToolTip.Hide();
            //_LastWorldPos = WorldPos;
            //if (btnMapToImage.Checked)
            //{
            //    label1.Left = ImagePos.Location.X;
            //    label1.Top = ImagePos.Location.Y;
            //    label1.Tag = ImagePos.Location;
            //    return;
            //}
            //if (MainMapImage.PickCoordinate)
            //{
            //    return;
            //}
            //MainMapImage = sender as MapImage;
            //if (MainMapImage == MainMapImage1)
            //{
            //    MapToolTip = myToolTipControl1;
            //    LayerView = LayerView1;
            //    MyMapEditImage = MyMapEditImage1;
            //    MapDBClass.IsCompareMap = false;
            //}
            //else
            //{
            //    MapToolTip = myToolTipControl2;
            //    LayerView = LayerView2;
            //    MyMapEditImage = MyMapEditImage2;
            //    MapDBClass.IsCompareMap = true;
            //}
            //SetToolBarStatus();
            //if (ImagePos.Button != MouseButtons.Left)
            //{
            //    return;
            //}
            //if (MainMapImage.ActiveTool == MapImage.Tools.Select)
            //{
            //    //if (MainMapImage.Map.CurrentLayer == null)
            //    //{
            //    //    return;
            //    //}
            //    //if (MainMapImage.Map.CurrentLayer.LayerName != "�����ڵ�ͼ��")
            //    //{
            //    //    return;
            //    //}
            //    //����ԭ�ж���ε�ѡ��״̬
            //    if (Control.ModifierKeys != Keys.Control)
            //    {
            //        ClearSelectObject();
            //    }
            //    bool find = false;

            //    VectorLayer findlayer = null;
            //    Geometry findobject = null;

            //    List<object> ret = MainMapImage.Map.PickUpObject(new System.Drawing.Point(ImagePos.X, ImagePos.Y), MainMapImage.Size, Resources.Flag.Size);
            //   //����ѡ�еؿ�
            //    lastRet = new List<object>();
            //    for (int i = 0; i < ret.Count; i++)
            //    {
            //        lastRet.Add(ret[i]);
            //    }
            //    find = (bool)ret[0];
            //    if (find)
            //    {
            //        findobject = ret[1] as Geometry;
            //        findlayer = ret[2] as VectorLayer;
            //        //if (!(findlayer.LayerName.IndexOf("�����ڵ�") >= 0 || findlayer.LayerName.IndexOf("˰���ڵ�") >= 0))
            //        //{
            //        //    return;
            //        //}
            //        MainMapImage.SelectObjects.Add(findobject);
            //    }
            //    if (find)
            //    {
            //        if (!_SelectObjectConfirm || (_SelectObjectConfirm && (findobject is Polygon || findobject is MultiPolygon)))
            //        {
            //            //if (_SelectObjectConfirm && (findobject is Polygon || findobject is MultiPolygon) && findobject.Text == "")
            //            //{
            //            //    MessageBox.Show("ѡ�������û���������ƣ����ܱ�ѡ��Ϊ�ڵأ�������ѡ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            //    return;
            //            //}
            //            TreeNode a = FindNodeLayer(LayerView.Nodes[0], findlayer);
            //            ////�����ǰѡ�е�Ԫ���ڵ�ǰ�Ӵ���ʾ��Χ�ڵĻ�����ô��ֹ��ѡ�е�Ԫ�����ڵ�ͼ��ķ�Χ������ʾ���Ӵ���
            //            _triggerEvent = false;
            //            LayerView.SelectedNode = a;
            //            MainMapImage.Map.CurrentLayer = findlayer;
            //            _triggerEvent = true;
            //        }
            //    }
            //    MainMapImage.Refresh();
            //    SetToolForm();
            //    if (find && MainMapImage.SelectObjects.Count == 1 && Control.ModifierKeys != Keys.Control)
            //    {
            //        if (!_SelectObjectConfirm || (_SelectObjectConfirm && (findobject is Polygon || findobject is MultiPolygon)))
            //        {
            //            //if (findobject.Text != "")
            //            //{
            //            MapToolTip.SelectObjectConfirm = _SelectObjectConfirm;
            //            string msg = findobject.Text;
            //            bool flag = true;
            //            int hight = -1;
            //            if (findlayer.LayerName.IndexOf(ZONG_DI_LAYER_NAME) == 0)
            //            {
            //                flag = false;
            //                List<SqlParameter> param = new List<SqlParameter>();
            //                param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
            //                param.Add(new SqlParameter("layerid", findlayer.ID));
            //                param.Add(new SqlParameter("objectid", findobject.ID));
            //                DataTable table = SqlHelper.Select(SqlHelper.GetSql("GetTipText"), param);

            //                if (table.Rows.Count > 0)
            //                {
            //                    //for (int row = 0; row < table.Rows.Count; row++)
            //                    //{
            //                        //hight++;
            //                        msg += "\r\n����֤��ţ�" + table.Rows[0]["bianhao"].ToString() + "\r\n����ʹ���ߣ�" + table.Rows[0]["shiyongquanren"].ToString();
            //                    //}
            //                }
            //                else
            //                {
            //                    msg += "\r\n����֤��ţ�\r\n����ʹ���ߣ�";
            //                }
            //            }
            //            else if (findlayer.LayerName == "˰���ڵ�ͼ��")
            //            {
            //                flag = false;
            //                List<SqlParameter> param = new List<SqlParameter>();
            //                param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId.ToString()));
            //                param.Add(new SqlParameter("layerid", findlayer.ID.ToString()));
            //                param.Add(new SqlParameter("objectid", findobject.ID.ToString()));
            //                //DataTable table = SqlHelper.Select(SqlHelper.GetSql("GetTipTextShuiwu"), param);
            //                DataTable table = SqlHelper.Select(SqlHelper.GetSql("GetTipText"), param);

            //                if (table.Rows.Count > 0)
            //                {                             
            //                    //for (int row = 0; row < table.Rows.Count; row++)
            //                    //{
            //                        //hight++;
            //                        //msg += "\r\n����֤��ţ�" + table.Rows[row]["����֤��"].ToString() + "\r\n����ʹ���ߣ�" + table.Rows[row]["ʹ��Ȩ��"].ToString();
            //                        msg += "\r\n����֤��ţ�" + table.Rows[0]["bianhao"].ToString() + "\r\n����ʹ���ߣ�" + table.Rows[0]["shiyongquanren"].ToString();
            //                    //}
            //                }
            //                else
            //                {
            //                    msg += "\r\n����֤��ţ�\r\n����ʹ���ߣ�";
            //                }
            //            }
            //            MapToolTip.Initial(MainMapImage.Map.MapId, findlayer.ID, findobject, msg, ImagePos.X, ImagePos.Y);
            //            if (flag)
            //            {
            //                MapToolTip.Height = 50;
            //            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
            //            else
            //            {
            //                MapToolTip.Width = 300;
            //            }
            //            //MapToolTip.Top = ImagePos.X - MapToolTip.Height - 10;
            //            MapToolTip.Show();
            //            //if (!MapToolTip.Visible)
            //            //{
            //            //    MessageBox.Show("ѡ�������û���������ƣ����ܱ�ѡ��Ϊ�ڵأ�������ѡ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //            //}
            //            //}
            //        }
            //    }
            //    SetToolBarStatus();
            //}
        }

        /// <summary>
        /// ������ͼ�������ƶ�ʱ��������ʾ��ͼ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapEditImage1_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender == MyMapEditImage1)
            {
                MainMapImage_MouseMove(MainMapImage1, MainMapImage.Map.ImageToWorld(new PointF(e.X, e.Y)), e);
            }
            else if (sender == MyMapEditImage2)
            {
                MainMapImage_MouseMove(MainMapImage2, MainMapImage.Map.ImageToWorld(new PointF(e.X, e.Y)), e);
            }
        }

        /// <summary>
        /// �ڶ���������֮�󣬽�������������ӵ����ݿ���
        /// </summary>
        /// <param name="area"></param>
        private void mapEditImage1_AfterDefineArea(SELECTION_TYPE type, Polygon area, double r)
        {
            //������ȡ������
            if (area == null)
            {
                //�ָ����ĵ�ͼ��������
                MainMapImage.ActiveTool = MainMapImage.LastOption;
                //����Ŵ�ť��ԭ
                ZoomAreatoolStripButton.Checked = false;
                return;
            }
            if (area.ExteriorRing.NumPoints > 0)
            {
                //����������壬����ǰͼ��׷������
                //if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint)
                if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea)
                {
                    area.ID = MapDBClass.GetObjectId(MainMapImage.Map.MapId, _EditLayer.ID);
                    if (MainMapImage.NeedSave && _EditLayer.NeedSave)
                    {
                        MapDBClass.InsertObject(MainMapImage.Map.MapId, _EditLayer.ID, area);
                        Command.SendAddObjectCommand(MainMapImage.Map.MapId, _EditLayer.ID, area.ID);
                        //������ݵ������ڵ�ͼ�����
                        SqlConnection conn = null;
                        SqlTransaction tran = null;
                        try
                        {
                            string insertSql = "insert into t_" + MainMapImage.Map.MapId + "_" + _EditLayer.ID + "(MapId ,LayerId,ObjectId,propertydate,ZJMC,FLDM,UpdateDate,CreateDate) values(@MapId ,@LayerId,@ObjectId,@propertydate,@ZJMC,'2',@UpdateDate,@CreateDate)";
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
                            param.Add(new SqlParameter("LayerId", _EditLayer.ID));
                            param.Add(new SqlParameter("ObjectId", area.ID));
                            param.Add(new SqlParameter("propertydate", DateTime.Now.Date.ToString().Substring(0,10)));
                            param.Add(new SqlParameter("ZJMC", area.Text));
                            param.Add(new SqlParameter("UpdateDate", DateTime.Now.Date.ToString().Substring(0, 10)));
                            param.Add(new SqlParameter("CreateDate", DateTime.Now.Date.ToString().Substring(0, 10)));
                            if (type == SELECTION_TYPE.PROBLEMAREA)
                            {
                                param.Add(new SqlParameter("�Ƿ�����", "��"));
                                insertSql = "insert into t_" + MainMapImage.Map.MapId + "_" + _EditLayer.ID + "(MapId ,LayerId,ObjectId,propertydate,ZJMC,FLDM,UpdateDate,CreateDate,�Ƿ�����,������������ѵ�) values(@MapId ,@LayerId,@ObjectId,@propertydate,@ZJMC,'2',@UpdateDate,@CreateDate,@�Ƿ�����,'2')";
                            }
                            conn = SqlHelper.GetConnection();
                            conn.Open();
                            tran = conn.BeginTransaction();
                            //����ͼ�����ݱ��浽��ѡͼ����
                            SqlHelper.Insert(conn, tran, insertSql, param);
                            tran.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            if (conn != null)
                            {
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                                conn.Close();
                            }
                            Common.ShowError(ex);
                        }
                    }
                    //InsertParentChild(JIE_DAO_LAYER_NAME, ZONG_DI_LAYER_NAME, area as Geometry);
                    //InsertParentChild(TAX_LAYER_NAME, ZONG_DI_LAYER_NAME, area as Geometry);
                    //AddGeometryToTree(area, area.Text);
                }
                if (Control.ModifierKeys != Keys.Control)
                {
                    MainMapImage.SelectObjects.Clear();
                }
                List<object> list = MainMapImage.Map.PickUpObject(MainMapImage.Size, area, type, r, true, true);

                if (list != null && list.Count > 0)
                {
                    for (int i = 1; i < list.Count; i += 2)
                    {
                        string name = (list[i - 1] as ILayer).LayerName;
                        if (name != BOAT_LAYER_NAME && name != RESCUE_LAYER_NAME && name != RESCUE_BOAT_LAYER_NAME && name!= RESCUE_WURENJI_LAYER_NAME)
                        {
                            continue;
                        }
                        Geometry geom = list[i] as Geometry;
                        MainMapImage.SelectObjects.Add(geom);
                        //if (!MainMapImage.SelectLayers.Contains(list[i - 1] as ILayer))
                        //{
                        //    MainMapImage.SelectLayers.Add(list[i - 1] as ILayer);
                        //}
                    }
                    MainMapImage.Refresh();
                    SetToolBarStatus();
                    SetToolForm();
                }
                else
                {
                    MainMapImage.SelectObjects.Add(area);
                    MainMapImage.Refresh();
                    SetToolBarStatus();
                    SetToolForm();
                }
                MainMapImage.Refresh();
                SetToolBarStatus();
                SetToolForm();
                ////���������ѡ����ȡ��ԭ��ѡ��Ԫ�أ�����ѯ�������ڵ�Ԫ�ز�ѡ��
                //else if (MainMapImage.ActiveTool == MapImage.Tools.SelectPoint)
                //{
                //    //ȡ��ԭ��ѡ��Ԫ��
                //    MainMapImage.SelectPoint.Clear();
                //    //��ѯ�������ڵ�Ԫ��
                //    MainMapImage.SelectPoint.AddRange(SelectPoint(area));
                //    MainMapImage.Refresh();
                //    SetToolBarStatus();
                //    SetToolForm();
                //}
            }
            //���������Ŵ�
            else if (MainMapImage.ActiveTool == MapImage.Tools.ZoomArea
                && area.ExteriorRing.NumPoints == 2)
            {
                //ȡ������
                EasyMap.Geometries.Point minpoint = new EasyMap.Geometries.Point(area.ExteriorRing.Vertices[0].X, area.ExteriorRing.Vertices[0].Y);
                EasyMap.Geometries.Point maxpoint = new EasyMap.Geometries.Point(area.ExteriorRing.Vertices[0].X, area.ExteriorRing.Vertices[0].Y);

                foreach (EasyMap.Geometries.Point point in area.ExteriorRing.Vertices)
                {
                    if (point.X < minpoint.X)
                        minpoint.X = point.X;
                    if (point.Y < minpoint.Y)
                        minpoint.Y = point.Y;
                    if (point.X > maxpoint.X)
                        maxpoint.X = point.X;
                    if (point.Y > maxpoint.Y)
                        maxpoint.Y = point.Y;
                }
                //����������Ŵ�����ʾ����
                BoundingBox box = new BoundingBox(minpoint.X, minpoint.Y, maxpoint.X, maxpoint.Y);
                MainMapImage.Map.ZoomToBox(box);
                MainMapImage.RequestFromServer = true;
                MainMapImage.Refresh();
                //�Ƚ��õĵ�ͼ��ʾ����ͱ�������
                MainMapImage_MapZoomChanged(MainMapImage, MainMapImage.Map.Zoom);
                //����Ŵ�ť��ԭ
                ZoomAreatoolStripButton.Checked = false;
            }
            MainMapImage.ActiveTool = MainMapImage.LastOption;
        }
        /// <summary>
        /// �ڶ���������֮�󣬽�������������ӵ����ݿ���
        /// </summary>
        /// <param name="area"></param>
        private void mapEditImage1_AfterDefineArea1(SELECTION_TYPE type, EasyMap.Geometries.Point area, double r)
        {
            //������ȡ������
            if (area == null)
            {
                //�ָ����ĵ�ͼ��������
                MainMapImage.ActiveTool = MainMapImage.LastOption;
                //����Ŵ�ť��ԭ
                ZoomAreatoolStripButton.Checked = false;
                return;
            }
            if (area != null)
            {
                //����������壬����ǰͼ��׷������
                //if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea || MainMapImage.ActiveTool == MapImage.Tools.SelectPoint)
                if (MainMapImage.ActiveTool == MapImage.Tools.DefineArea)
                {
                    //area.ID = MapDBClass.GetObjectId(MainMapImage.Map.MapId, _EditLayer.ID);
                    if (MainMapImage.NeedSave && _EditLayer.NeedSave)
                    {
                        MapDBClass.InsertObject(MainMapImage.Map.MapId, _EditLayer.ID, area);
                        Command.SendAddObjectCommand(MainMapImage.Map.MapId, _EditLayer.ID, area.ID);
                        //������ݵ������ڵ�ͼ�����
                        SqlConnection conn = null;
                        SqlTransaction tran = null;
                        try
                        {
                            string insertSql = "insert into t_" + MainMapImage.Map.MapId + "_" + _EditLayer.ID + "(MapId ,LayerId,ObjectId,propertydate,ZJMC,FLDM,UpdateDate,CreateDate) values(@MapId ,@LayerId,@ObjectId,@propertydate,@ZJMC,@FLDM,@UpdateDate,@CreateDate)";
                            List<SqlParameter> param = new List<SqlParameter>();
                            param.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
                            param.Add(new SqlParameter("LayerId", _EditLayer.ID));
                            param.Add(new SqlParameter("ObjectId", area.ID));
                            param.Add(new SqlParameter("propertydate", DateTime.Now.Date.ToString().Substring(0, 10)));
                            param.Add(new SqlParameter("ZJMC", area.Text));
                            param.Add(new SqlParameter("UpdateDate", DateTime.Now.Date.ToString().Substring(0, 10)));
                            param.Add(new SqlParameter("CreateDate", DateTime.Now.Date.ToString().Substring(0, 10)));
                            if (type == SELECTION_TYPE.CIRCLETEMP)
                            {
                                param.Add(new SqlParameter("FLDM", "2"));
                            }
                            else if (type == SELECTION_TYPE.PROBLEMPOINT)
                            {
                                param.Add(new SqlParameter("FLDM", "4"));
                                param.Add(new SqlParameter("�Ƿ�����", "��"));
                                insertSql = "insert into t_" + MainMapImage.Map.MapId + "_" + _EditLayer.ID + "(MapId ,LayerId,ObjectId,propertydate,ZJMC,FLDM,UpdateDate,CreateDate,�Ƿ�����,������������ѵ�) values(@MapId ,@LayerId,@ObjectId,@propertydate,@ZJMC,@FLDM,@UpdateDate,@CreateDate,@�Ƿ�����,'1')";
                            }
                            else
                            {
                                param.Add(new SqlParameter("FLDM", "0"));
                            }
                            conn = SqlHelper.GetConnection();
                            conn.Open();
                            tran = conn.BeginTransaction();
                            //����ͼ�����ݱ��浽��ѡͼ����
                            SqlHelper.Insert(conn, tran, insertSql, param);
                            tran.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            if (conn != null)
                            {
                                if (tran != null)
                                {
                                    tran.Rollback();
                                }
                                conn.Close();
                            }
                            Common.ShowError(ex);
                        }
                    }
                }
                if (Control.ModifierKeys != Keys.Control)
                {
                    MainMapImage.SelectObjects.Clear();
                }
                List<object> list = MainMapImage.Map.PickUpObject1(MainMapImage.Size, area, type, r, true, true);

                if (list != null && list.Count > 0)
                {
                    for (int i = 1; i < list.Count; i += 2)
                    {
                        string name = (list[i - 1] as ILayer).LayerName;
                        if (name != "�ֵ�ͼ��")
                        {
                            continue;
                        }
                        Geometry geom = list[i] as Geometry;
                        MainMapImage.SelectObjects.Add(geom);
                    }
                    MainMapImage.Refresh();
                    LoadSymbol();
                    SetToolBarStatus();
                    SetToolForm();
                }
                else
                {
                    MainMapImage.SelectObjects.Add(area);
                    LoadSymbol();
                    MainMapImage.Refresh();
                    SetToolBarStatus();
                    SetToolForm();
                }

                //LoadSymbol();
                //MainMapImage.Refresh();
                //SetToolBarStatus();
                //SetToolForm();
            }
            MainMapImage.Refresh();
            MainMapImage.ActiveTool = MainMapImage.LastOption;
        }
        /// <summary>
        /// ��ͼ���ĵ���ʱ������ǵ�ͼ�Ƚϣ�����Ҫ������һ����ͼ�����ĵ㣬ʹ֮���ĵ�һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="center"></param>
        private void MainMapImage_MapCenterChanged(object sender, EasyMap.Geometries.Point center)
        {
            MapImage.bottomLeftX = MainMapImage1.Map.Envelope.BottomLeft.X;//�������¶�x����
            MapImage.bottomLeftY = MainMapImage1.Map.Envelope.BottomLeft.Y;
            MapImage.topLeftY = MainMapImage1.Map.Envelope.TopLeft.Y;
            MapImage.topRigthX = MainMapImage1.Map.Envelope.TopRight.X;
            if (MapImage.bottomLeftX < minX - 500 || MapImage.bottomLeftY < minY - 500 || MapImage.topRigthX > maxX + 500 || MapImage.topLeftY > maxY + 500)
            {
                //������ͼ���ĵ�
                //MyMapEditImage1.Visible = false;
                //MainMapImage1.Map.Center = MainMapImage2.Map.Center;
                //MainMapImage2 = MainMapImage;
                //MainMapImage = MainMapImage1;
                Refresh();
                return;
            }
            if (CompareToolStripButton.Checked)
            {
                //�ж��Ƿ�����ͼ�������ĵ�
                if (sender == MainMapImage1)
                {
                    //������ͼ���ĵ�
                    MyMapEditImage2.Visible = false;
                    MainMapImage2.Map.Center = MainMapImage1.Map.Center;
                    MainMapImage2.Refresh();
                }
                //�ж��Ƿ��Ǹ�ͼ�������ĵ�
                else if (sender == MainMapImage2)
                {
                    //������ͼ���ĵ�
                    MyMapEditImage1.Visible = false;
                    MainMapImage1.Map.Center = MainMapImage2.Map.Center;
                    MainMapImage1.Refresh();
                }
            }
        }

        /// <summary>
        /// ��ͼ�������ʱ������ǵ�ͼ�Ƚϣ�����Ҫ������һ����ͼ�ı�����ʹ֮����һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="zoom"></param>
        private void MainMapImage_MapZoomChanged(object sender, double zoom)
        {
            //return;
            int i = 0;
            //if (!string.IsNullOrEmpty(ZoomtoolStripTextBox.Text))
            //{
            //    string[] a = ZoomtoolStripTextBox.Text.Split(':');
            //    if (a.Length == 1)
            //    {
            //        if (double.Parse(a[0]) > _MaxZoom && double.Parse(a[0]) < zoom)
            //        {
            //            //Refresh();
            //            ZoomtoolStripTextBox.Text = string.Format("1:{0:N}", (int)zoom).Replace(".00", "");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        if (double.Parse(a[1]) > _MaxZoom && double.Parse(a[1]) < zoom)
            //        {
            //            //Refresh();
            //            ZoomtoolStripTextBox.Text = string.Format("1:{0:N}", (int)zoom).Replace(".00", "");
            //            return;
            //        }
            //    }
            //}
            ZoomtoolStripTextBox.Text = string.Format("1:{0:N}", (int)zoom).Replace(".00", "");
            //���ֵ�����ԱȨ��
            if (!string.IsNullOrEmpty(this.userQuanxian))
            {
                if (this.userQuanxian.Substring(this.userQuanxian.Length - 2, 2) == "�ֵ�")
                {
                    //��С������Χ�������Ȩ�޷�Χ
                    if ((int)zoom > maxZoom + Convert.ToDouble(1000))
                    {
                        decimal objectid = 0;
                        decimal mapId = 0;
                        decimal layerId = 0;

                        List<SqlParameter> param = new List<SqlParameter>();
                        //��ѯ�ýֵ���objectid 
                        param.Clear();
                        param.Add(new SqlParameter("name", userQuanxian));
                        DataTable tableObject = SqlHelper.Select(SqlHelper.GetSql("SelectObject"), param);
                        objectid = decimal.Parse(tableObject.Rows[0]["ObjectId"].ToString());//�ֵ�id
                        layerId = decimal.Parse(tableObject.Rows[0]["LayerId"].ToString());//�ֵ�id
                        mapId = decimal.Parse(tableObject.Rows[0]["MapId"].ToString());//�ֵ�id
                        string tablename = string.Format("t_{0}_{1}", mapId, layerId);
                        string GetGeomtrySql = SqlHelper.GetSql("GetGeomtry").Replace("@table", tablename);
                        param.Clear();
                        param.Add(new SqlParameter("mapid", mapId));
                        param.Add(new SqlParameter("layerid", layerId));
                        param.Add(new SqlParameter("objectid", objectid));
                        DataTable GetGeomtryTable = SqlHelper.Select(GetGeomtrySql, param);
                        if (GetGeomtryTable.Rows.Count <= 0) return;
                        byte[] data = (byte[])GetGeomtryTable.Rows[0]["ObjectData"];
                        Geometry geometry = (Geometry)Common.DeserializeObject(data);
                        BoundingBox box = geometry.GetBoundingBox();
                        taxForm_DoubleClickObject(box, layerId, objectid);

                        //string[] a = ZuoBiaoLabel.Text.Split(':');
                        //string[] b = a[1].Split(' ');
                        //double x = double.Parse(b[1].Substring(0, b[1].Length - 1));
                        //double y = double.Parse(b[2]);
                        maxX = box.Right;
                        maxY = box.Top;
                        minX = box.Left;
                        minY = box.Bottom;
                    }
                }
                else
                {
                    //��С������Χ�������Ȩ�޷�Χ
                    if ((int)zoom > maxZoom + Convert.ToDouble(1000))
                    {
                        ZoomToExtentsToolStripButton_Click(null, null);
                    }
                }
            }
            
            if (CompareToolStripButton.Checked)
            {
                if (sender == MainMapImage1)
                {
                    MainMapImage2.Map.Zoom = MainMapImage1.Map.Zoom;
                    MainMapImage2.Map.Center = MainMapImage1.Map.Center;
                    MainMapImage2.Refresh();
                }
                else if (sender == MainMapImage2)
                {
                    MainMapImage1.Map.Zoom = MainMapImage2.Map.Zoom;
                    MainMapImage1.Map.Center = MainMapImage2.Map.Center;
                    MainMapImage1.Refresh();
                }
            }
        }

        private void MainMapImage2_SizeChanged(object sender, EventArgs e)
        {
            //������ͼ���ĵ��Լ�����ͬ��ͼһ��
            if (MainMapImage2.Map.Layers.Count > 0)
            {
                MainMapImage2.Map.Center = MainMapImage1.Map.Center;
                MainMapImage2.Map.Zoom = MainMapImage1.Map.Zoom;
                MainMapImage2.Refresh();
            }
        }

        /// <summary>
        /// ��ͼ��˫��ʱ����ʾ��ǰѡ��Ԫ�ص����Դ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMapImage1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Middle)
            {
                if (TranspraentTable.Count == 0)
                {
                    PutJieDaoTranspraent();
                }
                else
                {
                    RestoreJieDaoTranspraent();
                }
                MainMapImage.Refresh();
            }
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (MainMapImage.ActiveTool == MapImage.Tools.Select ||MainMapImage.ActiveTool == MapImage.Tools.Pan)
            {

                if (GetCurrentLayer() is VectorLayer)
                {
                    //ȡ�õ�ǰ�ͼ��
                    VectorLayer layer = (VectorLayer)GetCurrentLayer();
                    if (layer == null)
                    {
                        return;
                    }
                    if (layer.LayerName == "�ֵ�ͼ��" && MainMapImage.SelectObjects[0].Text != this.userQuanxian && this.userQuanxian.Substring(this.userQuanxian.Length-2,2) == "�ֵ�")
                    {
                        return;
                    }
                    if (MainMapImage.SelectObjects.Count == 1)
                    {
                        taxForm_DoubleClickObject(MainMapImage.SelectObjects[0].GetBoundingBox(), layer.ID, MainMapImage.SelectObjects[0].ID);
                    }
                    MyMapEditImage.Visible = false;
                    //PropertyToolStripMenuItem_Click(null, null);
                }
            }
        }

        /// <summary>
        /// ��ͼˢ��ǰ����������ʾ
        /// </summary>
        /// <param name="sender"></param>
        private void MainMapImage1_BeforeRefresh(object sender)
        {
            if (MainMapImage != null && MainMapImage.RequestFromServer)
            {
                MainToolStrip.Enabled = false;
                MapContextMenu.Enabled = false;
                menuStrip1.Enabled = false;
                LayerContextMenu.Enabled = false;
                LayerView1.Enabled = false;
                LayerView2.Enabled = false;
                MainMapImage1.Enabled = false;
                MainMapImage2.Enabled = false;
                waiting1.SetAutoProcess(true);
                waiting1.Tip = "����ȡ�õ�ͼ���ݣ����Ժ�...";
                waiting1.Visible = btnLoadPicture.Checked;
            }
        }
        // /// <summary>
        ///// ��ͼ�Ŵ���С���� 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="WorldPos"></param>
        ///// <param name="ImagePos"></param>
        //private void MainMapImage_MouseWheel(object sender, MouseEventArgs e)
        //{
        //}
        /// <summary>
        /// ��ͼ���̧����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="WorldPos"></param>
        /// <param name="ImagePos"></param>
        private void MainMapImage_MouseUp(object sender, EasyMap.Geometries.Point WorldPos, MouseEventArgs ImagePos)
        {
            //MapImage.bottomLeftX = MainMapImage.Map.Envelope.BottomLeft.X;//�������¶�x����
            //MapImage.bottomLeftY = MainMapImage.Map.Envelope.BottomLeft.Y;
            //MapImage.topLeftY = MainMapImage.Map.Envelope.TopLeft.Y;
            //MapImage.topRigthX = MainMapImage.Map.Envelope.TopRight.X;
            //MapImage.bottomLeftX = MainMapImage.Map.Envelope.BottomLeft.X;
            //MapImage.bottomLeftX = MainMapImage1.Map.Envelope.BottomLeft.X;
            //MapImage.bottomLeftX = MainMapImage2.Map.Envelope.BottomLeft.X;

            if (IsMove != 3 && MainMapImage.ActiveTool != MapImage.Tools.DefineArea)
            {
                if (ImagePos.Button == MouseButtons.Right)
                {
                    List<object> ret1 = MainMapImage.Map.PickUpObject(new System.Drawing.Point(ImagePos.X, ImagePos.Y), MainMapImage.Size, Resources.Flag.Size);
                    //�Ҽ�ѡ��֮ǰѡ�еĵؿ飬�����˵��ı�
                    bool isLast = true;
                    if (ret1.Count == lastRet.Count)
                    {
                        for (int i = 0; i < ret1.Count; i++)
                        {
                            if (!lastRet[i].Equals(ret1[i]))
                                isLast = false;
                        }
                    }
                    else
                    {
                        isLast = false;
                    }
                    if (isLast && (MainMapImage.Map.CurrentLayer.LayerName == ZONG_DI_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName == "˰���ڵ�ͼ��" || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(ZONG_DI_LAYER_NAME) == 0))
                    {
                        if (ImagePos.Button == MouseButtons.Right)
                        {
                            //ѡ��һ��ؿ飬����Ҽ�
                            ѡ��ToolStripMenuItem.Visible = false;
                            �ƶ�ToolStripMenuItem.Visible = false;
                            ����ToolStripMenuItem.Visible = false;
                            �Ŵ�ToolStripMenuItem.Visible = false;
                            ȫͼ��ʾToolStripMenuItem.Visible = false;
                            ZoomAreatoolStripMenuItem.Visible = false;
                            btnClearAreaPriceFill.Visible = false;
                            MeasureToolStripMenuItem.Visible = false;
                            //btnSetObjectStyle.Visible = false;
                            //btnDeleteObjectStyle.Visible = false;
                            //����֤��ϢToolStripMenuItem.Visible = true;
                            //˰����ϢToolStripMenuItem.Visible = true;
                            //TreeNode node_zongdi = FindNode("�ڵ���Ϣͼ��");

                            //foreach (TreeNode node in node_zongdi.Nodes)
                            //{
                            //    if (node.Text == "�����ڵ�ͼ��" || node.Text.IndexOf(ZONG_DI_LAYER_NAME) == 0)
                            //    {
                            //        if (node.Checked == true)
                            //        {
                            //            ����֤��ϢToolStripMenuItem.Visible = true;
                            //        }
                            //    }
                            //    if (node.Text == "˰���ڵ�ͼ��" || node.Text.IndexOf(ZONG_DI_LAYER_NAME) == 0)
                            //    {
                            //        if (node.Checked == true)
                            //        {
                            //            ˰����ϢToolStripMenuItem.Visible = true;
                            //        }
                            //    }
                            //}
                            //if (node_zongdi.Nodes["�����ڵ�ͼ��"].Checked == false)
                            //{
                            //    ����֤��ϢToolStripMenuItem.Visible = false;
                            //}
                            //if (node_zongdi.Nodes["˰���ڵ�ͼ��"].Checked == false)
                            //{
                            //    ˰����ϢToolStripMenuItem.Visible = false;
                            //}
                        }
                    }
                    else if (isLast && (MainMapImage.Map.CurrentLayer.LayerName == BOAT_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName == RESCUE_BOAT_LAYER_NAME))
                    {
                        if (ImagePos.Button == MouseButtons.Right)
                        {
                            //ѡ�У�����Ҽ�
                            ѡ��ToolStripMenuItem.Visible = false;
                            �ƶ�ToolStripMenuItem.Visible = false;
                            ����ToolStripMenuItem.Visible = false;
                            �Ŵ�ToolStripMenuItem.Visible = false;
                            ȫͼ��ʾToolStripMenuItem.Visible = false;
                            ZoomAreatoolStripMenuItem.Visible = false;
                            btnClearAreaPriceFill.Visible = false;
                            MeasureToolStripMenuItem.Visible = false;
                            //btnSetObjectStyle.Visible = false;
                            //btnDeleteObjectStyle.Visible = false;
                            ������ϢToolStripMenuItem.Visible = true;
                            �켣ToolStripMenuItem.Visible = true;
                            �Ѿȶ���ϢToolStripMenuItem.Visible = false;
                            ���˻���Ԯ��ϢToolStripMenuItem.Visible = false;
                            �ɳ��ѾȶӴ���ToolStripMenuItem.Visible = false;
                            ��Ԯ����ToolStripMenuItem.Visible = false;

                            if (MainMapImage.Map.CurrentLayer.LayerName == BOAT_LAYER_NAME)
                            {
                                Decimal objectId = MainMapImage.SelectObjects[0].ID;
                                //��ѯ�Ƿ���������������ѵ�

                                ����Ϊ���Ѵ���ToolStripMenuItem.Visible = true;
                            }
                            else
                            {
                                ����Ϊ���Ѵ���ToolStripMenuItem.Visible = false;
                            }
                            //��ѯ�Ƿ������ѵ����������
                            if (MainMapImage.Map.CurrentLayer.LayerName == BOAT_LAYER_NAME)
                            {
                                List<SqlParameter> param = new List<SqlParameter>();
                                param.Add(new SqlParameter("id", MainMapImage.SelectObjects[0].ID));
                                string sql = SqlHelper.GetSql("SelectBoatNewMessage");
                                sql = sql.Replace("@table", "t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.CurrentLayer.ID);
                                DataTable table = SqlHelper.Select(sql, param);
                                if (table.Rows[0]["������������ѵ�"].ToString() == "1" || table.Rows[0]["������������ѵ�"].ToString() == "2")
                                {
                                    ������ϢToolStripMenuItem.Visible = false;
                                    �켣ToolStripMenuItem.Visible = false;
                                    ����Ϊ���Ѵ���ToolStripMenuItem.Visible = false;
                                }
                            }
                        }
                    }
                    else if (isLast && (MainMapImage.Map.CurrentLayer.LayerName == RESCUE_LAYER_NAME))
                    {
                        if (ImagePos.Button == MouseButtons.Right)
                        {
                            //ѡ�У�����Ҽ�
                            ѡ��ToolStripMenuItem.Visible = false;
                            �ƶ�ToolStripMenuItem.Visible = false;
                            ����ToolStripMenuItem.Visible = false;
                            �Ŵ�ToolStripMenuItem.Visible = false;
                            ȫͼ��ʾToolStripMenuItem.Visible = false;
                            ZoomAreatoolStripMenuItem.Visible = false;
                            btnClearAreaPriceFill.Visible = false;
                            MeasureToolStripMenuItem.Visible = false;
                            //btnSetObjectStyle.Visible = false;
                            //btnDeleteObjectStyle.Visible = false;
                            ������ϢToolStripMenuItem.Visible = false;
                            �켣ToolStripMenuItem.Visible = false;
                            �Ѿȶ���ϢToolStripMenuItem.Visible = true;
                            ���˻���Ԯ��ϢToolStripMenuItem.Visible = false;
                            �ɳ��ѾȶӴ���ToolStripMenuItem.Visible = true;
                            ��Ԯ����ToolStripMenuItem.Visible = true;
                            ����Ϊ���Ѵ���ToolStripMenuItem.Visible = false;
                        }
                    }
                    else if (isLast && (MainMapImage.Map.CurrentLayer.LayerName == RESCUE_WURENJI_LAYER_NAME))
                    {
                        if (ImagePos.Button == MouseButtons.Right)
                        {
                            //ѡ�У�����Ҽ�
                            ѡ��ToolStripMenuItem.Visible = false;
                            �ƶ�ToolStripMenuItem.Visible = false;
                            ����ToolStripMenuItem.Visible = false;
                            �Ŵ�ToolStripMenuItem.Visible = false;
                            ȫͼ��ʾToolStripMenuItem.Visible = false;
                            ZoomAreatoolStripMenuItem.Visible = false;
                            btnClearAreaPriceFill.Visible = false;
                            MeasureToolStripMenuItem.Visible = false;
                            //btnSetObjectStyle.Visible = false;
                            //btnDeleteObjectStyle.Visible = false;
                            ������ϢToolStripMenuItem.Visible = false;
                            �켣ToolStripMenuItem.Visible = true;
                            �Ѿȶ���ϢToolStripMenuItem.Visible = false;
                            ���˻���Ԯ��ϢToolStripMenuItem.Visible = true;
                            �ɳ��ѾȶӴ���ToolStripMenuItem.Visible = false;
                            ��Ԯ����ToolStripMenuItem.Visible = false;
                            ����Ϊ���Ѵ���ToolStripMenuItem.Visible = false;
                        }
                    }
                    //else if (isLast && (MainMapImage.Map.CurrentLayer.LayerName == "���˻���Ԯע��"))
                    //{
                    //    if (ImagePos.Button == MouseButtons.Right)
                    //    {
                    //        //ѡ�У�����Ҽ�
                    //        ѡ��ToolStripMenuItem.Visible = false;
                    //        �ƶ�ToolStripMenuItem.Visible = false;
                    //        ����ToolStripMenuItem.Visible = false;
                    //        �Ŵ�ToolStripMenuItem.Visible = false;
                    //        ȫͼ��ʾToolStripMenuItem.Visible = false;
                    //        ZoomAreatoolStripMenuItem.Visible = false;
                    //        btnClearAreaPriceFill.Visible = false;
                    //        MeasureToolStripMenuItem.Visible = false;
                    //        //btnSetObjectStyle.Visible = false;
                    //        //btnDeleteObjectStyle.Visible = false;
                    //        ������ϢToolStripMenuItem.Visible = false;
                    //        �Ѿȶ���ϢToolStripMenuItem.Visible = false;
                    //        ���˻���Ԯ��ϢToolStripMenuItem.Visible = true;
                    //        �ɳ��ѾȶӴ���ToolStripMenuItem.Visible = true;
                    //        ��Ԯ����ToolStripMenuItem.Visible = true;
                    //        ����Ϊ���Ѵ���ToolStripMenuItem.Visible = false;
                    //    }
                    //}
                    else
                    {
                        if (quanxianList.Contains("ѡ��Ԫ��"))
                        {
                            ѡ��ToolStripMenuItem.Visible = true;
                        }
                        //�ƶ�ToolStripMenuItem.Visible = true;
                        if (quanxianList.Contains("��С"))
                        {
                            ����ToolStripMenuItem.Visible = true;
                        }
                        if (quanxianList.Contains("�Ŵ�"))
                        {
                            �Ŵ�ToolStripMenuItem.Visible = true;
                        }
                        if (quanxianList.Contains("ȫͼ��ʾ"))
                        {
                            ȫͼ��ʾToolStripMenuItem.Visible = true;
                        }
                        if (quanxianList.Contains("�ֲ�����"))
                        {
                            ZoomAreatoolStripMenuItem.Visible = true;
                        }
                        //btnClearAreaPriceFill.Visible = true;
                        if (quanxianList.Contains("����"))
                        {
                            MeasureToolStripMenuItem.Visible = true;
                        }
                        //btnSetObjectStyle.Visible = true;
                        //btnDeleteObjectStyle.Visible = true;
                        ������ϢToolStripMenuItem.Visible = false;
                        �켣ToolStripMenuItem.Visible = false;
                        �Ѿȶ���ϢToolStripMenuItem.Visible = false;
                        ���˻���Ԯ��ϢToolStripMenuItem.Visible = false;
                        �ɳ��ѾȶӴ���ToolStripMenuItem.Visible = false;
                        ��Ԯ����ToolStripMenuItem.Visible = false;
                        ����Ϊ���Ѵ���ToolStripMenuItem.Visible = false;
                    }
                }
                if (sender == MainMapImage1)
                {
                    LayerView1_Enter(LayerView1, EventArgs.Empty);
                }
                else
                {
                    LayerView1_Enter(LayerView2, EventArgs.Empty);
                }
                MapToolTip.Hide();
                _LastWorldPos = WorldPos;
                if (btnMapToImage.Checked)
                {
                    label1.Left = ImagePos.Location.X;
                    label1.Top = ImagePos.Location.Y;
                    label1.Tag = ImagePos.Location;
                    return;
                }
                if (MainMapImage.PickCoordinate)
                {
                    return;
                }
                MainMapImage = sender as MapImage;
                if (MainMapImage == MainMapImage1)
                {
                    MapToolTip = myToolTipControl1;
                    LayerView = LayerView1;
                    MyMapEditImage = MyMapEditImage1;
                    MapDBClass.IsCompareMap = false;
                }
                else
                {
                    MapToolTip = myToolTipControl2;
                    LayerView = LayerView2;
                    MyMapEditImage = MyMapEditImage2;
                    MapDBClass.IsCompareMap = true;
                }
                SetToolBarStatus();
                if (ImagePos.Button != MouseButtons.Left)
                {
                    return;
                }
                if (MainMapImage.ActiveTool == MapImage.Tools.Select)
                {
                    //if (MainMapImage.Map.CurrentLayer == null)
                    //{
                    //    return;
                    //}
                    //if (MainMapImage.Map.CurrentLayer.LayerName != "�����ڵ�ͼ��")
                    //{
                    //    return;
                    //}
                    //����ԭ�ж���ε�ѡ��״̬
                    if (Control.ModifierKeys != Keys.Control)
                    {
                        ClearSelectObject();
                    }
                    bool find = false;

                    VectorLayer findlayer = null;
                    Geometry findobject = null;

                    List<object> ret = MainMapImage.Map.PickUpObject(new System.Drawing.Point(ImagePos.X, ImagePos.Y), MainMapImage.Size, Resources.Flag.Size);
                    //����ѡ�еؿ�
                    lastRet = new List<object>();
                    for (int i = 0; i < ret.Count; i++)
                    {
                        lastRet.Add(ret[i]);
                    }
                    find = (bool)ret[0];
                    if (find)
                    {
                        findobject = ret[1] as Geometry;
                        findlayer = ret[2] as VectorLayer;
                        //if (!(findlayer.LayerName.IndexOf("�����ڵ�") >= 0 || findlayer.LayerName.IndexOf("˰���ڵ�") >= 0))
                        //{
                        //    return;
                        //}
                        MainMapImage.SelectObjects.Add(findobject);
                    }
                    if (find)
                    {
                        if (!_SelectObjectConfirm || (_SelectObjectConfirm && (findobject is Polygon || findobject is MultiPolygon)))
                        {
                            //if (_SelectObjectConfirm && (findobject is Polygon || findobject is MultiPolygon) && findobject.Text == "")
                            //{
                            //    MessageBox.Show("ѡ�������û���������ƣ����ܱ�ѡ��Ϊ�ڵأ�������ѡ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    return;
                            //}
                            TreeNode a = FindNodeLayer(LayerView.Nodes[0], findlayer);
                            ////�����ǰѡ�е�Ԫ���ڵ�ǰ�Ӵ���ʾ��Χ�ڵĻ�����ô��ֹ��ѡ�е�Ԫ�����ڵ�ͼ��ķ�Χ������ʾ���Ӵ���
                            _triggerEvent = false;
                            LayerView.SelectedNode = a;
                            MainMapImage.Map.CurrentLayer = findlayer;
                            _triggerEvent = true;
                        }
                    }
                    MainMapImage.Refresh();
                    SetToolForm();
                    if (find && MainMapImage.SelectObjects.Count == 1 && Control.ModifierKeys != Keys.Control)
                    {
                        if (!_SelectObjectConfirm || (_SelectObjectConfirm && (findobject is Polygon || findobject is MultiPolygon)))
                        {
                            //if (findobject.Text != "")
                            //{
                            MapToolTip.SelectObjectConfirm = _SelectObjectConfirm;
                            string msg = findobject.Text;
                            int num = 1;
                            if (findlayer.LayerName==BOAT_LAYER_NAME)
                            {
                                string sql = SqlHelper.GetSql("SelectBoatMessageByBoatNo1");
                                sql = sql.Replace("@table", "t_" + MainMapImage.Map.MapId + "_" + findlayer.ID);
                                //sql = sql + " where [layer_id]=@layerid ";
                                //sql = sql + " and [geom_id]=@objectid";
                                List<SqlParameter> param = new List<SqlParameter>();
                                //param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                                //param.Add(new SqlParameter("layerid", findlayer.ID));
                                //param.Add(new SqlParameter("objectid", findobject.ID));
                                param.Add(new SqlParameter("boatNo", msg));
                                DataTable table = SqlHelper.Select(sql, param);

                                if (table.Rows.Count > 0)
                                {
                                    if (table.Rows[0]["������������ѵ�"].ToString() == "1" || table.Rows[0]["������������ѵ�"].ToString() == "2")
                                    {
                                        //msg = "";
                                    }
                                    else
                                    {
                                        for (int row = 0; row < table.Rows.Count; row++)
                                        {
                                            if (row > 0)
                                            { num = num + 2; }
                                            msg += "\r\n�������ͣ�" + table.Rows[row]["boatType"].ToString() + "   ����״̬��" + table.Rows[row]["����״̬"].ToString() + "\r\n������"
                                                + table.Rows[row]["������"].ToString() + "      ���٣�" + table.Rows[row]["����"].ToString() + "\r\n���ʱ�䣺";
                                            if (!string.IsNullOrEmpty(table.Rows[row]["���ʱ��"].ToString()))
                                            {
                                                msg += DateTime.Parse(table.Rows[row]["���ʱ��"].ToString()).ToString();
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    msg += "\r\n�������ͣ�       ����״̬��\r\n������         ���٣�\r\n���ʱ�䣺             ";
                                }
                            } 
                            else if (findlayer.LayerName==RESCUE_BOAT_LAYER_NAME)
                            {
                                string sql = SqlHelper.GetSql("SelectBoatMessageByBoatNo1");
                                sql = sql.Replace("@table", "t_" + MainMapImage.Map.MapId + "_" + findlayer.ID);
                                //sql = sql + " where [layer_id]=@layerid ";
                                //sql = sql + " and [geom_id]=@objectid";
                                List<SqlParameter> param = new List<SqlParameter>();
                                //param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                                //param.Add(new SqlParameter("layerid", findlayer.ID));
                                //param.Add(new SqlParameter("objectid", findobject.ID));
                                param.Add(new SqlParameter("boatNo", msg));
                                DataTable table = SqlHelper.Select(sql, param);

                                if (table.Rows.Count > 0)
                                {
                                    for (int row = 0; row < table.Rows.Count; row++)
                                    {
                                        if (row > 0)
                                        { num = num + 2; }
                                        msg += "\r\n�������ͣ�" + table.Rows[row]["boatType"].ToString() + "   ����״̬��" + table.Rows[row]["����״̬"].ToString() + "\r\n������"
                                            + table.Rows[row]["������"].ToString() + "      ���٣�" + table.Rows[row]["����"].ToString() + "\r\n���ʱ�䣺";
                                        if (!string.IsNullOrEmpty(table.Rows[row]["���ʱ��"].ToString()))
                                        {
                                            msg += DateTime.Parse(table.Rows[row]["���ʱ��"].ToString()).ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    msg += "\r\n�������ͣ�       ����״̬��\r\n������         ���٣�\r\n���ʱ�䣺             ";
                                }
                            }
                            else if (findlayer.LayerName.IndexOf(RESCUE_LAYER_NAME) >= 0)
                            {
                                //��ѯ������Ϣ
                                string sql = "select t1.* from t_boatMessage t1 ";
                                sql = sql + " where [layer_id]=@layerid ";
                                sql = sql + " and [geom_id]=@objectid";
                                List<SqlParameter> param = new List<SqlParameter>();
                                param.Clear();
                                param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                                param.Add(new SqlParameter("layerid", findlayer.ID));
                                param.Add(new SqlParameter("objectid", findobject.ID));
                                DataTable table = SqlHelper.Select(sql, param);

                                //��ѯ���˻���Ϣ
                                sql = "select t1.* from t_wurenjiMessage t1  where [layer_id]=@layerid and [geom_id]=@objectid";
                                param.Clear();
                                param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                                param.Add(new SqlParameter("layerid", findlayer.ID));
                                param.Add(new SqlParameter("objectid", findobject.ID));
                                DataTable table1 = SqlHelper.Select(sql, param);
                                int rescueNum = 0;//���ھ�Ԯ����
                                int stopNum = 0;//���þ�Ԯ����
                                int wurenjiNum = 0;//���ھ�Ԯ���˻�
                                int stopWurenji = 0;//�������˻�
                                if (table.Rows.Count > 0)
                                {
                                    for (int i = 0; i < table.Rows.Count; i++)
                                    {
                                        if (table.Rows[i]["sailType"].ToString() == "���ھ�Ԯ")
                                        {
                                            rescueNum++;
                                        }
                                        else
                                        {
                                            stopNum++;
                                        }
                                    }
                                        
                                }
                                if (table1.Rows.Count > 0)
                                {
                                    for (int i = 0; i < table1.Rows.Count; i++)
                                    {
                                        if (table1.Rows[i]["״̬"].ToString() == "���ھ�Ԯ")
                                        {
                                            wurenjiNum++;
                                        }
                                        else
                                        {
                                            stopWurenji++;
                                        }
                                    }

                                }
                                msg += "\r\n���ھ�Ԯ������" + rescueNum.ToString() + "       ���þ�Ԯ������" + stopNum.ToString() +
                                        "\r\n���ھ�Ԯ���˻���" + wurenjiNum.ToString() + "     ���þ�Ԯ���˻���" + stopWurenji.ToString();
                            }
                            else if (findlayer.LayerName.IndexOf(RESCUE_WURENJI_LAYER_NAME) >= 0)
                            {
                                string sql = "select t1.* from t_wurenjiMessage t1 where ���˻���=@���˻���";
                                //sql = sql + " where [layer_id]=@layerid ";
                                //sql = sql + " and [geom_id]=@objectid";
                                List<SqlParameter> param = new List<SqlParameter>();
                                param.Clear();
                                //param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                                //param.Add(new SqlParameter("layerid", findlayer.ID));
                                //param.Add(new SqlParameter("objectid", findobject.ID));
                                param.Add(new SqlParameter("���˻���", msg));
                                DataTable table = SqlHelper.Select(sql, param);
                                if (table.Rows.Count > 0)
                                {
                                    msg += "\r\n��ؾ��룺" + table.Rows[0]["��ؾ���"].ToString() +
                                        "     ����ʱ�䣺" + table.Rows[0]["����ʱ��"].ToString() + "\r\nѲ���ٶȣ�" + table.Rows[0]["Ѳ���ٶ�"].ToString() +
                                        "     �غ�������" + table.Rows[0]["�غ�����"].ToString();
                                }
                                else
                                {
                                   msg += "\r\n��ؾ��룺     ����ʱ�䣺\r\nѲ���ٶȣ�     �غ�������" ;
                                }
                            }
                            //MapToolTip = myToolTipControl1;
                            MapToolTip.Initial(MainMapImage.Map.MapId, findlayer.ID, findobject, msg, ImagePos.X, ImagePos.Y, num);
                            MapToolTip.Width = 280;
                            MapToolTip.Show();
                        }
                    }
                    SetToolBarStatus();
                }
            }
            IsMove = 2;
            //MainMapImage1.ActiveTool = MapImage.Tools.Select;
            //MainMapImage2.ActiveTool = MapImage.Tools.Select;
            //MainMapImage.ActiveTool = MapImage.Tools.Select;
            //�����ǰ�ǽ�ͼ����������ʾ��ͼ�����ȡ����ť
            if (btnMapToImage.Checked)
            {
                label1.Tag = null;
                if (label1.Width < 5)
                {
                    return;
                }
                btnMapToImage.Checked = false;
                btnMapToImageCancel.Top = label1.Bottom + 5;
                btnMapToImageCancel.Left = label1.Right - btnMapToImageCancel.Width;
                btnMapToImageOk.Top = btnMapToImageCancel.Top;
                btnMapToImageOk.Left = btnMapToImageCancel.Left - btnMapToImageOk.Width - 5;
                btnMapToImageCancel.Visible = true;
                btnMapToImageOk.Visible = true;
            }

            if (MainMapImage.PickCoordinate && MainMapImage == sender)
            {
                if (WorldPos.X == _LastWorldPos.X && WorldPos.Y == _LastWorldPos.Y)
                {
                    if (inputGeometryForm != null && !inputGeometryForm.IsDisposed)
                    {
                        inputGeometryForm.InsertCoordinate(WorldPos);
                    }
                }
                return;
            }

            //MapImage.bottomLeftX = MainMapImage.Map.Envelope.BottomLeft.X;
            //MapImage.bottomLeftX = MainMapImage1.Map.Envelope.BottomLeft.X;
            //MapImage.bottomLeftX = MainMapImage2.Map.Envelope.BottomLeft.X;
            //MapImage.bottomLeftX = MainMapImage1.Map.Envelope.BottomLeft.X;//�������¶�x����
            //MapImage.bottomLeftY = MainMapImage2.Map.Envelope.BottomLeft.Y;
            //MapImage.topLeftY = MainMapImage.Map.Envelope.TopLeft.Y;
            //MapImage.topRigthX = MainMapImage.Map.Envelope.TopRight.X;
            //MapImage.topRigthX = MainMapImage.Map.Envelope.TopRight.X;
        }

        #endregion

        #region - ���� -

        private TreeNode FindDataNode(string txt)
        {
            foreach (TreeNode node in LayerView.Nodes[0].Nodes)
            {
                //if (node.Text == "˰�񼶱�ͼ��")
                //{
                //    return node;
                //}
                //else
                if (node.Text == "����ͼ��" || node.Text == "˰�񼶱�ͼ��")
                {
                    //if (txt != "˰�񼶱�ͼ��")
                    //{
                        foreach (TreeNode subnode in node.Nodes)
                        {
                            if (subnode.Text == txt)
                            {
                                return subnode;
                            }
                        }
                    //}
                }
            }
            return null;
        }
        private TreeNode FindBaseNode(string txt)
        {
            foreach (TreeNode node in LayerView.Nodes[0].Nodes)
            {
                if (node.Text == Resources.BaseLayer)
                {
                    return node;
                }
                //else if (node.Text == Resources.PhotoLayer)
                //{
                //    return node;
                //}
            }
            return null;
        }

        /// <summary>
        /// �½�ͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewRandomGeometryLayer_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            VectorLayer.LayerType layertype = GetLayerTypeByText(item.Text);
            TreeNode mainnode = new TreeNode(item.Text);
            mainnode.Checked = true;
            TreeNode findnode = null;
            if (item.Text == "����ͼ��")
            {
                findnode = FindBaseNode(item.Text);
            }
            else
            {
                findnode = FindDataNode(item.Text);
            }
            if (findnode != null)
            {
                mainnode = findnode;
            }
            else if (item.Text == Resources.BaseLayer)
            {
                if (LayerView.Nodes[0].Nodes.Count > 0)
                {
                    if (LayerView.Nodes[0].Nodes[0].Text == Resources.PhotoLayer)
                    {
                        LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                    }
                    else
                    {
                        LayerView.Nodes[0].Nodes.Insert(0, mainnode);
                    }
                }
                else
                {
                    LayerView.Nodes[0].Nodes.Add(mainnode);
                }
            }
            //else if (item.Text == Resources.MotionPointLayer)
            //{
            //    findnode = FindNode(Resources.MotionPointLayer);
            //    if (findnode == null)
            //    {
            //        findnode = new TreeNode(Resources.MotionPointLayer);
            //        //LayerView.Nodes[0].Nodes.Insert(1, mainnode);
            //        LayerView.Nodes[0].Nodes.Add(findnode);
            //    }
            //    findnode.Nodes.Add(mainnode);
            //}
            else if (item.Text == Resources.PhotoLayer)
            {
                findnode = FindNode(Resources.PhotoLayer);
                if (findnode == null)
                {
                    findnode = new TreeNode(Resources.PhotoLayer);
                    LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                }
                //findnode.Nodes.Add(mainnode);
            }
            else
            {
                findnode = FindNode(Resources.DataLayer);
                if (findnode == null)
                {
                    findnode = new TreeNode(Resources.DataLayer);
                    LayerView.Nodes[0].Nodes.Add(findnode);;
                }
                findnode.Nodes.Add(mainnode);
            }

            //ȡ��ͼ������
            int count = 1;
            if (mainnode.Nodes != null)
            {
                count = mainnode.Nodes.Count;
            }
            //������ͼ������
            string layername = Resources.NewLayerDefaultName + count.ToString();
            //������ͼ��
            VectorLayer layer = new VectorLayer(layername, layertype);
            Common.SetLayerDefaultStyle(layer);
            int index = 0;
            if (mainnode.LastNode != null)
            {
                index = mainnode.LastNode.Index + 1;
            }
            DialogResult ret = MessageBox.Show(Resources.NotSaveTipMessage, Resources.Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            layer.NeedSave = ret == DialogResult.Yes;
            decimal layerid = MapDBClass.GetNewLayerId(MainMapImage.Map.MapId, layername, index, ((int)layertype).ToString(), layer.NeedSave);
            layer.ID = layerid;
            if (layer.NeedSave)
            {
                Command.SendAddLayerCommand(MainMapImage.Map.MapId, layer.ID);
            }
            Collection<Geometry> geometry = new Collection<Geometry>();
            GeometryProvider provider = new GeometryProvider(geometry);
            layer.DataSource = provider;
            //�����½ڵ�
            TreeNode node = new TreeNode(layername);
            node.Checked = true;
            node.Tag = layer;
            mainnode.Nodes.Add(node);
            CheckNode(node);
            CheckParentNode(node);
            LayerView.MySelectedNode = node;
            //���ͼ��
            AddLayer(layer, node);
            //LayerView.ExpandAll();
            MainMapImage.Refresh();
            SetToolBarStatus();
        }

        /// <summary>
        /// ɾ����ǰͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveLayerToolStripButton_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            //���û��ѡ��ͼ�㣬��ʲô������
            if (LayerView.MySelectedNode == null)
            {
                return;
            }
            //���ѡ���˸��ڵ㣬��ʲô������
            if (LayerView.Nodes[0].IsSelected)
            {
                return;
            }
            if (e != null)
            {
                if (MessageBox.Show("ȷ��Ҫɾ����ǰѡ���ͼ���Լ���ͼ��������Ԫ�ص�������Ϣ��", "����", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
            }
            //ȡ��ѡ���ͼ������
            ILayer layer = GetCurrentLayer();
            if (MainMapImage.NeedSave && layer.NeedSave)
            {
                MapDBClass.DeleteLayer(MainMapImage.Map.MapId, layer.ID);
                Command.SendDeleteLayerCommand(MainMapImage.Map.MapId, layer.ID);
            }
            foreach (ToolStripButton btn in btnEditLayerList.DropDownItems)
            {
                if (btn.Tag == layer)
                {
                    btnEditLayerList.DropDownItems.Remove(btn);
                    if (btnEditLayerList.Tag == layer)
                    {
                        btnEditLayerList.Tag = null;
                        btnEditLayerList.Text = "ȡ���༭";
                    }
                    _EditLayer = null;
                    break;
                }
            }
            //ɾ��ѡ���ͼ��
            MainMapImage.Map.Layers.Remove(layer);
            //ɾ��ѡ��Ľڵ�
            if (layer is VectorLayer || layer is GdalRasterLayer)
            {
                if (LayerView.MySelectedNode.Tag is Geometry)
                {
                    LayerView.MySelectedNode = LayerView.MySelectedNode.Parent;
                }
                TreeNode parentnode = LayerView.MySelectedNode.Parent;
                LayerView.MySelectedNode.Remove();
                if (parentnode.Nodes.Count <= 0)
                {
                    parentnode.Remove();
                }
                if (layer is GdalRasterLayer)
                {
                    GdalRasterLayer gdalRasterLayer = layer as GdalRasterLayer;
                    if (MainMapImage.NeedSave)
                    {
                        MapDBClass.DeleteTifSettings(MainMapImage.Map.MapId, Resources.PhotoLayer, gdalRasterLayer.Filename);
                    }
                }
            }
            else
            {
                TreeNode parentnode = LayerView.MySelectedNode.Parent.Parent;
                LayerView.MySelectedNode.Parent.Remove();
                if (parentnode.Nodes.Count <= 0)
                {
                    parentnode.Remove();
                }
            }
            foreach (TreeNode node in LayerView.Nodes[0].Nodes)
            {
                if (node.Nodes.Count <= 0)
                {
                    node.Remove();
                }
            }

            MainMapImage.Refresh();
            SetToolBarStatus();
        }

        private void OpenLayer(ToolStripMenuItem item, string[] filelist, bool needUpdate)
        {
            DialogResult ret = DialogResult.No;

            MyMapEditImage.Visible = false;
            if (item.Text == Resources.PhotoLayer)
            {
                AddLayerDialog.Multiselect = true;
                AddLayerDialog.Filter = "*.TIF|*.TIF|*.JPEG|*.JPG";
            }
            else
            {
                AddLayerDialog.Filter = "Shapefiles|*.shp";
                ret = MessageBox.Show(Resources.NotSaveWhenOpen, Resources.Tip, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            }
            //���ѡ����shp�ļ�
            if (filelist != null && filelist.Length > 0)
            {
                VectorLayer.LayerType layertype = GetLayerTypeByText(item.Text);
                TreeNode mainnode = new TreeNode(item.Text);
                mainnode.Checked = true;
                TreeNode findnode = FindNode(item.Text);
                if (findnode != null)
                {
                    mainnode = findnode;
                }
                else if (item.Text == Resources.PhotoLayer)
                {
                    LayerView.Nodes[0].Nodes.Insert(0, mainnode);
                }
                else if (item.Text == Resources.BaseLayer)
                {
                    if (LayerView.Nodes[0].Nodes.Count > 0)
                    {
                        if (LayerView.Nodes[0].Nodes[0].Text == Resources.PhotoLayer)
                        {
                            LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                        }
                        else
                        {
                            LayerView.Nodes[0].Nodes.Insert(0, mainnode);
                        }
                    }
                    else
                    {
                        LayerView.Nodes[0].Nodes.Add(mainnode);
                    }
                }
                //else if (item.Text == Resources.MotionPointLayer)
                //{
                //    //bool flag = true;
                //    //foreach (TreeNode node in LayerView.Nodes[0].Nodes)
                //    //{
                //    //    if (node.Text == Resources.MotionPointLayer)
                //    //    {
                //    //        flag = false;
                //    //        node.Nodes.Add(mainnode);
                //    //    }
                //    //}
                //    //if(flag)
                //    //{
                //    //    LayerView.Nodes.Insert(0, mainnode);
                //    //}
                //    if (LayerView.Nodes[0].Nodes.Count > 0)
                //    {
                //        if (LayerView.Nodes[0].Nodes[0].Text == Resources.PhotoLayer)
                //        {
                //            LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                //        }
                //        else
                //        {
                //            LayerView.Nodes[0].Nodes.Insert(0, mainnode);
                //        }
                //    }
                //    else
                //    {
                //        LayerView.Nodes[0].Nodes.Add(mainnode);
                //    }
                //}
                else if (item.Text == Resources.MotionPointLayer)
                {
                    findnode = FindNode(Resources.MotionPointLayer);
                    if (findnode == null)
                    {
                        findnode = new TreeNode(Resources.MotionPointLayer);
                        //LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                        LayerView.Nodes[0].Nodes.Add(findnode);
                    }
                    findnode.Nodes.Add(mainnode);
                }
                else if (item.Text == Resources.PhotoLayer)
                {
                    findnode = FindNode(Resources.PhotoLayer);
                    if (findnode == null)
                    {
                        findnode = new TreeNode(Resources.PhotoLayer);
                        LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                    }
                    //findnode.Nodes.Add(mainnode);
                }
                else
                {
                    findnode = FindNode(Resources.DataLayer);
                    if (findnode == null)
                    {
                        findnode = new TreeNode(Resources.DataLayer);
                        LayerView.Nodes[0].Nodes.Add(findnode);
                    }
                    findnode.Nodes.Add(mainnode);
                }
                //ѭ��ѡ��Ķ���ļ�
                foreach (string fileName in filelist)
                {
                    if (!File.Exists(fileName))
                    {
                        continue;
                    }
                    //ȡ���ļ���չ��
                    string extension = Path.GetExtension(fileName).ToLower();
                    if (extension == ".shp")
                    {
                        ILayerFactory layerFactory = null;

                        if (!_layerFactoryCatalog.TryGetValue(extension, out layerFactory))
                        {
                            continue;
                        }
                        //��ʼ��ͼ��
                        VectorLayer layer = null;
                        ShapeFile shape = null;
                        //VectorLayer layer = (VectorLayer)layerFactory.Create(Path.GetFileNameWithoutExtension(fileName), fileName, layertype); 
                        //ShapeFile shape = (ShapeFile)layer.DataSource;
                        //layer = new VectorLayer(Path.GetFileNameWithoutExtension(fileName), layertype);
                        //Common.SetLayerDefaultStyle(layer);
                        ////��ʼ���ڵ�
                        //TreeNode node = new TreeNode(layer.LayerName);
                        //node.Checked = true;
                        //node.Tag = layer;
                        bool haveNode = false;
                        int num = 0;
                        ChoosePicForm picForm = new ChoosePicForm();//ѡ����Ҫ�����ͼ��
                        //if (picForm.DialogResult != DialogResult.OK)
                        //{
                        //    return;
                        //}
                        List<string> picfields = new List<string>();
                        for (int i = 0; i < mainnode.Nodes.Count; i++)
                        {
                            picfields.Add(mainnode.Nodes[i].Text);
                        }
                        picForm.Fields = picfields;
                        if (picForm.ShowDialog() == DialogResult.OK)
                        {
                            //û��ѡ��ͼ��Ĭ������һ��ͼ��
                            if (picForm.SelectField != "")
                            {
                                layer = (VectorLayer)layerFactory.Create(Path.GetFileNameWithoutExtension(picForm.SelectField), fileName, layertype);
                                shape = (ShapeFile)layer.DataSource;
                                layer = new VectorLayer(Path.GetFileNameWithoutExtension(picForm.SelectField), layertype);
                            }
                            else
                            {
                                layer = (VectorLayer)layerFactory.Create(Path.GetFileNameWithoutExtension(fileName), fileName, layertype);
                                shape = (ShapeFile)layer.DataSource;
                                layer = new VectorLayer(Path.GetFileNameWithoutExtension(fileName), layertype);
                            }
                        }
                        else {
                            return;
                        }
                        Common.SetLayerDefaultStyle(layer);
                        //��ʼ���ڵ�
                        TreeNode node = null;
                        if (picForm.SelectField != "")
                        {
                            node = new TreeNode(picForm.SelectField);
                        }
                        else
                        {
                            node = new TreeNode(layer.LayerName);
                        }
                        node.Checked = true;
                        node.Tag = layer;
                        if (picForm.SelectField != "")
                        {
                            node.Text = picForm.SelectField;
                        }
                        //bool haveNode = false;
                        //int num = 0;

                        for (int i = 0; i < mainnode.Nodes.Count; i++)
                        {
                            if (mainnode.Nodes[i].Text.Equals(node.Text))
                            {
                                haveNode = true;
                                num = i;
                                break;
                            }
                        }
                        if (!haveNode)
                        {
                            mainnode.Nodes.Add(node);
                        }
                        //mainnode.Nodes.Add(node);
                        LayerView.MySelectedNode = mainnode.Nodes[num];
                        layer.NeedSave = ret == DialogResult.Yes;
                        //���ͼ�㵽��ͼ
                        if (!haveNode)
                        {
                            layer.ID = MapDBClass.GetNewLayerId(MainMapImage.Map.MapId, layer.LayerName, node.Index, ((int)layertype).ToString(), layer.NeedSave);
                        }
                        else
                        {
                            layer.ID = MapDBClass.GetLayerId(MainMapImage.Map.MapId, node.Text);
                        }
                        //����DBF����

                        string dbf = fileName.Substring(0, fileName.Length - 3) + "dbf";
                        string field = "";
                        shape.Open();
                        if (File.Exists(dbf))
                        {
                            ChooseFieldForm form = new ChooseFieldForm();
                            DbaseReader dr = new DbaseReader(dbf);
                            dr.Open();
                            List<string> fields = new List<string>();
                            for (int i = 0; i < dr.Columns.Length; i++)
                            {
                                fields.Add(dr.Columns[i].ColumnName);
                            }
                            dr.Close();
                            form.Fields = fields;
                            if (!haveNode)
                            {
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    field = form.SelectField;
                                }
                            }
                        }


                        Collection<Geometry> list = shape.GetGeometriesInView(null);
                        int index = 0;
                        List<EasyMap.Data.FeatureDataRow> ds = new List<EasyMap.Data.FeatureDataRow>();
                        foreach (Geometry geometry in list)
                        {
                            geometry.LayerId = layer.ID;
                            EasyMap.Data.FeatureDataRow row = shape.GetFeature(geometry.GeometryId);
                            if (field != "")
                            {
                                geometry.Text = row[field].ToString();
                            }
                            ds.Add(row);
                        }
                        if (MainMapImage.NeedSave && layer.NeedSave)
                        {
                            if (File.Exists(dbf))
                            {
                                MapDBClass.UpdateLayerPropertyStruct(MainMapImage.Map.MapId, layer.ID, dbf);
                            }
                            //if (item.Text == Resources.DataLayer)
                            //{
                            //    //˰���ڵ�ͼ�㣺true��˰���ڵ�ͼ��geometryId = ����ͼ��geometryId
                            //    MapDBClass.InsertObjects(MainMapImage.Map.MapId, layer.ID, list, ds, true);
                            //}
                            //else
                            //{
                                MapDBClass.InsertObjects(MainMapImage.Map.MapId, layer.ID, list, ds,false);
                            //}
                        }
                        waiting1.SetAutoProcess(false);
                        waiting1.Visible = true;
                        waiting1.ProcessValue = 0;
                        waiting1.MaxProcessValue = list.Count;
                        foreach (Geometry geometry in list)
                        {
                            index++;
                            waiting1.ProcessValue = index;
                            Application.DoEvents();
                            AddGeometryToTree(geometry, geometry.Text);
                        }
                        waiting1.Visible = false;
                        waiting1.SetAutoProcess(true);
                        shape.Close();
                        GeometryProvider provider = new GeometryProvider(list);
                        layer.DataSource = provider;
                        if (picForm.SelectField == "")
                        {
                            AddLayer(layer, node);
                        }
                        //MainMapImage.Refresh();
                        MainMapImage.Map.AddLayer(layer);
                        if (layer is VectorLayer)
                        {
                            VectorLayer vlayer = layer as VectorLayer;
                            SetLayerStyle(vlayer, vlayer.Style.Fill, vlayer.Style.Outline, vlayer.Style.TextColor, vlayer.Style.TextFont, vlayer.Style.EnableOutline, vlayer.Style.HatchStyle, vlayer.Style.Line, vlayer.Style.Penstyle, node);
                        }
                        //ToolStripButton btn;
                        //btn = null;
                        //foreach (Control ctl in this.Controls)
                        //{
                        //    if (ctl.GetType() == typeof(System.Windows.Forms.ToolStrip))
                        //    {
                        //        ToolStrip ts = ctl as ToolStrip;
                        //        ToolStripItem[] items = ts.Items.Find(layer.LayerName, true);
                        //        if (items.Length > 0)
                        //        {
                        //            btn = (ToolStripButton)items[0];
                        //        }
                        //    }
                        //}
                        foreach (ToolStripItem toolItem in this.btnEditLayerList.DropDownItems) 
                        {
                            if (toolItem.Text == layer.LayerName) 
                            {
                                //toolItem.CheckOnClick = true;
                                //toolItem.Tag = layer;
                                toolItem.Click += EditLayer_Click;
                            }
                        }
                        Command.SendAddLayerCommand(MainMapImage.Map.MapId, layer.ID);
                        //CheckNode(node);
                        //CheckParentNode(node);
                    }
                    else
                    {
                        GdalRasterLayer layer = new EasyMap.Layers.GdalRasterLayer(Path.GetFileNameWithoutExtension(fileName), fileName);
                        layer.Type = layertype;
                        layer.NeedSave = false;
                        MainMapImage.Map.AddLayer(layer);//.Layers.Add(layer);
                        //��ʼ���ڵ�
                        TreeNode node = new TreeNode(layer.LayerName);
                        node.Checked = true;
                        node.Tag = layer;
                        mainnode.Nodes.Add(node);
                        //LayerView.MySelectedNode = node;
                        //CheckNode(node);
                        //CheckParentNode(node);
                    }
                }
                if (item.Text == Resources.PhotoLayer && needUpdate)
                {
                    if (MainMapImage.NeedSave)
                    {
                        if (_NeedDeleteOldTifInfo)
                        {
                            MapDBClass.DeleteTifSettings(MainMapImage.Map.MapId, "%", "%");
                            _NeedDeleteOldTifInfo = false;
                        }
                        MapDBClass.InsertTifSettings(MainMapImage.Map.MapId, item.Text, filelist);
                    }
                }
                //LayerView.ExpandAll();
                //��ͼˢ��
                MainMapImage.Refresh();
                SetToolBarStatus();

                if (MainMapImage == MainMapImage1)
                {
                    MainMapImage_MapZoomChanged(MainMapImage2, MainMapImage2.Map.Zoom);
                }
                else if (MainMapImage == MainMapImage2)
                {
                    MainMapImage_MapZoomChanged(MainMapImage1, MainMapImage1.Map.Zoom);
                }
            }
        }
        /// <summary>
        /// ���ͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLayerToolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Text == Resources.PhotoLayer)
            {
                AddLayerDialog.Multiselect = true;
                AddLayerDialog.Filter = "*.TIF|*.TIF|*.JPEG|*.JPG";
            }
            else
            {
                AddLayerDialog.Filter = "Shapefiles|*.shp";
            }
            //ѡ��shp�ļ�
            DialogResult result = AddLayerDialog.ShowDialog(this);
            AddLayerDialog.Multiselect = false;
            //���ѡ����shp�ļ�
            if (result == DialogResult.OK)
            {
                OpenLayer(sender as ToolStripMenuItem, AddLayerDialog.FileNames, true);
            }

        }

        /// <summary>
        /// ��ͼȫ��ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomToExtentsToolStripButton_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            if (MainMapImage1.Map.Layers.Count > 0)
            {
                MainMapImage1.Map.ZoomToExtents();
                TifData.Map = MainMapImage1.Map;
                List<Hashtable> table = TifData.TifFiles;
                MainMapImage1.RequestFromServer = true;
                MainMapImage1.Refresh();
            }
            if (MainMapImage2.Map.Layers.Count > 0)
            {
                MainMapImage2.RequestFromServer = true;
                MainMapImage2.Map.ZoomToExtents();
                MainMapImage2.Refresh();
            }

            MainMapImage_MapZoomChanged(MainMapImage, MainMapImage.Map.Zoom);
            SetMapControlLevel();
        }

        /// <summary>
        /// ��ͼ�ƶ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanToolStripButton_Click(object sender, EventArgs e)
        {
            PanToolStripButton.Checked = true;
            MainMapImage1.ActiveTool = MapImage.Tools.Pan;
            MainMapImage2.ActiveTool = MapImage.Tools.Pan;
            SelecttoolStripButton1.Checked = false;
            ZoomInModeToolStripButton.Checked = false;
            ZoomOutModeToolStripButton.Checked = false;
            MainMapImage.LastOption = MainMapImage.ActiveTool;
            MyMapEditImage.Visible = false;
        }

        /// <summary>
        /// ���Ű�ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomInModeToolStripButton_Click(object sender, EventArgs e)
        {
            ZoomInModeToolStripButton.Checked = true;
            ZoomToolStripMenuItem.Checked = true;
            //����ToolStripMenuItem.Checked = true;
            ZoomOutModeToolStripButton.Checked = false;
            ZoomOutToolStripMenuItem.Checked = false;
            �Ŵ�ToolStripMenuItem.Checked = false;
            MainMapImage1.ActiveTool = MapImage.Tools.ZoomIn;
            MainMapImage2.ActiveTool = MapImage.Tools.ZoomIn;
            SelecttoolStripButton1.Checked = false;
            PanToolStripButton.Checked = false;
            MainMapImage.LastOption = MainMapImage.ActiveTool;
            MyMapEditImage.Visible = false;
        }

        /// <summary>
        /// �Ŵ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomOutModeToolStripButton_Click(object sender, EventArgs e)
        {
            ZoomInModeToolStripButton.Checked = false;
            ZoomToolStripMenuItem.Checked = false;
            //����ToolStripMenuItem.Checked = false;
            ZoomOutModeToolStripButton.Checked = true;
            ZoomOutToolStripMenuItem.Checked = true;
            �Ŵ�ToolStripMenuItem.Checked = true;
            MainMapImage1.ActiveTool = MapImage.Tools.ZoomOut;
            MainMapImage2.ActiveTool = MapImage.Tools.ZoomOut;
            SelecttoolStripButton1.Checked = false;
            PanToolStripButton.Checked = false;
            MainMapImage.LastOption = MainMapImage.ActiveTool;
            MyMapEditImage.Visible = false;
        }

        /// <summary>
        /// ��ʼһ���µĵ�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            MapInputForm form = new MapInputForm();
            form.ShowDialog();
            if (form.Result != DialogResult.OK)
            {
                return;
            }
            //axShockwaveFlash1.Visible = false;
            Command.MadeMap = null;
            MainMapImage.HaveTif = false;
            _NeedDeleteOldTifInfo = true;
            for (int i = imageList1.Images.Count - 1; i > 0; i--)
            {
                imageList1.Images.RemoveAt(i);
            }
            //���ͼ��ڵ�
            LayerView.Nodes[0].Nodes.Clear();
            if (form.MapName.Trim() == "")
            {
                LayerView.Nodes[0].Text = Resources.DefaultMapName;
            }
            else
            {
                LayerView.Nodes[0].Text = form.MapName;
            }
            //��յ�ͼͼ��
            MainMapImage.Map.Layers.Clear();
            //��ͼˢ��
            MainMapImage.Refresh();
            MainMapImage.Map.MapName = form.MapName;
            MainMapImage.Map.Comment = form.MapComment;
            MainMapImage.Map.MapId = MapDBClass.GetNewMapId(form.MapName, form.MapComment);
            MainMapImage.IsOpened = true;
            SetToolBarStatus();
        }

        /// <summary>
        /// �������ڹرմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void measureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //����ͼ������
            MyMapEditImage.Visible = false;
            measureForm = null;
            MainMapImage.ActiveTool = MainMapImage.LastOption;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //if (_SelectObjectConfirm)
                //{
                //    if (projectAddForm != null && !projectAddForm.IsDisposed)
                //    {
                //        projectAddForm.Show();
                //        return;
                //    }
                //}
                //�����ǰ�ǲ���������ôȡ����������Ϣ����ʼ�µĲ���
                if (MainMapImage.ActiveTool == MapImage.Tools.MeasureLength)
                {
                }
                else if (MainMapImage.ActiveTool == MapImage.Tools.MeasureArea)
                {
                }
                if (MainMapImage == MainMapImage1)
                {
                    if (myToolTipControl1.Visible)
                    {
                        myToolTipControl1.Visible = false;
                        return;
                    }
                }
                else if (MainMapImage == MainMapImage2)
                {
                    if (myToolTipControl2.Visible)
                    {
                        myToolTipControl2.Visible = false;
                        return;
                    }
                }
                ClearSelectObject();
                MainMapImage.Refresh();
                SetToolForm();
                SetToolBarStatus();
            }
        }

        /// <summary>
        /// ͼ��ڵ㵯��ʽ�˵���ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerContextMenu_Opening(object sender, CancelEventArgs e)
        {
            MyMapEditImage.Visible = false;
            if (LayerContextMenu.Bottom > 676)
            {
                LayerContextMenu.Show((int)MousePosition.X, (int)MousePosition.Y - 170);
            }
            else
            {
                LayerContextMenu.Show(MousePosition);
            }
            SetToolBarStatus();
            ѡ��ToolStripMenuItem.Checked = SelecttoolStripButton1.Checked;
            �ƶ�ToolStripMenuItem.Checked = PanToolStripButton.Checked;
            ����ToolStripMenuItem.Checked = ZoomInModeToolStripButton.Checked;
            ZoomAreatoolStripMenuItem.Checked = ZoomAreatoolStripButton.Checked;
        }

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = LayerView.MySelectedNode;
            int index = node.PrevNode.Index;
            TreeNode newnode = (TreeNode)node.Clone();
            node.Parent.Nodes.Insert(index, newnode);
            LayerView.MySelectedNode = newnode;
            node.Remove();
            ResetLayerSort(newnode.Parent);
        }

        /// <summary>
        /// ���ƽڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = LayerView.MySelectedNode;
            int index = node.NextNode.Index;
            TreeNode newnode = (TreeNode)node.Clone();
            node.Parent.Nodes.Insert(index + 1, newnode);
            LayerView.MySelectedNode = newnode;
            node.Remove();
            ResetLayerSort(newnode.Parent);
        }

        /// <summary>
        /// ����¶��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            //����ͼ����ʾ
            MainMapImage.ActiveTool = MapImage.Tools.DefineArea;
            MyMapEditImage.MainMapImage = MainMapImage;
            MyMapEditImage.Initial();
            MyMapEditImage.BringToFront();
            MyMapEditImage.Visible = true;
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProblemAreaStripMenuItem_Click(object sender, MouseEventArgs e)
        {
            //ͼ��ѡ�д���ע��
            ToolStripButton btn = sender as ToolStripButton;
            foreach (ToolStripButton item in btnEditLayerList.DropDownItems)
            {
                item.Checked = false;
                if (item.Text == BOAT_LAYER_NAME)
                {
                    btn = item;
                }
            }
            btn.Checked = true;
            btnEditLayerList.Text = BOAT_LAYER_NAME;
            btnEditLayerList.Tag = btn.Tag;
            TreeNode node = FindNodeLayer(LayerView.Nodes[0], btn.Tag as ILayer);
            LayerView.SelectedNode = node;
            LayerView.MySelectedNode = node;
            btnEditLayer_Click(null, null);
            MyMapEditImage.Visible = false;
            //����ͼ����ʾ
            MainMapImage.ActiveTool = MapImage.Tools.DefineArea;
            MyMapEditImage.MainMapImage = MainMapImage;
            MyMapEditImage.Initial(SELECTION_TYPE.PROBLEMAREA); 
            MyMapEditImage.BringToFront();
            MyMapEditImage.Visible = true;
        }
        /// <summary>
        /// �����ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPolygonTempStripMenuItem_Click(object sender, MouseEventArgs e)
        {
            //ͼ��ѡ�о�Ԯ����ע��
            ToolStripButton btn = sender as ToolStripButton;
            foreach (ToolStripButton item in btnEditLayerList.DropDownItems)
            {
                item.Checked = false;
                if (item.Text == RESCUE_LAYER_NAME)
                {
                    btn = item;
                }
            }
            btn.Checked = true;
            btnEditLayerList.Text = RESCUE_LAYER_NAME;
            btnEditLayerList.Tag = btn.Tag;
            TreeNode node = FindNodeLayer(LayerView.Nodes[0], btn.Tag as ILayer);
            LayerView.SelectedNode = node;
            LayerView.MySelectedNode = node;
            btnEditLayer_Click(null, null);
            MyMapEditImage.Visible = false;
            //����ͼ����ʾ
            MainMapImage.ActiveTool = MapImage.Tools.DefineArea;
            MyMapEditImage.MainMapImage = MainMapImage;
            MyMapEditImage.Initial(SELECTION_TYPE.CIRCLETEMP);
            MyMapEditImage.BringToFront();
            MyMapEditImage.Visible = true;
        }
        /// <summary>
        /// ������ѵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProblemPointStripMenuItem_Click(object sender, MouseEventArgs e)
        {
            //ͼ��ѡ�д���ע��
            ToolStripButton btn = sender as ToolStripButton;
            foreach (ToolStripButton item in btnEditLayerList.DropDownItems)
            {
                item.Checked = false;
                if (item.Text == BOAT_LAYER_NAME)
                {
                    btn = item;
                }
            }
            btn.Checked = true;
            btnEditLayerList.Text = BOAT_LAYER_NAME;
            btnEditLayerList.Tag = btn.Tag;
            TreeNode node = FindNodeLayer(LayerView.Nodes[0], btn.Tag as ILayer);
            LayerView.SelectedNode = node;
            LayerView.MySelectedNode = node;
            btnEditLayer_Click(null, null);
            //������ѵ�
            MyMapEditImage.Visible = false;
            //����ͼ����ʾ
            MainMapImage.ActiveTool = MapImage.Tools.DefineArea;
            MyMapEditImage.MainMapImage = MainMapImage;
            MyMapEditImage.Initial(SELECTION_TYPE.PROBLEMPOINT);
            MyMapEditImage.BringToFront();
            MyMapEditImage.Visible = true;
        }
        /// <summary>
        /// ɾ����ǰѡ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            if (_EditLayer is VectorLayer)
            {
                GeometryProvider provider = ((VectorLayer)_EditLayer).DataSource as GeometryProvider;
                //����е�ǰѡ���������ͼ����ɾ��
                if (MainMapImage.SelectObjects.Count > 0)
                {
                    for (int i = MainMapImage.SelectObjects.Count - 1; i >= 0; i--)
                    {
                        Geometry geom = MainMapImage.SelectObjects[i];
                        if (MainMapImage.NeedSave && MainMapImage.Map.CurrentLayer.NeedSave)
                        {
                            MapDBClass.DeleteObject(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, geom.ID, MainMapImage.Map.CurrentLayer.Type);
                            Command.SendDeleteObjectCommand(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, geom.ID);
                        }
                        if (provider.Geometries.Contains(geom))
                        {
                            provider.Geometries.Remove(geom);
                        }
                        DeleteGeometryFromTree(geom);
                        MainMapImage.SelectObjects.Remove(geom);
                    }
                    //ǿ�Ƶ�ͼˢ��
                    MainMapImage.RequestFromServer = false;
                    MainMapImage.Refresh();
                    SetToolBarStatus();
                }
            }
        }

        /// <summary>
        /// ��ʾ���߸���Ԫ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            //������Դ���û�б���
            if (propertyFormInfo == null)
            {
                PolygonPropertyForm polygonPropertyForm = new PolygonPropertyForm(MainMapImage.Map.MapId);
                //polygonPropertyForm.Width = polygonPropertyForm.MinimumSize.Width;
                //polygonPropertyForm.Height = polygonPropertyForm.MinimumSize.Height + 100;
                polygonPropertyForm.Width = 285;
                polygonPropertyForm.Height = 400;
                polygonPropertyForm.AfterSave += new PolygonPropertyForm.AfterSaveEvent(polygonPropertyForm_AfterSave);
                polygonPropertyForm.SelectObject += FlashGeometry;
                polygonPropertyForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                propertyFormInfo = CreateDockInfo(polygonPropertyForm, POLYGONPROPERTYFORMGUID);
                propertyFormInfo.ShowContextMenuButton = true;
            }
            PolygonPropertyForm form = propertyFormInfo.DockableForm as PolygonPropertyForm;
            form.SelectObjects = MainMapImage.SelectObjects;
            form.SelectLayers = MainMapImage.SelectLayers;
            form.Initial(_EditLayer);
        }

        /// <summary>
        /// ���Դ��ڱ���󣬻���ˢ��
        /// </summary>
        /// <param name="obj"></param>
        void polygonPropertyForm_AfterSave(Geometry obj)
        {
            if (MainMapImage.NeedSave && MainMapImage.Map.CurrentLayer.NeedSave)
            {
                obj.LayerId = MainMapImage.Map.CurrentLayer.ID;
                MapDBClass.UpdateObject(MainMapImage.Map.MapId, obj);
            }
            MainMapImage.Refresh();
        }

        /// <summary>
        /// ��ʾͼƬ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //���ͼƬ��ʾ����û�б���
            if (pictureFormInfo == null)
            {
                ShowPictureForm showPictureForm = new ShowPictureForm(MainMapImage.Map.MapId);
                showPictureForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                pictureFormInfo = CreateDockInfo(showPictureForm, SHOWPICTUREFORMGUID);
                pictureFormInfo.ShowContextMenuButton = true;
                showPictureForm.Width = showPictureForm.MinimumSize.Width;
                showPictureForm.Height = showPictureForm.MinimumSize.Height;
                showPictureForm.SelectObjects = MainMapImage.SelectObjects;
                showPictureForm.SelectLayers = MainMapImage.SelectLayers;
                showPictureForm.SelectObject += FlashGeometry;
                //����ʾͼƬ����
                showPictureForm.Show();
            }
            ShowPictureForm form = pictureFormInfo.DockableForm as ShowPictureForm;
            form.Initial(_EditLayer);
        }

        /// <summary>
        /// ��ӵؼۼ��㴦��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPriceMotionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeoPoint point = new GeoPoint(MainMapImage.LastWorldPos.X, MainMapImage.LastWorldPos.Y);
            if (MainMapImage.Map.CurrentLayer is VectorLayer)
            {
                InputStringForm form = new InputStringForm(Resources.InputMotionPointTipMessage, "");
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
                GeometryProvider provider = (GeometryProvider)layer.DataSource;
                provider.Geometries.Add(point);
                point.IsAreaPriceMonitor = true;
                point.ID = MapDBClass.GetObjectId(MainMapImage.Map.MapId, layer.ID);
                point.Text = form.InputContext;
                if (MainMapImage.NeedSave && layer.NeedSave)
                {
                    MapDBClass.InsertObject(MainMapImage.Map.MapId, layer.ID, point);
                    Command.SendAddObjectCommand(MainMapImage.Map.MapId, layer.ID, point.ID);
                }
                MainMapImage.Refresh();
                AddGeometryToTree(point, point.Text);
            }
        }

        /// <summary>
        /// �����ݿ��м�����ͼ��Ϣ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            //��ʾ��ͼ�б�
            MapListForm form = new MapListForm(false);
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Open(form.MapId);
        }

        private void Open(decimal mapid)
        {

            if (MainMapImage.Map.MapId == mapid)
            {
                if (MessageBox.Show("ѡ��ĵ�ͼ�Ѿ��򿪡�ȷ��Ҫ���¼���ѡ��ĵ�ͼ��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
            }
            btnLayerView.Checked = true;
            btnLayerView_Click(null, null);
            CloseMapToolStripMenuItem_Click(null, null);
            LayerView.Enabled = false;
            OpenMapToolStripMenuItem.Enabled = false;
            OpenToolStripButton.Enabled = false;
            NewMapToolStripMenuItem.Enabled = false;
            NewToolStripButton.Enabled = false;
            MainMapImage.Reset();
            //axShockwaveFlash1.Visible = false;
            Command.MadeMap = null;
            _NeedDeleteOldTifInfo = true;
            for (int i = imageList1.Images.Count - 1; i > 0; i--)
            {
                imageList1.Images.RemoveAt(i);
            }
            //���Ŀǰͼ����
            LayerView.Nodes[0].Nodes.Clear();
            //���ͼ��
            MainMapImage.Map.Layers.Clear();
            //�򿪵�ͼ
            string mapname = MapDBClass.OpenMap(mapid);
            if (mapname.Trim() == "")
            {
                mapname = Resources.DefaultMapName;
            }
            LayerView.Nodes[0].Text = mapname;
            //waiting1.Visible = true;
            //waiting1.BringToFront();
            //Application.DoEvents();
            OpenMap(mapid);
            if (MainMapImage == MainMapImage1)
            {
                MainMapImage_MapZoomChanged(MainMapImage2, MainMapImage2.Map.Zoom);
            }
            else if (MainMapImage == MainMapImage2)
            {
                MainMapImage_MapZoomChanged(MainMapImage1, MainMapImage1.Map.Zoom);
            }

            DataTable table = MapDBClass.GetTifInformation(MainMapImage.Map.MapId);
            double width = 0;
            mapControl1.Visible = false;
            projectControl1.Initial(mapid);
            if (table != null && table.Rows.Count > 0)
            {
                //20150403��ʼ����Ӱ��ͼ
                //if (MessageBox.Show("�Ƿ��Ӱ��ͼ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //{
                //    btnLoadPicture.Checked = true;
                //    double minx = (double)table.Rows[0]["MinX"];
                //    double miny = (double)table.Rows[0]["MinY"];
                //    double maxx = (double)table.Rows[0]["MaxX"];
                //    double maxy = (double)table.Rows[0]["MaxY"];
                //    width = (int)table.Rows[0]["Width"];
                //    BoundingBox box = new BoundingBox(minx, miny, maxx, maxy);
                //    GdalRasterLayer layer = new GdalRasterLayer(box);
                //    MainMapImage.Map.Layers.Add(layer);
                //    MainMapImage.HaveTif = true;
                //    mapControl1.Visible = true;
                //}
                //else
                //{
                    btnLoadPicture.Checked = false;
                //}
            }
            SetToolBarStatus();
            ZoomToExtentsToolStripButton_Click(null, null);
            if (mapControl1.Visible)
            {
                _MaxZoom = (int)(width / Command.MadeMap.Width);
                _Seed = MainMapImage.Map.Zoom / (width / Command.MadeMap.Width);
                if (_MaxZoom > 32)
                {
                    MainMapImage.Map.Zoom = 32 * _Seed;
                    MainMapImage.Refresh();
                    _MaxZoom = 32;
                }
                else
                {
                    MainMapImage.Map.Zoom = _MaxZoom * _Seed;
                    MainMapImage.Refresh();
                }
                _ZoomList.Clear();
                double temp = _MaxZoom;
                while (temp >= 1)
                {
                    _ZoomList.Add(temp * _Seed);
                    temp /= 2;
                }
                temp = 1;
                int levelcount = TifData.GetLevelCount();
                for (int i = 0; i < levelcount; i++)
                {
                    for (int j = 1; j <= 6; j++)
                    {
                        _ZoomList.Add(_ZoomList[_ZoomList.Count - 1] / 2);
                    }
                }
                mapControl1.LevelCount = _ZoomList.Count - 1;
            }
            if (!btnLoadPicture.Checked)
            {
                btnLoadShiQu.Checked = true;
                btnLoadShiQu_Click(null, null);
            }
            LayerView.Enabled = true;
            OpenMapToolStripMenuItem.Enabled = true;
            OpenToolStripButton.Enabled = true;
            if (quanxianList.Contains("ɾ��ͼ��"))
            {
                NewMapToolStripMenuItem.Enabled = true;
            }
            NewToolStripButton.Enabled = true;
            DealParentChild();
            btnProjectView.Checked = true;
            toolStripMenuItem19_Click(null, null);
        }

        /// <summary>
        /// �����ͼ�����ݿ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            bool iscompare = MapDBClass.IsCompareMap;
            MapDBClass.IsCompareMap = false;
            //MapDBClass.SaveMap(MainMapImage1.Map.MapId);
            MapDBClass.IsCompareMap = iscompare;
        }

        /// <summary>
        /// ��ӡ��ǰ��ͼ��ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            InputStringForm form = new InputStringForm("���������", "");

            if (form.ShowDialog() == DialogResult.OK)
            {
                txt = form.InputContext;
            }
            else
            {
                return;
            }
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        /// <summary>
        /// �˳�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// �رյ�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage == MainMapImage1)
            {
                myToolTipControl1.Hide();
            }
            else
            {
                myToolTipControl2.Hide();
            }
            projectControl1.Clear();
            _EditLayer = null;
            btnEditLayerList.Tag = null;
            btnEditLayerList.Text = "ȡ���༭";
            MainMapImage.Map.Layers.Clear();
            MainMapImage.Map.MapId = 0;
            MainMapImage.SelectObjects.Clear();
            MainMapImage.SelectLayers.Clear();
            LayerView.MySelectedNode = null;
            LayerView.Nodes[0].Nodes.Clear();
            LayerView.Nodes[0].Text = Resources.NotOpenMapName;
            LayerView.Nodes[0].Checked = false;
            MainMapImage.IsOpened = false;
            MainMapImage.ClearSelectAll();
            CompareToolStripButton.Checked = false;
            CompareToolStripButton_Click(null, null);
            SetToolBarStatus();
            SetToolForm();
            projectControl1.Enabled = false;
            MainMapImage.Refresh();
            MainMapImage.Reset();
            _PriceTable.Clear();
            if (!MainMapImage1.IsOpened && !MainMapImage2.IsOpened)
            {
                //axShockwaveFlash1.Visible = true;
            }
        }

        /// <summary>
        /// ���Ƶ�ǰ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = String.Format(Resources.WorldPosFormat, MainMapImage.LastWorldPos.X, MainMapImage.LastWorldPos.Y);
            Clipboard.SetText(txt);
        }

        /// <summary>
        /// ɾ���ؼۼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePriceMotionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            if (MainMapImage.Map.CurrentLayer is VectorLayer)
            {
                //ȡ�õ�ǰͼ��
                VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
                //ȡ������Դ
                GeometryProvider provider = (GeometryProvider)layer.DataSource;
                //ɾ������
                for (int i = MainMapImage.SelectObjects.Count - 1; i >= 0; i--)
                {
                    if (MainMapImage.SelectObjects[i] is GeoPoint)
                    {
                        GeoPoint point = MainMapImage.SelectObjects[i] as GeoPoint;

                        if (provider.Geometries.Contains(point))
                        {
                            provider.Geometries.Remove(point);
                            //�����ݿ���ɾ������
                            if (MainMapImage.NeedSave && layer.NeedSave)
                            {
                                MapDBClass.DeleteObject(MainMapImage.Map.MapId, layer.ID, point.ID, MainMapImage.Map.CurrentLayer.Type);
                                Command.SendDeleteObjectCommand(MainMapImage.Map.MapId, layer.ID, point.ID);
                            }
                            TreeNode node = FindNode(LayerView.Nodes[0], point);
                            if (node != null)
                            {
                                node.Remove();
                            }
                            MainMapImage.SelectObjects.RemoveAt(i);
                        }
                    }
                }
                //��ͼ��auxin
                MainMapImage.Refresh();
                SetToolBarStatus();
            }
        }

        ///// <summary>
        ///// �򿪵ؼ۴���
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SalePriceToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MyMapEditImage.Visible = false;
        //    if (MainMapImage.SelectObjects.Count != 1)
        //    {
        //        return;
        //    }
        //    //������Դ���û�б���
        //    if (salePriceForm == null || salePriceForm.IsDisposed)
        //    {
        //        salePriceForm = new SalePriceForm();
        //        //��ʾ���Դ���
        //        salePriceForm.Show(this);
        //    }
        //    salePriceForm.AllowEdit = MainMapImage.Map.CurrentLayer.AllowEdit;
        //    salePriceForm.Initial(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, MainMapImage.SelectObjects[0].ID);
        //}
        //��pictureBox1��Paint�¼������        
        private void PrintPreviewToolStripMenuItem_Paint(object sender, PaintEventArgs e)        
        {           
            e.Graphics.DrawString("Hello World", this.Font, Brushes.Black, new PointF(10, 10));       
        } 
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string txt = null;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        { 
            //string txt = null;
            //if (MainMapImage.Map.CurrentLayer != null)
            //{
            //    txt = MainMapImage.Map.CurrentLayer.ToString();
            //}
            //else
            //{
            //    txt = MainMapImage.Map.Comment.ToString();
            //}
            Font font = new Font("", 20);
            SizeF size;
            size = e.Graphics.MeasureString(txt, font);
            e.Graphics.DrawString(txt, font, Brushes.Black, (e.PageBounds.Width - size.Width) / 2, 30);
            //if (e.MarginBounds.Width >= MainMapImage.Image.Width
            //    && e.MarginBounds.Height >= MainMapImage.Image.Height)
            //{
            //    e.Graphics.DrawImage(MainMapImage.Image, 0, 0);
            //}
            //else
            //{
            Rectangle srcrect = new Rectangle(0, 0, MainMapImage.Image.Width, MainMapImage.Image.Height);
            double ratex = (double)e.MarginBounds.Width / srcrect.Width;
            double ratey = (double)e.MarginBounds.Height / srcrect.Height;
            double rate = ratex;
            if (ratex > ratey)
            {
                rate = ratey;
            }
            //Bitmap saveBitmap = new Bitmap(1280, 1024);
            //g = Graphics.FromImage(saveBitmap);
                //Rectangle destrect = new Rectangle(e.MarginBounds.X, e.MarginBounds.Y + (srcrect.Height - e.MarginBounds.Height)*3/4, (int)(srcrect.Width * rate), (int)(srcrect.Height * rate));
            System.Drawing.Image imgSource = MainMapImage.Image;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // ����������
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            int destHeight = (int)e.PageBounds.Height;
            int destWidth = (int)e.PageBounds.Width-40;
            if (sHeight > destHeight-140 || sWidth > destWidth-40)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else if (sHeight > destHeight - 140)
                {
                    sW = destWidth;
                    sH = destHeight-140;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Size size1 = new System.Drawing.Size((int)(srcrect.Width), (int)(srcrect.Height));
            System.Drawing.Point point = new System.Drawing.Point(20, 70);
            Rectangle destrect = new Rectangle(point, size1);
            e.Graphics.DrawImage(MainMapImage.Image, new Rectangle((e.PageBounds.Width - sW) / 2, (e.PageBounds.Height - sH)*2 / 3, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            //}
        }

        /// <summary>
        /// ��ӡԤ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;

            InputStringForm form = new InputStringForm("���������", "");

            if (form.ShowDialog() == DialogResult.OK)
            {
                txt = form.InputContext;
            }
            else
            {
                return;
            }
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printPreviewDialog1.ShowDialog();
            }
        }

        /// <summary>
        /// ���Զ��崦��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyDefineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FieldDefineForm form = new FieldDefineForm();
            form.Show(this);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mesureLengthToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (measureForm == null || measureForm.IsDisposed)
            {
                //��ʼ����������
                measureForm = new MeasureForm();
                measureForm.MainMapImage = MainMapImage;
                measureForm.FormClosing += new FormClosingEventHandler(measureForm_FormClosing);
                measureForm.Show(this);
                //����ͼ����ʾ
                MyMapEditImage.MainMapImage = MainMapImage;
                MyMapEditImage.Initial();
                MyMapEditImage.BringToFront();
                MainMapImage.LastOption = MainMapImage.ActiveTool;
            }
            MyMapEditImage.Visible = true;
            MainMapImage.ActiveTool = MapImage.Tools.MeasureLength;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mesureAreaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (measureForm == null || measureForm.IsDisposed)
            {
                //��ʼ����������
                measureForm = new MeasureForm();
                measureForm.MainMapImage = MainMapImage;
                measureForm.FormClosing += new FormClosingEventHandler(measureForm_FormClosing);
                measureForm.Show(this);
                //����ͼ����ʾ
                MyMapEditImage.MainMapImage = MainMapImage;
                MyMapEditImage.Initial();
                MyMapEditImage.BringToFront();
                MyMapEditImage.Visible = true;
                MainMapImage.LastOption = MainMapImage.ActiveTool;
            }
            MainMapImage.ActiveTool = MapImage.Tools.MeasureArea;
        }

        /// <summary>
        /// ѡ��Ԫ��ģʽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SelecttoolStripButton1.Checked = true;
            MainMapImage1.ActiveTool = MapImage.Tools.Select;
            MainMapImage2.ActiveTool = MapImage.Tools.Select;
            PanToolStripButton.Checked = false;
            ZoomInModeToolStripButton.Checked = false;
            MainMapImage.LastOption = MainMapImage.ActiveTool;
            MyMapEditImage.Visible = false;
        } 
        
        /// <summary>
        /// ���궨λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XYtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            XYInputForm form = new XYInputForm();
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            EasyMap.Geometries.Point p1 = new EasyMap.Geometries.Point(form.XX, form.YY);
            if (!MainMapImage.Map.Envelope.Contains(p1))
            {
                MainMapImage.Map.Center = p1;
                MainMapImage.RequestFromServer = true;
                MainMapImage.Refresh();
            }
            EasyMap.Geometries.Point p2 = new EasyMap.Geometries.Point(form.XX + form.R, form.YY);
            PointF pp1 = MainMapImage.Map.WorldToImage(p1);
            PointF pp2 = MainMapImage.Map.WorldToImage(p2);
            int r = (int)Math.Abs(pp1.X - pp2.X);
            Rectangle rect = new Rectangle((int)pp1.X - r, (int)pp1.Y - r, r * 2, r * 2);
            MainMapImage.MyRefresh = true;
            Bitmap map = (Bitmap)MainMapImage.Image.Clone();
            Graphics g = Graphics.FromImage(map);
            g.DrawImage(Resources.xypoint, pp1.X - Resources.xypoint.Width / 2, pp1.Y - Resources.xypoint.Height / 2);
            g.DrawEllipse(Pens.Red, rect);
            g.Dispose();
            MainMapImage.Image = map;
            MainMapImage.Refresh();
            if (!MainMapImage.Map.Envelope.Contains(p1))
            {
                MainMapImage_MapCenterChanged(MainMapImage, MainMapImage.Map.Center);
            }
        }

        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer layer = GetCurrentLayer();
            VectorLayer vlayer = null;
            VectorLayer.LayerType layertype = VectorLayer.LayerType.PhotoLayer;
            if (layer is VectorLayer)
            {
                vlayer = layer as VectorLayer;
                layertype = vlayer.Type;
            }
            if (dbFormInfo == null)
            {
                DBForm dbform = new DBForm(MainMapImage.Map.MapId);
                dbform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                dbform.Width = dbform.MinimumSize.Width;
                dbform.Height = dbform.MinimumSize.Height + 100;
                dbFormInfo = CreateDockInfo(dbform, DBFORMGUID);
                dbform.SelectObjects = MainMapImage.SelectObjects;
                dbform.SelectLayers = MainMapImage.SelectLayers;
                if (!dbform.SelectLayers.Contains(layer))
                {
                    dbform.SelectLayers.Add(layer);
                }
                dbform.SelectObject += FlashGeometry;
                dbform.Initial(MainMapImage.Map.MapId);
            }
            DBForm form = dbFormInfo.DockableForm as DBForm;
        }

        /// <summary>
        /// ����Ԫ�ر�ѡ��
        /// </summary>
        /// <param name="layerId"></param>
        /// <param name="objectId"></param>
        void form_SelectObject(decimal layerId, decimal objectId)
        {
            TreeNode node = FindNodeById(LayerView.Nodes[0], layerId, objectId);
            if (node != null)
            {
                ClearSelectObject();
                LayerView.MySelectedNode = node;
                SelectObject(node.Tag as Geometry);
                SetToolForm();
                MainMapImage.Refresh();
            }
        }

        /// <summary>
        /// ����½�ͼ�㰴ť��չ��������ť�б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewRandomGeometryLayer_Click_1(object sender, EventArgs e)
        {
            AddNewRandomGeometryLayer.ShowDropDown();
        }

        /// <summary>
        /// �����ͼ�㰴ť��չ��������ť�б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLayerToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            AddLayerToolStripButton.ShowDropDown();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            MesuretoolStripSplitButton1.ShowDropDown();
        }

        /// <summary>
        /// ��������¼�����ݴ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputDatatoolStripButton1_Click(object sender, EventArgs e)
        {
            MyMapEditImage.Visible = false;
            if (inputGeometryForm == null || inputGeometryForm.IsDisposed)
            {
                inputGeometryForm = new InputGeometryForm();
                inputGeometryForm.AfterInput += new InputGeometryForm.AfterInputEvent(InputGeometryForm_AfterInput);
                inputGeometryForm.PickCoordinate += new InputGeometryForm.PickCoordinateEvent(inputGeometryForm_PickCoordinate);
                inputGeometryForm.Preview += new InputGeometryForm.PreviewEvent(inputGeometryForm_Preview);
                inputGeometryForm.FormClosed += new FormClosedEventHandler(inputGeometryForm_FormClosed);
            }
            inputGeometryForm.AllowEdit = GetCurrentLayer().AllowEdit;
            if (!inputGeometryForm.Visible)
            {
                inputGeometryForm.Show(this);
            }
            if (MainMapImage.SelectObjects.Count == 1)
            {
                inputGeometryForm.Initial(MainMapImage.SelectObjects[0]);
            }
            else
            {
                inputGeometryForm.Initial();
            }
        }

        void inputGeometryForm_Preview(Geometry newGeometry, Geometry oldGeometry)
        {
            foreach (ILayer layer in MainMapImage.Map.Layers)
            {
                if (layer is VectorLayer)
                {
                    VectorLayer vlayer = layer as VectorLayer;
                    if (vlayer.Enabled)
                    {
                        GeometryProvider provider = vlayer.DataSource as GeometryProvider;
                        if (oldGeometry != null)
                        {
                            provider.Geometries.Remove(oldGeometry);
                        }
                        if (newGeometry != null)
                        {
                            newGeometry.Select = true;
                            provider.Geometries.Add(newGeometry);
                        }
                        if ( newGeometry != null)
                        {
                            MainMapImage.Refresh();
                        }
                        return;
                    }
                }
            }
        }

        void inputGeometryForm_PickCoordinate(bool op)
        {
            MainMapImage.PickCoordinate = op;
        }

        /// <summary>
        /// ����¼�����ݴ��ڹر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void inputGeometryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            inputGeometryForm_PickCoordinate(false);
            //����¼�����ݴ������
            inputGeometryForm = null;
        }

        /// <summary>
        /// ����¼������Ԫ�ر���ʱ
        /// </summary>
        /// <param name="geomtry"></param>
        void InputGeometryForm_AfterInput(Geometry geomtry)
        {
            ILayer layer = GetCurrentLayer();
            if (layer is VectorLayer)
            {
                //�ǿ�˵������׷�ӵ�Ԫ�أ��������޸��ִ��Ԫ��
                if (geomtry != null)
                {
                    VectorLayer vlayer = (VectorLayer)layer;
                    GeometryProvider provider = vlayer.DataSource as GeometryProvider;
                    provider.Geometries.Add(geomtry);
                    AddGeometryToTree(geomtry, geomtry.Text);
                    geomtry.ID = MapDBClass.GetObjectId(MainMapImage.Map.MapId, layer.ID);
                    geomtry.LayerId = layer.ID;
                    MapDBClass.InsertObject(MainMapImage.Map.MapId, layer.ID, geomtry);
                }
                else
                {
                    Geometry obj = null;
                    if (MainMapImage.SelectObjects.Count == 1)
                    {
                        obj = MainMapImage.SelectObjects[0];
                    }
                    if (obj != null)
                    {
                        MapDBClass.UpdateObject(MainMapImage.Map.MapId, obj);
                    }

                }
                MainMapImage.Refresh();
            }
        }

        /// <summary>
        /// �����˳�ʱ��ѯ�ʱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Command.SendLogoutCommand();
            for (int i = 0; i < dockContainer1.Count; i++)
            {
                SaveDockStatus(dockContainer1.GetFormInfoAt(i));
            }
        }

        /// <summary>
        /// ��������ʾ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {

            int ret = Common.Check();
            if (ret == 2)
            {
                InputKeyForm inputKeyForm = new InputKeyForm();
                inputKeyForm.ShowDialog();
                Application.Exit();
            }
        }

        /// <summary>
        /// ��ӡҳ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printsetuptoolStripMenuItem14_Click(object sender, EventArgs e)
        {
            //��ʾ��ӡ���ô���
            pageSetupDialog1.ShowDialog();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��ʾ���ڴ���
            AboutForm form = new AboutForm();
            //testForm form = new testForm();
            form.Show(this);
        }

        /// <summary>
        /// ��ͼ�ȽϿ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompareToolStripButton_Click(object sender, EventArgs e)
        {
            //����ͼ��ȵ���
            MapPanel1.Width = Width - 15;
            MapPanel2.Width = MapPanel1.Width;
            //ȡ�ù�������߶�
            int h = toolStripContainer1.ClientRectangle.Height - menuStrip1.Height - MainToolStrip.Height - MainStatusStrip.Height;
            //�����ʼ�˵�ͼ�Ƚ�
            if (CompareToolStripButton.Checked)
            {
                //��������ͼ�ߴ�
                MapPanel1.Height = h / 2 - 5;
                MapPanel2.Top = MapPanel1.Bottom + 5;
                MapPanel2.Height = MapPanel1.Height;
                MapPanel2.Visible = true;
                if (MainMapImage2.IsOpened)
                {
                    Form form = new Form();
                    form.Text = "�Ƚ�ͼ��";
                    form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    form.Controls.Add(LayerView2);
                    LayerView2.Visible = true;
                    LayerView2.Dock = DockStyle.Fill;
                    form.Icon = this.Icon;
                    _LayerFormTempInfo = CreateDockInfo(form, LAYERGUID2);
                }
                //������ͼ���ĵ��Լ�����ͬ��ͼһ��
                //if (MainMapImage2.Map.Layers.Count > 0)
                //{
                //    MainMapImage2.Map.Center = MainMapImage1.Map.Center;
                //    MainMapImage2.Map.Zoom = MainMapImage1.Map.Zoom;
                //    MainMapImage2.Refresh();
                //}
                mapControl1.OtherMap = MainMapImage2;
                LayerView2.Focus();
                MainMapImage = MainMapImage2;
                picFlash = picFlash2;
                MapToolTip = myToolTipControl2;
                MyMapEditImage = MyMapEditImage2;
                MapDBClass.IsCompareMap = true;
                if (!MainMapImage2.IsOpened)
                {
                    MessageBox.Show("��ѡ��Ҫ�Ƚϵĵ�ͼ��");
                    OpenToolStripButton_Click(null, null);
                }
            }
            //����ر��˵�ͼ�Ƚϣ�������ͼ��ԭ
            else
            {
                LayerView2.Visible = false;
                this.Controls.Add(LayerView2);
                if (_LayerFormTempInfo != null)
                {
                    _LayerFormTempInfo.DockableForm.Close();
                }
                mapControl1.OtherMap = null;
                MapPanel2.Visible = false;
                MapPanel1.Height = h;
                MainMapImage.Refresh();
            }
        }

        /// <summary>
        /// �������ſ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomAreatoolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == ZoomAreatoolStripMenuItem)
            {
                ZoomAreatoolStripButton.Checked = !ZoomAreatoolStripButton.Checked;
            }
            if (ZoomAreatoolStripButton.Checked)
            {
                MyMapEditImage.Visible = false;
                //����ͼ����ʾ
                MainMapImage.ActiveTool = MapImage.Tools.ZoomArea;
                MyMapEditImage.MainMapImage = MainMapImage;
                MyMapEditImage.Initial();
                MyMapEditImage.BringToFront();
                MyMapEditImage.Visible = true;
            }
            else
            {
                MyMapEditImage.Visible = false;
                //����ͼ����ʾ
                MainMapImage.ActiveTool = MainMapImage.LastOption;
            }
        }
        public string formClick = null;
        private void MainForm_Resize(object sender, EventArgs e)
        {
            //    return;
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    formClick = "Minimized";
            //    //return;
            //}
            //else if (this.WindowState == FormWindowState.Maximized && (formClick == "Normal" || formClick == "Minimized"))
            //{
            //    formClick = "Maximized";
            //    MainMapImage.Refresh();
            //    //MainMapImage_MouseDown(sender, null,null);
            //    return;
            //}
            //else 
            //{
            //    formClick = "Normal";
            //����ͼ��ȵ���
            //ȡ�ù�������߶�
            int h = toolStripContainer1.ClientRectangle.Height - menuStrip1.Height - MainToolStrip.Height - MainStatusStrip.Height;
            //�����ʼ�˵�ͼ�Ƚ�
            if (CompareToolStripButton.Checked)
            {
                //��������ͼ�ߴ�
                MapPanel1.Height = h / 2 - 5;
                MapPanel2.Top = MapPanel1.Bottom + 5;
                MapPanel2.Height = MapPanel1.Height;
                MapPanel2.Visible = true;
                //������ͼ���ĵ��Լ�����ͬ��ͼһ��
                if (MainMapImage2.Map.Layers.Count > 0)
                {
                    MainMapImage2.Map.Center = MainMapImage1.Map.Center;
                    MainMapImage2.Map.Zoom = MainMapImage1.Map.Zoom;
                    MainMapImage2.Refresh();
                }
            }
            //����ر��˵�ͼ�Ƚϣ�������ͼ��ԭ
            else
            {
                MapPanel2.Visible = false;
                MapPanel1.Height = h;
                MainMapImage1.Refresh();
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            string sql = SqlHelper.GetSql("SelectTempSalePriceReport");
            sql = sql.Replace("@MapId", MainMapImage.Map.MapId.ToString());
            sql = sql.Replace("@LayerId", GetCurrentLayer().ID.ToString());
            sql = sql.Replace("@ObjectId", MainMapImage.SelectObjects[0].ID.ToString());
            sql = sql.Replace("@SaleDateMin", "'0'");
            sql = sql.Replace("@SaleDateMax", "'9999/99/99'");
            ReportForm form = new ReportForm();
            form.SQL = sql;
            form.Title = "����ؼ�����ͼ";
            form.XAxisColumnName = "��������";
            form.YAxisColumnName = "�ܼ�";
            form.GraphType = ReportForm.ReportGraphType.Bar;
            form.Show(this);
        }

        private void TifSplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TifSplitForm form = new TifSplitForm();
            form.Show(this);
        }

        #endregion

        #region - ��ͼ���ŵ����������¼� -

        /// <summary>
        /// ������ƿؼ�����Ŵ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapControl1_AreaZoom(object sender, EventArgs e)
        {
            ZoomAreatoolStripMenuItem.Checked = true;
            ZoomAreatoolStripButton_Click(ZoomAreatoolStripMenuItem, e);
        }

        /// <summary>
        /// ��ͼ���ƿؼ���ʾ��������¼�
        /// </summary>
        /// <param name="currentlevel"></param>
        /// <param name="newlevel"></param>
        private void mapControl1_ZoomChange(int currentlevel, int newlevel)
        {
            if (newlevel >= _ZoomList.Count)
            {
                return;
            }
            MainMapImage.Map.Zoom = _ZoomList[newlevel];
            MainMapImage.RequestFromServer = true;
            MainMapImage.Refresh();
            MainMapImage_MapZoomChanged(MainMapImage, _ZoomList[newlevel]);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMapControlLevel()
        {
            if (!mapControl1.Visible || _ZoomList.Count <= 0)
            {
                return;
            }
            double minzoom = Math.Abs(_ZoomList[0] - MainMapImage.Map.Zoom);
            double temp = 0;
            int level = 0;
            for (int i = 0; i < _ZoomList.Count; i++)
            {
                temp = Math.Abs(_ZoomList[i] - MainMapImage.Map.Zoom);
                if (temp < minzoom)
                {
                    minzoom = temp;
                    level = i;
                }
            }
            mapControl1.CurrentLevel = level;
        }

        #endregion

        #region - ��ͼ�������봦���¼� -

        /// <summary>
        /// ������Ұ�ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindArea_Click(object sender, EventArgs e)
        {
            if (FindAreaTextBox.Text.Trim() == "")
            {
                MessageBox.Show("�������ѯ���ơ�");
                return;
            }
            ClearSelectObject();
            //����Ӱ��ͼ�������������
            PhotoData data = TifData.FindByName(FindAreaTextBox.Text.Trim());
            if (data != null)
            {
                double centerx = (data.MinX + data.MaxX) / 2;
                double centery = (data.MinY + data.MaxY) / 2;

                MainMapImage.FindAreaList.Add(data);
                EasyMap.Geometries.Point p1 = new EasyMap.Geometries.Point(centerx, centery);
                if (!MainMapImage.Map.Envelope.Contains(p1))
                {
                    MainMapImage.Map.Center = p1;
                    MainMapImage.RequestFromServer = true;
                    MainMapImage_MapCenterChanged(MainMapImage, MainMapImage.Map.Center);
                }
                MainMapImage.Refresh();
                SaveFindAreaItem(FindAreaTextBox.Text.Trim());
                return;
            }
            //����Ԫ�ض������������
            string txt = FindAreaTextBox.Text.Trim();
            bool find = false;
            MainMapImage.FindAreaList.Clear();
            foreach (ILayer layer in MainMapImage.Map.Layers)
            {
                if (layer is VectorLayer)
                {
                    VectorLayer vlayer = layer as VectorLayer;
                    Collection<Geometry> geometries = new Collection<Geometry>();
                    GeometryProvider provider = vlayer.DataSource as GeometryProvider;

                    foreach (Geometry geom in provider.Geometries)
                    {
                        //if (geom.Text == txt)
                        if (System.Text.RegularExpressions.Regex.IsMatch(geom.Text, txt))
                        {
                            find = true;
                            data = new PhotoData();
                            BoundingBox box = geom.GetBoundingBox();
                            data.MinX = box.Min.X;
                            data.MinY = box.Min.Y;
                            data.MaxX = box.Max.X;
                            data.MaxY = box.Max.Y;
                            data.Name = geom.Text;
                            MainMapImage.SelectObjects.Add(geom);
                            //else
                            //{
                            MainMapImage.FindAreaList.Add(data);
                            //}
                            //if (!MainMapImage.Map.Envelope.Contains(geom.GetBoundingBox()))
                            //{
                            //    MainMapImage.Map.ZoomToBox(geom.GetBoundingBox());
                            //}

                            //TreeNode node = FindNode(layer.LayerName);
                            //LayerView.SelectedNode = node;
                        }
                    }
                }
            }
            if (!find)
            {
                MessageBox.Show("û�в�ѯ��ָ��λ�á�");
                return;
            }
            if (find)
            {
                BoundingBox box = new BoundingBox(MainMapImage.FindAreaList[0].MinX, MainMapImage.FindAreaList[0].MinY, MainMapImage.FindAreaList[0].MaxX, MainMapImage.FindAreaList[0].MaxY);
                for (int i = 1; i < MainMapImage.FindAreaList.Count; i++)
                {
                    if (box.Min.X > MainMapImage.FindAreaList[i].MinX)
                    {
                        box.Min.X = MainMapImage.FindAreaList[i].MinX;
                    }
                    if (box.Min.Y > MainMapImage.FindAreaList[i].MinY)
                    {
                        box.Min.Y = MainMapImage.FindAreaList[i].MinY;
                    }
                    if (box.Max.X < MainMapImage.FindAreaList[i].MaxX)
                    {
                        box.Max.X = MainMapImage.FindAreaList[i].MaxX;
                    }
                    if (box.Max.Y < MainMapImage.FindAreaList[i].MaxY)
                    {
                        box.Max.Y = MainMapImage.FindAreaList[i].MaxY;
                    }
                }

                double centerx = (box.Min.X + box.Max.X) / 2;
                double centery = (box.Min.Y + box.Max.Y) / 2;
                EasyMap.Geometries.Point p1 = new EasyMap.Geometries.Point(centerx, centery);
                //if (!MainMapImage.Map.Envelope.Contains(p1))
                //{
                MainMapImage.Map.Center = p1;
                for (int i = 0; i < MainMapImage.FindAreaList.Count; i++)
                {
                    BoundingBox box1 = new BoundingBox(MainMapImage.FindAreaList[0].MinX, MainMapImage.FindAreaList[0].MinY, MainMapImage.FindAreaList[0].MaxX, MainMapImage.FindAreaList[0].MaxY);
                    if (!MainMapImage.Map.Envelope.Contains(box1))
                    {
                        MainMapImage.Map.ZoomToBox(box);
                        break;
                    }
                }

                MainMapImage.RequestFromServer = true;
                MainMapImage_MapCenterChanged(MainMapImage, MainMapImage.Map.Center);
                //}
                MainMapImage.Refresh();
                SaveFindAreaItem(FindAreaTextBox.Text.Trim());
                SetToolBarStatus();
                SetToolForm();
            }
        }

        /// <summary>
        /// �������������س�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindAreaTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFindArea_Click(null, null);
            }
        }

        /// <summary>
        /// �����ѯ
        /// </summary>
        private void SaveFindAreaItem(string findarea)
        {
            if (!FindAreaTextBox.Items.Contains(findarea))
            {
                FindAreaTextBox.Items.Insert(0, findarea);
            }
            int maxcount = 10;
            Int32.TryParse(Common.IniReadValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem", "MaxCount"), out maxcount);
            if (maxcount <= 0)
            {
                maxcount = 10;
            }
            int count = FindAreaTextBox.Items.Count;
            if (count > maxcount)
            {
                count = maxcount;
            }
            while (FindAreaTextBox.Items.Count > count)
            {
                FindAreaTextBox.Items.RemoveAt(count);
            }
            Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem", "MaxCount", maxcount.ToString());
            Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem", "Count", count.ToString());

            for (int i = 0; i < count; i++)
            {
                Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem", "Item" + i, FindAreaTextBox.Items[i].ToString());
            }
        }

        #endregion

        #region - ��ͼ��ͼ�����¼� -

        /// <summary>
        /// ��ͼ��ʼ��ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapToImage_Click(object sender, EventArgs e)
        {
            label1.Width = 0;
            label1.Height = 0;
            label1.Visible = btnMapToImage.Checked;
            if (btnMapToImage.Checked)
            {
                //���浱ǰ����
                btnMapToImage.Tag = MainMapImage1.ActiveTool;
                MainMapImage1.ActiveTool = MapImage.Tools.None;
            }

        }

        /// <summary>
        /// ��ͼȡ����ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapToImageCancel_Click(object sender, EventArgs e)
        {
            menuStrip1.Enabled = true;
            MainToolStrip.Enabled = true;
            LayerContextMenu.Enabled = true;
            MapContextMenu.Enabled = true;
            label1.Visible = false;
            btnMapToImageCancel.Visible = false;
            btnMapToImageOk.Visible = false;
            btnMapToImage.Checked = false;
            MainToolStrip.Refresh();
            //�ָ�ԭ����
            MainMapImage1.ActiveTool = (MapImage.Tools)btnMapToImage.Tag;
        }

        /// <summary>
        /// ��ͼ���水ť����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapToImageOk_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            menuStrip1.Enabled = true;
            MainToolStrip.Enabled = true;
            LayerContextMenu.Enabled = true;
            MapContextMenu.Enabled = true;
            btnMapToImageCancel_Click(null, null);
            Bitmap map = new Bitmap(label1.Width, label1.Height);
            Graphics g = Graphics.FromImage(map);
            g.DrawImage(MainMapImage.Image, new RectangleF(0, 0, map.Width, map.Height), new Rectangle(label1.Left, label1.Top, label1.Width, label1.Height), GraphicsUnit.Pixel);
            g.Dispose();
            Common.SaveImage(saveFileDialog1.FileName, map);
            //�ָ�ԭ����
            MainMapImage1.ActiveTool = (MapImage.Tools)btnMapToImage.Tag;
        }

        /// <summary>
        /// ��ͼ��ߴ���ʱ����Ҫ���ı����ȡ����ťλ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Resize(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            MainToolStrip.Enabled = false;
            LayerContextMenu.Enabled = false;
            MapContextMenu.Enabled = false;
            btnMapToImageCancel.Top = label1.Bottom + 5;
            btnMapToImageCancel.Left = label1.Right - btnMapToImageCancel.Width;
            btnMapToImageOk.Top = btnMapToImageCancel.Top;
            btnMapToImageOk.Left = btnMapToImageCancel.Left - btnMapToImageOk.Width - 5;
        }

        #endregion

        #region - ��Ŀ�¼����� -

        /// <summary>
        /// �½���Ŀ�˵�����
        /// </summary>
        private void projectControl1_AddProject()
        {
            //if (projectAddForm == null || projectAddForm.IsDisposed)
            //{
            //    projectAddForm = new TuDiZhengSettings(user._userName);
            //    projectAddForm.Map = MainMapImage.Map;
            //    projectAddForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            //    //projectAddForm.ProjectChange += new ProjectAddForm.ProjectChangeEvent(projectAddForm_ProjectChange);
            //    projectAddForm.SelectObjectFromMap += new TuDiZhengSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            //    projectAddForm.SetPosition += new TuDiZhengSettings.SetPositionEvent(projectAddForm_SetPosition);

            //    projectAddForm.Show(this);
            //}
        }

        /// <summary>
        /// ��Ŀ����Ҫ�ӵ�ͼѡ���ڵ���Ϣ
        /// </summary>
        void projectAddForm_SelectObjectFromMap()
        {
            //ѡ��Ԫ��ȷ�ϱ�־��
            _SelectObjectConfirm = true;

        }

        /// <summary>
        /// ��Ŀ�޸���ϴ���
        /// </summary>
        /// <param name="ProjectData"></param>
        void projectAddForm_ProjectChange(ProjectData ProjectData)
        {
            //if (projectAddForm != null && !projectAddForm.IsDisposed)
            //{
            //    //��Ŀ�޸Ĵ���û��ȷ�����رպ󣬵�ͼΪ�˽���Ŀ�޸����ڵ�ͼ��ѡ���Ԫ�ص�ѡ��״̬�������Ҫ��ͼˢ��
            //    if (ProjectData == null)
            //    {
            //        MainMapImage.Refresh();
            //        return;
            //    }

            //    if (projectAddForm.IsModify)
            //    {
            //        projectControl1.ModifyProjectData(ProjectData);
            //    }
            //    else
            //    {
            //        projectControl1.AddProjectData(ProjectData);
            //    }
                //bool find = false;
                //foreach (ToolStripMenuItem button in btnShowPriceColor.DropDownItems)
                //{
                //    //����ؼ���ɫ��ע�򿪵Ļ�����Ҫִ����ɫ�ػ����
                //    if (button.Checked)
                //    {
                //        btnShowPriceColor_Click(button, null);
                //        find = true;
                //        break;
                //    }
                //}
                ////��Ŀ�޸Ĵ���ȷ�����رպ󣬵�ͼΪ�˽���Ŀ�޸����ڵ�ͼ��ѡ���Ԫ�ص�ѡ��״̬�������Ҫ��ͼˢ��
                //if (!find)
                //{
                //    MainMapImage.Refresh();
                //}
            //}
        }

        /// <summary>
        /// ��Ŀ�޸Ĳ˵�����
        /// </summary>
        /// <param name="projectData"></param>
        private void projectControl1_ModifyProject(ProjectData projectData)
        {
            //if (projectAddForm != null && !projectAddForm.IsDisposed)
            //{
            //    projectAddForm.Close();
            //}
            //if (projectAddForm == null || projectAddForm.IsDisposed)
            //{
            //    projectAddForm = new TuDiZhengSettings(user._userName);
            //    projectAddForm.Map = MainMapImage.Map;
            //    projectAddForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            //    //projectAddForm.ProjectChange += new ProjectAddForm.ProjectChangeEvent(projectAddForm_ProjectChange);
            //    projectAddForm.SelectObjectFromMap += new TuDiZhengSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            //    projectAddForm.SetPosition += new TuDiZhengSettings.SetPositionEvent(projectAddForm_SetPosition);
            //}
            ////projectAddForm.ProjectData = projectData;
            ////projectAddForm.IsModify = true;
            ////projectAddForm.Initial();
            //projectAddForm.Show(this);
        }

        /// <summary>
        /// Ԫ�ض�λ����
        /// </summary>
        /// <param name="layerId"></param>
        /// <param name="geom"></param>
        bool projectAddForm_SetPosition(decimal layerId, decimal geomId)
        {
            ClearSelectObject();
            MainMapImage.FindAreaList.Clear();
            for (int i = 0; i < MainMapImage.Map.Layers.Count; i++)
            {
                if (MainMapImage.Map.Layers[i].ID == layerId)
                {
                    Collection<Geometry> geometries = new Collection<Geometry>();
                    VectorLayer vlayer = MainMapImage.Map.Layers[i] as VectorLayer;
                    GeometryProvider provider = vlayer.DataSource as GeometryProvider;

                    Geometry destgeom = null;
                    foreach (Geometry geom in provider.Geometries)
                    {
                        if (geom.ID == geomId)
                        {
                            destgeom = geom;
                            break;
                        }
                    }
                    if (destgeom == null)
                    {
                        return false;
                    }
                    MainMapImage.SelectObjects.Add(destgeom);
                    BoundingBox box = destgeom.GetBoundingBox();
                    PhotoData data = new PhotoData();
                    data.MinX = box.Min.X;
                    data.MinY = box.Min.Y;
                    data.MaxX = box.Max.X;
                    data.MaxY = box.Max.Y;
                    data.Name = destgeom.Text;
                    MainMapImage.FindAreaList.Add(data);
                    double centerx = (box.Min.X + box.Max.X) / 2;
                    double centery = (box.Min.Y + box.Max.Y) / 2;
                    EasyMap.Geometries.Point p1 = new EasyMap.Geometries.Point(centerx, centery);
                    MainMapImage.Map.Center = p1;
                    if (!MainMapImage.Map.Envelope.Contains(box))
                    {
                        MainMapImage.Map.ZoomToBox(box);
                        break;
                    }
                    MainMapImage.RequestFromServer = true;
                    MainMapImage_MapCenterChanged(MainMapImage, MainMapImage.Map.Center);
                    MainMapImage.Refresh();
                    SaveFindAreaItem(FindAreaTextBox.Text.Trim());
                    SetToolBarStatus();
                    SetToolForm();
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region - �������¼����� -

        /// <summary>
        /// �����ߵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomtoolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ZoomtoolStripTextBox_SelectedIndexChanged(sender, null);
            }
        }

        /// <summary>
        /// �޶���������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomtoolStripTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        private void ZoomtoolStripTextBox_Enter(object sender, EventArgs e)
        {
            ZoomtoolStripTextBox.Text = ZoomtoolStripTextBox.Text.Replace(",", "").Replace("��", "").Replace("��", ":");
        }

        private void ZoomtoolStripTextBox_Leave(object sender, EventArgs e)
        {
            ZoomtoolStripTextBox.Text = string.Format("1:{0:N}", (int)MainMapImage.Map.Zoom).Replace(".00", "");
        }

        private void ZoomtoolStripTextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ZoomtoolStripTextBox.Text == "<�Զ�����б�...>")
            {
                ZoomListForm form = new ZoomListForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ZoomtoolStripTextBox.Items.Clear();
                    List<string> listitems = form.LoadZoom();
                    string[] items = new string[listitems.Count];
                    listitems.CopyTo(items);
                    ZoomtoolStripTextBox.Items.AddRange(items);
                }
                return;
            }

            double zoom = MainMapImage.Map.Zoom;

            string szoom = ZoomtoolStripTextBox.Text.Trim().Replace(",", "").Replace("��", ":");
            string[] list = szoom.Split(':');
            if (list.Length < 2)
            {
                double.TryParse(ZoomtoolStripTextBox.Text.Trim(), out zoom);
            }
            else
            {
                double zoom1, zoom2;
                double.TryParse(list[0], out zoom1);
                double.TryParse(list[1], out zoom2);
                if (zoom1 != 0)
                {
                    zoom = zoom2 / zoom1;
                }
            }
            if (zoom <= 0)
            {
                MessageBox.Show("���������ô�������������(���磺1:1000����1000)��");
                return;
            }
            if (zoom != MainMapImage.Map.Zoom)
            {
                MainMapImage.Map.Zoom = zoom;
                MainMapImage.RequestFromServer = true;
                MainMapImage.Refresh();
                MainMapImage_MapZoomChanged(MainMapImage, zoom);
            }
            ZoomtoolStripTextBox.Text = string.Format("1:{0:N}", (int)MainMapImage.Map.Zoom).Replace(".00", "");
        }

        #endregion

        #region - Timer�����¼� -

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left || Control.MouseButtons == MouseButtons.Right)
            {
                return;
            }
            timer1.Enabled = false;
            if (MainMapImage != null)
            {
                MainMapImage.RequestFromServer = true;
                MainMapImage.Refresh();
            }
        }

        /// <summary>
        /// �����н�����ˢ�´���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            waiting1.Refresh();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            exec.Execute(MainMapImage, LayerView);
            timer3.Enabled = true;
        }

        /// <summary>
        /// ���ڶ�ʱ��˸Ԫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (timer4.Tag == null)
            {
                picFlash.Visible = false;
                timer4.Tag = 0;
            }
            if ((int)timer4.Tag >= 5)
            {
                timer4.Enabled = false;
                return;
            }
            timer4.Tag = (int)timer4.Tag + 1;
            picFlash.Visible = (int)timer4.Tag % 2 == 0;
            myToolTipControl1.BringToFront();
            myToolTipControl2.BringToFront();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            //Command.SendGetDemo();
        }

        int id = 0;
        /// <summary>
        /// ���ڶ�ʱˢ�´�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer6_Tick(object sender, EventArgs e)
        {
            if (MainMapImage == null)
            {
                return;
            }
            if (id > 9)
            {
                id = 0;
            }

            //ȡ�õ�ǰͼ��
            VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            VectorLayer layer1 = (VectorLayer)MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME];
            VectorLayer layer2 = (VectorLayer)MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME];
            if (layer != null)
            {
                //����������Ϣ������ע�ǡ���Ԯ��������ע�ǡ���Ԯ�������˻�ע��ͼ��
                List<List<object>> list = MapDBClass.UpdateNewToBoat(id.ToString(), MainMapImage.Map.MapId, MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID,MainMapImage.Map);//����ֵ��һ������ע�ǵ㣬�ڶ�����ע�ǵ�
                List<object> listNew = list[0];//��ע�ǵ�
                List<object> listOld = list[1];//��ע�ǵ�
                id++;
                if (listNew.Count > 0)
                {
                    //�����ע�ǵ�
                    for (int i = 0; i < listNew.Count; i++)
                    {
                        GeometryProvider provider=null;
                        List<object> list1 = (List<object>)listNew[i];//��ע�ǵ�
                        string type = (string)list1[0];//����
                        if (type == "����")
                        {
                            //ȡ��ͼ������Դ
                            provider = (GeometryProvider)layer.DataSource;
                        }
                        else if (type == "��Ԯ����")
                        {
                            provider = (GeometryProvider)layer1.DataSource;
                        }
                        else if (type == "��Ԯ���˻�")
                        {
                            provider = (GeometryProvider)layer2.DataSource;
                        }
                        //����µ�ע�ǵ�
                        EasyMap.Geometries.Point newPoint = DeserializeObject((byte[])list1[1]) as EasyMap.Geometries.Point;

                        EasyMap.Geometries.Point newArea = new Geometries.Point();
                        //newPoint.
                        //��ӵ�ǰ�����ע��
                        newArea = newPoint.Clone();
                        newArea.ID = newPoint.ID;
                        newArea.Text = newPoint.Text;
                        provider.Geometries.Add(newArea);
                        LoadSymbol();

                        //AddGeometryToTree(newArea, newArea.Text);
                        //ǿ�Ƶ�ͼˢ��
                        //MainMapImage.Refresh();
                        //MyMapEditImage.Refresh();
                        //MainMapImage_AfterRefresh(sender);
                    }
                    //ɾ����ע�ǵ�
                    for (int j = 0; j < listOld.Count; j++)
                    {
                        List<object> list1 = (List<object>)listOld[j];
                        //ȡ��ͼ������Դ
                        GeometryProvider provider = null;
                        for (int n = 1; n < list1.Count; n++)
                        {
                            string type = (string)list1[0];//����
                            if (type == "����")
                            {
                                //ȡ��ͼ������Դ
                                provider = (GeometryProvider)layer.DataSource;
                            }
                            else if (type == "��Ԯ����")
                            {
                                provider = (GeometryProvider)layer1.DataSource;
                            }
                            else if (type == "��Ԯ���˻�")
                            {
                                provider = (GeometryProvider)layer2.DataSource;
                            }
                            EasyMap.Geometries.Point oldPoint = DeserializeObject((byte[])list1[n]) as EasyMap.Geometries.Point;
                            if (provider.Geometries.Contains(oldPoint))
                            {
                                provider.Geometries.Remove(oldPoint);
                            }
                            DeleteGeometryFromTree(oldPoint);
                            MainMapImage.SelectObjects.Remove(oldPoint);
                            MainMapImage.RequestFromServer = false;
                            //ǿ�Ƶ�ͼˢ��
                            //MainMapImage.Refresh();
                            //MyMapEditImage.Refresh();
                            //MainMapImage_AfterRefresh(sender);
                            // MainMapImage.Map.Layers[BOAT_LAYER_NAME].
                        }
                    }
                    LoadSymbol();
                    //ǿ�Ƶ�ͼˢ��
                    MainMapImage.Refresh();
                    //MyMapEditImage.Refresh();
                    MainMapImage_AfterRefresh(sender);
                }
            }
        }

        /// <summary>
        /// �������л���Ķ��������ݣ�����Ԫ��
        /// </summary>
        /// <param name="pBytes"></param>
        /// <returns></returns>
        public object DeserializeObject(byte[] pBytes)
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
                //Common.ShowError(ex);
            }
            return null;
        }
        #endregion

        #endregion

        #region - ���� -

        /// <summary>
        /// ע��Shapeͼ����
        /// </summary>
        private void registerLayerFactories()
        {
            _layerFactoryCatalog[".shp"] = new ShapeFileLayerFactory();
        }

        /// <summary>
        /// ׷��ͼ��
        /// </summary>
        /// <param name="layer"></param>
        private void AddLayer(ILayer layer, TreeNode node)
        {
            //MainMapImage.Map.Layers.Add(layer);
            MainMapImage.Map.AddLayer(layer);
            user user = new user();
            string quanxian = user.quanxian;
            if (layer is VectorLayer)
            {
                VectorLayer vlayer = layer as VectorLayer;
                SetLayerStyle(vlayer, vlayer.Style.Fill, vlayer.Style.Outline, vlayer.Style.TextColor, vlayer.Style.TextFont, vlayer.Style.EnableOutline, vlayer.Style.HatchStyle, vlayer.Style.Line, vlayer.Style.Penstyle, node);
            }
            ToolStripButton btn = new ToolStripButton(layer.LayerName);
            btn.CheckOnClick = true;
            btn.Tag = layer;
            btn.Click += EditLayer_Click;
            //if (quanxian.Substring(quanxian.Length-2,2) != "�ֵ�" && quanxian != "����Ա")
            if (quanxian == "��������Ա")
            {
                btnEditLayerList.DropDownItems.Add(btn);
            }
            else if (!string.IsNullOrEmpty(node.Parent.Text))
            {
                if (node.Parent.Text == "�ڵ���Ϣͼ��")
                {
                    btnEditLayerList.DropDownItems.Add(btn);
                }
            }
        }

        private void EditLayer_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton btn = sender as ToolStripButton;
                foreach (ToolStripButton item in btnEditLayerList.DropDownItems)
                {
                    item.Checked = false;
                }
                btn.Checked = true;
                btnEditLayerList.Text = btn.Text;
                btnEditLayerList.Tag = btn.Tag;
                if (btn.Tag == null)
                {
                    if (_EditLayer != null && _EditLayer.AllowEdit)
                    {
                        _EditLayer.AllowEdit = false;
                        _EditLayer = null;
                        SetToolBarStatus();
                        SetToolForm();
                    }
                }
                else
                {
                    TreeNode node = FindNodeLayer(LayerView.Nodes[0], btn.Tag as ILayer);
                    LayerView.SelectedNode = node;
                    LayerView.MySelectedNode = node;
                    btnEditLayer_Click(null, null);
                }
            }
            else
            {
                foreach (ToolStripButton item in btnEditLayerList.DropDownItems)
                {
                    if (item.Checked)
                    {
                        EditLayer_Click(item, null);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// ��������ͼ���������
        /// </summary>
        private void ResetLayerSort(TreeNode parent)
        {
            //��Ҫ��������ͼ������
            List<decimal> layersort = new List<decimal>();
            int index = 0;
            foreach (TreeNode node in parent.Nodes)
            {
                if (node.Tag is VectorLayer)
                {
                    VectorLayer layer = (VectorLayer)node.Tag;
                    layer.SortNo = index;
                    index++;
                    layersort.Add(layer.ID);
                }
            }
            if (layersort.Count > 0)
            {
                if (MainMapImage.NeedSave)
                {
                    MapDBClass.UpdateLayerSort(MainMapImage.Map.MapId, layersort);
                }
            }
        }

        /// <summary>
        /// ���ò˵�������ʽ�˵��Լ�������״̬
        /// </summary>
        private void SetToolBarStatus()
        {
            //ȡ��ͼ������
            int layercount = MainMapImage.Map.Layers.Count;
            //ȡ���Ƿ�ѡ����ͼ��
            ILayer layer = GetCurrentLayer();
            bool needSave = layer != null;
            if (layer != null)
            {
                needSave = layer.NeedSave;
            }
            VectorLayer vlayer = null;
            VectorLayer.LayerType layertype = VectorLayer.LayerType.PhotoLayer;
            if (layer is VectorLayer)
            {
                vlayer = layer as VectorLayer;
                layertype = vlayer.Type;
            }
            bool selectlayer = layertype != VectorLayer.LayerType.PhotoLayer;

            #region - ��������ť״̬���� -

            if (quanxianList.Contains("�½�ͼ��"))
            {
                //�½�ͼ�㰴ť
                AddNewRandomGeometryLayer.Enabled = MainMapImage.IsOpened;
            }
            if (quanxianList.Contains("��ͼ��"))
            {
                //��ͼ�㰴ť
                AddLayerToolStripButton.Enabled = MainMapImage.IsOpened;
            }
            //ɾ��ͼ�㰴ť
            if (quanxianList.Contains("ɾ��ͼ��"))
            {
                RemoveLayerToolStripButton.Enabled = layer != null || (LayerView.MySelectedNode != null && LayerView.MySelectedNode.Tag is GdalRasterLayer);
            }
            if (quanxianList.Contains("�����ͼ"))
            {
                //�����ͼ��ť
                SaveToolStripButton.Enabled = MainMapImage.IsOpened;
            }
            if (quanxianList.Contains("��ӡ"))
            {
                //��ӡ��ͼ��ť
                PrintToolStripButton.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("������ͼ"))
            {
                //������ͼ��ť
                pic_out.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("ѡ��Ԫ��"))
            {
                //ѡ��Ԫ�ذ�ť
                SelecttoolStripButton1.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("ȫ����ʾ"))
            {
                //������ȫͼ��ť
                ZoomToExtentsToolStripButton.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("��С"))
            {
                //���Ű�ť
                ZoomInModeToolStripButton.Enabled = layercount > 0;
            } if (quanxianList.Contains("�Ŵ�"))
            {
                ZoomOutModeToolStripButton.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�ֲ�����"))
            {
                //����Ŵ�ť
                ZoomAreatoolStripButton.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�ƶ���ͼ"))
            {
                //�ƶ���ť
                PanToolStripButton.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("����"))
            {
                //������ť
                MesuretoolStripSplitButton1.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("������ͼ"))
            {
                //���԰�ť
                PropertytoolStripButton2.Enabled = MainMapImage.SelectObjects.Count > 0;
            } if (quanxianList.Contains("���궨λ"))
            {
                //�������밴ť
                InputDatatoolStripButton1.Enabled = layercount > 0 && GetCurrentLayer() is VectorLayer && (MainMapImage.SelectObjects.Count <= 1 && needSave);
                toolStripDropDownButton1.Enabled = layercount > 0 && GetCurrentLayer() is VectorLayer && (MainMapImage.SelectObjects.Count <= 1 && needSave);
            }
            //��ͼ�Ƚϰ�ť
            CompareToolStripButton.Enabled = MainMapImage.IsOpened;
            //����ť
            ReporttoolStripButton.Enabled = layertype == VectorLayer.LayerType.MotionLayer && MainMapImage.SelectObjects.Count == 1;
            //�����ѯ�����
            FindAreaTextBox.Enabled = MainMapImage.IsOpened;
            //�����ѯ��ť
            btnFindArea.Enabled = MainMapImage.IsOpened;
            //��Ԯ�Ӳ�ѯ�����
            FindSaveBoat.Enabled = MainMapImage.IsOpened;
            //��Ԯ�Ӳ�ѯ��ť
            btnFindSaveBoat.Enabled = MainMapImage.IsOpened;
            //��ͼ��ť
            //btnMapToImage.Enabled = MainMapImage.IsOpened;
            if (quanxianList.Contains("��˰�ʼ�����ɫ"))
            {
                //�ؿ���ɫ��ɫ��ť
                btnShowPriceColor.Enabled = MainMapImage.IsOpened;
            } if (quanxianList.Contains("�༭ͼ��"))
            {
                btnEditLayerList.Enabled = MainMapImage.IsOpened;
            }
            if (_EditLayer != null && _EditLayer.AllowEdit)
            {
                foreach (ToolStripItem item in btnEditLayerList.DropDownItems)
                {
                    ToolStripButton btn = item as ToolStripButton;
                    btn.Checked = false;
                    if (item.Tag == _EditLayer)
                    {
                        btn.Checked = true;
                        btnEditLayerList.Text = btn.Text;
                        btnEditLayerList.Tag = btn.Tag;
                    }
                }
            }
            //List<ILayer> layers = MainMapImage.SelectLayers;
            //if (layers.Count > 1 || layers.Count == 0) lblCurrentLayer.Text = "";
            //else lblCurrentLayer.Text = "��ǰͼ�㣺" + layers[0].LayerName;
            TreeNode nodeSelect = LayerView.MySelectedNode;
            if (nodeSelect == null) 
            {
                lblCurrentLayer.Text = "";
            }
            else if (nodeSelect.Text != null || nodeSelect.Text != "")
            {
                lblCurrentLayer.Text = "��ǰͼ�㣺" + nodeSelect.Text;
            }
            else
            {
                lblCurrentLayer.Text = "";
            }
            if (lblCurrentLayer.Text == null && MainMapImage.Map.CurrentLayer != null)
            {
                lblCurrentLayer.Text = "��ǰͼ�㣺" + MainMapImage.Map.CurrentLayer.LayerName;
            }
            btnLoadPicture.Enabled = MainMapImage.IsOpened;
            btnLoadShiQu.Enabled = MainMapImage.IsOpened;
            btnTax.Enabled = MainMapImage.IsOpened;
            
            if (quanxianList.Contains("ѡ�񹤾�"))
            {
                btnSelect.Enabled = MainMapImage.IsOpened;
            }
            if (quanxianList.Contains("���ݲ�ѯ"))
            {
                btnDataSearch.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count > 0;
            }
            #endregion

            #region - �˵�״̬���� -
            if (quanxianList.Contains("�����ͼ"))
            {
                //����˵�
                SaveMapToolStripMenuItem.Enabled = MainMapImage.IsOpened;
            }
            if (quanxianList.Contains("�رյ�ͼ"))
            {
                //�رղ˵�
                CloseMapToolStripMenuItem.Enabled = MainMapImage.IsOpened;
            }
            if (quanxianList.Contains("��ӡ����"))
            {
                printsetuptoolStripMenuItem14.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("��ӡԤ��"))
            {
                //��ӡԤ���˵�
                PrintPreviewToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("��ӡ"))
            {
                //��ӡ�˵�
                PrintToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�ƶ���ͼ"))
            {
                //�ƶ��˵�
                MoveToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("ѡ��Ԫ��"))
            {
                //ѡ��˵�
                chooseToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("��С"))
            {
                //���Ų˵�
                ZoomToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�Ŵ�"))
            {
                ZoomOutToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("ȫ����ʾ"))
            {
                //��ͼȫ��ʾ
                ZoomToExtentsToolStripMenuItem.Enabled = layercount > 0;
            }
            //ɾ��ͼ��˵�
            if (quanxianList.Contains("ɾ��ͼ��"))
            {
                DeleteLayerToolStripMenuItem1.Enabled = selectlayer;
            }
            if (quanxianList.Contains("ɾ������"))
            {
                //ɾ������˵�
                DeleteAreaToolStripMenuItem1.Enabled = MainMapImage.SelectObjects.Count > 0 && selectlayer && MainMapImage.IsOpened && _EditLayer != null;
            }
            if (quanxianList.Contains("�½�ͼ��"))
            {
                //�½�ͼ��˵�
                InsertLayerToolStripMenuItem.Enabled = MainMapImage.IsOpened;
            }
            if (quanxianList.Contains("��ͼ��"))
            {
                //��ͼ��˵�
                OpenToolStripMenuItem.Enabled = MainMapImage.IsOpened;
            } if (quanxianList.Contains("�½�����"))
            {
                //�½�����˵�
                InsertAreaToolStripMenuItem.Enabled = selectlayer && MainMapImage.IsOpened && _EditLayer != null;
            }
            //�����˵�
            Measure1ToolStripMenuItem.Enabled = layercount > 0;
            //�½��ؼۼ���
            InsertPricePointToolStripMenuItem.Enabled = selectlayer && layertype == VectorLayer.LayerType.MotionLayer && MainMapImage.IsOpened && _EditLayer != null;
            //ɾ���ؼۼ���
            DeletePricePointToolStripMenuItem1.Enabled = MainMapImage.SelectObjects.Count > 0 && _EditLayer != null;
            //�ؼ���ɫ����
            btnLayerView.Enabled = MainMapImage.IsOpened;
            btnProjectView.Enabled = MainMapImage.IsOpened;
            btnPropertyView.Enabled = MainMapImage.IsOpened;
            btnPictureView.Enabled = MainMapImage.IsOpened;
            btnDataView.Enabled = MainMapImage.IsOpened;
            //areaManagerMenu.Enabled = MainMapImage.IsOpened;
            //areaMarkMenu.Enabled = MainMapImage.IsOpened;

            foreach (ToolStripItem item in ReportToolStripMenuItem.DropDownItems)
            {
                item.Enabled = MainMapImage.IsOpened;
            }
            #endregion

            #region - ����ʽ�˵�״̬���� -

            //ȡ�õ�ǰ�ڵ�
            TreeNode node = LayerView.MySelectedNode;
            //����Ǹ��ڵ㣬�����������Լ�ɾ��ͼ��Ĳ�����ֹ
            if (!MainMapImage.IsOpened)
            {
                //ͼ������
                MoveUpToolStripMenuItem.Enabled = false;
                //ͼ������
                MoveDownToolStripMenuItem.Enabled = false;
                //ɾ��ͼ��
                RemoveLayerToolStripMenuItem.Enabled = false;
                RemoveLayerToolStripButton.Enabled = false;
                DeleteLayerToolStripMenuItem1.Enabled = false;
                //�½�ͼ��
                AddLayerToolStripMenuItem.Enabled = false;
                VisibleToolStripMenuItem.Enabled = false;
                user user = new user();
                if (user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա")
                {
                    btnEditLayer.Enabled = false;
                }
                else
                {
                    btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                }
            }
            else if (node != null)
            {
                //if (node.Text.IndexOf("�����ڵ�ͼ��") == 0 || node.Text.IndexOf("˰���ڵ�ͼ��") == 0)
                //{
                //    //shuiwuSetting.Visible = true;
                //    tudiSetting.Visible = true;
                //}
                //else
                //{
                //    shuiwuSetting.Visible = false;
                //    tudiSetting.Visible = false;
                //}
                if (node.Text == "�����ڵ�ͼ��" || node.Text == "˰���ڵ�ͼ��")
                {
                    //ɾ��ͼ��
                    //RemoveLayerToolStripMenuItem.Enabled = false;
                    //RemoveLayerToolStripButton.Enabled = false;
                    //DeleteLayerToolStripMenuItem1.Enabled = false;
                    //�½�ͼ��
                    AddLayerToolStripMenuItem.Enabled = true;
                    if (quanxianList.Contains("�ɼ�������"))
                    {
                        //�ɼ�������
                        VisibleToolStripMenuItem.Enabled = true;
                    }
                    //ɾ��ͼ�����
                    if (quanxianList.Contains("ɾ��ͼ��"))
                    {
                        RemoveLayerToolStripMenuItem.Enabled = RemoveLayerToolStripButton.Enabled;
                    } 
                    if (quanxianList.Contains("����"))
                    {
                        //��������һ���ڵ㣬�����Ʋ�����ֹ
                        MoveDownToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.LastNode;
                    }
                    if (quanxianList.Contains("����"))
                    {
                        //����ǵ�һ���ڵ㣬�����Ʋ�����ֹ
                        MoveUpToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.FirstNode;
                    }
                    //�½�ͼ��
                    AddLayerToolStripMenuItem.Enabled = true;
                    if (quanxianList.Contains("�ɼ�������"))
                    {
                        VisibleToolStripMenuItem.Enabled = layer != null;
                    }
                    user user = new user();
                    if ((user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա") && node.Text != "˰���ڵ�ͼ��")
                    {
                        btnEditLayer.Enabled = false;
                    }
                    else
                    {
                        btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                    };
                }
                else if (node.Parent != null)
                {
                    if (node.Parent.Text == Resources.PhotoLayer)
                    {
                        //ͼ������
                        MoveUpToolStripMenuItem.Enabled = false;
                        //ͼ������
                        MoveDownToolStripMenuItem.Enabled = false;
                        //ɾ��ͼ��
                        RemoveLayerToolStripMenuItem.Enabled = false;
                        RemoveLayerToolStripButton.Enabled = false;
                        DeleteLayerToolStripMenuItem1.Enabled = false;
                        //�½�ͼ��
                        AddLayerToolStripMenuItem.Enabled = false;
                        //�ɼ�������
                        VisibleToolStripMenuItem.Enabled = false;
                        user user = new user();
                        if ((user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա") && node.Text != "˰���ڵ�ͼ��")
                        {
                            btnEditLayer.Enabled = false;
                        }
                        else
                        {
                            btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                        }
                    }
                    else if (node.Parent.Text == Resources.BaseLayer)
                    {
                        if (quanxianList.Contains("����"))
                        {
                            //��������һ���ڵ㣬�����Ʋ�����ֹ
                            MoveDownToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.LastNode;
                        }
                        if (quanxianList.Contains("����"))
                        {
                            //����ǵ�һ���ڵ㣬�����Ʋ�����ֹ
                            MoveUpToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.FirstNode;
                        }
                        if (quanxianList.Contains("�½�ͼ��"))
                        {
                            //�½�ͼ��
                            AddLayerToolStripMenuItem.Enabled = true;
                        }
                        if (quanxianList.Contains("�ɼ�������"))
                        {
                            //�ɼ�������
                            VisibleToolStripMenuItem.Enabled = true;
                        }
                        //ɾ��ͼ��
                        if (quanxianList.Contains("ɾ��ͼ��"))
                        {
                            RemoveLayerToolStripMenuItem.Enabled = RemoveLayerToolStripButton.Enabled;
                        }
                        //RemoveLayerToolStripMenuItem.Enabled = false;
                        //RemoveLayerToolStripButton.Enabled = false;
                        //DeleteLayerToolStripMenuItem1.Enabled = false;
                        //ɾ��Ԫ��
                        //DeleteObjectToolStripMenuItem.Enabled = false;
                        user user = new user();
                        if ((user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա") && node.Text != "˰���ڵ�ͼ��")
                        {
                            btnEditLayer.Enabled = false;
                        }
                        else
                        {
                            btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                        }
                    }
                    else
                    {
                        if (quanxianList.Contains("�½�ͼ��"))
                        {
                            //�½�ͼ��
                            AddLayerToolStripMenuItem.Enabled = true;
                        }
                        if (quanxianList.Contains("�ɼ�������"))
                        {
                            //�ɼ�������
                            VisibleToolStripMenuItem.Enabled = true;
                        }
                        //ɾ��ͼ�����
                        if (quanxianList.Contains("ɾ��ͼ��"))
                        {
                            RemoveLayerToolStripMenuItem.Enabled = RemoveLayerToolStripButton.Enabled;
                        }
                        if (quanxianList.Contains("����"))
                        {
                            //��������һ���ڵ㣬�����Ʋ�����ֹ
                            MoveDownToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.LastNode;
                        }
                        if (quanxianList.Contains("����"))
                        {
                            //����ǵ�һ���ڵ㣬�����Ʋ�����ֹ
                            MoveUpToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.FirstNode;
                        }
                        if (quanxianList.Contains("�½�ͼ��"))
                        {
                            //�½�ͼ��
                            AddLayerToolStripMenuItem.Enabled = true;
                        }
                        if (quanxianList.Contains("�ɼ�������"))
                        {
                            VisibleToolStripMenuItem.Enabled = layer != null;
                        }
                        user user = new user();
                        if ((user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա") && node.Text != "˰���ڵ�ͼ��")
                        {
                            btnEditLayer.Enabled = false;
                        }
                        else
                        {
                            btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                        }
                    }
                }
                else
                {
                    //�½�ͼ��
                    AddLayerToolStripMenuItem.Enabled = true;
                    //�ɼ�������
                    VisibleToolStripMenuItem.Enabled = true;
                    //ɾ��ͼ�����
                    if (quanxianList.Contains("ɾ��ͼ��"))
                    {
                        RemoveLayerToolStripMenuItem.Enabled = RemoveLayerToolStripButton.Enabled;
                    }
                    //��������һ���ڵ㣬�����Ʋ�����ֹ
                    MoveDownToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.LastNode;
                    //����ǵ�һ���ڵ㣬�����Ʋ�����ֹ
                    MoveUpToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.FirstNode;
                    //�½�ͼ��
                    AddLayerToolStripMenuItem.Enabled = true;
                    VisibleToolStripMenuItem.Enabled = layer != null;
                    user user = new user();
                    if ((user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա"))
                    {
                        btnEditLayer.Enabled = false;
                    }
                    else
                    {
                        btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                    }
                }
            }
            else
            {
                //�½�ͼ��
                AddLayerToolStripMenuItem.Enabled = true;
                //�ɼ�������
                VisibleToolStripMenuItem.Enabled = true;
                //ɾ��ͼ�����
                if (quanxianList.Contains("ɾ��ͼ��"))
                {
                    RemoveLayerToolStripMenuItem.Enabled = RemoveLayerToolStripButton.Enabled;
                }
                //��������һ���ڵ㣬�����Ʋ�����ֹ
                MoveDownToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.LastNode;
                //����ǵ�һ���ڵ㣬�����Ʋ�����ֹ
                MoveUpToolStripMenuItem.Enabled = node != null && node.Tag != null && node != node.Parent.FirstNode;
                //�½�ͼ��
                AddLayerToolStripMenuItem.Enabled = true;
                VisibleToolStripMenuItem.Enabled = layer != null;
                user user = new user();
                if ((user.quanxian == "˰���û�" || user.quanxian == "˰���û�����Ա"))
                {
                    btnEditLayer.Enabled = false;
                }
                else
                {
                    btnEditLayer.Enabled = selectlayer && (_EditLayer == null || !_EditLayer.AllowEdit) && layertype != VectorLayer.LayerType.ReportLayer;
                }
            }
            //DeleteObjectToolStripMenuItem.Enabled = LayerView.MySelectedNode != null && LayerView.MySelectedNode.Tag is Geometry && layertype != VectorLayer.LayerType.ReportLayer;
            if (quanxianList.Contains("��������"))
            {
                CopyXYToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.Map.Layers.Count > 0;
            }
            if (quanxianList.Contains("����"))
            {
                MeasureToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.Map.Layers.Count > 0;
            }
            //AddPolygonToolStripMenuItem.Enabled = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            //AddPolygonToolStripMenuItem.Visible = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            �ֶ��滭ToolStripMenuItem.Enabled = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            �ֶ��滭ToolStripMenuItem.Visible = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            AddPolygonTempStripMenuItem.Enabled = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            AddPolygonTempStripMenuItem.Visible = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            DeletePolygonToolStripMenuItem.Enabled = MainMapImage.SelectObjects.Count > 0 && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            DeletePolygonToolStripMenuItem.Visible = selectlayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            AddPriceMotionToolStripMenuItem.Enabled = selectlayer && layertype == VectorLayer.LayerType.MotionLayer;
            AddPriceMotionToolStripMenuItem.Visible = selectlayer && layertype == VectorLayer.LayerType.MotionLayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            DeletePriceMotionToolStripMenuItem.Enabled = MainMapImage.SelectObjects.Count > 0;
            DeletePriceMotionToolStripMenuItem.Visible = selectlayer && layertype == VectorLayer.LayerType.MotionLayer && MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.CurrentLayer.AllowEdit;
            AddPriceMotionToolStripMenuItem.Visible = false;
            DeletePriceMotionToolStripMenuItem.Visible = false;
            if (quanxianList.Contains("����"))
            {
            PropertyToolStripMenuItem.Enabled = MainMapImage.SelectObjects.Count > 0;
            }
            PropertyToolStripMenuItem.Visible = MainMapImage.SelectObjects.Count > 0;
            if (quanxianList.Contains("��Ƭ"))
            {
                PictureToolStripMenuItem.Enabled = MainMapImage.SelectObjects.Count > 0;
            }
            PictureToolStripMenuItem.Visible = MainMapImage.SelectObjects.Count > 0;
            if (quanxianList.Contains("���ݲ�ѯ"))
            {
                SearchToolStripMenuItem.Enabled = LayerView.MySelectedNode != null && LayerView.MySelectedNode.Tag is VectorLayer && selectlayer && layertype != VectorLayer.LayerType.PhotoLayer && layertype != VectorLayer.LayerType.ReportLayer;
            }
            SearchToolStripMenuItem.Visible = SearchToolStripMenuItem.Enabled;

            if (quanxianList.Contains("ѡ��Ԫ��"))
            {
                ѡ��ToolStripMenuItem.Enabled = layercount > 0;
            }
            //�ƶ�ToolStripMenuItem.Visible = true;
            //if (quanxianList.Contains("��С"))
            //{
            //    �ƶ�ToolStripMenuItem.Enabled = layercount > 0;
            //}
            if (quanxianList.Contains("��С"))
            {
                ����ToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�Ŵ�"))
            {
                �Ŵ�ToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("ȫͼ��ʾ"))
            {
                ȫͼ��ʾToolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�ֲ�����"))
            {
                ZoomAreatoolStripMenuItem.Enabled = layercount > 0;
            }
            //btnClearAreaPriceFill.Visible = true;
            if (quanxianList.Contains("����"))
            {
                XYtoolStripMenuItem.Enabled = layercount > 0;
            }
            if (quanxianList.Contains("�༭ͼ��"))
            {
                btnEditLayer.Enabled = selectlayer && (_EditLayer == null || LayerView.MySelectedNode.Text != _EditLayer.LayerName) && layertype != VectorLayer.LayerType.ReportLayer;
            }
            if (quanxianList.Contains("���ŵ�ͼ�㷶Χ"))
            {
                btnZoomToLayer.Enabled = selectlayer && layertype != VectorLayer.LayerType.ReportLayer;
            }
            if (quanxianList.Contains("��������ѡҪ��"))
            {
                btnZoomToSelectObjects.Enabled = MainMapImage.SelectObjects.Count > 0;
            }
            if (quanxianList.Contains("�����ѡҪ��"))
            {
                btnClearSelectObjects.Enabled = MainMapImage.SelectObjects.Count > 0;
            }
            if (quanxianList.Contains("�����ͼ�������"))
            {
                btnClearAreaPriceFill.Enabled = MainMapImage.IsOpened && _GeomPriceTable != null && _GeomPriceTable.Count > 0;
            }
             if (MainMapImage.Map.CurrentLayer != null)
             {
                 �켣ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == BOAT_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(BOAT_LAYER_NAME) == 0 || MainMapImage.Map.CurrentLayer.LayerName == RESCUE_BOAT_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_BOAT_LAYER_NAME) == 0 || MainMapImage.Map.CurrentLayer.LayerName == RESCUE_WURENJI_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_WURENJI_LAYER_NAME) == 0);
                
                ������ϢToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == BOAT_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(BOAT_LAYER_NAME) == 0 || MainMapImage.Map.CurrentLayer.LayerName == RESCUE_BOAT_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_BOAT_LAYER_NAME) == 0);
                �Ѿȶ���ϢToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == RESCUE_LAYER_NAME  || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_LAYER_NAME) == 0);
                �ɳ��ѾȶӴ���ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == RESCUE_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_LAYER_NAME) == 0 || MainMapImage.Map.CurrentLayer.LayerName == "���˻���Ԯע��" || MainMapImage.Map.CurrentLayer.LayerName.IndexOf("���˻���Ԯע��") == 0);
                ��Ԯ����ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == RESCUE_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_LAYER_NAME) == 0 || MainMapImage.Map.CurrentLayer.LayerName == "���˻���Ԯע��" || MainMapImage.Map.CurrentLayer.LayerName.IndexOf("���˻���Ԯע��") == 0);
                ����Ϊ���Ѵ���ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == BOAT_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(BOAT_LAYER_NAME) == 0);
                ���˻���Ԯ��ϢToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1 && (MainMapImage.Map.CurrentLayer.LayerName == RESCUE_WURENJI_LAYER_NAME || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_WURENJI_LAYER_NAME) == 0);
            }
            else
             {
                �켣ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
                ������ϢToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
                �Ѿȶ���ϢToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
                �ɳ��ѾȶӴ���ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
                ��Ԯ����ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
                ����Ϊ���Ѵ���ToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
                ���˻���Ԯ��ϢToolStripMenuItem.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count == 1 && MainMapImage.SelectLayers.Count >= 1;
            }
            if (quanxianList.Contains("ɾ����ʽ"))
            {
                btnDeleteObjectStyle.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count > 0;
            }
            if (quanxianList.Contains("������ʾ��ʽ"))
            {
                btnSetObjectStyle.Enabled = MainMapImage.IsOpened && MainMapImage.SelectObjects.Count > 0;
            }
            #endregion
            //setQuanxian();
        }

        /// <summary>
        /// ���ø����Ӵ�����ʾ����
        /// </summary>
        private void SetToolForm()
        {
            //if (salePriceForm != null && !salePriceForm.IsDisposed)
            //{
            //    if (MainMapImage.SelectObjects.Count == 1)
            //    {
            //        salePriceForm.Initial(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, MainMapImage.SelectObjects[0].ID);
            //    }
            //    else
            //    {
            //        salePriceForm.Initial();
            //    }
            //}
            if (pictureFormInfo != null)
            {
                ShowPictureForm showPictureForm = pictureFormInfo.DockableForm as ShowPictureForm;
                if (MainMapImage.SelectObjects.Count >= 1)
                {
                    showPictureForm.SelectObjects = MainMapImage.SelectObjects;
                    showPictureForm.SelectLayers = MainMapImage.SelectLayers;
                    showPictureForm.Initial(_EditLayer);
                }
                else
                {
                    showPictureForm.Initial(_EditLayer);
                }
            }
            if (propertyFormInfo != null)
            {
                PolygonPropertyForm polygonPropertyForm = propertyFormInfo.DockableForm as PolygonPropertyForm;
                if (MainMapImage.SelectObjects.Count >= 1)
                {
                    polygonPropertyForm.SelectObjects = MainMapImage.SelectObjects;
                    polygonPropertyForm.SelectLayers = MainMapImage.SelectLayers;
                    polygonPropertyForm.Initial(_EditLayer);
                }
                else
                {
                    polygonPropertyForm.Initial(_EditLayer);
                }
            }
            if (inputGeometryForm != null && !inputGeometryForm.IsDisposed)
            {

                if (MainMapImage.SelectObjects.Count == 1)
                {
                    inputGeometryForm.Initial(MainMapImage.SelectObjects[0]);
                }
                else
                {
                    inputGeometryForm.Initial();
                }
            }
            ILayer layer = GetCurrentLayer();
            VectorLayer vlayer = null;
            VectorLayer.LayerType layertype = VectorLayer.LayerType.PhotoLayer;
            if (layer is VectorLayer)
            {
                vlayer = layer as VectorLayer;
                layertype = vlayer.Type;
            }
            if (dbFormInfo != null)
            {
                DBForm dbform = dbFormInfo.DockableForm as DBForm;
                dbform.SelectObjects = MainMapImage.SelectObjects;
                dbform.SelectLayers = MainMapImage.SelectLayers;
                if (layer != null && !dbform.SelectLayers.Contains(layer))
                {
                    dbform.SelectLayers.Add(layer);
                }
                dbform.Initial(MainMapImage.Map.MapId);

            }
        }

        /// <summary>
        /// ���ѡ��Ԫ��
        /// </summary>
        private void ClearSelectObject()
        {
            MainMapImage.ClearSelectAll();
            MainMapImage.SelectLayers.Clear();
            lblCurrentLayer.Text = "";
        }

        /// <summary>
        /// ����ѡ��Ԫ��
        /// </summary>
        /// <param name="obj"></param>
        private void UnSelectObject(Geometry obj)
        {
            _LastSelectedGeometry = null;
            MainMapImage.SelectObjects.Remove(obj);
        }

        /// <summary>
        /// ����ѡ��Ԫ��
        /// </summary>
        /// <param name="obj"></param>
        private void SelectObject(Geometry obj)
        {
            EasyMap.Geometries.Point p1 = MainMapImage.Map.ImageToWorld(new PointF(0, 0));
            EasyMap.Geometries.Point p2 = MainMapImage.Map.ImageToWorld(new PointF(MainMapImage.Width, MainMapImage.Height));
            BoundingBox box = new BoundingBox(p1, p2);
            _LastSelectedGeometry = obj;
            MainMapImage.SelectObjects.Add(obj);
            //MainMapImage.SelectPolygon.ID = obj.ID;
            //MainMapImage.SelectPolygon.Select = true;
            BoundingBox box1 = obj.GetBoundingBox();
            if (!box.Contains(box1))
            {
                MainMapImage.Map.Center = new EasyMap.Geometries.Point(box1.Left + box1.Width / 2, box1.Top + box1.Height / 2);
            }
            if (!(obj is VectorLayer))
            {
                TreeNode node = FindNode(LayerView.Nodes[0], obj);
                if (node != null)
                {
                    LayerView.MySelectedNode = node;
                }
            }

        }

        /// <summary>
        /// ���Ԫ�ص�Ԫ����
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="text"></param>
        private void AddGeometryToTree(Geometry geometry, string text)
        {
            return;
            TreeNode node = new TreeNode(text);
            node.Checked = true;
            if (text == "")
            {
                node.Text = Resources.DefaultObjectName;
            }
            node.Tag = geometry;
            if (LayerView.MySelectedNode.Tag is VectorLayer)
            {
                if (text == "")
                {
                    node.Text = Resources.DefaultObjectName + (LayerView.MySelectedNode.Nodes.Count + 1);
                }
                LayerView.MySelectedNode.Nodes.Add(node);
            }
            else if (LayerView.MySelectedNode.Tag is Geometry)
            {
                if (text == "")
                {
                    node.Text = Resources.DefaultObjectName + (LayerView.MySelectedNode.Parent.Nodes.Count + 1);
                }
                LayerView.MySelectedNode.Parent.Nodes.Add(node);
            }
        }

        /// <summary>
        /// ��ͼ������ɾ��ָ����Ԫ�ض�Ӧ�Ľڵ�
        /// </summary>
        /// <param name="geometry"></param>
        private void DeleteGeometryFromTree(Geometry geometry)
        {
            TreeNode node = FindNode(LayerView.Nodes[0], geometry);
            if (node != null)
            {
                if (node.NextNode != null)
                {
                    LayerView.MySelectedNode = node.NextNode;
                }
                else if (node.PrevNode != null)
                {
                    LayerView.MySelectedNode = node.PrevNode;
                }
                else
                {
                    LayerView.MySelectedNode = node.Parent;
                }
                node.Remove();
            }
        }

        /// <summary>
        /// ȡ�õ�ǰͼ��
        /// </summary>
        /// <returns></returns>
        private ILayer GetCurrentLayer()
        {
            try
            {
                if (LayerView.MySelectedNode == null)
                {
                    return null;
                }
                if (LayerView.MySelectedNode.Tag is VectorLayer || LayerView.MySelectedNode.Tag is GdalRasterLayer)
                {
                    return LayerView.MySelectedNode.Tag as ILayer;
                }
                else if (LayerView.MySelectedNode != LayerView.Nodes[0] && LayerView.MySelectedNode.Tag is Geometry)
                {
                    return (VectorLayer)LayerView.MySelectedNode.Parent.Tag;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// ����ͼ����ʾ��ʽ
        /// </summary>
        /// <param name="layer"></param>
        private void SetLayerStyle(VectorLayer layer, SolidBrush fillBrush, Pen outLinePen, Color textColor, Font textFont, bool enableOutLine, int hatchStyle, Pen LinePen, int Penstyle, TreeNode node)
        {
            //if (node != null)
            //{
            //    if (node.Nodes != null)
            //    { 
            //        foreach(TreeNode subNode in node.Nodes)
            //        {
            //            subNode. = false;
            //        }
            //    }
            //}
            Common.SetLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutLine, hatchStyle, LinePen, Penstyle);
            Bitmap map = Common.DrawIcoByLayerStyle(layer, imageList1.ImageSize.Width, imageList1.ImageSize.Height);
            if (imageList1.Images.ContainsKey(layer.ID.ToString()))
            {
                imageList1.Images.RemoveByKey(layer.ID.ToString());

            }
            imageList1.Images.Add(layer.ID.ToString(), map);
            Common.SetNodeImage(LayerView.Nodes[0]);


        }

        private TreeNode FindNodeLayer(TreeNode node, ILayer layer)
        {
            TreeNode ret = null;
            if (node.Tag == layer)
            {
                return node;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                if (subnode.Tag == null || subnode.Tag is ILayer)
                {
                    ret = FindNodeLayer(subnode, layer);
                    if (ret != null)
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// ���ݽڵ����Ʋ��ҽڵ�
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private TreeNode FindNode(string text)
        {
            foreach (TreeNode node in LayerView.Nodes[0].Nodes)
            {
                if (text == Resources.BaseLayer || text == Resources.PhotoLayer || text == Resources.DataLayer)
                {
                    if (node.Text == text)
                    {
                        return node;
                    }
                }
                else
                {
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        if (subnode.Text == text)
                        {
                            return subnode;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// ���ո�����Ԫ�ز�ѯ�ڵ�
        /// </summary>
        /// <param name="node"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private TreeNode FindNode(TreeNode node, object geometry)
        {
            if (node.Tag == geometry)
            {
                return node;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                TreeNode findnode = FindNode(subnode, geometry);
                if (findnode != null)
                {
                    return findnode;
                }
            }
            return null;
        }

        /// <summary>
        /// ���ո�����Ԫ�ز�ѯ�ڵ�
        /// </summary>
        /// <param name="node"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private TreeNode FindNodeById(TreeNode node, decimal layerid, decimal id)
        {
            if (node.Tag != null)
            {
                if (node.Tag is Geometry)
                {
                    Geometry geomtry = node.Tag as Geometry;
                    if (geomtry.ID == id)
                    {
                        if (node.Parent.Tag is ILayer)
                        {
                            ILayer layer = node.Parent.Tag as ILayer;
                            if (layer.ID == layerid)
                            {
                                return node;
                            }
                        }
                    }
                }
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                TreeNode findnode = FindNodeById(subnode, layerid, id);
                if (findnode != null)
                {
                    return findnode;
                }
            }
            return null;
        }

        /// <summary>
        /// �����½�ͼ�����İ�ť������ȷ��ͼ������
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private VectorLayer.LayerType GetLayerTypeByText(string text)
        {
            VectorLayer.LayerType layertype = VectorLayer.LayerType.OtherLayer;
            if (text == Resources.PhotoLayer)
            {
                layertype = VectorLayer.LayerType.PhotoLayer;
            }
            else if (text == Resources.BaseLayer)
            {
                layertype = VectorLayer.LayerType.BaseLayer;
            }
            else if (text == Resources.ReportLayer)
            {
                layertype = VectorLayer.LayerType.ReportLayer;
            }
            else if (text == Resources.MotionPointLayer)
            {
                layertype = VectorLayer.LayerType.MotionLayer;
            }
            else if (text == Resources.SaleLayer)
            {
                layertype = VectorLayer.LayerType.SaleLayer;
            }
            else if (text == Resources.AreaInfoLayer)
            {
                layertype = VectorLayer.LayerType.AreaInformation;
            }
            else if (text == Resources.PriceLayer)
            {
                layertype = VectorLayer.LayerType.Pricelayer;
            }
            else if (text == Resources.RentLayer)
            {
                layertype = VectorLayer.LayerType.HireLayer;
            }
            else
            {
                layertype = VectorLayer.LayerType.OtherLayer;
            }
            return layertype;
        }

        /// <summary>
        /// �����ӽڵ��check״̬
        /// </summary>
        /// <param name="node"></param>
        private void CheckNode(TreeNode node)
        {
            if (node.Tag != null)
            {
                if (node.Tag is ILayer)
                {
                    ((ILayer)node.Tag).Enabled = node.Checked;
                }
                else if (node.Tag is Geometry)
                {
                    ((Geometry)node.Tag).Visible = node.Checked;
                }
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                subnode.Checked = node.Checked;
                CheckNode(subnode);
            }
        }

        /// <summary>
        /// ���ø��ڵ��check״̬
        /// </summary>
        /// <param name="node"></param>
        private void CheckParentNode(TreeNode node)
        {
            //����ڵ���Ԫ�صĻ�����ô���ڵ㲻����check
            if (node == null || node.Tag is Geometry)
            {
                return;
            }
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                bool flag = true;
                foreach (TreeNode subnode in parent.Nodes)
                {
                    if (!subnode.Checked)
                    {
                        flag = false;
                        break;
                    }
                }
                parent.Checked = flag;
                CheckParentNode(parent);
            }
        }

        /// <summary>
        /// ��ָ����ͼ
        /// </summary>
        /// <param name="mapid"></param>
        private void OpenMap(decimal mapid)
        {
            //ȡ�õ�ͼ��Ϣ
            DataTable maptable = MapDBClass.GetMapInfo(mapid);
            if (maptable == null || maptable.Rows.Count <= 0)
            {
                return;
            }
            ClearSelectObject();
            btnEditLayerList.DropDownItems.Clear();
            ToolStripButton btn = new ToolStripButton("ȡ���༭");
            btn.CheckOnClick = true;
            btn.Click += EditLayer_Click;
            btnEditLayerList.DropDownItems.Add(btn);
            //���õ�ͼ����

            string mapname = (string)maptable.Rows[0]["MapName"];
            if (mapname.Trim() == "")
            {
                mapname = Resources.DefaultMapName;
            }
            LayerView.Nodes[0].Text = mapname;
            MainMapImage.Map.MapId = (decimal)maptable.Rows[0]["MapId"];
            MainMapImage.Map.Comment = maptable.Rows[0]["Comment"].ToString();
            //PriceColorSettingForm form = new PriceColorSettingForm(MainMapImage.Map.MapId);
            //_PriceTable = form.PriceColorList;
            //ȡ��ͼ����Ϣ
            DataTable layertable = MapDBClass.GetLayerinfo(mapid);
            //waiting1.SetAutoProcess(false);
            //waiting1.Visible = true;
            //waiting1.MinProcessValue = 0;
            //waiting1.MaxProcessValue = layertable.Rows.Count;
            //waiting1.ProcessValue = 0;
            //ѭ��ͼ��
            for (int i = 0; i < layertable.Rows.Count; i++)
            {
                waiting1.SetAutoProcess(false);
                waiting1.Visible = true;
                waiting1.MinProcessValue = 0;
                waiting1.MaxProcessValue = layertable.Rows.Count;
                waiting1.ProcessValue = i;
                decimal layerid = (decimal)layertable.Rows[i]["LayerId"];
                string layername = layertable.Rows[i]["LayerName"].ToString();
                if (layername == "��·")
                {
                }
                waiting1.Tip = "���ڴ�ͼ�㣺" + layername;
                Application.DoEvents();
                int type = 0;
                Int32.TryParse(layertable.Rows[i]["LayerType"].ToString(), out type);
                VectorLayer layer = new VectorLayer(layername, (VectorLayer.LayerType)type);
                double maxvisible = (double)layertable.Rows[i]["MaxVisible"];
                double minvisible = (double)layertable.Rows[i]["MinVisible"];
                if (maxvisible != 0)
                {
                    layer.MaxVisible = maxvisible;
                }
                layer.MinVisible = minvisible;
                layer.ID = layerid;
                layer.Style.EnableOutline = layertable.Rows[i]["EnableOutline"].ToString() == "1" ? true : false;
                layer.Style.Fill = new SolidBrush(Color.FromArgb((int)layertable.Rows[i]["Fill"]));
                layer.Style.Line = new Pen(Color.Black);
                layer.Style.Penstyle = (int)layertable.Rows[i]["Penstyle"];
                if ((int)layertable.Rows[i]["Line"] >= 0 && (int)layertable.Rows[i]["Line"] <= 4)
                {
                    layer.Style.Line.DashStyle = (DashStyle)((int)layertable.Rows[i]["Line"]);
                }
                else
                {
                    layer.Style.Line.DashStyle = DashStyle.Solid;
                }
                layer.Style.Outline = new Pen(Color.FromArgb((int)layertable.Rows[i]["Outline"]), (int)layertable.Rows[i]["OutlineWidth"]);
                layer.Style.Outline.DashStyle = layer.Style.Line.DashStyle;
                int hatchbrush = -1;
                if (!int.TryParse(layertable.Rows[i]["hatchbrush"].ToString(), out hatchbrush))
                {
                    hatchbrush = -1;
                }
                layer.Style.HatchStyle = hatchbrush;
                int txtcolor = 0;
                if (int.TryParse(layertable.Rows[i]["TextColor"].ToString(), out txtcolor))
                {
                    layer.Style.TextColor = Color.FromArgb(txtcolor);
                }
                if (!string.IsNullOrEmpty(layertable.Rows[i]["TextFont"].ToString()))
                {
                    layer.Style.TextFont = (Font)Common.DeserializeObject((byte[])layertable.Rows[i]["TextFont"]);
                }
                Collection<Geometry> geometries = new Collection<Geometry>();
                GeometryProvider provider = new GeometryProvider(geometries);
                layer.DataSource = provider;
                TreeNode node = new TreeNode(layername);
                node.Tag = layer;
                node.Checked = true;

                //SetLayerStyle(layer, layer.Style.Fill, layer.Style.Line, layer.Style.Outline, layer.Style.EnableOutline, node);
                string layertext = "";
                switch (layer.Type)
                {
                    case VectorLayer.LayerType.BaseLayer:
                        layertext = Resources.BaseLayer;
                        break;
                    case VectorLayer.LayerType.PhotoLayer:
                        layertext = Resources.PhotoLayer;
                        break;
                    case VectorLayer.LayerType.ReportLayer:
                        layertext = Resources.ReportLayer;
                        break;
                    case VectorLayer.LayerType.MotionLayer:
                        layertext = Resources.MotionPointLayer;
                        break;
                    case VectorLayer.LayerType.SaleLayer:
                        layertext = Resources.SaleLayer;
                        break;
                    case VectorLayer.LayerType.AreaInformation:
                        layertext = Resources.AreaInfoLayer;
                        break;
                    case VectorLayer.LayerType.Pricelayer:
                        layertext = Resources.PriceLayer;
                        break;
                    case VectorLayer.LayerType.HireLayer:
                        layertext = Resources.RentLayer;
                        break;
                    case VectorLayer.LayerType.OtherLayer:
                        layertext = Resources.OtherLayer;
                        break;
                }
                TreeNode mainnode = new TreeNode(layertext);
                mainnode.Checked = true;
                TreeNode findnode = FindNode(layertext);
                if (findnode != null)
                {
                    mainnode = findnode;
                }
                else if (layertext == Resources.BaseLayer)
                {
                    if (LayerView.Nodes[0].Nodes.Count > 0)
                    {
                        if (LayerView.Nodes[0].Nodes[0].Text == Resources.PhotoLayer)
                        {
                            LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                        }
                        else
                        {
                            LayerView.Nodes[0].Nodes.Insert(0, mainnode);
                        }
                    }
                    else
                    {
                        LayerView.Nodes[0].Nodes.Add(mainnode);
                    }
                }
                else if (layertext == Resources.MotionPointLayer)
                {
                    findnode = FindNode(Resources.MotionPointLayer);
                    if (findnode == null)
                    {
                        findnode = new TreeNode(Resources.MotionPointLayer);
                        //LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                        LayerView.Nodes[0].Nodes.Add(findnode);
                    }
                    findnode.Nodes.Add(mainnode);
                }
                else if (layertext == Resources.PhotoLayer)
                {
                    findnode = FindNode(Resources.PhotoLayer);
                    if (findnode == null)
                    {
                        findnode = new TreeNode(Resources.PhotoLayer);
                        LayerView.Nodes[0].Nodes.Insert(1, mainnode);
                    }
                    //findnode.Nodes.Add(mainnode);
                }
                else
                {
                    findnode = FindNode(Resources.DataLayer);
                    if (findnode == null)
                    {
                        findnode = new TreeNode(Resources.DataLayer);
                        LayerView.Nodes[0].Nodes.Add(findnode);
                    }
                    findnode.Nodes.Add(mainnode);
                }
                mainnode.Nodes.Add(node);
                //LayerView.MySelectedNode = node;
                AddLayer(layer, node);
                //ȡ��Ԫ����Ϣ
                DataTable objecttable = MapDBClass.GetObject(mapid, layerid);
                for (int j = 0; j < objecttable.Rows.Count; j++)
                {
                    decimal objectid = (decimal)objecttable.Rows[j]["ObjectId"];
                    byte[] data = (byte[])objecttable.Rows[j]["ObjectData"];
                    Geometry geometry = (Geometry)Common.DeserializeObject(data);
                    geometry.LayerId = layer.ID;
                    geometry.ID = objectid;
                    geometry.Select = false;
                    //geometry.StyleType = string.IsNullOrEmpty(objecttable.Rows[j]["GeomType"].ToString())?0:int.Parse(objecttable.Rows[j]["GeomType"].ToString());
                    //if (geometry.StyleType == 1)
                    //{
                    //    geometry.EnableOutline = objecttable.Rows[j]["EnableOutline"].ToString() == "1" ? true : false;
                    //    geometry.Fill = (int)objecttable.Rows[j]["Fill"];
                    //    if ((int)objecttable.Rows[j]["Line"] >= 0 && (int)objecttable.Rows[j]["Line"] <= 4)
                    //    {
                    //        geometry.DashStyle = (int)objecttable.Rows[j]["Line"];
                    //    }
                    //    else
                    //    {
                    //        geometry.DashStyle = 0;
                    //    }
                    //    geometry.Outline = (int)objecttable.Rows[j]["Outline"];
                    //    geometry.OutlineWidth=(int)objecttable.Rows[j]["OutlineWidth"];
                    //    hatchbrush = -1;
                    //    if (!int.TryParse(objecttable.Rows[j]["hatchbrush"].ToString(), out hatchbrush))
                    //    {
                    //        hatchbrush = -1;
                    //    }
                    //    geometry.HatchStyle = hatchbrush;
                    //}
                    geometries.Add(geometry);
                    //List<SqlParameter> param = new List<SqlParameter>();
                    //param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                    //param.Add(new SqlParameter("layerid", layer.ID));
                    //param.Add(new SqlParameter("objectid", geometry.ID));
                    //param.Add(new SqlParameter("name", geometry.Text));
                    //SqlConnection conn = SqlHelper.GetConnection();
                    //conn.Open();
                    //SqlHelper.Update(conn, SqlHelper.GetSql("UpdateObjectText"), param);
                    //conn.Close();
                    //AddGeometryToTree(geometry, geometry.Text);
                }
            }
            waiting1.Visible = false;
            waiting1.SetAutoProcess(true);
            projectControl1.Initial(MainMapImage.Map.MapId);
            projectControl1.Enabled = true;
            LayerView.Nodes[0].Checked = true;
            //MainMapImage.Refresh();
            MainMapImage.IsOpened = true;
            if (MainMapImage == MainMapImage2)
            {
                Form _LayerFormTemp = new Form();
                _LayerFormTemp.Text = "�Ƚ�ͼ��";
                _LayerFormTemp.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                _LayerFormTemp.Controls.Add(LayerView2);
                LayerView2.Visible = true;
                LayerView2.Dock = DockStyle.Fill;
                _LayerFormTemp.Icon = this.Icon;
                _LayerFormTempInfo = CreateDockInfo(_LayerFormTemp, LAYERGUID2);
            }
            SetToolBarStatus();
            LoadSymbol();
            TreeNode taxnode = FindNode(Resources.MotionPointLayer);
            if (taxnode != null)
            {
                taxnode.Checked = false;
            }
            taxnode = FindNode(Resources.PhotoLayer);
            if (taxnode != null)
            {
                taxnode.Checked = false;
            }
            //TreeNode taxnode3 = FindNode("�����ڵ�ͼ���˰���һ��");
            //if (taxnode3 != null)
            //{
            //    taxnode3.Checked = false;
            //}
            //TreeNode taxnode1 = FindNode("�����ڵ�ͼ���˰�����һ��");
            //if (taxnode1 != null)
            //{
            //    taxnode1.Checked = false;
            //}
            //TreeNode taxnode2 = FindNode("�����ڵ�ͼ������˰����Ϣ");
            //if (taxnode2 != null)
            //{
            //    taxnode2.Checked = false;
            //}
            TreeNode taxnode1 = FindNode("�ڵ���Ϣͼ��");
            if (taxnode1 != null)
            {
                for (int i = 0; i < taxnode1.Nodes.Count; i++)
                {
                    if (taxnode1.Nodes[i].Text != "�����ڵ�ͼ��")
                        //&& taxnode1.Nodes[i].Text != "˰���ڵ�ͼ��")
                    {
                        taxnode1.Nodes[i].Checked = false;
                    }
                }
            }
            taxnode1 = FindNode("��ʱͼ��");
            if (taxnode1 != null)
            {
                taxnode1.Checked = false;
            }
        }

        /// <summary>
        /// ��װ�趨�Ķ�������򣬷��ظ������ڵĵؼۼ���
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private MyGeometryList SelectPoint(Polygon area)
        {
            MyGeometryList list = new MyGeometryList();
            ILayer layer = GetCurrentLayer();
            if (layer is VectorLayer)
            {
                VectorLayer vlayer = layer as VectorLayer;
                GeometryProvider provider = (GeometryProvider)vlayer.DataSource;
                EasyMap.Geometries.Point p1 = MainMapImage.Map.ImageToWorld(new PointF(0, 0));
                EasyMap.Geometries.Point p2 = MainMapImage.Map.ImageToWorld(new PointF(MainMapImage.Width, MainMapImage.Height));
                BoundingBox box = new BoundingBox(p1, p2);
                Collection<Geometry> geoms = provider.GetGeometriesInView(box);
                foreach (Geometry obj in geoms)
                {
                    if (obj is GeoPoint)
                    {
                        GeoPoint point = obj as GeoPoint;
                        if (point.IsAreaPriceMonitor)
                        {
                            if (area.InPoly(point))
                            {
                                list.Add(point);
                            }
                        }
                    }
                }
            }
            return list;
        }

        private void SetTip(string msg)
        {
            CoordinatesLabel.Text = msg;
        }

        #endregion

        /// <summary>
        /// ����ͼ��ɼ��Ե������С����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomVisibleSetForm form = new ZoomVisibleSetForm();
            ILayer layer = GetCurrentLayer();
            form.MaxVisible = layer.MaxVisible;
            form.MinVisible = layer.MinVisible;
            if (form.ShowDialog() == DialogResult.OK)
            {
                layer.MaxVisible = form.MaxVisible;
                layer.MinVisible = form.MinVisible;
                MainMapImage.Refresh();
                MapDBClass.UpdateLayerVisibleZoom(MainMapImage.Map.MapId, layer.ID, layer.MaxVisible, layer.MinVisible);
            }
        }

        /// <summary>
        /// �Զ��屨�������˵��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomReportMenuItem_Click(object sender, EventArgs e)
        {
            //ReportMakeForm form = new ReportMakeForm();
            //form.ShowDialog();
            CustomReportForm form = new CustomReportForm();
            form.Preview += new CustomReportForm.PreviewEvent(CustomReport_Preview);
            form.Show(this);
            LoadCustomReport();
        }

        /// <summary>
        /// �Զ��屨��Ԥ���¼�
        /// </summary>
        /// <param name="id"></param>
        void CustomReport_Preview(decimal id)
        {
            ToolStripMenuItem menu = new ToolStripMenuItem();
            menu.Tag = id;
            submenu_Click(menu, null);
        }

        /// <summary>
        /// �Զ��屨��˵��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void submenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            decimal id = (decimal)menu.Tag;
            string sql = SqlHelper.GetSql("SelectReportParameter");
            sql = sql.Replace("@Id", id.ToString());
            DataTable table = SqlHelper.Select(sql, null);
            sql = SqlHelper.GetSql("SelectReport");
            sql = sql.Replace("@Id", id.ToString());
            DataTable table1 = SqlHelper.Select(sql, null);
            if (table1 == null || table1.Rows.Count <= 0)
            {
                MessageBox.Show("�ñ����Ѿ���ɾ����");
                return;
            }
            string txt = table1.Rows[0]["ReportName"].ToString();
            sql = table1.Rows[0]["Sql"].ToString();
            string XColumn = table1.Rows[0]["XColumn"].ToString();
            if (table != null && table.Rows.Count > 0)
            {
                List<string> parametername = new List<string>();
                List<string> parameter = new List<string>();
                List<string> parametertype = new List<string>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    parametername.Add(table.Rows[i]["ParameterName"].ToString());
                    parameter.Add(table.Rows[i]["Parameter"].ToString());
                    parametertype.Add(table.Rows[i]["ParameterType"].ToString());
                }
                ConditionForm condition = new ConditionForm(parameter, parametername, parametertype);
                condition.Sql = sql;
                if (condition.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                sql = condition.Sql;
            }
            sql = sql.Replace("@MapId", MainMapImage.Map.MapId.ToString());
            if (GetCurrentLayer() != null)
            {
                sql = sql.Replace("@LayerId", GetCurrentLayer().ID.ToString());
            }
            else
            {
                sql = sql.Replace("@LayerId", "0");
            }
            if (_LastSelectedGeometry != null)
            {
                sql = sql.Replace("@ObjectId", _LastSelectedGeometry.ID.ToString());
            }
            else if (MainMapImage.SelectObjects.Count == 1)
            {
                sql = sql.Replace("@ObjectId", MainMapImage.SelectObjects[0].ID.ToString());
            }
            else
            {
                sql = sql.Replace("@ObjectId", "0");
            }
            ReportForm form = new ReportForm();
            form.SQL = sql;
            form.Title = txt;
            form.XAxisColumnName = XColumn;
            form.GraphType = ReportForm.ReportGraphType.Bar;
            form.Show(this);
        }

        /// <summary>
        /// ����ʾ�����ϵ��ȷ�ϻ���ȡ����ť����
        /// </summary>
        /// <param name="confirm"></param>
        private void myToolTipControl1_SelectConfirm(bool confirm)
        {
            //��ʾ���ڹر�
            MapToolTip.Hide();
            //ѡ��Ԫ��ȷ�ϱ�־�ر�
            _SelectObjectConfirm = false;
            //�����Ŀ���ڴ���
            //if (projectAddForm != null && !projectAddForm.IsDisposed)
            //{
            //    //��Ŀ�������ѡ���ڵش���
            //    if (confirm)
            //    {
            //        projectAddForm.SelectObjectFromMapDeal(myToolTipControl1.LayerId, myToolTipControl1.Geom, myToolTipControl1.ObjectName);
            //    }
            //    else
            //    {
            //        projectAddForm.SelectObjectFromMapDeal(0, null, null);
            //    }
            //}
            //else if (ShuiWuForm != null && !ShuiWuForm.IsDisposed)
            //{
            //    //��Ŀ�������ѡ���ڵش���
            //    if (confirm)
            //    {
            //        ShuiWuForm.SelectObjectFromMapDeal(myToolTipControl1.LayerId, myToolTipControl1.Geom, myToolTipControl1.ObjectName);
            //    }
            //    else
            //    {
            //        ShuiWuForm.SelectObjectFromMapDeal(0, null, null);
            //    }
            //}
        }

        /// <summary>
        /// �ڵ���Ϣ��ʾ�п�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPropertyControl_Click(object sender, EventArgs e)
        {
            TableControlForm form = new TableControlForm();
            form.ShowDialog();
        }

        /// <summary>
        /// ϵͳ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParameterSettings_Click(object sender, EventArgs e)
        {
            ParameterSettingForm form = new ParameterSettingForm();
            form.ShowDialog(this);
        }

        /// <summary>
        /// �ڵصؼ����ɫ�ػ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowPriceColor_Click(object sender, EventArgs e)
        {
            //�ؼ���ɫ���������
            _GeomPriceTable.Clear();
            //Ϊÿһ��ͼ�������ڵصؼ������
            foreach (ILayer layer in MainMapImage.Map.Layers)
            {
                if (layer is VectorLayer)
                {
                    ((VectorLayer)layer).GeometryColor.Clear();
                }
            }
            if (!btnShowPriceColor.Checked)
            {
                MainMapImage.Refresh();
                return;
            }
            ILayer ilayer = MainMapImage.Map.Layers[TAX_LAYER_NAME];
            string tablename = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, ilayer.ID);
            string sql = "select t1.color,t2.ObjectId from t_tax_level t1 inner join " + tablename + " t2 on t1.Level_Name=t2.SWJB and t1.MapId=t2.MapId where t1.MapId=" + MainMapImage.Map.MapId.ToString();
            DataTable table = SqlHelper.Select(sql, null);

            VectorLayer vlayer = ilayer as VectorLayer;
            GeometryProvider source = vlayer.DataSource as GeometryProvider;
            //�����ڵصؼ������
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    decimal objectid = (decimal)table.Rows[i]["ObjectId"];
                    Color color = Color.FromArgb((int)((decimal)table.Rows[i]["color"]));
                    foreach (Geometry geom in source.Geometries)
                    {
                        if (geom.ID == objectid)
                        {
                            vlayer.GeometryColor.Add(geom, color);
                            break;
                        }
                    }
                }
            }
            //��ͼ�ػ�
            MainMapImage.Refresh();
        }

        /// <summary>
        /// ����Զ��屨��˵�
        /// </summary>
        private void LoadCustomReport()
        {
            while (ReportToolStripMenuItem.DropDownItems.Count > _ReportCount)
            {
                ReportToolStripMenuItem.DropDownItems.RemoveAt(ReportToolStripMenuItem.DropDownItems.Count - 1);
            }
            string sql = SqlHelper.GetSql("SelectReportName");
            DataTable table = SqlHelper.Select(sql, null);
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string name = table.Rows[i]["ReportName"].ToString();
                    decimal id = (decimal)table.Rows[i]["Id"];
                    ToolStripMenuItem submenu = new ToolStripMenuItem(name);
                    submenu.Click += new EventHandler(submenu_Click);
                    submenu.Tag = id;
                    submenu.Enabled = MainMapImage.IsOpened;
                    ReportToolStripMenuItem.DropDownItems.Add(submenu);
                }
            }
        }

        /// <summary>
        /// �ݶ�ͼ��˫��ʱ�����ŵ���ͼ��ȫ����ʾ��Χ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerView1_DoubleClick(object sender, EventArgs e)
        {
            btnZoomToLayer_Click(sender, e);
        }

        /// <summary>
        /// �趨�༭ͼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditLayer_Click(object sender, EventArgs e)
        {
            _EditLayer = GetCurrentLayer();
            _EditLayer.AllowEdit = true;
            SetToolBarStatus();
            SetToolForm();
        }

        /// <summary>
        /// ���ػ���ȡ��Ӱ��ͼ��ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadPicture_Click(object sender, EventArgs e)
        {
            if (btnLoadPicture.Checked)
            {
                TreeNode node = FindNode(Resources.PhotoLayer);
                if (node != null)
                {
                    node.Checked = true;
                    node.Nodes[0].Checked = true;
                }
                if (!Command.IsConnect)
                {
                    Command.SendLoginCommand();
                    if (!Command.IsConnect)
                    {
                        btnLoadPicture.Checked = false;
                        MessageBox.Show("�������ӵ���������");
                        return;
                    }
                }
                if (btnLoadShiQu.Checked)
                {
                    btnLoadShiQu.Checked = false;
                    btnLoadShiQu_Click(null, null);
                }
                DataTable table = MapDBClass.GetTifInformation(MainMapImage.Map.MapId);
                double width = 0;
                mapControl1.Visible = false;
                projectControl1.Initial(MainMapImage.Map.MapId);
                if (table != null && table.Rows.Count > 0)
                {
                    double minx = (double)table.Rows[0]["MinX"];
                    double miny = (double)table.Rows[0]["MinY"];
                    double maxx = (double)table.Rows[0]["MaxX"];
                    double maxy = (double)table.Rows[0]["MaxY"];
                    width = (int)table.Rows[0]["Width"];
                    BoundingBox box = new BoundingBox(minx, miny, maxx, maxy);
                    GdalRasterLayer layer = new GdalRasterLayer(box);
                    MainMapImage.Map.Layers.Add(layer);
                    MainMapImage.HaveTif = true;
                    MainMapImage.ShowShiQu = false;
                    mapControl1.Visible = true;
                }
                SetToolBarStatus();
                //double width = 0;
                //ZoomToExtentsToolStripButton_Click(null, null);
                MyMapEditImage.Visible = false;
                if (MainMapImage1.Map.Layers.Count > 0)
                {
                    //MainMapImage1.Map.ZoomToExtents();
                    TifData.Map = MainMapImage1.Map;
                    List<Hashtable> table1 = TifData.TifFiles;
                    MainMapImage1.RequestFromServer = true;
                    MainMapImage1.Refresh();
                }
                if (MainMapImage2.Map.Layers.Count > 0)
                {
                    MainMapImage2.RequestFromServer = false;
                    //MainMapImage2.Map.ZoomToExtents();
                    MainMapImage2.Refresh();
                }

                //MainMapImage_MapZoomChanged(MainMapImage, MainMapImage.Map.Zoom);
                double zoomMain = Convert.ToDouble(zoomAll.Split(':')[1]); 
                if (mapControl1.Visible)
                {
                    //Command.MapMade = true;
                    //SendMessage(msg.ToString());
                    if (Command.MadeMap == null)
                    {
                        _MaxZoom = (int)width;
                        _Seed = zoomMain / width;
                    }
                    else
                    {
                        _MaxZoom = (int)(width / Command.MadeMap.Width);
                        _Seed = zoomMain / (width / Command.MadeMap.Width);
                    }
                    //_Seed = MainMapImage.Map.Zoom / (width / Command.MadeMap.Width);
                    if (_MaxZoom > 32)
                    {
                        //MainMapImage.Map.Zoom = 32 * _Seed;
                        mapControl1.Refresh();
                        _MaxZoom = 32;
                    }
                    else
                    {
                        //MainMapImage.Map.Zoom = _MaxZoom * _Seed;
                        mapControl1.Refresh();
                    }
                    _ZoomList.Clear();
                    double temp = _MaxZoom;
                    while (temp >= 1)
                    {
                        _ZoomList.Add(temp * _Seed);
                        temp /= 2;
                    }
                    temp = 1;
                    int levelcount = TifData.GetLevelCount();
                    for (int i = 0; i < levelcount; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            _ZoomList.Add(_ZoomList[_ZoomList.Count - 1] / 2);
                        }
                    }
                    mapControl1.LevelCount = _ZoomList.Count - 1;
                    //mapControl1.CurrentLevel = 5;
                }
                SetMapControlLevel();
                if (MainMapImage.HaveTif)
                {
                    PutTextColor();
                    MainMapImage.RequestFromServer = false;
                    MainMapImage.Refresh();
                    MainMapImage_MapZoomChanged(null, MainMapImage.Map.Zoom);
                }
            }
            else
            {
                RestoreTextColor();
                MainMapImage.HaveTif = false;
                mapControl1.Visible = false;
                TreeNode node = FindNode(Resources.PhotoLayer);
                if (node != null)
                {
                    node.Checked = false;
                    node.Nodes[0].Checked = false;
                }
                MainMapImage.Refresh();
            }
        }

        /// <summary>
        /// ���ŵ�ѡ��ͼ��ȫ����ʾ��Χ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZoomToLayer_Click(object sender, EventArgs e)
        {
            TreeNode node = LayerView.MySelectedNode;
            if (LayerView.MySelectedNode.Tag is VectorLayer)
            {
                MainMapImage.Map.CurrentLayer = (ILayer)LayerView.MySelectedNode.Tag;
                //ClearSelectObject();
                if (node.Checked)
                {
                    //if (MainMapImage.Map.Zoom < MainMapImage.Map.CurrentLayer.MinVisible
                    //    || MainMapImage.Map.Zoom > MainMapImage.Map.CurrentLayer.MaxVisible)
                    //{
                    BoundingBox box = MainMapImage.Map.CurrentLayer.Envelope;
                    if (box != null)
                    {
                        //MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible;
                        if (box.Width > 0 && box.Height > 0)
                        {
                            MainMapImage.Map.ZoomToBox(box);
                        }
                        if (MainMapImage.Map.Zoom > MainMapImage.Map.CurrentLayer.MaxVisible)
                        {
                            MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible - 1;
                        }
                        else if (MainMapImage.Map.Zoom < MainMapImage.Map.MinimumZoom)
                        {
                            MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible + 1;
                        }
                        ZoomtoolStripTextBox.Text = ((int)MainMapImage.Map.Zoom).ToString();
                        EasyMap.Geometries.Point center = new EasyMap.Geometries.Point();
                        center.X = (box.Left + box.Right) / 2;
                        center.Y = (box.Top + box.Bottom) / 2;
                        MainMapImage.Map.Center = center;
                        MainMapImage.RequestFromServer = true;
                        MainMapImage.Refresh();
                        MainMapImage_MapZoomChanged(MainMapImage, MainMapImage.Map.Zoom);
                    }
                    //}
                }
            }
        }

        /// <summary>
        /// Ԫ��ѡ�񹤾�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                foreach (ToolStripMenuItem submenu in btnSelect.DropDownItems)
                {
                    submenu.Checked = false;
                }
                item.Checked = true;
            }
            MyMapEditImage.Visible = false;
            //����ͼ����ʾ
            MainMapImage.ActiveTool = MapImage.Tools.SelectPoint;
            MyMapEditImage.MainMapImage = MainMapImage;
            if (btnRectangel.Checked)
            {
                btnSelect.Image = btnRectangel.Image;
                MyMapEditImage.Initial(SELECTION_TYPE.RECTANGLE);
            }
            else if (btnFreeCircle.Checked)
            {
                btnSelect.Image = btnFreeCircle.Image;
                MyMapEditImage.Initial(SELECTION_TYPE.CIRCLE);
            }
            else if (btnCircle.Checked)
            {
                btnSelect.Image = btnCircle.Image;
                MyMapEditImage.Initial(SELECTION_TYPE.CIRCLE_RADIO);
            }
            else if (btnFree.Checked)
            {
                btnSelect.Image = btnFree.Image;
                MyMapEditImage.Initial(SELECTION_TYPE.POLYGON);
            }
            else
            {
                MyMapEditImage.Initial(SELECTION_TYPE.NONE);
            }
            MyMapEditImage.BringToFront();
            MyMapEditImage.Visible = true;
        }

        private void btnDataSearch_Click(object sender, EventArgs e)
        {
            if (MainMapImage.SelectObjects.Count == 1)
            {
                DataSearchForm form = new DataSearchForm(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID, MainMapImage.SelectObjects[0].ID);
                form.SetMapAreaColor += form_SetMapAreaColor;
                form.ShowDialog(this);
                LoadCustomReport();
            }
            else
            {
                MultAreaDataSearchForm form = new MultAreaDataSearchForm(MainMapImage.Map.MapId, MainMapImage.SelectLayers, MainMapImage.SelectObjects);
                form.SetMapAreaColor += form_SetMapAreaColor;
                form.ShowDialog(this);
                LoadCustomReport();
            }
        }

        void form_SetMapAreaColor(List<PriceColorData> colors, List<AreaColor> datasource)
        {
            _PriceTable = colors;

            //�ؼ���ɫ���������
            _GeomPriceTable.Clear();

            //����۸������
            if (datasource != null)
            {
                foreach (AreaColor pricedata in datasource)
                {
                    foreach (PriceColorData data in _PriceTable)
                    {
                        if (pricedata.Number >= data.MinPrice && pricedata.Number <= data.MaxPrice)
                        {
                            string key = pricedata.LayerId.ToString() + "_" + pricedata.ObjectId.ToString();
                            _GeomPriceTable.Add(key, new SolidBrush(Color.FromArgb(data.Alhpa, data.FillColor)));
                            break;
                        }
                    }
                }
            }
            //Ϊÿһ��ͼ�����õؼ������
            foreach (ILayer layer in MainMapImage.Map.Layers)
            {
                if (layer is VectorLayer)
                {
                    ((VectorLayer)layer).PriceTable = _GeomPriceTable;
                }
            }
            //��ͼ�ػ�
            MainMapImage.Refresh();
        }

        /// <summary>
        /// ���ѡ��Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSelectObjects_Click(object sender, EventArgs e)
        {
            MainMapImage.SelectObjects.Clear();
            MainMapImage.Refresh();
            SetToolBarStatus();
            SetToolForm();
        }

        /// <summary>
        /// ��������ѡҪ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZoomToSelectObjects_Click(object sender, EventArgs e)
        {
            BoundingBox box = MainMapImage.SelectObjects[0].GetBoundingBox().Clone();
            double x1 = box.Left;
            double y1 = box.Top;
            double x2 = box.Right;
            double y2 = box.Bottom;
            foreach (Geometry geom in MainMapImage.SelectObjects)
            {
                BoundingBox box1 = geom.GetBoundingBox();
                x1 = Math.Min(box1.Left, x1);
                y1 = Math.Max(box1.Top, y1);
                x2 = Math.Max(box1.Right, x2);
                y2 = Math.Min(box1.Bottom, y2);
            }
            box = new BoundingBox(x1, y2, x2, y1);
            MainMapImage.Map.ZoomToBox(box);
            MainMapImage.RequestFromServer = true;
            MainMapImage.Refresh();
        }

        private void SaveDockStatus(DockableFormInfo info)
        {
            if (info == null)
            {
                return;
            }
            string inifile = "ClientSetting.ini";
            Common.IniWriteValue(inifile, info.Id.ToString(), "Dock", info.Dock.ToString());
            Common.IniWriteValue(inifile, info.Id.ToString(), "DockMode", info.DockMode.ToString());
            Common.IniWriteValue(inifile, info.Id.ToString(), "HostContainerDock", info.HostContainerDock.ToString());
            Common.IniWriteValue(inifile, info.Id.ToString(), "IsAutoHideMode", info.IsAutoHideMode ? "1" : "0");
        }

        private DockableFormInfo CreateDockInfo(Form form, string guid)
        {
            DockableFormInfo info = dockContainer1.Add(form, zAllowedDock.All, new Guid(guid));

            //string inifile = "ClientSetting.ini";
            //string sdock = Common.IniReadValue(inifile, info.Id.ToString(), "Dock");
            //string sDockMode = Common.IniReadValue(inifile, info.Id.ToString(), "DockMode");
            //string sHostContainerDock = Common.IniReadValue(inifile, info.Id.ToString(), "HostContainerDock");
            string sdock = "Top";
            string sDockMode = "Inner";
            string sHostContainerDock = "Left";
            DockStyle dock = sdock == "" ? DockStyle.Bottom : (DockStyle)Enum.Parse(DockStyle.Left.GetType(), sdock);
            zDockMode dockMode = sDockMode == "" ? zDockMode.Outer : (zDockMode)Enum.Parse(zDockMode.Inner.GetType(), sDockMode);
            DockStyle hostContainerDock = sHostContainerDock == "" ? DockStyle.Left : (DockStyle)Enum.Parse(DockStyle.Left.GetType(), sHostContainerDock);
            if (form.Text == "���ݲ�ѯ" || form.Text == "ͼƬ")
            {
                dock = DockStyle.Bottom;
                dockMode = zDockMode.Inner;
                hostContainerDock = DockStyle.Fill;
            } 
            if (form.Text == "����")
            {
                dock = DockStyle.None;
                dockMode = zDockMode.None;
                hostContainerDock = DockStyle.None;
            }
            if (guid == LAYERGUID || guid == LAYERGUID2 || guid == TAXFORMGUID)
            {
                dock = dock == DockStyle.None ? DockStyle.Left : dock;
                hostContainerDock = hostContainerDock == DockStyle.None ? DockStyle.Left : hostContainerDock;
                dockMode = dockMode == zDockMode.None ? zDockMode.Inner : dockMode;
            }
            bool find = false;
            if (hostContainerDock != dock)
            {
                for (int i = 0; i < dockContainer1.Count; i++)
                {
                    if (dockContainer1.GetFormInfoAt(i).HostContainerDock == hostContainerDock
                        || dockContainer1.GetFormInfoAt(i).AutoHideSavedDock == hostContainerDock)
                    {
                        find = true;
                        if (dockContainer1.GetFormInfoAt(i).IsAutoHideMode)
                        {
                            dockContainer1.SetAutoHide(dockContainer1.GetFormInfoAt(i), false);
                        }
                        dockContainer1.DockForm(info, dockContainer1.GetFormInfoAt(i), dock, dockMode);
                        break;
                    }
                }
            }
            if (!find && dock != DockStyle.None && hostContainerDock != DockStyle.None && dockMode != zDockMode.None)
            {
                dockContainer1.DockForm(info, hostContainerDock, zDockMode.Inner);
            }
            return info;
        }

        /// <summary>
        /// ���߲�����ر�ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockContainer1_FormClosing(object sender, DockableFormClosingEventArgs e)
        {
            DockableFormInfo info = dockContainer1.GetFormInfo(e.Form);
            SaveDockStatus(info);
            if (info == mapInfo)
            {
                e.Cancel = true;
            }
            else if (info == _LayerFormInfo)
            {
                LayerView1.Visible = false;
                this.Controls.Add(LayerView1);
                _LayerFormInfo = null;
                btnLayerView.Checked = false;
            }
            else if (info == _LayerFormTempInfo)
            {
                LayerView2.Visible = false;
                this.Controls.Add(LayerView2);
                _LayerFormTempInfo = null;
            }
            else if (info == _ProjectFormInfo)
            {
                projectControl1.Visible = false;
                this.Controls.Add(projectControl1);
                _ProjectFormInfo = null;
                btnProjectView.Checked = false;
            }
            else if (info == dbFormInfo)
            {
                dbFormInfo = null;
                btnDataView.Checked = false;
            }
            else if (info == pictureFormInfo)
            {
                pictureFormInfo = null;
                btnPictureView.Checked = false;
            }
            else if (info == propertyFormInfo)
            {
                propertyFormInfo = null;
                btnPropertyView.Checked = false;
            }
            info = null;
        }

        /// <summary>
        /// �򿪻��߹ر�ͼ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLayerView_Click(object sender, EventArgs e)
        {
            if (btnLayerView.Checked)
            {
                if (_LayerFormInfo == null)
                {
                    Form layerform = new Form();
                    layerform.Text = "ͼ��";
                    layerform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    layerform.Controls.Add(LayerView1);
                    LayerView1.Visible = true;
                    LayerView1.Dock = DockStyle.Fill;
                    layerform.Icon = this.Icon;
                    _LayerFormInfo = CreateDockInfo(layerform, LAYERGUID);
                }
                if (_LayerFormTempInfo == null)
                {
                    if (MainMapImage2.IsOpened)
                    {
                        Form form = new Form();
                        form.Text = "�Ƚ�ͼ��";
                        form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                        form.Controls.Add(LayerView2);
                        LayerView2.Visible = true;
                        LayerView2.Dock = DockStyle.Fill;
                        form.Icon = this.Icon;
                        _LayerFormTempInfo = CreateDockInfo(form, LAYERGUID2);
                    }
                }
            }
            else
            {
                if (_LayerFormInfo != null)
                {
                    _LayerFormInfo.DockableForm.Close();
                }
                if (_LayerFormTempInfo != null)
                {
                    _LayerFormTempInfo.DockableForm.Close();
                }
            }
        }

        /// <summary>
        /// �򿪻��߹ر���Ŀ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProjectView_Click(object sender, EventArgs e)
        {
            if (btnProjectView.Checked)
            {
                if (_ProjectFormInfo == null)
                {
                    Form projectform = new Form();
                    projectform.Text = "��Ŀ";
                    projectform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    projectform.Controls.Add(projectControl1);
                    projectControl1.Visible = true;
                    projectControl1.Dock = DockStyle.Fill;
                    projectform.Icon = this.Icon;
                    _ProjectFormInfo = CreateDockInfo(projectform, TAXFORMGUID);

                }
            }
            else
            {
                if (_ProjectFormInfo != null)
                {
                    _ProjectFormInfo.DockableForm.Close();
                }
            }
        }

        /// <summary>
        /// �򿪻��߹ر����Բ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPropertyView_Click(object sender, EventArgs e)
        {
            if (btnPropertyView.Checked)
            {
                if (propertyFormInfo == null)
                {
                    PropertyToolStripMenuItem_Click(null, null);
                }
            }
            else
            {
                if (propertyFormInfo != null)
                {
                    propertyFormInfo.DockableForm.Close();
                }
            }
        }

        /// <summary>
        /// �򿪻��߹ر�ͼƬ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPictureView_Click(object sender, EventArgs e)
        {
            if (btnPictureView.Checked)
            {
                if (pictureFormInfo == null)
                {
                    PictureToolStripMenuItem_Click(null, null);
                }
            }
            else
            {
                if (pictureFormInfo != null)
                {
                    pictureFormInfo.DockableForm.Close();
                }
            }
        }

        /// <summary>
        /// �򿪻��߹ر����ݲ�ѯ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDataView_Click(object sender, EventArgs e)
        {
            if (btnDataView.Checked)
            {
                if (dbFormInfo == null)
                {
                    SearchToolStripMenuItem_Click(null, null);
                }
            }
            else
            {
                if (dbFormInfo != null)
                {
                    dbFormInfo.DockableForm.Close();
                }
            }
        }

        /// <summary>
        /// ������˵���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockContainer1_ShowContextMenu(object sender, FormContextMenuEventArgs e)
        {
            DockableFormInfo info = dockContainer1.GetFormInfo(e.Form);
            if (info == dbFormInfo)
            {
                DBForm form = e.Form as DBForm;
                form.contextMenuStrip1.Show(e.Form, e.MenuLocation);
            }
            else if (info == mapInfo)
            {
                MapContextMenu.Show(e.Form, e.MenuLocation);
            }
            else if (info == _ProjectFormInfo)
            {
                projectControl1.contextMenuStrip1.Show(e.Form, e.MenuLocation);
            }
            else if (info == pictureFormInfo)
            {
                ShowPictureForm form = e.Form as ShowPictureForm;
                form.contextMenuStrip1.Show(e.Form, e.MenuLocation);
            }
            else if (info == propertyFormInfo)
            {
                PolygonPropertyForm form = e.Form as PolygonPropertyForm;
                form.contextMenuStrip1.Show(e.Form, e.MenuLocation);
            }
        }

        /// <summary>
        /// ��˸ѡ���Ԫ��
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="geom"></param>
        private void FlashGeometry(VectorLayer layer, Geometry geom)
        {
            BoundingBox geombox = geom.GetBoundingBox();
            EasyMap.Geometries.Point p1 = MainMapImage.Map.ImageToWorld(new PointF(0, 0));
            EasyMap.Geometries.Point p2 = MainMapImage.Map.ImageToWorld(new PointF(MainMapImage.Size.Width, MainMapImage.Size.Height));
            BoundingBox box = new BoundingBox(p1, p2);
            if (box.Contains(geombox))
            {
                PointF point1 = MainMapImage.Map.WorldToImage(new EasyMap.Geometries.Point(geombox.Left, geombox.Top));
                PointF point2 = MainMapImage.Map.WorldToImage(new EasyMap.Geometries.Point(geombox.Right, geombox.Bottom));
                float w = Math.Abs(point1.X - point2.X);
                float h = Math.Abs(point1.Y - point2.Y);
                picFlash.BackColor = Color.Transparent;
                picFlash.Width = (int)w;
                picFlash.Height = (int)h;
                picFlash.Left = (int)point1.X;
                picFlash.Top = (int)point1.Y;
                Bitmap map = new Bitmap(MainMapImage.Width, MainMapImage.Height);
                Graphics g = Graphics.FromImage(map);
                g.Clear(Color.Transparent);
                VectorStyle style = layer.Style.Clone();
                style.Outline.Color = Color.FromArgb(255 - style.Outline.Color.R, 255 - style.Outline.Color.G, 255 - style.Outline.Color.B);
                if (geom is Polygon)
                {
                    Polygon polygon = geom as Polygon;
                    g.DrawPolygon(style.Outline, polygon.ExteriorRing.TransformToImage(MainMapImage.Map));
                }
                else if (geom is MultiPolygon)
                {
                    MultiPolygon multiPolygon = geom as MultiPolygon;
                    for (int i = 0; i < multiPolygon.Polygons.Count; i++)
                    {
                        g.DrawPolygon(style.Outline, multiPolygon[i].ExteriorRing.TransformToImage(MainMapImage.Map));
                    }
                }
                else if (geom is LineString)
                {
                    LineString lineString = geom as LineString;
                    g.DrawLines(style.Outline, lineString.TransformToImage(MainMapImage.Map));
                }
                else if (geom is MultiLineString)
                {
                    MultiLineString MultiLineString = geom as MultiLineString;
                    for (int i = 0; i < MultiLineString.LineStrings.Count; i++)
                    {
                        g.DrawLines(style.Outline, MultiLineString[i].TransformToImage(MainMapImage.Map));
                    }
                }
                else if (geom is EasyMap.Geometries.Point)
                {
                    EasyMap.Geometries.Point point = geom as EasyMap.Geometries.Point;
                    PointF pp = MainMapImage.Map.WorldToImage(point);
                    Bitmap symbol = Resources.DiagramChangeToTargetClassic;
                    picFlash.Width = symbol.Width * 2;
                    picFlash.Height = symbol.Height * 2;
                    picFlash.Left = (int)point1.X - symbol.Width;
                    picFlash.Top = (int)point1.Y - symbol.Height;
                    g.DrawImageUnscaled(symbol, (int)(pp.X - symbol.Width / 2f),
                                    (int)(pp.Y - symbol.Height / 2f));
                }
                else if (geom is MultiPoint)
                {
                    MultiPoint points = geom as MultiPoint;
                    float minx = float.MaxValue;
                    float miny = float.MaxValue;
                    float maxx = float.MinValue;
                    float maxy = float.MinValue;
                    Bitmap symbol = Resources.DiagramChangeToTargetClassic;
                    for (int i = 0; i < points.Points.Count; i++)
                    {
                        EasyMap.Geometries.Point point = points[i];
                        PointF pp = MainMapImage.Map.WorldToImage(point);
                        g.DrawImageUnscaled(symbol, (int)(pp.X - symbol.Width / 2f),
                                        (int)(pp.Y - symbol.Height / 2f));
                        minx = Math.Min(pp.X, minx);
                        miny = Math.Min(pp.Y, miny);
                        maxx = Math.Max(pp.X, maxx);
                        maxy = Math.Max(pp.Y, maxy);
                    }

                    picFlash.Width = (int)Math.Abs(maxx - minx) + symbol.Width * 2;
                    picFlash.Height = (int)Math.Abs(maxy - miny) + symbol.Height * 2;
                    picFlash.Left = (int)Math.Min(maxx, minx) - symbol.Width;
                    picFlash.Top = (int)Math.Min(maxy, miny) - symbol.Height;
                }

                g.Dispose();
                if (picFlash.Width <= 0 || picFlash.Height <= 0)
                {
                    return;
                }
                Bitmap newmap = new Bitmap(picFlash.Width, picFlash.Height);
                g = Graphics.FromImage(newmap);
                g.DrawImage(MainMapImage.Image, new Rectangle(0, 0, picFlash.Width, picFlash.Height), new Rectangle(picFlash.Left, picFlash.Top, picFlash.Width, picFlash.Height), GraphicsUnit.Pixel);
                g.DrawImage(map, new Rectangle(0, 0, picFlash.Width, picFlash.Height), new Rectangle(picFlash.Left, picFlash.Top, picFlash.Width, picFlash.Height), GraphicsUnit.Pixel);
                g.Dispose();
                picFlash.Image = newmap;
                timer4.Tag = null;
                timer4.Interval = 300;
                timer4.Enabled = true;
            }
        }

        private void axShockwaveFlash1_VisibleChanged(object sender, EventArgs e)
        {
            //if (axShockwaveFlash1.Visible)
            //{
            //    axShockwaveFlash1.Play();
            //    axShockwaveFlash1.BringToFront();
            //}
            //else
            //{
            //    axShockwaveFlash1.Stop();
            //}
        }

        private void btnClearAreaPriceFill_Click(object sender, EventArgs e)
        {
            _GeomPriceTable.Clear();

            foreach (ILayer layer in MainMapImage.Map.Layers)
            {
                if (layer is VectorLayer)
                {
                    ((VectorLayer)layer).PriceTable = _GeomPriceTable;
                    ((VectorLayer)layer).GeometryColor.Clear();
                }
            }
            MainMapImage.Refresh();
        }

        private void areaInputMenu_Click(object sender, EventArgs e)
        {
            projectControl1_AddProject();
        }

        private void areaSearchMenu_Click(object sender, EventArgs e)
        {
            //fangZhengSettings_AddProject();
        }

        private void axShockwaveFlash1_MouseCaptureChanged(object sender, EventArgs e)
        {
            //axShockwaveFlash1.Visible = false;
            //userControl11.Visible = true;
            //userControl11.BringToFront();
            SqlConnection conn = null;
            SqlTransaction tran = null;
            DataTable table = new DataTable();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                conn = SqlHelper.GetConnection();
                conn.Open();
                param.Clear();
                string sql = SqlHelper.GetSql("SelectMapid");
                table = SqlHelper.Select(conn, tran, sql, param);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                MessageBox.Show(ex.Message);
            }
            Open(decimal.Parse(table.Rows[0]["MapId"].ToString()));
            //����ȫ��ͼ����
            zoomAll = ZoomtoolStripTextBox.Text;
            //����Ȩ��
            setQuanxian();
        }
        private void axShockwaveFlash1_MouseCaptureChanged()
        {
            //axShockwaveFlash1.Visible = false;
            //userControl11.Visible = true;
            //userControl11.BringToFront();
            SqlConnection conn = null;
            SqlTransaction tran = null;
            DataTable table = new DataTable();
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                conn = SqlHelper.GetConnection();
                conn.Open();
                param.Clear();
                string sql = SqlHelper.GetSql("SelectMapid");
                table = SqlHelper.Select(conn, tran, sql, param);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                MessageBox.Show(ex.Message);
            }
            Open(decimal.Parse(table.Rows[0]["MapId"].ToString()));
            //����Ȩ��
            setQuanxian();
        }
        private void userControl11_AreaClick()
        {
            decimal mapid;
            DataTable table = MapDBClass.GetMapList();
            if (decimal.TryParse(table.Rows[0]["mapid"].ToString(), out mapid))
            {
                userControl11.Visible = false;
                Open(mapid);
            }
        }

        private string searchDihao(decimal layerid, decimal objectid, string objectname)
        {
            string dihao = null;
            string sql = "select Ԥ���ڵغ� from " + string.Format("t_{0}_{1}", MainMapImage.Map.MapId, layerid);
            sql = sql + " where [MapId]=@mapid and [LayerId]=@layerid ";
            sql = sql + " and [ObjectId]=@geomid";
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
            param.Add(new SqlParameter("layerid", layerid));
            param.Add(new SqlParameter("geomid", objectid));
            DataTable table = SqlHelper.Select(sql, param);
            if (table.Rows.Count<1)
            {
                return null;
            }
            dihao = table.Rows[0]["Ԥ���ڵغ�"].ToString();
            return dihao;
        }
        private void InsertParentChild(string parent, string child)
        {
            VectorLayer parent_layer = MainMapImage.Map.Layers[parent] as VectorLayer;
            VectorLayer child_layer = MainMapImage.Map.Layers[child] as VectorLayer;
            GeometryProvider parent_provider = parent_layer.DataSource as GeometryProvider;
            GeometryProvider child_provider = child_layer.DataSource as GeometryProvider;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                List<SqlParameter> param = new List<SqlParameter>();
                string sql = SqlHelper.GetSql("InsertParentChild");
                tran = conn.BeginTransaction();
                double area = 0;
                foreach (Geometry geom in parent_provider.Geometries)
                {
                    foreach (Geometry subgeom in child_provider.Geometries)
                    {
                        if (GeometryUtils.IsIn(geom, subgeom, false, out area))
                        {
                            param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                            param.Add(new SqlParameter("layerid", parent_layer.ID));
                            param.Add(new SqlParameter("objectid", geom.ID));
                            param.Add(new SqlParameter("sublayerid", child_layer.ID));
                            param.Add(new SqlParameter("subobjectid", subgeom.ID));
                            param.Add(new SqlParameter("Area", area));
                            if (subgeom is Polygon) param.Add(new SqlParameter("TotalArea", ((Polygon)subgeom).Area));
                            else if (subgeom is MultiPolygon) param.Add(new SqlParameter("TotalArea", ((MultiPolygon)subgeom).Area));
                            else param.Add(new SqlParameter("TotalArea", 0));
                            SqlHelper.Insert(conn, tran, sql, param);
                            param.Clear();
                        }
                    }
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
            }
        }
        //���浱ǰ�ؿ��븸����ϵ�����ӣ������ؿ�˰�ʡ��µؿ������ĸ��ֵ���
        public void InsertParentChild(string parent, string child, Geometry subgeom)
        {
            VectorLayer parent_layer = MainMapImage.Map.Layers[parent] as VectorLayer;
            VectorLayer child_layer = MainMapImage.Map.Layers[child] as VectorLayer;
            GeometryProvider parent_provider = parent_layer.DataSource as GeometryProvider;
            GeometryProvider child_provider = child_layer.DataSource as GeometryProvider;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                List<SqlParameter> param = new List<SqlParameter>();
                string sql = SqlHelper.GetSql("InsertParentChild");
                tran = conn.BeginTransaction();
                double area = 0;
                foreach (Geometry geom in parent_provider.Geometries)
                {
                    //foreach (Geometry subgeom in child_provider.Geometries)
                    //{
                        if (GeometryUtils.IsIn(geom, subgeom, false, out area))
                        {
                            param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                            param.Add(new SqlParameter("layerid", parent_layer.ID));
                            param.Add(new SqlParameter("objectid", geom.ID));
                            param.Add(new SqlParameter("sublayerid", child_layer.ID));
                            param.Add(new SqlParameter("subobjectid", subgeom.ID));
                            param.Add(new SqlParameter("Area", area));
                            if (subgeom is Polygon) param.Add(new SqlParameter("TotalArea", ((Polygon)subgeom).Area));
                            else if (subgeom is MultiPolygon) param.Add(new SqlParameter("TotalArea", ((MultiPolygon)subgeom).Area));
                            else param.Add(new SqlParameter("TotalArea", 0));
                            SqlHelper.Insert(conn, tran, sql, param);
                            param.Clear();
                    }
                    //}
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
            }
        }

        private void InsertParentChild1(string parent, string child)
        {
            VectorLayer parent_layer = MainMapImage.Map.Layers[parent] as VectorLayer;
            VectorLayer child_layer = MainMapImage.Map.Layers[child] as VectorLayer;
            GeometryProvider parent_provider = parent_layer.DataSource as GeometryProvider;
            GeometryProvider child_provider = child_layer.DataSource as GeometryProvider;
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                List<SqlParameter> param = new List<SqlParameter>();
                string sql = SqlHelper.GetSql("InsertParentChild");
                tran = conn.BeginTransaction();
                double area = 0;
                foreach (Geometry geom in parent_provider.Geometries)
                {
                    foreach (Geometry subgeom in child_provider.Geometries)
                    {
                        if (GeometryUtils.IsIn(geom, subgeom, false, out area, MainMapImage.Map))
                        {
                            decimal TotalArea = 0;
                            param.Clear();
                            param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                            param.Add(new SqlParameter("layerid", parent_layer.ID));
                            param.Add(new SqlParameter("objectid", geom.ID));
                            param.Add(new SqlParameter("sublayerid", child_layer.ID));
                            param.Add(new SqlParameter("subobjectid", subgeom.ID));
                            param.Add(new SqlParameter("Area", area));
                            if (subgeom is Polygon) param.Add(new SqlParameter("TotalArea", ((Polygon)subgeom).Area));
                            else if (subgeom is MultiPolygon) param.Add(new SqlParameter("TotalArea", ((MultiPolygon)subgeom).Area));
                            else param.Add(new SqlParameter("TotalArea", TotalArea));
                            SqlHelper.Insert(conn, tran, sql, param);

                        }
                    }
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
            }
        }
        private void DealParentChild()
        {
            DataTable table = SqlHelper.Select(SqlHelper.GetSql("CheckParentChild"), null);
            if ((int)table.Rows[0][0] > 0)
            {
                return;
            }
            InsertParentChild(QU_JIE_LAYER_NAME, JIE_DAO_LAYER_NAME);
            InsertParentChild(JIE_DAO_LAYER_NAME, JIE_FANG_LAYER_NAME);
            string sql1 = SqlHelper.GetSql("SelectTempLayer");
            List<SqlParameter> param1 = new List<SqlParameter>();
            param1.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
            DataTable tableLayer = SqlHelper.Select(sql1, param1);
            foreach (DataRow row in tableLayer.Rows)
            {
                if (row["LayerName"].ToString().IndexOf(BOAT_LAYER_NAME) >= 0 || row["LayerName"].ToString().IndexOf(RESCUE_LAYER_NAME) >= 0)
                //if (row["LayerType"].ToString() == "4")
                {
                    InsertParentChild1(JIE_DAO_LAYER_NAME, row["LayerName"].ToString());
                    InsertParentChild1(JIE_FANG_LAYER_NAME, row["LayerName"].ToString());
                    //InsertParentChild(TAX_LAYER_NAME, row["LayerName"].ToString());
                }
            }
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                List<SqlParameter> param = new List<SqlParameter>();
                string sql = SqlHelper.GetSql("UpdateParentChild");
                tran = conn.BeginTransaction();

                param.Add(new SqlParameter("mapid", MainMapImage.Map.MapId));
                SqlHelper.Update(conn, tran, sql, param);
                param.Clear();
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                Common.ShowError(ex);
            }
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            if (btnProjectView.Checked)
            {
                //���ͼƬ��ʾ����û�б���
                if (taxFormInfo == null)
                {
                    TaxViewForm taxForm = new TaxViewForm(user._userName);
                    taxForm.MapId = MainMapImage.Map.MapId;
                    taxForm.QujieTable = string.Format("t_{0}_{1}", taxForm.MapId, MainMapImage.Map.Layers[QU_JIE_LAYER_NAME].ID);
                    taxForm.JiedaoTable = string.Format("t_{0}_{1}", taxForm.MapId, MainMapImage.Map.Layers[JIE_DAO_LAYER_NAME].ID);
                    //taxForm.ControlBox = false;
                    taxForm.DoubleClickObject += taxForm_DoubleClickObject;
                    taxForm.FormClosed += taxForm_FormClosed;
                    taxFormInfo = CreateDockInfo(taxForm, TAXFORMGUID);
                    //taxFormInfo.ShowCloseButton = false;
                    taxFormInfo.ShowContextMenuButton = false;
                    taxForm.Width = taxForm.MinimumSize.Width;
                    taxForm.Height = taxForm.MinimumSize.Height;
                    //����ʾͼƬ����
                    taxForm.Show();
                }
            }
            else
            {
                taxFormInfo.DockableForm.Close();
            }
        }

        void taxForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //20150403
            taxFormInfo = null;
            btnProjectView.Checked = false;
        }

        void taxForm_DoubleClickObject(BoundingBox box, decimal layerid, decimal objectid)
        {
            if (box != null)
            {
                foreach (ILayer layer in MainMapImage.Map.Layers)
                {
                    if (layer.ID == layerid)
                    {
                        MainMapImage.Map.CurrentLayer = layer;
                        VectorLayer vlayer = layer as VectorLayer;

                        GeometryProvider provider = vlayer.DataSource as GeometryProvider;
                        for (int i = 0; i < provider.Geometries.Count; i++)
                        {
                            if (provider.Geometries[i].ID == objectid)
                            {
                                ClearSelectObject();
                                MainMapImage.SelectObjects.Add(provider.Geometries[i]);
                                break;
                            }
                        }
                        break;
                    }
                }

                //MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible;
                if (box.Width > 0 && box.Height > 0)
                {
                    MainMapImage.Map.ZoomToBox(box);
                }
                if (MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.Zoom > MainMapImage.Map.CurrentLayer.MaxVisible)
                {
                    MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible - 1;
                }
                else if (MainMapImage.Map.CurrentLayer != null && MainMapImage.Map.Zoom < MainMapImage.Map.MinimumZoom)
                {
                    MainMapImage.Map.Zoom = MainMapImage.Map.CurrentLayer.MaxVisible + 1;
                }
                ZoomtoolStripTextBox.Text = ((int)MainMapImage.Map.Zoom).ToString();
                EasyMap.Geometries.Point center = new EasyMap.Geometries.Point();
                center.X = (box.Left + box.Right) / 2;
                center.Y = (box.Top + box.Bottom) / 2;
                MainMapImage.Map.Center = center;
                MainMapImage.RequestFromServer = true;
                MainMapImage.Refresh();
                SetToolForm();
                SetToolBarStatus();
                MainMapImage_MapZoomChanged(MainMapImage, MainMapImage.Map.Zoom);
            }
        }

        private void btnLoadShiQu_Click(object sender, EventArgs e)
        {
            if (btnLoadShiQu.Checked)
            {
                if (!Command.IsConnect)
                {
                    Command.SendLoginCommand();
                    if (!Command.IsConnect)
                    {
                        btnLoadPicture.Checked = false;
                        MessageBox.Show("�������ӵ���������");
                        return;
                    }
                }
                if (btnLoadPicture.Checked)
                {
                    btnLoadPicture.Checked = false;
                    btnLoadPicture_Click(null, null);
                }
                DataTable table = MapDBClass.GetTifInformation(MainMapImage.Map.MapId);
                double width = 0;
                mapControl1.Visible = false;
                projectControl1.Initial(MainMapImage.Map.MapId);
                if (table != null && table.Rows.Count > 0)
                {
                    double minx = (double)table.Rows[0]["MinX"];
                    double miny = (double)table.Rows[0]["MinY"];
                    double maxx = (double)table.Rows[0]["MaxX"];
                    double maxy = (double)table.Rows[0]["MaxY"];
                    width = (int)table.Rows[0]["Width"];
                    BoundingBox box = new BoundingBox(minx, miny, maxx, maxy);
                    GdalRasterLayer layer = new GdalRasterLayer(box);
                    MainMapImage.Map.Layers.Add(layer);
                    MainMapImage.HaveTif = true;
                    MainMapImage.ShowShiQu = true;
                    //mapControl1.Visible = true;
                }
                SetToolBarStatus();
                if (MainMapImage.HaveTif)
                {
                    MainMapImage.RequestFromServer = true;
                    MainMapImage.Refresh();
                    MainMapImage_MapZoomChanged(null, MainMapImage.Map.Zoom);
                }
            }
            else
            {
                MainMapImage.HaveTif = false;
                mapControl1.Visible = false;
                MainMapImage.Refresh();
            }
        }

        private void btnSetObjectStyle_Click(object sender, EventArgs e)
        {
            LayerStyleForm form = new LayerStyleForm();
            if (MainMapImage.SelectObjects.Count == 1)
            {
                form.FillBrush = new SolidBrush(Color.FromArgb(MainMapImage.SelectObjects[0].Fill));
                form.OutLinePen = new Pen(Color.FromArgb(MainMapImage.SelectObjects[0].Outline), MainMapImage.SelectObjects[0].OutlineWidth);
                form.TextColor = MainMapImage.SelectObjects[0].TextColor;
                form.TextFont = MainMapImage.SelectObjects[0].TextFont;
                form.EnableOutline = MainMapImage.SelectObjects[0].EnableOutline;
                form.HatchStyle = MainMapImage.SelectObjects[0].HatchStyle;
                Pen linepen = new Pen(Color.FromArgb(MainMapImage.SelectObjects[0].Line));
                linepen.DashStyle = (DashStyle)MainMapImage.SelectObjects[0].DashStyle;
                form.LinePen = linepen;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                foreach (Geometry geom in MainMapImage.SelectObjects)
                {
                    geom.Fill = form.FillBrush.Color.ToArgb();
                    geom.Outline = form.OutLinePen.Color.ToArgb();
                    geom.OutlineWidth = (int)form.OutLinePen.Width;
                    geom.TextColor = form.TextColor;
                    geom.TextFont = form.TextFont;
                    geom.EnableOutline = form.EnableOutline;
                    geom.HatchStyle = form.HatchStyle;
                    geom.Line = form.LinePen.Color.ToArgb();
                    geom.DashStyle = (int)form.LinePen.DashStyle;
                    geom.StyleType = 1;
                    geom.Penstyle = form.Penstyle;
                    MapDBClass.UpdateObject(MainMapImage.Map.MapId, geom);
                }
                MainMapImage.Refresh();
            }
        }

        private void btnDeleteObjectStyle_Click(object sender, EventArgs e)
        {
            foreach (Geometry geom in MainMapImage.SelectObjects)
            {
                geom.StyleType = 0;
                MapDBClass.UpdateObject(MainMapImage.Map.MapId, geom);
            }
            MainMapImage.Refresh();
        }


        //void form_BeforePrint(DiJiForm form, int width, int height)
        //{
        //    form.Image = MainMapImage.Image.Clone() as Image;
        //    return;
        //    BoundingBox box1 = MainMapImage.Map.Envelope;
        //    double minx, miny, maxx, maxy;
        //    BoundingBox box = null;
        //    if (MainMapImage.SelectObjects[0] is Polygon)
        //    {
        //        box = ((Polygon)MainMapImage.SelectObjects[0]).GetBoundingBox();
        //    }
        //    else if (MainMapImage.SelectObjects[0] is MultiPolygon)
        //    {
        //        box = ((MultiPolygon)MainMapImage.SelectObjects[0]).GetBoundingBox();
        //    }

        //    minx = box.Left;
        //    miny = box.Bottom;
        //    maxx = box.Right;
        //    maxy = box.Top;
        //    for (int i = 1; i < MainMapImage.SelectObjects.Count; i++)
        //    {
        //        if (MainMapImage.SelectObjects[i] is Polygon)
        //        {
        //            box = ((Polygon)MainMapImage.SelectObjects[i]).GetBoundingBox();
        //        }
        //        else if (MainMapImage.SelectObjects[i] is MultiPolygon)
        //        {
        //            box = ((MultiPolygon)MainMapImage.SelectObjects[i]).GetBoundingBox();
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        minx = Math.Min(minx, box.Left);
        //        miny = Math.Min(miny, box.Bottom);
        //        maxx = Math.Max(maxx, box.Right);
        //        maxy = Math.Max(maxy, box.Top);
        //    }
        //    MapPanel1.Dock = DockStyle.None;
        //    MapPanel1.Width = width;
        //    MapPanel1.Height = height;

        //    box = new BoundingBox(minx, miny, maxx, maxy);
        //    MainMapImage.RequestFromServer = btnLoadPicture.Checked;
        //    MainMapImage.Map.ZoomToBox(box);
        //    MainMapImage.Visible = false;
        //    MainMapImage.Refresh();
        //    form.Image = MainMapImage.Image.Clone() as Image;
        //    MapPanel1.Dock = DockStyle.Fill;
        //    MainMapImage.RequestFromServer = btnLoadPicture.Checked;
        //    MainMapImage.Map.ZoomToBox(box1);
        //    MainMapImage.Refresh();
        //    MainMapImage.Visible = true;
        //}


        private void LoadSymbol()
        {
            Hashtable imgs = new Hashtable();
            imgs.Add("0", Resources.��);
            imgs.Add("1", Resources.����);
            imgs.Add("2", Resources.��Ԯ��);
            imgs.Add("3", Resources.ҽԺ);
            imgs.Add("4", Resources.����ע��);
            imgs.Add("5", Resources.���˻�);
            imgs.Add("6", Resources.��Ԯ����);
            imgs.Add("20", Resources.blank);
            imgs.Add("0_selected", Resources.��);
            imgs.Add("default", Resources.����);
            imgs.Add("1_selected", Resources.����);
            imgs.Add("2_selected", Resources.��Ԯ��);
            imgs.Add("3_selected", Resources.ҽԺ);
            imgs.Add("4_selected", Resources.����ע��);
            imgs.Add("5_selected", Resources.���˻�);
            imgs.Add("6_selected", Resources.��Ԯ����);
            imgs.Add("20_selected", Resources.blank);
            imgs.Add("default_selected", Resources.����);
            foreach (ILayer ilayer in MainMapImage.Map.Layers)
            {
                VectorLayer layer = ilayer as VectorLayer;
                if (layer == null || layer.LayerName.IndexOf("ע��") < 0)
                {
                    continue;
                }
                GeometryProvider geoms = layer.DataSource as GeometryProvider;
                string sql = SqlHelper.GetSql("SelectSymbol");
                if (ilayer.LayerName == BOAT_LAYER_NAME || ilayer.LayerName == RESCUE_LAYER_NAME || ilayer.LayerName == RESCUE_WURENJI_LAYER_NAME || ilayer.LayerName == RESCUE_BOAT_LAYER_NAME)
                {
                    sql = "select MAX(FLDM),MAX(objectid) from @table group by ZJMC ";
                }
                if (ilayer.LayerName == BOAT_LAYER_NAME)
                { 
                
                }
                string tablename = "t_" + MainMapImage.Map.MapId + "_" + layer.ID;
                sql = sql.Replace("@table", tablename);
                DataTable table = SqlHelper.Select(sql, null);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    decimal id = (decimal)table.Rows[i][1];
                    if(id==119)
                    {
                    
                    }
                    string key = table.Rows[i][0].ToString();
                    foreach (Geometry geom in geoms.Geometries)
                    {
                        if (geom.ID == id)
                        {
                            if (imgs.ContainsKey(key))
                            {
                                geom.PointSymbol = (Image)imgs[key];
                            }
                            else
                            {
                                geom.PointSymbol = (Image)imgs["default"];
                            }
                            if (imgs.ContainsKey(key + "_selected"))
                            {
                                geom.PointSelectSymbol = (Image)imgs[key + "_selected"];
                            }
                            else
                            {
                                geom.PointSelectSymbol = (Image)imgs["default_selected"];
                            }
                            break;
                        }
                        //��ʱΪ��������ѵ�ʱ����һ��id����0�����
                        //else if (geom.ID == 0)
                        //{
                        //    geom.PointSymbol = (Image)imgs[4];
                        //    geom.PointSelectSymbol = (Image)imgs["4_selected"];
                        //}
                    }
                }
            }
        }

        private void PutJieDaoTranspraent()
        {
            RestoreJieDaoTranspraent();
            TranspraentTable.Clear();
            VectorLayer layer = MainMapImage.Map.Layers[JIE_DAO_LAYER_NAME] as VectorLayer;
            GeometryProvider geoms = layer.DataSource as GeometryProvider; 
            foreach (Geometry geom in geoms.Geometries)
            {
                if (geom.StyleType == 1)
                {
                    TranspraentTable.Add(geom.ID, geom.Fill);
                    Color fill = Color.FromArgb(geom.Fill);
                    geom.Fill = Color.FromArgb(0, Color.FromArgb(fill.R, fill.G, fill.B)).ToArgb();
                }
            }
            TranspraentTable.Add("Layer", layer.Style.Fill.Color);
            layer.Style.Fill.Color = Color.FromArgb(0, Color.FromArgb(layer.Style.Fill.Color.R, layer.Style.Fill.Color.G, layer.Style.Fill.Color.B));
        }

        private void RestoreJieDaoTranspraent()
        {
            if (TranspraentTable.Count <= 0)
            {
                return;
            }
            VectorLayer layer = MainMapImage.Map.Layers[JIE_DAO_LAYER_NAME] as VectorLayer;
            GeometryProvider geoms = layer.DataSource as GeometryProvider;
            foreach (Geometry geom in geoms.Geometries)
            {
                if (TranspraentTable.ContainsKey(geom.ID))
                {
                    geom.Fill = (int)TranspraentTable[geom.ID];
                }
            }
            layer.Style.Fill.Color = (Color)TranspraentTable["Layer"];
            TranspraentTable.Clear();
        }

        private void PutTextColor()
        {
            FontColorTable.Clear();
            foreach (ILayer ilayer in MainMapImage.Map.Layers)
            {
                VectorLayer layer = ilayer as VectorLayer;
                if (layer == null)
                {
                    continue;
                }
                if (layer.LayerName == "���ɵ�" || layer.LayerName == "�θɵ�" || layer.LayerName.IndexOf("ע��") >= 0 || layer.LayerName.IndexOf("·") >= 0 || layer.LayerName == "���" || layer.LayerName.IndexOf("�ڵ�ͼ��") >= 0)
                {
                    GeometryProvider geoms = layer.DataSource as GeometryProvider;
                    foreach (Geometry geom in geoms.Geometries)
                    {
                        if (geom.StyleType == 1)
                        {
                            FontColorTable.Add(layer.ID.ToString() + "|" + geom.ID.ToString(), geom.TextColor);
                        }
                    }
                    FontColorTable.Add(layer.ID, layer.Style.TextColor);
                    layer.Style.TextColor = Color.Yellow;
                }
            }
        }

        private void RestoreTextColor()
        {
            if (FontColorTable.Count <= 0)
            {
                return;
            }
            foreach (ILayer ilayer in MainMapImage.Map.Layers)
            {
                VectorLayer layer = ilayer as VectorLayer;
                if (layer == null)
                {
                    continue;
                }
                if (layer.LayerName == "���ɵ�" || layer.LayerName == "�θɵ�" || layer.LayerName.IndexOf("ע��") >= 0 || layer.LayerName.IndexOf("·") >= 0 || layer.LayerName == "���" || layer.LayerName.IndexOf("�ڵ�ͼ��") >= 0)
                {
                    GeometryProvider geoms = layer.DataSource as GeometryProvider;
                    foreach (Geometry geom in geoms.Geometries)
                    {
                        if (FontColorTable.ContainsKey(layer.ID.ToString() + "|" + geom.ID.ToString()))
                        {
                            geom.TextColor = (Color)FontColorTable[layer.ID.ToString() + "|" + geom.ID.ToString()];
                        }
                    }
                    layer.Style.TextColor = (Color)FontColorTable[layer.ID];
                }
            }
            FontColorTable.Clear();
        }
        private List<TreeNode> listNode = new List<TreeNode>();
        public void btnTax_Click(object sender, EventArgs e)
        {
            if (btnTax.Checked)
            {
                listNode = new List<TreeNode>();
                //�����ѹ�ѡ��ͼ��
                foreach(TreeNode node in LayerView1.Nodes[0].Nodes)
                {
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        if (subNode.Checked == true)
                        {
                            listNode.Add(subNode);
                        }
                       if (subNode.Nodes != null)
                        {
                            foreach (TreeNode subSubNode in subNode.Nodes)
                            {
                                if (subSubNode.Checked == true)
                                {
                                    listNode.Add(subSubNode);
                                }
                            }
                        }
                    }
                }
                PutJieDaoTranspraent();
                List<string> layers = new List<string>();
                layers.Add("�������");
                layers.Add("�ʾ��ӽֵ������");
                layers.Add("�ֵ�ͼ��");
                layers.Add("˰�񼶱�ͼ��");
                LayerView1.Nodes[0].Checked = false;
                foreach (string layername in layers)
                {
                    TreeNode node = FindNode(layername);
                    if (node != null)
                    {
                        node.Checked = true;
                    }
                }
            }
            else
            {
                RestoreJieDaoTranspraent();
                //LayerView1.Nodes[0].Checked = true;
                //��֮ǰ��ѡ��ͼ��ָ�ѡ��״̬
                for (int i = 0; i < listNode.Count; i++)
                {
                    listNode[i].Checked = true;
                }
                TreeNode node = FindNode(Resources.MotionPointLayer);
                if (node != null)
                {
                    node.Checked = false;
                }
                node = FindNode(Resources.PhotoLayer);
                if (node != null)
                {
                    node.Checked = false;
                }
            }
            MainMapImage.Refresh();
        }

        /// <summary>
        /// �Զ��屨�������˵��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomReportMenuItem_Click_1(object sender, EventArgs e)
        {
            //ReportMakeForm form = new ReportMakeForm();
            //form.ShowDialog();
            CustomReportForm form = new CustomReportForm();
            form.Preview += new CustomReportForm.PreviewEvent(CustomReport_Preview);
            form.Show(this);
            LoadCustomReport();
        }

        private void pic_out_Click(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(MainMapImage.Image.Width, MainMapImage.Image.Height);
            MainMapImage.DrawToBitmap(bit, MainMapImage.ClientRectangle);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "GIF Image (*.gif)|*.gif|JPEG Image File (*.jpg)|*.jpg|JPEG Image File (*.jpeg)|*.jpeg|Bitmaps (*.bmp)|*.bmp|Enhanced Metafiles (*.emf)|*.emf "; 
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string strpath = sfd.FileName;
            bit.Save(strpath, System.Drawing.Imaging.ImageFormat.Bmp);
            bit.Dispose();
            MessageBox.Show("�����ͼ�ɹ���");
        }

        private void quanxian_Click(object sender, EventArgs e)
        {
        //    //��ʾȨ�����ô���
        //    addUser form = new addUser();
        //    form.ShowDialog();
        }

        private void Ȩ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //��ʾȨ�����ô���
            addUser form = new addUser(MainMapImage.Map.Layers["�ֵ�ͼ��"].ID.ToString());
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show(this);
        }

        private void �����û�Ȩ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user_quanxian quanxian = new user_quanxian("tudi");
            quanxian .StartPosition = FormStartPosition.CenterScreen;
            quanxian.Show(this);
        }

        private void ˰���û�Ȩ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user_quanxian quanxian = new user_quanxian("shuiwu");
            quanxian.StartPosition = FormStartPosition.CenterScreen;
            quanxian.Show(this);
        }
        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = @Application.StartupPath + "\\�Եؿ�˰ʹ���ֲ�20160525.doc"; //����д���pdf·��
            System.Diagnostics.Process.Start(path);
            //Microsoft.Office.Interop.Word.ApplicationClass wa = new Microsoft.Office.Interop.Word.ApplicationClass();
            //object filename = @Application.StartupPath + "\\�Եؿ�˰ʹ���ֲ�20160525.doc";
            //object missing = System.Reflection.Missing.Value;
            //wa.Documents.Open(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            //wa.ShowMe();
        }      

        private void passwordUpdate_Click(object sender, EventArgs e)
        {
            passwordUpdate pwdUpdate = new passwordUpdate();
            pwdUpdate.StartPosition = FormStartPosition.CenterScreen;
            pwdUpdate.Show(this);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSelectObject();
            MainMapImage.FindAreaList.Clear();
            MainMapImage.Refresh();
        }
        //�ϲ�������ͼ��
        private void MergeShp_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                //bool haveNode = false;
                //int num = 0;
                ChoosePicForm picForm = new ChoosePicForm();//ѡ����Ҫ�����ͼ��
                List<string> picfields = new List<string>();
                TreeNode node = FindNode("�ڵ���Ϣͼ��");
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    if (node.Nodes[i].Text != MainMapImage.Map.CurrentLayer.LayerName)
                    {
                        picfields.Add(node.Nodes[i].Text);
                    }
                }
                picForm.Fields = picfields;
                if (picForm.ShowDialog() == DialogResult.OK)
                {
                    if (picForm.SelectField == string.Empty)
                    {
                        MessageBox.Show("δѡ��ϲ�����ͼ�㣡");
                        return;
                    }
                    //��ȡ��ѡ���ص�layerId
                    decimal layerId = MainMapImage.Map.Layers[picForm.SelectField].ID;
                    string sql = "select * from t_" + MainMapImage.Map.MapId + "_" + layerId + " where 1<>1";
                    DataTable table = SqlHelper.Select(sql, null);

                    //string MaxObjectSql = "select max(ObjectId) from t_" + MainMapImage.Map.MapId + "_" + layerId;
                    string MaxObjectSql = "select max(ObjectId) from t_object where LayerId = " + layerId;
                    DataTable tableMaxObject = SqlHelper.Select(MaxObjectSql, null);
                    string insertSql = "insert into t_" + MainMapImage.Map.MapId + "_" + layerId + " (";
                    List<string> cloumnName = new List<string>();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string sqlNew = "select * from t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.CurrentLayer.ID +" where 1<>1";
                        DataTable tableNew = SqlHelper.Select(sqlNew, null);
                        //��ͼ���Ƿ��д��ֶ�
                        for (int j = 0; j < tableNew.Columns.Count; j++)
                        {
                            if (table.Columns[i].ColumnName == tableNew.Columns[j].ColumnName)
                            {
                                //��ȡ��ѡͼ������
                                insertSql = insertSql + table.Columns[i].ColumnName + ",";
                                cloumnName.Add(table.Columns[i].ColumnName);
                                break;
                            }
                        }
                    }
                    //ȥ�����һ������
                    insertSql = insertSql.Substring(0, insertSql.Length - 1);
                    insertSql = insertSql + ") select ";
                    for (int i = 0; i < cloumnName.Count; i++)
                    {
                        //��ȡ��ѡͼ������
                        if (cloumnName[i] == "ObjectId")
                        { 
                            //objectIdΪ ��ѡͼ�����id+1�������ۼ�
                            //insertSql = insertSql + "(RANK() OVER (ORDER BY LayerId,ObjectId DESC))+" + tableMaxObject.Rows[0][0] + " as ObjectId, ";
                            insertSql = insertSql + "ObjectId+" + tableMaxObject.Rows[0][0] + " as ObjectId, ";
                        }
                        else if (cloumnName[i] == "LayerId")
                        {
                            //LayerIdΪ ��ѡͼ��LayerId
                            insertSql = insertSql + layerId + ",";
                        }
                        else
                        {
                            insertSql = insertSql + cloumnName[i] + ",";
                        }
                    }
                    //ȥ�����һ������
                    insertSql = insertSql.Substring(0, insertSql.Length - 1);
                    insertSql = insertSql + " from t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.CurrentLayer.ID;

                    conn = SqlHelper.GetConnection();
                    conn.Open();
                    tran = conn.BeginTransaction();
                    //����ͼ�����ݱ��浽��ѡͼ����
                    SqlHelper.Insert(conn, tran, insertSql, null);
                    //��ѯͼ�����ݲ���ӵ���ѡͼ����
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
                    param.Add(new SqlParameter("LayerId", MainMapImage.Map.CurrentLayer.ID));
                    DataTable tableObject = SqlHelper.Select(conn, tran, SqlHelper.GetSql("SelectTempObject"), param);
                    for (int i = 0; i < tableObject.Rows.Count; i++)
                    {
                        param.Clear();
                        param.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
                        param.Add(new SqlParameter("LayerId", MainMapImage.Map.Layers[picForm.SelectField].ID));
                        param.Add(new SqlParameter("ObjectId", (Int32.Parse(tableObject.Rows[i]["ObjectId"].ToString()) + Int32.Parse(tableMaxObject.Rows[0][0].ToString())).ToString()));
                        param.Add(new SqlParameter("ObjectData", tableObject.Rows[i]["ObjectData"]));
                        param.Add(new SqlParameter("Name", ""));
                        string updateSql = SqlHelper.GetSql("InsertTempObject");
                        SqlHelper.Insert(conn, tran, updateSql, param);
                    }
                    //��������Ϣ���ݵ���ͼ����
                    param.Clear();
                    param.Add(new SqlParameter("LayerId", MainMapImage.Map.CurrentLayer.ID));
                    param.Add(new SqlParameter("LayerIdNew", MainMapImage.Map.Layers[picForm.SelectField].ID));
                    param.Add(new SqlParameter("geomId", tableMaxObject.Rows[0][0].ToString()));
                    SqlHelper.Insert(conn, tran, SqlHelper.GetSql("InsertTudiFromOtherLayerId"), param);
                   //"update t_object set LayerId = " + layerId + " , ObjectId = ObjectId+" + tableMaxObject.Rows[0][0] + " where LayerId = " + MainMapImage.Map.CurrentLayer.ID;
                    //����object��layeridΪ��ѡͼ��id
                    tran.Commit();
                    conn.Close();
                    //ɾ����ͼ��
                    //RemoveLayerToolStripButton_Click(null,null);

                    //MainMapImage.Refresh();
                    MessageBox.Show("��������ϵͳ�󣬺ϲ���Ч!");
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                MessageBox.Show("�ϲ�ʧ��", ex.Message);
                return ;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            //DialogResult dr = MessageBox.Show("ȷ��Ҫ������ͼ����?", "����ͼ��", messButton);

            //if (dr != DialogResult.OK)
            //{
            //    return;
            //}

            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            //try
            //{
            //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //    saveFileDialog1.InitialDirectory = "c:\\";
            //    saveFileDialog1.Filter = "Shaper Files (*.shp)|*.shp|All files (*.*)|*.*";
            //    string inSHPpath = null;
            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        inSHPpath = saveFileDialog1.FileName;
            //    }
            //    else
            //    {
            //        return;
            //    }

            //    //waiting1.SetAutoProcess(true);
            //    //waiting1.Visible = true;
            //    //�ж����ɵ��ļ��Ƿ��Ѵ��ڣ�������ڣ���ɾ���Ѵ��ڵ��ļ���
            //    string shpDirName = System.IO.Path.GetDirectoryName(inSHPpath);
            //    string shpName1 = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);
            //    string shpFullName = shpName1 + ".shp";
            //    string prjName = shpName1 + ".prj";
            //    string dbfName = shpName1 + ".dbf";
            //    string shxName = shpName1 + ".shx";
            //    string sbnName = shpName1 + ".sbn";
            //    string xmlName = shpName1 + ".shp.xml";
            //    string sbxName = shpName1 + ".sbx";
            //    if (System.IO.File.Exists(shpDirName + "\\" + shpFullName))
            //        System.IO.File.Delete(shpDirName + "\\" + shpFullName);
            //    if (System.IO.File.Exists(shpDirName + "\\" + prjName))
            //        System.IO.File.Delete(shpDirName + "\\" + prjName);
            //    if (System.IO.File.Exists(shpDirName + "\\" + dbfName))
            //        System.IO.File.Delete(shpDirName + "\\" + dbfName);
            //    if (System.IO.File.Exists(shpDirName + "\\" + shxName))
            //        System.IO.File.Delete(shpDirName + shxName);
            //    if (System.IO.File.Exists(shpDirName + "\\" + sbnName))
            //        System.IO.File.Delete(shpDirName + "\\" + sbnName);
            //    if (System.IO.File.Exists(shpDirName + "\\" + xmlName))
            //        System.IO.File.Delete(shpDirName + "\\" + xmlName);
            //    if (System.IO.File.Exists(shpDirName + "\\" + sbxName))
            //        System.IO.File.Delete(shpDirName + "\\" + sbxName);

            //    //��ʼ����shp��

            //    string shpName = System.IO.Path.GetFileNameWithoutExtension(inSHPpath);   //��ȡ���ɵ�ʸ��

            //    //������shapefile�Ĺ����ռ䣻
            //    //ESRI.ArcGIS.Geodatabase.IFeatureWorkspace pFWS = null;

            //    //IAoInitialize m_AoInitialize = new AoInitializeClass();
            //    //esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            //    //licenseStatus = m_AoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine);

            //    //ESRI.ArcGIS.Geodatabase.IWorkspaceFactory pWSF = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactory();
            //    //pFWS = pWSF.OpenFromFile(shpDirName, 0) as IFeatureWorkspace;
            //    ////��������ͼ�Ŀռ�����ο�ϵͳ��
            //    //IGeometryDef geometryDef = new GeometryDefClass();
            //    //IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            //    //geometryDefEdit.GeometryType_2 = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon;
            //    //ESRI.ArcGIS.Geometry.ISpatialReferenceFactory spatialReferenceFactory = new ESRI.ArcGIS.Geometry.SpatialReferenceEnvironmentClass();
            //    //ESRI.ArcGIS.Geometry.ISpatialReference spatialReference = new ESRI.ArcGIS.Geometry.UnknownCoordinateSystemClass();

            //    //ESRI.ArcGIS.Geometry.ISpatialReferenceResolution spatialReferenceResolution = (ESRI.ArcGIS.Geometry.ISpatialReferenceResolution)spatialReference;
            //    //spatialReferenceResolution.ConstructFromHorizon();
            //    //ESRI.ArcGIS.Geometry.ISpatialReferenceTolerance spatialReferenceTolerance = (ESRI.ArcGIS.Geometry.ISpatialReferenceTolerance)spatialReference;
            //    //spatialReferenceTolerance.SetDefaultXYTolerance();
            //    //geometryDefEdit.SpatialReference_2 = spatialReference;

            //    //IField fields = new FieldClass();
            //    //IFieldEdit nameFieldEdit = (IFieldEdit)fields;
            //    //IField geometryField = new FieldClass();
            //    //IFields fields1 = new FieldsClass();
            //    //IFieldsEdit fieldsEdit = (IFieldsEdit)fields1;
            //    //IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            //    ////����ֶΡ�OID����
            //    //geometryFieldEdit.Name_2 = "Shape";
            //    //geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            //    //geometryFieldEdit.GeometryDef_2 = geometryDef;
            //    //fieldsEdit.AddField(geometryField);
            //    ////��ȡ������Ϣ
            //    //string sql = "select * from " + MapDBClass.GetPropertyTableName(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID);
            //    //DataTable temptable = SqlHelper.Select(sql, null);
            //    //for (int col = 0; col < temptable.Columns.Count; col++)
            //    //{
                //    //����ֶ�;
                //    if (temptable.Columns[col].ColumnName == "UpdateDate" || temptable.Columns[col].ColumnName == "CreateDate")
                //    {
                //        continue;
                //    }
                //    fields = new FieldClass();
                //    nameFieldEdit = (IFieldEdit)fields;
                //    nameFieldEdit.Name_2 = temptable.Columns[col].ColumnName;
                //    nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                //    nameFieldEdit.Length_2 = 40;
                //    fieldsEdit.AddField(nameFieldEdit);
                //}
                //IFieldChecker fieldChecker = new FieldCheckerClass();
                //IEnumFieldError enumFieldError = null;
                //IFields validatedFields = null;
                //fieldChecker.ValidateWorkspace = (IWorkspace)pFWS;
                //fieldChecker.Validate(fields1, out enumFieldError, out validatedFields);

                ////�ڹ����ռ�������FeatureClass;
                //ESRI.ArcGIS.Geodatabase.IFeatureClass pNewFeaCls = pFWS.CreateFeatureClass(shpName,
                //    validatedFields, null, null, ESRI.ArcGIS.Geodatabase.esriFeatureType.esriFTSimple, "Shape", "");

                //ESRI.ArcGIS.Geodatabase.IFeature feature = null;
                //ESRI.ArcGIS.Geometry.IPointArray pts = new ESRI.ArcGIS.Geometry.PointArrayClass();
                //ESRI.ArcGIS.Geometry.IPoint pt = new ESRI.ArcGIS.Geometry.PointClass();
                //ESRI.ArcGIS.Geometry.Ring polygon = new ESRI.ArcGIS.Geometry.RingClass();

        //        object missing = Type.Missing;
        //        //��ȡ������Ϣ
        //        DataTable dt = MapDBClass.GetObject(MainMapImage.Map.MapId, MainMapImage.Map.CurrentLayer.ID);
        //        int count = dt.Rows.Count;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            waiting1.SetAutoProcess(false);
        //            waiting1.Visible = true;
        //            waiting1.MinProcessValue = 0;
        //            waiting1.MaxProcessValue = dt.Rows.Count;
        //            waiting1.ProcessValue = i;
        //            waiting1.Tip = "���ڵ�����ͼ����:" + i + "/" + dt.Rows.Count;

        //            //waiting1.SetAutoProcess(false);
        //            //waiting1.Visible = true;
        //            ////waiting1.ProcessValue = 0;
        //            //waiting1.MinProcessValue = 0;
        //            //waiting1.MaxProcessValue = dt.Rows.Count;
        //            //waiting1.ProcessValue = i;
        //            ////waiting1.Tip = "���ڵ�����ͼ���ݣ����Ժ�...";
        //            //waiting1.Tip = "���ڵ�����ͼ����:"+i+"/"+count;
        //            polygon = new ESRI.ArcGIS.Geometry.RingClass();
        //            string a = null;
        //            string b = null;
        //            //������Ϣ
        //            byte[] data = (byte[])dt.Rows[i]["ObjectData"];
        //            pts = new ESRI.ArcGIS.Geometry.PointArrayClass();
        //            pt = new ESRI.ArcGIS.Geometry.PointClass();
        //            string str = Common.DeserializeObject(data).ToString();
        //            string[] newStr = str.Split('(');
        //            //ͼ�����ͣ��㡢�ߡ��棩
        //            string style = newStr[0].ToString();
        //            polygon = new ESRI.ArcGIS.Geometry.RingClass();
        //            ESRI.ArcGIS.Geometry.IGeometryCollection pointPolygon = new ESRI.ArcGIS.Geometry.PolygonClass();

        //            int intValue = 2;
        //            if (temptable.Rows.Count > i)
        //            {
        //                feature = pNewFeaCls.CreateFeature();
        //                for (int n = 1; n < newStr.Length; n++)
        //                {
        //                    if (newStr[n] != string.Empty)
        //                    {
        //                        polygon = new ESRI.ArcGIS.Geometry.RingClass();
        //                        string[] point = newStr[n].ToString().Split(',');

        //                        for (int m = 0; m < point.Length; m++)
        //                        {
        //                            string pointXY = point[m].ToString().Trim();
        //                            if (pointXY != string.Empty)
        //                            {
        //                                pt = new ESRI.ArcGIS.Geometry.PointClass();
        //                                pointXY = pointXY.Split(')')[0].ToString();
        //                                a = pointXY.Trim().Split(' ')[0].ToString();
        //                                b = pointXY.Trim().Split(' ')[1].ToString();
        //                                pt.PutCoords(double.Parse(a), double.Parse(b));
        //                                pts.Add(pt);
        //                                polygon.AddPoint(pt, ref missing, ref missing);
        //                            }
        //                        }
        //                        pointPolygon.AddGeometry(polygon as ESRI.ArcGIS.Geometry.IGeometry, ref missing, ref missing);
        //                    }
        //                }
        //                ESRI.ArcGIS.Geometry.IPolygon polyGonGeo = pointPolygon as ESRI.ArcGIS.Geometry.IPolygon;
        //                polyGonGeo.SimplifyPreserveFromTo();
        //                feature.Shape = polyGonGeo;
        //                if (temptable.Rows[i]["MapId"].ToString() != string.Empty &&
        //                    temptable.Rows[i]["LayerId"].ToString() != string.Empty &&
        //                    temptable.Rows[i]["ObjectId"].ToString() != string.Empty)
        //                {
        //                    for (int colTemp = 0; colTemp < temptable.Columns.Count; colTemp++)
        //                    {
        //                        if (temptable.Columns[colTemp].ColumnName != "UpdateDate" && temptable.Columns[colTemp].ColumnName != "CreateDate")
        //                        {
        //                            feature.set_Value(intValue, temptable.Rows[i][colTemp].ToString());

        //                            intValue++;
        //                        }
        //                    }
        //                    feature.Store();
        //                }
        //            }
        //        }
        //        waiting1.Visible = false;
        //        waiting1.SetAutoProcess(true);
        //        MessageBox.Show("�ɹ�����shp�ļ���");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("����shp�ļ�ʧ�ܣ�" + ex.ToString());
        //    }
        }

        private void Ӱ��ͼ�㼶����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetParentChildForm form = new SetParentChildForm(MainMapImage.Map.MapId);
            form.Show(this);
        }

       

        private void ����ԱToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user_quanxian quanxian = new user_quanxian("guanliyuan");
            quanxian.StartPosition = FormStartPosition.CenterScreen;
            quanxian.Show(this);
        }

        private void ���ĵؿ�������ע��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show(this);
        }

        private void ����ͼ�������ʽToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTypeForm form = new SetTypeForm(MainMapImage.Map.MapId);
            form.setLayerType += new SetTypeForm.SetLayerType(setLayerType);
            form.setLayerStyle += new SetTypeForm.SetLayerStyle(setLayerStyle);
            form.Map = MainMapImage.Map; 
            form.Show(this);
        }

        //void form_BeforePrint1(DiJiFormShuiWu form, int width, int height)
        //{
        //    form.Image = MainMapImage.Image.Clone() as Image;
        //    return;
        //    BoundingBox box1 = MainMapImage.Map.Envelope;
        //    double minx, miny, maxx, maxy;
        //    BoundingBox box = null;
        //    if (MainMapImage.SelectObjects[0] is Polygon)
        //    {
        //        box = ((Polygon)MainMapImage.SelectObjects[0]).GetBoundingBox();
        //    }
        //    else if (MainMapImage.SelectObjects[0] is MultiPolygon)
        //    {
        //        box = ((MultiPolygon)MainMapImage.SelectObjects[0]).GetBoundingBox();
        //    }

        //    minx = box.Left;
        //    miny = box.Bottom;
        //    maxx = box.Right;
        //    maxy = box.Top;
        //    for (int i = 1; i < MainMapImage.SelectObjects.Count; i++)
        //    {
        //        if (MainMapImage.SelectObjects[i] is Polygon)
        //        {
        //            box = ((Polygon)MainMapImage.SelectObjects[i]).GetBoundingBox();
        //        }
        //        else if (MainMapImage.SelectObjects[i] is MultiPolygon)
        //        {
        //            box = ((MultiPolygon)MainMapImage.SelectObjects[i]).GetBoundingBox();
        //        }
        //        else
        //        {
        //            continue;
        //        }

        //        minx = Math.Min(minx, box.Left);
        //        miny = Math.Min(miny, box.Bottom);
        //        maxx = Math.Max(maxx, box.Right);
        //        maxy = Math.Max(maxy, box.Top);
        //    }
        //    MapPanel1.Dock = DockStyle.None;
        //    MapPanel1.Width = width;
        //    MapPanel1.Height = height;

        //    box = new BoundingBox(minx, miny, maxx, maxy);
        //    MainMapImage.RequestFromServer = btnLoadPicture.Checked;
        //    MainMapImage.Map.ZoomToBox(box);
        //    MainMapImage.Visible = false;
        //    MainMapImage.Refresh();
        //    form.Image = MainMapImage.Image.Clone() as Image;
        //    MapPanel1.Dock = DockStyle.Fill;
        //    MainMapImage.RequestFromServer = btnLoadPicture.Checked;
        //    MainMapImage.Map.ZoomToBox(box1);
        //    MainMapImage.Refresh();
        //    MainMapImage.Visible = true;
        //}
        //protected override void OnResize(EventArgs e)
        //{
        //    //    return;
        //    if (this.WindowState == FormWindowState.Minimized)
        //    {
        //        formClick = "Minimized";
        //        //return;
        //    }
        //    else if (this.WindowState == FormWindowState.Normal && (formClick == "Normal"|| formClick =="Maximized"))
        //    {
        //        formClick = "Normal";
        //        //����ͼ��ȵ���
        //        //ȡ�ù�������߶�
        //        int h = toolStripContainer1.ClientRectangle.Height - menuStrip1.Height - MainToolStrip.Height - MainStatusStrip.Height;
        //        //�����ʼ�˵�ͼ�Ƚ�
        //        if (CompareToolStripButton.Checked)
        //        {
        //            //��������ͼ�ߴ�
        //            MapPanel1.Height = h / 2 - 5;
        //            MapPanel2.Top = MapPanel1.Bottom + 5;
        //            MapPanel2.Height = MapPanel1.Height;
        //            MapPanel2.Visible = true;
        //            //������ͼ���ĵ��Լ�����ͬ��ͼһ��
        //            if (MainMapImage2.Map.Layers.Count > 0)
        //            {
        //                MainMapImage2.Map.Center = MainMapImage1.Map.Center;
        //                MainMapImage2.Map.Zoom = MainMapImage1.Map.Zoom;
        //                MainMapImage2.Refresh();
        //            }
        //        }
        //        //����ر��˵�ͼ�Ƚϣ�������ͼ��ԭ
        //        else
        //        {
        //            MapPanel2.Visible = false;
        //            MapPanel1.Height = h;
        //            //MainMapImage1.Refresh();
        //        }
        //    }
        //    else if (this.WindowState == FormWindowState.Normal)
        //    {
        //        formClick = "Normal";
        //        return;
        //    }
        //    else if (this.WindowState == FormWindowState.Maximized && (formClick == "Normal" || formClick == "Minimized"))
        //    {
        //        formClick = "Maximized";
        //        this.OnResize;
        //        //MainMapImage_MouseDown(sender, null,null);
        //        return;
        //    }
        //    else
        //    {
        //        formClick = "Normal";
        //        //����ͼ��ȵ���
        //        //ȡ�ù�������߶�
        //        int h = toolStripContainer1.ClientRectangle.Height - menuStrip1.Height - MainToolStrip.Height - MainStatusStrip.Height;
        //        //�����ʼ�˵�ͼ�Ƚ�
        //        if (CompareToolStripButton.Checked)
        //        {
        //            //��������ͼ�ߴ�
        //            MapPanel1.Height = h / 2 - 5;
        //            MapPanel2.Top = MapPanel1.Bottom + 5;
        //            MapPanel2.Height = MapPanel1.Height;
        //            MapPanel2.Visible = true;
        //            //������ͼ���ĵ��Լ�����ͬ��ͼһ��
        //            if (MainMapImage2.Map.Layers.Count > 0)
        //            {
        //                MainMapImage2.Map.Center = MainMapImage1.Map.Center;
        //                MainMapImage2.Map.Zoom = MainMapImage1.Map.Zoom;
        //                MainMapImage2.Refresh();
        //            }
        //        }
        //        //����ر��˵�ͼ�Ƚϣ�������ͼ��ԭ
        //        else
        //        {
        //            MapPanel2.Visible = false;
        //            MapPanel1.Height = h;
        //            MainMapImage1.Refresh();
        //        }
        //    }

        //}  

        //������ʾ��ʽ ������ͼ�������ʽ��ʹ�÷���
        public void setLayerType(string no,LayerStyleForm form)
        {
            //SetLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutLine, hatchStyle, LinePen, Penstyle, node);
            //if (MainMapImage.NeedSave && layer.NeedSave)
            //{
                MapDBClass.UpdateLayerType(no, form);
                //}
            MainMapImage.Refresh();
        }
        public void setLayerStyle(VectorLayer layer, SolidBrush fillBrush, Pen outLinePen, Color textColor, Font textFont, bool enableOutLine, int hatchStyle, Pen LinePen, int Penstyle)
        {
            SetLayerStyle(layer, fillBrush, outLinePen, textColor, textFont, enableOutLine, hatchStyle, LinePen, Penstyle, null);
            MainMapImage.Refresh();
        }

        private void �������ɹ�Ͻ��ͼToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("ȷ��Ҫ�������ɹ�Ͻ��ͼ��?", "���ɹ�Ͻ��ͼ", messButton);

            if (dr != DialogResult.OK)
            {
                return;
            }
            SqlConnection conn = SqlHelper.GetConnection();
            conn.Open();
            string sql = "delete from t_parent_child";
            SqlHelper.Delete(conn, sql, null);
            conn.Close();
            DealParentChild();
            MessageBox.Show("���ɳɹ���");
        }

        private void ������Ԯ��_Click(object sender, EventArgs e)
        {
            double X = 0;
            double Y = 0;
            if (FindSaveBoat.Text.Trim() == "")
            {
                MessageBox.Show("��������Ҫ��Ԯ�������ơ�");
                return;
            }
            ClearSelectObject();
            PhotoData data;
            //����Ԫ�ض������������
            string txt = FindSaveBoat.Text.Trim();
            bool find = false;
            MainMapImage.FindAreaList.Clear();
            foreach (ILayer layer in MainMapImage.Map.Layers)
            {
                if (layer is VectorLayer)
                {
                    VectorLayer vlayer = layer as VectorLayer;
                    Collection<Geometry> geometries = new Collection<Geometry>();
                    GeometryProvider provider = vlayer.DataSource as GeometryProvider;

                    foreach (Geometry geom in provider.Geometries)
                    {
                        //if (geom.Text == txt)
                        if (System.Text.RegularExpressions.Regex.IsMatch(geom.Text, txt))
                        {
                            find = true;
                            data = new PhotoData();
                            BoundingBox box = geom.GetBoundingBox();
                            data.MinX = box.Min.X;
                            data.MinY = box.Min.Y;
                            data.MaxX = box.Max.X;
                            data.MaxY = box.Max.Y;
                            data.Name = geom.Text;
                            MainMapImage.SelectObjects.Add(geom);
                            //else
                            //{
                            MainMapImage.FindAreaList.Add(data);
                            //}
                            //if (!MainMapImage.Map.Envelope.Contains(geom.GetBoundingBox()))
                            //{
                            //    MainMapImage.Map.ZoomToBox(geom.GetBoundingBox());
                            //}

                            //TreeNode node = FindNode(layer.LayerName);
                            //LayerView.SelectedNode = node;
                        }
                    }
                }
            }
            if (!find)
            {
                MessageBox.Show("û�в�ѯ��ָ��������");
                return;
            }
            if (find)
            {
                BoundingBox box = new BoundingBox(MainMapImage.FindAreaList[0].MinX, MainMapImage.FindAreaList[0].MinY, MainMapImage.FindAreaList[0].MaxX, MainMapImage.FindAreaList[0].MaxY);
                for (int i = 1; i < MainMapImage.FindAreaList.Count; i++)
                {
                    if (box.Min.X > MainMapImage.FindAreaList[i].MinX)
                    {
                        box.Min.X = MainMapImage.FindAreaList[i].MinX;
                    }
                    if (box.Min.Y > MainMapImage.FindAreaList[i].MinY)
                    {
                        box.Min.Y = MainMapImage.FindAreaList[i].MinY;
                    }
                    if (box.Max.X < MainMapImage.FindAreaList[i].MaxX)
                    {
                        box.Max.X = MainMapImage.FindAreaList[i].MaxX;
                    }
                    if (box.Max.Y < MainMapImage.FindAreaList[i].MaxY)
                    {
                        box.Max.Y = MainMapImage.FindAreaList[i].MaxY;
                    }
                }

                double centerx = (box.Min.X + box.Max.X) / 2;
                double centery = (box.Min.Y + box.Max.Y) / 2;
                X = centerx;
                Y = centery;
                EasyMap.Geometries.Point p1 = new EasyMap.Geometries.Point(centerx, centery);
                //if (!MainMapImage.Map.Envelope.Contains(p1))
                //{
                MainMapImage.Map.Center = p1;
                for (int i = 0; i < MainMapImage.FindAreaList.Count; i++)
                {
                    BoundingBox box1 = new BoundingBox(MainMapImage.FindAreaList[0].MinX, MainMapImage.FindAreaList[0].MinY, MainMapImage.FindAreaList[0].MaxX, MainMapImage.FindAreaList[0].MaxY);
                    if (!MainMapImage.Map.Envelope.Contains(box1))
                    {
                        MainMapImage.Map.ZoomToBox(box);
                        break;
                    }
                }

                MainMapImage.RequestFromServer = true;
                MainMapImage_MapCenterChanged(MainMapImage, MainMapImage.Map.Center);
                //}
                MainMapImage.Refresh();
                SaveFindSaveBoatItem(FindSaveBoat.Text.Trim());
                SetToolBarStatus();
                SetToolForm();
            }
            //���Ҹ�����Ԯ��
            Polygon marea = new Polygon();
            float _R = 0.0F;//���Ѵ���������Ԯ�ӵĹ�����
            DataTable table = MapDBClass.SelectRange();
            if (table.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(table.Rows[0]["range"].ToString()))
                {
                    _R = float.Parse(table.Rows[0]["range"].ToString());//���Ѵ���������Ԯ�ӵĹ�����
                }
            }
            EasyMap.Geometries.Point WorldPos = new GeoPoint(X,Y);
            marea.ExteriorRing.Vertices.Add(WorldPos);
            List<object> list = MainMapImage.Map.PickUpObject(MainMapImage.Size, marea, SELECTION_TYPE.CIRCLE_RADIO, _R, true, true);

            if (list != null && list.Count > 0)
            {
                for (int i = 1; i < list.Count; i += 2)
                {
                    string name = (list[i - 1] as ILayer).LayerName;
                    if (name != RESCUE_LAYER_NAME&& name !="���˻���Ԯע��")
                    {
                        continue;
                    }
                    Geometry geom = list[i] as Geometry;
                    MainMapImage.SelectObjects.Add(geom);
                    //if (!MainMapImage.SelectLayers.Contains(list[i - 1] as ILayer))
                    //{
                    //    MainMapImage.SelectLayers.Add(list[i - 1] as ILayer);
                    //}
                }
                MainMapImage.Refresh();
                SetToolBarStatus();
                SetToolForm();
            }
        }
        /// <summary>
        /// �����ѯ
        /// </summary>
        private void SaveFindSaveBoatItem(string findarea)
        {
            if (!FindSaveBoat.Items.Contains(findarea))
            {
                FindSaveBoat.Items.Insert(0, findarea);
            }
            int maxcount = 10;
            Int32.TryParse(Common.IniReadValue(CommandType.SERVER_SETTING_FILENAME, "FindAreaItem1", "MaxCount"), out maxcount);
            if (maxcount <= 0)
            {
                maxcount = 10;
            }
            int count = FindSaveBoat.Items.Count;
            if (count > maxcount)
            {
                count = maxcount;
            }
            while (FindSaveBoat.Items.Count > count)
            {
                FindSaveBoat.Items.RemoveAt(count);
            }
            Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "FindSaveBoat", "MaxCount", maxcount.ToString());
            Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "FindSaveBoat", "Count", count.ToString());

            for (int i = 0; i < count; i++)
            {
                Common.IniWriteValue(CommandType.SERVER_SETTING_FILENAME, "FindSaveBoat", "Item" + i, FindSaveBoat.Items[i].ToString());
            }
        }

        //������Ϣ
        private void BoatSetting_Click(object sender, EventArgs e)
        {
            if (MainMapImage.Map.CurrentLayer == null)
            {
                MessageBox.Show("��ѡ��һ��������");
                return;
            }
            if (!(MainMapImage.Map.CurrentLayer.LayerName.IndexOf(BOAT_LAYER_NAME) >= 0))
            {
                MessageBox.Show("��ѡ��һ��������");
                return;
            }
            if (boatForm != null && !boatForm.IsDisposed)
            {
                boatForm.Close();
            }
            boatForm = new BoatSettings(user._userName);
            boatForm.Map = MainMapImage.Map;
            //boatForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            boatForm.SelectObjectFromMap += new BoatSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            boatForm.SetPosition += new BoatSettings.SetPositionEvent(projectAddForm_SetPosition);
            boatForm.Search(MainMapImage.Map.Layers[MainMapImage.Map.CurrentLayer.LayerName].ID, MainMapImage.SelectObjects[0].ID, MainMapImage.SelectObjects[0].Text);
            boatForm.Show(this);
        }
        private void ����Ϊ���Ѵ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.Map.CurrentLayer == null)
            {
                MessageBox.Show("��ѡ��һ��������");
                return;
            }
            if (!(MainMapImage.Map.CurrentLayer.LayerName.IndexOf(BOAT_LAYER_NAME) >= 0))
            {
                MessageBox.Show("��ѡ��һ��������");
                return;
            }
            MapDBClass.problemBoatSet(MainMapImage.Map.MapId, MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID, MainMapImage.SelectObjects[0].ID);
            MessageBox.Show("���óɹ���");
        }
        //�Ѿȶ���Ϣ
        private void RescueBoatSetting_Click(object sender, EventArgs e)
        {
            if (MainMapImage.Map.CurrentLayer == null)
            {
                MessageBox.Show("��ѡ��һ����Ԯ�ӣ�");
                return;
            }
            if (!(MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_LAYER_NAME) >= 0))
            {
                MessageBox.Show("��ѡ��һ����Ԯ�ӣ�");
                return;
            }
            if (rescueBoatForm != null && !rescueBoatForm.IsDisposed)
            {
                rescueBoatForm.Close();
            }
            rescueBoatForm = new RescueSettings(user._userName);
            rescueBoatForm.Map = MainMapImage.Map;
            //rescueBoatForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            rescueBoatForm.SelectObjectFromMap += new RescueSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            rescueBoatForm.SetPosition += new RescueSettings.SetPositionEvent(projectAddForm_SetPosition);
            rescueBoatForm.Search(MainMapImage.Map.Layers[MainMapImage.Map.CurrentLayer.LayerName].ID, MainMapImage.SelectObjects[0].ID, MainMapImage.SelectObjects[0].Text);
            rescueBoatForm.Show(this);
        }

        private void �ɳ��ѾȶӴ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.Map.CurrentLayer == null)
            {
                MessageBox.Show("��ѡ��һ����Ԯ�ӣ�");
                return;
            }
            if (!(MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_LAYER_NAME) >= 0))
            {
                MessageBox.Show("��ѡ��һ����Ԯ�ӣ�");
                return;
            }
            if (sendRescueBoat != null && !sendRescueBoat.IsDisposed)
            {
                sendRescueBoat.Close();
            }
            sendRescueBoat = new SendRescueBoat(user._userName);
            sendRescueBoat.Map = MainMapImage.Map;
            //sendRescueBoat.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            sendRescueBoat.SelectObjectFromMap += new SendRescueBoat.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            sendRescueBoat.SetPosition += new SendRescueBoat.SetPositionEvent(projectAddForm_SetPosition);
            sendRescueBoat.length += new SendRescueBoat.Length(length);
            sendRescueBoat.refresh += new SendRescueBoat.Refresh(refresh);

            sendRescueBoat.addNewArea += new SendRescueBoat.AddNewArea(addNewArea);
            sendRescueBoat.addNewPoint += new SendRescueBoat.AddNewPoint(addNewPoint);
            sendRescueBoat.deleteArea += new SendRescueBoat.DeleteArea(deleteArea);
            sendRescueBoat.guiji += new SendRescueBoat.Guiji(showGuiji);

            sendRescueBoat.layerId = MainMapImage.Map.Layers[RESCUE_LAYER_NAME].ID;
            sendRescueBoat.geomId = MainMapImage.SelectObjects[0].ID;
            //sendRescueBoat.Search(MainMapImage.Map.Layers[MainMapImage.Map.CurrentLayer.LayerName].ID, MainMapImage.SelectObjects[0].ID);
            sendRescueBoat.Show(this);
        }

        private void ���˻���Ԯ��ϢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.Map.CurrentLayer == null)
            {
                MessageBox.Show("��ѡ��һ�����˻���");
                return;
            }
            if (!(MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_WURENJI_LAYER_NAME) >= 0))
            {
                MessageBox.Show("��ѡ��һ�����˻���");
                return;
            }
            if (wurenjiSettings != null && !wurenjiSettings.IsDisposed)
            {
                wurenjiSettings.Close();
            }
            wurenjiSettings = new WurenjiSettings(user._userName);
            wurenjiSettings.Map = MainMapImage.Map;
            wurenjiSettings.SelectObjectFromMap += new WurenjiSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            wurenjiSettings.SetPosition += new WurenjiSettings.SetPositionEvent(projectAddForm_SetPosition);
            wurenjiSettings.Search(MainMapImage.Map.Layers[MainMapImage.Map.CurrentLayer.LayerName].ID, MainMapImage.SelectObjects[0].ID, MainMapImage.SelectObjects[0].Text);
            wurenjiSettings.Show(this);
        }
        private void ������Ԯ�ӷ�Χ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = "";
            DataTable table = MapDBClass.SelectRange();
            if (table.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(table.Rows[0]["range"].ToString()))
                {
                    txt =table.Rows[0]["range"].ToString();//���Ѵ���������Ԯ�ӵĹ�����
                }
            }
            InputStringForm form = new InputStringForm("������Ԯ�ӷ�Χ����", txt);
            if (form.ShowDialog() == DialogResult.OK)
            {
                txt = form.InputContext;
            }
            else
            {
                return;
            }
            MapDBClass.InsertRange(txt);
        }

        private void ����ˢ��ʱ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt="";
            //��ѯ����ˢ��ʱ��
            DataTable table1 = MapDBClass.SelectRange();
            if (table1.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(table1.Rows[0]["range1"].ToString()))
                {
                    txt = table1.Rows[0]["range1"].ToString();
                }
            }
            InputStringForm form = new InputStringForm("����ˢ��ʱ�����ã����룩", txt);
            if (form.ShowDialog() == DialogResult.OK)
            {
                txt = form.InputContext;
            }
            else
            {
                return;
            }
            //����ˢ��ʱ�䵽���ݿ�
            MapDBClass.InsertRange1(txt);
            DataTable table = MapDBClass.SelectRange();
            if (table.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(table.Rows[0]["range1"].ToString()))
                {
                    timer6.Interval = int.Parse(table.Rows[0]["range1"].ToString());//����ˢ�´���ʱ��
                }
            }
           
        }

        private void �������Ѵ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            problemBoatSetting = new ProblemBoatSetting(user._userName);
            problemBoatSetting.Map = MainMapImage.Map;
            problemBoatSetting.SelectObjectFromMap += new ProblemBoatSetting.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            problemBoatSetting.SetPosition += new ProblemBoatSetting.SetPositionEvent(projectAddForm_SetPosition);
            problemBoatSetting.Show(this);
        }

        private void �ɳ���Ԯ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sendRescueBoat != null && !sendRescueBoat.IsDisposed)
            {
                sendRescueBoat.Close();
            }
            sendRescueBoat = new SendRescueBoat(user._userName);
            sendRescueBoat.Map = MainMapImage.Map;
            sendRescueBoat.SelectObjectFromMap += new SendRescueBoat.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            sendRescueBoat.SetPosition += new SendRescueBoat.SetPositionEvent(projectAddForm_SetPosition);
            sendRescueBoat.length += new SendRescueBoat.Length(length);
            sendRescueBoat.refresh += new SendRescueBoat.Refresh(refresh);
            sendRescueBoat.addNewArea += new SendRescueBoat.AddNewArea(addNewArea);
            sendRescueBoat.addNewPoint += new SendRescueBoat.AddNewPoint(addNewPoint);
            sendRescueBoat.deleteArea += new SendRescueBoat.DeleteArea(deleteArea);
            sendRescueBoat.guiji += new SendRescueBoat.Guiji(showGuiji);
            sendRescueBoat.layerId = MainMapImage.Map.Layers[RESCUE_LAYER_NAME].ID;
            //sendRescueBoat.geomId = MainMapImage.SelectObjects[0].ID;
            sendRescueBoat.Show(this);
        }

        private void ������Ϣ��ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoatSearch form = new BoatSearch(user._userName);
            GeometryProvider geoms = ((VectorLayer)MainMapImage.Map.Layers[QU_JIE_LAYER_NAME]).DataSource as GeometryProvider;
            foreach (Geometry geom in geoms.Geometries)
            {
                string name = geom.Text.Replace(" ", "");

                if (name == "����" || string.IsNullOrEmpty(name) || name == "�ƺ�" || name == "������")
                {
                    continue;
                }
                form.TaxDataList.Add(new TaxData() { LayerId = geom.LayerId, ObjectId = geom.ID, Name = geom.Text });
            }
            form.JiedaoLayerId = MainMapImage.Map.Layers[JIE_DAO_LAYER_NAME].ID;
            form.QujieLayerId = MainMapImage.Map.Layers[QU_JIE_LAYER_NAME].ID;
            form.JiefangLayerId = MainMapImage.Map.Layers[JIE_FANG_LAYER_NAME].ID;
            form.guiji += new BoatSearch.Guiji(showGuiji);
            form.MapId = MainMapImage.Map.MapId;
            form.Map = MainMapImage.Map;
            form.openBoatSetting += new BoatSearch.OpenBoatSetting(OpenBoatSetting);
            form.SetPosition += new BoatSearch.SetPositionEvent(projectAddForm_SetPosition);
            form.Show(this);
        }

        private void ��Ԯ����Ϣ��ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RescueBoatSearch form = new RescueBoatSearch(user._userName);
            GeometryProvider geoms = ((VectorLayer)MainMapImage.Map.Layers[QU_JIE_LAYER_NAME]).DataSource as GeometryProvider;
            foreach (Geometry geom in geoms.Geometries)
            {
                string name = geom.Text.Replace(" ", "");

                if (name == "����" || string.IsNullOrEmpty(name) || name == "�ƺ�" || name == "������")
                {
                    continue;
                }
                form.TaxDataList.Add(new TaxData() { LayerId = geom.LayerId, ObjectId = geom.ID, Name = geom.Text });
            }
            form.JiedaoLayerId = MainMapImage.Map.Layers[JIE_DAO_LAYER_NAME].ID;
            form.QujieLayerId = MainMapImage.Map.Layers[QU_JIE_LAYER_NAME].ID;
            form.JiefangLayerId = MainMapImage.Map.Layers[JIE_FANG_LAYER_NAME].ID;
            form.MapId = MainMapImage.Map.MapId;
            form.SetPosition += new RescueBoatSearch.SetPositionEvent(projectAddForm_SetPosition);
            form.openRescueBoatSetting += new RescueBoatSearch.OpenRescueBoatSetting(OpenRescueBoatSetting);
            form.Show(this);
        }
        //˫����ѯ�򿪴�����Ϣ����
        private void OpenBoatSetting(string no, decimal layerid, decimal objectid)
        {
            if (boatForm != null && !boatForm.IsDisposed)
            {
                boatForm.Close();
            }
            boatForm = new BoatSettings(user._userName);
            boatForm.Map = MainMapImage.Map;
            //boatForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            boatForm.SelectObjectFromMap += new BoatSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            boatForm.SetPosition += new BoatSettings.SetPositionEvent(projectAddForm_SetPosition);
            boatForm.SearchByNo(no, layerid, objectid);
            boatForm.Show(this);
        }
        //˫����ѯ�򿪾�Ԯ����Ϣ����
        private void OpenRescueBoatSetting(string no, decimal layerid, decimal objectid,string rescueName)
        {
            if (rescueBoatForm != null && !rescueBoatForm.IsDisposed)
            {
                rescueBoatForm.Close();
            }
            rescueBoatForm = new RescueSettings(user._userName);
            rescueBoatForm.Map = MainMapImage.Map;
            //boatForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            rescueBoatForm.SelectObjectFromMap += new RescueSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            rescueBoatForm.SetPosition += new RescueSettings.SetPositionEvent(projectAddForm_SetPosition);
           
            rescueBoatForm.Search(layerid, objectid, rescueName);
            rescueBoatForm.Show(this);
        }
        //˫����ѯ���ɳ���Ԯ����
        private void OpenSendRescueBoat(string no)
        {
            if (sendRescueBoat != null && !sendRescueBoat.IsDisposed)
            {
                sendRescueBoat.Close();
            }
            sendRescueBoat = new SendRescueBoat(user._userName);
            sendRescueBoat.Map = MainMapImage.Map;
            sendRescueBoat.SelectObjectFromMap += new SendRescueBoat.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            sendRescueBoat.SetPosition += new SendRescueBoat.SetPositionEvent(projectAddForm_SetPosition);

            sendRescueBoat.addNewArea += new SendRescueBoat.AddNewArea(addNewArea);
            sendRescueBoat.addNewPoint += new SendRescueBoat.AddNewPoint(addNewPoint);
            sendRescueBoat.deleteArea += new SendRescueBoat.DeleteArea(deleteArea);
            sendRescueBoat.guiji += new SendRescueBoat.Guiji(showGuiji);
            sendRescueBoat.Search(no);
            
            sendRescueBoat.Show(this);
        }
        //˫����ѯ�����˻���Ϣ����
        private void OpenWurenjiSetting(string no, decimal layerid, decimal objectid, string rescueName)
        {
            if (wurenjiSettings != null && !wurenjiSettings.IsDisposed)
            {
                wurenjiSettings.Close();
            }
            wurenjiSettings = new WurenjiSettings(user._userName);
            wurenjiSettings.Map = MainMapImage.Map;
            //boatForm.ShuiWuTable = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, MainMapImage.Map.Layers[TAX_LAYER_NAME].ID);
            wurenjiSettings.SelectObjectFromMap += new WurenjiSettings.SelectObjectFromMapEvent(projectAddForm_SelectObjectFromMap);
            wurenjiSettings.SetPosition += new WurenjiSettings.SetPositionEvent(projectAddForm_SetPosition);

            wurenjiSettings.SearchByNo(no, layerid, objectid, rescueName);
            wurenjiSettings.Show(this);
        }
        //Ƕ�������
        //WebBrowser webBrowser1 = new System.Windows.Forms.WebBrowser();
        /// <summary>
        /// �л��ߵµ�ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            if (btnMap.Checked)
            {
                //�л��ߵµ�ͼ
                //webBrowser1.Url = new Uri("E:/��ͼ�ٶȵ�ͼ/Easymap/Easymap/map.html");
                webBrowser1.Url = new Uri("http://uri.amap.com/marker?position=116.473195,39.993253");
                webBrowser1.ScriptErrorsSuppressed = true;

                //�ر�ԭ�е�ͼ���򿪸ߵµ�ͼ
                Form mapform1 = new Form();
                //mapform.Bounds = this.ClientRectangle;
                //mapform.SizeChanged += mapform_SizeChanged;
                //mapform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                
                //MapPanel1.Controls.Add(webBrowser1);
                //MapPanel2.Controls.Add(webBrowser1);
                //mapform1.Controls.Add(mapform.Controls[0]);
                //mapform1.Controls.Add(webBrowser1);
                //mapform.Controls[0].Controls.Add(webBrowser1);
                //mapform1.Controls.Add(mapform.Controls[0]);
                //mapform1.Controls.Add(webBrowser1);
                mapform1.Text = "��ͼ111";
                mapform1.Bounds = this.ClientRectangle;// new Rectangle(MapPanel1.Left, MapPanel1.Top, MapPanel1.Width, MapPanel1.Height);
                mapform1.SizeChanged += mapform_SizeChanged;
                mapform1.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                mapform1.Controls.Add(MapPanel1);
                MapPanel1.Dock = DockStyle.Fill;
                //mapform1.Controls.Add(MapPanel2);
                //MapPanel2.Left = MapPanel1.Left;
                //MapPanel2.Top = MapPanel1.Bottom;
                //MapPanel2.Width = MapPanel1.Width;
                //MapPanel2.Height = 0;
                //mapform1.Icon = this.Icon;
                //mapform1.Controls.Add(mapform.Controls[1]);
                //mapform.Controls[1] = webBrowser1;
                webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
                webBrowser1.Location = new System.Drawing.Point(0, 25);
                webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
                webBrowser1.Name = "webBrowser1";
                webBrowser1.Size = new System.Drawing.Size(484, 387);
                webBrowser1.TabIndex = 167;
                //mapform1.Icon = this.Icon;
               // mapform1.Text = "��ͼ111";
                dockContainer1.Remove(mapInfo);
                mapInfo1 = dockContainer1.Add(mapform1, zAllowedDock.All, new Guid(MAINFORMGUID));
                dockContainer1.DockForm(mapInfo1, DockStyle.Fill, zDockMode.None);
                MainMapImage.Refresh();
            }
            else
            {
                //MapPanel2.Visible = false;
                //MapPanel1.Visible = false;
                ////�л�����ͼ��
                dockContainer1.Remove(mapInfo1);//�Ƴ��ߵµ�ͼ
                mapInfo = dockContainer1.Add(mapform, zAllowedDock.All, new Guid(MAINFORMGUID));
                dockContainer1.DockForm(mapInfo, DockStyle.Fill, zDockMode.None);
            }
        }
        //�����Ԯ���봬������
        private LineString mline = new LineString();
        public double length(EasyMap.Geometries.Point p1, EasyMap.Geometries.Point p2)
        {
            mline = new LineString();
            EasyMap.Geometries.Point WorldPos = p1;
            mline.Vertices.Add(WorldPos);
            EasyMap.Geometries.Point WorldPos1 = p2;
            mline.Vertices.Add(WorldPos1);
            double length =mline.Length;
            return length;
        }

        private void ѡ�������ڴ�����ϢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.SelectObjects.Count <= 0)
            {
                return;
            }
            SelectAreaBoat report = new SelectAreaBoat();
            report.MapId = MainMapImage.Map.MapId;
            report.SelectGeometry = new List<Geometry>();
            //VectorLayer layer = MainMapImage.Map.Layers[TAX_LAYER_NAME] as VectorLayer;

            //GeometryProvider provider = layer.DataSource as GeometryProvider;
            for (int i = 0; i < MainMapImage.SelectObjects.Count; i++)
            {
                report.SelectGeometry.Add(MainMapImage.SelectObjects[i]);
            }

            report.AreaLayer = MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            report.Map = MainMapImage.Map;
            report.LayerId = report.AreaLayer.ID;
            report.LayerTableName = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, report.AreaLayer.ID);
            report.SetPosition += new SelectAreaBoat.SetPositionEvent(projectAddForm_SetPosition);
            report.openBoatSetting += new SelectAreaBoat.OpenBoatSetting(OpenBoatSetting);
            report.Show(this);
        }

        private void ѡ�������ھ�Ԯ����ϢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.SelectObjects.Count <= 0)
            {
                return;
            }
            SelectAreaRescue report = new SelectAreaRescue();
            report.MapId = MainMapImage.Map.MapId;
            report.SelectGeometry = new List<Geometry>();
            //VectorLayer layer = MainMapImage.Map.Layers[TAX_LAYER_NAME] as VectorLayer;

            //GeometryProvider provider = layer.DataSource as GeometryProvider;
            for (int i = 0; i < MainMapImage.SelectObjects.Count; i++)
            {
                report.SelectGeometry.Add(MainMapImage.SelectObjects[i]);
            }

            report.AreaLayer = MainMapImage.Map.Layers[RESCUE_LAYER_NAME];
            report.LayerId = report.AreaLayer.ID;
            report.LayerTableName = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, report.AreaLayer.ID);
            report.SetPosition += new SelectAreaRescue.SetPositionEvent(projectAddForm_SetPosition);
            report.openRescueBoatSetting += new SelectAreaRescue.OpenRescueBoatSetting(OpenRescueBoatSetting);
            report.Show(this);
        }

        private void ѡ�����������˻���ϢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.SelectObjects.Count <= 0)
            {
                return;
            }
            SelectAreaWurenji report = new SelectAreaWurenji();
            report.MapId = MainMapImage.Map.MapId;
            report.SelectGeometry = new List<Geometry>();
            //VectorLayer layer = MainMapImage.Map.Layers[TAX_LAYER_NAME] as VectorLayer;

            //GeometryProvider provider = layer.DataSource as GeometryProvider;
            for (int i = 0; i < MainMapImage.SelectObjects.Count; i++)
            {
                report.SelectGeometry.Add(MainMapImage.SelectObjects[i]);
            }

            report.AreaLayer = MainMapImage.Map.Layers[RESCUE_LAYER_NAME];
            report.LayerId = report.AreaLayer.ID;
            report.LayerTableName = string.Format("t_{0}_{1}", MainMapImage.Map.MapId, report.AreaLayer.ID);
            report.SetPosition += new SelectAreaWurenji.SetPositionEvent(projectAddForm_SetPosition);
            report.openWurenjiSetting += new SelectAreaWurenji.OpenWurenjiSetting(OpenWurenjiSetting);
            report.Show(this);
        }
        //�ɳ���Ԯ�󣬵�ͼ������һ����Ԯ�������Ԯ���˻�
        private void refresh(EasyMap.Geometries.Point point,string type)
        {
            VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[type];
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            provider.Geometries.Add(point);
            AddGeometryToTree(point, point.Text);
            SetToolBarStatus();
            SetToolForm();
            MainMapImage.Refresh();
        }

        private void �ɳ���Ԯ�����ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RescueReportSearch form = new RescueReportSearch(user._userName);
            GeometryProvider geoms = ((VectorLayer)MainMapImage.Map.Layers[QU_JIE_LAYER_NAME]).DataSource as GeometryProvider;
            foreach (Geometry geom in geoms.Geometries)
            {
                string name = geom.Text.Replace(" ", "");

                if (name == "����" || string.IsNullOrEmpty(name) || name == "�ƺ�" || name == "������")
                {
                    continue;
                }
                form.TaxDataList.Add(new TaxData() { LayerId = geom.LayerId, ObjectId = geom.ID, Name = geom.Text });
            }
            form.JiedaoLayerId = MainMapImage.Map.Layers[JIE_DAO_LAYER_NAME].ID;
            form.QujieLayerId = MainMapImage.Map.Layers[QU_JIE_LAYER_NAME].ID;
            form.JiefangLayerId = MainMapImage.Map.Layers[JIE_FANG_LAYER_NAME].ID;
            form.guiji += new RescueReportSearch.Guiji(showGuiji);
            form.MapId = MainMapImage.Map.MapId;
            form.SetPosition += new RescueReportSearch.SetPositionEvent(projectAddForm_SetPosition);
            form.openSendRescueBoat += new RescueReportSearch.OpenSendRescueBoat(OpenSendRescueBoat);
            form.Show(this);
        }

        //����������
        private void addNewArea(Polygon polygon, string name, out string objectId)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                //��ȡ�����ڵ�ͼ������objectid
                string selSql = "select max(ObjectId) from t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID;
                DataTable maxNoTable = SqlHelper.Select(conn, tran, selSql, null);
                objectId = "";
                if (maxNoTable.Rows[0][0].ToString() == string.Empty)
                {
                    objectId = "1";
                }
                else
                {
                    objectId = (Int32.Parse(maxNoTable.Rows[0][0].ToString()) + 1).ToString();
                }
                //���ؿ鱣�浽�����ڵ�ͼ����
                List<SqlParameter> param = new List<SqlParameter>();
                param.Clear();
                param.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
                param.Add(new SqlParameter("LayerId", MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID));
                param.Add(new SqlParameter("ObjectId", objectId));
                param.Add(new SqlParameter("propertydate", DateTime.Now.Date.ToString().Substring(0, 10)));
                param.Add(new SqlParameter("UpdateDate", DateTime.Now.Date.ToString().Substring(0, 10)));
                param.Add(new SqlParameter("ZJMC", name));
                string sql = "INSERT INTO t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID + "(MapId,LayerId ,ObjectId,propertydate,UpdateDate,ZJMC,�Ƿ�����,������������ѵ�) VALUES(@MapId,@LayerId ,@ObjectId,@propertydate,@UpdateDate,@ZJMC,'��','2')";
                SqlHelper.Insert(conn, tran, sql, param);
                tran.Commit();
                conn.Close();
                //����ؿ鵽object
                polygon.ID = decimal.Parse(objectId);
                MapDBClass.InsertObject(MainMapImage.Map.MapId, MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID, polygon);
                MainMapImage.Refresh();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                MessageBox.Show("��������ʧ��");
                objectId = string.Empty;
                return;
            }
            //ȡ�õ�ǰͼ��
            VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            //ȡ��ͼ������Դ
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            //��ӵ�ǰ����Ķ����
            provider.Geometries.Add(polygon);

            AddGeometryToTree(polygon, polygon.Text);
            //ǿ�Ƶ�ͼˢ��
            MainMapImage.Refresh();
            //��ʱ�����ɫ
            LinshiTudiColor_Click(Decimal.Parse(objectId));
        }
        //���������ѵ�
        private void addNewPoint(EasyMap.Geometries.Point point, string name, out string objectId)
        {
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                //��ȡ�����ڵ�ͼ������objectid
                string selSql = "select max(ObjectId) from t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID;
                DataTable maxNoTable = SqlHelper.Select(conn, tran, selSql, null);
                objectId = "";
                if (maxNoTable.Rows[0][0].ToString() == string.Empty)
                {
                    objectId = "1";
                }
                else
                {
                    objectId = (Int32.Parse(maxNoTable.Rows[0][0].ToString()) + 1).ToString();
                }
                //���ؿ鱣�浽���ѵ�ע��ͼ����
                List<SqlParameter> param = new List<SqlParameter>();
                param.Clear();
                param.Add(new SqlParameter("MapId", MainMapImage.Map.MapId));
                param.Add(new SqlParameter("LayerId", MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID));
                param.Add(new SqlParameter("ObjectId", objectId));
                param.Add(new SqlParameter("propertydate", DateTime.Now.Date.ToString().Substring(0, 10)));
                param.Add(new SqlParameter("UpdateDate", DateTime.Now.Date.ToString().Substring(0, 10)));
                param.Add(new SqlParameter("ZJMC", name));
                param.Add(new SqlParameter("FLDM", "4"));
                string sql = "INSERT INTO t_" + MainMapImage.Map.MapId + "_" + MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID + "(MapId,LayerId ,ObjectId,propertydate,UpdateDate,ZJMC,FLDM,�Ƿ�����,������������ѵ�) VALUES(@MapId,@LayerId ,@ObjectId,@propertydate,@UpdateDate,@ZJMC,@FLDM,'��','1')";
                SqlHelper.Insert(conn, tran, sql, param);
                tran.Commit();
                conn.Close();
                //����ؿ鵽object
                point.ID = decimal.Parse(objectId);
                MapDBClass.InsertObject(MainMapImage.Map.MapId, MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID, point);
                MainMapImage.Refresh();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                MessageBox.Show("��������ʧ��");
                objectId = string.Empty;
                return;
            }
            //ȡ�õ�ǰͼ��
            VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            //ȡ��ͼ������Դ
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            //��ӵ�ǰ����Ķ����
            point.Text = name;
            EasyMap.Geometries.Point p = new EasyMap.Geometries.Point();
            p = point.Clone();
            p.ID = point.ID;
            p.Text = point.Text;
            provider.Geometries.Add(p);
            LoadSymbol();
            AddGeometryToTree(p, p.Text);

            //ǿ�Ƶ�ͼˢ��
            MainMapImage.Refresh();
            //��ʱ�����ɫ
            //LinshiTudiColor_Click(Decimal.Parse(objectId));
        }
        //����ʱ�����ɫ
        private void LinshiTudiColor_Click(decimal objectId)
        {
            GeometryProvider geoms = (MainMapImage.Map.Layers[BOAT_LAYER_NAME] as VectorLayer).DataSource as GeometryProvider;
            decimal[] keys = new decimal[ColorTable.Keys.Count];
            ColorTable.Keys.CopyTo(keys, 0);
            UndefineColor = colorDialog1.Color;
            ColorTable.Clear();
            GeometryProvider geoms1 = (MainMapImage.Map.Layers[BOAT_LAYER_NAME] as VectorLayer).DataSource as GeometryProvider;
            foreach (Geometry obj in geoms1.Geometries)
            {
                if (obj.ID == objectId)
                {
                    ColorTable.Add(objectId, new object[] 
                        { 
                            objectId
                            ,obj.Fill
                            ,obj.Outline
                            ,obj.OutlineWidth
                            ,obj.TextColor
                            ,obj.TextFont
                            ,obj.EnableOutline
                            ,obj.HatchStyle
                            ,obj.Line
                            ,obj.StyleType
                            ,obj.Penstyle
                        });
                    obj.Outline = Color.Red.ToArgb();
                    obj.TextColor = Color.Black;
                    obj.OutlineWidth = 2;
                    obj.Line = UndefineColor.ToArgb();
                    obj.StyleType = 1;
                    obj.EnableOutline = true;
                    break;
                }
            }

            LoadSymbol();
            MainMapImage.Refresh();
        }
        //ɾ����ʱ����
        private void deleteArea(Geometry geom)
        {
            GeometryProvider provider = (MainMapImage.Map.Layers[BOAT_LAYER_NAME] as VectorLayer).DataSource as GeometryProvider;
            //if (MainMapImage.NeedSave && MainMapImage.Map.Layers[BOAT_LAYER_NAME].NeedSave)
            //{
            MapDBClass.DeleteObject(MainMapImage.Map.MapId, MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID, geom.ID, MainMapImage.Map.Layers[BOAT_LAYER_NAME].Type);
            Command.SendDeleteObjectCommand(MainMapImage.Map.MapId, MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID, geom.ID);
            //}
            if (provider.Geometries.Contains(geom))
            {
                provider.Geometries.Remove(geom);
            }
            DeleteGeometryFromTree(geom);
            MainMapImage.SelectObjects.Remove(geom);

            //ǿ�Ƶ�ͼˢ��
            MainMapImage.RequestFromServer = false;
            MainMapImage.Refresh();
            SetToolBarStatus();
        }
        private List<LineString> linList = new List<LineString>();
        private List<EasyMap.Geometries.Point> pList = new List<EasyMap.Geometries.Point>();
        //��ʾ�켣
        private void btnGuiji_Click(object sender, EventArgs e)
        {
            //δѡ��ע��
            if (MainMapImage.SelectObjects.Count == 0)
            {
                return;
            }
            DateTime date_begin;//��ʼʱ��
            DateTime date_off;//����ʱ��
            GuijiSearchForm form = new GuijiSearchForm(MainMapImage.SelectObjects[0].Text,new DateTime(),new DateTime());
            if (form.ShowDialog() == DialogResult.OK)
            {
                date_begin = form.dateBegin;
                date_off = form.dateOff;
            }
            else
            {
                btnGuiji.Checked = false;
                return;
            }
             SqlConnection conn = null;
             SqlTransaction tran = null;
             List<SqlParameter> param = new List<SqlParameter>();
            try
            {
                conn=SqlHelper.GetConnection();
                conn.Open();
                tran=conn.BeginTransaction();
                param.Clear();
                SqlHelper.Delete(conn, tran, SqlHelper.GetSql("DeleteTempPoint"), null);
                string sql = SqlHelper.GetSql("InsertTempPoint");
                int id = 0;
                for (int i = 0; i < MainMapImage.SelectObjects.Count; i++)
                {
                    EasyMap.Geometries.Geometry geom = MainMapImage.SelectObjects[i];
                    param.Clear();
                    param.Add(new SqlParameter("id", id.ToString()));
                    param.Add(new SqlParameter("Name", geom.Text));
                    SqlHelper.Insert(conn, tran, sql, param);
                    id++;
                }
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    conn.Close();
                }
                MessageBox.Show(ex.Message);
            }
;            //��ȡȡ�켣ʱ��
            //DateTime dateTime = DateTime.Now.AddMinutes(-time);
            //��ȡ�켣����
            //List<SqlParameter> param = new List<SqlParameter>();
            param.Clear();
            param.Add(new SqlParameter("CreateDate1", date_begin));
            param.Add(new SqlParameter("CreateDate2", date_off));
            param.Add(new SqlParameter("LayerId1", MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID));
            param.Add(new SqlParameter("LayerId2", MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME].ID));
            param.Add(new SqlParameter("LayerId3", MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME].ID));
            DataTable table = SqlHelper.Select(SqlHelper.GetSql("SelectGuiji"), param);
            LineString line = new LineString();
            IList<EasyMap.Geometries.Point> pointList = line.Vertices;
            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
                //ȡ��ͼ��
                if (table.Rows[i]["LayerId"].ToString() == MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME].ID.ToString())
                {
                    layer = (VectorLayer)MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME];
                }
                else if (table.Rows[i]["LayerId"].ToString() == MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME].ID.ToString())
                {
                    layer = (VectorLayer)MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME];
                }
                //ȡ��ͼ������Դ
                GeometryProvider provider = (GeometryProvider)layer.DataSource;
                if (i == 0)
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);
                    //��ӵ�ǰ�����
                    provider.Geometries.Add(point);

                    AddGeometryToTree(point, point.Text);
                }
                else if (i == table.Rows.Count - 1)
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);
                    //��ӵ�ǰ�����
                    provider.Geometries.Add(point);

                    AddGeometryToTree(point, point.Text); 
                    guijiStyle(line);
                    linList.Add(line);

                    //��ӵ�ǰ�����
                    provider.Geometries.Add(line);

                    AddGeometryToTree(line, line.Text);
                    //ǿ�Ƶ�ͼˢ��
                    MainMapImage.Refresh();
                    line = new LineString();

                }
                else
                {
                    if (table.Rows[i]["Name"].ToString() == table.Rows[i - 1]["Name"].ToString())
                    {
                        point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                        pointList = line.Vertices;
                        pointList.Add(point);
                        pList.Add(point);

                    }
                    else
                    {
                        guijiStyle(line);
                        linList.Add(line);
                        provider.Geometries.Add(line);

                        AddGeometryToTree(line, line.Text);
                        //ǿ�Ƶ�ͼˢ��
                        MainMapImage.Refresh();
                        line = new LineString();
                        point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                        pointList = line.Vertices;
                        pointList.Add(point);
                        pList.Add(point);
                        //��ӵ�ǰ�����
                        provider.Geometries.Add(point);

                        AddGeometryToTree(point, point.Text);

                        EasyMap.Geometries.Point pointOld = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i - 1]["ObjectData"]);
                        //��ӵ�ǰ�����
                        provider.Geometries.Add(pointOld);
                        AddGeometryToTree(pointOld, pointOld.Text);
                    }
                }
                point.LayerId = layer.ID;
            }
            BoundingBox box = new BoundingBox(point.X, point.Y, point.X, point.Y);
            taxForm_DoubleClickObject(box, point.LayerId, point.ID);
        }

        //��ʾ�����켣
        private void �켣ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainMapImage.Map.CurrentLayer == null)
            {
                MessageBox.Show("��ѡ��һ�����������˻���");
                return;
            }
            if (!(MainMapImage.Map.CurrentLayer.LayerName.IndexOf(BOAT_LAYER_NAME) >= 0 || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_BOAT_LAYER_NAME) >= 0 || MainMapImage.Map.CurrentLayer.LayerName.IndexOf(RESCUE_WURENJI_LAYER_NAME) >= 0))
            {
                MessageBox.Show("��ѡ��һ�����������˻���");
                return;
            }
            //�Ѿ���ʾ�켣����ȡ���켣
            //if (true)
            //{
            //linList = new List<LineString>();
            //pList = new List<EasyMap.Geometries.Point>();
            //string txt = "";
            DateTime date_begin;//��ʼʱ��
            DateTime date_off;//����ʱ��
            //InputStringForm form = new InputStringForm("������Ҫ��ʾ�������ڵĹ켣", txt);
            GuijiSearchForm form = new GuijiSearchForm(MainMapImage.SelectObjects[0].Text, new DateTime(), new DateTime());
            if (form.ShowDialog() == DialogResult.OK)
            {
                date_begin = form.dateBegin;
                date_off = form.dateOff;
            }
            else
            {
                btnGuiji.Checked = false;
                return;
            }
            //��������ķ���������ʾ�켣
            //double time = 0.0;
            //try
            //{
            //    time = double.Parse(txt);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("���������֡�");
            //}
            //��ȡȡ�켣ʱ��
            //DateTime dateTime = DateTime.Now.AddMinutes(-time);
            //��ȡ�켣����
            List<SqlParameter> param = new List<SqlParameter>();
            param.Clear();
            param.Add(new SqlParameter("CreateDate1", date_begin));
            param.Add(new SqlParameter("CreateDate2", date_off));
            param.Add(new SqlParameter("LayerId", MainMapImage.Map.CurrentLayer.ID));
            param.Add(new SqlParameter("Name", MainMapImage.SelectObjects[0].Text));
            DataTable table = SqlHelper.Select(SqlHelper.GetSql("SelectGuijiById"), param);
            LineString line = new LineString();
            IList<EasyMap.Geometries.Point> pointList = line.Vertices;
            //ȡ�õ�ǰͼ��
            VectorLayer layer = (VectorLayer)MainMapImage.Map.CurrentLayer;
            //ȡ��ͼ������Դ
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i == 0)
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);
                    //��ӵ�ǰ�����
                    provider.Geometries.Add(point);

                    AddGeometryToTree(point, point.Text);
                }
                else if (i == table.Rows.Count - 1)
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);
                    //��ӵ�ǰ�����
                    provider.Geometries.Add(point);

                    AddGeometryToTree(point, point.Text);
                    guijiStyle(line);
                    linList.Add(line);

                    //��ӵ�ǰ�����
                    provider.Geometries.Add(line);

                    AddGeometryToTree(line, line.Text);
                    //ǿ�Ƶ�ͼˢ��
                    MainMapImage.Refresh();
                    line = new LineString();

                }
                else
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);

                }
                point.LayerId = layer.ID;
            }
            BoundingBox box = new BoundingBox(point.X, point.Y, point.X, point.Y);
            taxForm_DoubleClickObject(box, point.LayerId, point.ID);
        }

        private void showGuiji(string name, decimal layerId, decimal geomId, string dataBegin, string dateEnd)
        {
            DateTime date_begin = new DateTime();//��ʼʱ��
            DateTime date_off = new DateTime();//����ʱ��
            if (!string.IsNullOrEmpty(dataBegin))
            {
                date_begin = DateTime.Parse(dataBegin);
            }
            if (!string.IsNullOrEmpty(dateEnd.Trim()))
            {
                date_off = DateTime.Parse(dateEnd);
            }
            GuijiSearchForm form = new GuijiSearchForm(name, date_begin, date_off);
            if (form.ShowDialog() == DialogResult.OK)
            {
                date_begin = form.dateBegin;
                date_off = form.dateOff;
            }
            else
            {
                btnGuiji.Checked = false;
                return;
            }
            //��ȡ�켣����
            List<SqlParameter> param = new List<SqlParameter>();
            param.Clear();
            param.Add(new SqlParameter("CreateDate1", date_begin));
            param.Add(new SqlParameter("CreateDate2", date_off));
            param.Add(new SqlParameter("LayerId", layerId));
            param.Add(new SqlParameter("Name", name));
            DataTable table = SqlHelper.Select(SqlHelper.GetSql("SelectGuijiById"), param);
            LineString line = new LineString();
            IList<EasyMap.Geometries.Point> pointList = line.Vertices;
            //ȡ��ͼ��
            VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            if (layerId == MainMapImage.Map.Layers[BOAT_LAYER_NAME].ID)
            {
                layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            }
            else if (layerId == MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME].ID)
            {
                layer = (VectorLayer)MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME];
            }
            else if (layerId == MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME].ID)
            {
                layer = (VectorLayer)MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME];
            }
            //ȡ��ͼ������Դ
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i == 0)
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);
                    //��ӵ�ǰ�����
                    provider.Geometries.Add(point);

                    AddGeometryToTree(point, point.Text);
                }
                else if (i == table.Rows.Count - 1)
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);
                    //��ӵ�ǰ�����
                    provider.Geometries.Add(point);

                    AddGeometryToTree(point, point.Text);
                    guijiStyle(line);
                    linList.Add(line);

                    //��ӵ�ǰ�����
                    provider.Geometries.Add(line);

                    AddGeometryToTree(line, line.Text);
                    //ǿ�Ƶ�ͼˢ��
                    MainMapImage.Refresh();
                    line = new LineString();

                }
                else
                {
                    point = (EasyMap.Geometries.Point)DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                    pointList = line.Vertices;
                    pointList.Add(point);
                    pList.Add(point);

                }
                point.LayerId = layer.ID;
            }
            if (!point.IsEmpty())
            {
                BoundingBox box = new BoundingBox(point.X, point.Y, point.X, point.Y);
                taxForm_DoubleClickObject(box, point.LayerId, point.ID);
            }
        }

        //���ù켣��ʽ
        private void guijiStyle(LineString line)
        {
            line.Outline = Color.FromArgb(51, 122, 174).ToArgb();
            line.TextColor = Color.Black;
            line.OutlineWidth = 6;
            line.Line = UndefineColor.ToArgb();
            line.StyleType = 1;
            line.EnableOutline = true;
        }

        //����켣
        private void btnClearGuiji_Click(object sender, EventArgs e)
        {
            //����켣��Ϣ
            //ȡ�õ�ǰͼ��
            VectorLayer layer = (VectorLayer)MainMapImage.Map.Layers[BOAT_LAYER_NAME];
            VectorLayer layer1 = (VectorLayer)MainMapImage.Map.Layers[RESCUE_BOAT_LAYER_NAME];
            VectorLayer layer2 = (VectorLayer)MainMapImage.Map.Layers[RESCUE_WURENJI_LAYER_NAME];
            //ȡ��ͼ������Դ
            GeometryProvider provider = (GeometryProvider)layer.DataSource;
            GeometryProvider provider1 = (GeometryProvider)layer1.DataSource;
            GeometryProvider provider2 = (GeometryProvider)layer2.DataSource;
            for (int i = 0; i < linList.Count; i++)
            {
                provider.Geometries.Remove(linList[i]);
                provider1.Geometries.Remove(linList[i]);
                provider2.Geometries.Remove(linList[i]);
                DeleteGeometryFromTree(linList[i]);
                MainMapImage.SelectObjects.Remove(linList[i]);
            }
            linList = new List<LineString>();
            for (int j = 0; j < pList.Count; j++)
            {
                provider.Geometries.Remove(pList[j]);
                provider1.Geometries.Remove(pList[j]);
                provider2.Geometries.Remove(pList[j]);
                DeleteGeometryFromTree(pList[j]);
                MainMapImage.SelectObjects.Remove(pList[j]);
            }
            pList = new List<GeoPoint>();
            LoadSymbol();
            MainMapImage.Refresh();

        }
    }

}