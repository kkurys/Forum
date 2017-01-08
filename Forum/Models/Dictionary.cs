using Forum.Classes;
using Forum.Content.Localization;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Dictionary
    {
        public int ID { get; set; }
        [OnlyWord]
        [Display(Name = "Word", ResourceType = typeof(Resources))]
        public string ForbiddenWord { get; set; }
        [Display(Name = "Forbidden", ResourceType = typeof(Resources))]
        public bool IsForbidden { get; set; }
    }
}