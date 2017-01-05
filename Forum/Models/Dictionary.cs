using Forum.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class Dictionary
    {
        public int ID { get; set; }
        [OnlyWord]
        [Display(Name = "Słowo")]
        public string ForbiddenWord { get; set; }
        [Display(Name = "Zakazane")]
        public bool IsForbidden { get; set; }
    }
}