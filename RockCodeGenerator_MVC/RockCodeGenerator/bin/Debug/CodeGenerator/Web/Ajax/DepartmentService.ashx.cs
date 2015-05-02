using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using BLL.;
using Domain.Entities.;
using RockFramework;
using RockFramework.DWZ.Constant;
using RockFramework.Web;

namespace Web.Ajax.
{
    /// <summary>
    /// UserInfoService 的摘要说明
    /// </summary>
    public class DepartmentService : BaseHttpHandler
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
                var model = new Department();
                SetObjectPropertyValueFromHttpRequest(model, context);
                BllObjectFactory.DepartmentManager.Save(model);

                result = this.OutPutDialogString(ResponseStatus.Success, "新增成功", "DepartmentList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "新增失败", "DepartmentList", string.Empty,
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
                string ids = context.Request["DepartmentId"];

                //此写法还是可能存在SQL注入？
                BllObjectFactory.DepartmentManager.Delete("where Id in(" + ids + ")");//此处可以用Id名称、也可以用数据库库中的列名DepartmentId

                result = this.OutPutDialogString(ResponseStatus.Success, "删除成功", "DepartmentList", string.Empty,CallbackType.forward, string.Empty);

            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "删除失败", "DepartmentList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
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
                var model = BllObjectFactory.DepartmentManager.Get(context.Request["DepartmentId"]);
                
                SetObjectPropertyValueFromHttpRequest(model, context);                

                BllObjectFactory.DepartmentManager.Update(model);

                result = this.OutPutDialogString(ResponseStatus.Success, "修改成功", "DepartmentList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }
            catch (Exception)
            {
                result = this.OutPutDialogString(ResponseStatus.Fail, "修改失败", "DepartmentList", string.Empty,
                                                 CallbackType.closeCurrent, string.Empty);
            }

            context.Response.Write(result);
        }

       
    }
}

