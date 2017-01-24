using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Classes
{
    static public class Search
    {
        static public Dictionary<Post, Topic> SearchPostsAnd(List<Post> postsList, string[] keywordList, bool sizeDoesMatter)
        {
            Dictionary<Post, Topic> result = new Dictionary<Post, Topic>();

            foreach (var post in postsList)
            {
                bool all = true;

                foreach (var word in keywordList)
                {
                    if (!sizeDoesMatter)
                    {
                        if (!post.Content.ToUpper().Contains(word.ToUpper()))
                        {
                            all = false;
                            break;
                        }
                    }
                    else
                    {
                        if (!post.Content.Contains(word))
                        {
                            all = false;
                            break;
                        }
                    }
                }

                if (all == true)
                {
                    if (!result.Keys.Contains(post))
                    {
                        result.Add(post, post.Topic);
                    }
                }
            }

            return result;
        }

        static public Dictionary<Post, Topic> SearchPostsOr(List<Post> postsList, string[] keywordList, bool sizeDoesMatter)
        {
            Dictionary<Post, Topic> result = new Dictionary<Post, Topic>();

            foreach (var post in postsList)
            {
                foreach (var word in keywordList)
                {
                    if (!sizeDoesMatter)
                    {
                        if (post.Content.ToUpper().Contains(word.ToUpper()))
                        {
                            if (!result.Keys.Contains(post))
                            {
                                result.Add(post, post.Topic);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (post.Content.Contains(word))
                        {
                            if (!result.Keys.Contains(post))
                            {
                                result.Add(post, post.Topic);
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        static public Dictionary<Post, Topic> SearchOpossite(Dictionary<Post, Topic> allPosts, Dictionary<Post, Topic> toRemove)
        {
            Dictionary<Post, Topic> result = allPosts;

            foreach (var item in toRemove)
            {
                result.Remove(item.Key);
            }

            return result;
        }

        static public bool IsAllowed(string toCheck)
        {
            if (toCheck == null) return true;

            ApplicationDbContext db = new ApplicationDbContext();
            List<Dictionary> dictionary = db.Dictionary.ToList();

            bool result = true;
          
            foreach (Dictionary dict in dictionary)
            {
                if (toCheck.ToUpper().Contains(dict.ForbiddenWord.ToUpper()))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}