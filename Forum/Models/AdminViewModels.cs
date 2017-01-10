using Forum.Classes;
using Forum.Content.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ModeratorsListViewModel
    {
        public List<Category> Categories { get; set; }
        [Display(Name = "User", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Resources))]
        [UserExists]
        public string Username { get; set; }
        public int? ActiveCategory { get; set; }
        public int? ActiveForum { get; set; }
        public int? AdditionError { get; set; }
    }
    public class PartialModeratorsListViewModel
    {
        public int ForumID { get; set; }
        public Forum Forum { get; set; }
        public ICollection<User> Moderators { get; set; }
        public string Username { get; set; }
        public string UserID { get; set; }
        public int? AdditionError { get; set; }
    }
}