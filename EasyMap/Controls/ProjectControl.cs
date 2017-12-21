using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using EasyMap.Geometries;
using System.Collections;

namespace EasyMap.Controls
{
    public partial class ProjectControl : UserControl
    {
        private const string _IniFile = "ClientSetting.ini";
        private const string _Section = "ProjectSearch";
        private const string _SearchKey = "SearchText";
        private const string _SettingFileName = "Setting.ini";
        private const string _SettingSection = "Setting";
        private const string _Key = "ProjectDocPath";

        //项目选中事件
        public delegate void ProjectSelectionChangeEvent(ProjectData projectData);
        //宗地选中事件
        public delegate void AreaSelectChangeEvent(ProjectAreaData projectAreaData);
        public delegate void AddProjectEvent();
        public delegate void ModifyProjectEvent(ProjectData projectAreaData);

        public event ProjectSelectionChangeEvent ProjectSelectionChange;
        public event AreaSelectChangeEvent AreaSelectChange;
        public event AddProjectEvent AddProject;
        public event ModifyProjectEvent ModifyProject;

        //查找到的项目列表
        private List<TreeNode> _FindList = new List<TreeNode>();
        //当前查找的索引
        private int _Index = 0;
        //所有项目列表
        private List<ProjectData> _ProjectList = new List<ProjectData>();
        //项目分组模式  0：年份分组  1：地区分组
        private int _GroupMode = 0;
        //保存原有的查找项目名称输入
        private string _OldInput = "";
        //项目文档路径
        private string _ProjectDocPath = "";
        private List<string> _GroupTable = new List<string>();
        private decimal _MapId;

