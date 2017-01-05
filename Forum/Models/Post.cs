using Forum.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    public class Post
    {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string UserID { get; set; }

        [IsAllowed]
        [AllowHtml]
        [Display(Name = "Treść")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy, dddd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<PostFile> PostFiles { get; set; }
    }
}