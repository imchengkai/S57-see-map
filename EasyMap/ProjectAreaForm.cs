using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace EasyMap
{
    public partial class ProjectAreaForm : MyForm
    {
        private DataTable _ProjectTable;
        private decimal _MapId;
        private decimal _LayerId;
        private decimal _ObjectId;

        private const string _SettingFileName = "Setting.ini";
        private const string _SectionDoc = "Setting";

        public ProjectAreaForm(decimal mapid,decimal layerid,decimal objectid,string areaname)
        {
            InitializeComponent();
            _MapId = mapid;
            _LayerId = layerid;
            _ObjectId = objectid;
            lblName.Text = areaname;
            LoadProject(mapid, layerid, objectid);
        }

        private void LoadProject(decimal mapid,decimal layerid,decimal objectid)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("MapId", mapid));
                param.Add(new SqlParameter("LayerId", layerid));
                param.Add(new SqlParameter("ObjectId", objectid));
                _ProjectTable = SqlHelper.Select(SqlHelper.GetSql("SelectProjectByObject"), param);
                if (_ProjectTable != null)
                {
                    for (int i = 0; i < _ProjectTable.Rows.Count; i++)
                    {
                        comProject.Items.Add(_ProjectTable.Rows[i]["项目名称"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
        }

        private void comProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyList.Rows.Clear();
            decimal projectid = 0;
            decimal.TryParse(_ProjectTable.Rows[comProject.SelectedIndex]["项目ID"].ToString(), out projectid);
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param = new List<SqlParameter>();
                param.Add(new SqlParameter("tablename", "t_project_area"));
                DataTable coltable = SqlHelper.Select(SqlHelper.GetSql("SelectColumnControl"), param);

                param = new List<SqlParameter>();
                param.Add(new SqlParameter("项目ID", projectid));
                param.Add(new SqlParameter("MapId", _MapId));
                param.Add(new SqlParameter("LayerId", _LayerId));
                param.Add(new SqlParameter("ObjectId", _ObjectId));
                DataTable table = SqlHelper.Select(SqlHelper.GetSql("SelectProjectAreaByObjectId"), param);
                if (table != null && table.Rows.Count > 0 && coltable != null)
                {
                    for (int j = 0; j < coltable.Rows.Count; j++)
                    {
                        if (coltable.Rows[j]["visible"].ToString() == "1")
                        {
                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                if (coltable.Rows[j]["columnname"].ToString() == table.Columns[i].ColumnName)
                                {
                                    int row = propertyList.Rows.Add();
                                    propertyList.Rows[row].Cells["property"].Value = coltable.Rows[j]["comment"].ToString();
                                    propertyList.Rows[row].Cells["value"].Value = table.Rows[0][i];
                                    break;
                                }
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Common.ShowError(ex);
            }
        }

        private void btnOpenProjectDocPath_Click(object sender, EventArgs e)
        {
            string path = Common.IniReadValue(_SettingFileName, _SectionDoc, "ProjectDocPath");

            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }
            int index=comProject.SelectedIndex;
            string year = _ProjectTable.Rows[index]["项目年度"].ToString();
            string projectname = _ProjectTable.Rows[index]["项目名称"].ToString();
            path += year + "\\" + projectname;
            if (!Directory.Exists(path))
            {
                MessageBox.Show("当前选择的项目的文档路径" + path + "不存在。");
                return;
            }
            Process proc = new Process();
            proc.StartInfo.FileName = path;
            proc.Start();
        }

        private void ProjectAreaForm_Load(object sender, EventArgs e)
        {
            if (comProject.Items.Count > 0)
            {
                comProject.SelectedIndex = 0;
            }
        }
    }
}
