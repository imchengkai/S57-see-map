using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using EasyMap.Forms;
using EasyMap.Layers;
using EasyMap.Geometries;
using System.Collections.ObjectModel;
using EasyMap.Data.Providers;
using EasyMap.Controls;
using System.Collections;

namespace EasyMap
{
    public partial class ProjectAddForm : MyForm
    {
        private List<int[]> fields = new List<int[]>();
        private Hashtable hash = new Hashtable();
        private List<Color> colors = new List<Color>();
        private bool _SaveSuccess = true;
        private string _OldArea = "";
        private int _OldYear = 0;
        public delegate void ProjectChangeEvent(ProjectData ProjectData);
        public delegate void SelectObjectFromMapEvent();
        public delegate bool SetPositionEvent(decimal layerId, decimal geomId);
        public event ProjectChangeEvent ProjectChange;
        public event SelectObjectFromMapEvent SelectObjectFromMap;
        public event SetPositionEvent SetPosition;
        //项目对象
        private ProjectData _ProjectData;

        private const string _IniFile = "ClientSetting.ini";
        private const string _Section = "ProjectArea";
        private const string _SearchKey = "AreaText";
        //项目ID
        private decimal _ProjectId = 0;
        private Map _Map;
        private List<decimal> _DeleteLayer = new List<decimal>();
        private List<decimal> _DeleteObject = new List<decimal>();

        public Map Map
        {
            get { return _Map; }
            set { _Map = value; }
        }
        public decimal ProjectId
        {
            get { return _ProjectId; }
            set { _ProjectId = value; }
        }

        //窗口是编辑还是新建标志
        private bool _IsModify = false;

        public bool IsModify
        {
            get { return _IsModify; }
            set 
            { 
                _IsModify = value;
                //txtQuYu.Enabled = !value;
                //txtYear.Enabled = !value;
                if (value)
                {
                    this.Text = "项目编辑";
                }
                else
                {
                    this.Text = "新建项目";
                }
            }
        }

        public ProjectData ProjectData
        {
            get { return _ProjectData; }
            set 
            { 
                _ProjectData = value;
                if (value != null)
                {
                    IsModify = true;
                    //this.LoadDefaultValues = false;
                }
            }
        }

        //登记用途
        private List<string> item1=new List<string>();
        //现状用途
        private List<string> item2=new List<string>();
        //设定用途
        private List<string> item3 = new List<string>();
        //设定用途
        private List<string> item4 = new List<string>();
        //设定用途
        private List<string> item5 = new List<string>();
        //设定用途
        private List<string> item6 = new List<string>();
        //设定用途
        private List<string> item7 = new List<string>();

        public ProjectAddForm()
        {
            InitializeComponent();
            fields.Add(new int[] { 5, 6, 7, 8, 10, 11, 12, 13, 14, 24, 25, 26, 27, 28, 29 });
            hash.Add("工矿仓储用地", fields.Count - 1);
            colors.Add(LoadColor("工矿仓储用地", Color.FromArgb(255, 66, 66)));
            fields.Add(new int[] { 5, 7, 8, 10, 11, 12, 13, 14, 24, 25, 26, 28, 29 });
            hash.Add("商服用地", fields.Count - 1);
            colors.Add(LoadColor("商服用地", Color.FromArgb(255, 91, 91)));
            fields.Add(new int[] { 5, 7, 8, 10, 11, 12, 13, 14, 24, 25, 26, 28, 29 });
            hash.Add("住宅用地", fields.Count - 1);
            colors.Add(LoadColor("住宅用地", Color.FromArgb(255, 247, 89)));
            fields.Add(new int[] { 5, 7, 8, 10, 11, 12, 13, 14, 24, 25, 26, 28, 29 });
            hash.Add("公共管理与公共服务用地", fields.Count - 1);
            colors.Add(LoadColor("公共管理与公共服务用地", Color.FromArgb(252, 99, 61)));
            fields.Add(new int[] { 5, 7, 8, 10, 11, 12, 13, 14, 24, 25, 26, 28, 29 });
            hash.Add("交通运输用地", fields.Count - 1);
            colors.Add(LoadColor("交通运输用地", Color.FromArgb(192, 192, 192)));
            fields.Add(new int[] { 5, 7, 8, 10, 11, 12, 13, 14, 24, 25, 26, 28, 29 });
            hash.Add("其他需改造的低效", fields.Count - 1);
            colors.Add(LoadColor("其他需改造的低效", Color.FromArgb(210, 255, 74)));
            fields.Add(new int[] { 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 25, 26 });
            hash.Add("规划确定改造的城镇、棚户区、城中村、城边村等用地", fields.Count - 1);
            colors.Add(LoadColor("规划确定改造的城镇、棚户区、城中村、城边村等用地", Color.FromArgb(176, 176, 0)));
            fields.Add(new int[] { 5, 7, 8, 10, 11,12, 15, 16,17 });
            hash.Add("不符合规划地块（面积小于整体面积的10%）", fields.Count - 1);
            colors.Add(LoadColor("不符合规划地块（面积小于整体面积的10%）", Color.FromArgb(145, 145, 181)));
            areaList.colors = colors;
            areaList.hash = hash;
            areaList.fields = fields;
            areaList._Column = "低效地类型";
            //this.LoadDefaultValues = true;
            SetColumnItems("低效地类型", 1, item1);
            SetColumnItems("低效地子类型", 2, item2);
            SetColumnItems("区域名称", 3, item3);
            SetColumnItems("区域位置", 4, item4);
            SetColumnItems("是否有抵押", 5, item5);
            SetColumnItems("是否有司法强制措施", 6, item6);
            SetColumnItems("拟采取的改造方式", 7, item7);
            
            LoadAreaItems();
            DialogResult = DialogResult.Cancel;
            txtYear.Value = DateTime.Now.Year;
            areaList.hash = hash;
            areaList.fields = fields;
            areaList.colors = colors;
            areaList._SpecialColumn = 5;
            
        }

