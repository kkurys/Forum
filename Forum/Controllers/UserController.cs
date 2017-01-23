using Forum.Classes;
using Forum.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            var viewModel = new UserIndexViewModel();

            viewModel.UserPostCount = new Dictionary<User, UserDetails>();

            foreach (User user in db.Users.ToList())
            {
                UserDetails tmp = new UserDetails();

                tmp.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == user.Id).Count();
                tmp.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == user.Id).Count();

                tmp.Roles = new List<IdentityRole>();

                foreach (IdentityUserRole role in user.Roles)
                {
                    tmp.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
                }

                viewModel.UserPostCount.Add(user, tmp);
            }

            return View(viewModel);
        }

        // GET: User/Details/5
        public ActionResult Details(string userName)
        {
            IndexViewModel viewModel = new IndexViewModel();
            //UserDetailsViewModel viewModel = new UserDetailsViewModel();
            viewModel.User = db.Users.ToList().Find(x => x.UserName == userName);

            viewModel.Posts = db.Posts.ToList().FindAll(x => x.UserID == viewModel.User.Id);
            viewModel.Topics = db.Topics.ToList().FindAll(x => x.UserID == viewModel.User.Id);

            viewModel.PostsCount = viewModel.Posts.Count();
            viewModel.TopicsCount = viewModel.Topics.Count();
            
            viewModel.Posts.Sort((x, y) => DateTime.Compare(y.Date, x.Date));
            viewModel.Topics.Sort((x, y) => DateTime.Compare(y.Posts.First().Date, x.Posts.First().Date));
            if (viewModel.Posts.Count > 5) viewModel.Posts.RemoveRange(5, viewModel.Posts.Count - 5);
            if (viewModel.Topics.Count > 5) viewModel.Topics.RemoveRange(5, viewModel.Topics.Count - 5);

            return View(viewModel);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        [HttpGet]
        [OwnerAuthorize]
        public ActionResult Edit(string id)
        {
            User user = db.Users.ToList().Find(x => x.Id == id);

            ViewBag.PostsPerPageID = new SelectList(db.PostsPerPage, "ID", "Quantity", user.PostsPerPage);

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [OwnerAuthorize]
        public ActionResult Edit(User user)
        {
            ViewBag.PostsPerPageID = new SelectList(db.PostsPerPage, "ID", "Quantity", user.PostsPerPage);

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            User.Identity.AddUpdateClaim(user.Theme.ToString());

            try
            {

                return RedirectToAction("Details", new { userName = user.UserName });
            }
            catch
            {

                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
