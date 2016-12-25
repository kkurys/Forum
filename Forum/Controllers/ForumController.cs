using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Forum
        public ActionResult Index()
        {
            var forumList = db.Fora.ToList();

            return View(forumList);
        }

        // GET: Forum/Details/5
        public ActionResult Details(int id)
        {
            ForumTopicsViewModel viewModel = new ForumTopicsViewModel();
            viewModel.Forum = db.Fora.Find(id);
            viewModel.Topics = db.Topics.ToList().FindAll(f => f.ForumID == viewModel.Forum.ID);

            return View(viewModel);
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");

            return View();
        }

        // POST: Forum/Create
        [HttpPost]
        public ActionResult Create(Models.Forum forum)
        {
            db.Fora.Add(forum);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");

            Models.Forum forum = db.Fora.Find(id);
            return View(forum);
        }

        // POST: Forum/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Forum forum)
        {
            db.Entry(forum).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int id)
        {
            Models.Forum toDelete = db.Fora.Find(id);
            db.Fora.Remove(toDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Forum/Delete/5
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
