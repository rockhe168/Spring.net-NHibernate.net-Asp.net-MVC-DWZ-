using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockFramework
{
    /// <summary>
    /// 当前系统常量
    /// </summary>
    public class SystemConstant
    {

        /// <summary>
        /// 当前用户Session Object Key
        /// </summary>
        public static string CurrentUserInfo = "CurrentUserInfo";

        /// <summary>
        /// 当前页面权限Key
        /// </summary>
        public static string CurrentUserPageLimit = "CurrentUserPageLimit";

        /// <summary>
        /// 当前页面权限功能Key
        /// </summary>
        public static string CurrentUserPageFunLimit = "CurrentUserPageFunLimit";

        /// <summary>
        /// 默认GUID=36个0
        /// </summary>
        public static string DefaultGuid = "000000000000000000000000000000000000";
    }
}
