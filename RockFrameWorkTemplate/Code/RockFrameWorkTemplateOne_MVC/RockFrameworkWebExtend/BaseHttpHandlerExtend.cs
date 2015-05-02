using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using RockFramework.View.WebForm;
using RockFramework.Web;

namespace RockFrameworkWebExtend
{
    public class BaseHttpHandlerExtend:BaseHttpHandler
    {
        /// <summary>
        /// BLL 工厂
        /// </summary>
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
    }
}
