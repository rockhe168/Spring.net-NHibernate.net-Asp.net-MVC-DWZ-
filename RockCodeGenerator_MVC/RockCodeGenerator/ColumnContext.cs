using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockCodeGenerator
{
   /// <summary>
   /// 字段对象
   /// </summary>
   public  class ColumnContext
    {
       /// <summary>
       /// 字段名称
       /// </summary>
       public string Name { get; set; }

       /// <summary>
       /// 字段说明
       /// </summary>
       public string NameDescription { get; set; }

       /// <summary>
       /// 数据类型
       /// </summary>
       public string DateType { get; set; }

       /// <summary>
       /// 字段长度
       /// </summary>
       public int ? Length { get; set; }

       /// <summary>
       /// 字段默认值
       /// </summary>
       public string DefaultValue { get; set; }

       /// <summary>
       /// 是否可为空，true=可空，false=不能为空
       /// </summary>
       public bool IsNull { get; set; }

       /// <summary>
       /// 是否主键，true=是主键，false=不是主键
       /// </summary>
       public bool IsPk { get; set; }

       /// <summary>
       /// 是否外键
       /// </summary>
       public bool IsFk { get; set; }

    }
}
