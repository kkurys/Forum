using Forum.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Models
{
    public class HtmlMarker
    {
        public int ID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [AllowHtml]
        [HtmlMarkerValidation]
        [Display(Name = "Znacznik")]
        public string Code { get; set; }
        [Display(Name = "Aktywny")]
        public bool Active { get; set; }
    }
}