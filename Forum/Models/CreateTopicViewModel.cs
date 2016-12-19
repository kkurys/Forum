using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class CreateTopicViewModel
    {
        public Topic Topic { get; set; }
        public Post Post { get; set; }
        public int ForumID { get; set; }
    }
}