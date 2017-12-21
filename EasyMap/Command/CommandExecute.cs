using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Forms;
using System.Windows.Forms;
using EasyMap.Layers;
using EasyMap.Geometries;
using EasyMap.Data.Providers;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Data;
using EasyMap.Properties;

namespace EasyMap
{
    class CommandExecute
    {
        public delegate void SetToolBarStatusEvent();
        public delegate void SetLayerStyleEvent(VectorLayer layer, SolidBrush fillBrush, Pen linePen, Pen outLinePen, bool enableOutLine, TreeNode node);
        public event SetToolBarStatusEvent SetToolBarStatus;
        public event SetLayerStyleEvent SetLayerStyle;

        /// <summary>
        /// 命令执行
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        public void Execute(MapImage map,MyTree tree)
        {
            string[] commandlist = null;
            lock (Command.CommandList)
            {
                commandlist = new string[Command.CommandList.Count];
                Command.CommandList.CopyTo(commandlist);
                Command.CommandList.Clear();
            }
            foreach (string cmd in commandlist)
            {
                string[] list = cmd.Split(',');
                if (list.Length < 2)
                {
                    return;
                }
                decimal mapid = GetId(list[1]);
                if (mapid != map.Map.MapId)
                {
                    continue;
                }
                if (list[0] == CommandType.OptionType.CHANGE_MAP_NAME.ToString())
                {
                    ChangeMapName(map, tree, list);
                }
                else if (list[0] == CommandType.OptionType.ADD_LAYER.ToString())
                {
                    AddLayer(map, tree, list);
                }
                else if (list[0] == CommandType.OptionType.CHANGE_LAYER_NAME.ToString())
                {
                    ChangeLayerName(map, tree, list);
                }
                else if (list[0] == CommandType.OptionType.DELETE_LAYER.ToString())
                {
                    DeleteLayer(map, tree, list);
                }
                else if (list[0] == CommandType.OptionType.ADD_OBJECT.ToString())
                {
                    AddObject(map, tree, list);
                }
                else if (list[0] == CommandType.OptionType.CHANGE_OBJECT_NAME.ToString())
                {
                    ChangeObjectName(map, tree, list);
                }
                else if (list[0] == CommandType.OptionType.DELETE_OBJECT.ToString())
                {
                    DeleteObject(map, tree, list);
                }
                else if(list[0]==CommandType.OptionType.DRAW_PHOTO_DATA.ToString())
                {
                    List<decimal> maplist=new List<decimal>();
                    for(int i=1;i<list.Length;i++)
                    {
                        decimal id=0;
                        if(decimal.TryParse(list[i],out id))
                        {
                            maplist.Add(id);
                        }
                    }
                }
                else if (list[0] == CommandType.OptionType.GET_DEMO_FLAG.ToString())
                {
                    SqlHelper.IsDemo = list[1] != "100";
                }
            }
            if (commandlist.Length > 0)
            {
                map.Refresh();
                if (SetToolBarStatus != null)
                {
                    SetToolBarStatus();
                }
            }
        }

        /// <summary>
        /// 添加图层
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void AddLayer(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 3)
            {
                return;
            }
            decimal mapid = GetId(cmd[1]);
            decimal layerid = GetId(cmd[2]);
            //取得图层信息
            DataTable layertable = MapDBClass.GetLayerinfo(mapid, layerid);
            if (layertable == null || layertable.Rows.Count <= 0)
            {
                return;
            }
            string layername = layertable.Rows[0]["LayerName"].ToString();
            int type = 0;
            Int32.TryParse(layertable.Rows[0]["LayerType"].ToString(), out type);
            VectorLayer layer = new VectorLayer(layername, (VectorLayer.LayerType)type);

