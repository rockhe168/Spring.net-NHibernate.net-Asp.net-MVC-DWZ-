using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SysManager;
using Domain.Entities.SysManager;
using RockFramework.View.WebForm;

namespace Web.Programs.SysManager
{
    public partial class UserInfoList : DWZBasePager<UserInfo>
    {
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //当前页
                if (Request["pageNum"] != null)
                {
                    DefaultListPagination.CurrentPageNo = int.Parse(Request["pageNum"]);
                }

                #region 用法一

                ////登录名
                //if (Request["UserName"] != null && !string.IsNullOrWhiteSpace(Request["UserName"]))
                //{
                //    WhereStr += " and UserName  like '%" + Request["UserName"] + "%'";
                //}

                ////登录密码
                //if (Request["UserPassword"] != null && !string.IsNullOrWhiteSpace(Request["UserPassword"]))
                //{
                //    WhereStr += " and UserPassword  like '%" + Request["UserPassword"] + "%'";
                //}

                ////真实姓名
                //if (Request["UseActurlName"] != null && !string.IsNullOrWhiteSpace(Request["UseActurlName"]))
                //{
                //    WhereStr += " and UseActurlName  like '%" + Request["UseActurlName"] + "%'";
                //}

                ////所属角色
                //if (Request["RolID"] != null && !string.IsNullOrWhiteSpace(Request["RolID"]))
                //{
                //    WhereStr += " and RolID  like '%" + Request["RolID"] + "%'";
                //}

                ////所在公司
                //if (Request["CompanyId"] != null && !string.IsNullOrWhiteSpace(Request["CompanyId"]))
                //{
                //    WhereStr += " and CompanyId  like '%" + Request["CompanyId"] + "%'";
                //}

                ////所在部门
                //if (Request["DepartmentId"] != null && !string.IsNullOrWhiteSpace(Request["DepartmentId"]))
                //{
                //    WhereStr += " and DepartmentId  like '%" + Request["DepartmentId"] + "%'";
                //}

                ////用户电话
                //if (Request["UserTel"] != null && !string.IsNullOrWhiteSpace(Request["UserTel"]))
                //{
                //    WhereStr += " and UserTel  like '%" + Request["UserTel"] + "%'";
                //}

                ////手机
                //if (Request["UserMobile"] != null && !string.IsNullOrWhiteSpace(Request["UserMobile"]))
                //{
                //    WhereStr += " and UserMobile  like '%" + Request["UserMobile"] + "%'";
                //}

                ////邮件
                //if (Request["UserEmail"] != null && !string.IsNullOrWhiteSpace(Request["UserEmail"]))
                //{
                //    WhereStr += " and UserEmail  like '%" + Request["UserEmail"] + "%'";
                //}




                ////统计中条数
                ////DefaultListPagination.TotalCount = BllObjectFactory.UserInfoManager.Count(WhereStr);
                //DefaultListPagination.TotalCount = BllObjectFactory.UserInfoManager.AdoTemplate.Count("UserInfo_SelectTotalCount", WhereStr);

                ////查询当前页
                ////DefaultList = BllObjectFactory.UserInfoManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr);
                //DefaultTable = BllObjectFactory.UserInfoManager.AdoTemplate.
                //    SelectObjectToPagination("UserInfo_SelectGoToPager", 
                //    DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr, "a."+OrderbyStr, OrderByDirection);

                #endregion

                #region 用法二 参数化

                DefaultDbParameters = BllObjectFactory.UserInfoManager.AdoTemplate.CreateDbParameters();

                //登录名
                if (Request["UserName"] != null && !string.IsNullOrWhiteSpace(Request["UserName"]))
                {
                    WhereStr += " and UserName  like @UserName";
                    DefaultDbParameters.Add("UserName", DbType.String).Value = "%"+Request["UserName"]+"%";
                }

                //真实姓名
                if (Request["UseActurlName"] != null && !string.IsNullOrWhiteSpace(Request["UseActurlName"]))
                {
                    WhereStr += " and UseActurlName  like @UseActurlName";
                    DefaultDbParameters.Add("@UseActurlName",DbType.String).Value = "%" + Request["UseActurlName"] + "%";
                }

                //手机
                if (Request["UserMobile"] != null && !string.IsNullOrWhiteSpace(Request["UserMobile"]))
                {
                    WhereStr += " and UserMobile  like @UserMobile";
                    DefaultDbParameters.Add("@UserMobile", DbType.String).Value = "%" + Request["UserMobile"] + "%";
                }



                //统计中条数
                //DefaultListPagination.TotalCount = BllObjectFactory.UserInfoManager.Count(WhereStr);
                DefaultListPagination.TotalCount = BllObjectFactory.UserInfoManager.AdoTemplate.Count("UserInfo_SelectTotalCount", WhereStr,DefaultDbParameters);

                //查询当前页
                //DefaultList = BllObjectFactory.UserInfoManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr);
                DefaultTable = BllObjectFactory.UserInfoManager.AdoTemplate.
                    SelectObjectToPagination("UserInfo_SelectGoToPager",
                    DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr, "a." + OrderbyStr, OrderByDirection,DefaultDbParameters);


                #endregion
            }
        }
    }
}

