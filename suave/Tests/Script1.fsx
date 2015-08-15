//"C:\Program Files (x86)\Microsoft SDKs\F#\4.0\Framework\v4.0\Fsi.exe" Script1.fsx

#r "../CustomerService/bin/Debug/FsPickler.dll"
#r "System.Runtime.Serialization.dll"
#r "System.Core.dll"
#r "System.Xml.Linq.dll"
#load "../CustomerService/Models.fs"
#load "../CustomerService/Api.fs"
#load "../CustomerService/Xml.fs"
#load "../CustomerService/Enum.fs"
#load "../CustomerService/Serializer.fs"
#load "../CustomerService/CustomerService.fs"
#load "../CustomerService/httpAdapter.fs"
#load "Dict.fs"
#load "CustomerServiceFake.fs"
open Customers
open Tests
open System
open System.Text

let customer = { Customer.Empty with AccountNumber = 1; FirstName = "Oskar"; LastName = "Gewalli" }

let x = HttpAdapter.GetAllCustomers(CustomerServiceFake([|customer|]))

Console.WriteLine(x)
printfn "%A" (Serializer.deserialize(Encoding.UTF8.GetBytes x))