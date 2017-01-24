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
    public class TopicController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Topic
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var topicList = db.Topics.ToList();

            return View(topicList);
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            List<Topic> viewModel = new List<Topic>();
            if (id == null)
            {
                viewModel = db.Topics.ToList();
            }
            else
            {
                viewModel = db.Topics.ToList().FindAll(x => x.UserID == id);
            }

            return View(viewModel);
        }

        // GET: Topic/Details/5
        public ActionResult Details(int id, int? page, int? postId)
        {
            var viewedTopicsIDs = Session["ViewedTopicsIDs"] as List<int>;
            int postsPerPage, currPage = 1, postNumber = 0;
            var user = db.Users.Find(User.Identity.GetUserId());
            TopicViewModel viewModel = new TopicViewModel();

            viewModel.Admin = false;
            viewModel.CurrentUserId = User.Identity.GetUserId();
            viewModel.Topic = db.Topics.Find(id);
            viewModel.User = user;
            viewModel.Content = "";
            if (!viewedTopicsIDs.Contains(id))
            {
                viewedTopicsIDs.Add(id);
                viewModel.Topic.ViewsCount++;
                db.SaveChanges();
            }

            if (User.Identity.IsAuthenticated)
            {
                viewModel.CurrentUserId = user.Id;
            }
            else
            {
                viewModel.CurrentUserId = "";
            }

            if (User.Identity.IsAuthenticated)
            {
                postsPerPage = user.PostsPerPage.Quantity;
            }
            else
            {
                postsPerPage = 25;
            }
            viewModel.Topic = db.Topics.Find(id);
            var tmpList = db.Posts.ToList().FindAll(f => f.TopicID == viewModel.Topic.ID);

            if (postId != null)
            {
                foreach (Post post in tmpList)
                {
                    if (post.ID == postId)
                    {
                        break;
                    }
                    postNumber++;
                }
                while (currPage * postsPerPage < postNumber)
                {
                    currPage++;
                }
                ViewData["postId"] = postId;
            }
            else
            {
                currPage = page.HasValue ? page.Value : 1;
            }

            if (User.IsInRole("Admin"))
            {
                viewModel.Admin = true;
            }

            viewModel.Posts = tmpList.ToPagedList(currPage, postsPerPage);

            return View(viewModel);
        }
        // GET: Topic/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            var newTopic = new CreateTopicViewModel();
            newTopic.Forum = db.Fora.Find(id);

            return View(newTopic);
        }

        // POST: Topic/Create
        [HttpPost]
        [HandleError]
        public ActionResult Create(CreateTopicViewModel newTopic)
        {
            newTopic.Topic.LastPostDate = DateTime.Now;
            newTopic.Topic.PostCount = 1;
            newTopic.Topic.ViewsCount = 0;
            newTopic.Topic.UserID = User.Identity.GetUserId();
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                newTopic.Topic.UserID = user.Id;
                db.Users.Find(User.Identity.GetUserId()).PostsCount++;
                if (!user.OwnRank)
                {
                    db.Users.Find(User.Identity.GetUserId()).Rank = UserManagement.GetRank(user.PostsCount);
                }
            }
            else
            {
                newTopic.Topic.UserID = null;
            }
            newTopic.Topic.ForumID = newTopic.Forum.ID;
            db.Topics.Add(newTopic.Topic);
            db.SaveChanges();

            newTopic.Post.Date = DateTime.Now;
            newTopic.Post.TopicID = db.Topics.ToList().Last().ID;
            if (User.Identity.IsAuthenticated)
            {
                newTopic.Post.UserID = User.Identity.GetUserId();
            }
            else
            {
                newTopic.Post.UserID = null;
            }
            newTopic.Post.Content = Html.EditMarkers(newTopic.Post.Content);
            db.Posts.Add(newTopic.Post);

            db.Fora.Find(newTopic.Topic.ForumID).TopicCount++;
            db.Fora.Find(newTopic.Topic.ForumID).PostCount++;
            db.SaveChanges();

            return RedirectToAction("Details", "Topic", new { id = newTopic.Topic.ID });
        }
        [HttpPost]
        public ActionResult Details(TopicViewModel topic, int id)
        {
            if (!ModelState.IsValid)
            {
                var user = db.Users.Find(User.Identity.GetUserId());

                topic.Admin = false;
                topic.CurrentUserId = User.Identity.GetUserId();
                topic.Topic = db.Topics.Find(id);
                topic.User = user;
                topic.CurrentUserId = user.Id;
                var tmpList = db.Posts.ToList().FindAll(f => f.TopicID == topic.Topic.ID);
                int currPage = 1, postNumber = 0;
                int postsPerPage = 0;

                if (User.Identity.IsAuthenticated)
                {
                    postsPerPage = user.PostsPerPage.Quantity;
                }
                else
                {
                    postsPerPage = 25;
                }
                foreach (Post _post in tmpList)
                {
                    if (_post.ID == topic.Topic.Posts.ToList()[topic.Topic.Posts.Count - 1].ID)
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
            Post post = new Post();
            post.TopicID = id;
            post.Date = DateTime.Now;
            post.Content = "";
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
                    if (_post.ID == topic.Topic.Posts.ToList()[topic.Topic.Posts.Count - 1].ID)
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
        // GET: Topic/Edit/5
        [OwnerAuthorize]
        public ActionResult Edit(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }

        // POST: Topic/Edit/5
        [HttpPost]
        [OwnerAuthorize]
        public ActionResult Edit(int id, Topic topic)
        {
            try
            {
                db.Entry(topic).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return View(topic);
            }
        }

        // GET: Topic/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Topic/Delete/5
        [HttpPost]
        [OwnerAuthorize]
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
