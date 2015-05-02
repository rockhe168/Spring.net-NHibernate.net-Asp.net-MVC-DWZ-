using System;
using RockFramework.DomainModel;

namespace Domain.Entities.SysManager
{
    /// <summary>
    ///Company
    /// </summary>
    [Serializable]
    public class Company : Entity
    {
        /// <summary>
        ///机构名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 公司下面的部门集合
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Department> Departments { get; set; }
    }
}

