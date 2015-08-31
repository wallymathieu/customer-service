using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;
using System;
using System.IO;

namespace Customers
{
    public class Startup
    {
        private readonly Serializer serializer;
        private readonly ICustomerService svc;
        public Startup()
            : this(null, null)
        { }

        public Startup(Serializer serializer, ICustomerService svc)
        {
            this.serializer = serializer ?? new Serializer();
            this.svc = svc ?? new CustomerService();
        }

        public void Configuration(IAppBuilder app)
        {
            app.Map("/index.html", b =>b.Run(Index));

            app.Map("/CustomerService.svc", b1=>{
                b1.Map("/GetAllCustomers", b=> b.Run(GetAllCustomers));
                b1.Map("/SaveCustomer", b => b.Run(SaveCustomer));
            });

            app.Map("", b => b.Run(Index));
        }

        private Task Index(IOwinContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            Console.WriteLine("Index");
            return context.Response.WriteAsync(@"
<!DOCTYPE html>
<html>
<head>
    <title>Customer service</title>
    <meta charset=""utf-8"" />
</head>
<body>
Copy of 
<a href=""http://www.galasoft.ch/labs/Customers/CustomerService.svc"">galasoft CustomerService</a>
used in order to demonstrate mvvm.
You can mock implementation of the api here:
  <a href=""/CustomerService.svc"">Customer service</a>.
</body>
</html>
");
        }

        public Task SaveCustomer(IOwinContext context)
        {
            if (context.Request.Method.Equals("POST"))
            {
                context.Response.ContentType = "application/xml";
                var c = serializer.Deserialize<Customer>(context.Request.Body);
                return context.Response.WriteAsync(serializer.Serialize(svc.SaveCustomer(c)));
            }
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Not found");
        }

        public Task GetAllCustomers(IOwinContext context)
        {
            context.Response.ContentType = "application/xml";
            return context.Response.WriteAsync(serializer.Serialize(svc.GetAllCustomers()));
        }
    }
}
