using Forum.Classes;
using Forum.Content.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class PrivateMessagesViewModels
    {
    }
    public class PrivateThreadsListViewModel : UserDetailsViewModel
    {
        public List<PrivateThread> Threads
        {
            get;
            set;
        }
        public int PrivateThreadsCount
        {
            get
            {
                return Threads.Count;
            }
        }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

    }
    public class PrivateThreadViewModel : UserDetailsViewModel
    {
        public PrivateThread PrivateThread { get; set; }
        public List<PrivateMessage> Messages { get; set; }
        [Display(Name = "Content", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }

    }
    public class CreateThreadViewModel : UserDetailsViewModel
    {
        [Display(Name = "Recipient", ResourceType = typeof(Resources))]
        [Required]
        [UserExists]
        public string Recipient
        {
            get;
            set;
        }
        [Display(Name = "Topic", ResourceType = typeof(Resources))]
        public string Title
        {
            get;
            set;
        }
        [Display(Name = "Content", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Content
        {
            get;
            set;
        }
        [Display(Name = "Attachments", ResourceType = typeof(Resources))]
        public List<MessageFile> MessageFiles
        {
            get;
            set;
        }
    }
    public static class PrivateThreadExtensionMethods
    {
        public static int PostCount(this PrivateThread thread)
        {
            return thread.PrivateMessages.Count;
        }
        public static User LastPostAuthor(this PrivateThread thread)
        {
            if (thread.PrivateMessages.Count > 0)
            {
                var lastMessage = new List<PrivateMessage>(thread.PrivateMessages)[thread.PrivateMessages.Count - 1];
                return lastMessage.Author;
            }
            return null;
        }
        public static string LastPostDate(this PrivateThread thread)
        {
            if (thread.PrivateMessages.Count > 0)
            {
                var lastMessage = new List<PrivateMessage>(thread.PrivateMessages)[thread.PrivateMessages.Count - 1];
                return lastMessage.Date.ToString("G");
            }
            return null;
        }
    }


}