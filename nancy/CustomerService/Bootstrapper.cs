using System;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

namespace Customers
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            // Perform registation that should have an application lifetime

            existingContainer
                .Register<ICustomerService, CustomerService>();

        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime

            base.ConfigureRequestContainer(container, context);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
        }
    }
}

