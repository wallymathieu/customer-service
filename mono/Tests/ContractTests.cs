using System;
using NUnit.Framework;
using Customers;
using FluentAssertions;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class ContractTests
    {
        private CustomerServiceHttpAdapter adapter;
        private HttpContextFake context;
        private CustomerServiceFake svc;

        [SetUp]
        public void Init()
        {
            svc = new CustomerServiceFake();
            adapter = new CustomerServiceHttpAdapter(new Serializer(), svc);
            context = new HttpContextFake();
        }

        [Test]
        public void GetAllCustomers()
        {
            svc.AllCustomers.Add(new Customer
                { 
                    AccountNumber = 1, 
                    FirstName = "Oskar", 
                    LastName = "Gewalli" 
                });
            context.RequestFake.UrlFake = new Uri("http://localhost/CustomerService.svc/GetAllCustomers/");
            adapter.ProcessRequest(context);
            context.ResponseFake.AsXml().Should().BeEquivalentTo(XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
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
            svc.AllCustomers.Add(new Customer
                { 
                    AccountNumber = 1, 
                    FirstName = "Oskar", 
                    LastName = "Gewalli" 
                });
            context.RequestFake.UrlFake = new Uri("http://localhost/CustomerService.svc/SaveCustomer/");
            using (SetAsInputStream(new Customer
                { 
                    AccountNumber = 1, 
                    FirstName = "Oskar", 
                    LastName = "Gewalli" 
                }))
            {
                adapter.ProcessRequest(context);
                Console.WriteLine(context.ResponseFake.Content.ToString());
                context.ResponseFake.AsXml().Should().BeEquivalentTo(XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<boolean>true</boolean>"));
            }
        }

        Stream SetAsInputStream(Customer customer)
        {
            var serializer = new Serializer();
            var stream = new MemoryStream();
            var writer=new StreamWriter(stream);

            var buffer = serializer.Serialize(customer);
            writer.Write(Encoding.UTF8.GetString(buffer));
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            context.RequestFake.InputStreamFake = stream;
            return stream;
        }
    }
}

