using System;
using Nancy;

namespace Customers
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get("/",args => Negotiate.WithView("Index"));
        }
    }
}

