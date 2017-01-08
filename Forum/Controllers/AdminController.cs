using Forum.Models;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ModeratorsList(int? activeForum)
        {
            var viewModel = new ModeratorsListViewModel();

            viewModel.Forums = db.Fora.ToList();
            viewModel.ActiveForum = activeForum;

            return View(viewModel);
        }
        public ActionResult AddModerator(ModeratorsListViewModel viewModel)
        {
            var _user = db.Users.ToList().Find(x => x.UserName == viewModel.Username);
            var _forum = db.Fora.ToList().Find(x => x.ID == viewModel.ForumID);
            if (_user != null && _forum != null)
            {
                _user.Forums.Add(_forum);
                _forum.Moderators.Add(_user);
                db.SaveChanges();
            }
            return RedirectToAction("ModeratorsList", new { activeForum = viewModel.ForumID });
        }
    }
}