using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Forms;
using EasyMap.Properties;

namespace EasyMap
{
    public partial class MapControl : UserControl
    {
        private MapImage _Map = null;
        private MapImage _OtherMap = null;
        private int _Step = 5;
        private double _Scale = 0.1;
        public delegate void AreaZoomEvent(object sender, EventArgs e);
        public event AreaZoomEvent AreaZoom;
        public delegate void ZoomChangeEvent(int currentlevel,int newlevel);
        public event ZoomChangeEvent ZoomChange;
        private bool _BeginDrog = false;
        private int _DrogY = 0;
        private int _LevelCount = 1;
        private int _StepLength = 1;
        private int _CurrentLevel = 0;
        private int _NewLevel = 0;

        public int CurrentLevel
        {
            get { return _CurrentLevel; }
            set 
            { 
                _CurrentLevel = value;

                pictureBox4.Top = pictureBox2.Bottom - value * _StepLength - pictureBox4.Height / 2;
                pictureBox5.Top = pictureBox4.Top + pictureBox4.Height / 2;
                pictureBox5.Height = pictureBox2.Bottom - pictureBox5.Top;
                drawPoint(pictureBox2, Resources.bar4);
                drawPoint(pictureBox5, Resources.bar2);
            }
        }

        public int LevelCount
        {
            get { return _LevelCount; }
            set 
            { 
                _LevelCount = value;
                _StepLength = pictureBox2.Height / value;
                drawPoint(pictureBox2, Resources.bar4);
                drawPoint(pictureBox5, Resources.bar2);
            }
        }


        private enum ImageType
        {
            BACK=0,
            LEFT_NORMAL=1,
            LEFT_OVER=2,
            LEFT_DOWN=3,
            UP_NORMAL=4,
            UP_OVER=5,
            UP_DOWN=6,
            RIGHT_NORMAL=7,
            RIGHT_OVER=8,
            RIGHT_DOWN=9,
            DOWN_NORMAL=10,
            DOWN_OVER=11,
            DOWN_DOWN=12,
            ZOOMOUT_NORMAL=13,
            ZOOMOUT_OVER=14,
            ZOOMOUT_DOWN=15,
            ZOOMIN_NORMAL=16,
            ZOOMIN_OVER=17,
            ZOOMIN_DOWN=18
        }

        public int Step
        {
            get { return _Step; }
            set { _Step = value; }
        }

        public MapImage Map
        {
            get { return _Map; }
            set { _Map = value; }
        }

        public MapImage OtherMap
        {
            get { return _OtherMap; }
            set { _OtherMap = value; }
        }

        public MapControl()
        {
            InitializeComponent();
            picBack.Parent = this;
            picLeft.Parent = picBack;
            picUp.Parent = picBack;
            picRight.Parent = picBack;
            picDown.Parent = picBack;
            picAreaZoom.Parent = picBack;
            picZoomOut.Parent = this;
            picZoomIn.Parent = this;
            pictureBox1.Parent = this;
            pictureBox3.Parent = this;
            pictureBox5.Parent = this;
            pictureBox2.Parent = this;
            pictureBox4.Parent = this;
            pictureBox5.Height = 0;
            pictureBox5.Top = pictureBox2.Bottom;
            pictureBox4.Top = pictureBox2.Bottom - pictureBox4.Height / 2;
            drawPoint(pictureBox2, Resources.bar4);
            drawPoint(pictureBox5, Resources.bar2);
        }

        private void MoveMap(int x, int y)
        {
            Point pnt = new Point(Map.Width / 2 + x,Map.Height/2+y);
            Map.Map.Center = Map.Map.ImageToWorld(pnt, true);
            Map.RequestFromServer = true;
            Map.Refresh();
            if (OtherMap != null && OtherMap.Map != null)
            {
                OtherMap.Map.Center = OtherMap.Map.ImageToWorld(pnt, true);
                OtherMap.RequestFromServer = true;
                OtherMap.Refresh();
            }
        }

        private void MoveRight()
        {
            MoveMap(0 - Step, 0);
        }

