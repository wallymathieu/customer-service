namespace Customers

open Suave
open Suave.Operators
open Suave.Filters
open Suave.Writers
open Suave.Successful

#if COMPILED

(* module BoilerPlateForForm =[<System.STAThread>] do () *)
//    do System.Windows.Forms.Application.Run()

    module Main=
        let private c = new CustomerService()
        let app : WebPart =
            let getAllCustomers = path "/CustomerService.svc/GetAllCustomers" >=> setMimeType "application/xml" >=> OK(HttpAdapter.GetAllCustomers(c))
            let getIndex = path "/" >=> OK HttpAdapter.index
            let postCustomer = path "/CustomerService.svc/SaveCustomer" >=> request (fun req -> OK(HttpAdapter.SaveCustomer c req.rawForm))
            choose 
                [ GET >=> choose [ getAllCustomers; getIndex ]
                  POST >=> choose [ postCustomer ]
                ]

        [<EntryPoint>]
        let main argv =
          startWebServer defaultConfig app
          0


#endif



