namespace Customers
open System
open System.IO
    
    module Serializer = 
        val serialize : CustomerOutput -> string
        val deserialize : Stream -> CustomerInput

    