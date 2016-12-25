using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public List<Forum> Fora { get; set; }
    }
}