using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Forum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Users.Find(User.Identity.GetUserId());

            if (user != null && user.Language != Language.Browser)
            {
                string culture = "";
                switch (user.Language)
                {
                    case Language.Polski:
                        culture = "pl-PL";
                        break;
                    case Language.Deutsch:
                        culture = "de-DE";
                        break;
                    case Language.English:
                        culture = "en-US";
                        break;

                }

                CultureInfo ci = CultureInfo.GetCultureInfo(culture);

                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
        }
        protected void Session_Start()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var user = db.Users.Find(User.Identity.GetUserId());

            Session["ViewedTopicsIDs"] = new List<int>();
            if (user != null)
            {
                Session.Timeout = user.SessionTime.Minutes;
            }

        }
    }
}
