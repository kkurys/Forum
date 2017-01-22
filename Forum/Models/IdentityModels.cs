using Forum.Content.Localization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace Forum.Models
{
    public enum Language
    {
        [Display(Name = "Default", ResourceType = typeof(Resources))]
        Browser,
        Polski,
        English,
        Deutsch
    }

    public enum Theme
    {
        Default,
        Darkly,
        United
    }

    public class User : IdentityUser
    {
        [Display(Name = "Language", ResourceType = typeof(Resources))]
        public Language Language { get; set; }
        public Theme Theme { get; set; }
        [Display(Name = "PostsPerPage", ResourceType = typeof(Resources))]
        public int? PostsPerPageID { get; set; }
        [Display(Name = "PostsPerPage", ResourceType = typeof(Resources))]
        public virtual PostsPerPage PostsPerPage { get; set; }
        [Display(Name = "SessionTime", ResourceType = typeof(Resources))]
        public TimeSpan SessionTime { get; set; }
        [Display(Name = "Avatar")]
        public string AvatarFilename { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaims(new[]
            {
                new Claim("Theme",this.Theme.ToString())
            });

            return userIdentity;
        }

        public ICollection<PrivateMessage> PrivateMessages { get; set; }
        public virtual ICollection<Forum> Forums { get; set; }
    }

    

    public static class UserExtensionMethods
    {
        public static string GetTheme(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Theme");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : "Default";
        }

        public static void AddUpdateClaim(this IIdentity identity, string value)
        {
            if (identity == null)
                return;

            // check for existing claim and remove it
            var claim = ((ClaimsIdentity)identity).FindFirst("Theme");
            if (claim != null)
                ((ClaimsIdentity)identity).RemoveClaim(claim);

            // add new claim
            ((ClaimsIdentity)identity).AddClaim(new Claim("Theme", value));

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
        }


        //public static string GetTheme(this User user)
        //{
        //    return user.Theme.ToString();
        //}
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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Forum> Fora { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostFile> PostFiles { get; set; }
        public DbSet<MessageFile> MessageFiles { get; set; }
        public DbSet<PrivateThread> PrivateThreads { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<PostsPerPage> PostsPerPage { get; set; }
        public DbSet<HtmlMarker> HtmlMarkers { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
    }
}