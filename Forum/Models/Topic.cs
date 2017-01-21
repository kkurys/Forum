using Forum.Classes;
using Forum.Content.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Forum.Models
{
    [IsAllowed]
    public class Topic : IComparable<Topic>
    {
        public int ID { get; set; }
        public int ForumID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources))]
        public string Title { get; set; }
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { get; set; }
        [Display(Name = "Glued", ResourceType = typeof(Resources))]
        public bool IsGlued { get; set; }
        [Display(Name = "Posts", ResourceType = typeof(Resources))]
        public int PostCount { get; set; }
        [Display(Name = "Views", ResourceType = typeof(Resources))]
        public int ViewsCount { get; set; }
        public DateTime LastPostDate { get; set; }
        public virtual User User { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public Post LastPost
        {
            get
            {
                var posts = Posts.ToList();
                if (posts.Count == 0) return null;
                posts.OrderByDescending(x => x.Date);
                return posts[0];
            }
        }
        public int CompareTo(Topic other)
        {
            return other.LastPostDate.CompareTo(this.LastPostDate);
        }
    }
}