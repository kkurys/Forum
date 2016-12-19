using Forum.Models;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Post post, Topic topic)
        {
            post.TopicID = topic.ID;
            topic.PostCount = topic.PostCount + 1;
            // save to db..

            return View();
        }

    }
}