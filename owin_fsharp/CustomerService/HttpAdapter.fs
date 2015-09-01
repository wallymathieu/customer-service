namespace Customers
open System
open Microsoft.Owin;
open Owin;
open System.IO
open System.Threading.Tasks

module OwinAdapter=
    let appMap (path: string) (map : IAppBuilder->unit) (app: IAppBuilder): IAppBuilder =
        app.Map(path, map)
    let appRun func (b: IAppBuilder) =
        b.Run(Func<_,_>(func))
    let appMapWhen when' then' (b: IAppBuilder) =
        b.MapWhen(Func<_,_>(when'), Action<_>(then'))
open OwinAdapter

type HttpAdapter(svc: ICustomerService) = 
    let svc = svc
    let indexHtml = """
<!DOCTYPE html>
<html>
<head>
    <title>Customer service</title>
    <meta charset="utf-8" />
</head>
<body>
Copy of 
<a href="http://www.galasoft.ch/labs/Customers/CustomerService.svc">galasoft CustomerService</a>
used in order to demonstrate mvvm.
You can mock implementation of the api here:
  <a href="/CustomerService.svc">Customer service</a>.
</body>
</html>
"""


    let Index(context: IOwinContext) : Task =
            context.Response.ContentType <- "text/html; charset=utf-8"
            //Console.WriteLine("Index")
            context.Response.WriteAsync(indexHtml)

    let SaveCustomer (context: IOwinContext) : Task =
            if (context.Request.Method.Equals("POST")) then
                context.Response.ContentType <- "application/xml"
                let c = Serializer.deserialize(context.Request.Body);
                context.Response.WriteAsync(Serializer.serialize(svc.SaveCustomers(c)))
            else
                context.Response.StatusCode <- 404;
                context.Response.WriteAsync("Not found")
        
    let GetAllCustomers (context: IOwinContext) : Task = 
        context.Response.ContentType <- "application/xml"
        context.Response.WriteAsync(Serializer.serialize(svc.GetAllCustomers()))

    let DoesNotExist (context: IOwinContext) : Task =
        context.Response.StatusCode <- 404;
        context.Response.WriteAsync("Not found")

    member __.Configuration (app: IAppBuilder)=
        let matchIndex (ctx: IOwinContext)=
            ctx.Request.Path.Value.Equals("/")

        app
            |> appMap "/index.html" (appRun Index)
            |> appMap "/CustomerService.svc" (fun b' -> 
                    b'|> appMap "/GetAllCustomers" (appRun GetAllCustomers)
                      |> appMap "/SaveCustomer" (appRun SaveCustomer)
                      |> ignore
                 )
            |> appMapWhen matchIndex (appRun Index)
            |> ignore
