using Forum.Models;
using Microsoft.AspNet.Identity;
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
        public ActionResult Index()
        {
            var topicList = db.Topics.ToList();

            return View(topicList);
        }

        // GET: Topic/Details/5
        public ActionResult Details(int id)
        {
            TopicViewModel viewModel = new TopicViewModel();
            viewModel.Topic = db.Topics.Find(id);
            viewModel.Posts = db.Posts.ToList().FindAll(f => f.TopicID == viewModel.Topic.ID);

            return View(viewModel);
        }

        // GET: Topic/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            var newTopic = new CreateTopicViewModel();
            newTopic.ForumID = id;

            return View(newTopic);
        }

        // POST: Topic/Create
        [HttpPost]
        public ActionResult Create(CreateTopicViewModel newTopic)
        {
            newTopic.Topic.IsGlued = false;
            newTopic.Topic.PostCount = 1;
            newTopic.Topic.ViewsCount = 0;
            newTopic.Topic.UserID = User.Identity.GetUserId();
            newTopic.Topic.ForumID = newTopic.ForumID;
            db.Topics.Add(newTopic.Topic);
            db.SaveChanges();

            newTopic.Post.Date = DateTime.Now;
            newTopic.Post.TopicID = db.Topics.ToList().Last().ID;
            newTopic.Post.UserID = User.Identity.GetUserId();
            db.Posts.Add(newTopic.Post);

            db.Fora.Find(newTopic.Topic.ForumID).TopicCount++;
            db.Fora.Find(newTopic.Topic.ForumID).PostCount++;
            db.SaveChanges();

            return RedirectToAction("Details", "Topic", new { id = newTopic.Topic.ID });
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }

        // POST: Topic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Topic topic)
        {
            db.Entry(topic).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Topic/Delete/5
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
