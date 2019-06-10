using Invoke.CustomerServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Invoke
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CustomerServiceClient(new NetHttpBinding(),new EndpointAddress("http://localhost:50779/CustomerService.svc"));
            var customer = new Customer
            {
                AccountNumber = 1,
                FirstName = "Oskar",
                LastName = "GewalliZ"
            };

            var result = client.SaveCustomerAsync(customer).Result;



            Console.WriteLine(result);
        }
    }
}
