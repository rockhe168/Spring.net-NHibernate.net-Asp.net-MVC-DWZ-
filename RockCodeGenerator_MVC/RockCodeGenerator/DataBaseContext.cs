using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockCodeGenerator
{
    /// <summary>
    /// 当前数据库上下文
    /// </summary>
    public class DataBaseContext
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; }

        /// <summary>
        /// 当前选择的数据库表集合
        /// </summary>
        public List<TableContext> DataBaseTableList { get; set; }
    }
}
