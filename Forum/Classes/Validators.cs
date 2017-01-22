using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                return new ValidationResult("Proszę wpisać tylko nazwę znacznika, bez nawiasów oraz atrybutów!");
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
                return new ValidationResult("Słowo może być tylko jedno i ma się składać z samych liter");
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
            return new ValidationResult("Treść zawiera niedozwolone słowa!");
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

            return new ValidationResult("Nie ma takiego użytkownika!");
        }
    }
}