        private void MoveLeft()
        {
            MoveMap(Step, 0);
        }

        private void MoveUp()
        {
            MoveMap(0, Step);
        }

        private void MoveDown()
        {
            MoveMap(0, 0 - Step);
        }

        private void ZoomIn()
        {
            Map.Map.Zoom *= (1 + _Scale);
            Map.Refresh();
            if (OtherMap != null && OtherMap.Map != null)
            {
                OtherMap.Map.Zoom *= (1 + _Scale);
                OtherMap.Refresh();
            }
        }

        private void ZoomOut()
        {
            Map.Map.Zoom *= 1 / (1 + _Scale);
            Map.Refresh();
            if (OtherMap != null && OtherMap.Map != null)
            {
                OtherMap.Map.Zoom *= 1 / (1 + _Scale);
                OtherMap.Refresh();
            }
        }

        private void picUp_MouseEnter(object sender, EventArgs e)
        {
            picUp.Image = Resources.up_over;
        }

        private void picUp_MouseLeave(object sender, EventArgs e)
        {
            picUp.Image = Resources.up_normal;
        }

        private void picUp_MouseDown(object sender, MouseEventArgs e)
        {
            picUp.Image = Resources.up_down;
        }

        private void picUp_MouseUp(object sender, MouseEventArgs e)
        {
            picUp.Image = Resources.up_normal;
        }

