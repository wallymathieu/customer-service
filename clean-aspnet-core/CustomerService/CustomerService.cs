using System;
using System.Linq;

namespace Customers
{
    public class CustomerService : ICustomerService
    {
        public ArrayOfCustomer GetAllCustomers()
        {
            return new ArrayOfCustomer
            {
                Customer = new[] 
                    {
                        new Customer { AccountNumber=1, FirstName = "Oskar", LastName = "Gewalli" },
                        new Customer { AccountNumber=2,FirstName = "Greta", LastName = "Skogsberg" },
                        new Customer { AccountNumber=3,FirstName = "Matthias", LastName = "Wallisson" },
                        new Customer { AccountNumber=4,FirstName = "Ada", LastName = "Lundborg" },
                        new Customer { AccountNumber=5,FirstName = "Daniel", LastName = "Örnvik" },
                        new Customer { AccountNumber=6,FirstName = "Johan", LastName = "Irisson" },
                        new Customer { AccountNumber=7,FirstName = "Edda", LastName = "Tyvinge" },
                    }
            };
        }

        public Customer GetCustomerByAccountNumber(int accountNumber)
        {
            return GetAllCustomers().Customer
                .SingleOrDefault(c => c.AccountNumber == accountNumber);
        }

        public ArrayOfCustomer GetCustomers(string lastName)
        {
            return new ArrayOfCustomer
            { 
                Customer= GetAllCustomers().Customer
                    .Where(c => c.LastName == lastName).ToArray()
            };
        }

        public bool SaveCustomer(Customer editedCustomer)
        {
            return false;
        }

        public bool SaveCustomerLastName(string accountNumber, string newName)
        {
            return false;
        }
    }
}
