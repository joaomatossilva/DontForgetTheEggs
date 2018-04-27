using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DontForgetTheEggs.Migrations;
using DontForgetTheEggs.Web.Infrastructure.FeatureFolders;

namespace DontForgetTheEggs.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Experimental - Run migrations on startup
            MigrationsRunner.MigrateToLatest(ConfigurationManager.ConnectionStrings["ApplicationDb"].ConnectionString);

            //Set up the view locator for the Feature Folders layout
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }
    }
}
