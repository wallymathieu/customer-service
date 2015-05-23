using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Customers
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomerService: ICustomerService
    {
        public Customer[] GetAllCustomers()
        {
            return new[] { new Customer { FirstName="Oskar", LastName="Gewalli" } };
        }

        public Customer GetCustomerByAccountNumber(int accountNumber)
        {
            throw new NotImplementedException();
        }

        public Customer[] GetCustomers(string lastName)
        {
            throw new NotImplementedException();
        }

        public bool SaveCustomer(Customer editedCustomer)
        {
            throw new NotImplementedException();
        }

        public bool SaveCustomerLastName(string accountNumber, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
