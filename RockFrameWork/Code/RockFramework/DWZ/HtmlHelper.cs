using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockFramework.View.WebForm;

namespace RockFramework.DWZ
{
    /// <summary>
    /// 用于输出Html字符
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// 根据集合输出集合的Options字符串集合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="valuePropertyName">Option 中value要显示字段</param>
        /// <param name="textPropertyName">Option 中text要显示的字段</param>
        /// <returns></returns>
        public static string ComboxOutputOptions<T>(IList<T> list, string textPropertyName, string valuePropertyName)
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine("<option value=\"\">请选择</option>");
            try
            {
                foreach (T item in list)
                {
                    Type type = item.GetType(); ;
                    //获取对象的文本
                    string text = type.GetProperty(textPropertyName).GetValue(item, null).ToString();
                    //获取对象的值
                    string value = type.GetProperty(valuePropertyName).GetValue(item, null).ToString();

                    htmlBuilder.AppendLine("<option value=\"" + value + "\">" + text + "</option>");
                }
            }
            catch (Exception)
            {

            }
            var html = htmlBuilder.ToString();
            return html;
        }

        /// <summary>
        /// 根据集合输出集合的Options字符串集合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="valuePropertyName">Option 中value要显示属性名</param>
        /// <param name="textPropertyName">Option 中text要显示的属性名</param>
        /// <param name="selectvalue">被选中值</param>
        /// <returns></returns>
        public static string ComboxOutputOptions<T>(IList<T> list, string textPropertyName, string valuePropertyName, string selectvalue)
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine("<option value=\"\">请选择</option>");
            try
            {
                if (list != null)
                {
                    foreach (T item in list)
                    {
                        Type type = item.GetType();
                        //获取对象的文本
                        string text = type.GetProperty(textPropertyName).GetValue(item, null).ToString();
                        //获取对象的值
                        string value = type.GetProperty(valuePropertyName).GetValue(item, null).ToString();

                        if (!string.IsNullOrEmpty(selectvalue) && value.Trim().Equals(selectvalue.Trim()))
                        {
                            htmlBuilder.AppendLine("<option value=\"" + value + "\" selected=\"selected\">" + text +
                                                   "</option>");
                        }
                        else
                        {
                            htmlBuilder.AppendLine("<option value=\"" + value + "\">" + text + "</option>");
                        }
                    }
                }
            }
            catch (Exception err)
            {

            }
            var html = htmlBuilder.ToString();
            return html;
        }


        /// <summary>
        /// 根据集合输出集合的Options字符串jason集合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="valuePropertyName">Option 中value要显示字段</param>
        /// <param name="textPropertyName">Option 中text要显示的字段</param>
        /// <returns></returns>
        public static string ComboxJsonOutputOptions<T>(List<T> list, string textPropertyName, string valuePropertyName)
        {
            var htmlBuilder = new StringBuilder();

            if (list.Count == 0)
            {
                htmlBuilder.AppendLine("[[\"\", \"请选择\"]]");
            }
            else
            {
                htmlBuilder.AppendLine("[[\"\", \"请选择\"],");

                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Type type = list[i].GetType();
                        //获取对象的文本
                        string text = type.GetProperty(textPropertyName).GetValue(list[i], null).ToString();
                        //获取对象的值
                        string value = type.GetProperty(valuePropertyName).GetValue(list[i], null).ToString();

                        if (i == list.Count - 1)  //最后一个
                        {
                            htmlBuilder.AppendLine("[\"" + value + "\",\"" + text + "\"]");
                        }
                        else
                        {
                            htmlBuilder.AppendLine("[\"" + value + "\",\"" + text + "\"]" + ",");
                        }
                    }

                    htmlBuilder.AppendLine("]");
                }
                catch (Exception)
                {

                }
            }
            var html = htmlBuilder.ToString();
            return html;
        }

        /// <summary>
        /// 构造多选框，并选中已有的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y"></typeparam>
        /// <param name="list"></param>
        /// <param name="textPropertyName"></param>
        /// <param name="valuePropertyName"></param>
        /// <param name="listSelect"></param>
        /// <returns></returns>
        public static string CheckboxOutputOptions<T>(List<T> list, string textPropertyName, string valuePropertyName, List<string> listValueSelect, string CheckboxName)
        {
            //<label> <input type="checkbox" name="c1" value="1" checked="checked"/>选择1</label>

            var htmlBuilder = new StringBuilder();

            if (list.Count > 0)
            {
                try
                {
                    foreach (T t in list)
                    {
                        Type type = t.GetType();
                        //获取对象的文本
                        string text = type.GetProperty(textPropertyName).GetValue(t, null).ToString();
                        //获取对象的值
                        string value = type.GetProperty(valuePropertyName).GetValue(t, null).ToString();

                        if (listValueSelect.Exists(p => p == value))
                        {
                            htmlBuilder.AppendLine("<label><input type=\"checkbox\" name=\"" + CheckboxName + "\" value=\"" + value + "\" checked=\"checked\"/>" + text + "</label>");
                        }
                        else
                        {
                            htmlBuilder.AppendLine("<label><input type=\"checkbox\" name=\"" + CheckboxName + "\" value=\"" + value + "\"/>" + text + "</label>");
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            var html = htmlBuilder.ToString();
            return html;
        }


        #region Dwz分页部分

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
