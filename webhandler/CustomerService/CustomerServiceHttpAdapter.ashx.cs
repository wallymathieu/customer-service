using System;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Xml.Serialization;
using System.IO;


namespace Customers
{
    public class CustomerServiceHttpAdapter : System.Web.IHttpHandler
    {
        private readonly Serializer serializer;
        private readonly ICustomerService svc;
        public CustomerServiceHttpAdapter()
            : this(null, null)
        {}

        public CustomerServiceHttpAdapter(Serializer serializer, ICustomerService svc)
        {
            this.serializer = serializer ?? new Serializer();
            this.svc = svc ?? new CustomerService();
        }
        
        public void ProcessRequest(HttpContext context)
        {
            ProcessRequest(HttpUtility.Wrap(context));
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
                    context.Response.StatusCode = 404;
                    context.Response.Write("Not found");
                    return;
            }
            context.Response.StatusCode = 404;
            context.Response.Write("Not found");
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
    
