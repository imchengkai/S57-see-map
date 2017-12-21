using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EasyMap.Forms;
using EasyMap.Geometries;
using EasyMap.Layers;
using System.Collections.ObjectModel;
using EasyMap.Data.Providers;

namespace EasyMap
{
    public partial class MeasureForm : MyForm
    {
        private MapImage _MainMapImage = null;
        
        public MapImage MainMapImage
        {
            get { return _MainMapImage; }
            set { _MainMapImage = value; }
        }
        
        public MeasureForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 测量窗口关闭时，设置地图当前操作为什么都不做
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainMapImage.ActiveTool = MapImage.Tools.None;
        }

        public void SetLength(double totalLength, double currentLength)
        {
            textBox1.Text = String.Format("测量距离：\r\n当前距离：{0:N5}\r\n总距离：{1:N5}",currentLength, totalLength);
        }

        public void SetArea(double area)
        {
            textBox1.Text = String.Format("测量面积：\r\n当前面积：{0:N5}", area);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