            layer.ID = layerid;
            layer.Style.EnableOutline = layertable.Rows[0]["EnableOutline"].ToString() == "1" ? true : false;
            layer.Style.Fill = new SolidBrush(Color.FromArgb((int)layertable.Rows[0]["Fill"]));
            layer.Style.Line = new Pen(Color.FromArgb((int)layertable.Rows[0]["Line"]), (int)layertable.Rows[0]["LineWidth"]);
            layer.Style.Outline = new Pen(Color.FromArgb((int)layertable.Rows[0]["Outline"]), (int)layertable.Rows[0]["OutlineWidth"]);
            Collection<Geometry> geometries = new Collection<Geometry>();
            GeometryProvider provider = new GeometryProvider(geometries);
            layer.DataSource = provider;
            TreeNode node = new TreeNode(layername);
            node.Tag = layer;
            node.Checked = true;
            if(SetLayerStyle!=null)
            {
                SetLayerStyle(layer, layer.Style.Fill, layer.Style.Line, layer.Style.Outline, layer.Style.EnableOutline, node);
            }
            string layertext = "";
            switch (layer.Type)
            {
                case VectorLayer.LayerType.BaseLayer:
                    layertext = Resources.BaseLayer;
                    break;
                case VectorLayer.LayerType.PhotoLayer:
                    layertext = Resources.PhotoLayer;
                    break;
                case VectorLayer.LayerType.ReportLayer:
                    layertext = Resources.ReportLayer;
                    break;
                case VectorLayer.LayerType.MotionLayer:
                    layertext = Resources.MotionPointLayer;
                    break;
                case VectorLayer.LayerType.SaleLayer:
                    layertext = Resources.SaleLayer;
                    break;
                case VectorLayer.LayerType.AreaInformation:
                    layertext = Resources.AreaInfoLayer;
                    break;
                case VectorLayer.LayerType.Pricelayer:
                    layertext = Resources.PriceLayer;
                    break;
                case VectorLayer.LayerType.HireLayer:
                    layertext = Resources.RentLayer;
                    break;
                case VectorLayer.LayerType.OtherLayer:
                    layertext = Resources.OtherLayer;
                    break;
            }
            TreeNode mainnode = new TreeNode(layertext);
            mainnode.Checked = true;
            TreeNode findnode = FindNode(tree,layertext);
            if (findnode != null)
            {
                mainnode = findnode;
            }
            else if (layertext == Resources.BaseLayer)
            {
                if (tree.Nodes[0].Nodes.Count > 0)
                {
                    if (tree.Nodes[0].Nodes[0].Text == Resources.PhotoLayer)
                    {
                        tree.Nodes[0].Nodes.Insert(1, mainnode);
                    }
                    else
                    {
                        tree.Nodes[0].Nodes.Insert(0, mainnode);
                    }
                }
                else
                {
                    tree.Nodes[0].Nodes.Add(mainnode);
                }
            }
            else
            {
                findnode = FindNode(tree,Resources.DataLayer);
                if (findnode == null)
                {
                    findnode = new TreeNode(Resources.DataLayer);
                    tree.Nodes[0].Nodes.Add(findnode);
                }
                findnode.Nodes.Add(mainnode);
            }


            mainnode.Nodes.Add(node);
            map.Map.AddLayer(layer);
            //取得元素信息
            DataTable objecttable = MapDBClass.GetObject(mapid, layerid);
            for (int j = 0; j < objecttable.Rows.Count; j++)
            {
                decimal objectid = (decimal)objecttable.Rows[j]["ObjectId"];
                byte[] data = (byte[])objecttable.Rows[j]["ObjectData"];
                Geometry geometry = (Geometry)Common.DeserializeObject(data);
                geometry.ID = objectid;
                geometry.Select = false;
                geometries.Add(geometry);
                AddGeometryToTree(node, geometry, geometry.Text);
            }
        }

