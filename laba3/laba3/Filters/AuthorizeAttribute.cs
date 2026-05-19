using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace laba3.Filters
{
    // Base authorization filter mirroring System.Web.Mvc.AuthorizeAttribute (lab requirement).
    public abstract class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var httpContext = new HttpContextBase(context.HttpContext);
                if (!AuthorizeCore(httpContext))
                {
                    HandleUnauthorizedRequest(context);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                context.Result = HandleErrorAttribute.CreateViewResult(context, ex);
            }
            catch (FormatException ex)
            {
                context.Result = FormatExceptionFilterAttribute.CreateViewResult(context, ex);
            }
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationFilterContext context)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }

        protected abstract bool AuthorizeCore(HttpContextBase httpContext);
    }
}
