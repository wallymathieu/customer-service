namespace Tests
open Customers

    type CustomerServiceFake =
        new : (Customer []) -> CustomerServiceFake
        interface ICustomerService
        
        
