using Forum.Classes;
using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var postList = db.Posts.ToList();

            return View(postList);
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            List<Post> viewModel = new List<Post>();
            if (id == null)
            {
                viewModel = db.Posts.ToList();
            }
            else
            {
                viewModel = db.Posts.ToList().FindAll(x => x.UserID == id);
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var post = db.Posts.Find(id);

            return View(post);
        }

        [HttpGet]
        public ActionResult Create(int id, int? error)
        {
            var newPost = new Post();
            newPost.Topic = db.Topics.Find(id);
            newPost.TopicID = id;

            return View(newPost);
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            try
            {
                post.Date = DateTime.Now;
                if (User.Identity.IsAuthenticated)
                {
                    post.UserID = User.Identity.GetUserId();
                }
                else
                {
                    post.UserID = null;
                }

                post.Content = Html.EditMarkers(post.Content);

                db.Posts.Add(post);
                db.Topics.Find(post.TopicID).PostCount++;
                db.Fora.Find(db.Topics.Find(post.TopicID).ForumID).PostCount++;
                db.Topics.Find(post.TopicID).LastPostDate = post.Date;

                db.SaveChanges();

                return RedirectToAction("Details", "Topic", new { id = post.TopicID });
            }
            catch
            {
                return View(post);
            }
        }

        [OwnerAuthorize]
        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Find(id);
            return View(post);
        }

        // POST: Topic/Edit/5
        [HttpPost]
        [OwnerAuthorize]
        public ActionResult Edit(int id, Post post)
        {
            try
            {
                post.Content = Html.EditMarkers(post.Content);
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", "Topic", new { id = post.TopicID });
            }
            catch
            {
                return View(post);
            }
        }
        public ActionResult ReportPost(int id, int? postPage)
        {
            var viewModel = new ReportPostViewModel();
            viewModel.Post = db.Posts.ToList().Find(x => x.ID == id);
            viewModel.PostID = viewModel.Post.ID;
            viewModel.PostPage = postPage;

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ReportPost(ReportPostViewModel viewModel)
        {
            viewModel.Post = db.Posts.ToList().Find(x => x.ID == viewModel.PostID);

            var _moderators = viewModel.Post.Topic.Forum.Moderators;

            foreach (User _moderator in _moderators)
            {
                var _reportThread = db.PrivateThreads.ToList().Find(x => x.Title == "Zgłoszenia");

                if (_reportThread == null)
                {
                    _reportThread = new PrivateThread();
                    _reportThread.Recipient = _moderator;
                    _reportThread.Title = "Zgłoszenia";
                    _reportThread.Seen = false;

                    db.PrivateThreads.Add(_reportThread);
                }

                var _reportMessage = new PrivateMessage();

                _reportMessage.PrivateThread = _reportThread;
                _reportMessage.Author = db.Users.ToList().Find(x => x.Id == User.Identity.GetUserId());

                if (viewModel.PostPage.HasValue)
                {
                    _reportMessage.Content += "Zgłoszono post: " + "<a href=/Topic/Details/" + viewModel.Post.Topic.ID + "?page=" + viewModel.PostPage + "#" + viewModel.Post.ID + ">#" + viewModel.Post.ID + "</a><br />";
                }
                else
                {
                    _reportMessage.Content += "Zgłoszono post: " + "<a href=/Topic/Details/" + viewModel.Post.Topic.ID + "#" + viewModel.Post.ID + ">#" + viewModel.Post.ID + "</a><br />";
                }

                _reportMessage.Content += "Uzasadnienie: <br />" + viewModel.Reason;
                _reportMessage.Date = DateTime.Now;

                _reportThread.Seen = false;

                db.PrivateMessages.Add(_reportMessage);
            }
            db.SaveChanges();
            return View(viewModel);
        }
    }
}