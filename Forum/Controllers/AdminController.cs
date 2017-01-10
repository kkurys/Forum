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
        public ActionResult ModeratorsList(int? activeCategory, int? error, int? activeForum)
        {
            var viewModel = new ModeratorsListViewModel();


            viewModel.Categories = db.Categories.ToList();
            viewModel.ActiveCategory = activeCategory;
            viewModel.ActiveForum = activeForum;

            if (error.HasValue && error == 1)
            {
                viewModel.AdditionError = 1;
            }
            return View(viewModel);
        }
        public ActionResult GetForumModerators(int ForumID, int? AdditionError)
        {
            var viewModel = new PartialModeratorsListViewModel();
            var forum = db.Fora.ToList().Find(x => x.ID == ForumID);
            viewModel.ForumID = ForumID;
            viewModel.Forum = forum;
            viewModel.AdditionError = AdditionError;
            viewModel.Moderators = forum.Moderators;

            return PartialView("ModeratorsListPartial", viewModel);
        }
        public ActionResult AddModerator(PartialModeratorsListViewModel viewModel)
        {
            var _user = db.Users.ToList().Find(x => x.UserName == viewModel.Username);
            var _forum = db.Fora.ToList().Find(x => x.ID == viewModel.ForumID);
            if (_user != null && _forum != null)
            {
                if (_user.Forums.Contains(_forum) && _forum.Moderators.Contains(_user))
                {
                    viewModel.AdditionError = 1;
                }
                else
                {
                    _user.Forums.Add(_forum);
                    _forum.Moderators.Add(_user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ModeratorsList", new { activeCategory = _forum.CategoryID, activeForum = viewModel.ForumID, error = viewModel.AdditionError });
        }
        public ActionResult DeleteModerator(PartialModeratorsListViewModel viewModel)
        {
            var moderatorToRemove = db.Users.ToList().Find(x => x.Id == viewModel.UserID);
            var modifiedForum = db.Fora.ToList().Find(x => x.ID == viewModel.ForumID);

            if (moderatorToRemove != null && modifiedForum != null)
            {
                modifiedForum.Moderators.Remove(moderatorToRemove);
                moderatorToRemove.Forums.Remove(modifiedForum);

                db.SaveChanges();
            }

            return RedirectToAction("ModeratorsList", new { activeCategory = modifiedForum.CategoryID, activeForum = viewModel.ForumID });
        }
    }
}