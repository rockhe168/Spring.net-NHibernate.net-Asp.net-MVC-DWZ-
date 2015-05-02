using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SysManager;
using RockFramework.View.WebForm;
using Domain.Entities.SysManager;

namespace Web.Programs.SysManager
{
    public partial class CompanyAdd : DWZBasePager<Company>
    {

        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}

