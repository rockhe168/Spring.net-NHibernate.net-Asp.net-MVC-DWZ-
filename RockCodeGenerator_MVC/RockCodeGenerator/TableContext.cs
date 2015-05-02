using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockCodeGenerator
{
    /// <summary>
    /// 表对象
    /// </summary>
   public  class TableContext
    {
       /// <summary>
       /// 表名
       /// </summary>
        public string TableName { get; set; }

       /// <summary>
       /// 表名描述
       /// </summary>
        public string TableDescription { get; set; }

       /// <summary>
       /// 表下面所有的集合
       /// </summary>
        public List<ColumnContext> ColumnList { get; set; }

       /// <summary>
       /// 属于数据库名称
       /// </summary>
        public string DataBaseName { get; set; }
    }
}
