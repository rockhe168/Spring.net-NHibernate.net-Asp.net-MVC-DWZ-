using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.;
using Domain.Entities.;
using RockFramework.View.WebForm;

namespace Web.Programs.
{
    public partial class CompanyList   : DWZBasePager<Company>
    {
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
             if(!IsPostBack)
             {
                     //当前页
                     if (Request["pageNum"] != null)
                     {
                         DefaultListPagination.CurrentPageNo = int.Parse(Request["pageNum"]);
                     }

                     #region 用法一
                     
                     //
if (Request["Name"] != null && !string.IsNullOrWhiteSpace(Request["Name"]))
{
WhereStr += " and Name  like '%" + Request["Name"]+"%'";
}

//
if (Request["Description"] != null && !string.IsNullOrWhiteSpace(Request["Description"]))
{
WhereStr += " and Description  like '%" + Request["Description"]+"%'";
}


                     

                     //统计中条数
                     DefaultListPagination.TotalCount = BllObjectFactory.CompanyManager.Count(WhereStr);

                     //查询当前页
                     DefaultList = BllObjectFactory.CompanyManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr);
                     #endregion
                 
             }
        }
    }
}

