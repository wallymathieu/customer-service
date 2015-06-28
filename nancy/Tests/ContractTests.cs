using System;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using FluentAssertions;
using System.Xml.Linq;
using Nancy.Bootstrapper;
using Customers;

namespace Tests
{
    [TestFixture]
    public class ContractTests
    {
        private CustomerServiceFake svc;
        private INancyBootstrapper bootstrapper;
        private Browser browser;
        [SetUp]
        public void BeforeEachTest()
        {
            svc = new CustomerServiceFake();
            bootstrapper = new TestBootstrapper(svc);
            browser = new Browser(bootstrapper, defaults: to => to.Accept("application/xml"));
        }

        [Test]
        public void Should_return_status_ok_for_root_url()
        {
            // When
            var result = browser.Get("/", with => {
                with.HttpRequest();
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void Should_GetAllCustomers()
        {
            // Given
            svc.AllCustomers.Add(new Customer
                { 
                    AccountNumber = 1, 
                    FirstName = "Oskar", 
                    LastName = "Gewalli" 
                });
            
            // When
            var response = browser.Get("/CustomerService.svc/GetAllCustomers", (with) => {
                with.HttpRequest();
            });

            // Then
            var responseXml = response.BodyAsXml();

            responseXml.Should().BeEquivalentTo(XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
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

        [Test]
        public void SaveCustomer()
        {
            // Given
            svc.AllCustomers.Add(new Customer
                { 
                    AccountNumber = 1, 
                    FirstName = "Oskar", 
                    LastName = "Gewalli" 
                });

            // When
            var response = browser.Post("/CustomerService.svc/SaveCustomer", (with) => {
                with.HttpRequest();
                with.XMLBody(new Customer
                    { 
                        AccountNumber = 1, 
                        FirstName = "Oskar", 
                        LastName = "Gewalli" 
                    });
            });

            // Then
            response.BodyAsXml().Should().BeEquivalentTo(XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<boolean>true</boolean>"));
        }
    }
}

