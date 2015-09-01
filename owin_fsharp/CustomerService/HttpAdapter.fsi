namespace Customers
open Microsoft.Owin;
open Owin;

type HttpAdapter =
    new :ICustomerService -> HttpAdapter 
    member configuration : IAppBuilder -> unit
