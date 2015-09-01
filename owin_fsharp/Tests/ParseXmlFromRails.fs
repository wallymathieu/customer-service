namespace Tests
open NUnit.Framework
open FsUnit
open Customers
open System.Text
open System.IO

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

        let asStream (value:string)=
            let s = new MemoryStream()
            let w = new StreamWriter(s)
            w.Write(value)
            w.Flush()
            s.Seek(0L, SeekOrigin.Begin) |> ignore
            s

        [<Test>] member test.
         ``CanParse`` ()=
            using (asStream xml)
            (fun stream->
                match Serializer.deserialize(stream) with 
                    | CustomerInput.Single _ -> Assert.Fail("expected multiple")
                    | CustomerInput.Multiple cs -> cs |> List.head |> should equal customer
            )