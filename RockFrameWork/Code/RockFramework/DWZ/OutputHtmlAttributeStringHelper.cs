using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework.DWZ
{

    /// <summary>
    /// 用于输出Html标签属性字符串
    /// </summary>
    public  class OutputHtmlAttributeStringHelper
    {

        /// <summary>
        /// 输出CheckBox是否选中
        /// </summary>
        /// <param name="checkedstate">是否选中</param>
        /// <returns>输出是否选中html字符串</returns>
        public  static  string OutPutCheckBoxChecked(bool checkedstate)
        {
            if (checkedstate)
                return "checked='checked'";
            else
                return string.Empty;
        }

        /// <summary>
        /// 输出下拉框是否选中
        /// </summary>
        /// <param name="checkedstate">是否选中</param>
        /// <returns>输出是否选中html字符串</returns>
        public static string OutPutSelectChecked(bool selectstate)
        {
            if (selectstate)
                return "selected";
            else
                return string.Empty;
        }

        /// <summary>
        /// 输出Display='none'
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public  static  string OutPutDisabled(bool state)
        {
            if (state)
                return "disabled='disabled'";
            else
                return string.Empty;
        }

        public  static  string OutPutDisplayNone(bool state)
        {
            if (state)
                return "style='display: none'";
            else
                return string.Empty;
        }

    }
}
