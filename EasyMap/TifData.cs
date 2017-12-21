using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoSettings;
using System.Windows.Forms;
using System.Data;
using EasyMap;
using System.Collections;
using EasyMap.Geometries;
using ProjNet.CoordinateSystems.Transformations;
using System.Drawing;
using EasyMap.Layers;

namespace EasyMapServer
{
    [Serializable]
    public class TifData
    {
        public static TreeNode _PhotoRoot = new TreeNode();
        private static Map _Map;
        private static List<Hashtable> list;
        private static double _Seed = 0;
        private const string SHIQU_FILENAME = "大连市街区图";

        public static double Seed
        {
            get { return TifData._Seed; }
            set { TifData._Seed = value; }
        }

        public static Map Map
        {
            get { return TifData._Map; }
            set { TifData._Map = value; }
        }

        public static List<Hashtable> TifFiles
        {
            get
            {
                if (list == null)
                {
                    list = new List<Hashtable>();
                }
                return list;
            }
        }

        public static PhotoData FindByName(string name)
        {
            TreeNode node = FindNode(_PhotoRoot, name);
            if (node == null)
            {
                return null;
            }
            return node.Tag as PhotoData;
        }

        /// <summary>
        /// 根据区域名称查询节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static TreeNode FindNode(TreeNode node, string name)
        {
            if (node.Text == name)
            {
                return node;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                TreeNode ret = FindNode(subnode, name);
                if (ret != null)
                {
                    return ret;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置父节点领域
        /// </summary>
        /// <param name="node"></param>
        private static void SetParentData(TreeNode node)
        {
            TreeNode parent = node.Parent;
            if (parent == null)
            {
                return;
            }
            PhotoData childdata = node.Tag as PhotoData;
            if (childdata == null)
            {
                return;
            }
            PhotoData parentdata = parent.Tag as PhotoData;
            if (parentdata == null)
            {
                parentdata = new PhotoData();
                parentdata.MinX=childdata.MinX;
                parentdata.MinY=childdata.MinY;
                parentdata.MaxX=childdata.MaxX;
                parentdata.MaxY=childdata.MaxY;
                parent.Tag = parentdata;
            }
            if (parentdata.FileName == "" || parentdata.FileName == null)
            {
                if (parentdata.MinX > childdata.MinX)
                {
                    parentdata.MinX = childdata.MinX;
                }
                if (parentdata.MinY > childdata.MinY)
                {
                    parentdata.MinY = childdata.MinY;
                }
                if (parentdata.MaxX < childdata.MaxX)
                {
                    parentdata.MaxX = childdata.MaxX;
                }
                if (parentdata.MaxY < childdata.MaxY)
                {
                    parentdata.MaxY = childdata.MaxY;
                }
            }
            SetParentData(parent);
        }

        /// <summary>
        /// 影像图树初始化
        /// </summary>
        public static void Initial()
        {
            //查询影像图结构
            DataTable table = MapDBClass.GetPhotoParentChildList();
            //循环向树中添加节点
            for (int i = 0; table != null && i < table.Rows.Count; i++)
            {
                //取得父区域名称
                string parentName = (string)table.Rows[i]["ParentName"];
                //取得子区域名称
                string childName = (string)table.Rows[i]["ChildName"];
                //查询父节点
                TreeNode node = FindNode(_PhotoRoot, parentName);
                //如果父区域不存在，则增加父节点
                if (node == null)
                {
                    node = new TreeNode(parentName);
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        //取得父区域名称
                        string parentName1 = (string)table.Rows[j]["ParentName"];
                        if (parentName == parentName1)
                        {
                            //取得子区域名称
                            string childName1 = (string)table.Rows[j]["ChildName"];
                            TreeNode subnode1 = FindNode(_PhotoRoot, childName1);
                            if (subnode1 != null)
                            {
                                subnode1.Parent.Nodes.Remove(subnode1);
                                node.Nodes.Add(subnode1);
                            }
                        }
                    }
                    _PhotoRoot.Nodes.Add(node);
                    continue;
                }
                //增加子节点
                TreeNode subnode = new TreeNode(childName);
                node.Nodes.Add(subnode);
            }
            //取得区域范围数据
            table = MapDBClass.GetPhotoList();
            //循环更新区域范围数据到影像图树中
            for (int i = 0; table != null && i < table.Rows.Count; i++)
            {
                PhotoData data = new PhotoData();
                data.PhotoId = (int)table.Rows[i]["PhotoId"];
                data.Name = (string)table.Rows[i]["Name"];
                data.FileName = (string)table.Rows[i]["FileName"];
                data.MinX = (double)table.Rows[i]["MinX"];
                data.MinY = (double)table.Rows[i]["MinY"];
                data.MaxX = (double)table.Rows[i]["MaxX"];
                data.MaxY = (double)table.Rows[i]["MaxY"];
                data.Transform1 = (double)table.Rows[i]["Transform1"];
                data.Transform2 = (double)table.Rows[i]["Transform2"];
                data.Transform3 = (double)table.Rows[i]["Transform3"];
                data.Transform4 = (double)table.Rows[i]["Transform4"];
                data.Transform5 = (double)table.Rows[i]["Transform5"];
                data.Transform6 = (double)table.Rows[i]["Transform6"];
                data.Width = (int)table.Rows[i]["Width"];
                data.Height = (int)table.Rows[i]["Height"];
                TreeNode node = FindNode(_PhotoRoot, data.Name);
                if (node == null)
                {
                    continue;
                }
                node.Tag = data;
                SetParentData(node);
            }
            SetTreeNodeBoundingBox(_PhotoRoot);
        }

        private static void SetTreeNodeBoundingBox(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                SetTreeNodeBoundingBox(subnode);
            }
            if (node.Text == "普兰店")
            {
                string a = "";
            }
            if (node.Tag != null)
            {
                return;
            }
            PhotoData mydata = new PhotoData();
            if (node.Nodes.Count > 0)
            {
                PhotoData data = node.Nodes[0].Tag as PhotoData;
                mydata.MinX = data.MinX;
                mydata.MinY = data.MinY;
                mydata.MaxX = data.MaxX;
                mydata.MaxY = data.MaxY;
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                PhotoData data = subnode.Tag as PhotoData;
                if (mydata.MinX > data.MinX && data.MinX!=0)
                {
                    mydata.MinX = data.MinX;
                }
                if (mydata.MaxX < data.MaxX && data.MaxX != 0)
                {
                    mydata.MaxX = data.MaxX;
                }
                if (mydata.MinY > data.MinY && data.MinY != 0)
                {
                    mydata.MinY = data.MinY;
                }
                if (mydata.MaxY < data.MaxY && data.MaxY != 0)
                {
                    mydata.MaxY = data.MaxY;
                }
            }
            mydata.Name = node.Text;
            node.Tag = mydata;
        }

        /// <summary>
        /// 取得描画影像图所需文件以及金字塔级别和比例
        /// </summary>
        /// <param name="list"></param>
        /// <param name="node"></param>
        private static void FindDrawTif(List<Hashtable> list,TreeNode node,bool show_shiqu=false)
        {
            if (node == _PhotoRoot)
            {
                foreach (TreeNode subnode in node.Nodes)
                {
                    FindDrawTif(list, subnode, show_shiqu);
                }
            }
            else
            {
                PhotoData data = node.Tag as PhotoData;
                if (data == null)
                {
                    return;
                }
                if (data.FileName != "" && data.FileName!=null)
                {
                    int overviewlevel=0;
                    int overviewzoom=0;
                    bool flg = show_shiqu||SetOverView(data, out overviewlevel, out overviewzoom);
                    if (flg||(!flg&&node.Nodes.Count<=0&&overviewlevel==-1))
                    {
                        if ((!show_shiqu && data.FileName.IndexOf(SHIQU_FILENAME)<0) || (show_shiqu && data.FileName.IndexOf(SHIQU_FILENAME) >= 0))
                        {
                            Hashtable table = new Hashtable();
                            table.Add("PhotoData", data);
                            table.Add("overviewlevel", overviewlevel);
                            table.Add("overviewzoom", overviewzoom);
                            list.Add(table);
                        }
                        foreach (TreeNode subnode in node.Nodes)
                        {
                            FindDrawTif(list, subnode, show_shiqu);
                        }
                    }
                    else
                    {
                        foreach (TreeNode subnode in node.Nodes)
                        {
                            FindDrawTif(list, subnode, show_shiqu);
                        }
                    }
                }
                else
                {
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        FindDrawTif(list, subnode, show_shiqu);
                    }
                }
            }
        }

