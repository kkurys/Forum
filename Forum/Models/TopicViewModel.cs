using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }
        public IPagedList<Post> Posts { get; set; }
        public string CurrentUserId { get; set; }
        public bool Admin { get; set; }
        public User User { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}