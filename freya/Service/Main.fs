namespace Customers

open Freya.Core
open Arachne.Http
open Arachne.Http.Cors
open Arachne.Uri.Template
open Freya.Core
open Freya.Core.Operators
open Freya.Machine
open Freya.Machine.Extensions.Http
open Freya.Machine.Extensions.Http.Cors
open Freya.Machine.Router
open Freya.Router
open Microsoft.Owin.Hosting

#if COMPILED

(* module BoilerPlateForForm =[<System.STAThread>] do () *)
//    do System.Windows.Forms.Application.Run()

    module Main=
        let private c = new CustomerService()
        let index = HttpAdapter.index
        let getAllCustomers = 
             //>>= Writers.setMimeType "application/xml" 
             HttpAdapter.GetAllCustomers(c)
        let common =
            freyaMachine {
                using http
                using httpCors
                charsetsSupported utf8
                corsHeadersSupported corsHeaders
                corsOriginsSupported corsOrigins
                languagesSupported en
                mediaTypesSupported json }
        let todos =
            freyaMachine {
                including common
                corsMethodsSupported todosMethods
                methodsSupported todosMethods
                doDelete clearAction
                doPost addAction
                handleCreated addedHandler
                handleOk listHandler } |> FreyaMachine.toPipeline
        let routes =
            freyaRouter {
                resource (UriTemplate.Parse "/") index 
                resource (UriTemplate.Parse "/CustomerService.svc/GetAllCustomers") getAllCustomers 

            } |> FreyaRouter.toPipeline

        type App ()=
            member __.Configuration () =
            OwinAppFunc.ofFreya (routes)
            
            choose 
                [ GET >>= choose
                    [ path "/CustomerService.svc/GetAllCustomers"
                         >>= Writers.setMimeType "application/xml" 
                         >>= OK(HttpAdapter.GetAllCustomers(c))
                      path "/" >>= OK HttpAdapter.index
                    ]
                  POST >>= choose
                    [ path "/CustomerService.svc/SaveCustomer" 
                        >>= request (fun req -> OK(HttpAdapter.SaveCustomer c req.rawForm)) ]
                ]

        [<EntryPoint>]
        let main _ = 
            let _ = WebApp.Start<App> ("http://localhost:7000")
            let _ = System.Console.ReadLine ()
            0


#endif



