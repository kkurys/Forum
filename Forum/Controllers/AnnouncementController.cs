using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class AnnouncementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Announcement
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var announcementList = db.Announcements.ToList();

            return View(announcementList);
        }

        // GET: Announcement/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            Announcement viewModel = db.Announcements.Find(id);

            return View(viewModel);
        }

        // GET: Announcement/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcement/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Announcement announcement)
        {
            announcement.UserID = User.Identity.GetUserId();
            announcement.Date = DateTime.Now;
            db.Announcements.Add(announcement);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Announcement/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Announcement announcement = db.Announcements.Find(id);

            return View(announcement);
        }

        // POST: Announcement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Announcement announcement)
        {
            announcement.Date = DateTime.Now;
            db.Entry(announcement).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Announcement/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Announcement toDelete = db.Announcements.Find(id);
            db.Announcements.Remove(toDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Announcement/Delete/5
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
