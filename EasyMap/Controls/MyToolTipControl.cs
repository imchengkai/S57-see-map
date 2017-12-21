using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Geometries;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using Report;

namespace EasyMap.Controls
{
    public partial class MyToolTipControl : UserControl
    {
        public delegate void SelectObjectConfirmEvent(bool confirm);
        public event SelectObjectConfirmEvent SelectConfirm;

        private string _Caption = "";
        private string _Message = "";
        private decimal _MapId;
        private decimal _LayerId;
        private decimal _ObjectId;
        private string _ObjectName;
        private List<Image> _Images = new List<Image>();
        private bool _SelectObjectConfirm = false;
        private Geometry _Geom = null;

        public Geometry Geom
        {
            get { return _Geom; }
            set { _Geom = value; }
        }
        public string ObjectName
        {
            get{return _ObjectName;}
            set { _ObjectName = value; }
        }

        public bool SelectObjectConfirm
        {
            get { return _SelectObjectConfirm; }
            set { _SelectObjectConfirm = value; }
        }

        public decimal ObjectId
        {
            get { return _ObjectId; }
            set { _ObjectId = value; }
        }

        public decimal LayerId
        {
            get { return _LayerId; }
            set { _LayerId = value; }
        }

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

        public delegate void ImageClickEvent(Image sender);

