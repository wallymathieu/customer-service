using Customers;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    class TestStartupWithOneCustomer : Startup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            var svcFake = new CustomerServiceFake();
            svcFake.AllCustomers.Add(new Customer
            {
                AccountNumber = 1,
                FirstName = "Oskar",
                LastName = "Gewalli"
            });
            services.AddSingleton<ICustomerService>(svcFake);
        }
    }
}

