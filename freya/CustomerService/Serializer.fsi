namespace Customers
open System

    
    module Serializer = 
        val serialize : CustomerOutput -> string
        val deserialize : Byte[] -> CustomerInput

