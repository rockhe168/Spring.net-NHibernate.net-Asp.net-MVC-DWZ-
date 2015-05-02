using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using RockFramework.DWZ.Constant;
using RockFramework.DomainModel;
using RockFramework.Repository.Data;
using Spring.Data.Common;

namespace RockFramework.View.WebForm
{
    public class DWZBasePager<T> : BasePager
    {
        private Entity _currentUser;
        public Entity CurrentUser
        {
            get
            {
                _currentUser = HttpContext.Current.Session[SystemConstant.CurrentUserInfo] as Entity;
                return _currentUser;
            }
            set { _currentUser = value; }
        }

        //页面初始化，赋值
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //检查会话是否过期
            //CheckUserUserSession();

            //当前页
            if (Request["pageNum"] != null)
                DefaultListPagination.CurrentPageNo = int.Parse(Request["pageNum"] ?? "1");

            //页大小
            if (Request["numPerPage"] != null)
                DefaultListPagination.PageSize = int.Parse(Request["numPerPage"] ?? "1");

            //排序
            if (!String.IsNullOrEmpty(Request["orderField"]))
            {
                OrderbyStr = Request["orderField"];

                var direction = Request["orderDirection"];

                if (!String.IsNullOrEmpty(direction) && direction.ToLower() == "asc")
                {
                    OrderByDirection = Direction.Asc;
                }
                else if (!String.IsNullOrEmpty(direction) && direction.ToLower() == "desc")
                {
                    OrderByDirection = Direction.Desc;
                }

            }
        }

