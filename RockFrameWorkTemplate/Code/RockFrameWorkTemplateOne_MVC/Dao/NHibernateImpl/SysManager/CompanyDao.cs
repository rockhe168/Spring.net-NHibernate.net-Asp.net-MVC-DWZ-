using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dao.IDao.SysManager;
using Domain.Entities.SysManager;
using RockFramework.Repository;

namespace Dao.NHibernateImpl.SysManager
{
    public class CompanyDao : NHibernateRepository<Company>, ICompanyDao
    {
       
    }
}

