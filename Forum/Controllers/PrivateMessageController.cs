using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class PrivateMessageController : Controller
    {
        // GET: PrivateMessage
        public ActionResult Index()
        {
            return View();
        }

        // GET: PrivateMessage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrivateMessage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrivateMessage/Create
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

        // GET: PrivateMessage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrivateMessage/Edit/5
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

        // GET: PrivateMessage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrivateMessage/Delete/5
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
