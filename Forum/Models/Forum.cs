using Forum.Content.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Forum
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        [Display(Name = "Forum")]
        public string Name { get; set; }
        [Display(Name = "Topics", ResourceType = typeof(Resources))]
        public int TopicCount { get; set; }
        [Display(Name = "Posts", ResourceType = typeof(Resources))]
        public int PostCount { get; set; }

        [Display(Name = "Public", ResourceType = typeof(Resources))]
        public bool IsPublic { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<User> Moderators { get; set; }
    }
}