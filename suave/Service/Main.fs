namespace Customers
open Suave
open Suave.Web
open Suave.Http
open Suave.Types
open Suave.Http.Successful
open Suave.Http.Redirection
open Suave.Http.Files
open Suave.Http.RequestErrors
open Suave.Http.Applicatives
#if COMPILED

(* module BoilerPlateForForm =[<System.STAThread>] do () *)
//    do System.Windows.Forms.Application.Run()

    module Main=
        let private c = new CustomerService()
        let app : WebPart =
            choose 
                [ GET >>= choose
                    [ path "/CustomerService.svc/GetAllCustomers" >>= Writers.setMimeType "application/xml" >>= OK(HttpAdapter.GetAllCustomers(c))
                      path "/" >>= OK HttpAdapter.index
                    ]
                  POST >>= choose
                    [ path "/CustomerService.svc/SaveCustomer" >>= request (fun req -> OK(HttpAdapter.SaveCustomer c req.rawForm)) ]
                ]

        [<EntryPoint>]
        let main argv =
          startWebServer defaultConfig app
          0


#endif



