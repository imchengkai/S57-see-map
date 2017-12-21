// Copyright 2007 - Rory Plaire (codekaizen@gmail.com)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using EasyMap.Controls;
using EsayMap;
namespace EasyMap
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("图层");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            EasyMap.Controls.MyGeometryList myGeometryList1 = new EasyMap.Controls.MyGeometryList();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("图层");
            EasyMap.Controls.MyGeometryList myGeometryList2 = new EasyMap.Controls.MyGeometryList();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.info = new System.Windows.Forms.ToolStripStatusLabel();
            this.CoordinatesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ZuoBiaoLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LayerView2 = new EasyMap.MyTree();
            this.LayerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MoveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.LayerContextMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.AddLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutPutShp = new System.Windows.Forms.ToolStripMenuItem();
            this.MergeShp = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MapPanel2 = new System.Windows.Forms.Panel();
            this.picFlash2 = new System.Windows.Forms.PictureBox();
            this.myToolTipControl2 = new EasyMap.Controls.MyToolTipControl();
            this.MainMapImage2 = new EasyMap.Forms.MapImage();
            this.MapContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全图显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomAreatoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomToSelectObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearSelectObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearAreaPriceFill = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyXYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mesureLengthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mesureAreaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.XYtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPolygonTempStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProblemPointStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddProblemPolygonStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPriceMotionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePriceMotionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSetObjectStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDeleteObjectStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船舶信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.轨迹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置为遇难船舶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜救队信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.派出搜救队船舶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.无人机救援信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MyMapEditImage2 = new EasyMap.UI.Forms.MapEditImage(this.components);
            this.projectControl1 = new EasyMap.Controls.ProjectControl();
            this.LayerView1 = new EasyMap.MyTree();
            this.MapPanel1 = new System.Windows.Forms.Panel();
            this.picFlash1 = new System.Windows.Forms.PictureBox();
            this.myToolTipControl1 = new EasyMap.Controls.MyToolTipControl();
            this.btnMapToImageCancel = new EasyMap.Controls.MyButton();
            this.btnMapToImageOk = new EasyMap.Controls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            this.waiting1 = new EasyMap.Controls.Waiting();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.mapControl1 = new EasyMap.MapControl();
            this.MainMapImage1 = new EasyMap.Forms.MapImage();
            this.MyMapEditImage1 = new EasyMap.UI.Forms.MapEditImage(this.components);
            this.dockContainer1 = new Crom.Controls.Docking.DockContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.passwordUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.printsetuptoolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pic_out = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertyDefineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.InsertLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基础图层ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.宗地信息图层ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.其他图层ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.影像图图层ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.基础图层ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.宗地信息图层ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.其他图层ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertPricePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteLayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAreaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePricePointToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.影像图层级设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改地块上土地注记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改图层分类样式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新生成管辖视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomToExtentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLayerView = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDataView = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPropertyView = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPictureView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.btnProjectView = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TifSplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPropertyControl = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomReportMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.btnParameterSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索救援队范围设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船舶刷新时间设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mesure1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mesure2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.船舶信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.救援队信息查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.派出救援报告查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选中区域内查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选中区域内船舶信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选中区域内救援队信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选中区域内无人机信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.派出救援队ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置遇难船舶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanxian = new System.Windows.Forms.ToolStripMenuItem();
            this.权限设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理员ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.AddNewRandomGeometryLayer = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.AddLayerToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
            this.影像图图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基础图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.评估报告图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.监测点图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交易案例图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.宗地信息图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.房屋售价图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.房屋租赁图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveLayerToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PrintToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.btnSelect = new System.Windows.Forms.ToolStripSplitButton();
            this.btnRectangel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFreeCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFree = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SelecttoolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.PanToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomInModeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutModeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomToExtentsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomAreatoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ZoomtoolStripTextBox = new System.Windows.Forms.ToolStripComboBox();
            this.btnMap = new System.Windows.Forms.ToolStripButton();
            this.btnLoadPicture = new System.Windows.Forms.ToolStripButton();
            this.btnGuiji = new System.Windows.Forms.ToolStripButton();
            this.btnClearGuiji = new System.Windows.Forms.ToolStripButton();
            this.btnLoadShiQu = new System.Windows.Forms.ToolStripButton();
            this.btnTax = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.MesuretoolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.距离量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.面积量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertytoolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.坐标录入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shp文件导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动绘画ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InputDatatoolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowPriceColor = new System.Windows.Forms.ToolStripButton();
            this.CompareToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ReporttoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FindSaveBoat = new System.Windows.Forms.ToolStripComboBox();
            this.btnFindSaveBoat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FindAreaTextBox = new System.Windows.Forms.ToolStripComboBox();
            this.btnFindArea = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnMapToImage = new System.Windows.Forms.ToolStripButton();
            this.btnEditLayerList = new System.Windows.Forms.ToolStripSplitButton();
            this.lblCurrentLayer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDataSearch = new System.Windows.Forms.ToolStripButton();
            this.救援报告ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.AddLayerDialog = new System.Windows.Forms.OpenFileDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.timer6 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.LayerContextMenu.SuspendLayout();
            this.MapPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFlash2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapImage2)).BeginInit();
            this.MapContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MyMapEditImage2)).BeginInit();
            this.MapPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFlash1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyMapEditImage1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.MainStatusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripContainer1.ContentPanel.Controls.Add(this.LayerView2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.MapPanel2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.projectControl1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.LayerView1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.MapPanel1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dockContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1170, 390);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1170, 461);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainToolStrip);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainStatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.info,
            this.CoordinatesLabel,
            this.ZuoBiaoLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 0);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1170, 22);
            this.MainStatusStrip.TabIndex = 0;
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.info.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.info.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.info.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.info.ForeColor = System.Drawing.Color.Crimson;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(452, 17);
            this.info.Spring = true;
            this.info.Text = "存在比对不符的数据";
            this.info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.info.Visible = false;
            // 
            // CoordinatesLabel
            // 
            this.CoordinatesLabel.AutoSize = false;
            this.CoordinatesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CoordinatesLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.CoordinatesLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.CoordinatesLabel.Name = "CoordinatesLabel";
            this.CoordinatesLabel.Size = new System.Drawing.Size(905, 17);
            this.CoordinatesLabel.Spring = true;
            // 
            // ZuoBiaoLabel
            // 
            this.ZuoBiaoLabel.AutoSize = false;
            this.ZuoBiaoLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.ZuoBiaoLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.ZuoBiaoLabel.Name = "ZuoBiaoLabel";
            this.ZuoBiaoLabel.Size = new System.Drawing.Size(250, 17);
            this.ZuoBiaoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LayerView2
            // 
            this.LayerView2.AllowDrop = true;
            this.LayerView2.CheckBoxes = true;
            this.LayerView2.ContextMenuStrip = this.LayerContextMenu;
            this.LayerView2.HideSelection = false;
            this.LayerView2.ImageIndex = 0;
            this.LayerView2.ImageList = this.imageList1;
            this.LayerView2.LabelEdit = true;
            this.LayerView2.Location = new System.Drawing.Point(24, 206);
            this.LayerView2.MySelectedNode = null;
            this.LayerView2.Name = "LayerView2";
            treeNode1.Name = "root";
            treeNode1.Text = "图层";
            this.LayerView2.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.LayerView2.SelectedImageIndex = 0;
            this.LayerView2.Size = new System.Drawing.Size(168, 57);
            this.LayerView2.TabIndex = 2;
            this.LayerView2.Visible = false;
            this.LayerView2.NodeImageClick += new EasyMap.MyTree.OnNodeImageClickEvent(this.LayerView_NodeImageClick);
            this.LayerView2.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LayerView_BeforeLabelEdit);
            this.LayerView2.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LayerView_AfterLabelEdit);
            this.LayerView2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.LayerView_AfterCheck);
            this.LayerView2.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LayerView_ItemDrag);
            this.LayerView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LayerView_AfterSelect);
            this.LayerView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.LayerView_DragDrop);
            this.LayerView2.DragEnter += new System.Windows.Forms.DragEventHandler(this.LayerView_DragEnter);
            this.LayerView2.Enter += new System.EventHandler(this.LayerView1_Enter);
            // 
            // LayerContextMenu
            // 
            this.LayerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveUpToolStripMenuItem,
            this.MoveDownToolStripMenuItem,
            this.btnZoomToLayer,
            this.btnEditLayer,
            this.LayerContextMenuSeparator,
            this.AddLayerToolStripMenuItem,
            this.RemoveLayerToolStripMenuItem,
            this.toolStripMenuItem3,
            this.DeleteObjectToolStripMenuItem,
            this.SearchToolStripMenuItem,
            this.VisibleToolStripMenuItem,
            this.OutPutShp,
            this.MergeShp});
            this.LayerContextMenu.Name = "LayerContextMenu";
            this.LayerContextMenu.Size = new System.Drawing.Size(161, 258);
            this.LayerContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.LayerContextMenu_Opening);
            // 
            // MoveUpToolStripMenuItem
            // 
            this.MoveUpToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveUpToolStripMenuItem.Image")));
            this.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem";
            this.MoveUpToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MoveUpToolStripMenuItem.Text = "上移";
            this.MoveUpToolStripMenuItem.ToolTipText = "上移";
            this.MoveUpToolStripMenuItem.Click += new System.EventHandler(this.MoveUpToolStripMenuItem_Click);
            // 
            // MoveDownToolStripMenuItem
            // 
            this.MoveDownToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveDownToolStripMenuItem.Image")));
            this.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem";
            this.MoveDownToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MoveDownToolStripMenuItem.Text = "下移";
            this.MoveDownToolStripMenuItem.ToolTipText = "下移";
            this.MoveDownToolStripMenuItem.Click += new System.EventHandler(this.MoveDownToolStripMenuItem_Click);
            // 
            // btnZoomToLayer
            // 
            this.btnZoomToLayer.Name = "btnZoomToLayer";
            this.btnZoomToLayer.Size = new System.Drawing.Size(160, 22);
            this.btnZoomToLayer.Text = "缩放到图层范围";
            this.btnZoomToLayer.ToolTipText = "缩放到图层范围";
            this.btnZoomToLayer.Click += new System.EventHandler(this.btnZoomToLayer_Click);
            // 
            // btnEditLayer
            // 
            this.btnEditLayer.Name = "btnEditLayer";
            this.btnEditLayer.Size = new System.Drawing.Size(160, 22);
            this.btnEditLayer.Text = "编辑图层";
            this.btnEditLayer.ToolTipText = "编辑图层";
            this.btnEditLayer.Visible = false;
            this.btnEditLayer.Click += new System.EventHandler(this.btnEditLayer_Click);
            // 
            // LayerContextMenuSeparator
            // 
            this.LayerContextMenuSeparator.Name = "LayerContextMenuSeparator";
            this.LayerContextMenuSeparator.Size = new System.Drawing.Size(157, 6);
            this.LayerContextMenuSeparator.Visible = false;
            // 
            // AddLayerToolStripMenuItem
            // 
            this.AddLayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddLayerToolStripMenuItem.Image")));
            this.AddLayerToolStripMenuItem.Name = "AddLayerToolStripMenuItem";
            this.AddLayerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.AddLayerToolStripMenuItem.Text = "添加图层";
            this.AddLayerToolStripMenuItem.ToolTipText = "添加图层";
            this.AddLayerToolStripMenuItem.Visible = false;
            this.AddLayerToolStripMenuItem.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // RemoveLayerToolStripMenuItem
            // 
            this.RemoveLayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RemoveLayerToolStripMenuItem.Image")));
            this.RemoveLayerToolStripMenuItem.Name = "RemoveLayerToolStripMenuItem";
            this.RemoveLayerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RemoveLayerToolStripMenuItem.Text = "删除图层";
            this.RemoveLayerToolStripMenuItem.ToolTipText = "删除图层";
            this.RemoveLayerToolStripMenuItem.Click += new System.EventHandler(this.RemoveLayerToolStripButton_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 6);
            // 
            // DeleteObjectToolStripMenuItem
            // 
            this.DeleteObjectToolStripMenuItem.Name = "DeleteObjectToolStripMenuItem";
            this.DeleteObjectToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.DeleteObjectToolStripMenuItem.Text = "删除元素";
            this.DeleteObjectToolStripMenuItem.ToolTipText = "删除元素";
            this.DeleteObjectToolStripMenuItem.Visible = false;
            this.DeleteObjectToolStripMenuItem.Click += new System.EventHandler(this.DeletePolygonToolStripMenuItem_Click);
            // 
            // SearchToolStripMenuItem
            // 
            this.SearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SearchToolStripMenuItem.Image")));
            this.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem";
            this.SearchToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.SearchToolStripMenuItem.Text = "数据查询";
            this.SearchToolStripMenuItem.ToolTipText = "数据查询";
            this.SearchToolStripMenuItem.Click += new System.EventHandler(this.SearchToolStripMenuItem_Click);
            // 
            // VisibleToolStripMenuItem
            // 
            this.VisibleToolStripMenuItem.Name = "VisibleToolStripMenuItem";
            this.VisibleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.VisibleToolStripMenuItem.Text = "可见性设置";
            this.VisibleToolStripMenuItem.ToolTipText = "可见性设置";
            this.VisibleToolStripMenuItem.Click += new System.EventHandler(this.VisibleToolStripMenuItem_Click);
            // 
            // OutPutShp
            // 
            this.OutPutShp.Name = "OutPutShp";
            this.OutPutShp.Size = new System.Drawing.Size(160, 22);
            this.OutPutShp.Text = "导出图层";
            this.OutPutShp.ToolTipText = "导出图层";
            this.OutPutShp.Visible = false;
            this.OutPutShp.Click += new System.EventHandler(this.button5_Click);
            // 
            // MergeShp
            // 
            this.MergeShp.Name = "MergeShp";
            this.MergeShp.Size = new System.Drawing.Size(160, 22);
            this.MergeShp.Text = "合并到其他图层";
            this.MergeShp.ToolTipText = "合并到其他图层";
            this.MergeShp.Click += new System.EventHandler(this.MergeShp_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "BlogManageAccounts.png");
            // 
            // MapPanel2
            // 
            this.MapPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MapPanel2.Controls.Add(this.picFlash2);
            this.MapPanel2.Controls.Add(this.myToolTipControl2);
            this.MapPanel2.Controls.Add(this.MainMapImage2);
            this.MapPanel2.Controls.Add(this.MyMapEditImage2);
            this.MapPanel2.Location = new System.Drawing.Point(448, 206);
            this.MapPanel2.Name = "MapPanel2";
            this.MapPanel2.Size = new System.Drawing.Size(97, 127);
            this.MapPanel2.TabIndex = 4;
            // 
            // picFlash2
            // 
            this.picFlash2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.picFlash2.Location = new System.Drawing.Point(65, 20);
            this.picFlash2.Name = "picFlash2";
            this.picFlash2.Size = new System.Drawing.Size(100, 50);
            this.picFlash2.TabIndex = 9;
            this.picFlash2.TabStop = false;
            this.picFlash2.Visible = false;
            // 
            // myToolTipControl2
            // 
            this.myToolTipControl2.Caption = "";
            this.myToolTipControl2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myToolTipControl2.Geom = null;
            this.myToolTipControl2.LayerId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.myToolTipControl2.Location = new System.Drawing.Point(242, 20);
            this.myToolTipControl2.MapId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.myToolTipControl2.Message = "";
            this.myToolTipControl2.Name = "myToolTipControl2";
            this.myToolTipControl2.ObjectId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.myToolTipControl2.ObjectName = null;
            this.myToolTipControl2.SelectObjectConfirm = false;
            this.myToolTipControl2.Size = new System.Drawing.Size(236, 308);
            this.myToolTipControl2.TabIndex = 8;
            this.myToolTipControl2.Visible = false;
            // 
            // MainMapImage2
            // 
            this.MainMapImage2.ActiveTool = EasyMap.Forms.MapImage.Tools.Select;
            this.MainMapImage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainMapImage2.ContextMenuStrip = this.MapContextMenu;
            this.MainMapImage2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainMapImage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMapImage2.FineZoomFactor = 10D;
            this.MainMapImage2.HaveTif = false;
            this.MainMapImage2.IsOpened = false;
            this.MainMapImage2.LastOption = EasyMap.Forms.MapImage.Tools.Pan;
            this.MainMapImage2.LastWorldPos = null;
            this.MainMapImage2.Location = new System.Drawing.Point(0, 0);
            this.MainMapImage2.MyRefresh = false;
            this.MainMapImage2.Name = "MainMapImage2";
            this.MainMapImage2.NeedSave = false;
            this.MainMapImage2.PanOnClick = false;
            this.MainMapImage2.PickCoordinate = false;
            this.MainMapImage2.QueryLayerIndex = 0;
            this.MainMapImage2.RequestFromServer = false;
            this.MainMapImage2.SelectObjects = myGeometryList1;
            this.MainMapImage2.SelectOnClick = false;
            this.MainMapImage2.ShowShiQu = false;
            this.MainMapImage2.Size = new System.Drawing.Size(93, 123);
            this.MainMapImage2.TabIndex = 0;
            this.MainMapImage2.TabStop = false;
            this.MainMapImage2.WheelZoomMagnitude = 2D;
            this.MainMapImage2.ZoomOnDblClick = false;
            this.MainMapImage2.AfterRefresh += new EasyMap.Forms.MapImage.AfterRefreshEvent(this.MainMapImage_AfterRefresh);
            this.MainMapImage2.MouseMove += new EasyMap.Forms.MapImage.MouseEventHandler(this.MainMapImage_MouseMove);
            this.MainMapImage2.MouseDown += new EasyMap.Forms.MapImage.MouseEventHandler(this.MainMapImage_MouseDown);
            this.MainMapImage2.MouseUp += new EasyMap.Forms.MapImage.MouseEventHandler(this.MainMapImage_MouseUp);
            this.MainMapImage2.MapZoomChanged += new EasyMap.Forms.MapImage.MapZoomHandler(this.MainMapImage_MapZoomChanged);
            this.MainMapImage2.MapCenterChanged += new EasyMap.Forms.MapImage.MapCenterChangedHandler(this.MainMapImage_MapCenterChanged);
            this.MainMapImage2.SizeChanged += new System.EventHandler(this.MainMapImage2_SizeChanged);
            this.MainMapImage2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainMapImage1_MouseDoubleClick);
            // 
            // MapContextMenu
            // 
            this.MapContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择ToolStripMenuItem,
            this.移动ToolStripMenuItem,
            this.缩放ToolStripMenuItem,
            this.放大ToolStripMenuItem,
            this.全图显示ToolStripMenuItem,
            this.ZoomAreatoolStripMenuItem,
            this.btnZoomToSelectObjects,
            this.btnClearSelectObjects,
            this.btnClearAreaPriceFill,
            this.CopyXYToolStripMenuItem,
            this.MeasureToolStripMenuItem,
            this.XYtoolStripMenuItem,
            this.AddPolygonToolStripMenuItem,
            this.AddPolygonTempStripMenuItem,
            this.AddProblemPointStripMenuItem,
            this.AddProblemPolygonStripMenuItem,
            this.DeletePolygonToolStripMenuItem,
            this.AddPriceMotionToolStripMenuItem,
            this.DeletePriceMotionToolStripMenuItem,
            this.btnSetObjectStyle,
            this.btnDeleteObjectStyle,
            this.PropertyToolStripMenuItem,
            this.PictureToolStripMenuItem,
            this.船舶信息ToolStripMenuItem,
            this.轨迹ToolStripMenuItem,
            this.设置为遇难船舶ToolStripMenuItem,
            this.搜救队信息ToolStripMenuItem,
            this.派出搜救队船舶ToolStripMenuItem,
            this.无人机救援信息ToolStripMenuItem});
            this.MapContextMenu.Name = "MapContextMenu";
            this.MapContextMenu.Size = new System.Drawing.Size(185, 642);
            this.MapContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.LayerContextMenu_Opening);
            // 
            // 选择ToolStripMenuItem
            // 
            this.选择ToolStripMenuItem.CheckOnClick = true;
            this.选择ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("选择ToolStripMenuItem.Image")));
            this.选择ToolStripMenuItem.Name = "选择ToolStripMenuItem";
            this.选择ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.选择ToolStripMenuItem.Text = "选择";
            this.选择ToolStripMenuItem.ToolTipText = "选择";
            this.选择ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // 移动ToolStripMenuItem
            // 
            this.移动ToolStripMenuItem.CheckOnClick = true;
            this.移动ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("移动ToolStripMenuItem.Image")));
            this.移动ToolStripMenuItem.Name = "移动ToolStripMenuItem";
            this.移动ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.移动ToolStripMenuItem.Text = "移动";
            this.移动ToolStripMenuItem.ToolTipText = "移动";
            this.移动ToolStripMenuItem.Click += new System.EventHandler(this.PanToolStripButton_Click);
            // 
            // 缩放ToolStripMenuItem
            // 
            this.缩放ToolStripMenuItem.CheckOnClick = true;
            this.缩放ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("缩放ToolStripMenuItem.Image")));
            this.缩放ToolStripMenuItem.Name = "缩放ToolStripMenuItem";
            this.缩放ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.缩放ToolStripMenuItem.Text = "缩小";
            this.缩放ToolStripMenuItem.ToolTipText = "缩小";
            this.缩放ToolStripMenuItem.Click += new System.EventHandler(this.ZoomInModeToolStripButton_Click);
            // 
            // 放大ToolStripMenuItem
            // 
            this.放大ToolStripMenuItem.CheckOnClick = true;
            this.放大ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("放大ToolStripMenuItem.Image")));
            this.放大ToolStripMenuItem.Name = "放大ToolStripMenuItem";
            this.放大ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.放大ToolStripMenuItem.Text = "放大";
            this.放大ToolStripMenuItem.ToolTipText = "放大";
            this.放大ToolStripMenuItem.Click += new System.EventHandler(this.ZoomOutModeToolStripButton_Click);
            // 
            // 全图显示ToolStripMenuItem
            // 
            this.全图显示ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("全图显示ToolStripMenuItem.Image")));
            this.全图显示ToolStripMenuItem.Name = "全图显示ToolStripMenuItem";
            this.全图显示ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.全图显示ToolStripMenuItem.Text = "全图显示";
            this.全图显示ToolStripMenuItem.ToolTipText = "全图显示";
            this.全图显示ToolStripMenuItem.Click += new System.EventHandler(this.ZoomToExtentsToolStripButton_Click);
            // 
            // ZoomAreatoolStripMenuItem
            // 
            this.ZoomAreatoolStripMenuItem.CheckOnClick = true;
            this.ZoomAreatoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomAreatoolStripMenuItem.Image")));
            this.ZoomAreatoolStripMenuItem.Name = "ZoomAreatoolStripMenuItem";
            this.ZoomAreatoolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ZoomAreatoolStripMenuItem.Text = "区域放大";
            this.ZoomAreatoolStripMenuItem.ToolTipText = "区域放大";
            this.ZoomAreatoolStripMenuItem.Click += new System.EventHandler(this.ZoomAreatoolStripButton_Click);
            // 
            // btnZoomToSelectObjects
            // 
            this.btnZoomToSelectObjects.Name = "btnZoomToSelectObjects";
            this.btnZoomToSelectObjects.Size = new System.Drawing.Size(184, 22);
            this.btnZoomToSelectObjects.Text = "缩放至所选要素";
            this.btnZoomToSelectObjects.ToolTipText = "缩放至所选要素";
            this.btnZoomToSelectObjects.Click += new System.EventHandler(this.btnZoomToSelectObjects_Click);
            // 
            // btnClearSelectObjects
            // 
            this.btnClearSelectObjects.Name = "btnClearSelectObjects";
            this.btnClearSelectObjects.Size = new System.Drawing.Size(184, 22);
            this.btnClearSelectObjects.Text = "清除所选要素";
            this.btnClearSelectObjects.ToolTipText = "清除所选要素";
            this.btnClearSelectObjects.Click += new System.EventHandler(this.btnClearSelectObjects_Click);
            // 
            // btnClearAreaPriceFill
            // 
            this.btnClearAreaPriceFill.Name = "btnClearAreaPriceFill";
            this.btnClearAreaPriceFill.Size = new System.Drawing.Size(184, 22);
            this.btnClearAreaPriceFill.Text = "清除地图趋势填充";
            this.btnClearAreaPriceFill.Visible = false;
            this.btnClearAreaPriceFill.Click += new System.EventHandler(this.btnClearAreaPriceFill_Click);
            // 
            // CopyXYToolStripMenuItem
            // 
            this.CopyXYToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyXYToolStripMenuItem.Image")));
            this.CopyXYToolStripMenuItem.Name = "CopyXYToolStripMenuItem";
            this.CopyXYToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.CopyXYToolStripMenuItem.Text = "复制坐标";
            this.CopyXYToolStripMenuItem.ToolTipText = "复制坐标";
            this.CopyXYToolStripMenuItem.Click += new System.EventHandler(this.CopyXYToolStripMenuItem_Click);
            // 
            // MeasureToolStripMenuItem
            // 
            this.MeasureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mesureLengthToolStripMenuItem1,
            this.mesureAreaToolStripMenuItem1});
            this.MeasureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MeasureToolStripMenuItem.Image")));
            this.MeasureToolStripMenuItem.Name = "MeasureToolStripMenuItem";
            this.MeasureToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.MeasureToolStripMenuItem.Text = "量测";
            this.MeasureToolStripMenuItem.ToolTipText = "量测";
            // 
            // mesureLengthToolStripMenuItem1
            // 
            this.mesureLengthToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesureLengthToolStripMenuItem1.Image")));
            this.mesureLengthToolStripMenuItem1.Name = "mesureLengthToolStripMenuItem1";
            this.mesureLengthToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.mesureLengthToolStripMenuItem1.Text = "距离量测";
            this.mesureLengthToolStripMenuItem1.Click += new System.EventHandler(this.mesureLengthToolStripMenuItem1_Click);
            // 
            // mesureAreaToolStripMenuItem1
            // 
            this.mesureAreaToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesureAreaToolStripMenuItem1.Image")));
            this.mesureAreaToolStripMenuItem1.Name = "mesureAreaToolStripMenuItem1";
            this.mesureAreaToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.mesureAreaToolStripMenuItem1.Text = "面积量测";
            this.mesureAreaToolStripMenuItem1.Click += new System.EventHandler(this.mesureAreaToolStripMenuItem1_Click);
            // 
            // XYtoolStripMenuItem
            // 
            this.XYtoolStripMenuItem.Name = "XYtoolStripMenuItem";
            this.XYtoolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.XYtoolStripMenuItem.Text = "坐标定位";
            this.XYtoolStripMenuItem.ToolTipText = "坐标定位";
            this.XYtoolStripMenuItem.Click += new System.EventHandler(this.XYtoolStripMenuItem_Click);
            // 
            // AddPolygonToolStripMenuItem
            // 
            this.AddPolygonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddPolygonToolStripMenuItem.Image")));
            this.AddPolygonToolStripMenuItem.Name = "AddPolygonToolStripMenuItem";
            this.AddPolygonToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddPolygonToolStripMenuItem.Text = "添加区域";
            this.AddPolygonToolStripMenuItem.ToolTipText = "添加区域";
            this.AddPolygonToolStripMenuItem.Visible = false;
            this.AddPolygonToolStripMenuItem.Click += new System.EventHandler(this.AddPolygonToolStripMenuItem_Click);
            // 
            // AddPolygonTempStripMenuItem
            // 
            this.AddPolygonTempStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddPolygonTempStripMenuItem.Image")));
            this.AddPolygonTempStripMenuItem.Name = "AddPolygonTempStripMenuItem";
            this.AddPolygonTempStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddPolygonTempStripMenuItem.Text = "添加救援力量";
            this.AddPolygonTempStripMenuItem.ToolTipText = "添加救援力量";
            this.AddPolygonTempStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddPolygonTempStripMenuItem_Click);
            // 
            // AddProblemPointStripMenuItem
            // 
            this.AddProblemPointStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddProblemPointStripMenuItem.Image")));
            this.AddProblemPointStripMenuItem.Name = "AddProblemPointStripMenuItem";
            this.AddProblemPointStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddProblemPointStripMenuItem.Text = "添加遇难点";
            this.AddProblemPointStripMenuItem.ToolTipText = "添加遇难点";
            this.AddProblemPointStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddProblemPointStripMenuItem_Click);
            // 
            // AddProblemPolygonStripMenuItem
            // 
            this.AddProblemPolygonStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddProblemPolygonStripMenuItem.Image")));
            this.AddProblemPolygonStripMenuItem.Name = "AddProblemPolygonStripMenuItem";
            this.AddProblemPolygonStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddProblemPolygonStripMenuItem.Text = "添加遇难区域";
            this.AddProblemPolygonStripMenuItem.ToolTipText = "添加遇难区域";
            this.AddProblemPolygonStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddProblemAreaStripMenuItem_Click);
            // 
            // DeletePolygonToolStripMenuItem
            // 
            this.DeletePolygonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DeletePolygonToolStripMenuItem.Image")));
            this.DeletePolygonToolStripMenuItem.Name = "DeletePolygonToolStripMenuItem";
            this.DeletePolygonToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DeletePolygonToolStripMenuItem.Text = "删除元素";
            this.DeletePolygonToolStripMenuItem.ToolTipText = "删除元素";
            this.DeletePolygonToolStripMenuItem.Click += new System.EventHandler(this.DeletePolygonToolStripMenuItem_Click);
            // 
            // AddPriceMotionToolStripMenuItem
            // 
            this.AddPriceMotionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddPriceMotionToolStripMenuItem.Image")));
            this.AddPriceMotionToolStripMenuItem.Name = "AddPriceMotionToolStripMenuItem";
            this.AddPriceMotionToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddPriceMotionToolStripMenuItem.Text = "添加监测点";
            this.AddPriceMotionToolStripMenuItem.ToolTipText = "添加地价监测点";
            this.AddPriceMotionToolStripMenuItem.Click += new System.EventHandler(this.AddPriceMotionToolStripMenuItem_Click);
            // 
            // DeletePriceMotionToolStripMenuItem
            // 
            this.DeletePriceMotionToolStripMenuItem.Name = "DeletePriceMotionToolStripMenuItem";
            this.DeletePriceMotionToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DeletePriceMotionToolStripMenuItem.Text = "删除监测点";
            this.DeletePriceMotionToolStripMenuItem.ToolTipText = "删除地价监测点";
            this.DeletePriceMotionToolStripMenuItem.Click += new System.EventHandler(this.DeletePriceMotionToolStripMenuItem_Click);
            // 
            // btnSetObjectStyle
            // 
            this.btnSetObjectStyle.Name = "btnSetObjectStyle";
            this.btnSetObjectStyle.Size = new System.Drawing.Size(184, 22);
            this.btnSetObjectStyle.Text = "设置显示样式";
            this.btnSetObjectStyle.Click += new System.EventHandler(this.btnSetObjectStyle_Click);
            // 
            // btnDeleteObjectStyle
            // 
            this.btnDeleteObjectStyle.Name = "btnDeleteObjectStyle";
            this.btnDeleteObjectStyle.Size = new System.Drawing.Size(184, 22);
            this.btnDeleteObjectStyle.Text = "删除样式";
            this.btnDeleteObjectStyle.Click += new System.EventHandler(this.btnDeleteObjectStyle_Click);
            // 
            // PropertyToolStripMenuItem
            // 
            this.PropertyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PropertyToolStripMenuItem.Image")));
            this.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem";
            this.PropertyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.PropertyToolStripMenuItem.Text = "属性";
            this.PropertyToolStripMenuItem.ToolTipText = "属性";
            this.PropertyToolStripMenuItem.Click += new System.EventHandler(this.PropertyToolStripMenuItem_Click);
            // 
            // PictureToolStripMenuItem
            // 
            this.PictureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PictureToolStripMenuItem.Image")));
            this.PictureToolStripMenuItem.Name = "PictureToolStripMenuItem";
            this.PictureToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.PictureToolStripMenuItem.Text = "照片";
            this.PictureToolStripMenuItem.ToolTipText = "照片";
            this.PictureToolStripMenuItem.Click += new System.EventHandler(this.PictureToolStripMenuItem_Click);
            // 
            // 船舶信息ToolStripMenuItem
            // 
            this.船舶信息ToolStripMenuItem.Name = "船舶信息ToolStripMenuItem";
            this.船舶信息ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.船舶信息ToolStripMenuItem.Text = "船舶信息";
            this.船舶信息ToolStripMenuItem.ToolTipText = "船舶信息";
            this.船舶信息ToolStripMenuItem.Click += new System.EventHandler(this.BoatSetting_Click);
            // 
            // 轨迹ToolStripMenuItem
            // 
            this.轨迹ToolStripMenuItem.Name = "轨迹ToolStripMenuItem";
            this.轨迹ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.轨迹ToolStripMenuItem.Text = "显示轨迹";
            this.轨迹ToolStripMenuItem.ToolTipText = "显示轨迹";
            this.轨迹ToolStripMenuItem.Click += new System.EventHandler(this.轨迹ToolStripMenuItem_Click);
            // 
            // 设置为遇难船舶ToolStripMenuItem
            // 
            this.设置为遇难船舶ToolStripMenuItem.Name = "设置为遇难船舶ToolStripMenuItem";
            this.设置为遇难船舶ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.设置为遇难船舶ToolStripMenuItem.Text = "设置为遇难船舶";
            this.设置为遇难船舶ToolStripMenuItem.ToolTipText = "设置为遇难船舶";
            this.设置为遇难船舶ToolStripMenuItem.Click += new System.EventHandler(this.设置为遇难船舶ToolStripMenuItem_Click);
            // 
            // 搜救队信息ToolStripMenuItem
            // 
            this.搜救队信息ToolStripMenuItem.Name = "搜救队信息ToolStripMenuItem";
            this.搜救队信息ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.搜救队信息ToolStripMenuItem.Text = "救援力量信息";
            this.搜救队信息ToolStripMenuItem.ToolTipText = "救援力量信息";
            this.搜救队信息ToolStripMenuItem.Click += new System.EventHandler(this.RescueBoatSetting_Click);
            // 
            // 派出搜救队船舶ToolStripMenuItem
            // 
            this.派出搜救队船舶ToolStripMenuItem.Name = "派出搜救队船舶ToolStripMenuItem";
            this.派出搜救队船舶ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.派出搜救队船舶ToolStripMenuItem.Text = "派出救援力量";
            this.派出搜救队船舶ToolStripMenuItem.ToolTipText = "派出救援力量";
            this.派出搜救队船舶ToolStripMenuItem.Click += new System.EventHandler(this.派出搜救队船舶ToolStripMenuItem_Click);
            // 
            // 无人机救援信息ToolStripMenuItem
            // 
            this.无人机救援信息ToolStripMenuItem.Name = "无人机救援信息ToolStripMenuItem";
            this.无人机救援信息ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.无人机救援信息ToolStripMenuItem.Text = "救援力量无人机信息";
            this.无人机救援信息ToolStripMenuItem.ToolTipText = "救援力量无人机信息";
            this.无人机救援信息ToolStripMenuItem.Visible = false;
            this.无人机救援信息ToolStripMenuItem.Click += new System.EventHandler(this.无人机救援信息ToolStripMenuItem_Click);
            // 
            // MyMapEditImage2
            // 
            this.MyMapEditImage2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MyMapEditImage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyMapEditImage2.Location = new System.Drawing.Point(0, 0);
            this.MyMapEditImage2.MainMapImage = null;
            this.MyMapEditImage2.Name = "MyMapEditImage2";
            this.MyMapEditImage2.SelectionType = EsayMap.SELECTION_TYPE.NONE;
            this.MyMapEditImage2.Size = new System.Drawing.Size(93, 123);
            this.MyMapEditImage2.TabIndex = 1;
            this.MyMapEditImage2.TabStop = false;
            this.MyMapEditImage2.Visible = false;
            this.MyMapEditImage2.SetLength += new EasyMap.UI.Forms.MapEditImage.SetLengthEvent(this.mapEditImage1_SetLength);
            this.MyMapEditImage2.SetArea += new EasyMap.UI.Forms.MapEditImage.SetAreaEvent(this.mapEditImage1_SetArea);
            this.MyMapEditImage2.AfterDefineArea += new EasyMap.UI.Forms.MapEditImage.AfterDefineAreaEvent(this.mapEditImage1_AfterDefineArea);
            this.MyMapEditImage2.AfterDefineArea1 += new EasyMap.UI.Forms.MapEditImage.AfterDefineAreaEvent1(this.mapEditImage1_AfterDefineArea1);
            this.MyMapEditImage2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapEditImage1_MouseMove);
            // 
            // projectControl1
            // 
            this.projectControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.projectControl1.Enabled = false;
            this.projectControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.projectControl1.Location = new System.Drawing.Point(174, 10);
            this.projectControl1.Name = "projectControl1";
            this.projectControl1.Size = new System.Drawing.Size(170, 322);
            this.projectControl1.TabIndex = 0;
            this.projectControl1.AddProject += new EasyMap.Controls.ProjectControl.AddProjectEvent(this.projectControl1_AddProject);
            this.projectControl1.ModifyProject += new EasyMap.Controls.ProjectControl.ModifyProjectEvent(this.projectControl1_ModifyProject);
            // 
            // LayerView1
            // 
            this.LayerView1.AllowDrop = true;
            this.LayerView1.CheckBoxes = true;
            this.LayerView1.ContextMenuStrip = this.LayerContextMenu;
            this.LayerView1.HideSelection = false;
            this.LayerView1.ImageIndex = 0;
            this.LayerView1.ImageList = this.imageList1;
            this.LayerView1.LabelEdit = true;
            this.LayerView1.Location = new System.Drawing.Point(350, 10);
            this.LayerView1.MySelectedNode = null;
            this.LayerView1.Name = "LayerView1";
            treeNode2.Name = "root";
            treeNode2.Text = "图层";
            this.LayerView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.LayerView1.SelectedImageIndex = 0;
            this.LayerView1.Size = new System.Drawing.Size(177, 321);
            this.LayerView1.TabIndex = 2;
            this.LayerView1.NodeImageClick += new EasyMap.MyTree.OnNodeImageClickEvent(this.LayerView_NodeImageClick);
            this.LayerView1.SelectionChange += new EasyMap.MyTree.OnSelectionChangeEvent(this.LayerView1_SelectionChange);
            this.LayerView1.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LayerView_BeforeLabelEdit);
            this.LayerView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LayerView_AfterLabelEdit);
            this.LayerView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.LayerView_AfterCheck);
            this.LayerView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LayerView_ItemDrag);
            this.LayerView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LayerView_AfterSelect);
            this.LayerView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.LayerView_DragDrop);
            this.LayerView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.LayerView_DragEnter);
            this.LayerView1.DoubleClick += new System.EventHandler(this.LayerView1_DoubleClick);
            this.LayerView1.Enter += new System.EventHandler(this.LayerView1_Enter);
            // 
            // MapPanel1
            // 
            this.MapPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MapPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MapPanel1.Controls.Add(this.picFlash1);
            this.MapPanel1.Controls.Add(this.myToolTipControl1);
            this.MapPanel1.Controls.Add(this.btnMapToImageCancel);
            this.MapPanel1.Controls.Add(this.btnMapToImageOk);
            this.MapPanel1.Controls.Add(this.label1);
            this.MapPanel1.Controls.Add(this.waiting1);
            this.MapPanel1.Controls.Add(this.treeView1);
            this.MapPanel1.Controls.Add(this.mapControl1);
            this.MapPanel1.Controls.Add(this.MainMapImage1);
            this.MapPanel1.Controls.Add(this.MyMapEditImage1);
            this.MapPanel1.Location = new System.Drawing.Point(0, 0);
            this.MapPanel1.Name = "MapPanel1";
            this.MapPanel1.Size = new System.Drawing.Size(1171, 386);
            this.MapPanel1.TabIndex = 3;
            // 
            // picFlash1
            // 
            this.picFlash1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.picFlash1.Location = new System.Drawing.Point(690, 65);
            this.picFlash1.Name = "picFlash1";
            this.picFlash1.Size = new System.Drawing.Size(100, 50);
            this.picFlash1.TabIndex = 9;
            this.picFlash1.TabStop = false;
            this.picFlash1.Visible = false;
            // 
            // myToolTipControl1
            // 
            this.myToolTipControl1.Caption = "";
            this.myToolTipControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myToolTipControl1.Geom = null;
            this.myToolTipControl1.LayerId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.myToolTipControl1.Location = new System.Drawing.Point(129, 8);
            this.myToolTipControl1.MapId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.myToolTipControl1.Message = "";
            this.myToolTipControl1.Name = "myToolTipControl1";
            this.myToolTipControl1.ObjectId = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.myToolTipControl1.ObjectName = null;
            this.myToolTipControl1.SelectObjectConfirm = false;
            this.myToolTipControl1.Size = new System.Drawing.Size(236, 308);
            this.myToolTipControl1.TabIndex = 8;
            this.myToolTipControl1.Visible = false;
            this.myToolTipControl1.SelectConfirm += new EasyMap.Controls.MyToolTipControl.SelectObjectConfirmEvent(this.myToolTipControl1_SelectConfirm);
            // 
            // btnMapToImageCancel
            // 
            this.btnMapToImageCancel.Location = new System.Drawing.Point(280, 247);
            this.btnMapToImageCancel.Name = "btnMapToImageCancel";
            this.btnMapToImageCancel.Size = new System.Drawing.Size(75, 23);
            this.btnMapToImageCancel.TabIndex = 7;
            this.btnMapToImageCancel.Text = "取消";
            this.btnMapToImageCancel.UseVisualStyleBackColor = true;
            this.btnMapToImageCancel.Visible = false;
            this.btnMapToImageCancel.Click += new System.EventHandler(this.btnMapToImageCancel_Click);
            // 
            // btnMapToImageOk
            // 
            this.btnMapToImageOk.Location = new System.Drawing.Point(198, 248);
            this.btnMapToImageOk.Name = "btnMapToImageOk";
            this.btnMapToImageOk.Size = new System.Drawing.Size(75, 23);
            this.btnMapToImageOk.TabIndex = 6;
            this.btnMapToImageOk.Text = "保存";
            this.btnMapToImageOk.UseVisualStyleBackColor = true;
            this.btnMapToImageOk.Visible = false;
            this.btnMapToImageOk.Click += new System.EventHandler(this.btnMapToImageOk_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(181, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 0);
            this.label1.TabIndex = 5;
            this.label1.Move += new System.EventHandler(this.label1_Resize);
            this.label1.Resize += new System.EventHandler(this.label1_Resize);
            // 
            // waiting1
            // 
            this.waiting1.BackColor = System.Drawing.Color.Transparent;
            this.waiting1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waiting1.Location = new System.Drawing.Point(427, 163);
            this.waiting1.MaxProcessValue = 35;
            this.waiting1.MinProcessValue = 0;
            this.waiting1.Name = "waiting1";
            this.waiting1.ProcessValue = 16;
            this.waiting1.Size = new System.Drawing.Size(316, 60);
            this.waiting1.TabIndex = 4;
            this.waiting1.Tip = "正在取得地图数据，请稍后...";
            this.waiting1.Visible = false;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(305, 65);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(344, 251);
            this.treeView1.TabIndex = 3;
            this.treeView1.Visible = false;
            // 
            // mapControl1
            // 
            this.mapControl1.BackColor = System.Drawing.Color.Transparent;
            this.mapControl1.CurrentLevel = 0;
            this.mapControl1.LevelCount = 10;
            this.mapControl1.Location = new System.Drawing.Point(3, 44);
            this.mapControl1.Map = this.MainMapImage1;
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.OtherMap = this.MainMapImage2;
            this.mapControl1.Size = new System.Drawing.Size(100, 333);
            this.mapControl1.Step = 5;
            this.mapControl1.TabIndex = 2;
            this.mapControl1.AreaZoom += new EasyMap.MapControl.AreaZoomEvent(this.mapControl1_AreaZoom);
            this.mapControl1.ZoomChange += new EasyMap.MapControl.ZoomChangeEvent(this.mapControl1_ZoomChange);
            // 
            // MainMapImage1
            // 
            this.MainMapImage1.ActiveTool = EasyMap.Forms.MapImage.Tools.Select;
            this.MainMapImage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainMapImage1.ContextMenuStrip = this.MapContextMenu;
            this.MainMapImage1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MainMapImage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMapImage1.FineZoomFactor = 10D;
            this.MainMapImage1.HaveTif = false;
            this.MainMapImage1.IsOpened = false;
            this.MainMapImage1.LastOption = EasyMap.Forms.MapImage.Tools.Pan;
            this.MainMapImage1.LastWorldPos = null;
            this.MainMapImage1.Location = new System.Drawing.Point(0, 0);
            this.MainMapImage1.MyRefresh = false;
            this.MainMapImage1.Name = "MainMapImage1";
            this.MainMapImage1.NeedSave = true;
            this.MainMapImage1.PanOnClick = false;
            this.MainMapImage1.PickCoordinate = false;
            this.MainMapImage1.QueryLayerIndex = 0;
            this.MainMapImage1.RequestFromServer = false;
            this.MainMapImage1.SelectObjects = myGeometryList2;
            this.MainMapImage1.SelectOnClick = false;
            this.MainMapImage1.ShowShiQu = false;
            this.MainMapImage1.Size = new System.Drawing.Size(1167, 382);
            this.MainMapImage1.TabIndex = 0;
            this.MainMapImage1.TabStop = false;
            this.MainMapImage1.WheelZoomMagnitude = 2D;
            this.MainMapImage1.ZoomOnDblClick = false;
            this.MainMapImage1.AfterRefresh += new EasyMap.Forms.MapImage.AfterRefreshEvent(this.MainMapImage_AfterRefresh);
            this.MainMapImage1.BeforeRefresh += new EasyMap.Forms.MapImage.BeforeRefreshEvent(this.MainMapImage1_BeforeRefresh);
            this.MainMapImage1.MouseMove += new EasyMap.Forms.MapImage.MouseEventHandler(this.MainMapImage_MouseMove);
            this.MainMapImage1.MouseDown += new EasyMap.Forms.MapImage.MouseEventHandler(this.MainMapImage_MouseDown);
            this.MainMapImage1.MouseUp += new EasyMap.Forms.MapImage.MouseEventHandler(this.MainMapImage_MouseUp);
            this.MainMapImage1.MapZoomChanged += new EasyMap.Forms.MapImage.MapZoomHandler(this.MainMapImage_MapZoomChanged);
            this.MainMapImage1.MapCenterChanged += new EasyMap.Forms.MapImage.MapCenterChangedHandler(this.MainMapImage_MapCenterChanged);
            this.MainMapImage1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainMapImage1_MouseDoubleClick);
            // 
            // MyMapEditImage1
            // 
            this.MyMapEditImage1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.MyMapEditImage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyMapEditImage1.Location = new System.Drawing.Point(0, 0);
            this.MyMapEditImage1.MainMapImage = null;
            this.MyMapEditImage1.Name = "MyMapEditImage1";
            this.MyMapEditImage1.SelectionType = EsayMap.SELECTION_TYPE.NONE;
            this.MyMapEditImage1.Size = new System.Drawing.Size(1167, 382);
            this.MyMapEditImage1.TabIndex = 1;
            this.MyMapEditImage1.TabStop = false;
            this.MyMapEditImage1.Visible = false;
            this.MyMapEditImage1.SetLength += new EasyMap.UI.Forms.MapEditImage.SetLengthEvent(this.mapEditImage1_SetLength);
            this.MyMapEditImage1.SetArea += new EasyMap.UI.Forms.MapEditImage.SetAreaEvent(this.mapEditImage1_SetArea);
            this.MyMapEditImage1.AfterDefineArea += new EasyMap.UI.Forms.MapEditImage.AfterDefineAreaEvent(this.mapEditImage1_AfterDefineArea);
            this.MyMapEditImage1.AfterDefineArea1 += new EasyMap.UI.Forms.MapEditImage.AfterDefineAreaEvent1(this.mapEditImage1_AfterDefineArea1);
            this.MyMapEditImage1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapEditImage1_MouseMove);
            // 
            // dockContainer1
            // 
            this.dockContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.dockContainer1.CanMoveByMouseFilledForms = true;
            this.dockContainer1.Location = new System.Drawing.Point(0, 0);
            this.dockContainer1.Name = "dockContainer1";
            this.dockContainer1.Size = new System.Drawing.Size(1170, 385);
            this.dockContainer1.TabIndex = 2;
            this.dockContainer1.TitleBarGradientColor1 = System.Drawing.SystemColors.Control;
            this.dockContainer1.TitleBarGradientColor2 = System.Drawing.Color.White;
            this.dockContainer1.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this.dockContainer1.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this.dockContainer1.TitleBarTextColor = System.Drawing.Color.Black;
            this.dockContainer1.ShowContextMenu += new System.EventHandler<Crom.Controls.Docking.FormContextMenuEventArgs>(this.dockContainer1_ShowContextMenu);
            this.dockContainer1.FormClosing += new System.EventHandler<Crom.Controls.Docking.DockableFormClosingEventArgs>(this.dockContainer1_FormClosing);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.ReportToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.quanxian,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1170, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewMapToolStripMenuItem,
            this.OpenMapToolStripMenuItem,
            this.SaveMapToolStripMenuItem,
            this.CloseMapToolStripMenuItem,
            this.toolStripSeparator5,
            this.passwordUpdate,
            this.toolStripMenuItem1,
            this.printsetuptoolStripMenuItem14,
            this.PrintPreviewToolStripMenuItem,
            this.PrintToolStripMenuItem,
            this.pic_out,
            this.toolStripMenuItem2,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.FileToolStripMenuItem.Text = "文件(&F)";
            // 
            // NewMapToolStripMenuItem
            // 
            this.NewMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewMapToolStripMenuItem.Image")));
            this.NewMapToolStripMenuItem.Name = "NewMapToolStripMenuItem";
            this.NewMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.NewMapToolStripMenuItem.Text = "新建地图(&N)";
            this.NewMapToolStripMenuItem.Visible = false;
            this.NewMapToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // OpenMapToolStripMenuItem
            // 
            this.OpenMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenMapToolStripMenuItem.Image")));
            this.OpenMapToolStripMenuItem.Name = "OpenMapToolStripMenuItem";
            this.OpenMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.OpenMapToolStripMenuItem.Text = "打开地图(&O)";
            this.OpenMapToolStripMenuItem.Visible = false;
            this.OpenMapToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripButton_Click);
            // 
            // SaveMapToolStripMenuItem
            // 
            this.SaveMapToolStripMenuItem.Enabled = false;
            this.SaveMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SaveMapToolStripMenuItem.Image")));
            this.SaveMapToolStripMenuItem.Name = "SaveMapToolStripMenuItem";
            this.SaveMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.SaveMapToolStripMenuItem.Text = "保存地图(&S)";
            this.SaveMapToolStripMenuItem.Visible = false;
            this.SaveMapToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // CloseMapToolStripMenuItem
            // 
            this.CloseMapToolStripMenuItem.Enabled = false;
            this.CloseMapToolStripMenuItem.Name = "CloseMapToolStripMenuItem";
            this.CloseMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CloseMapToolStripMenuItem.Text = "关闭地图(&C)";
            this.CloseMapToolStripMenuItem.Visible = false;
            this.CloseMapToolStripMenuItem.Click += new System.EventHandler(this.CloseMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(179, 6);
            // 
            // passwordUpdate
            // 
            this.passwordUpdate.Name = "passwordUpdate";
            this.passwordUpdate.Size = new System.Drawing.Size(182, 22);
            this.passwordUpdate.Text = "修改密码(&D)";
            this.passwordUpdate.Click += new System.EventHandler(this.passwordUpdate_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(179, 6);
            // 
            // printsetuptoolStripMenuItem14
            // 
            this.printsetuptoolStripMenuItem14.Image = ((System.Drawing.Image)(resources.GetObject("printsetuptoolStripMenuItem14.Image")));
            this.printsetuptoolStripMenuItem14.Name = "printsetuptoolStripMenuItem14";
            this.printsetuptoolStripMenuItem14.Size = new System.Drawing.Size(182, 22);
            this.printsetuptoolStripMenuItem14.Text = "打印设置...";
            this.printsetuptoolStripMenuItem14.Click += new System.EventHandler(this.printsetuptoolStripMenuItem14_Click);
            // 
            // PrintPreviewToolStripMenuItem
            // 
            this.PrintPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintPreviewToolStripMenuItem.Image")));
            this.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem";
            this.PrintPreviewToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PrintPreviewToolStripMenuItem.Text = "打印预览(&W)";
            this.PrintPreviewToolStripMenuItem.Click += new System.EventHandler(this.PrintPreviewToolStripMenuItem_Click);
            // 
            // PrintToolStripMenuItem
            // 
            this.PrintToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintToolStripMenuItem.Image")));
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PrintToolStripMenuItem.Text = "打印(&P)";
            this.PrintToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // pic_out
            // 
            this.pic_out.Image = ((System.Drawing.Image)(resources.GetObject("pic_out.Image")));
            this.pic_out.Name = "pic_out";
            this.pic_out.Size = new System.Drawing.Size(182, 22);
            this.pic_out.Text = "导出地图(&Q)";
            this.pic_out.Click += new System.EventHandler(this.pic_out_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(179, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.ExitToolStripMenuItem.Text = "退出(&X)";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CutToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.PropertyDefineToolStripMenuItem,
            this.toolStripMenuItem4,
            this.InsertLayerToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.InsertAreaToolStripMenuItem,
            this.InsertPricePointToolStripMenuItem,
            this.toolStripMenuItem5,
            this.DeleteLayerToolStripMenuItem1,
            this.DeleteAreaToolStripMenuItem1,
            this.DeletePricePointToolStripMenuItem1,
            this.影像图层级设置ToolStripMenuItem,
            this.更改地块上土地注记ToolStripMenuItem,
            this.更改图层分类样式ToolStripMenuItem,
            this.重新生成管辖视图ToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.EditToolStripMenuItem.Text = "编辑(&E)";
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CutToolStripMenuItem.Image")));
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CutToolStripMenuItem.Text = "剪切(&T)";
            this.CutToolStripMenuItem.Visible = false;
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripMenuItem.Image")));
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CopyToolStripMenuItem.Text = "复制(&C)";
            this.CopyToolStripMenuItem.Visible = false;
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripMenuItem.Image")));
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PasteToolStripMenuItem.Text = "粘帖(&P)";
            this.PasteToolStripMenuItem.Visible = false;
            // 
            // PropertyDefineToolStripMenuItem
            // 
            this.PropertyDefineToolStripMenuItem.Name = "PropertyDefineToolStripMenuItem";
            this.PropertyDefineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PropertyDefineToolStripMenuItem.Text = "属性定义(&A)";
            this.PropertyDefineToolStripMenuItem.Visible = false;
            this.PropertyDefineToolStripMenuItem.Click += new System.EventHandler(this.PropertyDefineToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(179, 6);
            // 
            // InsertLayerToolStripMenuItem
            // 
            this.InsertLayerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础图层ToolStripMenuItem1,
            this.宗地信息图层ToolStripMenuItem1,
            this.其他图层ToolStripMenuItem1});
            this.InsertLayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InsertLayerToolStripMenuItem.Image")));
            this.InsertLayerToolStripMenuItem.Name = "InsertLayerToolStripMenuItem";
            this.InsertLayerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.InsertLayerToolStripMenuItem.Text = "新建图层";
            // 
            // 基础图层ToolStripMenuItem1
            // 
            this.基础图层ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("基础图层ToolStripMenuItem1.Image")));
            this.基础图层ToolStripMenuItem1.Name = "基础图层ToolStripMenuItem1";
            this.基础图层ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.基础图层ToolStripMenuItem1.Text = "基础图层";
            this.基础图层ToolStripMenuItem1.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // 宗地信息图层ToolStripMenuItem1
            // 
            this.宗地信息图层ToolStripMenuItem1.Name = "宗地信息图层ToolStripMenuItem1";
            this.宗地信息图层ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.宗地信息图层ToolStripMenuItem1.Text = "宗地信息图层";
            this.宗地信息图层ToolStripMenuItem1.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // 其他图层ToolStripMenuItem1
            // 
            this.其他图层ToolStripMenuItem1.Name = "其他图层ToolStripMenuItem1";
            this.其他图层ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.其他图层ToolStripMenuItem1.Text = "其他图层";
            this.其他图层ToolStripMenuItem1.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.影像图图层ToolStripMenuItem1,
            this.基础图层ToolStripMenuItem2,
            this.宗地信息图层ToolStripMenuItem2,
            this.其他图层ToolStripMenuItem2});
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.OpenToolStripMenuItem.Text = "打开图层";
            // 
            // 影像图图层ToolStripMenuItem1
            // 
            this.影像图图层ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("影像图图层ToolStripMenuItem1.Image")));
            this.影像图图层ToolStripMenuItem1.Name = "影像图图层ToolStripMenuItem1";
            this.影像图图层ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.影像图图层ToolStripMenuItem1.Text = "影像图图层";
            this.影像图图层ToolStripMenuItem1.Visible = false;
            this.影像图图层ToolStripMenuItem1.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 基础图层ToolStripMenuItem2
            // 
            this.基础图层ToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("基础图层ToolStripMenuItem2.Image")));
            this.基础图层ToolStripMenuItem2.Name = "基础图层ToolStripMenuItem2";
            this.基础图层ToolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.基础图层ToolStripMenuItem2.Text = "基础图层";
            this.基础图层ToolStripMenuItem2.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 宗地信息图层ToolStripMenuItem2
            // 
            this.宗地信息图层ToolStripMenuItem2.Name = "宗地信息图层ToolStripMenuItem2";
            this.宗地信息图层ToolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.宗地信息图层ToolStripMenuItem2.Text = "宗地信息图层";
            this.宗地信息图层ToolStripMenuItem2.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 其他图层ToolStripMenuItem2
            // 
            this.其他图层ToolStripMenuItem2.Name = "其他图层ToolStripMenuItem2";
            this.其他图层ToolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.其他图层ToolStripMenuItem2.Text = "其他图层";
            this.其他图层ToolStripMenuItem2.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // InsertAreaToolStripMenuItem
            // 
            this.InsertAreaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InsertAreaToolStripMenuItem.Image")));
            this.InsertAreaToolStripMenuItem.Name = "InsertAreaToolStripMenuItem";
            this.InsertAreaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.InsertAreaToolStripMenuItem.Text = "新建区域";
            this.InsertAreaToolStripMenuItem.Click += new System.EventHandler(this.AddPolygonToolStripMenuItem_Click);
            // 
            // InsertPricePointToolStripMenuItem
            // 
            this.InsertPricePointToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InsertPricePointToolStripMenuItem.Image")));
            this.InsertPricePointToolStripMenuItem.Name = "InsertPricePointToolStripMenuItem";
            this.InsertPricePointToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.InsertPricePointToolStripMenuItem.Text = "新建监测点";
            this.InsertPricePointToolStripMenuItem.Visible = false;
            this.InsertPricePointToolStripMenuItem.Click += new System.EventHandler(this.AddPriceMotionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(179, 6);
            // 
            // DeleteLayerToolStripMenuItem1
            // 
            this.DeleteLayerToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("DeleteLayerToolStripMenuItem1.Image")));
            this.DeleteLayerToolStripMenuItem1.Name = "DeleteLayerToolStripMenuItem1";
            this.DeleteLayerToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.DeleteLayerToolStripMenuItem1.Text = "删除图层";
            this.DeleteLayerToolStripMenuItem1.Click += new System.EventHandler(this.RemoveLayerToolStripButton_Click);
            // 
            // DeleteAreaToolStripMenuItem1
            // 
            this.DeleteAreaToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("DeleteAreaToolStripMenuItem1.Image")));
            this.DeleteAreaToolStripMenuItem1.Name = "DeleteAreaToolStripMenuItem1";
            this.DeleteAreaToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.DeleteAreaToolStripMenuItem1.Text = "删除区域";
            this.DeleteAreaToolStripMenuItem1.Click += new System.EventHandler(this.DeletePolygonToolStripMenuItem_Click);
            // 
            // DeletePricePointToolStripMenuItem1
            // 
            this.DeletePricePointToolStripMenuItem1.Name = "DeletePricePointToolStripMenuItem1";
            this.DeletePricePointToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.DeletePricePointToolStripMenuItem1.Text = "删除监测点";
            this.DeletePricePointToolStripMenuItem1.Visible = false;
            this.DeletePricePointToolStripMenuItem1.Click += new System.EventHandler(this.DeletePriceMotionToolStripMenuItem_Click);
            // 
            // 影像图层级设置ToolStripMenuItem
            // 
            this.影像图层级设置ToolStripMenuItem.Name = "影像图层级设置ToolStripMenuItem";
            this.影像图层级设置ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.影像图层级设置ToolStripMenuItem.Text = "影像图层级设置";
            this.影像图层级设置ToolStripMenuItem.Visible = false;
            this.影像图层级设置ToolStripMenuItem.Click += new System.EventHandler(this.影像图层级设置ToolStripMenuItem_Click);
            // 
            // 更改地块上土地注记ToolStripMenuItem
            // 
            this.更改地块上土地注记ToolStripMenuItem.Name = "更改地块上土地注记ToolStripMenuItem";
            this.更改地块上土地注记ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.更改地块上土地注记ToolStripMenuItem.Text = "更改地块上宗地注记";
            this.更改地块上土地注记ToolStripMenuItem.Visible = false;
            this.更改地块上土地注记ToolStripMenuItem.Click += new System.EventHandler(this.更改地块上土地注记ToolStripMenuItem_Click);
            // 
            // 更改图层分类样式ToolStripMenuItem
            // 
            this.更改图层分类样式ToolStripMenuItem.Name = "更改图层分类样式ToolStripMenuItem";
            this.更改图层分类样式ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.更改图层分类样式ToolStripMenuItem.Text = "更改图层分类样式";
            this.更改图层分类样式ToolStripMenuItem.Click += new System.EventHandler(this.更改图层分类样式ToolStripMenuItem_Click);
            // 
            // 重新生成管辖视图ToolStripMenuItem
            // 
            this.重新生成管辖视图ToolStripMenuItem.Name = "重新生成管辖视图ToolStripMenuItem";
            this.重新生成管辖视图ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.重新生成管辖视图ToolStripMenuItem.Text = "重新生成管辖视图";
            this.重新生成管辖视图ToolStripMenuItem.Click += new System.EventHandler(this.重新生成管辖视图ToolStripMenuItem_Click);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseToolStripMenuItem,
            this.MoveToolStripMenuItem,
            this.ZoomToolStripMenuItem,
            this.ZoomOutToolStripMenuItem,
            this.ZoomToExtentsToolStripMenuItem,
            this.toolStripMenuItem16,
            this.btnLayerView,
            this.btnDataView,
            this.btnPropertyView,
            this.btnPictureView,
            this.toolStripMenuItem17,
            this.btnProjectView});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.ViewToolStripMenuItem.Text = "视图(&V)";
            // 
            // chooseToolStripMenuItem
            // 
            this.chooseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("chooseToolStripMenuItem.Image")));
            this.chooseToolStripMenuItem.Name = "chooseToolStripMenuItem";
            this.chooseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.chooseToolStripMenuItem.Text = "选择";
            this.chooseToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MoveToolStripMenuItem
            // 
            this.MoveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveToolStripMenuItem.Image")));
            this.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem";
            this.MoveToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.MoveToolStripMenuItem.Text = "移动";
            this.MoveToolStripMenuItem.Visible = false;
            this.MoveToolStripMenuItem.Click += new System.EventHandler(this.PanToolStripButton_Click);
            // 
            // ZoomToolStripMenuItem
            // 
            this.ZoomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomToolStripMenuItem.Image")));
            this.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem";
            this.ZoomToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ZoomToolStripMenuItem.Text = "缩小";
            this.ZoomToolStripMenuItem.ToolTipText = "缩小";
            this.ZoomToolStripMenuItem.Click += new System.EventHandler(this.ZoomInModeToolStripButton_Click);
            // 
            // ZoomOutToolStripMenuItem
            // 
            this.ZoomOutToolStripMenuItem.CheckOnClick = true;
            this.ZoomOutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutToolStripMenuItem.Image")));
            this.ZoomOutToolStripMenuItem.Name = "ZoomOutToolStripMenuItem";
            this.ZoomOutToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ZoomOutToolStripMenuItem.Text = "放大";
            this.ZoomOutToolStripMenuItem.ToolTipText = "放大";
            this.ZoomOutToolStripMenuItem.Click += new System.EventHandler(this.ZoomOutModeToolStripButton_Click);
            // 
            // ZoomToExtentsToolStripMenuItem
            // 
            this.ZoomToExtentsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomToExtentsToolStripMenuItem.Image")));
            this.ZoomToExtentsToolStripMenuItem.Name = "ZoomToExtentsToolStripMenuItem";
            this.ZoomToExtentsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ZoomToExtentsToolStripMenuItem.Text = "地图全显示";
            this.ZoomToExtentsToolStripMenuItem.Click += new System.EventHandler(this.ZoomToExtentsToolStripButton_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(131, 6);
            // 
            // btnLayerView
            // 
            this.btnLayerView.CheckOnClick = true;
            this.btnLayerView.Name = "btnLayerView";
            this.btnLayerView.Size = new System.Drawing.Size(134, 22);
            this.btnLayerView.Text = "图层视图";
            this.btnLayerView.Click += new System.EventHandler(this.btnLayerView_Click);
            // 
            // btnDataView
            // 
            this.btnDataView.CheckOnClick = true;
            this.btnDataView.Name = "btnDataView";
            this.btnDataView.Size = new System.Drawing.Size(134, 22);
            this.btnDataView.Text = "数据视图";
            this.btnDataView.Click += new System.EventHandler(this.btnDataView_Click);
            // 
            // btnPropertyView
            // 
            this.btnPropertyView.CheckOnClick = true;
            this.btnPropertyView.Name = "btnPropertyView";
            this.btnPropertyView.Size = new System.Drawing.Size(134, 22);
            this.btnPropertyView.Text = "属性视图";
            this.btnPropertyView.Click += new System.EventHandler(this.btnPropertyView_Click);
            // 
            // btnPictureView
            // 
            this.btnPictureView.CheckOnClick = true;
            this.btnPictureView.Name = "btnPictureView";
            this.btnPictureView.Size = new System.Drawing.Size(134, 22);
            this.btnPictureView.Text = "图片视图";
            this.btnPictureView.Click += new System.EventHandler(this.btnPictureView_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(131, 6);
            // 
            // btnProjectView
            // 
            this.btnProjectView.CheckOnClick = true;
            this.btnProjectView.Name = "btnProjectView";
            this.btnProjectView.Size = new System.Drawing.Size(134, 22);
            this.btnProjectView.Text = "管辖视图";
            this.btnProjectView.Click += new System.EventHandler(this.toolStripMenuItem19_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TifSplitToolStripMenuItem,
            this.btnPropertyControl,
            this.CustomReportMenuItem2,
            this.toolStripMenuItem15,
            this.btnParameterSettings,
            this.搜索救援队范围设置ToolStripMenuItem,
            this.船舶刷新时间设置ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.工具ToolStripMenuItem.Text = "工具(&T)";
            // 
            // TifSplitToolStripMenuItem
            // 
            this.TifSplitToolStripMenuItem.Name = "TifSplitToolStripMenuItem";
            this.TifSplitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.TifSplitToolStripMenuItem.Text = "影像图拆分";
            this.TifSplitToolStripMenuItem.Visible = false;
            this.TifSplitToolStripMenuItem.Click += new System.EventHandler(this.TifSplitToolStripMenuItem_Click);
            // 
            // btnPropertyControl
            // 
            this.btnPropertyControl.Name = "btnPropertyControl";
            this.btnPropertyControl.Size = new System.Drawing.Size(182, 22);
            this.btnPropertyControl.Text = "属性控制";
            this.btnPropertyControl.Visible = false;
            this.btnPropertyControl.Click += new System.EventHandler(this.btnPropertyControl_Click);
            // 
            // CustomReportMenuItem2
            // 
            this.CustomReportMenuItem2.Name = "CustomReportMenuItem2";
            this.CustomReportMenuItem2.Size = new System.Drawing.Size(182, 22);
            this.CustomReportMenuItem2.Text = "自定义报表";
            this.CustomReportMenuItem2.Visible = false;
            this.CustomReportMenuItem2.Click += new System.EventHandler(this.CustomReportMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(179, 6);
            // 
            // btnParameterSettings
            // 
            this.btnParameterSettings.Name = "btnParameterSettings";
            this.btnParameterSettings.Size = new System.Drawing.Size(182, 22);
            this.btnParameterSettings.Text = "系统参数设置";
            this.btnParameterSettings.Visible = false;
            this.btnParameterSettings.Click += new System.EventHandler(this.btnParameterSettings_Click);
            // 
            // 搜索救援队范围设置ToolStripMenuItem
            // 
            this.搜索救援队范围设置ToolStripMenuItem.Name = "搜索救援队范围设置ToolStripMenuItem";
            this.搜索救援队范围设置ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.搜索救援队范围设置ToolStripMenuItem.Text = "搜索救援队范围设置";
            this.搜索救援队范围设置ToolStripMenuItem.Click += new System.EventHandler(this.搜索救援队范围设置ToolStripMenuItem_Click);
            // 
            // 船舶刷新时间设置ToolStripMenuItem
            // 
            this.船舶刷新时间设置ToolStripMenuItem.Name = "船舶刷新时间设置ToolStripMenuItem";
            this.船舶刷新时间设置ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.船舶刷新时间设置ToolStripMenuItem.Text = "船舶刷新时间设置";
            this.船舶刷新时间设置ToolStripMenuItem.Click += new System.EventHandler(this.船舶刷新时间设置ToolStripMenuItem_Click);
            // 
            // ReportToolStripMenuItem
            // 
            this.ReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Measure1ToolStripMenuItem,
            this.toolStripMenuItem14,
            this.船舶信息查询ToolStripMenuItem,
            this.救援队信息查询ToolStripMenuItem,
            this.派出救援报告查询ToolStripMenuItem,
            this.选中区域内查询ToolStripMenuItem});
            this.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem";
            this.ReportToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ReportToolStripMenuItem.Text = "查询(&Q)";
            // 
            // Measure1ToolStripMenuItem
            // 
            this.Measure1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mesure1ToolStripMenuItem1,
            this.mesure2ToolStripMenuItem1});
            this.Measure1ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Measure1ToolStripMenuItem.Image")));
            this.Measure1ToolStripMenuItem.Name = "Measure1ToolStripMenuItem";
            this.Measure1ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.Measure1ToolStripMenuItem.Text = "测量";
            this.Measure1ToolStripMenuItem.Visible = false;
            // 
            // mesure1ToolStripMenuItem1
            // 
            this.mesure1ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesure1ToolStripMenuItem1.Image")));
            this.mesure1ToolStripMenuItem1.Name = "mesure1ToolStripMenuItem1";
            this.mesure1ToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.mesure1ToolStripMenuItem1.Text = "距离量测";
            this.mesure1ToolStripMenuItem1.Click += new System.EventHandler(this.mesureLengthToolStripMenuItem1_Click);
            // 
            // mesure2ToolStripMenuItem1
            // 
            this.mesure2ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesure2ToolStripMenuItem1.Image")));
            this.mesure2ToolStripMenuItem1.Name = "mesure2ToolStripMenuItem1";
            this.mesure2ToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.mesure2ToolStripMenuItem1.Text = "面积量测";
            this.mesure2ToolStripMenuItem1.Click += new System.EventHandler(this.mesureAreaToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(167, 6);
            this.toolStripMenuItem14.Visible = false;
            // 
            // 船舶信息查询ToolStripMenuItem
            // 
            this.船舶信息查询ToolStripMenuItem.Name = "船舶信息查询ToolStripMenuItem";
            this.船舶信息查询ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.船舶信息查询ToolStripMenuItem.Text = "船舶信息查询";
            this.船舶信息查询ToolStripMenuItem.Click += new System.EventHandler(this.船舶信息查询ToolStripMenuItem_Click);
            // 
            // 救援队信息查询ToolStripMenuItem
            // 
            this.救援队信息查询ToolStripMenuItem.Name = "救援队信息查询ToolStripMenuItem";
            this.救援队信息查询ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.救援队信息查询ToolStripMenuItem.Text = "救援力量信息查询";
            this.救援队信息查询ToolStripMenuItem.Click += new System.EventHandler(this.救援队信息查询ToolStripMenuItem_Click);
            // 
            // 派出救援报告查询ToolStripMenuItem
            // 
            this.派出救援报告查询ToolStripMenuItem.Name = "派出救援报告查询ToolStripMenuItem";
            this.派出救援报告查询ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.派出救援报告查询ToolStripMenuItem.Text = "派出救援报告查询";
            this.派出救援报告查询ToolStripMenuItem.Click += new System.EventHandler(this.派出救援报告查询ToolStripMenuItem_Click);
            // 
            // 选中区域内查询ToolStripMenuItem
            // 
            this.选中区域内查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选中区域内船舶信息ToolStripMenuItem,
            this.选中区域内救援队信息ToolStripMenuItem,
            this.选中区域内无人机信息ToolStripMenuItem});
            this.选中区域内查询ToolStripMenuItem.Name = "选中区域内查询ToolStripMenuItem";
            this.选中区域内查询ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.选中区域内查询ToolStripMenuItem.Text = "选中区域内查询";
            // 
            // 选中区域内船舶信息ToolStripMenuItem
            // 
            this.选中区域内船舶信息ToolStripMenuItem.Name = "选中区域内船舶信息ToolStripMenuItem";
            this.选中区域内船舶信息ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.选中区域内船舶信息ToolStripMenuItem.Text = "选中区域内船舶信息";
            this.选中区域内船舶信息ToolStripMenuItem.Click += new System.EventHandler(this.选中区域内船舶信息ToolStripMenuItem_Click);
            // 
            // 选中区域内救援队信息ToolStripMenuItem
            // 
            this.选中区域内救援队信息ToolStripMenuItem.Name = "选中区域内救援队信息ToolStripMenuItem";
            this.选中区域内救援队信息ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.选中区域内救援队信息ToolStripMenuItem.Text = "选中区域内救援力量信息";
            this.选中区域内救援队信息ToolStripMenuItem.Click += new System.EventHandler(this.选中区域内救援队信息ToolStripMenuItem_Click);
            // 
            // 选中区域内无人机信息ToolStripMenuItem
            // 
            this.选中区域内无人机信息ToolStripMenuItem.Name = "选中区域内无人机信息ToolStripMenuItem";
            this.选中区域内无人机信息ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.选中区域内无人机信息ToolStripMenuItem.Text = "选中区域内无人机信息";
            this.选中区域内无人机信息ToolStripMenuItem.Visible = false;
            this.选中区域内无人机信息ToolStripMenuItem.Click += new System.EventHandler(this.选中区域内无人机信息ToolStripMenuItem_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.派出救援队ToolStripMenuItem,
            this.设置遇难船舶ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.操作ToolStripMenuItem.Text = "操作(M)";
            // 
            // 派出救援队ToolStripMenuItem
            // 
            this.派出救援队ToolStripMenuItem.Name = "派出救援队ToolStripMenuItem";
            this.派出救援队ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.派出救援队ToolStripMenuItem.Text = "派出救援船舶";
            this.派出救援队ToolStripMenuItem.Click += new System.EventHandler(this.派出救援队ToolStripMenuItem_Click);
            // 
            // 设置遇难船舶ToolStripMenuItem
            // 
            this.设置遇难船舶ToolStripMenuItem.Name = "设置遇难船舶ToolStripMenuItem";
            this.设置遇难船舶ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.设置遇难船舶ToolStripMenuItem.Text = "设置遇难船舶";
            this.设置遇难船舶ToolStripMenuItem.Click += new System.EventHandler(this.设置遇难船舶ToolStripMenuItem_Click);
            // 
            // quanxian
            // 
            this.quanxian.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.权限设置ToolStripMenuItem,
            this.管理员ToolStripMenuItem});
            this.quanxian.Name = "quanxian";
            this.quanxian.Size = new System.Drawing.Size(83, 20);
            this.quanxian.Text = "权限设置(&G)";
            this.quanxian.Visible = false;
            this.quanxian.Click += new System.EventHandler(this.quanxian_Click);
            // 
            // 权限设置ToolStripMenuItem
            // 
            this.权限设置ToolStripMenuItem.Name = "权限设置ToolStripMenuItem";
            this.权限设置ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.权限设置ToolStripMenuItem.Text = "权限设置";
            this.权限设置ToolStripMenuItem.Click += new System.EventHandler(this.权限设置ToolStripMenuItem_Click);
            // 
            // 管理员ToolStripMenuItem
            // 
            this.管理员ToolStripMenuItem.Name = "管理员ToolStripMenuItem";
            this.管理员ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.管理员ToolStripMenuItem.Text = "管理员权限设置";
            this.管理员ToolStripMenuItem.Visible = false;
            this.管理员ToolStripMenuItem.Click += new System.EventHandler(this.管理员ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助ToolStripMenuItem1,
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.帮助ToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 帮助ToolStripMenuItem1
            // 
            this.帮助ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("帮助ToolStripMenuItem1.Image")));
            this.帮助ToolStripMenuItem1.Name = "帮助ToolStripMenuItem1";
            this.帮助ToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.帮助ToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.帮助ToolStripMenuItem1.Text = "帮助";
            this.帮助ToolStripMenuItem1.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.MainToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripButton,
            this.AddNewRandomGeometryLayer,
            this.AddLayerToolStripButton,
            this.RemoveLayerToolStripButton,
            this.toolStripSeparator4,
            this.OpenToolStripButton,
            this.SaveToolStripButton,
            this.PrintToolStripButton,
            this.toolStripSeparator,
            this.CutToolStripButton,
            this.btnSelect,
            this.CopyToolStripButton,
            this.PasteToolStripButton,
            this.toolStripSeparator1,
            this.SelecttoolStripButton1,
            this.PanToolStripButton,
            this.ZoomInModeToolStripButton,
            this.ZoomOutModeToolStripButton,
            this.ZoomToExtentsToolStripButton,
            this.ZoomAreatoolStripButton,
            this.toolStripLabel2,
            this.ZoomtoolStripTextBox,
            this.btnMap,
            this.btnLoadPicture,
            this.btnGuiji,
            this.btnClearGuiji,
            this.btnLoadShiQu,
            this.btnTax,
            this.toolStripSeparator7,
            this.MesuretoolStripSplitButton1,
            this.PropertytoolStripButton2,
            this.toolStripDropDownButton1,
            this.InputDatatoolStripButton1,
            this.toolStripSeparator3,
            this.btnShowPriceColor,
            this.CompareToolStripButton,
            this.ReporttoolStripButton,
            this.FindSaveBoat,
            this.btnFindSaveBoat,
            this.toolStripSeparator2,
            this.FindAreaTextBox,
            this.btnFindArea,
            this.btnClear,
            this.btnMapToImage,
            this.btnEditLayerList,
            this.lblCurrentLayer,
            this.toolStripSeparator6,
            this.btnDataSearch});
            this.MainToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainToolStrip.Location = new System.Drawing.Point(3, 24);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(1145, 25);
            this.MainToolStrip.TabIndex = 0;
            // 
            // NewToolStripButton
            // 
            this.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewToolStripButton.Image")));
            this.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton.Name = "NewToolStripButton";
            this.NewToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.NewToolStripButton.Text = "新建";
            this.NewToolStripButton.ToolTipText = "新建地图";
            this.NewToolStripButton.Visible = false;
            this.NewToolStripButton.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // AddNewRandomGeometryLayer
            // 
            this.AddNewRandomGeometryLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddNewRandomGeometryLayer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13});
            this.AddNewRandomGeometryLayer.Enabled = false;
            this.AddNewRandomGeometryLayer.Image = ((System.Drawing.Image)(resources.GetObject("AddNewRandomGeometryLayer.Image")));
            this.AddNewRandomGeometryLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddNewRandomGeometryLayer.Name = "AddNewRandomGeometryLayer";
            this.AddNewRandomGeometryLayer.Size = new System.Drawing.Size(32, 22);
            this.AddNewRandomGeometryLayer.Text = "新建图层";
            this.AddNewRandomGeometryLayer.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click_1);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem6.Text = "基础图层";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem7.Text = "评估报告图层";
            this.toolStripMenuItem7.Visible = false;
            this.toolStripMenuItem7.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem8.Text = "税务级别图层";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem9.Text = "交易案例图层";
            this.toolStripMenuItem9.Visible = false;
            this.toolStripMenuItem9.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem10.Text = "宗地信息图层";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem11.Image")));
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem11.Text = "房屋售价图层";
            this.toolStripMenuItem11.Visible = false;
            this.toolStripMenuItem11.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem12.Text = "房屋租赁图层";
            this.toolStripMenuItem12.Visible = false;
            this.toolStripMenuItem12.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem13.Text = "其他图层";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // AddLayerToolStripButton
            // 
            this.AddLayerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddLayerToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.影像图图层ToolStripMenuItem,
            this.基础图层ToolStripMenuItem,
            this.评估报告图层ToolStripMenuItem,
            this.监测点图层ToolStripMenuItem,
            this.交易案例图层ToolStripMenuItem,
            this.宗地信息图层ToolStripMenuItem,
            this.房屋售价图层ToolStripMenuItem,
            this.房屋租赁图层ToolStripMenuItem,
            this.其他图层ToolStripMenuItem});
            this.AddLayerToolStripButton.Enabled = false;
            this.AddLayerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AddLayerToolStripButton.Image")));
            this.AddLayerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddLayerToolStripButton.Name = "AddLayerToolStripButton";
            this.AddLayerToolStripButton.Size = new System.Drawing.Size(32, 22);
            this.AddLayerToolStripButton.Text = "导入图层";
            this.AddLayerToolStripButton.ButtonClick += new System.EventHandler(this.AddLayerToolStripButton_ButtonClick);
            // 
            // 影像图图层ToolStripMenuItem
            // 
            this.影像图图层ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("影像图图层ToolStripMenuItem.Image")));
            this.影像图图层ToolStripMenuItem.Name = "影像图图层ToolStripMenuItem";
            this.影像图图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.影像图图层ToolStripMenuItem.Text = "影像图层";
            this.影像图图层ToolStripMenuItem.Visible = false;
            this.影像图图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 基础图层ToolStripMenuItem
            // 
            this.基础图层ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("基础图层ToolStripMenuItem.Image")));
            this.基础图层ToolStripMenuItem.Name = "基础图层ToolStripMenuItem";
            this.基础图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.基础图层ToolStripMenuItem.Text = "基础图层";
            this.基础图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 评估报告图层ToolStripMenuItem
            // 
            this.评估报告图层ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("评估报告图层ToolStripMenuItem.Image")));
            this.评估报告图层ToolStripMenuItem.Name = "评估报告图层ToolStripMenuItem";
            this.评估报告图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.评估报告图层ToolStripMenuItem.Text = "评估报告图层";
            this.评估报告图层ToolStripMenuItem.Visible = false;
            this.评估报告图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 监测点图层ToolStripMenuItem
            // 
            this.监测点图层ToolStripMenuItem.Name = "监测点图层ToolStripMenuItem";
            this.监测点图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.监测点图层ToolStripMenuItem.Text = "税务级别图层";
            this.监测点图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 交易案例图层ToolStripMenuItem
            // 
            this.交易案例图层ToolStripMenuItem.Name = "交易案例图层ToolStripMenuItem";
            this.交易案例图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.交易案例图层ToolStripMenuItem.Text = "交易案例图层";
            this.交易案例图层ToolStripMenuItem.Visible = false;
            this.交易案例图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 宗地信息图层ToolStripMenuItem
            // 
            this.宗地信息图层ToolStripMenuItem.Name = "宗地信息图层ToolStripMenuItem";
            this.宗地信息图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.宗地信息图层ToolStripMenuItem.Text = "宗地信息图层";
            this.宗地信息图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 房屋售价图层ToolStripMenuItem
            // 
            this.房屋售价图层ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("房屋售价图层ToolStripMenuItem.Image")));
            this.房屋售价图层ToolStripMenuItem.Name = "房屋售价图层ToolStripMenuItem";
            this.房屋售价图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.房屋售价图层ToolStripMenuItem.Text = "房屋售价图层";
            this.房屋售价图层ToolStripMenuItem.Visible = false;
            this.房屋售价图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 房屋租赁图层ToolStripMenuItem
            // 
            this.房屋租赁图层ToolStripMenuItem.Name = "房屋租赁图层ToolStripMenuItem";
            this.房屋租赁图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.房屋租赁图层ToolStripMenuItem.Text = "房屋租赁图层";
            this.房屋租赁图层ToolStripMenuItem.Visible = false;
            this.房屋租赁图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 其他图层ToolStripMenuItem
            // 
            this.其他图层ToolStripMenuItem.Name = "其他图层ToolStripMenuItem";
            this.其他图层ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.其他图层ToolStripMenuItem.Text = "其他图层";
            this.其他图层ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // RemoveLayerToolStripButton
            // 
            this.RemoveLayerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveLayerToolStripButton.Enabled = false;
            this.RemoveLayerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveLayerToolStripButton.Image")));
            this.RemoveLayerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveLayerToolStripButton.Name = "RemoveLayerToolStripButton";
            this.RemoveLayerToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveLayerToolStripButton.Text = "删除图层";
            this.RemoveLayerToolStripButton.ToolTipText = "删除当前图层";
            this.RemoveLayerToolStripButton.Click += new System.EventHandler(this.RemoveLayerToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // OpenToolStripButton
            // 
            this.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenToolStripButton.Image")));
            this.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripButton.Name = "OpenToolStripButton";
            this.OpenToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.OpenToolStripButton.Text = "打开";
            this.OpenToolStripButton.ToolTipText = "打开地图";
            this.OpenToolStripButton.Visible = false;
            this.OpenToolStripButton.Click += new System.EventHandler(this.OpenToolStripButton_Click);
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.Enabled = false;
            this.SaveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripButton.Image")));
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.SaveToolStripButton.Text = "保存";
            this.SaveToolStripButton.ToolTipText = "保存地图";
            this.SaveToolStripButton.Visible = false;
            this.SaveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // PrintToolStripButton
            // 
            this.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintToolStripButton.Enabled = false;
            this.PrintToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PrintToolStripButton.Image")));
            this.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripButton.Name = "PrintToolStripButton";
            this.PrintToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PrintToolStripButton.Text = "打印";
            this.PrintToolStripButton.ToolTipText = "打印当前地图";
            this.PrintToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // CutToolStripButton
            // 
            this.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CutToolStripButton.Image")));
            this.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutToolStripButton.Name = "CutToolStripButton";
            this.CutToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.CutToolStripButton.Text = "剪切";
            this.CutToolStripButton.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRectangel,
            this.btnFreeCircle,
            this.btnCircle,
            this.btnFree});
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(32, 22);
            this.btnSelect.Text = "选择工具";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnRectangel
            // 
            this.btnRectangel.Checked = true;
            this.btnRectangel.CheckOnClick = true;
            this.btnRectangel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnRectangel.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangel.Image")));
            this.btnRectangel.Name = "btnRectangel";
            this.btnRectangel.Size = new System.Drawing.Size(170, 22);
            this.btnRectangel.Text = "矩形框选择";
            this.btnRectangel.ToolTipText = "矩形框选择";
            this.btnRectangel.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnFreeCircle
            // 
            this.btnFreeCircle.CheckOnClick = true;
            this.btnFreeCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnFreeCircle.Image")));
            this.btnFreeCircle.Name = "btnFreeCircle";
            this.btnFreeCircle.Size = new System.Drawing.Size(170, 22);
            this.btnFreeCircle.Text = "自由圆形选择";
            this.btnFreeCircle.ToolTipText = "自由圆形选择";
            this.btnFreeCircle.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.CheckOnClick = true;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(170, 22);
            this.btnCircle.Text = "定义半径圆形选择";
            this.btnCircle.ToolTipText = "定义半径圆形选择";
            this.btnCircle.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnFree
            // 
            this.btnFree.CheckOnClick = true;
            this.btnFree.Image = ((System.Drawing.Image)(resources.GetObject("btnFree.Image")));
            this.btnFree.Name = "btnFree";
            this.btnFree.Size = new System.Drawing.Size(170, 22);
            this.btnFree.Text = "任意多边形选择";
            this.btnFree.ToolTipText = "任意多边形选择";
            this.btnFree.Visible = false;
            this.btnFree.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // CopyToolStripButton
            // 
            this.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripButton.Image")));
            this.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripButton.Name = "CopyToolStripButton";
            this.CopyToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.CopyToolStripButton.Text = "复制";
            this.CopyToolStripButton.Visible = false;
            // 
            // PasteToolStripButton
            // 
            this.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripButton.Image")));
            this.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripButton.Name = "PasteToolStripButton";
            this.PasteToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.PasteToolStripButton.Text = "粘帖";
            this.PasteToolStripButton.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // SelecttoolStripButton1
            // 
            this.SelecttoolStripButton1.Checked = true;
            this.SelecttoolStripButton1.CheckOnClick = true;
            this.SelecttoolStripButton1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SelecttoolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelecttoolStripButton1.Enabled = false;
            this.SelecttoolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("SelecttoolStripButton1.Image")));
            this.SelecttoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelecttoolStripButton1.Name = "SelecttoolStripButton1";
            this.SelecttoolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.SelecttoolStripButton1.Text = "选择";
            this.SelecttoolStripButton1.ToolTipText = "选择元素";
            this.SelecttoolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // PanToolStripButton
            // 
            this.PanToolStripButton.CheckOnClick = true;
            this.PanToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PanToolStripButton.Enabled = false;
            this.PanToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PanToolStripButton.Image")));
            this.PanToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PanToolStripButton.Name = "PanToolStripButton";
            this.PanToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PanToolStripButton.Text = "移动";
            this.PanToolStripButton.ToolTipText = "移动地图";
            this.PanToolStripButton.Visible = false;
            this.PanToolStripButton.Click += new System.EventHandler(this.PanToolStripButton_Click);
            // 
            // ZoomInModeToolStripButton
            // 
            this.ZoomInModeToolStripButton.CheckOnClick = true;
            this.ZoomInModeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInModeToolStripButton.Enabled = false;
            this.ZoomInModeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomInModeToolStripButton.Image")));
            this.ZoomInModeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInModeToolStripButton.Name = "ZoomInModeToolStripButton";
            this.ZoomInModeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomInModeToolStripButton.Text = "缩小";
            this.ZoomInModeToolStripButton.ToolTipText = "缩小";
            this.ZoomInModeToolStripButton.Click += new System.EventHandler(this.ZoomInModeToolStripButton_Click);
            // 
            // ZoomOutModeToolStripButton
            // 
            this.ZoomOutModeToolStripButton.CheckOnClick = true;
            this.ZoomOutModeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutModeToolStripButton.Enabled = false;
            this.ZoomOutModeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutModeToolStripButton.Image")));
            this.ZoomOutModeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutModeToolStripButton.Name = "ZoomOutModeToolStripButton";
            this.ZoomOutModeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomOutModeToolStripButton.Text = "放大";
            this.ZoomOutModeToolStripButton.Click += new System.EventHandler(this.ZoomOutModeToolStripButton_Click);
            // 
            // ZoomToExtentsToolStripButton
            // 
            this.ZoomToExtentsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomToExtentsToolStripButton.Enabled = false;
            this.ZoomToExtentsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomToExtentsToolStripButton.Image")));
            this.ZoomToExtentsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomToExtentsToolStripButton.Name = "ZoomToExtentsToolStripButton";
            this.ZoomToExtentsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomToExtentsToolStripButton.Text = "全部显示";
            this.ZoomToExtentsToolStripButton.ToolTipText = "全部显示";
            this.ZoomToExtentsToolStripButton.Click += new System.EventHandler(this.ZoomToExtentsToolStripButton_Click);
            // 
            // ZoomAreatoolStripButton
            // 
            this.ZoomAreatoolStripButton.CheckOnClick = true;
            this.ZoomAreatoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomAreatoolStripButton.Enabled = false;
            this.ZoomAreatoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomAreatoolStripButton.Image")));
            this.ZoomAreatoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomAreatoolStripButton.Name = "ZoomAreatoolStripButton";
            this.ZoomAreatoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ZoomAreatoolStripButton.Text = "区域放大";
            this.ZoomAreatoolStripButton.Click += new System.EventHandler(this.ZoomAreatoolStripButton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel2.Text = "比例尺：";
            // 
            // ZoomtoolStripTextBox
            // 
            this.ZoomtoolStripTextBox.AutoSize = false;
            this.ZoomtoolStripTextBox.Items.AddRange(new object[] {
            "1:1,000",
            "1:10,000",
            "1:25,000",
            "1:100,000",
            "1:250,000",
            "1:500,000",
            "1:750,000",
            "1:1,000,000",
            "1:3,000,000",
            "1:10,000,000",
            "<自定义此列表...>"});
            this.ZoomtoolStripTextBox.Name = "ZoomtoolStripTextBox";
            this.ZoomtoolStripTextBox.Size = new System.Drawing.Size(115, 25);
            this.ZoomtoolStripTextBox.SelectedIndexChanged += new System.EventHandler(this.ZoomtoolStripTextBox_SelectedIndexChanged);
            this.ZoomtoolStripTextBox.Enter += new System.EventHandler(this.ZoomtoolStripTextBox_Enter);
            this.ZoomtoolStripTextBox.Leave += new System.EventHandler(this.ZoomtoolStripTextBox_Leave);
            this.ZoomtoolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZoomtoolStripTextBox_KeyDown);
            this.ZoomtoolStripTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ZoomtoolStripTextBox_KeyPress);
            // 
            // btnMap
            // 
            this.btnMap.CheckOnClick = true;
            this.btnMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMap.Image = ((System.Drawing.Image)(resources.GetObject("btnMap.Image")));
            this.btnMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(23, 22);
            this.btnMap.Text = "切换地图";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnLoadPicture
            // 
            this.btnLoadPicture.CheckOnClick = true;
            this.btnLoadPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadPicture.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadPicture.Image")));
            this.btnLoadPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadPicture.Name = "btnLoadPicture";
            this.btnLoadPicture.Size = new System.Drawing.Size(23, 22);
            this.btnLoadPicture.Text = "加载影像图";
            this.btnLoadPicture.Click += new System.EventHandler(this.btnLoadPicture_Click);
            // 
            // btnGuiji
            // 
            this.btnGuiji.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuiji.Image = ((System.Drawing.Image)(resources.GetObject("btnGuiji.Image")));
            this.btnGuiji.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuiji.Name = "btnGuiji";
            this.btnGuiji.Size = new System.Drawing.Size(23, 22);
            this.btnGuiji.Text = "选中点轨迹";
            this.btnGuiji.Click += new System.EventHandler(this.btnGuiji_Click);
            // 
            // btnClearGuiji
            // 
            this.btnClearGuiji.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearGuiji.Image = ((System.Drawing.Image)(resources.GetObject("btnClearGuiji.Image")));
            this.btnClearGuiji.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearGuiji.Name = "btnClearGuiji";
            this.btnClearGuiji.Size = new System.Drawing.Size(23, 22);
            this.btnClearGuiji.Text = "清除轨迹";
            this.btnClearGuiji.ToolTipText = "清除轨迹";
            this.btnClearGuiji.Click += new System.EventHandler(this.btnClearGuiji_Click);
            // 
            // btnLoadShiQu
            // 
            this.btnLoadShiQu.CheckOnClick = true;
            this.btnLoadShiQu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadShiQu.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadShiQu.Image")));
            this.btnLoadShiQu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadShiQu.Name = "btnLoadShiQu";
            this.btnLoadShiQu.Size = new System.Drawing.Size(23, 22);
            this.btnLoadShiQu.Text = "加载市街图";
            this.btnLoadShiQu.Visible = false;
            this.btnLoadShiQu.Click += new System.EventHandler(this.btnLoadShiQu_Click);
            // 
            // btnTax
            // 
            this.btnTax.CheckOnClick = true;
            this.btnTax.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTax.Image = ((System.Drawing.Image)(resources.GetObject("btnTax.Image")));
            this.btnTax.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTax.Name = "btnTax";
            this.btnTax.Size = new System.Drawing.Size(23, 22);
            this.btnTax.Text = "税务展开";
            this.btnTax.Visible = false;
            this.btnTax.Click += new System.EventHandler(this.btnTax_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // MesuretoolStripSplitButton1
            // 
            this.MesuretoolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MesuretoolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.距离量测ToolStripMenuItem,
            this.面积量测ToolStripMenuItem});
            this.MesuretoolStripSplitButton1.Enabled = false;
            this.MesuretoolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("MesuretoolStripSplitButton1.Image")));
            this.MesuretoolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MesuretoolStripSplitButton1.Name = "MesuretoolStripSplitButton1";
            this.MesuretoolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.MesuretoolStripSplitButton1.Text = "量测";
            this.MesuretoolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // 距离量测ToolStripMenuItem
            // 
            this.距离量测ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("距离量测ToolStripMenuItem.Image")));
            this.距离量测ToolStripMenuItem.Name = "距离量测ToolStripMenuItem";
            this.距离量测ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.距离量测ToolStripMenuItem.Text = "距离量测";
            this.距离量测ToolStripMenuItem.Click += new System.EventHandler(this.mesureLengthToolStripMenuItem1_Click);
            // 
            // 面积量测ToolStripMenuItem
            // 
            this.面积量测ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("面积量测ToolStripMenuItem.Image")));
            this.面积量测ToolStripMenuItem.Name = "面积量测ToolStripMenuItem";
            this.面积量测ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.面积量测ToolStripMenuItem.Text = "面积量测";
            this.面积量测ToolStripMenuItem.Click += new System.EventHandler(this.mesureAreaToolStripMenuItem1_Click);
            // 
            // PropertytoolStripButton2
            // 
            this.PropertytoolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PropertytoolStripButton2.Enabled = false;
            this.PropertytoolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("PropertytoolStripButton2.Image")));
            this.PropertytoolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PropertytoolStripButton2.Name = "PropertytoolStripButton2";
            this.PropertytoolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.PropertytoolStripButton2.Text = "属性";
            this.PropertytoolStripButton2.ToolTipText = "元素属性";
            this.PropertytoolStripButton2.Click += new System.EventHandler(this.PropertyToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.坐标录入ToolStripMenuItem,
            this.shp文件导入ToolStripMenuItem,
            this.手动绘画ToolStripMenuItem});
            this.toolStripDropDownButton1.Enabled = false;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Tag = "新增地块";
            this.toolStripDropDownButton1.Text = "新增地块";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // 坐标录入ToolStripMenuItem
            // 
            this.坐标录入ToolStripMenuItem.Name = "坐标录入ToolStripMenuItem";
            this.坐标录入ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.坐标录入ToolStripMenuItem.Text = "坐标输入";
            this.坐标录入ToolStripMenuItem.Click += new System.EventHandler(this.InputDatatoolStripButton1_Click);
            // 
            // shp文件导入ToolStripMenuItem
            // 
            this.shp文件导入ToolStripMenuItem.Name = "shp文件导入ToolStripMenuItem";
            this.shp文件导入ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.shp文件导入ToolStripMenuItem.Text = "宗地信息图层";
            this.shp文件导入ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // 手动绘画ToolStripMenuItem
            // 
            this.手动绘画ToolStripMenuItem.Name = "手动绘画ToolStripMenuItem";
            this.手动绘画ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.手动绘画ToolStripMenuItem.Text = "手动添加区域";
            this.手动绘画ToolStripMenuItem.Click += new System.EventHandler(this.AddPolygonToolStripMenuItem_Click);
            // 
            // InputDatatoolStripButton1
            // 
            this.InputDatatoolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InputDatatoolStripButton1.Enabled = false;
            this.InputDatatoolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("InputDatatoolStripButton1.Image")));
            this.InputDatatoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InputDatatoolStripButton1.Name = "InputDatatoolStripButton1";
            this.InputDatatoolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.InputDatatoolStripButton1.Text = "坐标输入";
            this.InputDatatoolStripButton1.Visible = false;
            this.InputDatatoolStripButton1.Click += new System.EventHandler(this.InputDatatoolStripButton1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnShowPriceColor
            // 
            this.btnShowPriceColor.CheckOnClick = true;
            this.btnShowPriceColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowPriceColor.Enabled = false;
            this.btnShowPriceColor.Image = ((System.Drawing.Image)(resources.GetObject("btnShowPriceColor.Image")));
            this.btnShowPriceColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowPriceColor.Name = "btnShowPriceColor";
            this.btnShowPriceColor.Size = new System.Drawing.Size(23, 22);
            this.btnShowPriceColor.Text = "按税率级别着色";
            this.btnShowPriceColor.Visible = false;
            this.btnShowPriceColor.Click += new System.EventHandler(this.btnShowPriceColor_Click);
            // 
            // CompareToolStripButton
            // 
            this.CompareToolStripButton.CheckOnClick = true;
            this.CompareToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CompareToolStripButton.Enabled = false;
            this.CompareToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CompareToolStripButton.Image")));
            this.CompareToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CompareToolStripButton.Name = "CompareToolStripButton";
            this.CompareToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CompareToolStripButton.Text = "对比";
            this.CompareToolStripButton.Visible = false;
            this.CompareToolStripButton.Click += new System.EventHandler(this.CompareToolStripButton_Click);
            // 
            // ReporttoolStripButton
            // 
            this.ReporttoolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ReporttoolStripButton.Enabled = false;
            this.ReporttoolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ReporttoolStripButton.Image")));
            this.ReporttoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReporttoolStripButton.Name = "ReporttoolStripButton";
            this.ReporttoolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ReporttoolStripButton.Text = "报表";
            this.ReporttoolStripButton.Visible = false;
            this.ReporttoolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // FindSaveBoat
            // 
            this.FindSaveBoat.Enabled = false;
            this.FindSaveBoat.Name = "FindSaveBoat";
            this.FindSaveBoat.Size = new System.Drawing.Size(121, 25);
            // 
            // btnFindSaveBoat
            // 
            this.btnFindSaveBoat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFindSaveBoat.Enabled = false;
            this.btnFindSaveBoat.ForeColor = System.Drawing.Color.Black;
            this.btnFindSaveBoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindSaveBoat.Name = "btnFindSaveBoat";
            this.btnFindSaveBoat.Size = new System.Drawing.Size(83, 22);
            this.btnFindSaveBoat.Text = "附近救援力量";
            this.btnFindSaveBoat.ToolTipText = "附近救援力量";
            this.btnFindSaveBoat.Click += new System.EventHandler(this.附近救援队_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FindAreaTextBox
            // 
            this.FindAreaTextBox.Enabled = false;
            this.FindAreaTextBox.Name = "FindAreaTextBox";
            this.FindAreaTextBox.Size = new System.Drawing.Size(121, 25);
            this.FindAreaTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FindAreaTextBox_KeyUp);
            // 
            // btnFindArea
            // 
            this.btnFindArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFindArea.Enabled = false;
            this.btnFindArea.ForeColor = System.Drawing.Color.Black;
            this.btnFindArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindArea.Name = "btnFindArea";
            this.btnFindArea.Size = new System.Drawing.Size(59, 22);
            this.btnFindArea.Text = "地图搜索";
            this.btnFindArea.ToolTipText = "地图搜索";
            this.btnFindArea.Click += new System.EventHandler(this.btnFindArea_Click);
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 22);
            this.btnClear.Text = "清除选择元素";
            this.btnClear.ToolTipText = "清除选择元素";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnMapToImage
            // 
            this.btnMapToImage.CheckOnClick = true;
            this.btnMapToImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapToImage.Enabled = false;
            this.btnMapToImage.Image = ((System.Drawing.Image)(resources.GetObject("btnMapToImage.Image")));
            this.btnMapToImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapToImage.Name = "btnMapToImage";
            this.btnMapToImage.Size = new System.Drawing.Size(23, 22);
            this.btnMapToImage.Text = "截图";
            this.btnMapToImage.ToolTipText = "截图";
            this.btnMapToImage.Click += new System.EventHandler(this.btnMapToImage_Click);
            // 
            // btnEditLayerList
            // 
            this.btnEditLayerList.Image = ((System.Drawing.Image)(resources.GetObject("btnEditLayerList.Image")));
            this.btnEditLayerList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditLayerList.Name = "btnEditLayerList";
            this.btnEditLayerList.Size = new System.Drawing.Size(87, 22);
            this.btnEditLayerList.Text = "编辑图层";
            // 
            // lblCurrentLayer
            // 
            this.lblCurrentLayer.Name = "lblCurrentLayer";
            this.lblCurrentLayer.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnDataSearch
            // 
            this.btnDataSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDataSearch.Enabled = false;
            this.btnDataSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnDataSearch.Image")));
            this.btnDataSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataSearch.Name = "btnDataSearch";
            this.btnDataSearch.Size = new System.Drawing.Size(23, 22);
            this.btnDataSearch.Text = "数据查询";
            this.btnDataSearch.Visible = false;
            this.btnDataSearch.Click += new System.EventHandler(this.btnDataSearch_Click);
            // 
            // 救援报告ToolStripMenuItem
            // 
            this.救援报告ToolStripMenuItem.Name = "救援报告ToolStripMenuItem";
            this.救援报告ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.救援报告ToolStripMenuItem.Text = "救援报告打印";
            this.救援报告ToolStripMenuItem.ToolTipText = "救援报告打印";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://uri.amap.com/marker?position=116.473195,39.993253", System.UriKind.Absolute);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer4.Size = new System.Drawing.Size(481, 358);
            this.splitContainer4.SplitterDistance = 220;
            this.splitContainer4.TabIndex = 0;
            // 
            // AddLayerDialog
            // 
            this.AddLayerDialog.Filter = "Shapefiles|*.shp|*.TIF|*.TIF|*.JPEG|*.JPG|All files|*.*";
            this.AddLayerDialog.InitialDirectory = ".";
            this.AddLayerDialog.Multiselect = true;
            this.AddLayerDialog.RestoreDirectory = true;
            this.AddLayerDialog.Title = "Choose Layer Data";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPEG格式|*.jpg|PNG格式|*.png|BMP格式|*.bmp|GIF格式|*.gif";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer4
            // 
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer5
            // 
            this.timer5.Enabled = true;
            this.timer5.Interval = 50000;
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // timer6
            // 
            this.timer6.Enabled = true;
            this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1170, 461);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "海事搜救信息平台 Ver1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.axShockwaveFlash1_MouseCaptureChanged);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.LayerContextMenu.ResumeLayout(false);
            this.MapPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFlash2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapImage2)).EndInit();
            this.MapContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MyMapEditImage2)).EndInit();
            this.MapPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFlash1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainMapImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyMapEditImage1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.SplitContainer splitContainer4;
        private EasyMap.Forms.MapImage MainMapImage1;
		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.ToolStripButton NewToolStripButton;
		private System.Windows.Forms.ToolStripButton OpenToolStripButton;
		private System.Windows.Forms.ToolStripButton SaveToolStripButton;
		private System.Windows.Forms.ToolStripButton PrintToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel CoordinatesLabel;
		private System.Windows.Forms.ToolStripButton ZoomToExtentsToolStripButton;
        private System.Windows.Forms.ToolStripButton ZoomInModeToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton RemoveLayerToolStripButton;
		private System.Windows.Forms.ContextMenuStrip LayerContextMenu;
		private System.Windows.Forms.ToolStripMenuItem MoveUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MoveDownToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator LayerContextMenuSeparator;
		private System.Windows.Forms.ToolStripMenuItem AddLayerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RemoveLayerToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog AddLayerDialog;
        private System.Windows.Forms.ToolStripButton PanToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private MyTree LayerView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ZuoBiaoLabel;
        private EasyMap.UI.Forms.MapEditImage MyMapEditImage1;
        private System.Windows.Forms.ContextMenuStrip MapContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddPolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddPolygonTempStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddProblemPointStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddProblemPolygonStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePolygonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddPriceMotionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePriceMotionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MeasureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem PrintPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Measure1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CloseMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CutToolStripButton;
        private System.Windows.Forms.ToolStripButton CopyToolStripButton;
        private System.Windows.Forms.ToolStripButton PasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem CopyXYToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem DeleteObjectToolStripMenuItem;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripMenuItem PropertyDefineToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem InsertLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InsertPricePointToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem DeleteLayerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DeleteAreaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem DeletePricePointToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton SelecttoolStripButton1;
        private System.Windows.Forms.ToolStripSplitButton MesuretoolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem 距离量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 面积量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mesureLengthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mesureAreaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton PropertytoolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem mesure1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mesure2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem chooseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XYtoolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton AddLayerToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem 影像图图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基础图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 评估报告图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 监测点图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 交易案例图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 宗地信息图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 房屋售价图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 房屋租赁图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton AddNewRandomGeometryLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem SearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基础图层ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 宗地信息图层ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 其他图层ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 影像图图层ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 基础图层ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 宗地信息图层ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 其他图层ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ZoomToExtentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton InputDatatoolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem printsetuptoolStripMenuItem14;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ToolStripMenuItem 选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩放ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全图显示ToolStripMenuItem;
        private MyTree LayerView2;
        private EasyMap.Forms.MapImage MainMapImage2;
        private EasyMap.UI.Forms.MapEditImage MyMapEditImage2;
        private System.Windows.Forms.ToolStripButton CompareToolStripButton;
        private System.Windows.Forms.ToolStripButton ZoomAreatoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem ZoomAreatoolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ReporttoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TifSplitToolStripMenuItem;
        private System.Windows.Forms.Timer timer3;
        private MapControl mapControl1;
        private System.Windows.Forms.TreeView treeView1;
        private EasyMap.Controls.Waiting waiting1;
        private System.Windows.Forms.ToolStripButton btnFindArea;
        private System.Windows.Forms.ToolStripButton btnMapToImage;
        private System.Windows.Forms.Label label1;
        private MyButton btnMapToImageOk;
        private MyButton btnMapToImageCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripComboBox FindAreaTextBox;
        private System.Windows.Forms.ToolStripMenuItem VisibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OutPutShp;
        private System.Windows.Forms.ToolStripMenuItem MergeShp;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private EasyMap.Controls.MyToolTipControl myToolTipControl1;
        private EasyMap.Controls.MyToolTipControl myToolTipControl2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem CustomReportMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private ProjectControl projectControl1;
        private System.Windows.Forms.ToolStripMenuItem btnPropertyControl;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem btnParameterSettings;
        private System.Windows.Forms.ToolStripMenuItem btnEditLayer;
        private System.Windows.Forms.ToolStripButton btnDataSearch;
        private System.Windows.Forms.ToolStripButton btnLoadPicture;
        private System.Windows.Forms.ToolStripMenuItem btnZoomToLayer;
        private System.Windows.Forms.ToolStripSplitButton btnSelect;
        private System.Windows.Forms.ToolStripMenuItem btnRectangel;
        private System.Windows.Forms.ToolStripMenuItem btnFreeCircle;
        private System.Windows.Forms.ToolStripMenuItem btnCircle;
        private System.Windows.Forms.ToolStripMenuItem btnFree;
        private System.Windows.Forms.ToolStripMenuItem btnZoomToSelectObjects;
        private System.Windows.Forms.ToolStripMenuItem btnClearSelectObjects;
        private System.Windows.Forms.ToolStripComboBox ZoomtoolStripTextBox;
        private Crom.Controls.Docking.DockContainer dockContainer1;
        private System.Windows.Forms.Panel MapPanel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel MapPanel2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem btnLayerView;
        private System.Windows.Forms.ToolStripMenuItem btnDataView;
        private System.Windows.Forms.ToolStripMenuItem btnPropertyView;
        private System.Windows.Forms.ToolStripMenuItem btnPictureView;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
        private System.Windows.Forms.ToolStripButton ZoomOutModeToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem ZoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 放大ToolStripMenuItem;
        private System.Windows.Forms.PictureBox picFlash1;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox picFlash2;
        private System.Windows.Forms.ToolStripSplitButton btnEditLayerList;
        private System.Windows.Forms.ToolStripLabel lblCurrentLayer;
        private System.Windows.Forms.ToolStripMenuItem btnClearAreaPriceFill;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.Timer timer6;
        private UserControl1 userControl11;
        private System.Windows.Forms.ToolStripButton btnShowPriceColor;
        private System.Windows.Forms.ToolStripMenuItem 船舶信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 轨迹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置为遇难船舶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜救队信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 无人机救援信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 救援报告ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 派出搜救队船舶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnProjectView;
        private System.Windows.Forms.ToolStripButton btnLoadShiQu;
        private System.Windows.Forms.ToolStripMenuItem btnSetObjectStyle;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteObjectStyle;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripButton btnTax;
        private System.Windows.Forms.ToolStripMenuItem pic_out;
        private System.Windows.Forms.ToolStripMenuItem quanxian;
        private System.Windows.Forms.ToolStripMenuItem 权限设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem passwordUpdate;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tudi_message;
        private System.Windows.Forms.ToolStripButton shuiwu_message;
        private System.Windows.Forms.ToolStripMenuItem 影像图层级设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理员ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel info;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem 坐标录入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shp文件导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动绘画ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改地块上土地注记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改图层分类样式ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新生成管辖视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox FindSaveBoat;
        private System.Windows.Forms.ToolStripButton btnFindSaveBoat;
        private System.Windows.Forms.ToolStripMenuItem 搜索救援队范围设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船舶刷新时间设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 派出救援队ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置遇难船舶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船舶信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 救援队信息查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnMap;
        private System.Windows.Forms.ToolStripMenuItem 选中区域内查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选中区域内船舶信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选中区域内救援队信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选中区域内无人机信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 派出救援报告查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnGuiji;
        private System.Windows.Forms.ToolStripButton btnClearGuiji;
	}
}

