using Forum.Content.Localization;
using System;
using System.ComponentModel.DataAnnotations;
namespace Forum.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Title", ResourceType = typeof(Resources))]
        public string Title { get; set; }

        [Display(Name = "Content", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Date", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy, dddd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}