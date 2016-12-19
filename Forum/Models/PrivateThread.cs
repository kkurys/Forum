namespace Forum.Models
{
    public class PrivateThread
    {
        public int ID { get; set; }
        public string SenderID { get; set; }
        public string RecipientID { get; set; }
        public string Title { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }

    }
}