namespace Customers
open System
open Microsoft.Owin;
open Owin;
open System.IO
open System.Threading.Tasks

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
            context.Response.ContentType <- "text/html; charset=utf-8";
            //Console.WriteLine("Index");
            context.Response.WriteAsync(indexHtml);

    let SaveCustomer (context: IOwinContext) : Task =
            if (context.Request.Method.Equals("POST")) then
                context.Response.ContentType <- "application/xml";
                let c = Serializer.deserialize(context.Request.Body);
                context.Response.WriteAsync(Serializer.serialize(svc.SaveCustomers(c)));
            else
                context.Response.StatusCode <- 404;
                context.Response.WriteAsync("Not found");
        
    let GetAllCustomers (context: IOwinContext) : Task = 
        context.Response.ContentType <- "application/xml";
        context.Response.WriteAsync(Serializer.serialize(svc.GetAllCustomers()));

    member __.configuration (app: IAppBuilder)=
        app
            .Map("/index.html", (fun b ->b.Run(Func<_,_>(Index))))
            .Map("/CustomerService.svc/GetAllCustomers", (fun b-> b.Run(Func<_,_>(GetAllCustomers))))
            .Map("/CustomerService.svc/SaveCustomer", (fun b -> b.Run(Func<_,_>(SaveCustomer))))
            .Map("", (fun b-> b.Run(Func<_,_>(Index)))) |> ignore

