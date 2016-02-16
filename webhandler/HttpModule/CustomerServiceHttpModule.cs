using Customers;
using System;
using HttpApplication= System.Web.HttpApplication;

namespace HttpModule
{
    public class CustomerServiceHttpModule : System.Web.IHttpModule
    {
        public void Dispose()
        {
        }
        private readonly Serializer serializer;
        private readonly ICustomerService svc;
        public CustomerServiceHttpModule()
            : this(null, null)
        { }

        public CustomerServiceHttpModule(Serializer serializer, ICustomerService svc)
        {
            this.serializer = serializer ?? new Serializer();
            this.svc = svc ?? new CustomerService();
        }


        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.Application_BeginRequest;
            context.EndRequest += this.Application_EndRequest;
        }

        private void Application_EndRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            //throw new NotImplementedException();
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            ProcessRequest(HttpUtility.Wrap(application.Context));
        }

        public void ProcessRequest(IHttpContext context)
        {
            var command = HttpUtility.GetLastPath(context.Request.Url);
            switch (command)
            {
                case "GetAllCustomers":
                    context.Response.ContentType = "application/xml";
                    context.Response.BinaryWrite(serializer.Serialize(svc.GetAllCustomers()));
                    return;
                case "SaveCustomer":
                    if (context.Request.HttpMethod.Equals("POST"))
                    {
                        context.Response.ContentType = "application/xml";
                        var c = serializer.Deserialize<Customer>(context.Request.InputStream);
                        context.Response.BinaryWrite(serializer.Serialize(svc.SaveCustomer(c)));
                        return;
                    }
                    break;
                default:
                    return;
            }
        }
    }
}
