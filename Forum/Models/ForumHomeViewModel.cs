using System.Collections.Generic;

namespace Forum.Models
{
    public class ForumHomeViewModel
    {
        public List<Announcement> Announcements
        {
            get;
            set;
        }

        public Dictionary<Category, List<Forum>> CategoryForums
        {
            get;
            set;
        }

    }
}