using Forum.Models;
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
            if (reg.IsMatch(marker))
            {
                result = true;
            }

            return result;
        }

        static private string MakeEndMarker(string startMarker)
        {
            string endMarker = startMarker.Insert(1, "/");
            return endMarker;
        }

        static private bool IsAllowed(string marker)
        {
            bool result = false;
            
            ApplicationDbContext db = new ApplicationDbContext();
            var tmpList = db.HtmlMarkers.ToList();
            List<string> allMarkersList = new List<string>();

            foreach (var item in tmpList)
            {
                if (item.Active)
                {
                    allMarkersList.Add(item.Code);
                    allMarkersList.Add(MakeEndMarker(item.Code));
                }
            }

            foreach (var pattern in allMarkersList)
            {
                if (marker == pattern)
                {
                    result = true;
                }
            }

            return result;
        }

        static public string EditMarkers(string toEdit)
        {
            //toEdit.Replace(Environment.NewLine, "<br>");
            //toEdit = Regex.Replace(toEdit, @"\r\n?|\n", "<br>");
            //var wordList = toEdit.Split();
            var wordList = Regex.Split(toEdit, @"(<[/]?[a-zA-Z0-9]+>)");
            List<string> finalWordList = new List<string>();

            foreach (var word in wordList)
            {
                if (MarkerValidate(word))
                {
                    if (IsAllowed(word))
                    {
                        finalWordList.Add(word);
                    }
                }
                else
                {
                    finalWordList.Add(word);
                }
            }
            string finalString = string.Join("", finalWordList.ToArray());

            return finalString;
        }
    }
}