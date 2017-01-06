using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}