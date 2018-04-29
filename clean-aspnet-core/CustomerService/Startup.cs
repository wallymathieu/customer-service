using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Customers
{
    public class Startup
    {
        private ICustomerService svc;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            this.svc = new CustomerService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/index.html", b => b.Run(Index));

            app.Map("/CustomerService.svc", b1 =>
            {
                b1.Map("/GetAllCustomers", b => b.Run(GetAllCustomers));
                b1.Map("/SaveCustomer", b => b.Run(SaveCustomer));
            });

            app.MapWhen(ctx => ctx.Request.Path.Value.Equals("/"), b => b.Run(Index));

        }

        private Task Index(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            //Console.WriteLine("Index");
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

        public Task SaveCustomer(HttpContext context)
        {
            if (context.Request.Method.Equals("POST"))
            {
                if (context.Request.TryReadFromXml<Customer>(out var customer))
                {
                    context.Response.ContentType = "application/xml";
                    var result = svc.SaveCustomer(customer);
                    context.Response.WriteXml(result);
                    return Task.CompletedTask;
                }
                else 
                { 
                    context.Response.StatusCode = 400;
                    return context.Response.WriteAsync("Bad request");
                }
            }
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Not found");
        }

        public Task GetAllCustomers(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            var res = svc.GetAllCustomers();
            context.Response.WriteXml(res);
            return Task.CompletedTask;
        }
    }
}
