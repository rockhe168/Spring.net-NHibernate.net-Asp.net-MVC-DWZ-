using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SysManager;
using Domain.Entities.SysManager;
using RockFramework.Repository.Data;
using RockFramework.View.WebForm;

namespace Web.Programs.SysManager
{
    public partial class CompanyList : DWZBasePager<Company>
    {
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 用法一

                ////机构名称
                //if (Request["AuditDate"] != null && !string.IsNullOrWhiteSpace(Request["AuditDate"]))
                //{
                //    WhereStr += " and Name  like '" + Request["Name"] + "'";
                //}

                ////描述
                //if (Request["AuditDate"] != null && !string.IsNullOrWhiteSpace(Request["AuditDate"]))
                //{
                //    WhereStr += " and Description  like '" + Request["Description"] + "'";
                //}

                ////统计中条数
                //DefaultListPagination.TotalCount = BllObjectFactory.CompanyManager.Count(WhereStr);

                ////查询当前页
                //DefaultList = BllObjectFactory.CompanyManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr);
                #endregion

                #region 用法二、 参数化查询

                //查询
                if (Request["companyName"] != null && !string.IsNullOrWhiteSpace(Request["companyName"]))
                {
                    WhereStr += " and companyName like :companyName";
                    DefaultHqlParameterDictionary.Add("companyName", new HqlParameter(DataType.String, Request["companyName"], ExpressionType.Like));
                }

                //地址
                if (Request["address"] != null && !string.IsNullOrWhiteSpace(Request["address"]))
                {
                    WhereStr += " and address like :address";
                    DefaultHqlParameterDictionary.Add("address", new HqlParameter(DataType.String, Request["address"], ExpressionType.LinkStart));
                }

                //城市
                if (Request["cityName"] != null && !string.IsNullOrWhiteSpace(Request["cityName"]))
                {
                    WhereStr += " and cityName=:cityName";
                    DefaultHqlParameterDictionary.Add("cityName", new HqlParameter(DataType.String, Request["cityName"]));
                }

                //统计中条数
                DefaultListPagination.TotalCount = BllObjectFactory.CompanyManager.Count(WhereStr, DefaultHqlParameterDictionary);

                //查询当前页
                DefaultList = BllObjectFactory.CompanyManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr, DefaultHqlParameterDictionary);

                #endregion

            }
        }
    }
}

