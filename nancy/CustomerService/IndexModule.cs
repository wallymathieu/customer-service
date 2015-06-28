using System;
using Nancy;

namespace Customers
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ =>
                {
                    return Negotiate
                        .WithView("Index.html");
                };
        }
    }
}

