using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class HtmlController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Html
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var viewModel = db.HtmlMarkers.ToList();
            return View(viewModel);
        }

        // GET: Html/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Html/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Html/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(HtmlMarker html)
        {
            try
            {
                db.HtmlMarkers.Add(html);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Html/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            HtmlMarker toEdit = db.HtmlMarkers.Find(id);
            return View(toEdit);
        }

        // POST: Html/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, HtmlMarker toEdit)
        {
            try
            {
                db.Entry(toEdit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Html/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            HtmlMarker toDelete = db.HtmlMarkers.Find(id);
            db.HtmlMarkers.Remove(toDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Html/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
