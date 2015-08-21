//"C:\Program Files (x86)\Microsoft SDKs\F#\4.0\Framework\v4.0\Fsi.exe" Script1.fsx

#r "../CustomerService/bin/Debug/FsPickler.dll"
#r "System.Runtime.Serialization.dll"
#r "System.Core.dll"
#r "System.Xml.Linq.dll"
#load "../CustomerService/Models.fs"
#load "../CustomerService/Api.fs"
#load "../paket-files/wallymathieu/Perch/lib/Enum.fs"
#load "../paket-files/wallymathieu/Perch/lib/Xml.fs"
#load "../CustomerService/Serializer.fs"
#load "../CustomerService/CustomerService.fs"
#load "../CustomerService/httpAdapter.fs"
#load "../paket-files/wallymathieu/Perch/lib/Hash.fs"
#load "CustomerServiceFake.fs"
open Customers
open Tests
open System
open System.Text

let customer = { Customer.Empty with AccountNumber = 1; FirstName = "Oskar"; LastName = "Gewalli" }

let x = HttpAdapter.GetAllCustomers(CustomerServiceFake([|customer|]))

Console.WriteLine(x)
printfn "%A" (Serializer.deserialize(Encoding.UTF8.GetBytes x))

#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data

type ArrayOfCustomer = XmlProvider<"""<?xml version="1.0" encoding="utf-8"?>
<ArrayOfCustomer xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://schemas.datacontract.org/2004/07/Customers">
  <Customer>
    <AccountNumber>1</AccountNumber>
    <AddressCity xsi:nil="true" />
    <AddressCountry xsi:nil="true" />
    <AddressStreet xsi:nil="true" />
    <FirstName>Oskar</FirstName>
    <Gender>Male</Gender>
    <LastName>Gewalli</LastName>
    <PictureUri xsi:nil="true" />
  </Customer>
  <Customer>
    <AccountNumber>2</AccountNumber>
    <AddressCity>Stockholm</AddressCity>
    <AddressCountry>Sweden</AddressCountry>
    <AddressStreet>Stureplan 1</AddressStreet>
    <FirstName>Elle</FirstName>
    <Gender>Female</Gender>
    <LastName>Lastname</LastName>
    <PictureUri>https://upload.wikimedia.org/wikipedia/commons/c/c4/PM5544_with_non-PAL_signals.png</PictureUri>
  </Customer>
</ArrayOfCustomer>""">
  

ArrayOfCustomer.AddressCity(nil= Some(true), value= None)