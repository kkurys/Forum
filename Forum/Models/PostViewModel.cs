using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    public class PostViewModel
    {
    }

    public class PostCreateViewModel
    {
        public PostCreateViewModel()
        {
            Post = new Post();
        }
        public Post Post { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ReportPostViewModel
    {
        public int PostID { get; set; }
        public Post Post { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }
        public int? PostPage { get; set; }
    }
}