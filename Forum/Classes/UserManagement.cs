using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Classes
{
    public static class UserManagement
    {
        public static string GetRank(int posts)
        {
            if (posts < 5)
            {
                return "Lodołamacz";
            }
            else if (posts >= 5 && posts < 10)
            {
                return "Bywalec";
            }
            else if (posts >= 10 && posts < 100)
            {
                return "Swój";
            }
            else if (posts >= 100 && posts < 1000)
            {
                return "Stały klient";
            }
            else
            {
                return "Wierny";
            }
        }
    }
}