        public event ImageClickEvent ImageClick;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; label2.Text = value; }
        }

        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; label1.Text = value; }
        }

        public MyToolTipControl()
        {
            InitializeComponent();
            label1.Parent = this;
            label2.Parent = this;
            panel1.Parent = this;
            Hide();
        }

        public void Initial(decimal mapid, decimal layerid, Geometry geom,string objname, int x, int y,int num)
        {
            this.Visible = true;
            MapId = mapid;
            LayerId = layerid;
            ObjectId = geom.ID;
            _Geom = geom;
            ObjectName = objname;
            GetObjectInformation();
            this.Height = MeasureHeight(num);
            this.Left = x - this.Width / 2;
            this.Top = y - this.Height-10;
            if (this.Top < 0)
            {
                this.Top = y + 10;
            }
            if (this.Left < 0)
            {
                this.Left = x;
            }
            if (this.Right > this.Parent.Width)
            {
                this.Left = x - this.Width;
            }
        }

        public void Show()
        {
            if (Caption == "" && Message == "" && !tabControl1.Visible)
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
        }

        public void Hide()
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
        }

        private void GetObjectInformation()
        {
            DataTable table = MapDBClass.GetObjectById(MapId, LayerId, ObjectId);
            if (table != null && table.Rows.Count > 0)
            {
                byte[] data = (byte[])table.Rows[0]["ObjectData"];
                Geometry geometry = (Geometry)Common.DeserializeObject(data);
                Caption = ObjectName;// geometry.Text;
                if (Caption == null)
                {
                    Caption = "";
                }
                Message = geometry.Message;
                if (Message == null)
                {
                    Message = "";
                }
                tabControl1.TabPages.Clear();
                List<Image> images = MapDBClass.GetPicures(MapId, LayerId, ObjectId);
                _Images = images;
                for (int i = 0; i < images.Count; i++)
                {
                    int index=i+1;
                    tabControl1.TabPages.Add(index.ToString(), index.ToString());
                    PictureBox pic = new PictureBox();
                    pic.Image = images[i];
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Click += new EventHandler(pic_Click);
                    pic.Cursor = Cursors.Hand;
                    tabControl1.TabPages[i].Controls.Add(pic);
                    pic.Dock = DockStyle.Fill;
                }
                if (images == null || images.Count <= 0)
                {
                    tabControl1.Visible = false;
                }
                else
                {
                    tabControl1.Visible = true;
                    //tabControl1.Location = new System.Drawing.Point(10, this.Height-100);
                }
            }
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("MapId", MapId));
                param.Add(new SqlParameter("LayerId", LayerId));
                param.Add(new SqlParameter("ObjectId", ObjectId));
                table = SqlHelper.Select(SqlHelper.GetSql("SelectProjectByObject"), param);
                btnProjectArea.Visible = table != null && table.Rows.Count > 0;
                btnProjectAreaPrice.Visible = btnProjectArea.Visible;
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
        }

        void pic_Click(object sender, EventArgs e)
        {

            if (ImageClick != null)
            {
                PictureBox pic = sender as PictureBox;
                ImageClick(pic.Image);
            }
            else
            {
                PictureViewForm form = new PictureViewForm();
                form.Images = _Images;
                form.ImageIndex = tabControl1.SelectedIndex;
                form.ShowDialog();
            }
        }

        private void MyToolTipControl_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rec=new Rectangle(0,0,Width,Height);
            e.Graphics.FillRectangle(new LinearGradientBrush(rec, Color.FromArgb(250, 248, 245), Color.FromArgb(255, 182, 108), 90), rec);
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(204, 153, 51), 2), rec);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            int y = label2.Location.X;
            Font font = new Font("", 9);
            e.Graphics.DrawString(Message, font, Brushes.Black, label2.Left, label2.Top, format);
        }

        private int MeasureHeight(int num)
        {
            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            int y = label2.Location.X;
            Font font = new Font("", 9);
            int start = 0;
            int len = 1;
            if (Message!=null&&Message.Length > 0)
            {
                string[] lines = Message.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    SizeF size = g.MeasureString(lines[i], font);
                    if ((int)size.Width > label2.Width)
                    {
                        while (start + len < lines[i].Length)
                        {
                            size = g.MeasureString(lines[i].Substring(start,len), font);
                            if ((int)size.Width > label2.Width)
                            {
                                lines[i] = lines[i].Substring(0, start + len) + "\r\n" + lines[i].Substring(start + len);
                                start += len + 2;
                                len = 0;
                            }
                            len++;
                        }
                    }
                }
                Message = "";
                foreach (string line in lines)
                {
                    Message += line.Trim() + "\r\n";
                }
                SizeF size1 = g.MeasureString(Message, font);
                y += (int)size1.Height;
            }
            btnCancel.Visible = SelectObjectConfirm;
            btnOk.Visible = SelectObjectConfirm;
            y += 10 + num*42;
            tabControl1.Top = y;
            if (tabControl1.Visible)
            {
                y= tabControl1.Bottom + 10;
            }
            panel1.Top = y;
            panel1.Left = 0;
            panel1.Width = this.Width;
            if (SelectObjectConfirm || btnProjectArea.Visible)
            {
                y += panel1.Height + 10;
            }
            return y;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SelectConfirm != null)
            {
                SelectConfirm(true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (SelectConfirm != null)
            {
                SelectConfirm(false);
            }
        }

        private void btnProjectArea_Click(object sender, EventArgs e)
        {
            ProjectAreaForm form = new ProjectAreaForm(MapId, LayerId, ObjectId,Caption);
            form.ShowDialog();
        }

        private void btnProjectAreaPrice_Click(object sender, EventArgs e)
        {

            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("MapId", MapId));
                param.Add(new SqlParameter("LayerId", LayerId));
                param.Add(new SqlParameter("ObjectId", ObjectId));
                DataTable table = SqlHelper.Select(SqlHelper.GetSql("SelectMaxMinProjectYear"), param);
                int startyear = DateTime.Now.Year;
                int endyear = DateTime.Now.Year;
                if (table != null && table.Rows.Count > 0)
                {
                    int.TryParse(table.Rows[0]["MinYear"].ToString(), out startyear);
                    int.TryParse(table.Rows[0]["MaxYear"].ToString(), out endyear);
                }
                ProjectAreaPriceReportConditionForm form = new ProjectAreaPriceReportConditionForm();
                form.StartYear = startyear;
                form.EndYear = endyear;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string sql = SqlHelper.GetSql("SelectProjectAreaPrice");
                    sql = sql.Replace("@StartYear", form.StartYear.ToString());
                    sql = sql.Replace("@EndYear", form.EndYear.ToString());
                    sql = sql.Replace("@MapId", MapId.ToString());
                    sql = sql.Replace("@LayerId", LayerId.ToString());
                    sql = sql.Replace("@ObjectId", ObjectId.ToString());
                    ReportForm report = new ReportForm();
                    report.SQL = sql;
                    report.XAxisColumnName = "估价基准日";
                    report.Title = "宗地地价走势图";
                    report.GraphType = ReportForm.ReportGraphType.Line;
                    report.Show();
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
