﻿using Forum.Content.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class SearchViewModel
    {
        public string Keywords { get; set; }
        //public string Authors { get; set; }
        [Display(Name = "AllTogether", ResourceType = typeof(Resources))]
        public bool KeywordsAll { get; set; }
        [Display(Name = "Negation", ResourceType = typeof(Resources))]
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