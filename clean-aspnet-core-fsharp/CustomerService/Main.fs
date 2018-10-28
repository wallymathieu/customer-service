module CustomerService.Main
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Customers

let buildWebHost(args:string[]) =
            WebHost.CreateDefaultBuilder(args)
                .Configure(fun app->HttpAdapter.configuration ( CustomerService() ) app)
                .Build();
[<EntryPoint>]
let main argv =
    buildWebHost(argv).Run()
    0