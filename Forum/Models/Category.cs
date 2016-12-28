using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Kategoria")]
        public string Name { get; set; }

    }
}