using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace laba3.Filters
{
    public class PostIndexActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Determine the value of the numeric identifier parameter ("Id") from ActionParameters
            if (context.ActionArguments != null && context.ActionArguments.ContainsKey("Id"))
            {
                var val = context.ActionArguments["Id"];
                if (val is int intVal)
                {
                    if (intVal < 0)
                    {
                        // Create a ViewResult, set view name and model
                        var vr = new ViewResult {
                            ViewName = "PostIndexNegativeId",
                            ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), context.ModelState)
                        };

                        // Use ActionParameters as model
                        var model = new Dictionary<string, object>();
                        foreach (var kv in context.ActionArguments)
                        {
                            model[kv.Key] = kv.Value ?? "(null)";
                        }
                        vr.ViewData.Model = model;

                        context.Result = vr;
                    }
                }
                // If value is a nullable int boxed as int when HasValue, previous branch handles it.
            }

            base.OnActionExecuting(context);
        }
    }
}
