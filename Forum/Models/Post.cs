using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Post
    {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string UserID { get; set; }

        [Display(Name = "Treść")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual ICollection<PostFile> PostFiles { get; set; }
    }
}