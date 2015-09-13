namespace FsPickler.XmlFormat.Tests

namespace Tests
open System
open NUnit.Framework
open FsUnit
open System.Text
open FluentAssertions
open FluentAssertions.Xml
open System.Xml.Linq
open System.IO

open Models
open FsPickler.XmlFormat

    [<TestFixture>] 
    type ``Should be able to serialize and deserialize xml``() =
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

        let serialize c=
            let xmlSerializer = XmlSerializer.Create(indent = true)
            using(new MemoryStream())
             (fun m ->
                let w = new StreamWriter(m)
                xmlSerializer.Serialize (w, c, leaveOpen=true)
                w.Flush()
                m.Seek(0L, SeekOrigin.Begin) |> ignore
                let r = new StreamReader(m)
                r.ReadToEnd()
                )

        let should_be_xml_equivalent expected actual =
            ignore(XDocument.Parse(actual).Should()
                    .BeEquivalentTo(XDocument.Parse(expected)))

        [<Test>] member test.
         ``serialize it`` ()=
                should_be_xml_equivalent serializedCustomers (serialize customer)
