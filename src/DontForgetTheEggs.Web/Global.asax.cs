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
using DontForgetTheEggs.Web.Infrastructure.ModelBinders;

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

            //This prevents the default adding for non nullable members, because it will cause duplication
            //with the fluentvalidation ones and it causes an error
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            //Model Binder to set the Model on action executing - See the class documentation for more details
            ModelBinders.Binders.DefaultBinder = new SetModelOnViewDataModelBinder();
        }
    }
}
