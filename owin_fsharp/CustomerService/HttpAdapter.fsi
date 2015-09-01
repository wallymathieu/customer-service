namespace Customers
open Microsoft.Owin;
open Owin;

type HttpAdapter =
    new :ICustomerService -> HttpAdapter 
    member Configuration : IAppBuilder -> unit
