using System.Collections.Generic;

namespace Forum.Models
{
    public class PrivateThread
    {
        public int ID { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public string Title { get; set; }
        public bool Seen { get; set; } = false;
        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
        public virtual ICollection<PrivateMessage> PrivateMessages { get; set; }

    }
}