using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Topic
    {
        public int ID { get; set; }
        public int ForumID { get; set; }
        public string UserID { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
        public bool IsGlued { get; set; }
        public int PostCount { get; set; }
        public int ViewsCount { get; set; }

        public virtual User User { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

    }
}