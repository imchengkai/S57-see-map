using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyMap.Geometries;
using EasyMap.Forms;
using System.IO;
using CoorCon;

namespace EasyMap
{
    public partial class InputGeometryForm : MyForm
    {
        private Geometry _Geomtry = null;
        public delegate void AfterInputEvent(Geometry geomtry);
        public event AfterInputEvent AfterInput;
        public delegate void PreviewEvent(Geometry newGeometry,Geometry oldGeometry);
        public event PreviewEvent Preview;
        public delegate void PickCoordinateEvent(bool op);
        public event PickCoordinateEvent PickCoordinate;
        private Geometry _PreviewGeomtry = null;
        private bool _op = false;
        private bool _triggerevent = true;
        private bool _AllowEdit = false;
        private bool addNew = false;

        public bool AllowEdit
        {
            get { return _AllowEdit; }
            set 
            {
                _AllowEdit = value;
                view.ReadOnly = !value;
                pictureBox1.Enabled = value;
                paste.Enabled = value;
                btnOk.Enabled = value;
                btnImport.Enabled = value;
            }
        }

        public InputGeometryForm()
        {
            InitializeComponent();
        }

        public void Initial()
        {
            chkCurve.Enabled = true;
            chkPoint.Enabled = true;
            chkPolygon.Enabled = true;
            view.AllowUserToAddRows = true;
            view.Rows.Clear();
        }

        public void Initial(Geometry geomtry)
        {
            _triggerevent = false;
            _Geomtry = geomtry;
            chkCurve.Enabled = false;
            chkPoint.Enabled = false;
            chkPolygon.Enabled = false;
            if (geomtry is EasyMap.Geometries.Point)
            {
                EasyMap.Geometries.Point point =geomtry as EasyMap.Geometries.Point;
                chkPoint.Checked = true;
                view.AllowUserToAddRows = false;
                view.RowCount = 1;
                
                view.Rows[0].Cells[0].Value = String.Format("{0:N5}", point.X);
                view.Rows[0].Cells[1].Value = String.Format("{0:N5}", point.Y);
            }
            else if (geomtry is LinearRing || geomtry is LineString)
            {
                chkCurve.Checked = true;
                IList<EasyMap.Geometries.Point> vertices=null;
                if (geomtry is LinearRing)
                {
                    LinearRing line = geomtry as LinearRing;
                    vertices = line.Vertices;
                }
                else
                {
                    LineString line = geomtry as LineString;
                    vertices = line.Vertices;
                }
                view.RowCount = vertices.Count;
                int index = 0;
                foreach (EasyMap.Geometries.Point point in vertices)
                {
                    view.Rows[index].Cells[0].Value = String.Format("{0:N5}", point.X);
                    view.Rows[index].Cells[1].Value = String.Format("{0:N5}", point.Y);
                    index++;
                }
            }
            else if (geomtry is Polygon)
            {
                chkPolygon.Checked = true;
                Polygon polygon = geomtry as Polygon;
                IList<EasyMap.Geometries.Point> vertices = polygon.ExteriorRing.Vertices;
                view.RowCount = vertices.Count;
                int index = 0;
                foreach (EasyMap.Geometries.Point point in vertices)
                {
                    view.Rows[index].Cells[0].Value = String.Format("{0:N5}", point.X);
                    view.Rows[index].Cells[1].Value = String.Format("{0:N5}", point.Y);
                    index++;
                }
            }
            else if (geomtry is MultiPolygon)
            {
                chkPolygon.Checked = true;
                MultiPolygon polygons = geomtry as MultiPolygon;
                int index = 0;

                for (int i = 0; i < polygons.Polygons.Count; i++)
                {
                    Polygon polygon = polygons.Polygons[i];
                    IList<EasyMap.Geometries.Point> vertices = polygon.ExteriorRing.Vertices;

                    foreach (EasyMap.Geometries.Point point in vertices)
                    {
                        index++;
                    }
                }
                view.RowCount = index;
                index = 0;
                for (int i = 0; i < polygons.Polygons.Count;i++ )
                {
                    Polygon polygon = polygons.Polygons[i];
                    IList<EasyMap.Geometries.Point> vertices = polygon.ExteriorRing.Vertices;
                    
                    foreach (EasyMap.Geometries.Point point in vertices)
                    {
                        view.Rows[index].Cells[0].Value = String.Format("{0:N5}", point.X);
                        view.Rows[index].Cells[1].Value = String.Format("{0:N5}", point.Y);
                        view.Rows[index].Cells[2].Value = i;
                        index++;
                    }
                }
            }
            _triggerevent = true;
        }

