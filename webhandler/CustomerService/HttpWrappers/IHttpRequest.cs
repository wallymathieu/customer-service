using System;
using System.IO;

namespace Customers
{
    public interface IHttpRequest
    {
        Uri Url{get;}
        Stream InputStream{get;}
        string HttpMethod{ get;}
    }
}

