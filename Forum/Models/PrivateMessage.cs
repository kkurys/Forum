using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Forum.Models
{
    public class PrivateMessage
    {
        public int ID { get; set; }
        public int PrivateThreadID { get; set; }
        public string AuthorID { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public User Author { get; set; }
        public virtual PrivateThread PrivateThread { get; set; }
        public virtual ICollection<MessageFile> MessageFiles { get; set; }
    }
}