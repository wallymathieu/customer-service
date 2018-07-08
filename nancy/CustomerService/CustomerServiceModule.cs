using System;
using Nancy;
using Nancy.Responses;
using Nancy.ModelBinding;

namespace Customers
{
    public class CustomerServiceModule:Nancy.NancyModule
    {
        public CustomerServiceModule(ICustomerService customerService)
        {
            Get("/CustomerService.svc/GetAllCustomers", args =>
                {
                    var model = customerService.GetAllCustomers();
                    return Negotiate
                        .WithModel(model);
                });
            Post("/CustomerService.svc/SaveCustomer",args =>
                {
                    var customer = this.Bind<Customer>();
                    var model = customerService.SaveCustomer(customer);
                    return Negotiate
                        .WithModel(model);
                });
        }
    }
}

