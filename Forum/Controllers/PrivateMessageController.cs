using Forum.Classes;
using Forum.Content.Localization;
using Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class PrivateMessageController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            int postsPerPage;
            var user = db.Users.Find(User.Identity.GetUserId());

            PrivateThreadsListViewModel viewModel = new PrivateThreadsListViewModel();

            if (User.Identity.IsAuthenticated)
            {
                postsPerPage = user.PostsPerPage.Quantity;
            }
            else
            {
                postsPerPage = 25;
            }


            viewModel.User = user;
            var tmpThreads = db.PrivateThreads.ToList().FindAll(x => x.RecipientID == user.Id || x.SenderID == user.Id);

            viewModel.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();
            viewModel.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();

            viewModel.Roles = new List<IdentityRole>();

            foreach (IdentityUserRole role in viewModel.User.Roles)
            {
                viewModel.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
            }

            int currPage = page.HasValue ? page.Value : 1;

            viewModel.Threads = tmpThreads.ToPagedList(currPage, postsPerPage);
            viewModel.Threads.OrderByDescending(x => x.LastPostDate()).ToList();
            
            
            var lastRole = viewModel.User.Roles.Last();
            viewModel.UserRole = db.Roles.Find(lastRole.RoleId).Name;

            return View(viewModel);
        }

        public ActionResult ViewThread(int id, int? page)
        {
            int postsPerPage;
            var user = db.Users.Find(User.Identity.GetUserId());

            PrivateThreadViewModel viewModel = new PrivateThreadViewModel();

            viewModel.User = db.Users.ToList().Find(x => x.Id == user.Id);

            if (User.Identity.IsAuthenticated)
            {
                postsPerPage = user.PostsPerPage.Quantity;
            }
            else
            {
                postsPerPage = 25;
            }

            viewModel.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();
            viewModel.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();

            viewModel.Roles = new List<IdentityRole>();

            var tmpMessages = db.PrivateMessages.ToList().FindAll(x => x.PrivateThreadID == id);
            viewModel.PrivateThread = db.PrivateThreads.ToList().Find(x => x.ID == id);
            viewModel.PrivateThread.Seen = true;
            db.SaveChanges();

            int currPage = page.HasValue ? page.Value : 1;
            viewModel.Messages = tmpMessages.ToPagedList(currPage, postsPerPage);

            foreach (IdentityUserRole role in viewModel.User.Roles)
            {
                viewModel.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
            }

            var lastRole = viewModel.User.Roles.Last();
            viewModel.UserRole = db.Roles.Find(lastRole.RoleId).Name;

            return View(viewModel);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateReply(PrivateThreadViewModel request, int id)
        {
            var _newMessage = new PrivateMessage();
            var user = db.Users.Find(User.Identity.GetUserId());
            int postsPerPage;

            _newMessage.Content = Html.EditMarkers(request.Content);

            request = new PrivateThreadViewModel();

            _newMessage.Date = DateTime.Now;
            _newMessage.Author = db.Users.ToList().Find(x => x.Id == user.Id);

            request.User = _newMessage.Author;

            request.Content = "";

            request.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == request.User.Id).Count();
            request.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == request.User.Id).Count();

            request.Roles = new List<IdentityRole>();

            request.PrivateThread = db.PrivateThreads.ToList().Find(x => x.ID == id);
            request.PrivateThread.Seen = false;

            _newMessage.PrivateThreadID = id;

            if (User.Identity.IsAuthenticated)
            {
                postsPerPage = user.PostsPerPage.Quantity;
            }
            else
            {
                postsPerPage = 25;
            }

            request.Messages = db.PrivateMessages.ToList().FindAll(x => x.PrivateThreadID == id).ToPagedList(1, postsPerPage);
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
                return View("ViewThread", request);
            }

            db.PrivateMessages.Add(_newMessage);
            db.SaveChanges();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];

                if (file.FileName == "") continue;

                file.SaveAs(HttpContext.Server.MapPath("~/Content/Attachments/")
                         + file.FileName);
                MessageFile _messageFile = new MessageFile();
                _messageFile.Filename = "~/Content/Attachments/" + file.FileName;
                _messageFile.PrivateMessage = _newMessage;
                db.MessageFiles.Add(_messageFile);
                db.SaveChanges();
            }

            ModelState.Clear();

            return RedirectToAction("ViewThread", new { id = id });
        }

        // GET: PrivateMessage/Create
        public ActionResult CreateThread()
        {
            CreateThreadViewModel viewModel = new CreateThreadViewModel();
            var userId = User.Identity.GetUserId();
            viewModel.User = db.Users.ToList().Find(x => x.Id == userId);

            viewModel.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();
            viewModel.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == viewModel.User.Id).Count();

            viewModel.Roles = new List<IdentityRole>();

            foreach (IdentityUserRole role in viewModel.User.Roles)
            {
                viewModel.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
            }

            var lastRole = viewModel.User.Roles.Last();
            viewModel.UserRole = db.Roles.Find(lastRole.RoleId).Name;

            return View(viewModel);
        }

        // POST: PrivateMessage/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateThread(CreateThreadViewModel request)
        {
            try
            {
                var newThread = new PrivateThread();

                var userId = User.Identity.GetUserId();

                newThread.Recipient = db.Users.ToList().Find(x => x.UserName == request.Recipient);
                newThread.SenderID = userId;
                newThread.Title = request.Title;

                db.PrivateThreads.Add(newThread);
                db.SaveChanges();

                var newMessage = new PrivateMessage();

                newMessage.AuthorID = userId;
                newMessage.Content = Html.EditMarkers(request.Content);
                newMessage.Date = DateTime.Now;
                newMessage.PrivateThread = newThread;


                if (Request.Files != null)
                {
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
                        request.User = db.Users.ToList().Find(x => x.Id == userId);
                        request.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == request.User.Id).Count();
                        request.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == request.User.Id).Count();

                        request.Roles = new List<IdentityRole>();

                        foreach (IdentityUserRole role in request.User.Roles)
                        {
                            request.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
                        }

                        return View(request);
                    }
                    db.PrivateMessages.Add(newMessage);
                    db.SaveChanges();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        if (file == null || file.FileName == "") continue;
                        file.SaveAs(HttpContext.Server.MapPath("~/Content/Attachments/")
                                 + file.FileName);
                        MessageFile _messageFile = new MessageFile();
                        _messageFile.Filename = "~/Content/Attachments/" + file.FileName;
                        _messageFile.PrivateMessage = newMessage;
                        db.MessageFiles.Add(_messageFile);
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.PrivateMessages.Add(newMessage);
                    db.SaveChanges();
                }

                return RedirectToAction("ViewThread", new { id = newThread.ID });
            }
            catch
            {
                var userId = User.Identity.GetUserId();

                request.User = db.Users.ToList().Find(x => x.Id == userId);
                request.PostsCount = db.Posts.ToList().FindAll(x => x.UserID == request.User.Id).Count();
                request.TopicsCount = db.Topics.ToList().FindAll(x => x.UserID == request.User.Id).Count();

                request.Roles = new List<IdentityRole>();

                foreach (IdentityUserRole role in request.User.Roles)
                {
                    request.Roles.Add(db.Roles.ToList().Find(x => x.Id == role.RoleId));
                }

                return View(request);
            }
        }

        // GET: PrivateMessage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrivateMessage/Edit/5
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
        public ActionResult DeleteThread(int id)
        {
            var threadToDelete = db.PrivateThreads.Find(id);
            db.PrivateThreads.Remove(threadToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: PrivateMessage/Delete/5
        public ActionResult Delete(int id)
        {
            var messageToDelete = db.PrivateMessages.Find(id);
            int threadIdHold = messageToDelete.PrivateThread.ID;
            db.PrivateMessages.Remove(messageToDelete);
            db.SaveChanges();
            return RedirectToAction("ViewThread", new { id = threadIdHold });
        }

        // POST: PrivateMessage/Delete/5
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
        public FileResult DownloadAttachment(string Filename)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(HttpContext.Server.MapPath(Filename));

            string fileName = Filename.Split('/').Last();

            return File(Filename, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public JsonResult GetMatchingUsers(string term)
        {
            var results = db.Users.Where(user => term == null || user.UserName.ToLower().Contains(term.ToLower())).Select(x => new { id = x.Id, value = x.UserName }).Take(5).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }

}
