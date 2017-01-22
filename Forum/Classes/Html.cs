using Forum.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Forum.Classes
{
    static public class Html
    {
        static public bool MarkerValidate(string marker)
        {
            bool result = false;
            
            Regex reg = new Regex("<[/]?[a-zA-Z0-9]+>");
            //Regex reg = new Regex("<[^>]*>");
            if (reg.IsMatch(marker))
            {
                result = true;
            }

            return result;
        }

        static private bool IsAllowed(HtmlNode node, List<HtmlMarker> allowedMarkers)
        {
            bool result = false;
            
            foreach (var pattern in allowedMarkers)
            {
                if (node.Name == "#text" || (node.Name == pattern.Code && pattern.Active == true))
                {
                    result = true;
                }
            }

            return result;
        }

        static public string EditMarkers(string toEdit)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var allowedMarkers = db.HtmlMarkers.ToList();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(toEdit);

            List<HtmlNode> nodesToDelete = new List<HtmlNode>();

            foreach (HtmlNode node in doc.DocumentNode.Descendants())
            {
                if (!IsAllowed(node, allowedMarkers))
                {
                    nodesToDelete.Add(node);
                }
            }
            nodesToDelete.Reverse();
            foreach (HtmlNode node in nodesToDelete)
            {
                node.ParentNode.RemoveChild(node, true);
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}