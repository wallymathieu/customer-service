// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.
#r "../packages/Owin/lib/net40/Owin.dll"
#r "../packages/Microsoft.Owin/lib/net45/Microsoft.Owin.dll"
#r "../packages/Microsoft.Owin.Hosting/lib/net45/Microsoft.Owin.Hosting.dll"
#r "../packages/Microsoft.Owin.Host.HttpListener/lib/net45/Microsoft.Owin.Host.HttpListener.dll"
#r "System.Xml"
#r "System.Xml.Linq"
#load "../paket-files/wallymathieu/Perch/lib/Xml.fs"
#load "../paket-files/wallymathieu/Perch/lib/Enum.fs"
#load "Models.fs"
#load "Api.fs"
#load "CustomerService.fs"
#load "Serializer.fs"
#load "HttpAdapter.fs"
open Customers
open System
open Microsoft.Owin.Hosting
module Script=
    let uri = "http://localhost:8080/"

    using (WebApp.Start<HttpAdapter>(uri))
          (fun s->
                Console.WriteLine("Started")
                Console.ReadKey()|> ignore
                Console.WriteLine("Stopping")
          )