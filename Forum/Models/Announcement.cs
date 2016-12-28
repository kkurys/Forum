using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Treść")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy, dddd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}