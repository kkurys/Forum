using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Forum
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        [Display(Name = "Forum")]
        public string Name { get; set; }
        [Display(Name = "Tematów")]
        public int TopicCount { get; set; }
        [Display(Name = "Postów")]
        public int PostCount { get; set; }

        [Display(Name = "Publiczne")]
        public bool IsPublic { get; set; }

        public virtual Category Category { get; set; }
    }
}