using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Models
{
    public enum Language
    {
        Polish,
        English,
        Russian,
        German
    }
    public enum PostsPerPage
    {
        [Display(Name = "10")]
        Ten,
        [Display(Name = "25")]
        TwentyFive,
        [Display(Name = "50")]
        Fifty
    }
    public class User : IdentityUser
    {
        public Language Language { get; set; }
        public PostsPerPage PostsPerPage { get; set; }
        public TimeSpan SessionTime { get; set; }
        public string AvatarFilename { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}