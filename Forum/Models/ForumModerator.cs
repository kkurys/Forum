using Forum.Content.Localization;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ForumModerator
    {
        public int ID { get; set; }
        [Display(Name = "User", ResourceType = typeof(Resources))]
        public string UserID { get; set; }
        [Display(Name = "Forum")]
        public int ForumID { get; set; }
        public virtual User User { get; set; }
        public virtual Forum Forum { get; set; }
    }
}