using System;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;
using System.Collections.Generic;
using Nancy.Configuration;

namespace Customers
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            // Perform registation that should have an application lifetime

            existingContainer
                .Register<ICustomerService, CustomerService>();
        }
    }
}

