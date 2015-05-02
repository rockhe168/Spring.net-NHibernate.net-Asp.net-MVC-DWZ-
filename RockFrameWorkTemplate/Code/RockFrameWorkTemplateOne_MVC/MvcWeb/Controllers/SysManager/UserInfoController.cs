using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWeb.Controllers.SysManager
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        public ActionResult Index()
        {
            //return View();
            return View("~/Views/SysManager/UserInfo/Index.cshtml");
        }

        //
        // GET: /UserInfo/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UserInfo/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserInfo/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserInfo/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UserInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserInfo/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserInfo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
