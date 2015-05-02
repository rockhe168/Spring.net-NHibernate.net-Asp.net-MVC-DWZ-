using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.Repository
{
    /// <summary>
    /// 业务层顶级接口类
    /// </summary>
    /// <typeparam name="T">业务对象类型</typeparam>
    public interface IBusinessRepository<T> : IRepository<T>
    {
        RockAdoTemplate AdoTemplate { get; set; }
    }
}
