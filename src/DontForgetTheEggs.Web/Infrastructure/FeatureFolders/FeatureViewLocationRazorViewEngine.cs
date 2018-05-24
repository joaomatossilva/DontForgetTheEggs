using System.Web.Mvc;

namespace DontForgetTheEggs.Web.Infrastructure.FeatureFolders
{
    public class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        public FeatureViewLocationRazorViewEngine()
        {
            ViewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml"
            };

            MasterLocationFormats = ViewLocationFormats;

            PartialViewLocationFormats = new[]
            {
                "~/Features/{1}/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml"
            };

            AreaViewLocationFormats = new[]
            {
                "~/Features/{2}/{1}/{0}.cshtml",
                "~/Features/{2}/Shared/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Features/{2}/{1}/{0}.cshtml",
                "~/Features/{2}/Shared/{0}.cshtml"
            };
        }
    }
}