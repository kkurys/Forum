using Forum.Models;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index()
        {
            var postList = db.Posts.ToList();

            return View(postList);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var newPost = new Post();
            newPost.Topic = db.Topics.Find(id);
            newPost.TopicID = id;

            return View(newPost);
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            post.Date = DateTime.Now;
            post.UserID = User.Identity.GetUserId();

            db.Posts.Add(post);
            db.SaveChanges();
            // save to db..

            return RedirectToAction("Index");
        }

    }
}