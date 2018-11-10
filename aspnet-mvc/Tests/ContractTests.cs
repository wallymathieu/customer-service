using Customers;
using System.Xml.Linq;
using System.IO;
using System.Net.Http;
using System.Linq;
using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class ContractTests
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


        [Fact]
        public async Task GetAllCustomers()
        {
            using (var adapter = TestServers.Create<TestStartupWithOneCustomer>())
            {
                var httpClient = adapter.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Accept","text/xml");
                var result = await httpClient.GetAsync("/CustomerService.svc/GetAllCustomers");
                var stringResult =await result.Content.ReadAsStringAsync();
                //Console.WriteLine(stringResult);
                XDocument.Parse(stringResult).Should().BeEquivalentTo(XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfCustomer xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://schemas.datacontract.org/2004/07/Customers"">
  <Customer>
    <AccountNumber>1</AccountNumber>
    <AddressCity xsi:nil=""true"" />
    <AddressCountry xsi:nil=""true"" />
    <AddressStreet xsi:nil=""true"" />
    <FirstName>Oskar</FirstName>
    <Gender>Male</Gender>
    <LastName>Gewalli</LastName>
    <PictureUri xsi:nil=""true"" />
  </Customer>
</ArrayOfCustomer>"));
            }
        }

        [Fact]
        public async Task SaveCustomer()
        {
            using (var adapter = TestServers.Create<TestStartupWithOneCustomer>())
            {
                var httpClient = adapter.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Accept", "text/xml");
                var customer = new Customer
                {
                    AccountNumber = 1,
                    FirstName = "Oskar",
                    LastName = "GewalliZ"
                };
                var result = await httpClient.PostAsXmlAsync("/CustomerService.svc/SaveCustomer", customer);
                var textResult = await result.Content.ReadAsStringAsync();
                //Console.WriteLine(textResult);
                XDocument.Parse(textResult).Should().BeEquivalentTo(XDocument.Parse(
                    @"<?xml version=""1.0"" encoding=""utf-8""?>
<boolean>true</boolean>"));
                var allCustomersResponse = await httpClient.GetAsync("/CustomerService.svc/GetAllCustomers");
                var allCustomersString = await allCustomersResponse.Content.ReadAsStringAsync();
                var allCustomers = new Serializer().Deserialize<ArrayOfCustomer>(allCustomersString);

                Assert.Equal("GewalliZ", allCustomers.Customer.Single().LastName);
            }
        }

        Stream AsStream(Customer customer)
        {
            var serializer = new Serializer();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var buffer = serializer.Serialize(customer);
            writer.Write(buffer);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}

