using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DontForgetTheEggs.Web.HtmlHelpers
{
    public static class DropDownListsExtensions
    {
        public static MvcHtmlString AjaxDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression, string remoteUrl, string initClass, string selectedText = null)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(
                expression, helper.ViewData
            );

            var hiddenElementName = modelMetadata.PropertyName + "SelectedText";
            var formData = helper.ViewContext.RequestContext.HttpContext.Request.Form;
            //check to see if we got a deafult text value posted
            if (!string.IsNullOrEmpty(formData[hiddenElementName]))
            {
                selectedText = formData[hiddenElementName];
            }

            //load the value
            // ReSharper disable once MergeConditionalExpression
            // - using the Resharper's `var value = (TProperty) modelMetadata.Model` recomendation results into a NRE because TProperty might not be nullable
            var value = (TProperty)(modelMetadata.Model ?? default(TProperty));
            var selectedValue = value?.ToString();
            if (!string.IsNullOrEmpty(formData[modelMetadata.PropertyName]))
            {
                selectedValue = formData[modelMetadata.PropertyName];
            }

            //build the initial options if we have any default value
            List<SelectListItem> options = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(selectedValue))
            {
                var option = new SelectListItem
                {
                    Selected = true,
                    Text = selectedText ?? selectedValue,
                    Value = selectedValue
                };
                options.Add(option);
            }

            var result = helper.Hidden(hiddenElementName, selectedText).ToString();
            result += helper.DropDownListFor(expression, options, new
            {
                data_ajax__url = remoteUrl,
                @class = "form-control " + initClass,
                data_selected_element = hiddenElementName
            });
            return new MvcHtmlString(result);
        }
    }
}