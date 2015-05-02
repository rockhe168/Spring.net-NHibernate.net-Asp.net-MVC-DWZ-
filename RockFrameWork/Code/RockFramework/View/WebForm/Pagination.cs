using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.View.WebForm
{

    /// <summary>
    /// 用于当前页面列表分页
    /// </summary>
    public class Pagination
    {
        private int pageSize;
        /// <summary>
        /// 当前页面列表每页多少条
        /// </summary>
        public int PageSize
        {
            get
            {
                if (pageSize == 0)
                    return 20;//默认每页20条
                else
                    return pageSize;
            }
            set { pageSize = value; }
        }

        private int currentPageNo;
        /// <summary>
        ///当前列表当前页页码
        /// </summary>
        public int CurrentPageNo
        {
            get
            {
                if (currentPageNo == 0)
                    return 1;//默认第一页
                else
                    return currentPageNo;
            }
            set { currentPageNo = value; }
        }

        private int totalCount;
        /// <summary>
        /// 当前列表总条数
        /// </summary>
        public int TotalCount
        {
            get
            {
                if (totalCount == 0)
                    return 0;
                else
                    return totalCount;
            } 
            set { totalCount = value; }
        }

    }

    /// <summary>
    /// 页面分页渲染类型
    /// </summary>
    public enum PagePaginationRenderingType
    {
        NavTab,//选项卡形式
        Dialog//弹出窗口分页形式
    }
}
