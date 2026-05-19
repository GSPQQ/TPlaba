using System;

namespace laba3.Filters
{
    // Variant 9: short, double — потомок AuthorizeAttribute с AuthorizeCore(HttpContextBase).
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext?.Request?.QueryString == null)
                throw new ArgumentOutOfRangeException(nameof(httpContext), "Query string is not available.");

            var qs = httpContext.Request.QueryString;

            // GET без параметров — показать форму (часть I).
            if (string.Equals(httpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)
                && qs.Count == 0)
            {
                return true;
            }

            if (qs.Count < 2)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(qs),
                    qs.Count,
                    "В строке запроса должно быть не менее двух переменных.");
            }

            // Convert / Parse — при ошибке конвертации выбрасывается FormatException.
            _ = Convert.ToInt16(qs["shortVal"]);
            _ = Convert.ToDouble(qs["doubleVal"]);

            return true;
        }
    }
}
