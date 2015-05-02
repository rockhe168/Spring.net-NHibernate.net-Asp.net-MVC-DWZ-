using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dao.IDao.SysManager;//{SpaceTag}

using RockFramework.Repository;

namespace Dao
{
    /// <summary>
    /// 数据处理对象管理工厂
    /// </summary>
    public class DataObjectManagerFactory : IObjectManagerFactory
    {
        /// <summary>
        /// DepartmentDao
        /// </summary>
        public IDepartmentDao DepartmentDao { get; set; }

        /// <summary>
        /// UserInfoDao
        /// </summary>
        public IUserInfoDao UserInfoDao { get; set; }

        /// <summary>
        /// CompanyDao
        /// </summary>
        public ICompanyDao CompanyDao { get; set; }

        //{Tag}



    }
}









