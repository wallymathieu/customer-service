using Customers;
using System.Xml.Linq;
using System.IO;
using System.Net.Http;
using System.Linq;
using System;
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

                var svcFake = new CustomerServiceFake();
                svcFake.AllCustomers.Add(new Customer
                {
                    AccountNumber = 1,
                    FirstName = "Oskar",
                    LastName = "Gewalli"
                });
                this.svc = svcFake;
            }
        }


        [Fact]
        public void GetAllCustomers()
        {

            using (var adapter = TestServers.Create<TestStartupWithOneCustomer>())
            {
                var result = adapter.CreateClient().GetAsync("/CustomerService.svc/GetAllCustomers").Result;
                var stringResult = result.Content.ReadAsStringAsync().Result;
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
        public void SaveCustomer()
        {
            using (var adapter = TestServers.Create<TestStartupWithOneCustomer>())
            {

                using (var c = AsStream(new Customer
                {
                    AccountNumber = 1,
                    FirstName = "Oskar",
                    LastName = "GewalliZ"
                }))
                {
                    var result = adapter.CreateClient().PostAsync("/CustomerService.svc/SaveCustomer", new StreamContent(c)).Result;
                    var textResult = result.Content.ReadAsStringAsync().Result;
                    //Console.WriteLine(textResult);
                    XDocument.Parse(textResult).Should().BeEquivalentTo(XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<boolean>true</boolean>"));
                    var allCustomersString = adapter.CreateClient().GetAsync("/CustomerService.svc/GetAllCustomers").Result.Content.ReadAsStringAsync().Result;
                    var allCustomers = new Serializer().Deserialize<ArrayOfCustomer>(allCustomersString);

                    Assert.Equal("GewalliZ", allCustomers.Customer.Single().LastName);
                }
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

