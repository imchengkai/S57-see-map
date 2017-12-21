using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyMap.Controls
{
    public partial class UserControl1 : UserControl
    {
        private bool _Change = false;
        Label lbInfo = new Label();       //显示信息
        Bitmap tempmap;
        public delegate void AreaClickEvent();
        public event AreaClickEvent AreaClick;
        public UserControl1()
        {
            InitializeComponent();
            //设置label

            this.Controls.Add(lbInfo);
            lbInfo.Parent = pictureBox1;
            lbInfo.BackColor = Color.FromArgb(150, 0, 0, 0);
            lbInfo.ForeColor = Color.White;
            lbInfo.TextAlign = ContentAlignment.MiddleCenter;
            lbInfo.Visible = false;
            pictureBox3.Parent = pictureBox1;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= tempmap.Width || e.Y >= tempmap.Height)
            {
                return;
            }
            Color clr = tempmap.GetPixel(e.X, e.Y);
            if (clr.ToArgb() == Color.Black.ToArgb())
            {
                //pictureBox1.Cursor = Cursors.Hand;
                //Bitmap map = global::EasyMap.Properties.Resources._22222.Clone() as Bitmap;
                //Graphics g = Graphics.FromImage(map);
                //g.DrawImage(global::EasyMap.Properties.Resources.info2, 0, 0);
                //g.Dispose();
                //pictureBox1.Image = map;
                pictureBox3.Visible = true;
                _Change = true;
                lbInfo.Text = "高新园区";
                if (!lbInfo.Visible)
                {
                    lbInfo.Visible = true;
                }
            }
            //else
            //{
            //    if (_Change)
            //    {
            //        pictureBox1.Image = global::EasyMap.Properties.Resources._22222;
            //        _Change = false;
            //        lbInfo.Visible = false;
            //    }
            //    pictureBox1.Cursor = Cursors.Arrow;
            //}
            //if (lbInfo != null)
            //{
            //    //设置label位置
            //    if (e.X + 20 + lbInfo.Width >= pictureBox1.Width)
            //        lbInfo.Left = e.X - 20 - lbInfo.Width;
            //    else
            //        lbInfo.Left = e.X + 20;
            //    if (e.Y + 20 + lbInfo.Height >= pictureBox1.Height)
            //        lbInfo.Top = e.Y - 20 - lbInfo.Height;
            //    else
            //        lbInfo.Top = e.Y + 20;
            //}
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (pictureBox2.Width <= 0 || pictureBox2.Height <= 0)
            {
                return;
            }
            tempmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.DrawToBitmap(tempmap, pictureBox2.ClientRectangle);
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= tempmap.Width || e.Y >= tempmap.Height)
            {
                return;
            }
            try
            {
                Color clr = tempmap.GetPixel(e.X, e.Y);
                if (clr.ToArgb() != Color.Black.ToArgb())
                {
                    pictureBox3.Visible = false;
                    _Change = false;
                    lbInfo.Visible = false;
                    return;
                }
                if (lbInfo != null)
                {
                    //设置label位置
                    if (e.X + 20 + lbInfo.Width >= pictureBox1.Width)
                        lbInfo.Left = e.X - 20 - lbInfo.Width;
                    else
                        lbInfo.Left = e.X + 20;
                    if (e.Y + 20 + lbInfo.Height >= pictureBox1.Height)
                        lbInfo.Top = e.Y - 20 - lbInfo.Height;
                    else
                        lbInfo.Top = e.Y + 20;
                }
            }
            catch
            { }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            if (_Change)
            {
                if (AreaClick != null)
                {
                    AreaClick();
                }
            }
        }
    }
}
