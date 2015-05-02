using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.SysManager;//{SpaceTag}

using RockFramework.Repository;

namespace BLL
{
    /// <summary>
    /// 业务层对象管理工厂类
    /// </summary>
    public class BusinessObjectManagerFactory : IObjectManagerFactory
    {
         /// <summary>
/// UserInfoManager
/// </summary>
public IUserInfoManager UserInfoManager { get; set; }

 /// <summary>
/// CompanyManager
/// </summary>
public ICompanyManager CompanyManager { get; set; }

 /// <summary>
/// DepartmentManager
/// </summary>
public IDepartmentManager DepartmentManager { get; set; }

//{Tag}



    }
}












