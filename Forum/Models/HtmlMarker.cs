using Forum.Classes;
using Forum.Content.Localization;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Models
{
    [HtmlMarkerValidation]
    public class HtmlMarker
    {
        public int ID { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }
        [AllowHtml]
        [Display(Name = "Marker", ResourceType = typeof(Resources))]
        public string Code { get; set; }
        [Display(Name = "Active", ResourceType = typeof(Resources))]
        public bool Active { get; set; }
    }
}