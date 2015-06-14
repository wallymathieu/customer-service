using System;
using System.Web;
using System.IO;


namespace Customers
{
    public class HttpRequestWrapper:IHttpRequest
    {
        private HttpRequest request;
        public HttpRequestWrapper(HttpRequest request)
        {
            this.request = request;
        }

        public Uri Url
        {
            get
            {
                return this.request.Url;
            }
        }

        public Stream InputStream
        {
            get
            {
                return this.InputStream;
            }
        }
    }
}

