using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyMap.Geometries;

namespace EasyMap
{
    public class ProjectData
    {
        //项目名称
        private string _Date;
        //项目年度
        private string _Year;
        //项目区域
        private string _Area;
        //项目宗地列表
        private List<ProjectAreaData> _AreaList = new List<ProjectAreaData>();
        //项目ID
        private string _ProjectNo;

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        internal List<ProjectAreaData> AreaList
        {
            get { return _AreaList; }
            set { _AreaList = value; }
        }

        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public BoundingBox Envelope
        {
            get
            {
                if(_AreaList.Count==0)
                {
                    return null;
                }
                BoundingBox box = _AreaList[0].AreaObject.GetBoundingBox();
                foreach (ProjectAreaData area in _AreaList)
                {
                    BoundingBox box1 = area.AreaObject.GetBoundingBox();
                    if (box.Min.X > box1.Min.X)
                    {
                        box.Min.X = box1.Min.X;
                    }
                    if (box.Min.Y > box1.Min.Y)
                    {
                        box.Min.Y = box1.Min.Y;
                    }
                    if (box.Max.X < box1.Max.X)
                    {
                        box.Max.X = box1.Max.X;
                    }
                    if (box.Max.Y < box1.Max.Y)
                    {
                        box.Max.Y = box1.Max.Y;
                    }
                }
                return box;
            }
        }
    }
}
