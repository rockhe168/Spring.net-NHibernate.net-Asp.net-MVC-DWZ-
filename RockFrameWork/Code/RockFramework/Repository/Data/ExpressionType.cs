using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.Repository.Data
{
    /// <summary>
    /// 操作表达式类型
    /// </summary>
    public enum ExpressionType
    {
        Default,
        Like,//like '%xxx%' 语句
        LinkStart,//like '%xxx' 语句
        LinkEnd//like 'xxx%'语句
    }
}
