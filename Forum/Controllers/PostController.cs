using Forum.Classes;
using Forum.Content.Localization;
using Forum.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        [HandleError]
        public ActionResult Create(Post post)
        {
            post.Date = DateTime.Now;
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                post.UserID = user.Id;
                db.Users.Find(User.Identity.GetUserId()).PostsCount++;
                if (!user.OwnRank)
                {
                    db.Users.Find(User.Identity.GetUserId()).Rank = UserManagement.GetRank(user.PostsCount);
                }
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
        public ActionResult QuickReply(TopicViewModel topic, int id)
        {
            Post post = new Post();
            post.TopicID = id;
            post.Date = DateTime.Now;
            post.Content = "";
            if (topic.Content == null)
            {
                topic.Content = "";
            }
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                post.UserID = user.Id;
                db.Users.Find(User.Identity.GetUserId()).PostsCount++;
                if (!user.OwnRank)
                {
                    db.Users.Find(User.Identity.GetUserId()).Rank = UserManagement.GetRank(user.PostsCount);
                }
            }
            else
            {
                post.UserID = null;
            }

            post.Content = Html.EditMarkers(topic.Content);

            bool error = false;
            if (Request.Files.Count > 3)
            {
                error = true;
                ViewBag.Error = Resources.AttachmentCount;
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength > 512 * 1024)
                {
                    ViewBag.Error = Resources.AttachmentSize;
                    error = true;
                    break;
                }
            }
            if (error)
            {

                var user = db.Users.Find(User.Identity.GetUserId());

                topic.Admin = false;
                topic.CurrentUserId = User.Identity.GetUserId();
                topic.Topic = db.Topics.Find(id);
                topic.User = user;
                topic.CurrentUserId = user.Id;
                int postsPerPage = 0;

                if (User.Identity.IsAuthenticated)
                {
                    postsPerPage = user.PostsPerPage.Quantity;
                }
                else
                {
                    postsPerPage = 25;
                }
                topic.Topic = db.Topics.Find(id);
                var tmpList = db.Posts.ToList().FindAll(f => f.TopicID == topic.Topic.ID);


                if (User.IsInRole("Admin"))
                {
                    topic.Admin = true;
                }
                int currPage = 1, postNumber = 0;
                foreach (Post _post in tmpList)
                {
                    if (post.ID == topic.Topic.Posts.ToList()[topic.Topic.Posts.Count - 1].ID)
                    {
                        break;
                    }
                    postNumber++;
                }
                while (currPage * postsPerPage < postNumber)
                {
                    currPage++;
                }
                topic.Posts = tmpList.ToPagedList(currPage, postsPerPage);
                return View("~/Views/Topic/Details.cshtml", topic);
            }

            db.Posts.Add(post);
            db.Topics.Find(post.TopicID).PostCount++;
            db.Fora.Find(db.Topics.Find(post.TopicID).ForumID).PostCount++;
            db.Topics.Find(post.TopicID).LastPostDate = post.Date;


            db.SaveChanges();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.FileName == "") continue;

                file.SaveAs(HttpContext.Server.MapPath("~/Content/Attachments/")
                         + file.FileName);
                PostFile _postFile = new PostFile();
                _postFile.Filename = "~/Content/Attachments/" + file.FileName;
                _postFile.Post = post;
                db.PostFiles.Add(_postFile);
                db.SaveChanges();
            }


            return RedirectToAction("Details", "Topic", new { id = id });
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
        public ActionResult ReportPost(int id, int? page)
        {
            var viewModel = new ReportPostViewModel();
            viewModel.Post = db.Posts.ToList().Find(x => x.ID == id);
            viewModel.PostID = viewModel.Post.ID;
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


                _reportMessage.Content += "Zgłoszono post: " + "<a href=\"/Topic/Details/" + viewModel.Post.Topic.ID.ToString() + "?postId=" + viewModel.Post.ID + "\">#" + viewModel.Post.ID + "</a><br />";


                _reportMessage.Content += "Uzasadnienie: <br />" + viewModel.Reason;
                _reportMessage.Date = DateTime.Now;

                _reportThread.Seen = false;

                db.PrivateMessages.Add(_reportMessage);
            }
            db.SaveChanges();

            return View("PostReported", viewModel);
        }
        public ActionResult Delete(int id)
        {
            Post toDelete = db.Posts.Find(id);
            db.Posts.Remove(toDelete);
            db.SaveChanges();
            return RedirectToAction("Details", "Topic", new { id = toDelete.TopicID });
        }
    }
}