namespace Tests
open NUnit.Framework
open FsUnit
open Customers
open System.Text

    [<TestFixture>] 
    type ``ParseXmlFromRails``() =
        let customer = {Customer.Empty with AccountNumber = 1; FirstName = "Oskar"; LastName = "Gewalli" }

        let xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<ArrayOfCustomer xmlns=""http://schemas.datacontract.org/2004/07/Customers"">
    <Customer>
        <AccountNumber>1</AccountNumber>
        <AddressCity nil=""true""/>
        <AddressCountry nil=""true""/>
        <AddressStreet nil=""true""/>
        <FirstName>Oskar</FirstName>
        <Gender>Male</Gender>
        <LastName>Gewalli</LastName>
        <PictureUri nil=""true""/>
    </Customer>
</ArrayOfCustomer>"
        
        [<Test>] member test.
         ``CanParse`` ()=
            match Serializer.deserialize(xml |> Encoding.UTF8.GetBytes) with 
                | CustomerInput.Single _ -> Assert.Fail("expected multiple")
                | CustomerInput.Multiple cs -> cs |> List.head |> should equal customer

