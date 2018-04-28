using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Common.Exceptions;
using Newtonsoft.Json;

namespace DontForgetTheEggs.Web.Infrastructure.Validation
{
    public class EggsHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!(filterContext.Exception is EggsDataException eggsDataException))
            {
                base.OnException(filterContext);
                return;
            }

            filterContext.Controller.ViewData.ModelState.AddModelError("", eggsDataException.Message);
            filterContext.ExceptionHandled = true;

            //Note: this code below is very similar to the previous.
            // Unfortunatly we can't extract it to a base method since the arguments are diferent 
            // ActionExecutingContext vs ResultExecutedContext and their base class ControllerContext 
            // has no Result property so we can set the results

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
            }

            //model state is not valid and we're being requested by a POST not async
            filterContext.Result = new ViewResult
            {
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };
        }
    }
}