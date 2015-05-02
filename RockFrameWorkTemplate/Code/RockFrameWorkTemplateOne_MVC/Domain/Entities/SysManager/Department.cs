using System;
using RockFramework.DomainModel;

namespace Domain.Entities.SysManager
{
    /// <summary>
    ///Department
    /// </summary>
    [Serializable]
    public class Department : Entity
    {
        /// <summary>
        ///所属机构
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        ///部门名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///部门代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 部门所属公司名称
        /// </summary>
        public virtual string CompanyName { get; set; }


    }
}

