using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BLL;

namespace RockFrameworkWebExtend
{
    public class BaseController : Controller
    {
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
    }
}
