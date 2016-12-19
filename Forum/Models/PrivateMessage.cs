using System;
using System.Collections.Generic;

namespace Forum.Models
{
    public class PrivateMessage
    {
        public int ID { get; set; }
        public int PrivateThreadID { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<PostFile> MessageFiles { get; set; }
    }
}