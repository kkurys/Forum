using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class SearchViewModel
    {
        public string Keywords { get; set; }
        //public string Authors { get; set; }
        [Display(Name = "Wszystkie razem")]
        public bool KeywordsAll { get; set; }
        [Display(Name = "Negacja")]
        public bool Negation { get; set; }
        public bool SizeDoesMatter { get; set; }

    }



    public class SearchResultViewModel
    {
        public SearchViewModel SearchProperties { get; set; }
        public Dictionary<Post, Topic> PostTopic
        {
            get;
            set;
        }

    }
}