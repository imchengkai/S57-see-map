using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace EasyMap
{
    public partial class FieldDefineForm : MyForm
    {
        public FieldDefineForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出属性定义设置吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            Close();
        }

        private void FieldDefineForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(null, null);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView view = dataGridView1;
            button3.Enabled = view.RowCount > 0 && view.SelectedRows != null;
            if (view.SelectedRows != null && view.SelectedRows.Count > 0)
            {
                int index = view.SelectedRows[0].Index;
                if (view.Rows[index].Cells["isnewrow"].Value!=null&&view.Rows[index].Cells["isnewrow"].Value.ToString()=="1")
                {
                    txtName.Text = "";
                    comType.Enabled = true;
                    comType.SelectedIndex = 0;
                    chkSelect.Checked = false;
                    chkInput.Checked = false;
                    chkVisible.Checked = true;
                    txtinput.Text = "";
                }
                else
                {
                    txtName.Text = GetValue(view,index,0);
                    comType.Text = GetValue(view, index, 1);
                    comType.Enabled = false;
                    txtinput.Enabled = false;
                    chkSelect.Enabled = false;
                    chkInput.Enabled = false;
                    chkSelect.Checked = "允许" == GetValue(view, index, 2);
                    chkInput.Checked = "允许" == GetValue(view, index, 3);
                    chkVisible.Checked = "允许" == GetValue(view, index, 6);
                    txtinput.Text = GetValue(view, index, 4);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Save();
            dataGridView1_SelectionChanged(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("如果删除该属性，那么该属性中包含的所有的数据将被永久性删除。\r\n确定要删除该图层中的这个属性吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            DataGridView view = dataGridView1;
            view.Rows.Remove(view.SelectedRows[0]);
            button3.Enabled = view.RowCount > 0;
            Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridView view = dataGridView1;
            int row=view.Rows.Add();
            view.Rows[row].Cells["isnewrow"].Value = "1";
            view.ClearSelection();
            view.Rows[row].Selected = true;
            button3.Enabled = true;
            txtName.Focus();
        }

        private void Save()
        {
            List<Hashtable> list = new List<Hashtable>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (GetValue(dataGridView1, i, 0).Trim() == "")
                {
                    MessageBox.Show("有属性名称没有设定，请重新设定属性名称。");
                    return;
                }
                Hashtable data = new Hashtable();
                data.Add("fieldname", GetValue(dataGridView1, i, 0));
                data.Add("oldfieldname", GetValue(dataGridView1, i, "oldname"));
                data.Add("fieldtype", (comboBox1.SelectedIndex+1).ToString());
                data.Add("datatype", GetValue(dataGridView1, i, 1));
                data.Add("allowunselect", GetValue(dataGridView1, i, 2) == "允许" ? "1" : "0");
                data.Add("allowinput", GetValue(dataGridView1, i, 3) == "允许" ? "1" : "0");
                data.Add("allowvisible", GetValue(dataGridView1, i, 6) == "允许" ? "1" : "0");
                data.Add("inputlist", GetValue(dataGridView1, i, 4));
                list.Add(data);
            }
            MapDBClass.InsertPropertyDefine(list, (comboBox1.SelectedIndex+1).ToString(),comboBox1.SelectedItem.ToString());

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["isnewrow"].Value = "0";
                dataGridView1.Rows[i].Cells["oldname"].Value = dataGridView1.Rows[i].Cells["fieldname"].Value;
            }
        }

        private string GetValue(DataGridView view,int row, int col)
        {
            if (view.Rows[row].Cells[col].Value == null)
            {
                return "";
            }
            return view.Rows[row].Cells[col].Value.ToString();
        }

        private string GetValue(DataGridView view, int row, string col)
        {
            if (view.Rows[row].Cells[col].Value == null)
            {
                return "";
            }
            return view.Rows[row].Cells[col].Value.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = MapDBClass.GetPropertyDefine((comboBox1.SelectedIndex+1).ToString());
            DataGridView view = dataGridView1;
            view.Rows.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                
                int row = view.Rows.Add();
                view.Rows[row].Cells[0].Value = table.Rows[i]["PropertyName"];
                view.Rows[row].Cells["oldname"].Value = table.Rows[i]["PropertyName"];
                view.Rows[row].Cells[1].Value = table.Rows[i]["DataType"];
                view.Rows[row].Cells[2].Value = "";
                view.Rows[row].Cells[3].Value = "";
                if (table.Rows[i]["DataType"].ToString() == "列表")
                {
                    view.Rows[row].Cells[2].Value = table.Rows[i]["AllowUnSelect"].ToString() == "1" ? "允许" : "不允许";
                    view.Rows[row].Cells[3].Value = table.Rows[i]["AllowInput"].ToString() == "1" ? "允许" : "不允许";
                }
                view.Rows[row].Cells[6].Value = table.Rows[i]["AllowVisible"].ToString() == "1" ? "允许" : "不允许";
                view.Rows[row].Cells[4].Value = table.Rows[i]["List"];
                view.Rows[row].Cells["isnewrow"].Value = "0";
            }
            btnOk.Enabled = true;
            button4.Enabled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtName.Text = Common.ToDBC(txtName.Text);
            DataGridView view = dataGridView1;
            for (int i = 0; i < view.RowCount; i++)
            {
                if (GetValue(view, i, 0) == txtName.Text.Trim() && i != view.SelectedRows[0].Index && txtName.Text.Trim()!="")
                {
                    MessageBox.Show("属性名称已经存在，请重新设置。");
                    return;
                }
            }
            if (view.SelectedRows != null && view.SelectedRows.Count > 0)
            {
                if (sender == txtName)
                {
                    view.SelectedRows[0].Cells[0].Value = txtName.Text.Trim();
                    view.SelectedRows[0].Cells[1].Value = comType.Text;
                }
                else if (sender == comType)
                {
                    view.SelectedRows[0].Cells[1].Value = comType.Text;
                }
                else if (sender == chkSelect)
                {
                    view.SelectedRows[0].Cells[2].Value = "";
                    if (comType.Text == "列表")
                    {
                        view.SelectedRows[0].Cells[2].Value = chkSelect.Checked ? "允许" : "不允许";
                    }
                }
                else if (sender == chkInput)
                {
                    view.SelectedRows[0].Cells[3].Value = "";
                    if (comType.Text == "列表")
                    {
                        view.SelectedRows[0].Cells[3].Value = chkInput.Checked ? "允许" : "不允许";
                    }
                }
                else if (sender == txtinput)
                {
                    view.SelectedRows[0].Cells[4].Value = "";
                    if (comType.Text == "列表")
                    {
                        view.SelectedRows[0].Cells[4].Value = txtinput.Text.Trim();
                    }
                }
                view.SelectedRows[0].Cells[6].Value = chkVisible.Checked ? "允许" : "不允许";
            }
        }

        private void comType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkInput.Enabled = comType.Text == "列表";
            chkSelect.Enabled = chkInput.Enabled;
            txtinput.Enabled = chkInput.Enabled;
            if (comType.Text != "列表")
            {
                txtinput.Text = "";
            }
            txtName_TextChanged(sender, e);
        }

    }
}
