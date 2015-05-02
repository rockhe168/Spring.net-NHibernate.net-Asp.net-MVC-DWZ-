using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.DWZ.Constant
{
    /// <summary>
    /// 返回操作类型
    /// </summary>
    public enum  CallbackType
    {
        closeCurrent, //关闭当前窗口
        forward       //跳转，重定向
    }
}
