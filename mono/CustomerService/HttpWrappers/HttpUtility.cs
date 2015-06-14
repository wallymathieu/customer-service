using System;
using System.Linq;
using System.Web;


namespace Customers
{
    public class HttpUtility
    {
        public HttpUtility()
        {
        }

        public static string GetLastPath(string path)
        {
            return (path ?? "").Split(new []{'/'}, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        public static IHttpContext Wrap(HttpContext context)
        {
            return new HttpContextWrapper(context);
        }
    }
}