        public ProjectData SelectedProject
        {
            get
            {
                if (projectTree.SelectedNode != null)
                {
                    ProjectData data=projectTree.SelectedNode.Tag as ProjectData;
                    return data;
                    ProjectData projectData = new ProjectData();

                    string sql = SqlHelper.GetSql("SelectProject");
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("MapId", _MapId));
                    param.Add(new SqlParameter("date", data.Date));
                    param.Add(new SqlParameter("area", data.Area));
                    DataTable table = SqlHelper.Select(sql, param);
                    if (table != null)
                    {
                        decimal projectId = 0;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if ((int)table.Rows[i]["项目ID"] != projectId)
                            {
                                if (projectData != null)
                                {
                                    _ProjectList.Add(projectData);
                                }
                                projectData = new ProjectData();
                                projectData.Area = table.Rows[i]["区域名称"].ToString();
                                projectData.Year = table.Rows[i]["项目年度"].ToString();
                                projectData.Date = table.Rows[i]["项目年度"].ToString();
                                projectId = (int)table.Rows[i]["项目ID"];
                            }
                            if (table.Rows[i]["ObjectData"]!=DBNull.Value)
                            {
                                ProjectAreaData areadata = new ProjectAreaData();
                                areadata.AreaName = table.Rows[i]["宗地编号"].ToString();
                                areadata.AreaObject = (Geometry)Common.DeserializeObject((byte[])table.Rows[i]["ObjectData"]);
                                projectData.AreaList.Add(areadata);
                            }
                        }
                    }
                    return projectData;
                }
                return null;
            }
        }

        public ProjectControl()
        {
            InitializeComponent();
            Clear();
            LoadSearchItems();
        }

        private void SetGroupItem()
        {
            comboBox1.Items.Clear();
            _GroupTable.Clear();
            string sql = SqlHelper.GetSql("SelectColumnNameWithoutKey");
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("tablename", "t_dixiaoarea_coll"));
            DataTable table = SqlHelper.Select(sql, param);
            param = new List<SqlParameter>();
            param.Add(new SqlParameter("tablename", "t_dixiaoarea_detail"));
            DataTable table1 = SqlHelper.Select(sql, param);
            //for (int i = 0; i < table.Rows.Count; i++)
            //{
                comboBox1.Items.Add("项目年度");
                _GroupTable.Add("t_dixiaoarea_coll.date");
                comboBox1.Items.Add("划分区域");
                _GroupTable.Add("t_dixiaoarea_coll.area");
            //}
            //for (int i = 0; i < table1.Rows.Count; i++)
            //{
            //    comboBox1.Items.Add(table1.Rows[i]["name"].ToString());
            //    _GroupTable.Add(table1.Rows[i][2].ToString() + "." + table1.Rows[i]["name"].ToString());
            //}
        }

        public void Clear()
        {
            _FindList.Clear();
            _Index = 0;
            _ProjectList.Clear();
            _GroupMode = 0;
            _OldInput = "";
            projectTree.Nodes[0].Nodes.Clear();
        }

        public void Initial(decimal mapid)
        {
            Clear();

            _MapId = mapid;

            SetGroupItem();
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            //ResetTree(_GroupMode);
        }

        private void LoadProject(decimal mapid)
        {
            return;
            int index = comboBox1.SelectedIndex;
            if (index < 0)
            {
                return;
            }
            string colname = _GroupTable[index];
            string sql = "select "+colname+" as groupname"+SqlHelper.GetSql("SelectAllProject") + colname;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("MapId", mapid));
            DataTable table = SqlHelper.Select(sql, param);
            projectTree.Nodes[0].Nodes.Clear();
            if (table == null)
            { return; }
            string firstvalue = "";

            TreeNode node = projectTree.Nodes[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string groupname = table.Rows[i][0].ToString();
                if (groupname == "")
                {
                    groupname = "未定义";
                }
                if (firstvalue != groupname)
                {
                    node = new TreeNode();
                    projectTree.Nodes[0].Nodes.Add(node);
                    node.Text = groupname;
                    firstvalue = groupname;
                }
                TreeNode subnode = new TreeNode();
                ProjectData data = new ProjectData();
                data.Date = table.Rows[i]["date"].ToString();
                data.Area = table.Rows[i]["area"].ToString();
                data.Year = data.Date;
                if (index == 0)
                {
                    subnode.Text = data.Area;
                }
                else if (index == 1)
                {
                    subnode.Text = data.Date;
                }
                subnode.Tag = data;
                node.Nodes.Add(subnode);
            }
            projectTree.Nodes[0].Expand();
        }

        /// <summary>
        /// 保存当前查找的内容到历史记录中
        /// </summary>
        private void SaveSearchItems()
        {
            int index = 1;
            string key = _SearchKey + index;
            string txt = Common.IniReadValue(_IniFile, _Section, key);
            while (txt != "")
            {
                if (txt == comFindText.Text.ToLower().Trim())
                {
                    return;
                }
                index++;
                key = _SearchKey + index;
                txt = Common.IniReadValue(_IniFile, _Section, key);
            }
            Common.IniWriteValue(_IniFile, _Section, key, comFindText.Text.ToLower().Trim());
        }

        /// <summary>
        /// 将历史查询记录加载到查找输入框中
        /// </summary>
        private void LoadSearchItems()
        {
            int index = 1;
            string key = _SearchKey + index;
            string txt = Common.IniReadValue(_IniFile, _Section, key);
            while (txt != "")
            {
                comFindText.Items.Add(txt);
                index++;
                key = _SearchKey + index;
                txt = Common.IniReadValue(_IniFile, _Section, key);
            }
        }

        /// <summary>
        /// 按照给定的项目名称查询项目，把查询结果的节点保存到_FindList中
        /// </summary>
        /// <param name="node"></param>
        /// <param name="findtext"></param>
        private void Find(TreeNode node, string findtext)
        {
            if (node.Text.ToLower().IndexOf(findtext.ToLower()) >= 0)
            {
                _FindList.Add(node);
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                Find(subnode, findtext);
            }
        }

        /// <summary>
        /// 删除指定节点，删除后，如果该节点的父节点为空，则父节点也一并删除
        /// </summary>
        /// <param name="node"></param>
        private void DeleteNode(TreeNode node)
        {
            if (node == projectTree.Nodes[0])
            {
                return;
            }
            TreeNode parentNode = node.Parent;
            node.Remove();
            if (parentNode.Nodes.Count == 0)
            {
                DeleteNode(parentNode);
            }
        }

        /// <summary>
        /// 将项目包含的宗地信息添加到项目节点中
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        private void AddAreaNode(TreeNode node, ProjectData data)
        {
            //node.Nodes.Clear();
            //foreach (ProjectAreaData area in data.AreaList)
            //{
            //    TreeNode newNode = new TreeNode(area.AreaName);
            //    newNode.Tag = area;
            //    node.Nodes.Add(newNode);
            //}
        }

        /// <summary>
        /// 按照给定分组模式，将项目添加到项目树中
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private TreeNode AddNode(int flag, ProjectData data)
        {
            string nodetext = data.Year;
            if (flag == 0)
            {
                nodetext = data.Year;
            }
            else if (flag == 1)
            {
                nodetext = data.Area;
            }
            TreeNode findnode = null;
            foreach (TreeNode node in projectTree.Nodes[0].Nodes)
            {
                if (node.Text == nodetext)
                {
                    TreeNode newNode = new TreeNode(data.Area);
                    newNode.Tag = data;
                    AddAreaNode(newNode, data);
                    node.Nodes.Add(newNode);
                    return node;
                }
                if (node.Text.CompareTo(nodetext) < 0)
                {
                    findnode = node;
                }
            }
            if (findnode == null)
            {
                findnode = new TreeNode(nodetext);
                projectTree.Nodes[0].Nodes.Add(findnode);
            }
            else
            {
                findnode = projectTree.Nodes[0].Nodes.Insert(findnode.Index + 1, nodetext);
            }
            TreeNode newNode1 = new TreeNode(data.Area);
            newNode1.Tag = data;
            findnode.Nodes.Add(newNode1);
            AddAreaNode(newNode1, data);
            if (!_ProjectList.Contains(data))
            {
                _ProjectList.Add(data);
            }
            return newNode1;

        }

        /// <summary>
        /// 重置项目树
        /// </summary>
        /// <param name="flag"></param>
        private void ResetTree(int flag)
        {
            projectTree.Nodes[0].Nodes.Clear();
            foreach (ProjectData data in _ProjectList)
            {
                AddNode(flag, data);
            }
        }

        /// <summary>
        /// 查找输入框内容变更时，重新查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comFindText_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        /// <summary>
        /// 点击查找按钮时，如果已经查找过，则显示当前索引的下一个节点，否则，重新查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (comFindText.Text.Trim() != _OldInput)
            {
                _FindList.Clear();
                _Index = 0;
                _OldInput = comFindText.Text.Trim();
            }
            if (_FindList.Count > _Index + 1)
            {
                _Index++;
                projectTree.SelectedNode = _FindList[_Index];
                projectTree.Focus();
                return;
            }
            Find(projectTree.Nodes[0], comFindText.Text.Trim());
            _Index = 0;
            if (_FindList.Count <= _Index)
            {
                MessageBox.Show("没有找到该项目。");
                comFindText.Focus();
                return;
            }
            projectTree.SelectedNode = _FindList[_Index];
            projectTree.Focus();
            SaveSearchItems();
        }

        /// <summary>
        /// 弹出菜单的可用性设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            btnModify.Enabled = projectTree.SelectedNode != null && projectTree.SelectedNode.Tag != null;
            btnDelete.Enabled = btnModify.Enabled;
            btnOpenDoc.Enabled = btnModify.Enabled;
        }

        /// <summary>
        /// 删除选中的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除选中的项目吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            SqlConnection conn = null;
            SqlTransaction tran = null;
            try
            {
                string sql = SqlHelper.GetSql("DeleteProjectById");
                List<SqlParameter> param = new List<SqlParameter>();
                ProjectData data = projectTree.SelectedNode.Tag as ProjectData;
                conn = SqlHelper.GetConnection();
                conn.Open();
                tran = conn.BeginTransaction();
                param.Add(new SqlParameter("area", data.Area));
                param.Add(new SqlParameter("MapId", _MapId));
                param.Add(new SqlParameter("date", data.Date));
                SqlHelper.Delete(conn, tran, sql, param);
                sql = SqlHelper.GetSql("DeleteProjectArea");
                param = new List<SqlParameter>();
                param.Add(new SqlParameter("area", data.Area));
                param.Add(new SqlParameter("MapId", _MapId));
                param.Add(new SqlParameter("date", data.Date));
                SqlHelper.Delete(conn, tran, sql, param);
                tran.Commit();
                conn.Close();
                tran = null;
                conn = null;
                //清除原有查询旧值，以便再次点击查询按钮时重新检索
                _OldInput = "";
            }
            catch (Exception ex)
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
            }
            if (_FindList.Contains(projectTree.SelectedNode))
            {
                _FindList.Remove(projectTree.SelectedNode);
                _Index--;
            }
            DeleteNode(projectTree.SelectedNode);
        }

        /// <summary>
        /// 修改选中的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (ModifyProject != null)
            {
                ProjectData projectData = SelectedProject;
                if(projectData==null)
                {
                    MessageBox.Show("没有查询到该项目，可能已经被删除。");
                    return;
                }
                ModifyProject(projectData);
            }
        }

        /// <summary>
        /// 项目数据修改处理
        /// </summary>
        /// <param name="projectData"></param>
        public void ModifyProjectData(ProjectData projectData)
        {
            for (int i = 0; i < _ProjectList.Count; i++)
            {
                if (_ProjectList[i].Area == projectData.Area && _ProjectList[i].Date == projectData.Date)
                {
                    _ProjectList.RemoveAt(i);
                }
            }
            TreeNode parentnode = projectTree.SelectedNode.Parent;
            projectTree.SelectedNode.Remove();
            if (parentnode.Nodes.Count <= 0)
            {
                parentnode.Remove();
            }
            AddNode(_GroupMode, projectData);
        }

        /// <summary>
        /// 节点双击时，修改选中的项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void projectTree_DoubleClick(object sender, EventArgs e)
        {
            if (projectTree.SelectedNode != null && projectTree.SelectedNode.Tag != null)
            {
                btnModify_Click(sender, e);
            }
        }

        /// <summary>
        /// 增加一个新项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddProject != null)
            {
                AddProject();
            }
        }

        /// <summary>
        /// 向树中添加一个新项目
        /// </summary>
        /// <param name="projectData"></param>
        public void AddProjectData(ProjectData projectData)
        {
            LoadProject(_MapId);
            foreach (TreeNode node in projectTree.Nodes[0].Nodes)
            {
                foreach (TreeNode subnode in node.Nodes)
                {
                    ProjectData data = subnode.Tag as ProjectData;
                    if (projectData.Date == data.Date&&projectData.Area==data.Area)
                    {
                        projectTree.SelectedNode = subnode;
                        break;
                    }
                }
            }
            //projectTree.SelectedNode = AddNode(_GroupMode, projectData);
        }

        /// <summary>
        /// 改变项目分组模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                _GroupMode = 0;
                ResetTree(0);
            }
            else if (e.ColumnIndex == 1)
            {
                _GroupMode = 1;
                ResetTree(1);
            }
        }

        /// <summary>
        /// 查找输入框中按键处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comFindText_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (projectListTip.SelectedRows.Count <= 0)
                {
                    if (projectListTip.RowCount > 0)
                    {
                        projectListTip.Rows[0].Selected = true;
                    }
                }
                else
                {
                    if (projectListTip.SelectedRows[0].Index == projectListTip.RowCount - 1)
                    {
                        projectListTip.Rows[0].Selected = true;
                    }
                    else
                    {
                        projectListTip.Rows[projectListTip.SelectedRows[0].Index + 1].Selected = true;
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                if (projectListTip.SelectedRows.Count <= 0)
                {
                    if (projectListTip.RowCount > 0)
                    {
                        projectListTip.Rows[projectListTip.RowCount - 1].Selected = true;
                    }
                }
                else
                {
                    if (projectListTip.SelectedRows[0].Index == 0)
                    {
                        projectListTip.Rows[projectListTip.RowCount - 1].Selected = true;
                    }
                    else
                    {
                        projectListTip.Rows[projectListTip.SelectedRows[0].Index - 1].Selected = true;
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (projectListTip.RowCount > 0)
                {
                    if (projectListTip.SelectedRows.Count > 0)
                    {
                        comFindText.Text = projectListTip.SelectedRows[0].Cells[0].Value.ToString();
                    }
                }
                projectListTip.Visible = false;
                btnSearch_Click(sender, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                comFindText.Text = _OldInput;
                projectListTip.Visible = false;
            }
        }

        /// <summary>
        /// 项目提示列表双击后，将选择的项目名称填入到查找输入框中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void projectListTip_DoubleClick(object sender, EventArgs e)
        {
            if (projectListTip.SelectedRows.Count <= 0)
            {
                return;
            }
            comFindText.Text = projectListTip.SelectedRows[0].Cells[0].Value.ToString();
            projectListTip.Visible = false;
        }

        private void projectTree_Click(object sender, EventArgs e)
        {
            if (projectTree.SelectedNode != null && projectTree.SelectedNode.Tag != null)
            {
                if (projectTree.SelectedNode.Tag is ProjectData)
                {
                    if (ProjectSelectionChange != null)
                    {
                        ProjectSelectionChange(projectTree.SelectedNode.Tag as ProjectData);
                    }
                }
                else if (projectTree.SelectedNode.Tag is ProjectAreaData)
                {
                    if (AreaSelectChange != null)
                    {
                        AreaSelectChange(projectTree.SelectedNode.Tag as ProjectAreaData);
                    }
                }
            }
        }

        /// <summary>
        /// 打开项目文档路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDoc_Click(object sender, EventArgs e)
        {
            _ProjectDocPath = Common.IniReadValue(_SettingFileName, _SettingSection, _Key);
            string path = _ProjectDocPath;
            if (!path.EndsWith("\\"))
            {
                path += "\\";
            }

            ProjectData data = SelectedProject;
            if (data == null)
            {
                MessageBox.Show("没有查询到该项目，可能已经被删除。");
                return;
            }
            path += data.Year + "\\" + data.Area;
            if (!Directory.Exists(path))
            {
                MessageBox.Show("当前选择的项目的文档路径" + path + "不存在。");
                return;
            }
            Process proc = new Process();
            proc.StartInfo.FileName = path;
            proc.Start();
        }

        private void comFindText_KeyPress(object sender, KeyPressEventArgs e)
        {
            string txt = comFindText.Text.Trim();
            txt = txt.ToLower();
            if (txt == "")
            {
                projectListTip.Visible = false;
                return;
            }
            projectListTip.Rows.Clear();
            foreach (ProjectData data in _ProjectList)
            {
                if (data.Area.ToLower().IndexOf(txt) >= 0)
                {
                    int row = projectListTip.Rows.Add();
                    projectListTip.Rows[row].Cells[0].Value = data.Area;
                    projectListTip.Rows[row].Cells[1].Value = data.Date;
                }
            }
            if (projectListTip.RowCount <= 0)
            {
                projectListTip.Visible = false;
                return;
            }
            projectListTip.Sort(projectListTip.Columns[0], ListSortDirection.Ascending);
            projectListTip.ClearSelection();

            projectListTip.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProject(_MapId);
        }
    }
}
