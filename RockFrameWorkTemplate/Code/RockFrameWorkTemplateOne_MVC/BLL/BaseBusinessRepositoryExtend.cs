using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockFramework.Repository;
using Dao;

namespace BLL
{
    /// <summary>
    /// 业务对象扩展内
    /// </summary>
    /// <typeparam name="T">业务类型</typeparam>
    public class BaseBusinessRepositoryExtend<T> : BaseBusinessRepositoryImpl<T>
    {
        /// <summary>
        /// 所有Dao层对象管理工厂
        /// </summary>
        public DataObjectManagerFactory DataObjectFactory { get; set; }
    }
}
