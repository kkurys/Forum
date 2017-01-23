using Forum.Classes;
using Forum.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult ViewPost(int id, int postId)
        {
            //var viewedTopicsIDs = Session["ViewedTopicsIDs"] as List<int>;

            //int startIndex = 0, endIndex = 0, postsPerPage;
            //TopicViewModel viewModel = new TopicViewModel();

            //viewModel.Topic = db.Topics.Find(id);
            //viewModel.Admin = false;
            //viewModel.CurrentUserId = User.Identity.GetUserId();

            //if (!viewedTopicsIDs.Contains(id))
            //{
            //    viewedTopicsIDs.Add(id);
            //    viewModel.Topic.ViewsCount++;
            //    db.SaveChanges();
            //}

            //var user = db.Users.Find(viewModel.CurrentUserId);
            //if (User.Identity.IsAuthenticated)
            //{
            //    postsPerPage = user.PostsPerPage.Quantity;
            //}
            //else
            //{
            //    postsPerPage = 25;
            //}

            //var tmpList = db.Posts.ToList().FindAll(f => f.TopicID == viewModel.Topic.ID);
            //viewModel.Pages = tmpList.Count() / postsPerPage + 1;

            //int postIndex = tmpList.FindIndex(x => x.ID == postId);
            //int currentPage = 0;
            //for (currentPage = 0; currentPage < viewModel.Pages; currentPage++)
            //{
            //    startIndex = currentPage * postsPerPage;
            //    endIndex = startIndex + postsPerPage;
            //    if (postIndex >= startIndex && postIndex < endIndex)
            //    {
            //        break;
            //    }
            //}

            //if (endIndex > tmpList.Count)
            //{
            //    endIndex = tmpList.Count;
            //}

            //if (User.IsInRole("Admin"))
            //{
            //    viewModel.Admin = true;
            //}

            //viewModel.Posts = tmpList.GetRange(startIndex, endIndex - startIndex);
            //ViewData["postId"] = postId;
            //return View("Details", viewModel);
            return View();
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
            db.Posts.Add(newTopic.Post);

            db.Fora.Find(newTopic.Topic.ForumID).TopicCount++;
            db.Fora.Find(newTopic.Topic.ForumID).PostCount++;
            db.SaveChanges();

            return RedirectToAction("Details", "Topic", new { id = newTopic.Topic.ID });
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
