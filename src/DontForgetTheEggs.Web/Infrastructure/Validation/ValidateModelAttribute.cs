using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DontForgetTheEggs.Web.Infrastructure.Validation
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid)
            {
                return;
            }

            //model state is not valid, and we're returning from a GET request
            if (filterContext.HttpContext.Request.HttpMethod == "GET")
            {
                var result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                filterContext.Result = result;
            }

            //model state is not valid and we're being requested by an async POST 
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var result = new ContentResult();
                string content = JsonConvert.SerializeObject(filterContext.Controller.ViewData.ModelState,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result.Content = content;
                result.ContentType = "application/json";

                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.Result = result;
                return;
            }

            //model state is not valid and we're being requested by a POST not async
            filterContext.Result = new ViewResult
            {
                ViewName = filterContext.ActionDescriptor.ActionName,
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };
        }
    }
}