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
        [Display(Name = "Znacznik")]
        [AllowHtml]
        public string Code { get; set; }
    }
}