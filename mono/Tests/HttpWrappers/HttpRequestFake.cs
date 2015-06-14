using System;
using Customers;
using System.IO;

namespace Tests
{
    public class HttpRequestFake:IHttpRequest
    {
        public HttpRequestFake()
        {
        }
        public Uri UrlFake;
        public Uri Url
        {
            get
            {
                return UrlFake;
            }
        }
        public Stream InputStreamFake;
        public Stream InputStream
        {
            get
            {
                return InputStreamFake;
            }
        }
        public string HttpMethodFake;
        public string HttpMethod
        {
            get
            {
                return HttpMethodFake;
            }
        }
    }
}

