namespace Customers
open System
open System.Runtime.Serialization
open System.Xml.Serialization
open System.Xml.Schema
open System.Xml

[<Interface>]
type ICustomerService = 
    interface
    abstract GetAllCustomers : unit -> CustomerOutput
    abstract SaveCustomers : CustomerInput -> CustomerOutput
end
