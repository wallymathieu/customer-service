namespace Customers
open System
open System.IO
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder;
open Microsoft.AspNetCore.Hosting;
open Microsoft.AspNetCore.Http;
open Microsoft.Extensions.DependencyInjection

module HttpAdapter=
    let appMap (path: string) (map : IApplicationBuilder->unit) (app: IApplicationBuilder): IApplicationBuilder =
        app.Map(PathString(path), Action<_>(map))
    let appRun func (b: IApplicationBuilder) =
        b.Run(RequestDelegate(func))
    let appMapWhen when' then' (b: IApplicationBuilder) =
        b.MapWhen(Func<_,_>(when'), Action<_>(then'))
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


    let index(context: HttpContext) : Task =
            context.Response.ContentType <- "text/html; charset=utf-8"
            //Console.WriteLine("Index")
            context.Response.WriteAsync(indexHtml)

    let saveCustomer (svc: ICustomerService) (context: HttpContext) : Task =
            if (context.Request.Method.Equals("POST")) then
                context.Response.ContentType <- "application/xml"
                let c = Serializer.deserialize(context.Request.Body);
                context.Response.WriteAsync(Serializer.serialize(svc.SaveCustomers(c)))
            else
                context.Response.StatusCode <- 404
                context.Response.WriteAsync("Not found")
        
    let getAllCustomers (svc: ICustomerService) (context: HttpContext) : Task = 
        context.Response.ContentType <- "application/xml"
        context.Response.WriteAsync(Serializer.serialize(svc.GetAllCustomers()))

    let doesNotExist (context: HttpContext) : Task =
        context.Response.StatusCode <- 404;
        context.Response.WriteAsync("Not found")

    let configuration (svc: ICustomerService) (app: IApplicationBuilder)=
        let matchIndex (ctx: HttpContext)=
            ctx.Request.Path.Value.Equals("/")

        app
            |> appMap "/index.html" (appRun index)
            |> appMap "/CustomerService.svc" (fun b' -> 
                    b'|> appMap "/GetAllCustomers" (appRun (getAllCustomers svc))
                      |> appMap "/SaveCustomer" (appRun (saveCustomer svc))
                      |> ignore
                 )
            |> appMapWhen matchIndex (appRun index)
            |> ignore
