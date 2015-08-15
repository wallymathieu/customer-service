namespace Tests
open Customers
open System.Reflection


    type CustomerServiceFake (allCustomers)=
        let mutable allCustomers = allCustomers
        let replace xs f sub = 
          let processItem (found,list) x =
            if found then (true,x::list) 
            elif f x then (true,(sub x)::list) 
            else (false,x::list)
          let (found, list) = xs |> List.fold processItem (false,[])
          if found then Some(List.rev list)
          else None

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
                    let ids = Dict.fromSeq accNr editedCustomers
                    let replaced = replace (allCustomers|> Array.toList) (fun c-> Dict.tryGet ids  (accNr(c)) |> Option.isSome ) (fun c-> Dict.get ids (accNr(c)))
                    match replaced with
                        | Option.None -> CustomerOutput.Success(false)
                        | Option.Some cs -> allCustomers <-  cs |> List.toArray; CustomerOutput.Success(true)
                else
                    CustomerOutput.Success(false)
                
                
                
                
