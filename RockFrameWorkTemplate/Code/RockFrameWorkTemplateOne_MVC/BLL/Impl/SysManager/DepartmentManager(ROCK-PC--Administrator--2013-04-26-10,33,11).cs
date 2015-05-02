using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.SysManager;
using Domain.Entities.SysManager;
using RockFramework.Repository;
using RockFramework.Repository.Data;

namespace BLL.Impl.SysManager
{
    public class DepartmentManager : BaseBusinessRepositoryExtend<Department>, IDepartmentManager
    {

        //public ICompanyManager CompanyManager { get; set; }
        /// <summary>
        /// 测试同时添加两个部门，后一个失败，看事物回滚情况          【测试事务有效，成功回滚】
        /// </summary>
        /// <param name="department"></param>
        /// <param name="department2"> </param>
        public void TestAddTwoDepartment(Department department,Department department2)
        {
            
            
            this.CurrentDao.Save(department);

            //让其长度大约数据库指定长度
            department2.Code = "一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一一";
            department2.Code = department2.Code + department2.Code;
            int length= department2.Code.Length;

            this.CurrentDao.Save(department2);

            

        }

        /// <summary>
        /// 测试同时添加部门与公司，公司添加失败，看事物回滚情况   【测试事务有效，成功回滚】
        /// </summary>
        /// <param name="department"></param>
        public void TestAddDepartmentAndCompany(Department department)
        {
            this.CurrentDao.Save(department);

            var company = new Company {Name = null, Description = "一二三"};
   

            this.DataObjectFactory.CompanyDao.Save(company);
        }

        /// <summary>
        /// 测试同时添加部门与公司，公司添加失败，看事物回滚情况 [Dao层也加入对象工厂]  【测试事务有效，成功回滚】
        /// </summary>
        /// <param name="department"></param>
        public void TestAddDepartmentAndCompanyAndDaoBuildFactory(Department department)
        {
            //this.CurrentDao.Save(department);

            this.DataObjectFactory.DepartmentDao.Save(department);

            var company = new Company { Name = department.Name, Description = "一二三" };

            this.DataObjectFactory.CompanyDao.Save(company);

        }

        public void hello()
        {
            //this.AdoTemplate.
        }
    }
}

