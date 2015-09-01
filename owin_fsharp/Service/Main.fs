namespace Customers
open Customers
open System
open Microsoft.Owin.Hosting

#if COMPILED

module Main=
    let uri = "http://localhost:8080/"
    [<EntryPoint>]
    let main argv =
        using (WebApp.Start<HttpAdapter>(uri))
              (fun s->
                    Console.WriteLine("Started")
                    Console.ReadKey()|> ignore
                    Console.WriteLine("Stopping")
              )
        0

#endif



