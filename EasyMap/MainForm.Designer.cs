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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ͼ��");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            EasyMap.Controls.MyGeometryList myGeometryList1 = new EasyMap.Controls.MyGeometryList();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("ͼ��");
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
            this.ѡ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ƶ�ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�Ŵ�ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ȫͼ��ʾToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.������ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�켣ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����Ϊ���Ѵ���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�Ѿȶ���ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ɳ��ѾȶӴ���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.���˻���Ԯ��ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.����ͼ��ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.�ڵ���Ϣͼ��ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ��ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ӱ��ͼͼ��ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ��ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.�ڵ���Ϣͼ��ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ��ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InsertPricePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteLayerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAreaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePricePointToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Ӱ��ͼ�㼶����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.���ĵؿ�������ע��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ�������ʽToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�������ɹ�Ͻ��ͼToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TifSplitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPropertyControl = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomReportMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.btnParameterSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.������Ԯ�ӷ�Χ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ˢ��ʱ������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Measure1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mesure1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mesure2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.������Ϣ��ѯToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��Ԯ����Ϣ��ѯToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ɳ���Ԯ�����ѯToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ѡ�������ڲ�ѯToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ѡ�������ڴ�����ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ѡ�����������˻���ϢToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ɳ���Ԯ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�������Ѵ���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanxian = new System.Windows.Forms.ToolStripMenuItem();
            this.Ȩ������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ԱToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Ӱ��ͼͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��������ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.���װ���ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ڵ���Ϣͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�����ۼ�ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��������ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.����ͼ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.��������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertytoolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.����¼��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shp�ļ�����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�ֶ��滭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.��Ԯ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.info.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.info.ForeColor = System.Drawing.Color.Crimson;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(452, 17);
            this.info.Spring = true;
            this.info.Text = "���ڱȶԲ���������";
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
            treeNode1.Text = "ͼ��";
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
            this.MoveUpToolStripMenuItem.Text = "����";
            this.MoveUpToolStripMenuItem.ToolTipText = "����";
            this.MoveUpToolStripMenuItem.Click += new System.EventHandler(this.MoveUpToolStripMenuItem_Click);
            // 
            // MoveDownToolStripMenuItem
            // 
            this.MoveDownToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveDownToolStripMenuItem.Image")));
            this.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem";
            this.MoveDownToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.MoveDownToolStripMenuItem.Text = "����";
            this.MoveDownToolStripMenuItem.ToolTipText = "����";
            this.MoveDownToolStripMenuItem.Click += new System.EventHandler(this.MoveDownToolStripMenuItem_Click);
            // 
            // btnZoomToLayer
            // 
            this.btnZoomToLayer.Name = "btnZoomToLayer";
            this.btnZoomToLayer.Size = new System.Drawing.Size(160, 22);
            this.btnZoomToLayer.Text = "���ŵ�ͼ�㷶Χ";
            this.btnZoomToLayer.ToolTipText = "���ŵ�ͼ�㷶Χ";
            this.btnZoomToLayer.Click += new System.EventHandler(this.btnZoomToLayer_Click);
            // 
            // btnEditLayer
            // 
            this.btnEditLayer.Name = "btnEditLayer";
            this.btnEditLayer.Size = new System.Drawing.Size(160, 22);
            this.btnEditLayer.Text = "�༭ͼ��";
            this.btnEditLayer.ToolTipText = "�༭ͼ��";
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
            this.AddLayerToolStripMenuItem.Text = "���ͼ��";
            this.AddLayerToolStripMenuItem.ToolTipText = "���ͼ��";
            this.AddLayerToolStripMenuItem.Visible = false;
            this.AddLayerToolStripMenuItem.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // RemoveLayerToolStripMenuItem
            // 
            this.RemoveLayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RemoveLayerToolStripMenuItem.Image")));
            this.RemoveLayerToolStripMenuItem.Name = "RemoveLayerToolStripMenuItem";
            this.RemoveLayerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RemoveLayerToolStripMenuItem.Text = "ɾ��ͼ��";
            this.RemoveLayerToolStripMenuItem.ToolTipText = "ɾ��ͼ��";
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
            this.DeleteObjectToolStripMenuItem.Text = "ɾ��Ԫ��";
            this.DeleteObjectToolStripMenuItem.ToolTipText = "ɾ��Ԫ��";
            this.DeleteObjectToolStripMenuItem.Visible = false;
            this.DeleteObjectToolStripMenuItem.Click += new System.EventHandler(this.DeletePolygonToolStripMenuItem_Click);
            // 
            // SearchToolStripMenuItem
            // 
            this.SearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SearchToolStripMenuItem.Image")));
            this.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem";
            this.SearchToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.SearchToolStripMenuItem.Text = "���ݲ�ѯ";
            this.SearchToolStripMenuItem.ToolTipText = "���ݲ�ѯ";
            this.SearchToolStripMenuItem.Click += new System.EventHandler(this.SearchToolStripMenuItem_Click);
            // 
            // VisibleToolStripMenuItem
            // 
            this.VisibleToolStripMenuItem.Name = "VisibleToolStripMenuItem";
            this.VisibleToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.VisibleToolStripMenuItem.Text = "�ɼ�������";
            this.VisibleToolStripMenuItem.ToolTipText = "�ɼ�������";
            this.VisibleToolStripMenuItem.Click += new System.EventHandler(this.VisibleToolStripMenuItem_Click);
            // 
            // OutPutShp
            // 
            this.OutPutShp.Name = "OutPutShp";
            this.OutPutShp.Size = new System.Drawing.Size(160, 22);
            this.OutPutShp.Text = "����ͼ��";
            this.OutPutShp.ToolTipText = "����ͼ��";
            this.OutPutShp.Visible = false;
            this.OutPutShp.Click += new System.EventHandler(this.button5_Click);
            // 
            // MergeShp
            // 
            this.MergeShp.Name = "MergeShp";
            this.MergeShp.Size = new System.Drawing.Size(160, 22);
            this.MergeShp.Text = "�ϲ�������ͼ��";
            this.MergeShp.ToolTipText = "�ϲ�������ͼ��";
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
            this.myToolTipControl2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.ѡ��ToolStripMenuItem,
            this.�ƶ�ToolStripMenuItem,
            this.����ToolStripMenuItem,
            this.�Ŵ�ToolStripMenuItem,
            this.ȫͼ��ʾToolStripMenuItem,
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
            this.������ϢToolStripMenuItem,
            this.�켣ToolStripMenuItem,
            this.����Ϊ���Ѵ���ToolStripMenuItem,
            this.�Ѿȶ���ϢToolStripMenuItem,
            this.�ɳ��ѾȶӴ���ToolStripMenuItem,
            this.���˻���Ԯ��ϢToolStripMenuItem});
            this.MapContextMenu.Name = "MapContextMenu";
            this.MapContextMenu.Size = new System.Drawing.Size(185, 642);
            this.MapContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.LayerContextMenu_Opening);
            // 
            // ѡ��ToolStripMenuItem
            // 
            this.ѡ��ToolStripMenuItem.CheckOnClick = true;
            this.ѡ��ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ѡ��ToolStripMenuItem.Image")));
            this.ѡ��ToolStripMenuItem.Name = "ѡ��ToolStripMenuItem";
            this.ѡ��ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ѡ��ToolStripMenuItem.Text = "ѡ��";
            this.ѡ��ToolStripMenuItem.ToolTipText = "ѡ��";
            this.ѡ��ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // �ƶ�ToolStripMenuItem
            // 
            this.�ƶ�ToolStripMenuItem.CheckOnClick = true;
            this.�ƶ�ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("�ƶ�ToolStripMenuItem.Image")));
            this.�ƶ�ToolStripMenuItem.Name = "�ƶ�ToolStripMenuItem";
            this.�ƶ�ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.�ƶ�ToolStripMenuItem.Text = "�ƶ�";
            this.�ƶ�ToolStripMenuItem.ToolTipText = "�ƶ�";
            this.�ƶ�ToolStripMenuItem.Click += new System.EventHandler(this.PanToolStripButton_Click);
            // 
            // ����ToolStripMenuItem
            // 
            this.����ToolStripMenuItem.CheckOnClick = true;
            this.����ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("����ToolStripMenuItem.Image")));
            this.����ToolStripMenuItem.Name = "����ToolStripMenuItem";
            this.����ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.����ToolStripMenuItem.Text = "��С";
            this.����ToolStripMenuItem.ToolTipText = "��С";
            this.����ToolStripMenuItem.Click += new System.EventHandler(this.ZoomInModeToolStripButton_Click);
            // 
            // �Ŵ�ToolStripMenuItem
            // 
            this.�Ŵ�ToolStripMenuItem.CheckOnClick = true;
            this.�Ŵ�ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("�Ŵ�ToolStripMenuItem.Image")));
            this.�Ŵ�ToolStripMenuItem.Name = "�Ŵ�ToolStripMenuItem";
            this.�Ŵ�ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.�Ŵ�ToolStripMenuItem.Text = "�Ŵ�";
            this.�Ŵ�ToolStripMenuItem.ToolTipText = "�Ŵ�";
            this.�Ŵ�ToolStripMenuItem.Click += new System.EventHandler(this.ZoomOutModeToolStripButton_Click);
            // 
            // ȫͼ��ʾToolStripMenuItem
            // 
            this.ȫͼ��ʾToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ȫͼ��ʾToolStripMenuItem.Image")));
            this.ȫͼ��ʾToolStripMenuItem.Name = "ȫͼ��ʾToolStripMenuItem";
            this.ȫͼ��ʾToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ȫͼ��ʾToolStripMenuItem.Text = "ȫͼ��ʾ";
            this.ȫͼ��ʾToolStripMenuItem.ToolTipText = "ȫͼ��ʾ";
            this.ȫͼ��ʾToolStripMenuItem.Click += new System.EventHandler(this.ZoomToExtentsToolStripButton_Click);
            // 
            // ZoomAreatoolStripMenuItem
            // 
            this.ZoomAreatoolStripMenuItem.CheckOnClick = true;
            this.ZoomAreatoolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomAreatoolStripMenuItem.Image")));
            this.ZoomAreatoolStripMenuItem.Name = "ZoomAreatoolStripMenuItem";
            this.ZoomAreatoolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ZoomAreatoolStripMenuItem.Text = "����Ŵ�";
            this.ZoomAreatoolStripMenuItem.ToolTipText = "����Ŵ�";
            this.ZoomAreatoolStripMenuItem.Click += new System.EventHandler(this.ZoomAreatoolStripButton_Click);
            // 
            // btnZoomToSelectObjects
            // 
            this.btnZoomToSelectObjects.Name = "btnZoomToSelectObjects";
            this.btnZoomToSelectObjects.Size = new System.Drawing.Size(184, 22);
            this.btnZoomToSelectObjects.Text = "��������ѡҪ��";
            this.btnZoomToSelectObjects.ToolTipText = "��������ѡҪ��";
            this.btnZoomToSelectObjects.Click += new System.EventHandler(this.btnZoomToSelectObjects_Click);
            // 
            // btnClearSelectObjects
            // 
            this.btnClearSelectObjects.Name = "btnClearSelectObjects";
            this.btnClearSelectObjects.Size = new System.Drawing.Size(184, 22);
            this.btnClearSelectObjects.Text = "�����ѡҪ��";
            this.btnClearSelectObjects.ToolTipText = "�����ѡҪ��";
            this.btnClearSelectObjects.Click += new System.EventHandler(this.btnClearSelectObjects_Click);
            // 
            // btnClearAreaPriceFill
            // 
            this.btnClearAreaPriceFill.Name = "btnClearAreaPriceFill";
            this.btnClearAreaPriceFill.Size = new System.Drawing.Size(184, 22);
            this.btnClearAreaPriceFill.Text = "�����ͼ�������";
            this.btnClearAreaPriceFill.Visible = false;
            this.btnClearAreaPriceFill.Click += new System.EventHandler(this.btnClearAreaPriceFill_Click);
            // 
            // CopyXYToolStripMenuItem
            // 
            this.CopyXYToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyXYToolStripMenuItem.Image")));
            this.CopyXYToolStripMenuItem.Name = "CopyXYToolStripMenuItem";
            this.CopyXYToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.CopyXYToolStripMenuItem.Text = "��������";
            this.CopyXYToolStripMenuItem.ToolTipText = "��������";
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
            this.MeasureToolStripMenuItem.Text = "����";
            this.MeasureToolStripMenuItem.ToolTipText = "����";
            // 
            // mesureLengthToolStripMenuItem1
            // 
            this.mesureLengthToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesureLengthToolStripMenuItem1.Image")));
            this.mesureLengthToolStripMenuItem1.Name = "mesureLengthToolStripMenuItem1";
            this.mesureLengthToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.mesureLengthToolStripMenuItem1.Text = "��������";
            this.mesureLengthToolStripMenuItem1.Click += new System.EventHandler(this.mesureLengthToolStripMenuItem1_Click);
            // 
            // mesureAreaToolStripMenuItem1
            // 
            this.mesureAreaToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesureAreaToolStripMenuItem1.Image")));
            this.mesureAreaToolStripMenuItem1.Name = "mesureAreaToolStripMenuItem1";
            this.mesureAreaToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.mesureAreaToolStripMenuItem1.Text = "�������";
            this.mesureAreaToolStripMenuItem1.Click += new System.EventHandler(this.mesureAreaToolStripMenuItem1_Click);
            // 
            // XYtoolStripMenuItem
            // 
            this.XYtoolStripMenuItem.Name = "XYtoolStripMenuItem";
            this.XYtoolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.XYtoolStripMenuItem.Text = "���궨λ";
            this.XYtoolStripMenuItem.ToolTipText = "���궨λ";
            this.XYtoolStripMenuItem.Click += new System.EventHandler(this.XYtoolStripMenuItem_Click);
            // 
            // AddPolygonToolStripMenuItem
            // 
            this.AddPolygonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddPolygonToolStripMenuItem.Image")));
            this.AddPolygonToolStripMenuItem.Name = "AddPolygonToolStripMenuItem";
            this.AddPolygonToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddPolygonToolStripMenuItem.Text = "�������";
            this.AddPolygonToolStripMenuItem.ToolTipText = "�������";
            this.AddPolygonToolStripMenuItem.Visible = false;
            this.AddPolygonToolStripMenuItem.Click += new System.EventHandler(this.AddPolygonToolStripMenuItem_Click);
            // 
            // AddPolygonTempStripMenuItem
            // 
            this.AddPolygonTempStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddPolygonTempStripMenuItem.Image")));
            this.AddPolygonTempStripMenuItem.Name = "AddPolygonTempStripMenuItem";
            this.AddPolygonTempStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddPolygonTempStripMenuItem.Text = "��Ӿ�Ԯ����";
            this.AddPolygonTempStripMenuItem.ToolTipText = "��Ӿ�Ԯ����";
            this.AddPolygonTempStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddPolygonTempStripMenuItem_Click);
            // 
            // AddProblemPointStripMenuItem
            // 
            this.AddProblemPointStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddProblemPointStripMenuItem.Image")));
            this.AddProblemPointStripMenuItem.Name = "AddProblemPointStripMenuItem";
            this.AddProblemPointStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddProblemPointStripMenuItem.Text = "������ѵ�";
            this.AddProblemPointStripMenuItem.ToolTipText = "������ѵ�";
            this.AddProblemPointStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddProblemPointStripMenuItem_Click);
            // 
            // AddProblemPolygonStripMenuItem
            // 
            this.AddProblemPolygonStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddProblemPolygonStripMenuItem.Image")));
            this.AddProblemPolygonStripMenuItem.Name = "AddProblemPolygonStripMenuItem";
            this.AddProblemPolygonStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddProblemPolygonStripMenuItem.Text = "�����������";
            this.AddProblemPolygonStripMenuItem.ToolTipText = "�����������";
            this.AddProblemPolygonStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddProblemAreaStripMenuItem_Click);
            // 
            // DeletePolygonToolStripMenuItem
            // 
            this.DeletePolygonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DeletePolygonToolStripMenuItem.Image")));
            this.DeletePolygonToolStripMenuItem.Name = "DeletePolygonToolStripMenuItem";
            this.DeletePolygonToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DeletePolygonToolStripMenuItem.Text = "ɾ��Ԫ��";
            this.DeletePolygonToolStripMenuItem.ToolTipText = "ɾ��Ԫ��";
            this.DeletePolygonToolStripMenuItem.Click += new System.EventHandler(this.DeletePolygonToolStripMenuItem_Click);
            // 
            // AddPriceMotionToolStripMenuItem
            // 
            this.AddPriceMotionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddPriceMotionToolStripMenuItem.Image")));
            this.AddPriceMotionToolStripMenuItem.Name = "AddPriceMotionToolStripMenuItem";
            this.AddPriceMotionToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.AddPriceMotionToolStripMenuItem.Text = "��Ӽ���";
            this.AddPriceMotionToolStripMenuItem.ToolTipText = "��ӵؼۼ���";
            this.AddPriceMotionToolStripMenuItem.Click += new System.EventHandler(this.AddPriceMotionToolStripMenuItem_Click);
            // 
            // DeletePriceMotionToolStripMenuItem
            // 
            this.DeletePriceMotionToolStripMenuItem.Name = "DeletePriceMotionToolStripMenuItem";
            this.DeletePriceMotionToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DeletePriceMotionToolStripMenuItem.Text = "ɾ������";
            this.DeletePriceMotionToolStripMenuItem.ToolTipText = "ɾ���ؼۼ���";
            this.DeletePriceMotionToolStripMenuItem.Click += new System.EventHandler(this.DeletePriceMotionToolStripMenuItem_Click);
            // 
            // btnSetObjectStyle
            // 
            this.btnSetObjectStyle.Name = "btnSetObjectStyle";
            this.btnSetObjectStyle.Size = new System.Drawing.Size(184, 22);
            this.btnSetObjectStyle.Text = "������ʾ��ʽ";
            this.btnSetObjectStyle.Click += new System.EventHandler(this.btnSetObjectStyle_Click);
            // 
            // btnDeleteObjectStyle
            // 
            this.btnDeleteObjectStyle.Name = "btnDeleteObjectStyle";
            this.btnDeleteObjectStyle.Size = new System.Drawing.Size(184, 22);
            this.btnDeleteObjectStyle.Text = "ɾ����ʽ";
            this.btnDeleteObjectStyle.Click += new System.EventHandler(this.btnDeleteObjectStyle_Click);
            // 
            // PropertyToolStripMenuItem
            // 
            this.PropertyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PropertyToolStripMenuItem.Image")));
            this.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem";
            this.PropertyToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.PropertyToolStripMenuItem.Text = "����";
            this.PropertyToolStripMenuItem.ToolTipText = "����";
            this.PropertyToolStripMenuItem.Click += new System.EventHandler(this.PropertyToolStripMenuItem_Click);
            // 
            // PictureToolStripMenuItem
            // 
            this.PictureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PictureToolStripMenuItem.Image")));
            this.PictureToolStripMenuItem.Name = "PictureToolStripMenuItem";
            this.PictureToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.PictureToolStripMenuItem.Text = "��Ƭ";
            this.PictureToolStripMenuItem.ToolTipText = "��Ƭ";
            this.PictureToolStripMenuItem.Click += new System.EventHandler(this.PictureToolStripMenuItem_Click);
            // 
            // ������ϢToolStripMenuItem
            // 
            this.������ϢToolStripMenuItem.Name = "������ϢToolStripMenuItem";
            this.������ϢToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.������ϢToolStripMenuItem.Text = "������Ϣ";
            this.������ϢToolStripMenuItem.ToolTipText = "������Ϣ";
            this.������ϢToolStripMenuItem.Click += new System.EventHandler(this.BoatSetting_Click);
            // 
            // �켣ToolStripMenuItem
            // 
            this.�켣ToolStripMenuItem.Name = "�켣ToolStripMenuItem";
            this.�켣ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.�켣ToolStripMenuItem.Text = "��ʾ�켣";
            this.�켣ToolStripMenuItem.ToolTipText = "��ʾ�켣";
            this.�켣ToolStripMenuItem.Click += new System.EventHandler(this.�켣ToolStripMenuItem_Click);
            // 
            // ����Ϊ���Ѵ���ToolStripMenuItem
            // 
            this.����Ϊ���Ѵ���ToolStripMenuItem.Name = "����Ϊ���Ѵ���ToolStripMenuItem";
            this.����Ϊ���Ѵ���ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.����Ϊ���Ѵ���ToolStripMenuItem.Text = "����Ϊ���Ѵ���";
            this.����Ϊ���Ѵ���ToolStripMenuItem.ToolTipText = "����Ϊ���Ѵ���";
            this.����Ϊ���Ѵ���ToolStripMenuItem.Click += new System.EventHandler(this.����Ϊ���Ѵ���ToolStripMenuItem_Click);
            // 
            // �Ѿȶ���ϢToolStripMenuItem
            // 
            this.�Ѿȶ���ϢToolStripMenuItem.Name = "�Ѿȶ���ϢToolStripMenuItem";
            this.�Ѿȶ���ϢToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.�Ѿȶ���ϢToolStripMenuItem.Text = "��Ԯ������Ϣ";
            this.�Ѿȶ���ϢToolStripMenuItem.ToolTipText = "��Ԯ������Ϣ";
            this.�Ѿȶ���ϢToolStripMenuItem.Click += new System.EventHandler(this.RescueBoatSetting_Click);
            // 
            // �ɳ��ѾȶӴ���ToolStripMenuItem
            // 
            this.�ɳ��ѾȶӴ���ToolStripMenuItem.Name = "�ɳ��ѾȶӴ���ToolStripMenuItem";
            this.�ɳ��ѾȶӴ���ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.�ɳ��ѾȶӴ���ToolStripMenuItem.Text = "�ɳ���Ԯ����";
            this.�ɳ��ѾȶӴ���ToolStripMenuItem.ToolTipText = "�ɳ���Ԯ����";
            this.�ɳ��ѾȶӴ���ToolStripMenuItem.Click += new System.EventHandler(this.�ɳ��ѾȶӴ���ToolStripMenuItem_Click);
            // 
            // ���˻���Ԯ��ϢToolStripMenuItem
            // 
            this.���˻���Ԯ��ϢToolStripMenuItem.Name = "���˻���Ԯ��ϢToolStripMenuItem";
            this.���˻���Ԯ��ϢToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.���˻���Ԯ��ϢToolStripMenuItem.Text = "��Ԯ�������˻���Ϣ";
            this.���˻���Ԯ��ϢToolStripMenuItem.ToolTipText = "��Ԯ�������˻���Ϣ";
            this.���˻���Ԯ��ϢToolStripMenuItem.Visible = false;
            this.���˻���Ԯ��ϢToolStripMenuItem.Click += new System.EventHandler(this.���˻���Ԯ��ϢToolStripMenuItem_Click);
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
            this.projectControl1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            treeNode2.Text = "ͼ��";
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
            this.myToolTipControl1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.btnMapToImageCancel.Text = "ȡ��";
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
            this.btnMapToImageOk.Text = "����";
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
            this.waiting1.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waiting1.Location = new System.Drawing.Point(427, 163);
            this.waiting1.MaxProcessValue = 35;
            this.waiting1.MinProcessValue = 0;
            this.waiting1.Name = "waiting1";
            this.waiting1.ProcessValue = 16;
            this.waiting1.Size = new System.Drawing.Size(316, 60);
            this.waiting1.TabIndex = 4;
            this.waiting1.Tip = "����ȡ�õ�ͼ���ݣ����Ժ�...";
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
            this.����ToolStripMenuItem,
            this.ReportToolStripMenuItem,
            this.����ToolStripMenuItem,
            this.quanxian,
            this.����ToolStripMenuItem});
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
            this.FileToolStripMenuItem.Text = "�ļ�(&F)";
            // 
            // NewMapToolStripMenuItem
            // 
            this.NewMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewMapToolStripMenuItem.Image")));
            this.NewMapToolStripMenuItem.Name = "NewMapToolStripMenuItem";
            this.NewMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.NewMapToolStripMenuItem.Text = "�½���ͼ(&N)";
            this.NewMapToolStripMenuItem.Visible = false;
            this.NewMapToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripButton_Click);
            // 
            // OpenMapToolStripMenuItem
            // 
            this.OpenMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("OpenMapToolStripMenuItem.Image")));
            this.OpenMapToolStripMenuItem.Name = "OpenMapToolStripMenuItem";
            this.OpenMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.OpenMapToolStripMenuItem.Text = "�򿪵�ͼ(&O)";
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
            this.SaveMapToolStripMenuItem.Text = "�����ͼ(&S)";
            this.SaveMapToolStripMenuItem.Visible = false;
            this.SaveMapToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripButton_Click);
            // 
            // CloseMapToolStripMenuItem
            // 
            this.CloseMapToolStripMenuItem.Enabled = false;
            this.CloseMapToolStripMenuItem.Name = "CloseMapToolStripMenuItem";
            this.CloseMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CloseMapToolStripMenuItem.Text = "�رյ�ͼ(&C)";
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
            this.passwordUpdate.Text = "�޸�����(&D)";
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
            this.printsetuptoolStripMenuItem14.Text = "��ӡ����...";
            this.printsetuptoolStripMenuItem14.Click += new System.EventHandler(this.printsetuptoolStripMenuItem14_Click);
            // 
            // PrintPreviewToolStripMenuItem
            // 
            this.PrintPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintPreviewToolStripMenuItem.Image")));
            this.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem";
            this.PrintPreviewToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PrintPreviewToolStripMenuItem.Text = "��ӡԤ��(&W)";
            this.PrintPreviewToolStripMenuItem.Click += new System.EventHandler(this.PrintPreviewToolStripMenuItem_Click);
            // 
            // PrintToolStripMenuItem
            // 
            this.PrintToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PrintToolStripMenuItem.Image")));
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PrintToolStripMenuItem.Text = "��ӡ(&P)";
            this.PrintToolStripMenuItem.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // pic_out
            // 
            this.pic_out.Image = ((System.Drawing.Image)(resources.GetObject("pic_out.Image")));
            this.pic_out.Name = "pic_out";
            this.pic_out.Size = new System.Drawing.Size(182, 22);
            this.pic_out.Text = "������ͼ(&Q)";
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
            this.ExitToolStripMenuItem.Text = "�˳�(&X)";
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
            this.Ӱ��ͼ�㼶����ToolStripMenuItem,
            this.���ĵؿ�������ע��ToolStripMenuItem,
            this.����ͼ�������ʽToolStripMenuItem,
            this.�������ɹ�Ͻ��ͼToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.EditToolStripMenuItem.Text = "�༭(&E)";
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CutToolStripMenuItem.Image")));
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CutToolStripMenuItem.Text = "����(&T)";
            this.CutToolStripMenuItem.Visible = false;
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CopyToolStripMenuItem.Image")));
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CopyToolStripMenuItem.Text = "����(&C)";
            this.CopyToolStripMenuItem.Visible = false;
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripMenuItem.Image")));
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PasteToolStripMenuItem.Text = "ճ��(&P)";
            this.PasteToolStripMenuItem.Visible = false;
            // 
            // PropertyDefineToolStripMenuItem
            // 
            this.PropertyDefineToolStripMenuItem.Name = "PropertyDefineToolStripMenuItem";
            this.PropertyDefineToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.PropertyDefineToolStripMenuItem.Text = "���Զ���(&A)";
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
            this.����ͼ��ToolStripMenuItem1,
            this.�ڵ���Ϣͼ��ToolStripMenuItem1,
            this.����ͼ��ToolStripMenuItem1});
            this.InsertLayerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InsertLayerToolStripMenuItem.Image")));
            this.InsertLayerToolStripMenuItem.Name = "InsertLayerToolStripMenuItem";
            this.InsertLayerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.InsertLayerToolStripMenuItem.Text = "�½�ͼ��";
            // 
            // ����ͼ��ToolStripMenuItem1
            // 
            this.����ͼ��ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("����ͼ��ToolStripMenuItem1.Image")));
            this.����ͼ��ToolStripMenuItem1.Name = "����ͼ��ToolStripMenuItem1";
            this.����ͼ��ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem1.Text = "����ͼ��";
            this.����ͼ��ToolStripMenuItem1.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // �ڵ���Ϣͼ��ToolStripMenuItem1
            // 
            this.�ڵ���Ϣͼ��ToolStripMenuItem1.Name = "�ڵ���Ϣͼ��ToolStripMenuItem1";
            this.�ڵ���Ϣͼ��ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.�ڵ���Ϣͼ��ToolStripMenuItem1.Text = "�ڵ���Ϣͼ��";
            this.�ڵ���Ϣͼ��ToolStripMenuItem1.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // ����ͼ��ToolStripMenuItem1
            // 
            this.����ͼ��ToolStripMenuItem1.Name = "����ͼ��ToolStripMenuItem1";
            this.����ͼ��ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem1.Text = "����ͼ��";
            this.����ͼ��ToolStripMenuItem1.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ӱ��ͼͼ��ToolStripMenuItem1,
            this.����ͼ��ToolStripMenuItem2,
            this.�ڵ���Ϣͼ��ToolStripMenuItem2,
            this.����ͼ��ToolStripMenuItem2});
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.OpenToolStripMenuItem.Text = "��ͼ��";
            // 
            // Ӱ��ͼͼ��ToolStripMenuItem1
            // 
            this.Ӱ��ͼͼ��ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("Ӱ��ͼͼ��ToolStripMenuItem1.Image")));
            this.Ӱ��ͼͼ��ToolStripMenuItem1.Name = "Ӱ��ͼͼ��ToolStripMenuItem1";
            this.Ӱ��ͼͼ��ToolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
            this.Ӱ��ͼͼ��ToolStripMenuItem1.Text = "Ӱ��ͼͼ��";
            this.Ӱ��ͼͼ��ToolStripMenuItem1.Visible = false;
            this.Ӱ��ͼͼ��ToolStripMenuItem1.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ����ͼ��ToolStripMenuItem2
            // 
            this.����ͼ��ToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("����ͼ��ToolStripMenuItem2.Image")));
            this.����ͼ��ToolStripMenuItem2.Name = "����ͼ��ToolStripMenuItem2";
            this.����ͼ��ToolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem2.Text = "����ͼ��";
            this.����ͼ��ToolStripMenuItem2.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // �ڵ���Ϣͼ��ToolStripMenuItem2
            // 
            this.�ڵ���Ϣͼ��ToolStripMenuItem2.Name = "�ڵ���Ϣͼ��ToolStripMenuItem2";
            this.�ڵ���Ϣͼ��ToolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.�ڵ���Ϣͼ��ToolStripMenuItem2.Text = "�ڵ���Ϣͼ��";
            this.�ڵ���Ϣͼ��ToolStripMenuItem2.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ����ͼ��ToolStripMenuItem2
            // 
            this.����ͼ��ToolStripMenuItem2.Name = "����ͼ��ToolStripMenuItem2";
            this.����ͼ��ToolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem2.Text = "����ͼ��";
            this.����ͼ��ToolStripMenuItem2.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // InsertAreaToolStripMenuItem
            // 
            this.InsertAreaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InsertAreaToolStripMenuItem.Image")));
            this.InsertAreaToolStripMenuItem.Name = "InsertAreaToolStripMenuItem";
            this.InsertAreaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.InsertAreaToolStripMenuItem.Text = "�½�����";
            this.InsertAreaToolStripMenuItem.Click += new System.EventHandler(this.AddPolygonToolStripMenuItem_Click);
            // 
            // InsertPricePointToolStripMenuItem
            // 
            this.InsertPricePointToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InsertPricePointToolStripMenuItem.Image")));
            this.InsertPricePointToolStripMenuItem.Name = "InsertPricePointToolStripMenuItem";
            this.InsertPricePointToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.InsertPricePointToolStripMenuItem.Text = "�½�����";
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
            this.DeleteLayerToolStripMenuItem1.Text = "ɾ��ͼ��";
            this.DeleteLayerToolStripMenuItem1.Click += new System.EventHandler(this.RemoveLayerToolStripButton_Click);
            // 
            // DeleteAreaToolStripMenuItem1
            // 
            this.DeleteAreaToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("DeleteAreaToolStripMenuItem1.Image")));
            this.DeleteAreaToolStripMenuItem1.Name = "DeleteAreaToolStripMenuItem1";
            this.DeleteAreaToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.DeleteAreaToolStripMenuItem1.Text = "ɾ������";
            this.DeleteAreaToolStripMenuItem1.Click += new System.EventHandler(this.DeletePolygonToolStripMenuItem_Click);
            // 
            // DeletePricePointToolStripMenuItem1
            // 
            this.DeletePricePointToolStripMenuItem1.Name = "DeletePricePointToolStripMenuItem1";
            this.DeletePricePointToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.DeletePricePointToolStripMenuItem1.Text = "ɾ������";
            this.DeletePricePointToolStripMenuItem1.Visible = false;
            this.DeletePricePointToolStripMenuItem1.Click += new System.EventHandler(this.DeletePriceMotionToolStripMenuItem_Click);
            // 
            // Ӱ��ͼ�㼶����ToolStripMenuItem
            // 
            this.Ӱ��ͼ�㼶����ToolStripMenuItem.Name = "Ӱ��ͼ�㼶����ToolStripMenuItem";
            this.Ӱ��ͼ�㼶����ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.Ӱ��ͼ�㼶����ToolStripMenuItem.Text = "Ӱ��ͼ�㼶����";
            this.Ӱ��ͼ�㼶����ToolStripMenuItem.Visible = false;
            this.Ӱ��ͼ�㼶����ToolStripMenuItem.Click += new System.EventHandler(this.Ӱ��ͼ�㼶����ToolStripMenuItem_Click);
            // 
            // ���ĵؿ�������ע��ToolStripMenuItem
            // 
            this.���ĵؿ�������ע��ToolStripMenuItem.Name = "���ĵؿ�������ע��ToolStripMenuItem";
            this.���ĵؿ�������ע��ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.���ĵؿ�������ע��ToolStripMenuItem.Text = "���ĵؿ����ڵ�ע��";
            this.���ĵؿ�������ע��ToolStripMenuItem.Visible = false;
            this.���ĵؿ�������ע��ToolStripMenuItem.Click += new System.EventHandler(this.���ĵؿ�������ע��ToolStripMenuItem_Click);
            // 
            // ����ͼ�������ʽToolStripMenuItem
            // 
            this.����ͼ�������ʽToolStripMenuItem.Name = "����ͼ�������ʽToolStripMenuItem";
            this.����ͼ�������ʽToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.����ͼ�������ʽToolStripMenuItem.Text = "����ͼ�������ʽ";
            this.����ͼ�������ʽToolStripMenuItem.Click += new System.EventHandler(this.����ͼ�������ʽToolStripMenuItem_Click);
            // 
            // �������ɹ�Ͻ��ͼToolStripMenuItem
            // 
            this.�������ɹ�Ͻ��ͼToolStripMenuItem.Name = "�������ɹ�Ͻ��ͼToolStripMenuItem";
            this.�������ɹ�Ͻ��ͼToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.�������ɹ�Ͻ��ͼToolStripMenuItem.Text = "�������ɹ�Ͻ��ͼ";
            this.�������ɹ�Ͻ��ͼToolStripMenuItem.Click += new System.EventHandler(this.�������ɹ�Ͻ��ͼToolStripMenuItem_Click);
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
            this.ViewToolStripMenuItem.Text = "��ͼ(&V)";
            // 
            // chooseToolStripMenuItem
            // 
            this.chooseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("chooseToolStripMenuItem.Image")));
            this.chooseToolStripMenuItem.Name = "chooseToolStripMenuItem";
            this.chooseToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.chooseToolStripMenuItem.Text = "ѡ��";
            this.chooseToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MoveToolStripMenuItem
            // 
            this.MoveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("MoveToolStripMenuItem.Image")));
            this.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem";
            this.MoveToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.MoveToolStripMenuItem.Text = "�ƶ�";
            this.MoveToolStripMenuItem.Visible = false;
            this.MoveToolStripMenuItem.Click += new System.EventHandler(this.PanToolStripButton_Click);
            // 
            // ZoomToolStripMenuItem
            // 
            this.ZoomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomToolStripMenuItem.Image")));
            this.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem";
            this.ZoomToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ZoomToolStripMenuItem.Text = "��С";
            this.ZoomToolStripMenuItem.ToolTipText = "��С";
            this.ZoomToolStripMenuItem.Click += new System.EventHandler(this.ZoomInModeToolStripButton_Click);
            // 
            // ZoomOutToolStripMenuItem
            // 
            this.ZoomOutToolStripMenuItem.CheckOnClick = true;
            this.ZoomOutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutToolStripMenuItem.Image")));
            this.ZoomOutToolStripMenuItem.Name = "ZoomOutToolStripMenuItem";
            this.ZoomOutToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ZoomOutToolStripMenuItem.Text = "�Ŵ�";
            this.ZoomOutToolStripMenuItem.ToolTipText = "�Ŵ�";
            this.ZoomOutToolStripMenuItem.Click += new System.EventHandler(this.ZoomOutModeToolStripButton_Click);
            // 
            // ZoomToExtentsToolStripMenuItem
            // 
            this.ZoomToExtentsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ZoomToExtentsToolStripMenuItem.Image")));
            this.ZoomToExtentsToolStripMenuItem.Name = "ZoomToExtentsToolStripMenuItem";
            this.ZoomToExtentsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ZoomToExtentsToolStripMenuItem.Text = "��ͼȫ��ʾ";
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
            this.btnLayerView.Text = "ͼ����ͼ";
            this.btnLayerView.Click += new System.EventHandler(this.btnLayerView_Click);
            // 
            // btnDataView
            // 
            this.btnDataView.CheckOnClick = true;
            this.btnDataView.Name = "btnDataView";
            this.btnDataView.Size = new System.Drawing.Size(134, 22);
            this.btnDataView.Text = "������ͼ";
            this.btnDataView.Click += new System.EventHandler(this.btnDataView_Click);
            // 
            // btnPropertyView
            // 
            this.btnPropertyView.CheckOnClick = true;
            this.btnPropertyView.Name = "btnPropertyView";
            this.btnPropertyView.Size = new System.Drawing.Size(134, 22);
            this.btnPropertyView.Text = "������ͼ";
            this.btnPropertyView.Click += new System.EventHandler(this.btnPropertyView_Click);
            // 
            // btnPictureView
            // 
            this.btnPictureView.CheckOnClick = true;
            this.btnPictureView.Name = "btnPictureView";
            this.btnPictureView.Size = new System.Drawing.Size(134, 22);
            this.btnPictureView.Text = "ͼƬ��ͼ";
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
            this.btnProjectView.Text = "��Ͻ��ͼ";
            this.btnProjectView.Click += new System.EventHandler(this.toolStripMenuItem19_Click);
            // 
            // ����ToolStripMenuItem
            // 
            this.����ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TifSplitToolStripMenuItem,
            this.btnPropertyControl,
            this.CustomReportMenuItem2,
            this.toolStripMenuItem15,
            this.btnParameterSettings,
            this.������Ԯ�ӷ�Χ����ToolStripMenuItem,
            this.����ˢ��ʱ������ToolStripMenuItem});
            this.����ToolStripMenuItem.Name = "����ToolStripMenuItem";
            this.����ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.����ToolStripMenuItem.Text = "����(&T)";
            // 
            // TifSplitToolStripMenuItem
            // 
            this.TifSplitToolStripMenuItem.Name = "TifSplitToolStripMenuItem";
            this.TifSplitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.TifSplitToolStripMenuItem.Text = "Ӱ��ͼ���";
            this.TifSplitToolStripMenuItem.Visible = false;
            this.TifSplitToolStripMenuItem.Click += new System.EventHandler(this.TifSplitToolStripMenuItem_Click);
            // 
            // btnPropertyControl
            // 
            this.btnPropertyControl.Name = "btnPropertyControl";
            this.btnPropertyControl.Size = new System.Drawing.Size(182, 22);
            this.btnPropertyControl.Text = "���Կ���";
            this.btnPropertyControl.Visible = false;
            this.btnPropertyControl.Click += new System.EventHandler(this.btnPropertyControl_Click);
            // 
            // CustomReportMenuItem2
            // 
            this.CustomReportMenuItem2.Name = "CustomReportMenuItem2";
            this.CustomReportMenuItem2.Size = new System.Drawing.Size(182, 22);
            this.CustomReportMenuItem2.Text = "�Զ��屨��";
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
            this.btnParameterSettings.Text = "ϵͳ��������";
            this.btnParameterSettings.Visible = false;
            this.btnParameterSettings.Click += new System.EventHandler(this.btnParameterSettings_Click);
            // 
            // ������Ԯ�ӷ�Χ����ToolStripMenuItem
            // 
            this.������Ԯ�ӷ�Χ����ToolStripMenuItem.Name = "������Ԯ�ӷ�Χ����ToolStripMenuItem";
            this.������Ԯ�ӷ�Χ����ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.������Ԯ�ӷ�Χ����ToolStripMenuItem.Text = "������Ԯ�ӷ�Χ����";
            this.������Ԯ�ӷ�Χ����ToolStripMenuItem.Click += new System.EventHandler(this.������Ԯ�ӷ�Χ����ToolStripMenuItem_Click);
            // 
            // ����ˢ��ʱ������ToolStripMenuItem
            // 
            this.����ˢ��ʱ������ToolStripMenuItem.Name = "����ˢ��ʱ������ToolStripMenuItem";
            this.����ˢ��ʱ������ToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.����ˢ��ʱ������ToolStripMenuItem.Text = "����ˢ��ʱ������";
            this.����ˢ��ʱ������ToolStripMenuItem.Click += new System.EventHandler(this.����ˢ��ʱ������ToolStripMenuItem_Click);
            // 
            // ReportToolStripMenuItem
            // 
            this.ReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Measure1ToolStripMenuItem,
            this.toolStripMenuItem14,
            this.������Ϣ��ѯToolStripMenuItem,
            this.��Ԯ����Ϣ��ѯToolStripMenuItem,
            this.�ɳ���Ԯ�����ѯToolStripMenuItem,
            this.ѡ�������ڲ�ѯToolStripMenuItem});
            this.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem";
            this.ReportToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ReportToolStripMenuItem.Text = "��ѯ(&Q)";
            // 
            // Measure1ToolStripMenuItem
            // 
            this.Measure1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mesure1ToolStripMenuItem1,
            this.mesure2ToolStripMenuItem1});
            this.Measure1ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Measure1ToolStripMenuItem.Image")));
            this.Measure1ToolStripMenuItem.Name = "Measure1ToolStripMenuItem";
            this.Measure1ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.Measure1ToolStripMenuItem.Text = "����";
            this.Measure1ToolStripMenuItem.Visible = false;
            // 
            // mesure1ToolStripMenuItem1
            // 
            this.mesure1ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesure1ToolStripMenuItem1.Image")));
            this.mesure1ToolStripMenuItem1.Name = "mesure1ToolStripMenuItem1";
            this.mesure1ToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.mesure1ToolStripMenuItem1.Text = "��������";
            this.mesure1ToolStripMenuItem1.Click += new System.EventHandler(this.mesureLengthToolStripMenuItem1_Click);
            // 
            // mesure2ToolStripMenuItem1
            // 
            this.mesure2ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("mesure2ToolStripMenuItem1.Image")));
            this.mesure2ToolStripMenuItem1.Name = "mesure2ToolStripMenuItem1";
            this.mesure2ToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.mesure2ToolStripMenuItem1.Text = "�������";
            this.mesure2ToolStripMenuItem1.Click += new System.EventHandler(this.mesureAreaToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(167, 6);
            this.toolStripMenuItem14.Visible = false;
            // 
            // ������Ϣ��ѯToolStripMenuItem
            // 
            this.������Ϣ��ѯToolStripMenuItem.Name = "������Ϣ��ѯToolStripMenuItem";
            this.������Ϣ��ѯToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.������Ϣ��ѯToolStripMenuItem.Text = "������Ϣ��ѯ";
            this.������Ϣ��ѯToolStripMenuItem.Click += new System.EventHandler(this.������Ϣ��ѯToolStripMenuItem_Click);
            // 
            // ��Ԯ����Ϣ��ѯToolStripMenuItem
            // 
            this.��Ԯ����Ϣ��ѯToolStripMenuItem.Name = "��Ԯ����Ϣ��ѯToolStripMenuItem";
            this.��Ԯ����Ϣ��ѯToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.��Ԯ����Ϣ��ѯToolStripMenuItem.Text = "��Ԯ������Ϣ��ѯ";
            this.��Ԯ����Ϣ��ѯToolStripMenuItem.Click += new System.EventHandler(this.��Ԯ����Ϣ��ѯToolStripMenuItem_Click);
            // 
            // �ɳ���Ԯ�����ѯToolStripMenuItem
            // 
            this.�ɳ���Ԯ�����ѯToolStripMenuItem.Name = "�ɳ���Ԯ�����ѯToolStripMenuItem";
            this.�ɳ���Ԯ�����ѯToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.�ɳ���Ԯ�����ѯToolStripMenuItem.Text = "�ɳ���Ԯ�����ѯ";
            this.�ɳ���Ԯ�����ѯToolStripMenuItem.Click += new System.EventHandler(this.�ɳ���Ԯ�����ѯToolStripMenuItem_Click);
            // 
            // ѡ�������ڲ�ѯToolStripMenuItem
            // 
            this.ѡ�������ڲ�ѯToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ѡ�������ڴ�����ϢToolStripMenuItem,
            this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem,
            this.ѡ�����������˻���ϢToolStripMenuItem});
            this.ѡ�������ڲ�ѯToolStripMenuItem.Name = "ѡ�������ڲ�ѯToolStripMenuItem";
            this.ѡ�������ڲ�ѯToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.ѡ�������ڲ�ѯToolStripMenuItem.Text = "ѡ�������ڲ�ѯ";
            // 
            // ѡ�������ڴ�����ϢToolStripMenuItem
            // 
            this.ѡ�������ڴ�����ϢToolStripMenuItem.Name = "ѡ�������ڴ�����ϢToolStripMenuItem";
            this.ѡ�������ڴ�����ϢToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ѡ�������ڴ�����ϢToolStripMenuItem.Text = "ѡ�������ڴ�����Ϣ";
            this.ѡ�������ڴ�����ϢToolStripMenuItem.Click += new System.EventHandler(this.ѡ�������ڴ�����ϢToolStripMenuItem_Click);
            // 
            // ѡ�������ھ�Ԯ����ϢToolStripMenuItem
            // 
            this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem.Name = "ѡ�������ھ�Ԯ����ϢToolStripMenuItem";
            this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem.Text = "ѡ�������ھ�Ԯ������Ϣ";
            this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem.Click += new System.EventHandler(this.ѡ�������ھ�Ԯ����ϢToolStripMenuItem_Click);
            // 
            // ѡ�����������˻���ϢToolStripMenuItem
            // 
            this.ѡ�����������˻���ϢToolStripMenuItem.Name = "ѡ�����������˻���ϢToolStripMenuItem";
            this.ѡ�����������˻���ϢToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ѡ�����������˻���ϢToolStripMenuItem.Text = "ѡ�����������˻���Ϣ";
            this.ѡ�����������˻���ϢToolStripMenuItem.Visible = false;
            this.ѡ�����������˻���ϢToolStripMenuItem.Click += new System.EventHandler(this.ѡ�����������˻���ϢToolStripMenuItem_Click);
            // 
            // ����ToolStripMenuItem
            // 
            this.����ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.�ɳ���Ԯ��ToolStripMenuItem,
            this.�������Ѵ���ToolStripMenuItem});
            this.����ToolStripMenuItem.Name = "����ToolStripMenuItem";
            this.����ToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.����ToolStripMenuItem.Text = "����(M)";
            // 
            // �ɳ���Ԯ��ToolStripMenuItem
            // 
            this.�ɳ���Ԯ��ToolStripMenuItem.Name = "�ɳ���Ԯ��ToolStripMenuItem";
            this.�ɳ���Ԯ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.�ɳ���Ԯ��ToolStripMenuItem.Text = "�ɳ���Ԯ����";
            this.�ɳ���Ԯ��ToolStripMenuItem.Click += new System.EventHandler(this.�ɳ���Ԯ��ToolStripMenuItem_Click);
            // 
            // �������Ѵ���ToolStripMenuItem
            // 
            this.�������Ѵ���ToolStripMenuItem.Name = "�������Ѵ���ToolStripMenuItem";
            this.�������Ѵ���ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.�������Ѵ���ToolStripMenuItem.Text = "�������Ѵ���";
            this.�������Ѵ���ToolStripMenuItem.Click += new System.EventHandler(this.�������Ѵ���ToolStripMenuItem_Click);
            // 
            // quanxian
            // 
            this.quanxian.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ȩ������ToolStripMenuItem,
            this.����ԱToolStripMenuItem});
            this.quanxian.Name = "quanxian";
            this.quanxian.Size = new System.Drawing.Size(83, 20);
            this.quanxian.Text = "Ȩ������(&G)";
            this.quanxian.Visible = false;
            this.quanxian.Click += new System.EventHandler(this.quanxian_Click);
            // 
            // Ȩ������ToolStripMenuItem
            // 
            this.Ȩ������ToolStripMenuItem.Name = "Ȩ������ToolStripMenuItem";
            this.Ȩ������ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.Ȩ������ToolStripMenuItem.Text = "Ȩ������";
            this.Ȩ������ToolStripMenuItem.Click += new System.EventHandler(this.Ȩ������ToolStripMenuItem_Click);
            // 
            // ����ԱToolStripMenuItem
            // 
            this.����ԱToolStripMenuItem.Name = "����ԱToolStripMenuItem";
            this.����ԱToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.����ԱToolStripMenuItem.Text = "����ԱȨ������";
            this.����ԱToolStripMenuItem.Visible = false;
            this.����ԱToolStripMenuItem.Click += new System.EventHandler(this.����ԱToolStripMenuItem_Click);
            // 
            // ����ToolStripMenuItem
            // 
            this.����ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.����ToolStripMenuItem1,
            this.����ToolStripMenuItem});
            this.����ToolStripMenuItem.Name = "����ToolStripMenuItem";
            this.����ToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.����ToolStripMenuItem.Text = "����(&H)";
            // 
            // ����ToolStripMenuItem1
            // 
            this.����ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("����ToolStripMenuItem1.Image")));
            this.����ToolStripMenuItem1.Name = "����ToolStripMenuItem1";
            this.����ToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.����ToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.����ToolStripMenuItem1.Text = "����";
            this.����ToolStripMenuItem1.Click += new System.EventHandler(this.����ToolStripMenuItem_Click);
            // 
            // ����ToolStripMenuItem
            // 
            this.����ToolStripMenuItem.Name = "����ToolStripMenuItem";
            this.����ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.����ToolStripMenuItem.Text = "����";
            this.����ToolStripMenuItem.Click += new System.EventHandler(this.����ToolStripMenuItem_Click);
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
            this.NewToolStripButton.Text = "�½�";
            this.NewToolStripButton.ToolTipText = "�½���ͼ";
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
            this.AddNewRandomGeometryLayer.Text = "�½�ͼ��";
            this.AddNewRandomGeometryLayer.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click_1);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem6.Text = "����ͼ��";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem7.Text = "��������ͼ��";
            this.toolStripMenuItem7.Visible = false;
            this.toolStripMenuItem7.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem8.Text = "˰�񼶱�ͼ��";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem9.Text = "���װ���ͼ��";
            this.toolStripMenuItem9.Visible = false;
            this.toolStripMenuItem9.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem10.Text = "�ڵ���Ϣͼ��";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem11.Image")));
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem11.Text = "�����ۼ�ͼ��";
            this.toolStripMenuItem11.Visible = false;
            this.toolStripMenuItem11.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem12.Text = "��������ͼ��";
            this.toolStripMenuItem12.Visible = false;
            this.toolStripMenuItem12.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem13.Text = "����ͼ��";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.AddNewRandomGeometryLayer_Click);
            // 
            // AddLayerToolStripButton
            // 
            this.AddLayerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddLayerToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Ӱ��ͼͼ��ToolStripMenuItem,
            this.����ͼ��ToolStripMenuItem,
            this.��������ͼ��ToolStripMenuItem,
            this.����ͼ��ToolStripMenuItem,
            this.���װ���ͼ��ToolStripMenuItem,
            this.�ڵ���Ϣͼ��ToolStripMenuItem,
            this.�����ۼ�ͼ��ToolStripMenuItem,
            this.��������ͼ��ToolStripMenuItem,
            this.����ͼ��ToolStripMenuItem});
            this.AddLayerToolStripButton.Enabled = false;
            this.AddLayerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("AddLayerToolStripButton.Image")));
            this.AddLayerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddLayerToolStripButton.Name = "AddLayerToolStripButton";
            this.AddLayerToolStripButton.Size = new System.Drawing.Size(32, 22);
            this.AddLayerToolStripButton.Text = "����ͼ��";
            this.AddLayerToolStripButton.ButtonClick += new System.EventHandler(this.AddLayerToolStripButton_ButtonClick);
            // 
            // Ӱ��ͼͼ��ToolStripMenuItem
            // 
            this.Ӱ��ͼͼ��ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Ӱ��ͼͼ��ToolStripMenuItem.Image")));
            this.Ӱ��ͼͼ��ToolStripMenuItem.Name = "Ӱ��ͼͼ��ToolStripMenuItem";
            this.Ӱ��ͼͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.Ӱ��ͼͼ��ToolStripMenuItem.Text = "Ӱ��ͼ��";
            this.Ӱ��ͼͼ��ToolStripMenuItem.Visible = false;
            this.Ӱ��ͼͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ����ͼ��ToolStripMenuItem
            // 
            this.����ͼ��ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("����ͼ��ToolStripMenuItem.Image")));
            this.����ͼ��ToolStripMenuItem.Name = "����ͼ��ToolStripMenuItem";
            this.����ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem.Text = "����ͼ��";
            this.����ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ��������ͼ��ToolStripMenuItem
            // 
            this.��������ͼ��ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("��������ͼ��ToolStripMenuItem.Image")));
            this.��������ͼ��ToolStripMenuItem.Name = "��������ͼ��ToolStripMenuItem";
            this.��������ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.��������ͼ��ToolStripMenuItem.Text = "��������ͼ��";
            this.��������ͼ��ToolStripMenuItem.Visible = false;
            this.��������ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ����ͼ��ToolStripMenuItem
            // 
            this.����ͼ��ToolStripMenuItem.Name = "����ͼ��ToolStripMenuItem";
            this.����ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem.Text = "˰�񼶱�ͼ��";
            this.����ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ���װ���ͼ��ToolStripMenuItem
            // 
            this.���װ���ͼ��ToolStripMenuItem.Name = "���װ���ͼ��ToolStripMenuItem";
            this.���װ���ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.���װ���ͼ��ToolStripMenuItem.Text = "���װ���ͼ��";
            this.���װ���ͼ��ToolStripMenuItem.Visible = false;
            this.���װ���ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // �ڵ���Ϣͼ��ToolStripMenuItem
            // 
            this.�ڵ���Ϣͼ��ToolStripMenuItem.Name = "�ڵ���Ϣͼ��ToolStripMenuItem";
            this.�ڵ���Ϣͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.�ڵ���Ϣͼ��ToolStripMenuItem.Text = "�ڵ���Ϣͼ��";
            this.�ڵ���Ϣͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // �����ۼ�ͼ��ToolStripMenuItem
            // 
            this.�����ۼ�ͼ��ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("�����ۼ�ͼ��ToolStripMenuItem.Image")));
            this.�����ۼ�ͼ��ToolStripMenuItem.Name = "�����ۼ�ͼ��ToolStripMenuItem";
            this.�����ۼ�ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.�����ۼ�ͼ��ToolStripMenuItem.Text = "�����ۼ�ͼ��";
            this.�����ۼ�ͼ��ToolStripMenuItem.Visible = false;
            this.�����ۼ�ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ��������ͼ��ToolStripMenuItem
            // 
            this.��������ͼ��ToolStripMenuItem.Name = "��������ͼ��ToolStripMenuItem";
            this.��������ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.��������ͼ��ToolStripMenuItem.Text = "��������ͼ��";
            this.��������ͼ��ToolStripMenuItem.Visible = false;
            this.��������ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // ����ͼ��ToolStripMenuItem
            // 
            this.����ͼ��ToolStripMenuItem.Name = "����ͼ��ToolStripMenuItem";
            this.����ͼ��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.����ͼ��ToolStripMenuItem.Text = "����ͼ��";
            this.����ͼ��ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // RemoveLayerToolStripButton
            // 
            this.RemoveLayerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RemoveLayerToolStripButton.Enabled = false;
            this.RemoveLayerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RemoveLayerToolStripButton.Image")));
            this.RemoveLayerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveLayerToolStripButton.Name = "RemoveLayerToolStripButton";
            this.RemoveLayerToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.RemoveLayerToolStripButton.Text = "ɾ��ͼ��";
            this.RemoveLayerToolStripButton.ToolTipText = "ɾ����ǰͼ��";
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
            this.OpenToolStripButton.Text = "��";
            this.OpenToolStripButton.ToolTipText = "�򿪵�ͼ";
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
            this.SaveToolStripButton.Text = "����";
            this.SaveToolStripButton.ToolTipText = "�����ͼ";
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
            this.PrintToolStripButton.Text = "��ӡ";
            this.PrintToolStripButton.ToolTipText = "��ӡ��ǰ��ͼ";
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
            this.CutToolStripButton.Text = "����";
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
            this.btnSelect.Text = "ѡ�񹤾�";
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
            this.btnRectangel.Text = "���ο�ѡ��";
            this.btnRectangel.ToolTipText = "���ο�ѡ��";
            this.btnRectangel.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnFreeCircle
            // 
            this.btnFreeCircle.CheckOnClick = true;
            this.btnFreeCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnFreeCircle.Image")));
            this.btnFreeCircle.Name = "btnFreeCircle";
            this.btnFreeCircle.Size = new System.Drawing.Size(170, 22);
            this.btnFreeCircle.Text = "����Բ��ѡ��";
            this.btnFreeCircle.ToolTipText = "����Բ��ѡ��";
            this.btnFreeCircle.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.CheckOnClick = true;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(170, 22);
            this.btnCircle.Text = "����뾶Բ��ѡ��";
            this.btnCircle.ToolTipText = "����뾶Բ��ѡ��";
            this.btnCircle.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnFree
            // 
            this.btnFree.CheckOnClick = true;
            this.btnFree.Image = ((System.Drawing.Image)(resources.GetObject("btnFree.Image")));
            this.btnFree.Name = "btnFree";
            this.btnFree.Size = new System.Drawing.Size(170, 22);
            this.btnFree.Text = "��������ѡ��";
            this.btnFree.ToolTipText = "��������ѡ��";
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
            this.CopyToolStripButton.Text = "����";
            this.CopyToolStripButton.Visible = false;
            // 
            // PasteToolStripButton
            // 
            this.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PasteToolStripButton.Image")));
            this.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripButton.Name = "PasteToolStripButton";
            this.PasteToolStripButton.Size = new System.Drawing.Size(23, 26);
            this.PasteToolStripButton.Text = "ճ��";
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
            this.SelecttoolStripButton1.Text = "ѡ��";
            this.SelecttoolStripButton1.ToolTipText = "ѡ��Ԫ��";
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
            this.PanToolStripButton.Text = "�ƶ�";
            this.PanToolStripButton.ToolTipText = "�ƶ���ͼ";
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
            this.ZoomInModeToolStripButton.Text = "��С";
            this.ZoomInModeToolStripButton.ToolTipText = "��С";
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
            this.ZoomOutModeToolStripButton.Text = "�Ŵ�";
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
            this.ZoomToExtentsToolStripButton.Text = "ȫ����ʾ";
            this.ZoomToExtentsToolStripButton.ToolTipText = "ȫ����ʾ";
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
            this.ZoomAreatoolStripButton.Text = "����Ŵ�";
            this.ZoomAreatoolStripButton.Click += new System.EventHandler(this.ZoomAreatoolStripButton_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel2.Text = "�����ߣ�";
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
            "<�Զ�����б�...>"});
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
            this.btnMap.Text = "�л���ͼ";
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
            this.btnLoadPicture.Text = "����Ӱ��ͼ";
            this.btnLoadPicture.Click += new System.EventHandler(this.btnLoadPicture_Click);
            // 
            // btnGuiji
            // 
            this.btnGuiji.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuiji.Image = ((System.Drawing.Image)(resources.GetObject("btnGuiji.Image")));
            this.btnGuiji.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuiji.Name = "btnGuiji";
            this.btnGuiji.Size = new System.Drawing.Size(23, 22);
            this.btnGuiji.Text = "ѡ�е�켣";
            this.btnGuiji.Click += new System.EventHandler(this.btnGuiji_Click);
            // 
            // btnClearGuiji
            // 
            this.btnClearGuiji.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearGuiji.Image = ((System.Drawing.Image)(resources.GetObject("btnClearGuiji.Image")));
            this.btnClearGuiji.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearGuiji.Name = "btnClearGuiji";
            this.btnClearGuiji.Size = new System.Drawing.Size(23, 22);
            this.btnClearGuiji.Text = "����켣";
            this.btnClearGuiji.ToolTipText = "����켣";
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
            this.btnLoadShiQu.Text = "�����н�ͼ";
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
            this.btnTax.Text = "˰��չ��";
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
            this.��������ToolStripMenuItem,
            this.�������ToolStripMenuItem});
            this.MesuretoolStripSplitButton1.Enabled = false;
            this.MesuretoolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("MesuretoolStripSplitButton1.Image")));
            this.MesuretoolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MesuretoolStripSplitButton1.Name = "MesuretoolStripSplitButton1";
            this.MesuretoolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.MesuretoolStripSplitButton1.Text = "����";
            this.MesuretoolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // ��������ToolStripMenuItem
            // 
            this.��������ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("��������ToolStripMenuItem.Image")));
            this.��������ToolStripMenuItem.Name = "��������ToolStripMenuItem";
            this.��������ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.��������ToolStripMenuItem.Text = "��������";
            this.��������ToolStripMenuItem.Click += new System.EventHandler(this.mesureLengthToolStripMenuItem1_Click);
            // 
            // �������ToolStripMenuItem
            // 
            this.�������ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("�������ToolStripMenuItem.Image")));
            this.�������ToolStripMenuItem.Name = "�������ToolStripMenuItem";
            this.�������ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.�������ToolStripMenuItem.Text = "�������";
            this.�������ToolStripMenuItem.Click += new System.EventHandler(this.mesureAreaToolStripMenuItem1_Click);
            // 
            // PropertytoolStripButton2
            // 
            this.PropertytoolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PropertytoolStripButton2.Enabled = false;
            this.PropertytoolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("PropertytoolStripButton2.Image")));
            this.PropertytoolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PropertytoolStripButton2.Name = "PropertytoolStripButton2";
            this.PropertytoolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.PropertytoolStripButton2.Text = "����";
            this.PropertytoolStripButton2.ToolTipText = "Ԫ������";
            this.PropertytoolStripButton2.Click += new System.EventHandler(this.PropertyToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.����¼��ToolStripMenuItem,
            this.shp�ļ�����ToolStripMenuItem,
            this.�ֶ��滭ToolStripMenuItem});
            this.toolStripDropDownButton1.Enabled = false;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Tag = "�����ؿ�";
            this.toolStripDropDownButton1.Text = "�����ؿ�";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // ����¼��ToolStripMenuItem
            // 
            this.����¼��ToolStripMenuItem.Name = "����¼��ToolStripMenuItem";
            this.����¼��ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.����¼��ToolStripMenuItem.Text = "��������";
            this.����¼��ToolStripMenuItem.Click += new System.EventHandler(this.InputDatatoolStripButton1_Click);
            // 
            // shp�ļ�����ToolStripMenuItem
            // 
            this.shp�ļ�����ToolStripMenuItem.Name = "shp�ļ�����ToolStripMenuItem";
            this.shp�ļ�����ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.shp�ļ�����ToolStripMenuItem.Text = "�ڵ���Ϣͼ��";
            this.shp�ļ�����ToolStripMenuItem.Click += new System.EventHandler(this.AddLayerToolStripButton_Click);
            // 
            // �ֶ��滭ToolStripMenuItem
            // 
            this.�ֶ��滭ToolStripMenuItem.Name = "�ֶ��滭ToolStripMenuItem";
            this.�ֶ��滭ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.�ֶ��滭ToolStripMenuItem.Text = "�ֶ��������";
            this.�ֶ��滭ToolStripMenuItem.Click += new System.EventHandler(this.AddPolygonToolStripMenuItem_Click);
            // 
            // InputDatatoolStripButton1
            // 
            this.InputDatatoolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InputDatatoolStripButton1.Enabled = false;
            this.InputDatatoolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("InputDatatoolStripButton1.Image")));
            this.InputDatatoolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InputDatatoolStripButton1.Name = "InputDatatoolStripButton1";
            this.InputDatatoolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.InputDatatoolStripButton1.Text = "��������";
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
            this.btnShowPriceColor.Text = "��˰�ʼ�����ɫ";
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
            this.CompareToolStripButton.Text = "�Ա�";
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
            this.ReporttoolStripButton.Text = "����";
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
            this.btnFindSaveBoat.Text = "������Ԯ����";
            this.btnFindSaveBoat.ToolTipText = "������Ԯ����";
            this.btnFindSaveBoat.Click += new System.EventHandler(this.������Ԯ��_Click);
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
            this.btnFindArea.Text = "��ͼ����";
            this.btnFindArea.ToolTipText = "��ͼ����";
            this.btnFindArea.Click += new System.EventHandler(this.btnFindArea_Click);
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 22);
            this.btnClear.Text = "���ѡ��Ԫ��";
            this.btnClear.ToolTipText = "���ѡ��Ԫ��";
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
            this.btnMapToImage.Text = "��ͼ";
            this.btnMapToImage.ToolTipText = "��ͼ";
            this.btnMapToImage.Click += new System.EventHandler(this.btnMapToImage_Click);
            // 
            // btnEditLayerList
            // 
            this.btnEditLayerList.Image = ((System.Drawing.Image)(resources.GetObject("btnEditLayerList.Image")));
            this.btnEditLayerList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditLayerList.Name = "btnEditLayerList";
            this.btnEditLayerList.Size = new System.Drawing.Size(87, 22);
            this.btnEditLayerList.Text = "�༭ͼ��";
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
            this.btnDataSearch.Text = "���ݲ�ѯ";
            this.btnDataSearch.Visible = false;
            this.btnDataSearch.Click += new System.EventHandler(this.btnDataSearch_Click);
            // 
            // ��Ԯ����ToolStripMenuItem
            // 
            this.��Ԯ����ToolStripMenuItem.Name = "��Ԯ����ToolStripMenuItem";
            this.��Ԯ����ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.��Ԯ����ToolStripMenuItem.Text = "��Ԯ�����ӡ";
            this.��Ԯ����ToolStripMenuItem.ToolTipText = "��Ԯ�����ӡ";
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
            this.saveFileDialog1.Filter = "JPEG��ʽ|*.jpg|PNG��ʽ|*.png|BMP��ʽ|*.bmp|GIF��ʽ|*.gif";
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
            this.Text = "�����Ѿ���Ϣƽ̨ Ver1.0";
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
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem ��������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mesureLengthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mesureAreaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton PropertytoolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem mesure1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mesure2ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem chooseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XYtoolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton AddLayerToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem Ӱ��ͼͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��������ͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ���װ���ͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ڵ���Ϣͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �����ۼ�ͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��������ͼ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem �ڵ���Ϣͼ��ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Ӱ��ͼͼ��ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem �ڵ���Ϣͼ��ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ��ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ZoomToExtentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton InputDatatoolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem printsetuptoolStripMenuItem14;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ToolStripMenuItem ѡ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ƶ�ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ȫͼ��ʾToolStripMenuItem;
        private MyTree LayerView2;
        private EasyMap.Forms.MapImage MainMapImage2;
        private EasyMap.UI.Forms.MapEditImage MyMapEditImage2;
        private System.Windows.Forms.ToolStripButton CompareToolStripButton;
        private System.Windows.Forms.ToolStripButton ZoomAreatoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem ZoomAreatoolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ReporttoolStripButton;
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem �Ŵ�ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem ������ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �켣ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����Ϊ���Ѵ���ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �Ѿȶ���ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ���˻���Ԯ��ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��Ԯ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ɳ��ѾȶӴ���ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnProjectView;
        private System.Windows.Forms.ToolStripButton btnLoadShiQu;
        private System.Windows.Forms.ToolStripMenuItem btnSetObjectStyle;
        private System.Windows.Forms.ToolStripMenuItem btnDeleteObjectStyle;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripButton btnTax;
        private System.Windows.Forms.ToolStripMenuItem pic_out;
        private System.Windows.Forms.ToolStripMenuItem quanxian;
        private System.Windows.Forms.ToolStripMenuItem Ȩ������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem passwordUpdate;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tudi_message;
        private System.Windows.Forms.ToolStripButton shuiwu_message;
        private System.Windows.Forms.ToolStripMenuItem Ӱ��ͼ�㼶����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ԱToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel info;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem ����¼��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shp�ļ�����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ֶ��滭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ���ĵؿ�������ע��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ͼ�������ʽToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �������ɹ�Ͻ��ͼToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox FindSaveBoat;
        private System.Windows.Forms.ToolStripButton btnFindSaveBoat;
        private System.Windows.Forms.ToolStripMenuItem ������Ԯ�ӷ�Χ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ˢ��ʱ������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ����ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ɳ���Ԯ��ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �������Ѵ���ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ������Ϣ��ѯToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ��Ԯ����Ϣ��ѯToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnMap;
        private System.Windows.Forms.ToolStripMenuItem ѡ�������ڲ�ѯToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ѡ�������ڴ�����ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ѡ�������ھ�Ԯ����ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ѡ�����������˻���ϢToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem �ɳ���Ԯ�����ѯToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnGuiji;
        private System.Windows.Forms.ToolStripButton btnClearGuiji;
	}
}

