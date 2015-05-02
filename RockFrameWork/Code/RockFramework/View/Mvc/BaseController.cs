using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RockFramework.View.Mvc
{
    public class BaseController:Controller
    {
        /// <summary>
        /// 通过httpRequest设置对象的属性值
        /// </summary>
        /// <param name="obj">对象实例</param>
        /// <param name="collection">上下文集合</param>
        /// <returns>返回对象</returns>
        public static void SetObjectPropertyValueFromFormCollection(object obj, FormCollection collection)
        {
            PropertyInfo[] propertys = obj.GetType().GetProperties();//没有保护私有的
            foreach (PropertyInfo pi in propertys)
            {
                dynamic requestValue = collection[pi.Name];
                //pi.SetValue(obj, requestValue, null);
                if (requestValue != null)
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
    }
}
