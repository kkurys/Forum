namespace Forum.Models
{
    public class PostFile
    {
        public int ID { get; set; }
        public string Filename { get; set; }
        public int PostID { get; set; }

        public Post Post { get; set; }
    }
}