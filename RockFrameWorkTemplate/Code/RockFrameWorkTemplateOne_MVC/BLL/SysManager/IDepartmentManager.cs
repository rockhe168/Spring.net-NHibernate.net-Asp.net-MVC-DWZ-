using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities.SysManager;
using RockFramework.Repository;

namespace BLL.SysManager
{
    public interface IDepartmentManager : IBusinessRepository<Department>
    {
        void TestAddTwoDepartment(Department department, Department department2);
        void TestAddDepartmentAndCompany(Department department);
        void TestAddDepartmentAndCompanyAndDaoBuildFactory(Department department);

    }
}