        private string GetValue(int row, int col)
        {
            if (view.Rows[row].Cells[col].Value == null)
            {
                return "";
            }
            return view.Rows[row].Cells[col].Value.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
                double x = 0;
                double y = 0;
                int count = 0;
                if (!addNew)
                {
                    if (chkPoint.Checked)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (!double.TryParse(GetValue(i, 0), out x))
                            {
                                continue;
                            }
                            if (!double.TryParse(GetValue(i, 1), out y))
                            {
                                continue;
                            }
                            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                            if (_Geomtry != null)
                            {
                                point = _Geomtry as EasyMap.Geometries.Point;
                                point.X = x;
                                point.Y = y;
                            }

                            if (Preview != null)
                            {
                                Preview(null, _PreviewGeomtry);
                            }
                            if (AfterInput != null)
                            {
                                if (_Geomtry != null)
                                {
                                    AfterInput(null);
                                }
                                else
                                {
                                    AfterInput(point);
                                }
                            }
                        }
                    }
                    else if (chkCurve.Checked)
                    {
                        if (_Geomtry != null)
                        {
                            count = 0;
                            for (int i = 0; i < view.RowCount; i++)
                            {
                                if (!double.TryParse(GetValue(i, 0), out x))
                                {
                                    continue;
                                }
                                if (!double.TryParse(GetValue(i, 1), out y))
                                {
                                    continue;
                                }
                                count++;
                            }
                            if (count < 2)
                            {
                                return;
                            }
                        }
                        LineString lines = new LineString();

                        IList<EasyMap.Geometries.Point> vertices = lines.Vertices;
                        if (_Geomtry != null)
                        {
                            if (_Geomtry is LinearRing)
                            {
                                LinearRing line = _Geomtry as LinearRing;
                                vertices = line.Vertices;
                            }
                            else if (_Geomtry is LineString)
                            {
                                LineString line = _Geomtry as LineString;
                                vertices = line.Vertices;
                            }
                        }
                        vertices.Clear();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (!double.TryParse(GetValue(i, 0), out x))
                            {
                                continue;
                            }
                            if (!double.TryParse(GetValue(i, 1), out y))
                            {
                                continue;
                            }
                            count++;
                            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                            vertices.Add(point);
                        }

