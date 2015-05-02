using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using RockFramework.View.WebForm;

namespace RockFrameworkWebExtend
{
    public class DWZBasePagerExtend<T> : DWZBasePager<T>
    {
        /// <summary>
        /// BLL 对象工厂
        /// </summary>
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
    }
}
