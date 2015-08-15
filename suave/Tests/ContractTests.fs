namespace Tests
open System
open NUnit.Framework
open FsUnit
open Customers
open System.Text
open FluentAssertions
open FluentAssertions.Xml
open System.Xml.Linq

    [<TestFixture>] 
    type ``ContractTests``() =
        let customer = {Customer.Empty with AccountNumber = 1; FirstName = "Oskar"; LastName = "Gewalli" }
        let ns =  "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.datacontract.org/2004/07/Customers\""
        let serializedCustomers = String.Format( @"<?xml version=""1.0"" encoding=""utf-8""?>
<ArrayOfCustomer {0}>
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
</ArrayOfCustomer>", ns)

        let serialized customer =
            let buffer = Serializer.serialize( CustomerOutput.Single(customer) )
            Encoding.UTF8.GetBytes(buffer)
        
        let should_be_xml_equivalent expected actual =
            ignore(XDocument.Parse(actual).Should()
                    .BeEquivalentTo(XDocument.Parse(expected)))

        [<Test>] member test.
         ``get all customers`` ()=
            let svc = CustomerServiceFake([|customer|]) in
                should_be_xml_equivalent serializedCustomers (HttpAdapter.GetAllCustomers(svc))
                    
        [<Test>] member test.
         ``save customer`` ()=
            let svc = CustomerServiceFake([|customer|]) in
                let bytes = serialized customer in
                    should_be_xml_equivalent @"<?xml version=""1.0"" encoding=""utf-8""?>
                            <boolean>true</boolean>" (HttpAdapter.SaveCustomer svc bytes)

