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

                var topics = Topics.ToList();

                if (topics.Count == 0)
                {
                    return null;
                }
                topics.OrderByDescending(x => x.LastPostDate);
                return topics[0];
            }
        }
    }
}