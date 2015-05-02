using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RockFramework.DomainModel;
using RockFramework.Repository.Data;

namespace RockFramework.View.WebForm
{
    /// <summary>
    /// 用于前端页面基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public  class ClientDWZBasePager<T>:System.Web.UI.Page
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

        //检查用户是否会话过期
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (CurrentUser == null)
            {
                //提示过期，并跳转到登录页面
                //Response.Write("<script type='text/javascript'>SessionOut();</script>");
                //Response.End();
                Session.Abandon();
                Response.Write("<script language='javascript'>alert('您登录已超时，请重新登录！');top.location.href = 'MemberLogin.aspx';</script>");
                Response.End();
            }
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
                    return string.Empty;
                }
                else
                {
                    return _orderbystring;
                }
            }
            set { _orderbystring = value; }
        }


        private Dictionary<string, HqlParameter> _defaultHqlParameterDictionary;
        /// <summary>
        /// 查询Hql参数字典
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

        #endregion
    }
}
