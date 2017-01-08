using Forum.Classes;
using Forum.Content.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ModeratorsListViewModel
    {
        public List<Forum> Forums { get; set; }
        [Display(Name = "User", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resources))]
        [UserExists]
        public string Username { get; set; }
        public int ForumID { get; set; }
        public int? ActiveForum { get; set; }
    }

}