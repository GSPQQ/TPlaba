using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace laba3.Filters
{
    // Variant A: ArgumentOutOfRangeException — HandleErrorAttribute, FormatException — custom filter.
    public class HandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentOutOfRangeException ex)
            {
                context.Result = CreateViewResult(context, ex);
                context.ExceptionHandled = true;
            }
        }

        public static ViewResult CreateViewResult(FilterContext context, ArgumentOutOfRangeException ex)
        {
            var model = new Dictionary<string, object>
            {
                ["ExceptionMessage"] = ex.Message,
                ["QueryCount"] = context.HttpContext.Request.Query.Count,
                ["Query"] = context.HttpContext.Request.Query.ToDictionary(
                    k => k.Key,
                    v => v.Value.ToString())
            };

            return new ViewResult
            {
                ViewName = "ArgumentOutOfRangeError",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    Model = model
                }
            };
        }
    }

    public class FormatExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is FormatException ex)
            {
                context.Result = CreateViewResult(context, ex);
                context.ExceptionHandled = true;
            }
        }

        public static ViewResult CreateViewResult(FilterContext context, FormatException ex)
        {
            var model = new Dictionary<string, object>
            {
                ["ExceptionMessage"] = ex.Message,
                ["shortVal"] = context.HttpContext.Request.Query["shortVal"].ToString(),
                ["doubleVal"] = context.HttpContext.Request.Query["doubleVal"].ToString()
            };

            return new ViewResult
            {
                ViewName = "FormatError",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    Model = model
                }
            };
        }
    }
}
