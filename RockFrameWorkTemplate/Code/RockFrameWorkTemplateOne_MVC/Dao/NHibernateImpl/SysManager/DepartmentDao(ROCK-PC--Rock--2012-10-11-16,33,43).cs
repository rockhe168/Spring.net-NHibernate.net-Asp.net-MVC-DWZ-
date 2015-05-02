using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dao.IDao.SysManager;
using Domain.Entities.SysManager;
using RockFramework.Repository;

namespace Dao.NHibernateImpl.SysManager
{
    public class DepartmentDao : NHibernateRepository<Department>, IDepartmentDao
    {
        public override IList<Department> SelectObjectToPagination(int currentPageNo, int pageSize, string whereStr, string orderbyStr, RockFramework.Repository.Data.Direction orderByDirection)
        {
            //return base.SelectObjectToPagination(currentPageNo, pageSize, whereStr, orderbyStr, orderByDirection);
            try
            {
                string hql = string.Format("select distinct a from Department a left join a.Company b on a.Company=b.CompanyId {0} order by {1}", 
                                           whereStr.ToUpper().StartsWith("WHERE") ? whereStr : "WHERE " + whereStr, orderbyStr + " " + orderByDirection);
                IList<Department> list =
                    this.HibernateTemplate.ExecuteFind<Department>(
                        session =>
                        session.CreateQuery(hql).SetFirstResult((currentPageNo - 1) * pageSize).SetMaxResults(pageSize).
                            List<Department>());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

