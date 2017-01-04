using Forum.Classes;
using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search/Details/5
        public ActionResult Details()
        {
            SearchResultViewModel viewModel = new SearchResultViewModel();
            return View(viewModel);
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            SearchResultViewModel viewModel = new SearchResultViewModel();

            return View(viewModel);
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(SearchResultViewModel viewModel)
        {
            try
            {
                var postsList = db.Posts.ToList();
                var keywordList = viewModel.SearchProperties.Keywords.Split();
                Dictionary<Post, Topic> allPosts = new Dictionary<Post, Topic>();
                foreach (var post in db.Posts.ToList())
                {
                    allPosts.Add(post, post.Topic);
                }

                if (viewModel.SearchProperties.KeywordsAll)
                {
                    // AND
                    viewModel.PostTopic = Search.SearchPostsAnd(postsList, keywordList, viewModel.SearchProperties.SizeDoesMatter);
                    if (viewModel.SearchProperties.Negation)
                    {
                        viewModel.PostTopic = Search.SearchOpossite(allPosts, viewModel.PostTopic);
                    }
                }
                else
                {
                    // OR
                    viewModel.PostTopic = Search.SearchPostsOr(postsList, keywordList, viewModel.SearchProperties.SizeDoesMatter);
                    if (viewModel.SearchProperties.Negation)
                    {
                        viewModel.PostTopic = Search.SearchOpossite(allPosts, viewModel.PostTopic);
                    }
                }

                //else if (viewModel.SearchProperties.Authors != null)
                //{
                //    var authorList = viewModel.SearchProperties.Authors.Split();

                //    foreach (var post in postsList)
                //    {
                //        foreach (var author in authorList)
                //        {
                //            if (post.User.UserName == author)
                //            {
                //                if (!viewModel.Topics.Contains(post.Topic))
                //                {
                //                    viewModel.Topics.Add(post.Topic);
                //                }
                //            }
                //        }
                //    }
                //}

                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
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
