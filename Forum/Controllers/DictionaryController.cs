using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class DictionaryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dictionary
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var viewModel = db.Dictionary.ToList();
            return View(viewModel);
        }

        // GET: Dictionary/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dictionary/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dictionary/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Dictionary word)
        {
            try
            {
                db.Dictionary.Add(word);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dictionary/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Dictionary toEdit = db.Dictionary.Find(id);
            return View(toEdit);
        }

        // POST: Dictionary/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Dictionary toEdit)
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

        // GET: Dictionary/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Dictionary toDelete = db.Dictionary.Find(id);
            db.Dictionary.Remove(toDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Dictionary/Delete/5
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
