using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.DomainModel
{
    /// <summary>
    /// 权限相关
    /// </summary>
    public class FunLimitEntity:Entity
    {
        /// <summary>
        /// 功能代码(具体代码xml配置)
        /// </summary>
        public virtual string FunCode { get; set; }
        /// <summary>
        /// 功能Url
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 1=页面、2=具体动作
        /// </summary>
        public virtual int? Level { get; set; }
        /// <summary>
        /// 父功能代码
        /// </summary>
        public virtual string ParentFunCode { get; set; }        
    }
}
