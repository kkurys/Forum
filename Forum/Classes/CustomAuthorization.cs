using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Classes
{
    public class OwnerAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.IsInRole("Admin"))
            {
                return true;
            }
            else
            {
                ApplicationDbContext db = new ApplicationDbContext();

                var authorized = base.AuthorizeCore(httpContext);
                if (!authorized)
                {
                    return false;
                }

                var rd = httpContext.Request.RequestContext.RouteData;

                string controller = rd.Values["controller"].ToString();
                var userId = httpContext.User.Identity.GetUserId();

                if (controller == "Post")
                {
                    var tmpId = rd.Values["id"];
                    int postId = Int32.Parse(tmpId.ToString());

                    Post post = db.Posts.Find(postId);
                    var userItemId = post.UserID;
                    var postForumModerators = post.Topic.Forum.Moderators;

                    return userItemId == userId || postForumModerators.Any(x => x.Id == userId);
                }
                else if (controller == "Topic")
                {
                    var tmpId = rd.Values["id"];
                    int topicId = Int32.Parse(tmpId.ToString());

                    Topic topic = db.Topics.Find(topicId);
                    string userItemId = topic.UserID;
                    var topicForumModerators = topic.Forum.Moderators;
                    return userItemId == userId || topicForumModerators.Any(x => x.Id == userId);
                }
                else if (controller == "User")
                {
                    /*   var userName = (string)rd.Values["id"];
                       var userItemId = db.Users.ToList().Find(x => x.UserName == userName).Id; */
                    var userItemId = (string)rd.Values["id"];
                    return userItemId == userId;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}