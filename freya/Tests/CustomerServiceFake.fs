namespace Tests
open Customers
open System.Reflection
open Perch

    type CustomerServiceFake (allCustomers)=
        let mutable allCustomers = allCustomers
        let replace xs f = 
            let processItem list x =
                if f x |> Option.isSome then (f x |> Option.get)::list
                else x::list
            xs |> List.fold processItem []

        interface ICustomerService with
            member x.GetAllCustomers () =
                CustomerOutput.Multiple(allCustomers |> Array.toList)

            member x.SaveCustomers (input: CustomerInput)=
                let accNr (a:Customer) =
                    a.AccountNumber

                let sameAccountNumber a b=
                    accNr(a) = accNr(b)
                
                let editedCustomers=
                    match input with
                        | CustomerInput.Single c -> [c]
                        | CustomerInput.Multiple cs -> cs

                if editedCustomers |> List.isEmpty |> not then
                    let ids = Hash.fromSeq accNr id editedCustomers
                    let replaced = replace (allCustomers|> Array.toList) 
                                    (fun c-> ids |> Hash.tryGet (accNr(c))) // what to replace
                                    
                    allCustomers <-  replaced |> List.toArray
                    CustomerOutput.Success(true)
                else
                    CustomerOutput.Success(false)
                
                
                
                
