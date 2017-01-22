using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumTopicsViewModel
    {
        public Forum Forum { get; set; }
        public IPagedList<Topic> Topics { get; set; }

    }
}