using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class UserDetails
    {
        public List<IdentityRole> Roles { get; set; }
        public int PostsCount { get; set; }
        public int TopicsCount { get; set; }
    }
    public class UserIndexViewModel
    {
        public Dictionary<User, UserDetails> UserPostCount
        {
            get;
            set;
        }
    }
    public class UserDetailsViewModel
    {
        public User User { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public int PostsCount { get; set; }
        public int TopicsCount { get; set; }
    }
}