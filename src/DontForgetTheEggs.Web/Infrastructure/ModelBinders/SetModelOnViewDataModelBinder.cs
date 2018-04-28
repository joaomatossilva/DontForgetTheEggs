using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DontForgetTheEggs.Web.Infrastructure.ModelBinders
{
    //This model binder set the Model on the ViewData Context in order to be able to be accessed on the OnActionExecuting action filters
    //Taken from https://stackoverflow.com/a/25250239/4634243
    public class SetModelOnViewDataModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            controllerContext.Controller.ViewData.Model = bindingContext.Model;
            base.OnModelUpdated(controllerContext, bindingContext);
        }
    }
}