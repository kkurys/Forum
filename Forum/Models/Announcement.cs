using System;

namespace Forum.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}