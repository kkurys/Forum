using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.Category = db.Categories.Find(id);
            viewModel.Fora = db.Fora.ToList().FindAll(f => f.CategoryID == viewModel.Category.ID);

            return View(viewModel);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Category category)
        {

            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Category toDelete = db.Categories.Find(id);
            db.Categories.Remove(toDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Category/Delete/5
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
