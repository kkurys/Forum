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
                if (node.Name == "#text")
                {
                    result = true;
                    break;
                }
                else
                {
                    if(node.Name == pattern.Code && pattern.Active == true)
                    {
                        if (pattern.Attribute != null && pattern.Attribute != "")
                        {
                            string nodeValue = node.GetAttributeValue(pattern.Attribute, "");
                            string styleValue = node.GetAttributeValue("style", "");
                            if (nodeValue != "")
                            {
                                if (pattern.Value != null && pattern.Value != "")
                                {
                                    if (nodeValue == pattern.Value)
                                    {
                                        result = true;
                                        break;
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                                else
                                {
                                    result = true;
                                    break;
                                }
                            }
                            else if (styleValue != "")
                            {
                                if (pattern.Value != null && pattern.Value != "")
                                {
                                    string patternValue = pattern.Attribute + ": " + pattern.Value;
                                    if (styleValue.Contains(patternValue))
                                    {
                                        result = true;
                                        break;
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                                else
                                {
                                    if (styleValue.Contains(pattern.Attribute))
                                    {
                                        result = true;
                                        break;
                                    }
                                    else
                                    {
                                        result = false;
                                    }
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = true;
                            break;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        public static void RemoveNode(HtmlNode parent, HtmlNode oldChild)
        {
            if (oldChild.ChildNodes != null)
            {
                HtmlNode previousSibling = oldChild.PreviousSibling;
                foreach (HtmlNode newChild in oldChild.ChildNodes)
                {
                    parent.InsertAfter(newChild, previousSibling);
                    previousSibling = newChild;
                }
            }
            parent.RemoveChild(oldChild);
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

            foreach (HtmlNode node in nodesToDelete)
            {
                //node.ParentNode.RemoveChild(node, true);
                RemoveNode(node.ParentNode, node);
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}