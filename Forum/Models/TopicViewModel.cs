using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }
        public List<Post> Posts { get; set; }
    }
}