        public static bool SetOverView(PhotoData data,out int overviewlevel, out int overviewzoom)
        {
            Size _imagesize = new Size(data.Width, data.Height);
            double[] trans=new double[6];
            trans[0] = data.Transform1;
            trans[1] = data.Transform2;
            trans[2] = data.Transform3;
            trans[3] = data.Transform4;
            trans[4] = data.Transform5;
            trans[5] = data.Transform6;
            GeoTransform _geoTransform = new GeoTransform(trans);
            double right = 0, left = 0, top = 0, bottom = 0;
            double dblW, dblH;
            dblW = _imagesize.Width;
            dblH = _imagesize.Height;

            left = _geoTransform.EnvelopeLeft(dblW, dblH);
            right = _geoTransform.EnvelopeRight(dblW, dblH);
            top = _geoTransform.EnvelopeTop(dblW, dblH);
            bottom = _geoTransform.EnvelopeBottom(dblW, dblH);
            BoundingBox _envelope = new BoundingBox(left, bottom, right, top);
            overviewlevel = -1;
            overviewzoom = 1;
            int viewcount = 4;
            int[] scale = new int[viewcount + 1];
            int[] scalelevel = new int[viewcount + 1];
            scale[0] = 1;
            scalelevel[0] = -1;
            for (int i = 1; i <= viewcount; i++)
            {
                scale[i] = Convert.ToInt32(Math.Pow(2.0, i + 1));
                scalelevel[i] = i - 1;
            }
            BoundingBox displayBbox = Map.Envelope;
            // bounds of section of image to be displayed
            left = Math.Max(displayBbox.Left, _envelope.Left);
            top = Math.Min(displayBbox.Top, _envelope.Top);
            right = Math.Min(displayBbox.Right, _envelope.Right);
            bottom = Math.Max(displayBbox.Bottom, _envelope.Bottom);


            if ((displayBbox.Left > _envelope.Right) || (displayBbox.Right < _envelope.Left)
                || (displayBbox.Top < _envelope.Bottom) || (displayBbox.Bottom > _envelope.Top))
            {
                overviewlevel = scalelevel[1];
                overviewzoom = scale[1];
                return false;
            }

            for (int i = scale.Length - 1; i >= 0; i--)
            {
                BoundingBox trueImageBbox = new BoundingBox(left, bottom, left + (right - left) / scale[i], top);
                BoundingBox shownImageBbox;
                EasyMap.Geometries.Point imageTL = new EasyMap.Geometries.Point(), imageBR = new EasyMap.Geometries.Point();
                // put display bounds into current projection
//                if (_transform != null)
//                {
//#if !DotSpatialProjections
//                    _transform.MathTransform.Invert();
//                    shownImageBbox = GeometryTransform.TransformBox(trueImageBbox, _transform.MathTransform);
//                    _transform.MathTransform.Invert();
//#else
//                    shownImageBbox = GeometryTransform.TransformBox(trueImageBbox, _transform.Source, _transform.Target);
//#endif
//                }
//                else
                    shownImageBbox = trueImageBbox;

                // find min/max x and y pixels needed from image
                imageBR.X =
                    (int)
                    (Math.Max(_geoTransform.GroundToImage(shownImageBbox.TopLeft).X,
                              Math.Max(_geoTransform.GroundToImage(shownImageBbox.TopRight).X,
                                       Math.Max(_geoTransform.GroundToImage(shownImageBbox.BottomLeft).X,
                                                _geoTransform.GroundToImage(shownImageBbox.BottomRight).X))) + 1);
                imageBR.Y =
                    (int)
                    (Math.Max(_geoTransform.GroundToImage(shownImageBbox.TopLeft).Y,
                              Math.Max(_geoTransform.GroundToImage(shownImageBbox.TopRight).Y,
                                       Math.Max(_geoTransform.GroundToImage(shownImageBbox.BottomLeft).Y,
                                                _geoTransform.GroundToImage(shownImageBbox.BottomRight).Y))) + 1);
                imageTL.X =
                    (int)
                    Math.Min(_geoTransform.GroundToImage(shownImageBbox.TopLeft).X,
                             Math.Min(_geoTransform.GroundToImage(shownImageBbox.TopRight).X,
                                      Math.Min(_geoTransform.GroundToImage(shownImageBbox.BottomLeft).X,
                                               _geoTransform.GroundToImage(shownImageBbox.BottomRight).X)));
                imageTL.Y =
                    (int)
                    Math.Min(_geoTransform.GroundToImage(shownImageBbox.TopLeft).Y,
                             Math.Min(_geoTransform.GroundToImage(shownImageBbox.TopRight).Y,
                                      Math.Min(_geoTransform.GroundToImage(shownImageBbox.BottomLeft).Y,
                                               _geoTransform.GroundToImage(shownImageBbox.BottomRight).Y)));

                // stay within image
                if (imageBR.X > _imagesize.Width)
                    imageBR.X = _imagesize.Width;
                if (imageBR.Y > _imagesize.Height)
                    imageBR.Y = _imagesize.Height;
                if (imageTL.Y < 0)
                    imageTL.Y = 0;
                if (imageTL.X < 0)
                    imageTL.X = 0;
                double cx2 = imageBR.X;
                double cx1 = imageTL.X;

                int displayImageLength = (int)(imageBR.X - imageTL.X);
                int displayImageHeight = (int)(imageBR.Y - imageTL.Y);

                // convert ground coordinates to map coordinates to figure out where to place the bitmap
                System.Drawing.Point bitmapBR = new System.Drawing.Point((int)Map.WorldToImage(trueImageBbox.BottomRight).X + 1,
                                     (int)Map.WorldToImage(trueImageBbox.BottomRight).Y + 1);
                System.Drawing.Point bitmapTL = new System.Drawing.Point((int)Map.WorldToImage(trueImageBbox.TopLeft).X,
                                     (int)Map.WorldToImage(trueImageBbox.TopLeft).Y);

                int bitmapLength = bitmapBR.X - bitmapTL.X;
                int bitmapHeight = bitmapBR.Y - bitmapTL.Y;

                // check to see if image is on its side
                if (bitmapLength > bitmapHeight && displayImageLength < displayImageHeight)
                {
                    displayImageLength = bitmapHeight;
                    displayImageHeight = bitmapLength;
                }
                else
                {
                    displayImageLength = bitmapLength;
                    displayImageHeight = bitmapHeight;
                }
                double temp = (cx2 - cx1) / (displayImageLength - 1);
                if (i == 0 && temp < 1)
                {
                    return false;
                }
                else if (temp >= scale[i])
                {
                    overviewzoom = scale[i];
                    overviewlevel = scalelevel[i];
                    return true;
                }
            }
            overviewzoom = scale[0];
            overviewlevel = scalelevel[0];
            return true;
        }

