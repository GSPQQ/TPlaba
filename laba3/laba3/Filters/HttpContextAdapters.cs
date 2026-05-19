using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;

namespace laba3.Filters
{
    // Adapters mirroring System.Web.HttpContextBase for AuthorizeCore(HttpContextBase).
    public class HttpRequestBase
    {
        public string HttpMethod { get; }
        public NameValueCollection QueryString { get; }

        public HttpRequestBase(HttpRequest request)
        {
            HttpMethod = request.Method;
            var nvc = new NameValueCollection();
            foreach (var kv in request.Query)
            {
                nvc.Add(kv.Key, kv.Value.ToString());
            }
            QueryString = nvc;
        }
    }

    public class HttpContextBase
    {
        public HttpRequestBase Request { get; }

        public HttpContextBase(HttpContext context)
        {
            Request = new HttpRequestBase(context.Request);
        }
    }
}
