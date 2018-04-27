using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace DontForgetTheEggs.Web.Infrastructure.FeatureFolders
{
    public class FeatureControllerFactory : DefaultControllerFactory
    {
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            return typeof(FeatureControllerFactory).Assembly.GetType($"DontForgetTheEggs.Web.Features.{controllerName}.{controllerName}Controller");
        }
    }
}