        /// <summary>
        /// 更改地图名称
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void ChangeMapName(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 3)
            {
                return;
            }
            tree.Nodes[0].Text = cmd[2];
        }

        /// <summary>
        /// 图层名称变更
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void ChangeLayerName(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 4)
            {
                return;
            }
            TreeNode node = FindLayer(tree.Nodes[0], GetId(cmd[2]));
            if (node == null)
            {
                return;
            }
            node.Text = cmd[3];
        }

        /// <summary>
        /// 删除图层
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void DeleteLayer(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 3)
            {
                return;
            }
            decimal layerid = GetId(cmd[2]);
            TreeNode layernode = FindLayer(tree.Nodes[0], layerid);
            if (layernode == null)
            {
                return;
            }
            //取得当前图层
            VectorLayer layer = layernode.Tag as VectorLayer;
            if (map.NeedSave && layer.NeedSave)
            {
                MapDBClass.DeleteLayer(map.Map.MapId, layer.ID);
            }
            //删除选择的图层
            map.Map.Layers.Remove(layer);
            TreeNode parentnode = layernode.Parent;
            //删除选择的节点
            tree.Nodes.Remove(layernode);
            if (parentnode.Nodes.Count <= 0)
            {
                parentnode.Remove();
            }
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                if (node.Nodes.Count <= 0)
                {
                    node.Remove();
                }
            }
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void AddObject(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 4)
            {
                return;
            }
            decimal mapid = GetId(cmd[1]);
            decimal layerid = GetId(cmd[2]);
            decimal objectid = GetId(cmd[3]);
            TreeNode layerNode = FindLayer(tree.Nodes[0], layerid);
            if (layerNode == null)
            {
                return;
            }
            if (!(layerNode.Tag is VectorLayer))
            {
                return;
            }
            VectorLayer layer = layerNode.Tag as VectorLayer;
            if (!(layer.DataSource is GeometryProvider))
            {
                return;
            }
            GeometryProvider provider = layer.DataSource as GeometryProvider;
            Collection<Geometry> geometries = provider.Geometries as Collection<Geometry>;
            //取得元素信息
            DataTable objecttable = MapDBClass.GetObjectById(mapid, layerid,objectid);
            for (int j = 0; j < objecttable.Rows.Count; j++)
            {
                byte[] data = (byte[])objecttable.Rows[j]["ObjectData"];
                Geometry geometry = (Geometry)Common.DeserializeObject(data);
                geometry.ID = objectid;
                geometry.Select = false;
                geometries.Add(geometry);
                AddGeometryToTree(layerNode, geometry, geometry.Text);
            }
        }

        /// <summary>
        /// 元素名称变更
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void ChangeObjectName(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 5)
            {
                return;
            }
            TreeNode node = FindObject(tree.Nodes[0], GetId(cmd[2]), GetId(cmd[3]));
            if (node == null)
            {
                return;
            }
            node.Text = cmd[4];
        }

        /// <summary>
        /// 删除元素
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tree"></param>
        /// <param name="cmd"></param>
        private void DeleteObject(MapImage map, MyTree tree, string[] cmd)
        {
            if (cmd.Length != 4)
            {
                return;
            }
            decimal layerid=GetId(cmd[2]);
            decimal objectid=GetId(cmd[3]);
            TreeNode layernode = FindLayer(tree.Nodes[0],layerid);
            TreeNode objectnode = FindObject(layernode,layerid , objectid);
            if (objectnode == null)
            {
                return;
            }
            //取得当前图层
            VectorLayer layer = layernode.Tag as VectorLayer;
            //如果有当前选择区域，则从图层中删除
            if (map.NeedSave && layer.NeedSave)
            {
                MapDBClass.DeleteObject(map.Map.MapId, layerid, objectid, layer.Type);
            }
            ((GeometryProvider)layer.DataSource).Geometries.Remove(objectnode.Tag as Geometry);
            tree.Nodes.Remove(objectnode);
        }

        /// <summary>
        /// 查询指定图层
        /// </summary>
        /// <param name="node"></param>
        /// <param name="layerid"></param>
        /// <returns></returns>
        private TreeNode FindLayer(TreeNode node, decimal layerid)
        {
            if (node.Tag is VectorLayer)
            {
                VectorLayer layer = node.Tag as VectorLayer;
                if (layer.ID == layerid)
                {
                    return node;
                }
            }
            foreach (TreeNode subnode in node.Nodes)
            {
                TreeNode ret=FindLayer(subnode, layerid);
                if (ret != null)
                {
                    return ret;
                }
            }
            return null;
        }

        /// <summary>
        /// 查询指定元素
        /// </summary>
        /// <param name="node"></param>
        /// <param name="layerid"></param>
        /// <param name="objectid"></param>
        /// <returns></returns>
        private TreeNode FindObject(TreeNode node, decimal layerid, decimal objectid)
        {
            TreeNode layerNode = FindLayer(node, layerid);
            if (layerNode != null)
            {
                foreach (TreeNode subnode in layerNode.Nodes)
                {
                    if (subnode.Tag is Geometry)
                    {
                        Geometry geo = subnode.Tag as Geometry;
                        if (geo.ID == objectid)
                        {
                            return subnode;
                        }
                    }
                }
            }
            return null;
        }

        private decimal GetId(string id)
        {
            decimal mapid = 0;
            decimal.TryParse(id, out mapid);
            return mapid;
        }
        /// <summary>
        /// 根据节点名称查找节点
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private TreeNode FindNode(MyTree tree, string text)
        {
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                if (text == Resources.BaseLayer || text == Resources.PhotoLayer||text=="数据图层")
                {
                    if (node.Text == text)
                    {
                        return node;
                    }
                }
                else
                {
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        if (subnode.Text == text)
                        {
                            return subnode;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 添加元素到元素树
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="text"></param>
        private void AddGeometryToTree(TreeNode parentNode,Geometry geometry,string text)
        {
            TreeNode node = new TreeNode(text);
            node.Checked = true;
            if (text == "")
            {
                node.Text = Resources.DefaultObjectName;
            }
            node.Tag = geometry;
            if (text == "")
            {
                node.Text = Resources.DefaultObjectName + (parentNode.Nodes.Count + 1);
            }
            parentNode.Nodes.Add(node);
        }

    }
}
