using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SysManager;
using RockFramework;

namespace Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session[SystemConstant.CurrentUserInfo] = null;
                Session[SystemConstant.CurrentUserPageLimit] = null;
                Session[SystemConstant.CurrentUserPageFunLimit] = null;
            }
        }
    }
}