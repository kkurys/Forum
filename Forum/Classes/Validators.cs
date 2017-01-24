using Forum.Content.Localization;
using Forum.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Forum.Classes
{
    public class HtmlMarkerValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (HtmlMarker)validationContext.ObjectInstance;

            if (model.Code.All(Char.IsLetter))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(Resources.HtmlMarkerError);
            }
        }
    }

    public class OnlyWord : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Dictionary)validationContext.ObjectInstance;

            if (model.ForbiddenWord.All(Char.IsLetter))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(Resources.OnlyWordError);
            }
        }
    }

    public class IsAllowed : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is Post)
            {
                Post model = (Post)validationContext.ObjectInstance;
                if (Search.IsAllowed(model.Content))
                {
                    return ValidationResult.Success;
                }
            }
            else if (validationContext.ObjectInstance is Topic)
            {
                Topic model = (Topic)validationContext.ObjectInstance;
                if (Search.IsAllowed(model.Description) && Search.IsAllowed(model.Title))
                {
                    return ValidationResult.Success;
                }
            }
            else if (validationContext.ObjectInstance is TopicViewModel)
            {
                var model = (TopicViewModel)validationContext.ObjectInstance;
                if (Search.IsAllowed(model.Content))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(Resources.UnallowedContent);
        }
    }
    public class UserExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is CreateThreadViewModel)
            {
                ApplicationDbContext db = new ApplicationDbContext();

                var model = (CreateThreadViewModel)validationContext.ObjectInstance;
                if (db.Users.ToList().Find(user => user.UserName == value as string) != null)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(Resources.UserDoesntExist);
        }
    }
    public class ValidAvatar : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is HttpPostedFileBase)
            {
                var avatar = value as HttpPostedFileBase;
                var fileName = Path.GetFileName(avatar.FileName);
                Stream stream = new MemoryStream();
                avatar.InputStream.CopyTo(stream);
                WebImage img = new WebImage(stream);
                if (img.Width > 192 || img.Height > 192 | avatar.ContentLength > 128 * 1024)
                {
                    return new ValidationResult(Resources.UploadedAvatarError);
                }
                return ValidationResult.Success;
            }
            return new ValidationResult(Resources.UploadedAvatarError);
        }
    }
}