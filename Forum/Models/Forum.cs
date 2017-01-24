using Forum.Content.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public virtual ICollection<Topic> Topics { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<User> Moderators { get; set; }
        public Topic LastTopic
        {
            get
            {
                if (Topics != null && Topics.Count > 0)
                {
                    var topics = Topics.ToList();
                    topics = topics.OrderByDescending(x => x.LastPost.Date).ToList();
                    return topics[0];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}