        public void Initial()
        {
            if (IsModify && ProjectData != null)
            {
                string sql = SqlHelper.GetSql("SelectProjectById");
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("area", ProjectData.Area));
                param.Add(new SqlParameter("date", ProjectData.Date));
                param.Add(new SqlParameter("MapId", _Map.MapId));
                DataTable table = SqlHelper.Select(sql, param);
                if (table == null || table.Rows.Count <= 0)
                {
                    return;
                }
                int year = DateTime.Now.Year;
                if (!int.TryParse(table.Rows[0]["date"].ToString(), out year))
                {
                    year = DateTime.Now.Year;
                }
                txtYear.Value = year;
                txtQuYu.Text = table.Rows[0]["area"].ToString();

                areaList.Rows.Clear();
                if (table == null || table.Rows.Count <= 0)
                {
                    return;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    int row = areaList.Rows.Add();
                    for (int j = 0; j < areaList.ColumnCount; j++)
                    {
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            if (areaList.Columns[j].Name == table.Columns[k].ColumnName)
                            {
                                areaList.Rows[row].Cells[j].Value = table.Rows[i][k];
                                break;
                            }
                        }
                        areaList.Rows[row].Cells["OldLayerId"].Value = table.Rows[i]["LayerId"];
                        areaList.Rows[row].Cells["OldObjectId"].Value = table.Rows[i]["ObjectId"];
                        areaList.Rows[row].Cells["modify"].Value = "";
                    }
                }
                for (int i = 0; i < areaList.RowCount; i++)
                {
                    decimal layerid = (decimal)areaList.GetGridValue(i, "LayerId");
                    decimal objid = (decimal)areaList.GetGridValue(i, "ObjectId");
                    foreach (ILayer layer in Map.Layers)
                    {
                        if (layer.ID == layerid && layer is VectorLayer)
                        {
                            GeometryProvider provider = (GeometryProvider)((VectorLayer)layer).DataSource;
                            foreach (Geometry geom in provider.Geometries)
                            {
                                if (geom.ID == objid)
                                {
                                    areaList.Rows[i].Cells["areaname"].Tag = geom;
                                    areaList.Rows[i].Cells["areaname"].Value = geom.Text;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            for (int row = 0; row < areaList.RowCount; row++)
            {
                string data = areaList.GetGridValue(row, "低效地类型").ToString();
                //areaList_CellValueInputed(areaList.Rows[row].Cells["低效地类型"], data);

                if (hash.ContainsKey(data))
                {
                    int index = (int)hash[data];
                    if (fields.Count > index && index >= 0)
                    {
                        for (int i = 0; i < areaList.ColumnCount; i++)
                        {
                            areaList.Rows[row].Cells[i].ReadOnly = true;
                        }
                        for (int i = 0; i < fields[index].Length; i++)
                        {
                            areaList.Rows[row].Cells[fields[index][i]].ReadOnly = false;
                        }
                    }
                }
            }
            _OldArea = txtQuYu.Text.Trim();
            _OldYear = (int)txtYear.Value;
            IsModify = false;
            areaList.Focus();
        }

        private Color LoadColor(string key,Color defaultColor)
        {
            string scolor = Common.IniReadValue("ClientSetting.ini", "ProjectColor", key);
            if (string.IsNullOrEmpty(scolor))
            {
                return defaultColor;
            }
            int icolor = 0;
            if (int.TryParse(scolor, out icolor))
            {
                return Color.FromArgb(icolor);
            }
            return defaultColor;
        }

        private void SaveColor(string key, Color color)
        {
            Common.IniWriteValue("ClientSetting.ini", "ProjectColor", key, color.ToArgb().ToString());
        }

        private void SetColumnItems(string columnname, int id,List<string> items)
        {
            DataGridViewComboBoxColumn col = areaList.Columns[columnname] as DataGridViewComboBoxColumn;
            items = GetValue(id);
            foreach (string item in items)
            {
                col.Items.Add(item);
            }
        }

        /// <summary>
        /// 保存当前划分区域的内容到历史记录中
        /// </summary>
        private void SaveAreaItems()
        {
            int index = 1;
            string key = _SearchKey + index;
            string txt = Common.IniReadValue(_IniFile, _Section, key);
            while (txt != "")
            {
                if (txt == txtQuYu.Text.ToLower().Trim())
                {
                    return;
                }
                index++;
                key = _SearchKey + index;
                txt = Common.IniReadValue(_IniFile, _Section, key);
            }
            Common.IniWriteValue(_IniFile, _Section, key, txtQuYu.Text.ToLower().Trim());
        }

        /// <summary>
        /// 将历史划分区域记录加载到划分区域输入框中
        /// </summary>
        private void LoadAreaItems()
        {
            int index = 1;
            string key = _SearchKey + index;
            string txt = Common.IniReadValue(_IniFile, _Section, key);
            while (txt != "")
            {
                txtQuYu.Items.Add(txt);
                index++;
                key = _SearchKey + index;
                txt = Common.IniReadValue(_IniFile, _Section, key);
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 保存数据，关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            _SaveSuccess = true;
            if (!CheckInput())
            {
                _SaveSuccess = false;
                return;
            }
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                DeleteProjectArea(conn, tran);
                //追加新数据
                if (!IsModify)
                {
                    SaveProject(conn, tran);
                }
                SaveProjectArea(conn, tran);
                tran.Commit();
                conn.Close();
                tran = null;
                conn = null;
                //设置返回的项目对象数据
                ProjectData data = new ProjectData();
                data.Date = txtYear.Value.ToString();
                data.Year = txtYear.Value.ToString();
                data.Area = txtQuYu.Text;
                List<ProjectAreaData> list = new List<ProjectAreaData>();
                for (int i = 0; i < areaList.RowCount; i++)
                {
                    ProjectAreaData area = new ProjectAreaData();
                    area.AreaName = areaList.GetGridValue(i, "areaname").ToString();
                    area.AreaObject = areaList.Rows[i].Cells["areaname"].Tag as Geometry;
                    area.AreaObject.Select = false;
                    list.Add(area);
                }
                data.AreaList = list;
                //设置处理成功标志
                DialogResult = DialogResult.OK;
                if (ProjectChange != null)
                {
                    ProjectChange(data);
                }
                SaveAreaItems();
                _DeleteLayer.Clear();
                _DeleteObject.Clear();
                for (int i = 0; i < areaList.RowCount; i++)
                {
                    areaList.Rows[i].Cells["modify"].Value = "";
                }
            }
            catch(Exception ex)
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
                _SaveSuccess = false;
            }
        }

        /// <summary>
        /// 添加宗地信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            areaList.ClearSelection();
            int row=areaList.Rows.Add();
            areaList.Rows[row].Cells["MapId"].Value = Map.MapId;
            areaList.Rows[row].Cells["低效地类型"].Selected = true;
            areaList.Rows[row].Cells["居民户数"].Value=0;
            areaList.Rows[row].Cells["区域总面积"].Value=0;
            areaList.Rows[row].Cells["国有土地"].Value=0;
            areaList.Rows[row].Cells["集体土地"].Value=0;
            areaList.Rows[row].Cells["农用地"].Value=0;
            areaList.Rows[row].Cells["建设用地"].Value=0;
            areaList.Rows[row].Cells["未利用地"].Value=0;
            areaList.Rows[row].Cells["住宅用地"].Value=0;
            areaList.Rows[row].Cells["商服用地"].Value=0;
            areaList.Rows[row].Cells["工矿仓储用地"].Value=0;
            areaList.Rows[row].Cells["公共管理与公共服务用地"].Value=0;
            areaList.Rows[row].Cells["交通运输用地"].Value=0;
            areaList.Rows[row].Cells["其他用地"].Value=0;
            areaList.Rows[row].Cells["土地使用权人"].Value=0;
            areaList.Rows[row].Cells["容积率"].Value=0;
            areaList.Rows[row].Cells["建筑密度"].Value=0;
            areaList.Rows[row].Cells["土地产出率"].Value = 0;
            areaList.CurrentCell = areaList.Rows[row].Cells["低效地类型"];
            areaList.Rows[row].Cells["modify"].Value = "0";
            SetControlStatus();
        }

        /// <summary>
        /// 删除宗地信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (areaList.CurrentCell == null || areaList.RowCount <= areaList.CurrentCell.RowIndex || areaList.CurrentCell.RowIndex < 0)
            {
                return;
            }
            if (MessageBox.Show("确定要删除选择的宗地设置吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            if (areaList.Rows[areaList.CurrentCell.RowIndex].Cells["LayerId"].Value != null)
            {
                decimal layerid = decimal.Parse(areaList.Rows[areaList.CurrentCell.RowIndex].Cells["LayerId"].Value.ToString());
                decimal objectid = decimal.Parse(areaList.Rows[areaList.CurrentCell.RowIndex].Cells["ObjectId"].Value.ToString());

                _DeleteLayer.Add(layerid);
                _DeleteObject.Add(objectid);
            }
            areaList.Rows.RemoveAt(areaList.CurrentCell.RowIndex);
            //List<int> rows = new List<int>();
            //for(int i=areaList.Rows.Count-1;i>=0;i--)
            //{
            //    if(areaList.IsSelected(i))
            //    {
            //        rows.Add(i);
            //    }
            //}
            //for (int i = rows.Count - 1; i >= 0; i--)
            //{
            //    areaList.Rows.RemoveAt(rows[i]);
            //}
            SetControlStatus();
        }

        /// <summary>
        /// 设置窗口空间状态
        /// </summary>
        private void SetControlStatus()
        {
            btnDetail.Enabled = false;
            btnDelete.Enabled = areaList.CurrentCell != null;
            btnPickFromMap.Enabled = areaList.RowCount > 0;
            if (areaList.CurrentCell!=null)
            {
                string layerid = areaList.GetGridValue(areaList.CurrentCell.RowIndex, "LayerId").ToString();
                string objid = areaList.GetGridValue(areaList.CurrentCell.RowIndex, "ObjectId").ToString();
                if (layerid != "" && objid != "")
                {
                    btnDetail.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 检查输入合法性
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (txtQuYu.Text.Trim() == "")
            {
                MessageBox.Show("请输入区域名称。");
                txtQuYu.Focus();
                return false;
            }
            for (int i = 0; i < areaList.RowCount; i++)
            {
                string areaname = areaList.GetGridValue(i, "areaname").ToString();
                if (areaname == "")
                {
                    areaList.ClearSelection();
                    areaList.Rows[i].Selected = true;
                    MessageBox.Show("第"+(i+1)+"行宗地信息没有设置。");
                    return false;
                }
                string layerid=areaList.GetGridValue(i,"LayerId").ToString();
                string objectid=areaList.GetGridValue(i,"ObjectId").ToString();
                string sql = "select count(1) from t_dixiaoarea_detail where mapid=" + Map.MapId.ToString() + " and area='" + txtQuYu.Text.Trim() + "' and date='" + txtYear.Value.ToString() + "' and layerid=" + layerid + " and objectid=" + objectid;
                DataTable table = SqlHelper.Select(sql, null);
                if (table != null && table.Rows.Count > 0)
                {
                    string scount = table.Rows[0][0].ToString();
                    int count = 0;
                    if (int.TryParse(scount, out count))
                    {
                        if (count > 1)
                        {
                            MessageBox.Show("第" + (i + 1) + "行宗地信息已经设置过了，不能重复设置。");
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 保存项目主表数据
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        private void SaveProject(SqlConnection conn, SqlTransaction tran)
        {
            string selectsql = "select * from t_dixiaoarea_coll where mapid=" + Map.MapId.ToString() + " and date='" + txtYear.Value.ToString() + "' and area='" + txtQuYu.Text.Trim() + "'";
            DataTable table = SqlHelper.Select(conn, tran, selectsql, null);
            if (table == null || table.Rows.Count == 0)
            {
                string sql = SqlHelper.GetSql("InsertProjectId");
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("MapId", Map.MapId));
                param.Add(new SqlParameter("date", txtYear.Value.ToString()));
                param.Add(new SqlParameter("area", txtQuYu.Text));
                SqlHelper.Insert(conn, tran, sql, param);
            }
        }

        /// <summary>
        /// 删除项目关联的宗地
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        private void DeleteProjectArea(SqlConnection conn, SqlTransaction tran)
        {
            string sql = SqlHelper.GetSql("DeleteProjectAreaById");
            List<SqlParameter> param = new List<SqlParameter>();
            for (int i = 0; i < _DeleteObject.Count; i++)
            {
                param.Clear();
                param.Add(new SqlParameter("MapId", Map.MapId));
                param.Add(new SqlParameter("LayerId", _DeleteLayer[i]));
                param.Add(new SqlParameter("ObjectId", _DeleteObject[i]));
                param.Add(new SqlParameter("date", txtYear.Value.ToString()));
                param.Add(new SqlParameter("area", txtQuYu.Text));
                SqlHelper.Delete(conn, tran, sql, param);
            }
        }

        /// <summary>
        /// 保存项目关联的宗地
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        private void SaveProjectArea(SqlConnection conn, SqlTransaction tran)
        {
            string sql = SqlHelper.GetSql("InsertProjectArea");
            string updatesql = SqlHelper.GetSql("UpdateProjectArea");
            List<SqlParameter> param = new List<SqlParameter>();
            for (int i = 0; i < areaList.RowCount; i++)
            {
                param.Clear();
                param.Add(new SqlParameter("area", txtQuYu.Text));
                param.Add(new SqlParameter("date", txtYear.Value.ToString()));
                param.Add(new SqlParameter("MapId", Map.MapId));
                param.Add(new SqlParameter("LayerId", areaList.GetGridValue(i, "LayerId")));
                param.Add(new SqlParameter("ObjectId", areaList.GetGridValue(i, "ObjectId")));
                param.Add(new SqlParameter("OldLayerId", areaList.GetGridValue(i, "OldLayerId")));
                param.Add(new SqlParameter("OldObjectId", areaList.GetGridValue(i, "OldObjectId")));
                param.Add(new SqlParameter("低效地类型", areaList.GetGridValue(i, "低效地类型")));
                param.Add(new SqlParameter("低效地子类型", areaList.GetGridValue(i, "低效地子类型")));
                param.Add(new SqlParameter("项目名称", areaList.GetGridValue(i, "项目名称")));
                param.Add(new SqlParameter("居民户数", areaList.GetGridValue(i, "居民户数")));
                param.Add(new SqlParameter("区域名称", areaList.GetGridValue(i, "区域名称")));
                param.Add(new SqlParameter("区域位置", areaList.GetGridValue(i, "区域位置")));
                param.Add(new SqlParameter("区域总面积", areaList.GetGridValue(i, "区域总面积")));
                param.Add(new SqlParameter("国有土地", areaList.GetGridValue(i, "国有土地")));
                param.Add(new SqlParameter("集体土地", areaList.GetGridValue(i, "集体土地")));
                param.Add(new SqlParameter("农用地", areaList.GetGridValue(i, "农用地")));
                param.Add(new SqlParameter("建设用地", areaList.GetGridValue(i, "建设用地")));
                param.Add(new SqlParameter("未利用地", areaList.GetGridValue(i, "未利用地")));
                param.Add(new SqlParameter("住宅用地", areaList.GetGridValue(i, "住宅用地")));
                param.Add(new SqlParameter("商服用地", areaList.GetGridValue(i, "商服用地")));
                param.Add(new SqlParameter("工矿仓储用地", areaList.GetGridValue(i, "工矿仓储用地")));
                param.Add(new SqlParameter("公共管理与公共服务用地", areaList.GetGridValue(i, "公共管理与公共服务用地")));
                param.Add(new SqlParameter("交通运输用地", areaList.GetGridValue(i, "交通运输用地")));
                param.Add(new SqlParameter("其他用地", areaList.GetGridValue(i, "其他用地")));
                param.Add(new SqlParameter("土地使用权人", areaList.GetGridValue(i, "土地使用权人")));
                param.Add(new SqlParameter("容积率", areaList.GetGridValue(i, "容积率")));
                param.Add(new SqlParameter("建筑密度", areaList.GetGridValue(i, "建筑密度")));
                param.Add(new SqlParameter("土地产出率", areaList.GetGridValue(i, "土地产出率")));
                param.Add(new SqlParameter("是否有抵押", areaList.GetGridValue(i, "是否有抵押")));
                param.Add(new SqlParameter("是否有司法强制措施", areaList.GetGridValue(i, "是否有司法强制措施")));
                param.Add(new SqlParameter("拟采取的改造方式", areaList.GetGridValue(i, "拟采取的改造方式")));
                if (areaList.GetGridValue(i, "modify").ToString() == "0")
                {
                    SqlHelper.Insert(conn, tran, sql, param);
                }
                else if (areaList.GetGridValue(i, "modify").ToString() == "1")
                {
                    SqlHelper.Update(conn, tran, updatesql, param);
                }
            }
        }

        /// <summary>
        /// 数据转换为数字
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private decimal ConvertToNumeric(object data)
        {
            decimal ret = 0;
            if (decimal.TryParse(data.ToString(), out ret))
            {
            }
            return ret;
        }

        private void InsertValue(int id, string value)
        {
            string sql = SqlHelper.GetSql("InsertValue");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("id", id));
            param.Add(new SqlParameter("value", value));
            SqlConnection conn = null; 
            SqlTransaction tran = null;
            try
            {
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                SqlHelper.Insert(conn,tran, sql, param);
                tran.Commit();
                conn.Close();
            }
            catch(Exception ex)
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

        private List<string> GetValue(int id)
        {
            List<string> items = new List<string>();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("id", id));
            DataTable table = SqlHelper.Select(SqlHelper.GetSql("SelectValue"), param);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                items.Add(table.Rows[i][0].ToString());
            }
            return items;
        }

        private void CheckComboxColumn(List<string> item, int id,string data)
        {
            if (!item.Contains(data))
            {
                item.Add(data);
                InsertValue(id, data);
            }
        }

        /// <summary>
        /// 宗地表数据输入后处理
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="data"></param>
        private void areaList_CellValueInputed(DataGridViewCell cell, string data)
        {
            if (areaList.Columns[cell.ColumnIndex].Name == "低效地类型")
            {
                CheckComboxColumn(item1, 1, data);
                if (hash.ContainsKey(data))
                {
                    int index = (int)hash[data];
                    if (fields.Count > index && index >= 0)
                    {
                        for (int i = 0; i < areaList.ColumnCount; i++)
                        {
                            areaList.Rows[cell.RowIndex].Cells[i].ReadOnly = true;
                        }
                        for (int i = 0; i < fields[index].Length; i++)
                        {
                            areaList.Rows[cell.RowIndex].Cells[fields[index][i]].ReadOnly = false;
                        }
                    }
                }
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "低效地子类型")
            {
                CheckComboxColumn(item2, 2, data);
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "区域名称")
            {
                CheckComboxColumn(item3, 3, data);
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "区域位置")
            {
                CheckComboxColumn(item4, 4, data);
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "是否有抵押")
            {
                CheckComboxColumn(item5, 5, data);
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "是否有司法强制措施")
            {
                CheckComboxColumn(item6, 6, data);
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "拟采取的改造方式")
            {
                CheckComboxColumn(item7, 7, data);
            }
            else if (areaList.Columns[cell.ColumnIndex].Name == "areaname")
            {
                if (areaList.GetGridValue(cell.RowIndex, cell.ColumnIndex).ToString().Trim() == "")
                {
                    return;
                }
                AreaTipForm form = new AreaTipForm();
                form.AreaName = areaList.GetGridValue(cell.RowIndex, cell.ColumnIndex).ToString();
                form.Map = Map;
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                if (form.AreaName == "")
                {
                    MessageBox.Show("没有该宗地编号。");
                    cell.Value = form.AreaName;
                    areaList.Rows[cell.RowIndex].Cells["LayerId"].Value = null;
                    areaList.Rows[cell.RowIndex].Cells["ObjectId"].Value = null;
                }
                else
                {
                    cell.Value = form.AreaName;
                    areaList.Rows[cell.RowIndex].Cells["LayerId"].Value = form.SelectLayer.ID;
                    areaList.Rows[cell.RowIndex].Cells["ObjectId"].Value = form.SelectGeometry.ID;
                    areaList.Rows[cell.RowIndex].Cells["areaname"].Tag = form.SelectGeometry;
                }
            }
            if (areaList.GetGridValue(cell.RowIndex, "modify").ToString() != "0")
            {
                areaList.Rows[cell.RowIndex].Cells["modify"].Value = "1";
            }
        }

        /// <summary>
        /// 从地图选择宗地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPickFromMap_Click(object sender, EventArgs e)
        {
            if (SelectObjectFromMap != null)
            {
                this.Hide();
                SelectObjectFromMap();
            }
        }

        /// <summary>
        /// 在地图上选择宗地并确认后处理
        /// </summary>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        /// <param name="objectname"></param>
        public void SelectObjectFromMapDeal(decimal layerid, Geometry geom, string objectname)
        {
            Show();
            //有效性验证
            if (layerid <= 0 || geom == null || objectname == null)
            {
                return;
            }
            //添加选择的宗地信息
            int row = areaList.RowCount - 1;
            if (areaList.CurrentCell!=null)
            {
                row = areaList.CurrentCell.RowIndex;
            }
            for (int i = 0; i < areaList.RowCount; i++)
            {
                if (areaList.Rows[i].Cells["areaname"].Tag == geom)
                {
                    MessageBox.Show("该宗地已经添加到本项目中，不能重复添加同样一块宗地。");
                    return;
                }
            }
            areaList.Rows[row].Cells["areaname"].Value = objectname;
            areaList.Rows[row].Cells["areaname"].Tag = geom;
            areaList.Rows[row].Cells["MapId"].Value = _Map.MapId;
            areaList.Rows[row].Cells["LayerId"].Value = layerid;
            areaList.Rows[row].Cells["ObjectId"].Value = geom.ID;
            if (areaList.GetGridValue(row,"modify").ToString()!="0")
            {
                areaList.Rows[row].Cells["modify"].Value = "1";
            }
            SetControlStatus();
        }

        private void areaList_SelectionChanged(object sender, EventArgs e)
        {
            SetControlStatus();
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectAddForm_Shown(object sender, EventArgs e)
        {
            SetControlStatus();
        }

        /// <summary>
        /// 元素定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (areaList.CurrentCell==null)
            {
                return;
            }
            if (SetPosition != null)
            {
                Geometry geom = areaList.Rows[areaList.CurrentCell.RowIndex].Cells["areaname"].Tag as Geometry;
                decimal layerid = (decimal)areaList.Rows[areaList.CurrentCell.RowIndex].Cells["LayerId"].Value;
                decimal geomid = (decimal)areaList.Rows[areaList.CurrentCell.RowIndex].Cells["ObjectId"].Value;
                if (!SetPosition(layerid, geomid))
                {
                    MessageBox.Show("没有查询到该宗地。");
                }
            }
        }

        private void txtQuYu_TextChanged(object sender, EventArgs e)
        {
            if (_OldArea == txtQuYu.Text.Trim() && (int)txtYear.Value == _OldYear)
            {
                return;
            }
            bool ismodify = _DeleteObject.Count>0;
            if (!ismodify)
            {
                for (int i = 0; i < areaList.RowCount; i++)
                {
                    string flag = areaList.GetGridValue(i, "modify").ToString();
                    if (flag == "0" || flag == "1")
                    {
                        ismodify = true;
                        break;
                    }
                }
            }
            if (ismodify)
            {
                DialogResult ret = MessageBox.Show("数据已经被更改，是否需要保存更改的数据？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (ret == DialogResult.Cancel)
                {
                    return;
                }
                if (ret == DialogResult.Yes)
                {
                    btnOk_Click(null, null);
                    if (!_SaveSuccess)
                    {
                        txtQuYu.Text = _OldArea;
                        txtYear.Value = _OldYear;
                        return;
                    }
                }
            }
            if (txtQuYu.Text.Trim() == "")
            {
                MessageBox.Show("请选择区域。");
                return;
            }
            _OldArea = txtQuYu.Text.Trim();
            _OldYear = (int)txtYear.Value;
            string sql = SqlHelper.GetSql("SelectProjectById");
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("area", txtQuYu.Text.Trim()));
            param.Add(new SqlParameter("date", txtYear.Value.ToString()));
            param.Add(new SqlParameter("MapId", _Map.MapId));
            DataTable table = SqlHelper.Select(sql, param);
            areaList.Rows.Clear();
            if (table == null || table.Rows.Count <= 0)
            {
                SetControlStatus();
                return;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                int row = areaList.Rows.Add();
                for (int j = 0; j < areaList.ColumnCount; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        if (areaList.Columns[j].Name == table.Columns[k].ColumnName)
                        {
                            areaList.Rows[row].Cells[j].Value = table.Rows[i][k];
                            break;
                        }
                    }
                    areaList.Rows[row].Cells["OldLayerId"].Value = table.Rows[i]["LayerId"];
                    areaList.Rows[row].Cells["OldObjectId"].Value = table.Rows[i]["ObjectId"];
                    areaList.Rows[row].Cells["modify"].Value = "";
                }
            }
            for (int i = 0; i < areaList.RowCount; i++)
            {
                decimal layerid = (decimal)areaList.GetGridValue(i, "LayerId");
                decimal objid = (decimal)areaList.GetGridValue(i, "ObjectId");
                foreach (ILayer layer in Map.Layers)
                {
                    if (layer.ID == layerid && layer is VectorLayer)
                    {
                        GeometryProvider provider = (GeometryProvider)((VectorLayer)layer).DataSource;
                        foreach (Geometry geom in provider.Geometries)
                        {
                            if (geom.ID == objid)
                            {
                                areaList.Rows[i].Cells["areaname"].Tag = geom;
                                areaList.Rows[i].Cells["areaname"].Value = geom.Text;
                                break;
                            }
                        }
                        break;
                    }
                }
            }


            for (int row = 0; row < areaList.RowCount; row++)
            {
                string data = areaList.GetGridValue(row, "低效地类型").ToString();
                //areaList_CellValueInputed(areaList.Rows[row].Cells["低效地类型"], data);

                if (hash.ContainsKey(data))
                {
                    int index = (int)hash[data];
                    if (fields.Count > index && index >= 0)
                    {
                        for (int i = 0; i < areaList.ColumnCount; i++)
                        {
                            areaList.Rows[row].Cells[i].ReadOnly = true;
                        }
                        for (int i = 0; i < fields[index].Length; i++)
                        {
                            areaList.Rows[row].Cells[fields[index][i]].ReadOnly = false;
                        }
                    }
                }
            }
            _OldArea = txtQuYu.Text.Trim();
            _OldYear = (int)txtYear.Value;
            IsModify = false;
            SetControlStatus();
        }

        private void ProjectAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            bool ismodify = _DeleteObject.Count > 0;
            if (!ismodify)
            {
                for (int i = 0; i < areaList.RowCount; i++)
                {
                    string flag = areaList.GetGridValue(i, "modify").ToString();
                    if (flag == "0" || flag == "1")
                    {
                        ismodify = true;
                        break;
                    }
                }
            }
            if (ismodify)
            {
                DialogResult ret = MessageBox.Show("数据已经被更改，是否需要保存更改的数据？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (ret == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (ret == DialogResult.Yes)
                {
                    btnOk_Click(null, null);
                    if (!_SaveSuccess)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            if (ProjectChange != null)
            {
                ProjectChange(null);
            }
        }

        private void areaList_CellPostPaint(int row, int column)
        {
            //string data = areaList.GetGridValue(row, "低效地类型").ToString();
            //if (hash.ContainsKey(data))
            //{
            //    int index = (int)hash[data];
            //    if (fields.Count > index&&index>=0&&colors.Count>index)
            //    {
            //        foreach (int col in fields[index])
            //        {
            //            if (col == column)
            //            {
            //                areaList.Rows[row].Cells[column].Style.BackColor = colors[index];
            //                break;
            //            }
            //        }
            //    }
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.BackColor = colors[comboBox1.SelectedIndex];
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                return;
            }
            colorDialog1.Color = label2.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label2.BackColor = colorDialog1.Color;
                colors[comboBox1.SelectedIndex] = colorDialog1.Color;
                SaveColor(comboBox1.Items[comboBox1.SelectedIndex].ToString(), colorDialog1.Color);
                areaList.Refresh();
            }
        }

    }
}
