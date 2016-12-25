using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumTopicsViewModel
    {
        public Forum Forum { get; set; }
        public List<Topic> Topics { get; set; }

    }
}