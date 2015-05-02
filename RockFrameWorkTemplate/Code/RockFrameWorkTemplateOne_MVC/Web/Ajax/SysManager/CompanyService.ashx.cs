using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using BLL.SysManager;
using Domain.Entities.SysManager;
using RockFramework;
using RockFramework.DWZ.Constant;
using RockFramework.Web;

namespace Web.Ajax.SysManager
{
    /// <summary>
    /// UserInfoService 的摘要说明
    /// </summary>
    public class CompanyService : BaseHttpHandler
    {

        public BusinessObjectManagerFactory BllObjectFactory { get; set; }

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //检查安全性

            //逻辑代码
            string actionMethod = context.Request["action"] ?? string.Empty;
            switch (actionMethod)
            {
                case "Add":
                    Add(context);
                    break;
                case "Update":
                    Update(context);
                    break;
                case "Delete":
                    Delete(context);
                    break;
                case "DeleteByPk":
                    DeleteByPk(context);
                    break;
                    
            }

        }
        
        /// <summary>
        /// 新增
        /// </summary>
        private void Add(HttpContext context)
        {
            string result;
            try
            {
                var model = new Company();
                SetObjectPropertyValueFromHttpRequest(model, context);
                BllObjectFactory.CompanyManager.Save(model);

                result = this.OutPutDialogString(ResponseStatus.Success, "新增成功", "CompanyList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "新增失败", "CompanyList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }

            context.Response.Write(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete(HttpContext context)
        {
            string result;
            try
            {
                string ids = context.Request["CompanyId"];

                //此写法还是可能存在SQL注入？
                BllObjectFactory.CompanyManager.Delete("where Id in(" + ids + ")");//此处可以用Id名称、也可以用数据库库中的列名DepartmentId

                result = this.OutPutDialogString(ResponseStatus.Success, "删除成功", "CompanyList", string.Empty,CallbackType.forward, string.Empty);

            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "删除失败", "CompanyList", string.Empty,
                                                 CallbackType.forward, string.Empty);
            }

            context.Response.Write(result);
        }

        /// <summary>
        /// 根据主键进行删除
        /// </summary>
        /// <param name="context"></param>
        private void DeleteByPk(HttpContext context)
        {
            string result;
            try
            {
                string id = context.Request["CompanyId"];

                //此写法还是可能存在SQL注入？
                BllObjectFactory.CompanyManager.Delete("where Id = '" + id + "'");//此处可以用Id名称、也可以用数据库库中的列名DepartmentId

                //此处应该要用formward，还不是closeCurrent，应该dialogAjaxDone 有这样一段代码，如下：
                //if ("closeCurrent" == json.callbackType) {
			    //$.pdialog.closeCurrent();
		        //}
                //还提示框则是有alertMsg对象产生，所以请细看dwz源码才能明白
                result = this.OutPutDialogString(ResponseStatus.Success, "删除成功", "CompanyList", string.Empty, CallbackType.forward, string.Empty);

            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "删除失败", "CompanyList", string.Empty,
                                                 CallbackType.forward, string.Empty);
            }

            context.Response.Write(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void Update(HttpContext context)
        {
            string result;
            try
            {
                var model = BllObjectFactory.CompanyManager.Get(context.Request["CompanyId"]);

                SetObjectPropertyValueFromHttpRequest(model, context);

                BllObjectFactory.CompanyManager.Update(model);

                result = this.OutPutDialogString(ResponseStatus.Success, "修改成功", "CompanyList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "修改失败", "CompanyList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }

            context.Response.Write(result);
        }

       
    }
}