        private static void dealList(List<Hashtable> list)
        {
            for(int i = list.Count - 1;i>=0;i--)
            {
                PhotoData data = list[i]["PhotoData"] as PhotoData;
                string name = data.Name;
                TreeNode node = FindNode(_PhotoRoot, name);
                while (node.Parent != null)
                {
                    bool find = false;
                    string parentname=node.Parent.Text;
                    foreach (Hashtable table1 in list)
                    {
                        PhotoData data1 = table1["PhotoData"] as PhotoData;
                        if (data1.Name == parentname)
                        {
                            if (data1.MinX < data.MinX && data1.MaxX > data.MaxX && data1.MinY < data.MinY && data1.MaxY > data.MaxY)
                            {
                                find = true;
                                break;
                            }
                        }
                    }
                    if (find)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                    node = node.Parent;
                }
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                PhotoData data = list[i]["PhotoData"] as PhotoData;
                string name = data.Name;
                TreeNode node = FindNode(_PhotoRoot, name);
                while (node.Parent != null)
                {
                    bool find = false;
                    string parentname=node.Parent.Text;
                    foreach (Hashtable table1 in list)
                    {
                        PhotoData data1 = table1["PhotoData"] as PhotoData;
                        if (data1.Name == parentname)
                        {
                            find = true;
                            break;
                        }
                    }
                    if (find)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                    node = node.Parent;
                }
            }
        }

