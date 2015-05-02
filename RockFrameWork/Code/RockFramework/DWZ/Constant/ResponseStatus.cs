using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.DWZ.Constant
{
    /// <summary>
    /// 响应状态
    /// </summary>
    public enum ResponseStatus
    {
        Success = 200, //操作成功
        Fail = 300, //操作失败
        SessionTimeout = 301  //会话超时
    }
}
