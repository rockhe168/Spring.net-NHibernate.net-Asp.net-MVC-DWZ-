using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SysManager;
using BLL;
using Domain.Entities.SysManager;
using RockFramework.Repository.Data;
using RockFramework.View.WebForm;

namespace Web.Programs.SysManager
{
    public partial class DepartmentList : DWZBasePager<Department>
    {
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                #region 用法一

                //所属机构
                if (Request["AuditDate"] != null && !string.IsNullOrWhiteSpace(Request["AuditDate"]))
                {
                    WhereStr += " and Company  like '" + Request["Company"] + "'";
                }

                //部门名称
                if (Request["AuditDate"] != null && !string.IsNullOrWhiteSpace(Request["AuditDate"]))
                {
                    WhereStr += " and Name  like '" + Request["Name"] + "'";
                }

                //部门代码
                if (Request["AuditDate"] != null && !string.IsNullOrWhiteSpace(Request["AuditDate"]))
                {
                    WhereStr += " and Code  like '" + Request["Code"] + "'";
                }

                //描述
                if (Request["AuditDate"] != null && !string.IsNullOrWhiteSpace(Request["AuditDate"]))
                {
                    WhereStr += " and Description  like '" + Request["Description"] + "'";
                }

                //统计中条数
                DefaultListPagination.TotalCount = BllObjectFactory.DepartmentManager.Count(WhereStr);


                //查询当前页
                DefaultList = BllObjectFactory.DepartmentManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr,OrderbyStr,OrderByDirection);
               
                #endregion

                #region 用法二 参数化查询

                #endregion

            }
        }
    }
}

