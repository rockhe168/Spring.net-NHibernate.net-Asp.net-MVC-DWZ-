using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using {BLL}.{ConfigNameSpace};
using RockFramework.View.WebForm;
using {Domain}.Entities.{ConfigNameSpace};

namespace {Web}.Programs.{ConfigNameSpace}
{
    public partial class {TableName}Add : DWZBasePager<{TableName}>
    {

        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}