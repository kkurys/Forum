using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Forum
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int TopicCount { get; set; }
        public int PostCount { get; set; }

        [Display(Name = "Publiczne")]
        public bool IsPublic { get; set; }

        public virtual Category Category { get; set; }
    }
}