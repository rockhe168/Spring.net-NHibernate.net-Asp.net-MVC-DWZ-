using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.ggg;
using Domain.Entities.ggg;
using RockFramework.View.WebForm;

namespace Web.Programs.ggg
{
    public partial class DepartmentEdit : DWZBasePager<Department>
    {

        public BusinessObjectManagerFactory BllObjectFactory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
             if(!IsPostBack)
             {
                 this.DefaultObject = BllObjectFactory.DepartmentManager.Get(Request["DepartmentId"]);
             }
        }
    }
}

