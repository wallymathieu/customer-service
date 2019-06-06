using Customers;
using System.Linq;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.ServiceModel;
using Microsoft.AspNetCore.Hosting;

namespace Tests
{
    public class ContractTests
    {
        static ContractTests()
        {
            Task.Run(() =>
            {
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseUrls("http://localhost:5151")
                    .UseStartup<TestStartupWithOneCustomer>()
                    .Build();

                host.Run();
            }).Wait(1000);
        }

        private ICustomerService CreateClient()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(new Uri("http://localhost:5151/CustomerService.svc"));
            var channelFactory = new ChannelFactory<ICustomerService>(binding, endpoint);
            var serviceClient = channelFactory.CreateChannel();
            return serviceClient;
        }

        [Fact]
        public async Task GetAllCustomers()
        {
            var client = CreateClient();
            var allCustomers = await client.GetAllCustomersAsync();
            Assert.NotEmpty(allCustomers.Customer);
        }

        [Fact]
        public async Task SaveCustomer()
        {
            var client = CreateClient();
            var customer = new Customer
            {
                AccountNumber = 1,
                FirstName = "Oskar",
                LastName = "GewalliZ"
            };
            var result = await client.SaveCustomerAsync(customer);
            Console.WriteLine(result);
            result.Should().BeTrue();
            var allCustomers = await client.GetAllCustomersAsync();

            Assert.Equal("GewalliZ", allCustomers.Customer.Single().LastName);
        }


    }
}

