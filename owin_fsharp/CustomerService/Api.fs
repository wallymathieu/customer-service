namespace Customers
open System

[<Interface>]
type ICustomerService = 
    interface
    abstract GetAllCustomers : unit -> CustomerOutput
    abstract SaveCustomers : CustomerInput -> CustomerOutput
end
