using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMap
{
    class CommandType
    {
        public enum OptionType
        {
            //添加图层
            ADD_LAYER = 1,
            //变更图层名称
            CHANGE_LAYER_NAME = 2,
            //变更图层显示风格
            CHANGE_LAYER_STYLE = 3,
            //变更图层顺序
            CHANGE_LAYER_SORT = 4,
            //删除图层
            DELETE_LAYER = 5,

            //添加元素
            ADD_OBJECT = 6,
            //锁定元素
            LOCK_OBJECT = 7,
            //变更元素名称
            CHANGE_OBJECT_NAME = 8,
            //变更元素
            CHANGE_OBJECT = 9,
            //解锁元素
            UNLOCK_OBJECT = 10,
            //删除元素
            DELETE_OBJECT = 11,
            //变更元素顺序
            CHANGE_OBJECT_SORT = 12,

            //更改地图名称
            CHANGE_MAP_NAME = 13,

            //传输描画地图影像图层所需参数
            DRAW_PHOTO_DATA = 14,
            //传输描画地图影像图层位图数据
            DRAW_PHOTO_IMAGE = 15,
            //取得演示标志
            GET_DEMO_FLAG = 97,
            //取得打开的地图ID列表
            GET_OPEN_MAP_LIST = 98,
            //传输打开的地图ID
            OPEN_MAP_ID = 99,

            //系统轮询
            SYSTEM_ASK = 0,
            //退出
            LOGOUT = -1
        }

        public static string SERVER_SETTING_FILENAME = "ClientSetting.ini";
    }
}
