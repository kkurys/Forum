using Forum.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var homeView = new ForumHomeViewModel();

            homeView.Announcements = db.Announcements.ToList();
            homeView.CategoryForums = new Dictionary<Category, List<Models.Forum>>();
            foreach (Category cat in db.Categories.ToList())
            {
                List<Models.Forum> categoryForums = db.Fora.ToList().FindAll(f => f.CategoryID == cat.ID);

                homeView.CategoryForums.Add(cat, categoryForums);
            }

            return View(homeView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}