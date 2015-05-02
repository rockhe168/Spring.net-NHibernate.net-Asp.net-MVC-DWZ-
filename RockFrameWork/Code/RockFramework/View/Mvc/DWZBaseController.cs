using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using RockFramework.Repository.Data;
using RockFramework.View.WebForm;

namespace RockFramework.View.Mvc
{
    public class DWZBaseController<T> :BaseController
    {

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
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
        /// 每个操作方法[Action]都会执行完后都会调用此事件
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            //获取Action的名称
            var actionName = filterContext.ActionDescriptor.ActionName;

            //初始化视图需要的数据【如果Action的类型是查询列表、则初始化分页及查询相关信息】
            if (actionName.Contains("Index") || actionName.Contains("List"))
            {

                InitViewData();

                
            }
        }

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
        /// 默认列表分页对象
        /// </summary>
        public IList<T> DefaultList { get; set; }

        /// <summary>
        /// 默认列表分页DatatTable
        /// </summary>
        public DataTable DefaultTable { get; set; }


        /// <summary>
        /// 初始化视图需要的数据集合
        /// </summary>
        private void InitViewData()
        {
            //分页相关信息
            ViewBag.Pagination = OutPutPagerNavigation();
            ViewBag.PageSize = this.DefaultListPagination.PageSize;
            ViewBag.OrderbyStr = OrderbyStr;
            ViewBag.OrderByDirection = OrderByDirection;

            //查询条件相关信息
            PropertyInfo[] propertys = DefaultObject.GetType().GetProperties();//没有保护私有的
            foreach (PropertyInfo pi in propertys)
            {
                dynamic requestValue = Request[pi.Name];
                //pi.SetValue(obj, requestValue, null);
                if (requestValue != null)
                {
                    ViewData[pi.Name] = requestValue;
                }
            }
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
