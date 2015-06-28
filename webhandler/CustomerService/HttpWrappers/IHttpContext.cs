using System;

namespace Customers
{
    public interface IHttpContext
    {
        IHttpRequest Request{get;}
        IHttpResponse Response{get;}
    }
}

