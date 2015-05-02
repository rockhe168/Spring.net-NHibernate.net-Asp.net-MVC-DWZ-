using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SysManager;
using Domain.Entities.SysManager;
using RockFramework.View.WebForm;
using BLL;

namespace Web.Programs.SysManager
{
    public partial class DepartmentEdit : DWZBasePager<Department>
    {

        public BusinessObjectManagerFactory BllObjectFactory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DefaultObject =BllObjectFactory.DepartmentManager.Get(Request["DepartmentId"]);
            }
        }
    }
}

