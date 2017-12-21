using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EasyMap
{
    public partial class LayerStyleForm : MyForm
    {
        private SolidBrush _FillBrush = new SolidBrush(Color.Blue);
        private bool _EnableOutline = true;
        private Pen _LinePen = Pens.Black;
        private Pen _OutLinePen = Pens.Black;
        private int _HatchStyle = -1;
        private Color _TextColor = Color.Black;
        private Font _TextFont = new Font("", 9);
        public int Penstyle { get; set; }

        public Font TextFont
        {
            get { return _TextFont; }
            set { _TextFont = value; fontDialog1.Font = value; }
        }

        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; fontDialog1.Color = value; colorDialog4.Color = value; }
        }

        public int HatchStyle
        {
            get { return _HatchStyle; }
            set 
            {
                _HatchStyle = value; 
                if(value==-1)
                {
                    radioButton1.Checked=true;
                }
                else
                {
                    radioButton2.Checked=true;
                }
                radioButton1_CheckedChanged(null, null);
                listView1.SelectedItems.Clear();
                if (radioButton2.Checked)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if ((int)item.Tag == value)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        public Pen OutLinePen
        {
            get { return _OutLinePen; }
            set 
            { 
                _OutLinePen = value;
                numericUpDown1.Value = (int)value.Width;
                colorDialog1.Color = value.Color;
                Draw(null, null);
            }
        }

        public Pen LinePen
        {
            get { return _LinePen; }
            set 
            { 
                _LinePen = value;
                //numericUpDown2.Value = (int)value.Width;
                colorDialog3.Color = value.Color;
                int styleindex = (int)value.DashStyle;
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    if (listView2.Items[i].Tag.ToString() == styleindex.ToString())
                    {
                        listView2.Items[i].Selected = true;
                        pictureBox2.Image = imageList2.Images[listView2.Items[i].ImageIndex];
                        pictureBox2.Tag = styleindex;
                        break;
                    }
                }
                Draw(null, null);
            }
        }

        public bool EnableOutline
        {
            get { return _EnableOutline; }
            set 
            { 
                _EnableOutline = value;
                //checkBox1.Checked = value;
                Draw(null, null);
            }
        }

        public SolidBrush FillBrush
        {
            get { return _FillBrush; }
            set 
            { 
                _FillBrush = value; 
                SolidBrush br=value as SolidBrush;
                colorDialog2.Color = br.Color;
                numericUpDown3.Value = (255-br.Color.A)*100/255;
                Draw(null, null);
            }
        }

        public LayerStyleForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            SetView();
            Draw(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.SolidColorOnly = true;
            colorDialog1.ShowDialog();
            Draw(sender, e);
        }

        private void Draw(object sender, EventArgs e)
        {
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(map);
            //g.Clear(Color.White);
            Rectangle rect = new Rectangle(0, 0, map.Width, map.Height - (int)numericUpDown1.Value - 15);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            g.DrawString("", new Font("", 16), Brushes.Black, rect, format);
            if (radioButton1.Checked)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb((int)((100-numericUpDown3.Value) * 255 / 100), colorDialog2.Color)), new Rectangle(2, 2, rect.Width - 6, rect.Height - 6));
            }
            else
            {
                if (radioButton2.Checked && listView1.SelectedItems.Count <= 0)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb((int)((100-numericUpDown3.Value) * 255 / 100), colorDialog2.Color)), new Rectangle(2, 2, rect.Width - 6, rect.Height - 6));
                }
                else
                {
                    HatchStyle style = (HatchStyle)listView1.SelectedItems[0].Tag;
                    g.FillRectangle(new HatchBrush(style, colorDialog1.Color,Color.FromArgb((int)((100-numericUpDown3.Value)*255/100), colorDialog2.Color)), new Rectangle(2, 2, rect.Width - 6, rect.Height - 6));
                }
            }

            int width = (int)numericUpDown1.Value;
            Pen pen = new Pen(colorDialog1.Color, width);
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            //pen.DashPattern = new float[] { 3, 3 };
            int penstyle = int.Parse(pictureBox2.Tag.ToString());

            pen.DashStyle = (System.Drawing.Drawing2D.DashStyle)(penstyle>4?0:penstyle);
            g.DrawRectangle(pen, new Rectangle(2, 2, rect.Width - 6, rect.Height - 6));
            g.DrawString("文本", new Font("", 9), Brushes.Black, rect, format);
            int y = rect.Bottom + 5;
            g.DrawLine(new Pen(colorDialog3.Color, (int)numericUpDown1.Value), 0, y, map.Width, y);
            g.Dispose();
            pictureBox1.Image = map;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog2.SolidColorOnly = true;
            colorDialog2.ShowDialog();
            Draw(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            int width=(int)numericUpDown1.Value;
            _OutLinePen = new Pen(Color.FromArgb(255,colorDialog1.Color), width);
            _EnableOutline = true;
            _FillBrush = new SolidBrush(Color.FromArgb((int)((100-numericUpDown3.Value)*255/100), colorDialog2.Color));
            int penstyle = int.Parse(pictureBox2.Tag.ToString());
            _LinePen = new Pen(Color.FromArgb(255,colorDialog1.Color), width);
            LinePen.DashStyle = (DashStyle)(penstyle>4?0:penstyle);
            Penstyle = penstyle;
            _TextColor = colorDialog4.Color;
            if (TextColor == Color.Empty)
            {
                _TextColor = Color.Black;
            }
            _TextFont = fontDialog1.Font;
            if (radioButton1.Checked)
            {
                _HatchStyle = -1;
            }
            else
            {
                if (listView1.SelectedItems.Count <= 0)
                {
                    _HatchStyle = -1;
                }
                else
                {
                    _HatchStyle = (int)listView1.SelectedItems[0].Tag;
                }
            }
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog3.SolidColorOnly = true;
            colorDialog3.ShowDialog();
            Draw(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetView()
        {
            listView1.Items.Clear();
            if (radioButton1.Checked)
            {
                foreach (KnownColor kcolor in Enum.GetValues(typeof(KnownColor)))
                {
                    Color color = Color.FromKnownColor(kcolor);
                    if (!color.IsSystemColor)
                    {
                        SolidBrush brush = new SolidBrush(color);
                        Bitmap map = new Bitmap(imageList1.ImageSize.Width, imageList1.ImageSize.Height);
                        Graphics g = Graphics.FromImage(map);
                        g.FillRectangle(brush, new Rectangle(0, 0, map.Width, map.Height));
                        g.DrawRectangle(Pens.Black, new Rectangle(0, 0, map.Width-1, map.Height-1));
                        g.Dispose();
                        int id = imageList1.Images.Add(map, Color.Transparent);
                        ListViewItem item = new ListViewItem(color.Name);
                        item.ImageIndex = id;
                        item.Tag = brush;
                        listView1.Items.Add(item);
                    }
                }
            }
            else
            {
                foreach (HatchStyle style in Enum.GetValues(typeof(HatchStyle)))
                {
                    HatchBrush brush1 = new HatchBrush(style, Color.Blue, Color.Transparent);
                    int a = (int)style;
                    HatchStyle s = (HatchStyle)a;
                    Bitmap map = new Bitmap(imageList1.ImageSize.Width, imageList1.ImageSize.Height);
                    Graphics g = Graphics.FromImage(map);
                    g.FillRectangle(brush1, new Rectangle(0, 0, map.Width, map.Height));
                    g.DrawRectangle(Pens.Black, new Rectangle(0, 0, map.Width-1, map.Height-1));
                    g.Dispose();
                    int id = imageList1.Images.Add(map, Color.Transparent);
                    ListViewItem item = new ListViewItem(style.ToString());
                    item.ImageIndex = id;
                    item.Tag = style;
                    listView1.Items.Add(item);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetView();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (radioButton1.Checked)
            {
                SolidBrush brush = e.Item.Tag as SolidBrush;
                colorDialog2.Color = brush.Color;
            }
            Draw(sender,null);
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            colorDialog3.ShowDialog();
        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            //fontDialog1.ShowColor = true;
            fontDialog1.ShowDialog();
        }

        private void listView2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems != null && listView2.SelectedItems.Count > 0)
            {
                int index = listView2.SelectedItems[0].ImageIndex;
                pictureBox2.Image = imageList2.Images[index];
                pictureBox2.Tag = listView2.SelectedItems[0].Tag;
                listView2.Visible = false;
                Draw(null, null);
            }
        }

        private void listView2_Leave(object sender, EventArgs e)
        {
            listView2.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            listView2.Visible = true;
            listView2.Focus();
        }
        //字体颜色
        private void myButton2_Click_1(object sender, EventArgs e)
        {
            colorDialog4.ShowDialog();
        }
        
    }
}