        private void picUp_Click(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void picRight_Click(object sender, EventArgs e)
        {
            MoveRight();
        }

        private void picRight_MouseDown(object sender, MouseEventArgs e)
        {
            picRight.Image = Resources.right_down;
        }

        private void picRight_MouseEnter(object sender, EventArgs e)
        {
            picRight.Image = Resources.right_over;
        }

        private void picRight_MouseLeave(object sender, EventArgs e)
        {
            picRight.Image = Resources.right_normal;
        }

        private void picRight_MouseUp(object sender, MouseEventArgs e)
        {
            picRight.Image = Resources.right_normal;
        }

        private void picDown_MouseUp(object sender, MouseEventArgs e)
        {
            picDown.Image = Resources.down_normal;
        }

        private void picDown_MouseLeave(object sender, EventArgs e)
        {
            picDown.Image = Resources.down_normal;
        }

        private void picDown_MouseEnter(object sender, EventArgs e)
        {
            picDown.Image = Resources.down_over;
        }

        private void picDown_MouseDown(object sender, MouseEventArgs e)
        {
            picDown.Image = Resources.down_down;
        }

        private void picDown_Click(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void picLeft_Click(object sender, EventArgs e)
        {
            MoveLeft();
        }

        private void picLeft_MouseDown(object sender, MouseEventArgs e)
        {
            picLeft.Image = Resources.left_down;
        }

        private void picLeft_MouseEnter(object sender, EventArgs e)
        {
            picLeft.Image = Resources.left_over;
        }

        private void picLeft_MouseLeave(object sender, EventArgs e)
        {
            picLeft.Image = Resources.left_normal;
        }

        private void picLeft_MouseUp(object sender, MouseEventArgs e)
        {
            picLeft.Image = Resources.left_normal;
        }

        private void picZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void picZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void picBack_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void picAreaZoom_MouseDown(object sender, MouseEventArgs e)
        {
            picAreaZoom.Image = Resources.area_zoom_select;
        }

        private void picAreaZoom_MouseUp(object sender, MouseEventArgs e)
        {
            picAreaZoom.Image = Resources.area_zoom_normal;
        }

        private void picAreaZoom_Click(object sender, EventArgs e)
        {
            if (AreaZoom != null)
            {
                AreaZoom(sender, e);
            }
        }

        private void picZoomOut_MouseDown_1(object sender, MouseEventArgs e)
        {
            picZoomOut.Image = Resources.plus_select;
            _NewLevel = _CurrentLevel;
        }

        private void picZoomOut_MouseUp_1(object sender, MouseEventArgs e)
        {
            picZoomOut.Image = Resources.plus_normal;
        }

        private void picZoomIn_MouseUp(object sender, MouseEventArgs e)
        {
            picZoomIn.Image = Resources.minus_normal;
        }

        private void picZoomIn_MouseDown(object sender, MouseEventArgs e)
        {
            picZoomIn.Image = Resources.minus_select;
            _NewLevel = _CurrentLevel;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            _BeginDrog = true;
            _DrogY = Control.MousePosition.Y;
            _NewLevel = _CurrentLevel;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            _BeginDrog = false;
            if (_CurrentLevel != _NewLevel)
            {
                if (ZoomChange != null)
                {
                    ZoomChange(_NewLevel, _CurrentLevel);
                    _NewLevel = _CurrentLevel;
                }
            }
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_BeginDrog)
            {
                return;
            }
            if (pictureBox4.Top + Control.MousePosition.Y - _DrogY + pictureBox4.Height / 2 < pictureBox2.Top)
            {
                return;
            }
            if (pictureBox4.Top + Control.MousePosition.Y - _DrogY + pictureBox4.Height / 2 > pictureBox2.Bottom)
            {
                return;
            }
            if (Math.Abs(_DrogY - Control.MousePosition.Y) < _StepLength)
            {
                return;
            }
            if (_DrogY - Control.MousePosition.Y > 0)
            {
                _CurrentLevel++;
            }
            else
            {
                _CurrentLevel--;
            }
            int len = Control.MousePosition.Y - _DrogY;
            int len1 = len / _StepLength * _StepLength;
            pictureBox4.Top += len1;
            _DrogY = Control.MousePosition.Y+(len-len1);
            pictureBox5.Top = pictureBox4.Top + pictureBox4.Height / 2;
            pictureBox5.Height = pictureBox2.Bottom - pictureBox5.Top;
            drawPoint(pictureBox2, Resources.bar4);
            drawPoint(pictureBox5, Resources.bar2);
        }

        private void picZoomOut_Click_1(object sender, EventArgs e)
        {
            if (CurrentLevel == _LevelCount)
            {
                return;
            }
            _CurrentLevel++;
            pictureBox4.Top -= _StepLength;
            pictureBox5.Top = pictureBox4.Top + pictureBox4.Height / 2;
            pictureBox5.Height = pictureBox2.Bottom - pictureBox5.Top;
            drawPoint(pictureBox2, Resources.bar4);
            drawPoint(pictureBox5, Resources.bar2);

            if (ZoomChange != null)
            {
                ZoomChange(_NewLevel, _CurrentLevel);
                _NewLevel = _CurrentLevel;
            }
        }

        private void picZoomIn_Click_1(object sender, EventArgs e)
        {
            if (CurrentLevel == 0)
            {
                return;
            }
            _CurrentLevel--;
            pictureBox4.Top += _StepLength;
            pictureBox5.Top = pictureBox4.Top + pictureBox4.Height / 2;
            pictureBox5.Height = pictureBox2.Bottom - pictureBox5.Top;
            drawPoint(pictureBox2, Resources.bar4);
            drawPoint(pictureBox5, Resources.bar2);

            if (ZoomChange != null)
            {
                ZoomChange(_NewLevel, _CurrentLevel);
                _NewLevel = _CurrentLevel;
            }
        }

        private void drawPoint(PictureBox pic, Image img)
        {
            if (pic.Width <= 0 || pic.Height <= 0)
            {
                return;
            }
            int pos = 0;
            if (pic == pictureBox2)
            {
                pos = 5;
            }
            else if (pic == pictureBox5)
            {
                pos = 3;
            }
            Image image = img.Clone() as Image;
            Bitmap map = new Bitmap(pic.Width, pic.Height);
            Graphics g = Graphics.FromImage(map);
            g.DrawImage(image, new Rectangle(0, 0, map.Width, map.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            int y = pic.Bottom-_StepLength-pic.Top;
            while (y > 0)
            {
                g.FillPie(Brushes.Red, new Rectangle(pic.Width/2-pos, y - 2, 4, 4), 0, 360);
                y -= _StepLength;
            }
            g.Dispose();
            pic.Image = map;
        }
    
    }
}
