using System.Web;
using System.Web.Mvc;
using DontForgetTheEggs.Web.Infrastructure.Validation;

namespace DontForgetTheEggs.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //This default attribute is replaced by the EggsHandleErrorAttribute below
            // I kept it here for the sake of documenting it
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new EggsHandleErrorAttribute());
            filters.Add(new ValidateModelAttribute());
        }
    }
}
