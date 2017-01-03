namespace Forum.Models
{
    public class MessageFile
    {
        public int ID { get; set; }
        public string Filename { get; set; }
        public int PrivateMessageID { get; set; }
        public virtual PrivateMessage PrivateMessage { get; set; }

    }
}