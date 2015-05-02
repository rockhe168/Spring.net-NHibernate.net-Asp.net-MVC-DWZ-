using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Domain.Entities.SysManager;
using RockFramework.DWZ.Constant;
using RockFramework.View.Mvc;

namespace MvcWeb.Controllers.SysManager
{
    public class CompanyController : DWZBaseController<Company>
    {
        public BusinessObjectManagerFactory BllObjectFactory { get; set; }
        //
        // GET: /Company/

        public ActionResult Index()
        {
            //机构名称
            if (Request["Name"] != null && !string.IsNullOrWhiteSpace(Request["Name"]))
            {
                WhereStr += " and Name  like '%" + Request["Name"] + "%'";
            }

            //描述
            if (Request["Description"] != null && !string.IsNullOrWhiteSpace(Request["Description"]))
            {
                WhereStr += " and Description  like '%" + Request["Description"] + "%'";
            }

            //统计中条数
            DefaultListPagination.TotalCount = BllObjectFactory.CompanyManager.Count(WhereStr);

            //查询当前页
            DefaultList = BllObjectFactory.CompanyManager.SelectObjectToPagination(DefaultListPagination.CurrentPageNo, DefaultListPagination.PageSize, WhereStr);


            return View("~/Views/SysManager/Company/Index.cshtml", DefaultList);
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(int id)
        {
            try
            {
                var model = BllObjectFactory.CompanyManager.Get(Request["CompanyId"]);

                return View("~/Views/SysManager/Company/Details.cshtml", model);
            }
            catch (Exception)
            {
                return Json(new { statusCode = ResponseStatus.Fail, message = "修改失败", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.closeCurrent.ToString(), forwardUrl = string.Empty });
            }
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View("~/Views/SysManager/Company/Create.cshtml");
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var model = new Company();

                SetObjectPropertyValueFromFormCollection(model,collection);

                BllObjectFactory.CompanyManager.Save(model);

                return Json(new { statusCode = ResponseStatus.Success, message = "新增成功", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.closeCurrent.ToString(), forwardUrl=string.Empty });

                //return RedirectToAction("Index");
            }
            catch
            {
                return Json(new { statusCode = ResponseStatus.Fail, message = "新增失败", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.closeCurrent.ToString(), forwardUrl = string.Empty });
            }
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit()
        {

            try
            {
                var model = BllObjectFactory.CompanyManager.Get(Request["CompanyId"]);

                return View("~/Views/SysManager/Company/Edit.cshtml",model);
            }
            catch (Exception)
            {
                return Json(new { statusCode = ResponseStatus.Fail, message = "修改失败", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.closeCurrent.ToString(), forwardUrl = string.Empty });
            }
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var model = BllObjectFactory.CompanyManager.Get(Request["CompanyId"]);

                SetObjectPropertyValueFromFormCollection(model, collection);

                BllObjectFactory.CompanyManager.Update(model);

                return Json(new { statusCode = ResponseStatus.Success, message = "修改成功", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.closeCurrent.ToString(), forwardUrl = string.Empty });

                //return RedirectToAction("Index");
            }
            catch
            {
                return Json(new { statusCode = ResponseStatus.Fail, message = "修改失败", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.closeCurrent.ToString(), forwardUrl = string.Empty });
            }
        }

        //
        // GET: /Company/Delete/5

        [HttpPost]
        public ActionResult DeleteOptions()
        {
            try
            {
                string ids = Request["Ids"];

                //此写法还是可能存在SQL注入？
                BllObjectFactory.CompanyManager.Delete("where Id in(" + ids + ")");//此处可以用Id名称、也可以用数据库库中的列名DepartmentId

                return Json(new { statusCode = ResponseStatus.Success, message = "删除成功", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.forward.ToString(), forwardUrl = string.Empty });
            }
            catch
            {
                return Json(new { statusCode = ResponseStatus.Fail, message = "删除失败", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.forward.ToString(), forwardUrl = string.Empty });
            }
        }

        //
        // POST: /Company/Delete/5

        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                string id = Request["CompanyId"];

                //此写法还是可能存在SQL注入？
                BllObjectFactory.CompanyManager.Delete("where Id in('" + id + "')");//此处可以用Id名称、也可以用数据库库中的列名DepartmentId

                return Json(new { statusCode = ResponseStatus.Success, message = "删除成功", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.forward.ToString(), forwardUrl = string.Empty });
            }
            catch
            {
                return Json(new { statusCode = ResponseStatus.Fail, message = "删除失败", navTabId = "CompanyList", rel = string.Empty, callbackType = CallbackType.forward.ToString(), forwardUrl = string.Empty });
            }
        }
    }
}
