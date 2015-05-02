using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.Repository.Data
{
    public class HqlParameter
    {
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数值类型
        /// </summary>
        public DataType DataType { get; set; }


        /// <summary>
        ///操作表达式类型
        /// </summary>
        public ExpressionType ExpressionType { get; set; }

        /// <summary>
        /// Hql差数值配置初始化
        /// </summary>
        /// <param name="dataType">类型</param>
        /// <param name="value">值</param>
        public HqlParameter(DataType dataType,object value)
        {
            this.Value = value;
            this.DataType = dataType;
            this.ExpressionType=ExpressionType.Default;
        }

        /// <summary>
        /// Hql差数值配置初始化
        /// </summary>
        /// <param name="dataType">类型</param>
        /// <param name="value">值</param>
        /// <param name="expressionType">操作表达式类型</param>
        public HqlParameter(DataType dataType, object value,ExpressionType expressionType)
        {
            this.Value = value;
            this.DataType = dataType;
            this.ExpressionType = expressionType;
        }

    }
}
