using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Treść")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}