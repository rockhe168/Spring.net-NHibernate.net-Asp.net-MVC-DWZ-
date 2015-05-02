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
    public partial class {TableName}List   : DWZBasePager<{TableName}>
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
                     
                     {PropertySearchListStr}
                     

                     //统计中条数
                     DefaultListPagination.TotalCount = BllObjectFactory.{TableName}Manager.Count(WhereStr);

                     //查询当前页
                     DefaultList = BllObjectFactory.{TableName}Manager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr);
                     #endregion
                 
             }
        }
    }
}