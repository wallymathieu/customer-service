namespace Customers
open System  
type CustomerService () = 
    let trim (x: string) = x.Trim()
    let splitIntoColumns (line:string) =
        line.Split([|','|]) |> Seq.map trim |> Seq.toList
    let mapToCustomer (columns: string list)=
         {Customer.Empty with 
            AccountNumber = columns |> List.head |> Int32.Parse ; 
            FirstName = List.nth columns 1; 
            LastName = List.nth columns 2;
         }

    let cs = """1,Oskar,Gewalli,
            2,Greta,Skogsberg,
            3,Matthias,Wallisson,
            4,Ada,Lundborg,
            5,Daniel,Örnvik,
            6,Johan,Irisson,
            7,Edda,Tyvinge""".Split(separator =[|'\n'; '\r'|], options=System.StringSplitOptions.RemoveEmptyEntries)
            |> Seq.map (splitIntoColumns) 
            |> Seq.map (mapToCustomer)
        
    interface ICustomerService with
        member x.GetAllCustomers () =
            CustomerOutput.Multiple( cs |> Seq.toList )

        member x.SaveCustomers (cs: CustomerInput)=
            CustomerOutput.Success(false)

