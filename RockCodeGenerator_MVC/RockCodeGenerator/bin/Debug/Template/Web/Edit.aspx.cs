using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using {BLL}.{ConfigNameSpace};
using {Domain}.Entities.{ConfigNameSpace};
using RockFramework.View.WebForm;

namespace {Web}.Programs.{ConfigNameSpace}
{
    public partial class {TableName}Edit : DWZBasePager<{TableName}>
    {

        public BusinessObjectManagerFactory BllObjectFactory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
             if(!IsPostBack)
             {
                 this.DefaultObject = BllObjectFactory.{TableName}Manager.Get(Request["{PKName}"]);
             }
        }
    }
}