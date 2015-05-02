using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RockFramework.DomainModel
{
    /// <summary>
    /// 可以持久到数据库的业务类都要继承的基类
    /// </summary>
    public abstract class Entity
    {
        //protected Entity()
        //{
        //    Id = Guid.NewGuid().ToString();
        //    CreateTime = DateTime.Now;
        //    UpdateTime = DateTime.Now;
        //    IsDelete = false;
        //    var currentUser =HttpContext.Current.Session==null ? null : HttpContext.Current.Session[SystemConstant.CurrentUserInfo] as Entity;
        //    if (currentUser != null)
        //    {
        //        CreateUser = currentUser.Id;
        //        UpdateUser = currentUser.Id;
        //    }
        //}

        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; protected set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; protected set; }

        /// <summary>
        /// 最好一次修改时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }

        /// <summary>
        /// 逻辑删除状态（true=已删除,false=未删除）
        /// </summary>
        public virtual bool IsDelete { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>
        public virtual string CreateUser { get; set; }

        /// <summary>
        /// 最后一次修改用户Id
        /// </summary>
        public virtual string UpdateUser { get; set; }

        /// <summary>
        /// 此数据当前版本
        /// </summary>
        public virtual Int32 Version { get; protected set; }
    }
}
