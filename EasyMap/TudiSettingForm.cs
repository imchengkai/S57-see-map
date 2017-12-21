using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using EasyMap.Geometries;

namespace EasyMap
{
    public partial class TudiSettingForm : MyForm
    {
        private List<PriceColorData> _PriceColorList = new List<PriceColorData>();
        private Map _Map;
        private Color _Color1 = Color.Empty;
        private Color _Color2 = Color.Empty;
        private bool _IsMove = false;
        internal List<PriceColorData> PriceColorList
        {
            get { return _PriceColorList; }
            set { _PriceColorList = value; }
        }

        public TudiSettingForm(Map Map)
        {
            InitializeComponent();
            colorDialog1.Color = Color.White;
            colorDialog2.Color = Color.Red;
            _Color1 = colorDialog1.Color;
            _Color2 = colorDialog2.Color;
            _Map = Map;
            PriceColorSettingForm_Load(null, null);
            DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            //for (int i = 0; i < panel2.Controls.Count; i++)
            //{
            //    TudiSettingControl obj = panel2.Controls[i] as TudiSettingControl; 
            //    for (int j = i+1; j < panel2.Controls.Count; j++)
            //    {
            //        TudiSettingControl obj1 = panel2.Controls[j] as TudiSettingControl;
            //        if (obj.TaxName == obj1.TaxName)
            //        {
            //            MessageBox.Show("错误", "税率名称重复。");
            //            return;
            //        }
            //    }
            //}
            //DialogResult = DialogResult.OK;
            //PriceColorList.Clear();
            //SqlConnection conn = null;
            //SqlTransaction tran = null;
            //try
            //{
            //    conn = SqlHelper.GetConnection();
            //    conn.Open();
            //    tran = conn.BeginTransaction();
            //    string sql = "delete t_tax_level where MapId='60'";
            //    SqlHelper.Delete(conn, tran, sql, null);
            //    //sql = "insert into t_tax_level values(@MapId,@Id,@tax_rate,@Color)";
            //    sql = "update @table set  "
            //    List<SqlParameter> param = new List<SqlParameter>();
            //    for (int i = 0; i < panel2.Controls.Count; i++)
            //    {
            //        param.Clear();
            //        TudiSettingControl obj = panel2.Controls[i] as TudiSettingControl;
            //        PriceColorData data = new PriceColorData();
            //        data.Alhpa = obj.Alhpa;
            //        data.FillColor = obj.FillColor;
            //        //data.MinPrice = obj.MinPrice;
            //        PriceColorList.Add(data);
            //        param.Add(new SqlParameter("MapId", 60));
            //        param.Add(new SqlParameter("Id", obj.TaxName));
            //        //param.Add(new SqlParameter("tax_rate", obj.MinPrice.ToString()));
            //        param.Add(new SqlParameter("Color", obj.FillColor.ToArgb()));
            //        SqlHelper.Insert(conn,tran,sql, param);
            //    }
            //    tran.Commit();
            //    conn.Close();
            //    Close();
            //}
            //catch(Exception ex)
            //{
            //    if (conn != null)
            //    {
            //        if (tran != null)
            //        {
            //            tran.Rollback();
            //        }
            //        conn.Close();
            //    }
            //}
            //LayerStyleForm form = new LayerStyleForm();
            //if (MainMapImage.SelectObjects.Count == 1)
            //{
            //    form.FillBrush = new SolidBrush(Color.FromArgb(MainMapImage.SelectObjects[0].Fill));
            //    form.OutLinePen = new Pen(Color.FromArgb(MainMapImage.SelectObjects[0].Outline), MainMapImage.SelectObjects[0].OutlineWidth);
            //    form.TextColor = MainMapImage.SelectObjects[0].TextColor;
            //    form.TextFont = MainMapImage.SelectObjects[0].TextFont;
            //    form.EnableOutline = MainMapImage.SelectObjects[0].EnableOutline;
            //    form.HatchStyle = MainMapImage.SelectObjects[0].HatchStyle;
            //    Pen linepen = new Pen(Color.FromArgb(MainMapImage.SelectObjects[0].Line));
            //    linepen.DashStyle = (DashStyle)MainMapImage.SelectObjects[0].DashStyle;
            //    form.LinePen = linepen;
            //}
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    foreach (Geometry geom in MainMapImage.SelectObjects)
            //    {
            //        geom.Fill = form.FillBrush.Color.ToArgb();
            //        geom.Outline = form.OutLinePen.Color.ToArgb();
            //        geom.OutlineWidth = (int)form.OutLinePen.Width;
            //        geom.TextColor = form.TextColor;
            //        geom.TextFont = form.TextFont;
            //        geom.EnableOutline = form.EnableOutline;
            //        geom.HatchStyle = form.HatchStyle;
            //        geom.Line = form.LinePen.Color.ToArgb();
            //        geom.DashStyle = (int)form.LinePen.DashStyle;
            //        geom.StyleType = 1;
            //        geom.Penstyle = form.Penstyle;
            //        MapDBClass.UpdateObject(MainMapImage.Map.MapId, geom);
            //    }
            //    MainMapImage.Refresh();
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PriceColorSettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string tableName = "t_" + _Map.MapId + "_" + _Map.Layers["土地宗地图层"].ID;
                string sql = "SELECT distinct flag FROM " + tableName;
                DataTable table = SqlHelper.Select(sql, null);
                if (table != null && table.Rows.Count > 0)
                {
                    this.txt1.Text = table.Rows[1]["flag"].ToString().Trim();
                    this.txt2.Text = table.Rows[2]["flag"].ToString().Trim();
                    this.txt3.Text = table.Rows[3]["flag"].ToString().Trim();
                    this.txt4.Text = table.Rows[4]["flag"].ToString().Trim();
                    //int num = 0;
                    //int.TryParse(table.Rows[i][2].ToString(), out num);
                    //Color color = Color.FromArgb(num);
                    //obj.Alhpa = color.A;
                    //obj.FillColor = color;
                    //obj.MinPrice = (decimal)table.Rows[i][1];
                    //obj.TaxName = table.Rows[i][0].ToString();
                    //PriceColorData data = new PriceColorData();
                    //data.Alhpa = obj.Alhpa;
                    //data.FillColor = obj.FillColor;
                    //data.MinPrice = obj.MinPrice;
                    //PriceColorList.Add(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.set(txt1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        public void set(string txt)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                string tableName = "t_" + _Map.MapId + "_" + _Map.Layers["土地宗地图层"].ID;
                string sql = "select t1.ObjectData from t_object t1 left join "+tableName+" t2 on t1.ObjectId = t2.ObjectId and t1.LayerId = t2.LayerId and t1.MapId = t2.MapId where t2.flag =@flag";
                //param.Add(new SqlParameter("table1", tableName));
                param.Add(new SqlParameter("flag", txt));
                DataTable table = SqlHelper.Select(sql, param);
                LayerStyleForm form = new LayerStyleForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        byte[] data = (byte[])row["ObjectData"];
                        Geometry geom = (Geometry)Common.DeserializeObject(data);
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
                        MapDBClass.UpdateObject(_Map.MapId, geom);
                    }
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
