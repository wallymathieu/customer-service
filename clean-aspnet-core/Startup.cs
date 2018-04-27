using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Customers
{
    public class Startup
    {
        JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        });
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

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
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
                context.Response.ContentType = "application/xml";
                using (var reader = new StreamReader(context.Request.Body))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var c = serializer.Deserialize<Customer>(jsonReader);
                    var res = svc.SaveCustomer(c);
                    using (var writer = new StreamWriter(context.Response.Body))
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {
                        serializer.Serialize(jsonWriter, res);
                    }
                    return Task.CompletedTask;
                }
            }
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Not found");
        }

        public Task GetAllCustomers(HttpContext context)
        {
            context.Response.ContentType = "application/xml";
            var res = svc.GetAllCustomers();
            using (var writer = new StreamWriter(context.Response.Body))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, res);
            }
            return Task.CompletedTask;
        }
    }
}
