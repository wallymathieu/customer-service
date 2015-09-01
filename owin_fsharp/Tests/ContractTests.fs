namespace Tests
open System
open NUnit.Framework
open FsUnit
open Customers
open System.Text
open FluentAssertions
open FluentAssertions.Xml
open System.Xml.Linq
open Microsoft.Owin.Testing
open System.Net.Http
open System.IO

    [<TestFixture>] 
    type ``ContractTests``() =
        let customer = {Customer.Empty with AccountNumber = 1; FirstName = "Oskar"; LastName = "Gewalli" }
        let ns =  "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.datacontract.org/2004/07/Customers\""
        let serializedCustomers = String.Format( @"<ArrayOfCustomer {0}>
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
            let s = new MemoryStream()
            let w = new StreamWriter(s)
            w.Write(buffer)
            w.Flush()
            s.Seek(0L, SeekOrigin.Begin) |> ignore
            s

        let should_be_xml_equivalent expected actual =
            ignore(XDocument.Parse(actual).Should()
                    .BeEquivalentTo(XDocument.Parse(expected)))

        let context c=
            let svc = new CustomerServiceFake(c)
            TestServer.Create(fun b-> 
                let a = new HttpAdapter(svc)
                a.configuration(b)
                )
 
        let GETXml (server: TestServer) (url:string)=
            server.HttpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        let POSTXml (server: TestServer) (url:string) c=
            server.HttpClient.PostAsync(url, c).Result.Content.ReadAsStringAsync().Result;



        [<Test>] member test.
         ``get all customers`` ()=
            let result = GETXml (context([|customer|])) "/CustomerService.svc/GetAllCustomers" in
                should_be_xml_equivalent serializedCustomers result
                    
        [<Test>] member test.
         ``save customer`` ()=
            using (new StreamContent(serialized customer))
            (fun s->
                let result = POSTXml (context([|customer|])) "/CustomerService.svc/SaveCustomer" s 
                should_be_xml_equivalent @"<boolean>true</boolean>" result
            )
