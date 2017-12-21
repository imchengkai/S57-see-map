using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyMap
{
    public partial class MapListForm : MyForm
    {
        private decimal _MapId = 0;
        private bool _OpenTempMap = false;

        public bool OpenTempMap
        {
            get { return _OpenTempMap; }
            set { _OpenTempMap = value; }
        }

        public decimal MapId
        {
            get { return _MapId; }
            set { _MapId = value; }
        }

        public bool HasTempMap
        {
            get { return dataGridView1.RowCount>0; }
        }

        public MapListForm(bool opentempmap)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            OpenTempMap=opentempmap;
            DataTable table;
            if (opentempmap)
            {
                this.Text = "选择要恢复的地图";
                table = MapDBClass.GetTempMapList();
            }
            else
            {
                table=MapDBClass.GetMapList();
            }
            dataGridView1.DataSource = table;
            btnDelete.Enabled = table.Rows.Count > 0;
            button1.Enabled = table.Rows.Count > 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount<=0||dataGridView1.SelectedRows.Count<=0)
            {
                button2_Click(sender,e);
                return;
            }
            DialogResult = DialogResult.OK;
            MapId = (decimal)dataGridView1.SelectedRows[0].Cells["mapid"].Value;
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0 || dataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            if (!OpenTempMap)
            {
                if (MessageBox.Show("确定要删除选定的地图吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    decimal mapid = (decimal)dataGridView1.SelectedRows[0].Cells["mapid"].Value;
                    MapDBClass.DeleteMap(mapid);
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }
            button1.Enabled = dataGridView1.RowCount > 0;
        }

        private void MapListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2_Click(null, null);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            button1_Click(null, null);
        }
    }
}
