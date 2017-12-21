using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using DragControl;

namespace EasyMap
{
    public partial class PictureViewForm : MyForm
    {
        int _x = 0;
        int _y = 0;
        int _left = 0;
        int _top = 0;
        int scale = 100;
        private List<Image> _Images = new List<Image>();
        private int _ImageIndex = 0;

        public int ImageIndex
        {
            get { return _ImageIndex; }
            set 
            {
                if (value >= Images.Count)
                {
                    return;
                }
                _ImageIndex = value; 
                ChangeImage(); 
                SetToolBarStatus();
                toolStripComboBox1.Text = (value+1).ToString();
            }
        }

        public List<Image> Images
        {
            get { return _Images; }
            set 
            { 
                _Images = value; 
                ChangeImage(); 
                SetToolBarStatus();
                toolStripComboBox1.Items.Clear();
                for (int i = 0; i < value.Count; i++)
                {
                    toolStripComboBox1.Items.Add((i + 1).ToString());
                }
                toolStripComboBox1.Text = ImageIndex.ToString();
            }
        }

        public PictureViewForm()
        {
            InitializeComponent();
            ChangeImage();
            SetToolBarStatus(); 
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (ImageIndex > 0)
            {
                ImageIndex--;
                ChangeImage();
            }
            SetToolBarStatus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ImageIndex < Images.Count)
            {
                ImageIndex++;
                ChangeImage();
            }
            SetToolBarStatus();
        }

        private void SetToolBarStatus()
        {
            btnNext.Enabled = ImageIndex < Images.Count - 1;
            btnPrev.Enabled = ImageIndex > 0;
        }

        private void ChangeImage()
        {
            if (toolStripButton3.Checked)
            {
                toolStripButton3_Click(null, null);
            }
            else if (toolStripButton4.Checked)
            {
                toolStripButton4_Click(null, null);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Cursor = Cursors.NoMove2D;
            _x = Control.MousePosition.X;
            _y = Control.MousePosition.Y;
            _left = pictureBox1.Left;
            _top = pictureBox1.Top;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox1.Left = _left - (_x - Control.MousePosition.X);
                pictureBox1.Top = _top - (_y - Control.MousePosition.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Cursor = Cursors.Default;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (Images == null || Images.Count <= ImageIndex || ImageIndex < 0)
            {
                return;
            }
            pictureBox1.Width = Images[ImageIndex].Width * (int)scale / 100;
            pictureBox1.Height = Images[ImageIndex].Height * (int)scale / 100;
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(map);
            g.DrawImage(Images[ImageIndex], new Rectangle(0, 0, map.Width, map.Height), new Rectangle(0, 0, Images[ImageIndex].Width, Images[ImageIndex].Height), GraphicsUnit.Pixel);
            g.Dispose();
            pictureBox1.Image = map;
            if (pictureBox1.Width < panel2.Width)
            {
                pictureBox1.Left = (panel2.Width - pictureBox1.Width) / 2;
            }
            else
            {
                pictureBox1.Left = 0;
            }
            if (pictureBox1.Height < panel2.Height)
            {
                pictureBox1.Top = (panel2.Height - pictureBox1.Height) / 2;
            }
            else
            {
                pictureBox1.Top = 0;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (scale > 10)
            {
                scale -= 10;
                trackBar1_Scroll(null, null);
                toolStripComboBox2.Text = scale.ToString() + "%";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (scale < 990)
            {
                scale += 10;
                trackBar1_Scroll(null, null);
                toolStripComboBox2.Text = scale.ToString() + "%";
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            toolStripButton4.Checked = true;
            toolStripButton1.Enabled = toolStripButton4.Checked;
            toolStripButton2.Enabled = toolStripButton4.Checked;
            toolStripButton3.Checked = !toolStripButton4.Checked;
            if (toolStripButton4.Checked)
            {
                if (Images == null || Images.Count <= ImageIndex || ImageIndex < 0)
                {
                    return;
                }
                panel2.Left = 0;
                panel2.Top = 0;
                pictureBox1.Left = 0;
                pictureBox1.Top = 0;
                pictureBox1.Size = panel2.Size;
                int scalex = 1;
                int scaley = 1;
                scalex = (int)(pictureBox1.Width * 100 / Images[ImageIndex].Width);
                scaley = (int)(pictureBox1.Height * 100 / Images[ImageIndex].Height);
                scale = Math.Min(scalex, scaley);
                trackBar1_Scroll(null, null);
                toolStripComboBox2.Text = scale.ToString() + "%";
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton3.Checked = true;
            toolStripButton4.Checked = !toolStripButton3.Checked;
            toolStripButton1.Enabled = toolStripButton4.Checked;
            toolStripButton2.Enabled = toolStripButton4.Checked;
            if (Images == null || Images.Count <= ImageIndex || ImageIndex < 0)
            {
                return;
            }
            pictureBox1.Width = Images[ImageIndex].Width;
            pictureBox1.Height = Images[ImageIndex].Height;
            pictureBox1.Image = Images[ImageIndex];
            if (pictureBox1.Width > panel2.Width)
            {
                pictureBox1.Left = 0;
            }
            else
            {
                pictureBox1.Left = (panel2.Width - pictureBox1.Width) / 2;
            }
            if (pictureBox1.Height > panel2.Height)
            {
                pictureBox1.Top = 0;
            }
            else
            {
                pictureBox1.Top = (panel2.Height - pictureBox1.Height) / 2;
            }
            toolStripComboBox2.Text = "100%";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Common.SaveImage(saveFileDialog1.FileName, pictureBox1.Image);
           
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string txt = toolStripComboBox2.Text.Replace("%", "");
            int scale1 = 100;
            if (Int32.TryParse(txt, out scale1))
            {
                scale = scale1;
                trackBar1_Scroll(null, null);
                if (scale != 100)
                {
                    toolStripButton4.Checked = true;
                    toolStripButton3.Checked = false;
                }
                else
                {
                    toolStripButton3.Checked = true;
                    toolStripButton4.Checked = false;
                }
            }
        }

        private void toolStripComboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string txt = toolStripComboBox2.Text.Replace("%", "");
                int scale1 = 100;
                if (Int32.TryParse(txt, out scale1))
                {
                    scale = scale1;
                    trackBar1_Scroll(null, null);
                }
            }
        }

        private void toolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int index = 0;
                if (Int32.TryParse(toolStripComboBox1.Text, out index))
                {

                    ImageIndex = index-1;
                }

            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            if (Int32.TryParse(toolStripComboBox1.Text, out index))
            {

                ImageIndex = index - 1;
            }
        }
    }
}
