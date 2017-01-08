using Forum.Content.Localization;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources))]
        public string Name { get; set; }

    }
}