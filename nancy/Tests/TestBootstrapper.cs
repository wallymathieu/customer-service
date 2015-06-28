using System;
using Customers;
using Nancy.TinyIoc;

namespace Tests
{
    public class TestBootstrapper: Bootstrapper 
    {
        private readonly ICustomerService customerService;
        public TestBootstrapper(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            // Perform registation that should have an application lifetime
            existingContainer
                .Register<ICustomerService>(customerService);
        }
    }
}

