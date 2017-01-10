using Forum.Classes;
using Forum.Models;
using Microsoft.AspNet.Identity.EntityFramework;
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
            UserDetailsViewModel viewModel = new UserDetailsViewModel();
            viewModel.User = db.Users.ToList().Find(x => x.UserName == userName);

            viewModel.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();
            viewModel.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();

            viewModel.Roles = new List<IdentityRole>();

            foreach (IdentityUserRole role in viewModel.User.Roles)
            {
                viewModel.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
            }

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
            ViewBag.PostsPerPageID = new SelectList(db.PostsPerPage, "ID", "Quantity");

            User user = db.Users.ToList().Find(x => x.Id == id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [OwnerAuthorize]
        public ActionResult Edit(User user)
        {
            ViewBag.PostsPerPageID = new SelectList(db.PostsPerPage, "ID", "Quantity");
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

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