        /// <summary>
        /// 检查页面功能按钮的权限
        /// </summary>
        /// <param name="funType"></param>
        /// <returns></returns>
        protected string CheckFunOutDisplay(FunType funType)
        {
            //先放开
            return string.Empty;

            string result = "style='display: none'";
            string pagePath = HttpContext.Current.Request.CurrentExecutionFilePath.Substring(1);
            int startIndex = pagePath.LastIndexOf('/') + 1;
            int length = pagePath.LastIndexOf('.') - startIndex;
            string pageName = pagePath.Substring(startIndex, length);
            var funLimitList = Session[SystemConstant.CurrentUserPageFunLimit] as IEnumerable<FunLimitEntity>;

            if (funLimitList != null && funLimitList.Count(p => p.ParentFunCode.Equals(pageName)) > 0)
            {
                if (funLimitList.Count(p => p.FunCode.Equals(funType.ToString())) > 0)
                    result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 检查页面功能按钮的权限
        /// </summary>
        /// <param name="funType">功能联系</param>
        /// <param name="parentCode">页面</param>
        /// <returns></returns>
        protected bool IsExistsPageFun(FunType funType, SysPageList parentCode)
        {
            bool result = false;

            var funLimitList = Session[SystemConstant.CurrentUserPageFunLimit] as IEnumerable<FunLimitEntity>;

            if (funLimitList != null && funLimitList.Count(p => p.ParentFunCode.Equals(parentCode.ToString())) > 0)
            {
                if (funLimitList.Count(p => p.FunCode.Equals(funType.ToString())) > 0)
                    result = true;
            }
            return result;
        }


        private Pagination _pagination;
        /// <summary>
        /// 当前页面分页对象
        /// </summary>
        public Pagination DefaultListPagination
        {
            get
            {
                if (_pagination == null)
                {
                    _pagination = new Pagination();
                    return _pagination;
                }
                else
                {
                    return _pagination;
                }

            }
            set { _pagination = value; }
        }

        /// <summary>
        /// 默认列表分页对象
        /// </summary>
        public IList<T> DefaultList { get; set; }

        /// <summary>
        /// 默认列表分页DatatTable
        /// </summary>
        public DataTable DefaultTable { get; set; }



        public T _defaultObject = default(T);

        /// <summary>
        /// 默页面认对象
        /// </summary>
        public T DefaultObject
        {
            get
            {
                if (_defaultObject == null)
                {
                    _defaultObject =
                        (T)System.Reflection.Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());
                }
                return _defaultObject;
            }
            set { _defaultObject = value; }
        }

        private string _wherestring = string.Empty;
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string WhereStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_wherestring))
                {
                    return "where 1=1";
                }
                else
                {
                    return _wherestring;
                }
            }
            set { _wherestring = value; }
        }

        private string _orderbystring = string.Empty;
        /// <summary>
        /// 排序字符串
        /// </summary>
        public string OrderbyStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_orderbystring))
                {
                    return "CreateTime";
                }
                else
                {
                    return _orderbystring;
                }
            }
            set { _orderbystring = value; }
        }

        private Direction _orderDirection = Direction.Desc;

        /// <summary>
        /// 排序方向
        /// </summary>
        public Direction OrderByDirection
        {
            get
            {
                return _orderDirection;
            }
            set { _orderDirection = value; }
        }

        /// <summary>
        /// 根据当前列名显示列头样式
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string ShowColumnHeaderOrderByDirection(string columnName)
        {
            return OrderbyStr.Equals(columnName) ? "class='" + OrderByDirection.ToString().ToLower() + "'" : "";
        }

        private Dictionary<string, HqlParameter> _defaultHqlParameterDictionary;
        /// <summary>
        /// 查询Hql参数字典【用于NHibernate Hql 查询】
        /// </summary>
        public Dictionary<string, HqlParameter> DefaultHqlParameterDictionary
        {
            get
            {
                if (_defaultHqlParameterDictionary == null)
                {
                    _defaultHqlParameterDictionary = new Dictionary<string, HqlParameter>();
                    return _defaultHqlParameterDictionary;
                }
                else
                {
                    return _defaultHqlParameterDictionary;
                }
            }
            set { this._defaultHqlParameterDictionary = value; }
        }

        private IDbParameters _defaultDbParameters;
        public IDbParameters DefaultDbParameters
        {
            get
            {
                if (_defaultDbParameters == null)
                {
                    //_defaultDbParameters = 
                    return _defaultDbParameters;
                }
                else
                {
                    return _defaultDbParameters;
                }
            }
            set { this._defaultDbParameters = value; }
        }

        #region DWZ Helper

        /// <summary>
        /// 分页导航(默认为当前页页面列表、针对页码只有一个列表页)
        /// </summary>
        /// <returns></returns>
        public string OutPutPagerNavigation()
        {
            var pager = new StringBuilder();
            pager.Append("<div class=\"panelBar\">");
            pager.Append("<div class=\"pages\">");
            pager.Append("<span>显示</span>");
            pager.Append("<select name=\"numPerPage\" onchange=\"navTabPageBreak({numPerPage:this.value})\">");
            switch (DefaultListPagination.PageSize)
            {
                case 20:
                    pager.Append("<option value=\"20\" selected=\"selected\">20</option>");
                    pager.Append("<option value=\"50\">50</option>");
                    pager.Append("<option value=\"100\">100</option>");
                    break;
                case 50:
                    pager.Append("<option value=\"20\">20</option>");
                    pager.Append("<option value=\"50\" selected=\"selected\">50</option>");
                    pager.Append("<option value=\"100\">100</option>");
                    break;
                case 100:
                    pager.Append("<option value=\"20\">20</option>");
                    pager.Append("<option value=\"50\">50</option>");
                    pager.Append("<option value=\"100\" selected=\"selected\">100</option>");
                    break;
            }
            pager.Append("</select>");
            pager.Append("<span>条，总共" + DefaultListPagination.TotalCount + "条，每页" + DefaultListPagination.PageSize + "条，当前第" + DefaultListPagination.CurrentPageNo + "页</span>");
            pager.Append("</div>");
            pager.Append("<div class=\"pagination\" targetType=\"navTab\" totalCount=\"" + DefaultListPagination.TotalCount + "\" numPerPage=\"" + DefaultListPagination.PageSize + "\" pageNumShown=\"10\" currentPage=\"" + DefaultListPagination.CurrentPageNo + "\"></div>");
            pager.Append("</div>");
            return pager.ToString();
        }
        /// <summary>
        /// 分页导航（针对页码有多个列表页）
        /// </summary>
        /// <param name="page">列表分页对象</param>
        /// <returns></returns>
        public static string OutPutPagerNavigation(Pagination page)
        {
            var pager = new StringBuilder();
            pager.Append("<div class=\"panelBar\">");
            pager.Append("<div class=\"pages\">");
            pager.Append("<span>显示</span>");
            pager.Append("<select name=\"numPerPage\" onchange=\"navTabPageBreak({numPerPage:this.value})\">");
            switch (page.PageSize)
            {
                case 20:
                    pager.Append("<option value=\"20\" selected=\"selected\">20</option>");
                    pager.Append("<option value=\"50\">50</option>");
                    pager.Append("<option value=\"100\">100</option>");
                    break;
                case 50:
                    pager.Append("<option value=\"20\">20</option>");
                    pager.Append("<option value=\"50\" selected=\"selected\">50</option>");
                    pager.Append("<option value=\"100\">100</option>");
                    break;
                case 100:
                    pager.Append("<option value=\"20\">20</option>");
                    pager.Append("<option value=\"50\">50</option>");
                    pager.Append("<option value=\"100\" selected=\"selected\">100</option>");
                    break;
            }
            pager.Append("</select>");
            pager.Append("<span>条，总共" + page.TotalCount + "条，每页" + page.PageSize + "条，当前第" + page.CurrentPageNo + "页</span>");
            pager.Append("</div>");
            pager.Append("<div class=\"pagination\" targetType=\"navTab\" totalCount=\"" + page.TotalCount + "\" numPerPage=\"" + page.PageSize + "\" pageNumShown=\"10\" currentPage=\"" + page.CurrentPageNo + "\"></div>");
            pager.Append("</div>");
            return pager.ToString();
        }

        /// <summary>
        /// 分页导航（针对页码有多个列表页）
        /// </summary>
        /// <param name="page">列表分页对象</param>
        /// <param name="pageRenderingType"> 渲染分页类型[NavTab=选项卡形式，Dialog=弹出框的分页模式]</param>
        /// <returns></returns>
        public static string OutPutPagerNavigation(Pagination page, PagePaginationRenderingType pageRenderingType)
        {
            var pager = new StringBuilder();
            pager.Append("<div class=\"panelBar\">");
            pager.Append("<div class=\"pages\">");
            pager.Append("<span>显示</span>");

            if (pageRenderingType == PagePaginationRenderingType.NavTab)
            {
                pager.Append("<select name=\"numPerPage\" onchange=\"navTabPageBreak({numPerPage:this.value})\">");
            }
            else if (pageRenderingType == PagePaginationRenderingType.Dialog)
            {
                pager.Append("<select name=\"numPerPage\" onchange=\"dialogPageBreak({numPerPage:this.value})\">");
            }
            //pager.Append("<select name=\"numPerPage\" onchange=\"navTabPageBreak({numPerPage:this.value})\">");
            switch (page.PageSize)
            {
                case 20:
                    pager.Append("<option value=\"20\" selected=\"selected\">20</option>");
                    pager.Append("<option value=\"50\">50</option>");
                    pager.Append("<option value=\"100\">100</option>");
                    break;
                case 50:
                    pager.Append("<option value=\"20\">20</option>");
                    pager.Append("<option value=\"50\" selected=\"selected\">50</option>");
                    pager.Append("<option value=\"100\">100</option>");
                    break;
                case 100:
                    pager.Append("<option value=\"20\">20</option>");
                    pager.Append("<option value=\"50\">50</option>");
                    pager.Append("<option value=\"100\" selected=\"selected\">100</option>");
                    break;
            }
            pager.Append("</select>");
            pager.Append("<span>条，总共" + page.TotalCount + "条，每页" + page.PageSize + "条，当前第" + page.CurrentPageNo + "页</span>");
            pager.Append("</div>");
            switch (pageRenderingType)
            {
                case PagePaginationRenderingType.NavTab:
                    pager.Append("<div class=\"pagination\" targetType=\"navTab\" totalCount=\"" + page.TotalCount + "\" numPerPage=\"" + page.PageSize + "\" pageNumShown=\"10\" currentPage=\"" + page.CurrentPageNo + "\"></div>");
                    break;
                case PagePaginationRenderingType.Dialog:
                    pager.Append("<div class=\"pagination\" targetType=\"dialog\" totalCount=\"" + page.TotalCount + "\" numPerPage=\"" + page.PageSize + "\" pageNumShown=\"10\" currentPage=\"" + page.CurrentPageNo + "\"></div>");
                    break;
            }
            pager.Append("</div>");
            return pager.ToString();
        }

        #endregion
    }
}