                        if (Preview != null)
                        {
                            Preview(null, _PreviewGeomtry);
                            _PreviewGeomtry = null;
                        }
                        if (AfterInput != null && count > 1)
                        {
                            if (_Geomtry == null)
                            {
                                AfterInput(lines);
                            }
                            else
                            {
                                AfterInput(null);
                            }
                        }
                        if (_Geomtry == null)
                        {
                            Initial(lines);
                        }
                    }
                    else if (chkPolygon.Checked)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (!double.TryParse(GetValue(i, 0), out x))
                            {
                                continue;
                            }
                            if (!double.TryParse(GetValue(i, 1), out y))
                            {
                                continue;
                            }
                            count++;
                        }
                        if (count < 2)
                        {
                            return;
                        }
                        Polygon polygon = new Polygon();
                        IList<EasyMap.Geometries.Point> vertices = polygon.ExteriorRing.Vertices;
                        if (_Geomtry == null)
                        {
                            vertices = polygon.ExteriorRing.Vertices;
                            vertices.Clear();
                            for (int i = 0; i < view.RowCount; i++)
                            {
                                if (!double.TryParse(GetValue(i, 0), out x))
                                {
                                    continue;
                                }
                                if (!double.TryParse(GetValue(i, 1), out y))
                                {
                                    continue;
                                }
                                count++;
                                EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                                vertices.Add(point);
                            }
                        }
                        else
                        {
                            if (_Geomtry is Polygon)
                            {
                                polygon = _Geomtry as Polygon;
                                vertices = polygon.ExteriorRing.Vertices;
                                vertices.Clear();
                                for (int i = 0; i < view.RowCount; i++)
                                {
                                    if (!double.TryParse(GetValue(i, 0), out x))
                                    {
                                        continue;
                                    }
                                    if (!double.TryParse(GetValue(i, 1), out y))
                                    {
                                        continue;
                                    }
                                    count++;
                                    EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                                    vertices.Add(point);
                                }
                            }
                            else if (_Geomtry is MultiPolygon)
                            {
                                MultiPolygon polygons = _Geomtry as MultiPolygon;
                                for (int i = 0; i < polygons.Polygons.Count; i++)
                                {

                                    polygon = polygons.Polygons[i];
                                    vertices = polygon.ExteriorRing.Vertices;
                                    vertices.Clear();
                                }
                                for (int i = 0; i < view.RowCount; i++)
                                {
                                    int index = Int32.Parse(GetValue(i, 2));
                                    polygon = polygons.Polygons[index];
                                    vertices = polygon.ExteriorRing.Vertices;
                                    if (!double.TryParse(GetValue(i, 0), out x))
                                    {
                                        continue;
                                    }
                                    if (!double.TryParse(GetValue(i, 1), out y))
                                    {
                                        continue;
                                    }
                                    count++;
                                    EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                                    vertices.Add(point);
                                }
                            }
                        }
                        if (Preview != null)
                        {
                            Preview(_PreviewGeomtry, null);
                            _PreviewGeomtry = null;
                        }
                        if (AfterInput != null && count > 2)
                        {
                            if (_Geomtry == null)
                            {
                                AfterInput(polygon);
                            }
                            else
                            {
                                AfterInput(null);
                            }
                        }
                        if (_Geomtry == null)
                        {
                            Initial(polygon);
                        }
                    }
                }
                else
                {
                    // double x = 0;
                    //double y = 0;
                    //int count = 0;
                    if (chkPoint.Checked)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (!double.TryParse(GetValue(i, 0), out x))
                            {
                                continue;
                            }
                            if (!double.TryParse(GetValue(i, 1), out y))
                            {
                                continue;
                            }
                            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                            if (_Geomtry != null)
                            {
                                point = _Geomtry as EasyMap.Geometries.Point;
                                point.X = x;
                                point.Y = y;
                            }

                            if (Preview != null)
                            {
                                Preview(null, _PreviewGeomtry);
                            }
                            if (AfterInput != null)
                            {
                                if (_Geomtry != null)
                                {
                                    AfterInput(null);
                                }
                                else
                                {
                                    AfterInput(point);
                                }
                            }
                        }
                    }
                    else if (chkCurve.Checked)
                    {
                        if (_Geomtry != null)
                        {
                            count = 0;
                            for (int i = 0; i < view.RowCount; i++)
                            {
                                if (!double.TryParse(GetValue(i, 0), out x))
                                {
                                    continue;
                                }
                                if (!double.TryParse(GetValue(i, 1), out y))
                                {
                                    continue;
                                }
                                count++;
                            }
                            if (count < 2)
                            {
                                return;
                            }
                        }
                        LineString lines = new LineString();

                        IList<EasyMap.Geometries.Point> vertices = lines.Vertices;
                        if (_Geomtry != null)
                        {
                            if (_Geomtry is LinearRing)
                            {
                                LinearRing line = _Geomtry as LinearRing;
                                vertices = line.Vertices;
                            }
                            else if (_Geomtry is LineString)
                            {
                                LineString line = _Geomtry as LineString;
                                vertices = line.Vertices;
                            }
                        }
                        vertices.Clear();
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (!double.TryParse(GetValue(i, 0), out x))
                            {
                                continue;
                            }
                            if (!double.TryParse(GetValue(i, 1), out y))
                            {
                                continue;
                            }
                            count++;
                            EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                            vertices.Add(point);
                        }

                        if (Preview != null)
                        {
                            Preview(null, _PreviewGeomtry);
                            _PreviewGeomtry = null;
                        }
                        if (AfterInput != null && count > 1)
                        {
                            if (_Geomtry == null)
                            {
                                AfterInput(lines);
                            }
                            else
                            {
                                AfterInput(null);
                            }
                        }
                        if (_Geomtry == null)
                        {
                            Initial(lines);
                        }
                    }
                    else if (chkPolygon.Checked)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            if (!double.TryParse(GetValue(i, 0), out x))
                            {
                                continue;
                            }
                            if (!double.TryParse(GetValue(i, 1), out y))
                            {
                                continue;
                            }
                            count++;
                        }
                        if (count < 2)
                        {
                            return;
                        }
                        Polygon polygon = new Polygon();
                        IList<EasyMap.Geometries.Point> vertices = polygon.ExteriorRing.Vertices;
                        if (_Geomtry == null)
                        {
                            vertices = polygon.ExteriorRing.Vertices;
                            vertices.Clear();
                            for (int i = 0; i < view.RowCount; i++)
                            {
                                if (!double.TryParse(GetValue(i, 0), out x))
                                {
                                    continue;
                                }
                                if (!double.TryParse(GetValue(i, 1), out y))
                                {
                                    continue;
                                }
                                count++;
                                EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                                vertices.Add(point);
                            }
                        }
                        else
                        {
                            if (_Geomtry is Polygon)
                            {
                                //polygon = _Geomtry as Polygon;
                                polygon = new Geometries.Polygon();
                                vertices = polygon.ExteriorRing.Vertices;
                                vertices.Clear();
                                for (int i = 0; i < view.RowCount; i++)
                                {
                                    if (!double.TryParse(GetValue(i, 0), out x))
                                    {
                                        continue;
                                    }
                                    if (!double.TryParse(GetValue(i, 1), out y))
                                    {
                                        continue;
                                    }
                                    count++;
                                    EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                                    vertices.Add(point);
                                }
                            }
                            else if (_Geomtry is MultiPolygon)
                            {
                                //MultiPolygon polygons = _Geomtry as MultiPolygon;
                                MultiPolygon polygons = new MultiPolygon();
                                for (int i = 0; i < polygons.Polygons.Count; i++)
                                {

                                    polygon = polygons.Polygons[i];
                                    vertices = polygon.ExteriorRing.Vertices;
                                    vertices.Clear();
                                }
                                for (int i = 0; i < view.RowCount; i++)
                                {
                                    int index = Int32.Parse(GetValue(i, 2));
                                    polygon = polygons.Polygons[index];
                                    vertices = polygon.ExteriorRing.Vertices;
                                    if (!double.TryParse(GetValue(i, 0), out x))
                                    {
                                        continue;
                                    }
                                    if (!double.TryParse(GetValue(i, 1), out y))
                                    {
                                        continue;
                                    }
                                    count++;
                                    EasyMap.Geometries.Point point = new EasyMap.Geometries.Point(x, y);
                                    vertices.Add(point);
                                }
                            }
                        }
                        if (Preview != null)
                        {
                            Preview(_PreviewGeomtry, null);
                            _PreviewGeomtry = null;
                        }
                        if (AfterInput != null && count > 2)
                        {
                            if (_Geomtry == null)
                            {
                                AfterInput(polygon);
                            }
                            else
                            {
                                AfterInput(null);
                            }
                        }
                        if (_Geomtry == null)
                        {
                            Initial(polygon);
                        }
                    }
                }
                }

        private void view_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double data = 0;
            if (!double.TryParse(GetValue(e.RowIndex, e.ColumnIndex), out data))
            {
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            }
        }

        private void paste_Click(object sender, EventArgs e)
        {
            Common.PasteToGrid(view, true);
        }

        public void InsertCoordinate(EasyMap.Geometries.Point point)
        {
            int row = 0;
            if (view.CurrentCell != null)
            {
                row = view.CurrentCell.RowIndex;
            }
            view.Rows[row].Cells[0].Value = point.X;
            view.Rows[row].Cells[1].Value = point.Y;
            if (row == view.RowCount - 1)
            {
                int newrow=view.Rows.Add();

                view.Rows[newrow].Cells[0].Value = point.X;
                view.Rows[newrow].Cells[1].Value = point.Y;
                view.Rows[row+1].Cells[0].Value = "";
                view.Rows[row+1].Cells[1].Value = "";
            }
            row++;
            view.CurrentCell = view.Rows[row].Cells[0];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (PickCoordinate != null)
            {
                PickCoordinate(pictureBox1.Checked);
            }
        }

        private void view_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if (!_triggerevent)
            {
                return;
            }
            Geometry old = _PreviewGeomtry;
            NewPreviewGeomtry();
            
            if (Preview != null)
            {
                Preview(_PreviewGeomtry, old);
            }
        }

        private void NewPreviewGeomtry()
        {
            if (chkPoint.Checked)
            {
                MultiPoint ps = new MultiPoint();
                _PreviewGeomtry = ps;
                for(int i=0;i<view.RowCount;i++)
                {
                    double x=0;
                    double y=0;
                    string sx=GetValue(i,0);
                    string sy=GetValue(i,1);
                    if(sx!=""&&sy!="")
                    {
                        double.TryParse(sx,out x);
                        double.TryParse(sy,out y);
                        ps.Points.Add(new EasyMap.Geometries.Point(x, y));
                    }
                }
                
            }
            else if (chkCurve.Checked)
            {
                LineString line=new LineString();
                _PreviewGeomtry = line;

                for (int i = 0; i < view.RowCount; i++)
                {
                    double x = 0;
                    double y = 0;
                    string sx = GetValue(i, 0);
                    string sy = GetValue(i, 1);
                    if (sx != "" && sy != "")
                    {
                        double.TryParse(sx, out x);
                        double.TryParse(sy, out y);
                        line.Vertices.Add(new EasyMap.Geometries.Point(x, y));
                    }
                }
            }
            else if (chkPolygon.Checked)
            {
                Polygon polygon = new Polygon();
                _PreviewGeomtry = polygon;
                for (int i = 0; i < view.RowCount; i++)
                {
                    double x = 0;
                    double y = 0;
                    string sx = GetValue(i, 0);
                    string sy = GetValue(i, 1);
                    if (sx != "" && sy != "")
                    {
                        double.TryParse(sx, out x);
                        double.TryParse(sy, out y);
                        polygon.ExteriorRing.Vertices.Add(new EasyMap.Geometries.Point(x, y));
                    }
                }
                
            }
        }

        private void chkPoint_CheckedChanged(object sender, EventArgs e)
        {
            Geometry old = _PreviewGeomtry;
            NewPreviewGeomtry();

            if (Preview != null)
            {
                Preview(_PreviewGeomtry, old);
            }
        }

        private void InputGeometryForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (Preview != null)
            {
                Preview(null, _PreviewGeomtry);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (!File.Exists(openFileDialog1.FileName))
            {
                MessageBox.Show("没有找到指定文件。");
                return;
            }
            StreamReader sr=new StreamReader(openFileDialog1.FileName);
            string[] lines = sr.ReadToEnd().Split('\n');
            sr.Close();
            List<EasyMap.Geometries.Point> points = new List<Geometries.Point>();
            string first = "";
            string last = "";
            DataTable transtable = new DataTable();
            transtable.Columns.Add("X");
            transtable.Columns.Add("Y");
            transtable.Columns.Add("Z");
            foreach (string line in lines)
            {
                string data = line.Trim();
                if (!data.StartsWith("J"))
                {
                    continue;
                }
                string[] list = data.Split(',');
                if (list.Length < 3)
                {
                    continue;
                }
                double x=0,y=0;
                if(!double.TryParse(list[list.Length-2],out x)
                    ||!double.TryParse(list[list.Length-1],out y))
                {
                    continue;
                }
                if (first == "")
                {
                    first = list[0];
                }
                else
                {
                    last = list[0];
                }
                DataRow row = transtable.Rows.Add();
                row[0] = x;
                row[1] = y;
                row[2] = 0;
            }
            PConvert convertform = new PConvert();
            convertform.table = transtable;
            if (convertform.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            bool convertSuccess = true;
            for (int i = 0; i < convertform.table.Rows.Count; i++)
            {
                double x, y;
                if(!double.TryParse ( convertform.table.Rows[i][0].ToString(),out x))
                {
                    convertSuccess = false;
                    continue;
                }
                if (!double.TryParse(convertform.table.Rows[i][1].ToString(), out y))
                {
                    convertSuccess = false;
                    continue;
                }
                EasyMap.Geometries.Point point = new Geometries.Point(x,y);
                points.Add(point);
            }
            view.Rows.Clear();
            if (first == last && first != "")
            {
                chkPolygon.Checked = true;
            }
            else if (first != "")
            {
                chkCurve.Checked = true;
            }
            for (int i = 0; i < points.Count;i++ )
            {
                if (first == last && first != "" && i == points.Count - 1)
                {
                    continue;
                }
                EasyMap.Geometries.Point point = points[i];
                int row = view.Rows.Add();
                view.Rows[row].Cells[0].Value = point.X;
                view.Rows[row].Cells[1].Value = point.Y;
            }
            if (!convertSuccess)
            {
                MessageBox.Show("导入完毕，但是坐标转换过程中有错误发生，已经被忽略。");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewPreviewGeomtry();
            if (Preview != null)
            {
                Preview(_PreviewGeomtry,null );
            }
            view.Rows.Clear();
            addNew =true;
            chkPoint.Enabled = true;
            chkCurve.Enabled = true;
            chkPolygon.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //view.Rows.Add();
            view.Rows.AddCopy(view.SelectedRows.Count);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //删除选中行
            if (view.Rows.Count < 2)
            {
                view.Rows.Clear();
            }
            else
            {
                //view.Rows.Remove(view.CurrentRow);
                DataRowView drv = view.SelectedRows[0].DataBoundItem as DataRowView;
                drv.Delete(); 
            }
        }

    }
}
