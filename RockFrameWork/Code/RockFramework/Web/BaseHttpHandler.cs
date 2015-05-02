using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;
using RockFramework.DWZ.Constant;
using RockFramework.DomainModel;

namespace RockFramework.Web
{
    public class BaseHttpHandler:IHttpHandler,IRequiresSessionState
    {

        public bool IsReusable
        {
            //get { throw new NotImplementedException(); }
            get { return false; }
        }

        public  BaseHttpHandler():base()
        {
            string baseString = "one.........";

            //检查安全性、权限.........................
            
            
        }

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


        public virtual void ProcessRequest(HttpContext context)
        {

        }

        /// <summary>
        /// 通过httpRequest设置对象的属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <param name="context">上下文</param>
        /// <returns>返回对象</returns>
        public static void SetObjectPropertyValueFromHttpRequest(object obj,HttpContext context)
        {
            PropertyInfo[] propertys = obj.GetType().GetProperties();//没有保护私有的
            foreach (PropertyInfo pi in propertys)
            {
                dynamic requestValue=context.Request[pi.Name];
                //pi.SetValue(obj, requestValue, null);
                if(requestValue != null)
                {
                    //obj.GetType().GetProperty(pi.Name).SetValue(obj,requestValue,null);
                    try
                    {
                        var typeName = pi.PropertyType.FullName;
                        if (typeName.Contains("String"))
                        {
                            pi.SetValue(obj, requestValue, null);
                        }
                        else
                        {
                            if (typeName.Contains("Decimal"))
                            {
                                requestValue = decimal.Parse(requestValue);
                                pi.SetValue(obj, requestValue, null);
                            }
                            else if (typeName.Contains("Int"))
                            {
                                requestValue = int.Parse(requestValue);
                                pi.SetValue(obj, requestValue, null);
                            }
                            else if (typeName.Contains("DateTime"))
                            {
                                requestValue = DateTime.Parse(requestValue);
                                pi.SetValue(obj, requestValue, null);
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
            //return obj;
        }

        #region DWZ

        /// <summary>
        /// 输出弹出窗口需要的json数据;
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="message">提示信息</param>
        /// <param name="navTabId">刷新的选项卡标识（选项卡ID）</param>
        /// <param name="rel"></param>
        /// <param name="callbackType">返回类型（200=成功、300=操作失败、301=会话超时）</param>
        /// <param name="forwardUrl">重定向路径</param>
        /// <returns></returns>
        protected string OutPutDialogString(ResponseStatus statusCode,string message,string navTabId,string rel,CallbackType callbackType,string forwardUrl)
        {
            //string statusStr = statusCode.ToString();
            int statusInt = (int)statusCode;//获取枚举值

            string json = "{";
            json += "\"statusCode\":\"" + statusInt + "\"" + ",";
            json += "\"message\":\"" + message + "\"" + ",";
            json += "\"navTabId\":\"" + navTabId + "\"" + ",";
            json += "\"rel\":\"" + rel + "\"" + ",";
            json += "\"callbackType\":\"" + callbackType + "\"" + ",";
            json += "\"forwardUrl\":\"" + forwardUrl + "\"";
            json += "}";

            return json;

        }

        #endregion


    }
}