        public static List<Hashtable> GetTifFileList(bool show_shiqu=false)
        {
            list = new List<Hashtable>();
            FindDrawTif(list, _PhotoRoot, show_shiqu);
            if (!show_shiqu)
            {
                dealList(list);
            }
            return list;
        }

        public static List<PhotoData> GetFlags(string name)
        {
            List<PhotoData> ret = new List<PhotoData>();
            if (name.Trim() != "")
            {
                TreeNode node = FindNode(_PhotoRoot, name);
                if (node != null)
                {
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        PhotoData subdata = subnode.Tag as PhotoData;
                        if (subdata.Name == null || subdata.Name == "")
                        {
                            subdata.Name = subnode.Text;
                        }
                        ret.Add(subdata);
                    }
                    return ret;
                }
            }
            return GetFlags();
        }

        public static List<PhotoData> GetFlags()
        {
            List<PhotoData> ret = new List<PhotoData>(); 
            if (list == null)
            {
                list = new List<Hashtable>();
            }
            foreach (Hashtable table in list)
            {
                PhotoData data = table["PhotoData"] as PhotoData;
                TreeNode node = _PhotoRoot.Nodes[0];
                if (data != null)
                {
                    node = FindNode(_PhotoRoot, data.Name);
                }
                if (node != null)
                {
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        PhotoData subdata = subnode.Tag as PhotoData;
                        if (subdata.Name == null || subdata.Name == "")
                        {
                            subdata.Name = subnode.Text;
                        }
                        ret.Add(subdata);
                    }
                    PhotoData subdata1 = node.Tag as PhotoData;
                    if (subdata1.Name == null || subdata1.Name == "")
                    {
                        subdata1.Name = node.Text;
                    }
                    ret.Add(subdata1);
                }
            }
            return ret;
        }

        public static BoundingBox GetCenterPhotoData(int currentlevel, int level)
        {
            EasyMap.Geometries.Point center = Map.Center;
            int rootlevel = level / 4+1;
            TreeNode node = _PhotoRoot;
            DataTable table = MapDBClass.GetCenterTifInformation(center.X, center.Y);
            bool find = false;
            PhotoData data = new PhotoData();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string name = table.Rows[i]["Name"] as string;
                node = FindNode(_PhotoRoot, name);
                PhotoData tempdata=null;
                if(node!=null)
                {
                    if(node.Parent!=null)
                    {
                        tempdata=node.Parent.Tag as PhotoData;
                    }
                }
                if (node != null && (node.Level == rootlevel||(tempdata!=null&&(tempdata.FileName==""||tempdata.FileName==null)&&node.Level-1==rootlevel)))
                {
                    
                    data.PhotoId = (int)table.Rows[i]["PhotoId"];
                    data.Name = (string)table.Rows[i]["Name"];
                    data.FileName = (string)table.Rows[i]["FileName"];
                    data.MinX = (double)table.Rows[i]["MinX"];
                    data.MinY = (double)table.Rows[i]["MinY"];
                    data.MaxX = (double)table.Rows[i]["MaxX"];
                    data.MaxY = (double)table.Rows[i]["MaxY"];
                    data.Transform1 = (double)table.Rows[i]["Transform1"];
                    data.Transform2 = (double)table.Rows[i]["Transform2"];
                    data.Transform3 = (double)table.Rows[i]["Transform3"];
                    data.Transform4 = (double)table.Rows[i]["Transform4"];
                    data.Transform5 = (double)table.Rows[i]["Transform5"];
                    data.Transform6 = (double)table.Rows[i]["Transform6"];
                    data.Width = (int)table.Rows[i]["Width"];
                    data.Height = (int)table.Rows[i]["Height"];
                    find = true;
                    break;
                }
            }
            if (!find)
            {
                return Map.Envelope;
            }
            int step = -100;
            if (currentlevel < level)
            {
                step = -100;
            }
            int overviewlevel = 0;
            int overviewzoom = 0;
            find = false;
            BoundingBox box = Map.Envelope;
            box = new BoundingBox(box.Min.X, box.Min.Y, box.Max.X, box.Max.Y);

            if (currentlevel < level)
            {
                while (overviewlevel != (4 - level % 4) && box.Min.X < box.Max.X && box.Min.Y < box.Max.Y)
                {
                    box.Min.X -= step;
                    box.Min.Y -= step;
                    box.Max.X += step;
                    box.Max.Y += step;

                    find = SetOverView(data, out overviewlevel, out overviewzoom);
                }
            }
            else
            {
                BoundingBox maxbox = Map.GetExtents();
                while (overviewlevel != (4 - level % 4) && box.Min.X > maxbox.Min.X && box.Min.Y > maxbox.Min.Y && box.Max.X < maxbox.Max.X&&box.Max.Y<maxbox.Max.Y)
                {
                    box.Min.X -= step;
                    box.Min.Y -= step;
                    box.Max.X += step;
                    box.Max.Y += step;

                    find = SetOverView(data, out overviewlevel, out overviewzoom);
                }
            }
            return box;
        }

        public static int GetLevelCount()
        {
            return 1;
        }
    }
}
